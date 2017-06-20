/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Parse;
using iText.Html2pdf.Css.Pseudo;
using iText.Html2pdf.Css.Resolve.Shorthand;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Css.Validate;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.Html2pdf.Resolver.Resource;
using iText.IO.Log;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve {
    public class DefaultCssResolver : ICssResolver {
        private CssStyleSheet cssStyleSheet;

        private MediaDeviceDescription deviceDescription;

        private IList<CssFontFaceRule> fonts = new List<CssFontFaceRule>();

        public DefaultCssResolver(INode treeRoot, MediaDeviceDescription mediaDeviceDescription, ResourceResolver 
            resourceResolver) {
            this.deviceDescription = mediaDeviceDescription;
            CollectCssDeclarations(treeRoot, resourceResolver, null);
            CollectFonts();
        }

        public DefaultCssResolver(INode treeRoot, ProcessorContext context) {
            this.deviceDescription = context.GetDeviceDescription();
            CollectCssDeclarations(treeRoot, context.GetResourceResolver(), context.GetCssContext());
            CollectFonts();
        }

        public virtual IDictionary<String, String> ResolveStyles(INode element, CssContext context) {
            IList<CssDeclaration> nodeCssDeclarations = UserAgentCss.GetStyles(element);
            if (element is IElementNode) {
                nodeCssDeclarations.AddAll(HtmlStylesToCssConverter.Convert((IElementNode)element));
            }
            nodeCssDeclarations.AddAll(cssStyleSheet.GetCssDeclarations(element, deviceDescription));
            if (element is IElementNode) {
                String styleAttribute = ((IElementNode)element).GetAttribute(AttributeConstants.STYLE);
                if (styleAttribute != null) {
                    nodeCssDeclarations.AddAll(CssRuleSetParser.ParsePropertyDeclarations(styleAttribute));
                }
            }
            IDictionary<String, String> elementStyles = CssDeclarationsToMap(nodeCssDeclarations);
            String parentFontSizeStr = null;
            if (element.ParentNode() is IStylesContainer) {
                IStylesContainer parentNode = (IStylesContainer)element.ParentNode();
                IDictionary<String, String> parentStyles = parentNode.GetStyles();
                if (parentStyles == null && !(element.ParentNode() is IDocumentNode)) {
                    ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Resolve.DefaultCssResolver));
                    logger.Error(iText.Html2pdf.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES);
                }
                if (parentStyles != null) {
                    foreach (KeyValuePair<String, String> entry in parentStyles) {
                        MergeParentCssDeclaration(elementStyles, entry.Key, entry.Value);
                    }
                    parentFontSizeStr = parentStyles.Get(CssConstants.FONT_SIZE);
                }
            }
            String elementFontSize = elementStyles.Get(CssConstants.FONT_SIZE);
            if (CssUtils.IsRelativeValue(elementFontSize) || CssConstants.LARGER.Equals(elementFontSize) || CssConstants
                .SMALLER.Equals(elementFontSize)) {
                float baseFontSize;
                if (CssUtils.IsRemValue(elementFontSize)) {
                    baseFontSize = context.GetRootFontSize();
                }
                else {
                    if (parentFontSizeStr == null) {
                        baseFontSize = FontStyleApplierUtil.ParseAbsoluteFontSize(CssDefaults.GetDefaultValue(CssConstants.FONT_SIZE
                            ));
                    }
                    else {
                        baseFontSize = CssUtils.ParseAbsoluteLength(parentFontSizeStr);
                    }
                }
                float absoluteFontSize = FontStyleApplierUtil.ParseRelativeFontSize(elementFontSize, baseFontSize);
                elementStyles.Put(CssConstants.FONT_SIZE, System.Convert.ToString(absoluteFontSize, System.Globalization.CultureInfo.InvariantCulture
                    ) + CssConstants.PT);
            }
            else {
                elementStyles.Put(CssConstants.FONT_SIZE, System.Convert.ToString(FontStyleApplierUtil.ParseAbsoluteFontSize
                    (elementFontSize), System.Globalization.CultureInfo.InvariantCulture) + CssConstants.PT);
            }
            // Update root font size
            if (element is IElementNode && TagConstants.HTML.Equals(((IElementNode)element).Name())) {
                context.SetRootFontSize(elementStyles.Get(CssConstants.FONT_SIZE));
            }
            ICollection<String> keys = new HashSet<String>();
            foreach (KeyValuePair<String, String> entry in elementStyles) {
                if (CssConstants.INITIAL.Equals(entry.Value) || CssConstants.INHERIT.Equals(entry.Value)) {
                    // if "inherit" is not resolved till now, parents don't have it
                    keys.Add(entry.Key);
                }
            }
            foreach (String key in keys) {
                elementStyles.Put(key, CssDefaults.GetDefaultValue(key));
            }
            // This is needed for correct resolving of content property, so doing it right here
            CounterProcessorUtil.ProcessCounters(elementStyles, context, element);
            ResolveContentProperty(elementStyles, element, context);
            return elementStyles;
        }

        public virtual IList<CssFontFaceRule> GetFonts() {
            return fonts;
        }

        private void ResolveContentProperty(IDictionary<String, String> styles, INode contentContainer, CssContext
             context) {
            if (contentContainer is CssPseudoElementNode || contentContainer is PageMarginBoxContextNode) {
                IList<INode> resolvedContent = CssContentPropertyResolver.ResolveContent(styles, contentContainer, context
                    );
                if (resolvedContent != null) {
                    foreach (INode child in resolvedContent) {
                        contentContainer.AddChild(child);
                    }
                }
            }
        }

        private IDictionary<String, String> CssDeclarationsToMap(IList<CssDeclaration> nodeCssDeclarations) {
            IDictionary<String, String> stylesMap = new Dictionary<String, String>();
            for (int i = 0; i < nodeCssDeclarations.Count; i++) {
                CssDeclaration cssDeclaration = nodeCssDeclarations[i];
                IShorthandResolver shorthandResolver = ShorthandResolverFactory.GetShorthandResolver(cssDeclaration.GetProperty
                    ());
                if (shorthandResolver == null) {
                    PutDeclarationInMapIfValid(stylesMap, cssDeclaration);
                }
                else {
                    IList<CssDeclaration> resolvedShorthandProps = shorthandResolver.ResolveShorthand(cssDeclaration.GetExpression
                        ());
                    foreach (CssDeclaration resolvedProp in resolvedShorthandProps) {
                        PutDeclarationInMapIfValid(stylesMap, resolvedProp);
                    }
                }
            }
            return stylesMap;
        }

        private void PutDeclarationInMapIfValid(IDictionary<String, String> stylesMap, CssDeclaration cssDeclaration
            ) {
            if (CssDeclarationValidationMaster.CheckDeclaration(cssDeclaration)) {
                stylesMap.Put(cssDeclaration.GetProperty(), cssDeclaration.GetExpression());
            }
            else {
                ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Resolve.DefaultCssResolver));
                logger.Warn(String.Format(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, cssDeclaration
                    ));
            }
        }

        private INode CollectCssDeclarations(INode rootNode, ResourceResolver resourceResolver, CssContext cssContext
            ) {
            cssStyleSheet = new CssStyleSheet();
            LinkedList<INode> q = new LinkedList<INode>();
            q.Add(rootNode);
            while (!q.IsEmpty()) {
                INode currentNode = q.JGetFirst();
                q.RemoveFirst();
                if (currentNode is IElementNode) {
                    IElementNode headChildElement = (IElementNode)currentNode;
                    if (headChildElement.Name().Equals(TagConstants.STYLE)) {
                        if (currentNode.ChildNodes().Count > 0 && currentNode.ChildNodes()[0] is IDataNode) {
                            String styleData = ((IDataNode)currentNode.ChildNodes()[0]).GetWholeData();
                            CheckIfPagesCounterMentioned(styleData, cssContext);
                            CssStyleSheet styleSheet = CssStyleSheetParser.Parse(styleData);
                            styleSheet = WrapStyleSheetInMediaQueryIfNecessary(headChildElement, styleSheet);
                            cssStyleSheet.AppendCssStyleSheet(styleSheet);
                        }
                    }
                    else {
                        if (HtmlUtils.IsStyleSheetLink(headChildElement)) {
                            String styleSheetUri = headChildElement.GetAttribute(AttributeConstants.HREF);
                            try {
                                Stream stream = resourceResolver.RetrieveStyleSheet(styleSheetUri);
                                byte[] bytes = StreamUtil.InputStreamToArray(stream);
                                CheckIfPagesCounterMentioned(iText.IO.Util.JavaUtil.GetStringForBytes(bytes), cssContext);
                                CssStyleSheet styleSheet = CssStyleSheetParser.Parse(new MemoryStream(bytes), resourceResolver.ResolveAgainstBaseUri
                                    (styleSheetUri).ToExternalForm());
                                styleSheet = WrapStyleSheetInMediaQueryIfNecessary(headChildElement, styleSheet);
                                cssStyleSheet.AppendCssStyleSheet(styleSheet);
                            }
                            catch (Exception exc) {
                                ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Resolve.DefaultCssResolver));
                                logger.Error(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_EXTERNAL_CSS_FILE, exc);
                            }
                        }
                    }
                }
                foreach (INode child in currentNode.ChildNodes()) {
                    if (child is IElementNode) {
                        q.Add(child);
                    }
                }
            }
            return null;
        }

        private void CheckIfPagesCounterMentioned(String cssContents, CssContext cssContext) {
            // TODO more efficient (avoid searching in text string) and precise (e.g. skip spaces) check during the parsing.
            if (cssContents.Contains("counter(pages)") || cssContents.Contains("counters(pages")) {
                // The presence of counter(pages) means that theoretically relayout may be needed.
                // We don't know it yet because that selector might not even be used, but
                // when we know it for sure, it's too late because the Document is created right in the start.
                cssContext.SetPagesCounterPresent(true);
            }
        }

        private CssStyleSheet WrapStyleSheetInMediaQueryIfNecessary(IElementNode headChildElement, CssStyleSheet styleSheet
            ) {
            String mediaAttribute = headChildElement.GetAttribute(AttributeConstants.MEDIA);
            if (mediaAttribute != null && mediaAttribute.Length > 0) {
                CssMediaRule mediaRule = new CssMediaRule(mediaAttribute);
                mediaRule.AddStatementsToBody(styleSheet.GetStatements());
                styleSheet = new CssStyleSheet();
                styleSheet.AddStatement(mediaRule);
            }
            return styleSheet;
        }

        private void MergeParentCssDeclaration(IDictionary<String, String> styles, String cssProperty, String parentPropValue
            ) {
            String childPropValue = styles.Get(cssProperty);
            if ((childPropValue == null && CssInheritance.IsInheritable(cssProperty)) || CssConstants.INHERIT.Equals(childPropValue
                )) {
                styles.Put(cssProperty, parentPropValue);
            }
            else {
                if (CssConstants.TEXT_DECORATION.Equals(cssProperty)) {
                    // TODO Note! This property is formally not inherited, but the browsers behave very similar to inheritance here.
                    /* Text decorations on inline boxes are drawn across the entire element,
                    going across any descendant elements without paying any attention to their presence. */
                    // Also, when, for example, parent element has text-decoration:underline, and the child text-decoration:overline,
                    // then the text in the child will be both overline and underline. This is why the declarations are merged
                    // See TextDecorationTest#textDecoration01Test
                    styles.Put(cssProperty, CssPropertyMerger.MergeTextDecoration(childPropValue, parentPropValue));
                }
            }
        }

        private void CollectFonts() {
            foreach (CssStatement cssStatement in cssStyleSheet.GetStatements()) {
                CollectFonts(cssStatement);
            }
        }

        private void CollectFonts(CssStatement cssStatement) {
            if (cssStatement is CssFontFaceRule) {
                fonts.Add((CssFontFaceRule)cssStatement);
            }
            else {
                if (cssStatement is CssMediaRule && ((CssMediaRule)cssStatement).MatchMediaDevice(deviceDescription)) {
                    foreach (CssStatement cssSubStatement in ((CssMediaRule)cssStatement).GetStatements()) {
                        CollectFonts(cssSubStatement);
                    }
                }
            }
        }
    }
}
