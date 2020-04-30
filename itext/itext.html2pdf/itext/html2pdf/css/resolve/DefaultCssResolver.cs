/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Html;
using iText.IO.Util;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Parse;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Css.Resolve;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Resolver.Resource;

namespace iText.Html2pdf.Css.Resolve {
    /// <summary>
    /// Default implementation of the
    /// <see cref="iText.StyledXmlParser.Css.ICssResolver"/>
    /// interface.
    /// </summary>
    public class DefaultCssResolver : ICssResolver {
        /// <summary>The CSS style sheet.</summary>
        private CssStyleSheet cssStyleSheet;

        /// <summary>The device description.</summary>
        private MediaDeviceDescription deviceDescription;

        /// <summary>Css inheritance checker</summary>
        private IStyleInheritance cssInheritance = new CssInheritance();

        /// <summary>The list of fonts.</summary>
        private IList<CssFontFaceRule> fonts = new List<CssFontFaceRule>();

        private static readonly IList<String> fontSizeDependentPercentage = new List<String>(2);

        static DefaultCssResolver() {
            fontSizeDependentPercentage.Add(CssConstants.FONT_SIZE);
            fontSizeDependentPercentage.Add(CssConstants.LINE_HEIGHT);
        }

        /// <summary>
        /// Creates a new
        /// <see cref="DefaultCssResolver"/>
        /// instance.
        /// </summary>
        /// <param name="treeRoot">the root node</param>
        /// <param name="mediaDeviceDescription">the media device description</param>
        /// <param name="resourceResolver">the resource resolver</param>
        public DefaultCssResolver(INode treeRoot, MediaDeviceDescription mediaDeviceDescription, ResourceResolver 
            resourceResolver) {
            this.deviceDescription = mediaDeviceDescription;
            CollectCssDeclarations(treeRoot, resourceResolver, null);
            CollectFonts();
        }

        /// <summary>
        /// Creates a new
        /// <see cref="DefaultCssResolver"/>
        /// instance.
        /// </summary>
        /// <param name="treeRoot">the root node</param>
        /// <param name="context">the processor context</param>
        public DefaultCssResolver(INode treeRoot, ProcessorContext context) {
            this.deviceDescription = context.GetDeviceDescription();
            CollectCssDeclarations(treeRoot, context.GetResourceResolver(), context.GetCssContext());
            CollectFonts();
        }

        public virtual IDictionary<String, String> ResolveStyles(INode element, AbstractCssContext context) {
            if (context is CssContext) {
                return ResolveStyles(element, (CssContext)context);
            }
            throw new Html2PdfException("custom AbstractCssContext implementations are not supported yet");
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.resolve.ICssResolver#resolveStyles(com.itextpdf.html2pdf.html.node.INode, com.itextpdf.html2pdf.css.resolve.CssContext)
        */
        private IDictionary<String, String> ResolveStyles(INode element, CssContext context) {
            IList<CssRuleSet> ruleSets = new List<CssRuleSet>();
            ruleSets.Add(new CssRuleSet(null, UserAgentCss.GetStyles(element)));
            if (element is IElementNode) {
                ruleSets.Add(new CssRuleSet(null, HtmlStylesToCssConverter.Convert((IElementNode)element)));
            }
            ruleSets.AddAll(cssStyleSheet.GetCssRuleSets(element, deviceDescription));
            if (element is IElementNode) {
                String styleAttribute = ((IElementNode)element).GetAttribute(AttributeConstants.STYLE);
                if (styleAttribute != null) {
                    ruleSets.Add(new CssRuleSet(null, CssRuleSetParser.ParsePropertyDeclarations(styleAttribute)));
                }
            }
            IDictionary<String, String> elementStyles = CssStyleSheet.ExtractStylesFromRuleSets(ruleSets);
            if (CssConstants.CURRENTCOLOR.Equals(elementStyles.Get(CssConstants.COLOR))) {
                // css-color-3/#currentcolor:
                // If the ‘currentColor’ keyword is set on the ‘color’ property itself, it is treated as ‘color: inherit’.
                elementStyles.Put(CssConstants.COLOR, CssConstants.INHERIT);
            }
            String parentFontSizeStr = null;
            if (element.ParentNode() is IStylesContainer) {
                IStylesContainer parentNode = (IStylesContainer)element.ParentNode();
                IDictionary<String, String> parentStyles = parentNode.GetStyles();
                if (parentStyles == null && !(element.ParentNode() is IDocumentNode)) {
                    ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Css.Resolve.DefaultCssResolver));
                    logger.Error(iText.Html2pdf.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES);
                }
                if (parentStyles != null) {
                    foreach (KeyValuePair<String, String> entry in parentStyles) {
                        MergeParentCssDeclaration(elementStyles, entry.Key, entry.Value, parentStyles);
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
                        baseFontSize = CssUtils.ParseAbsoluteFontSize(CssDefaults.GetDefaultValue(CssConstants.FONT_SIZE));
                    }
                    else {
                        baseFontSize = CssUtils.ParseAbsoluteLength(parentFontSizeStr);
                    }
                }
                float absoluteFontSize = CssUtils.ParseRelativeFontSize(elementFontSize, baseFontSize);
                // Format to 4 decimal places to prevent differences between Java and C#
                elementStyles.Put(CssConstants.FONT_SIZE, DecimalFormatUtil.FormatNumber(absoluteFontSize, "0.####") + CssConstants
                    .PT);
            }
            else {
                elementStyles.Put(CssConstants.FONT_SIZE, Convert.ToString(CssUtils.ParseAbsoluteFontSize(elementFontSize)
                    , System.Globalization.CultureInfo.InvariantCulture) + CssConstants.PT);
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

        /// <summary>Gets the list of fonts.</summary>
        /// <returns>
        /// the list of
        /// <see cref="iText.StyledXmlParser.Css.CssFontFaceRule"/>
        /// instances
        /// </returns>
        public virtual IList<CssFontFaceRule> GetFonts() {
            return fonts;
        }

        /// <summary>Resolves a content property.</summary>
        /// <param name="styles">the styles map</param>
        /// <param name="contentContainer">the content container</param>
        /// <param name="context">the CSS context</param>
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

        /// <summary>Collects CSS declarationss.</summary>
        /// <param name="rootNode">the root node</param>
        /// <param name="resourceResolver">the resource resolver</param>
        /// <param name="cssContext">the CSS context</param>
        private void CollectCssDeclarations(INode rootNode, ResourceResolver resourceResolver, CssContext cssContext
            ) {
            cssStyleSheet = new CssStyleSheet();
            LinkedList<INode> q = new LinkedList<INode>();
            q.Add(rootNode);
            while (!q.IsEmpty()) {
                INode currentNode = q.JGetFirst();
                q.RemoveFirst();
                if (currentNode is IElementNode) {
                    IElementNode headChildElement = (IElementNode)currentNode;
                    if (TagConstants.STYLE.Equals(headChildElement.Name())) {
                        if (currentNode.ChildNodes().Count > 0 && currentNode.ChildNodes()[0] is IDataNode) {
                            String styleData = ((IDataNode)currentNode.ChildNodes()[0]).GetWholeData();
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
                                CssStyleSheet styleSheet = CssStyleSheetParser.Parse(new MemoryStream(bytes), resourceResolver.ResolveAgainstBaseUri
                                    (styleSheetUri).ToExternalForm());
                                styleSheet = WrapStyleSheetInMediaQueryIfNecessary(headChildElement, styleSheet);
                                cssStyleSheet.AppendCssStyleSheet(styleSheet);
                            }
                            catch (Exception exc) {
                                ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Css.Resolve.DefaultCssResolver));
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
            CheckIfPagesCounterMentioned(cssStyleSheet, cssContext);
        }

        /// <summary>Check if a pages counter is mentioned.</summary>
        /// <param name="styleSheet">the stylesheet to analyze</param>
        /// <param name="cssContext">the CSS context</param>
        private void CheckIfPagesCounterMentioned(CssStyleSheet styleSheet, CssContext cssContext) {
            // The presence of counter(pages) means that theoretically relayout may be needed.
            // We don't know it yet because that selector might not even be used, but
            // when we know it for sure, it's too late because the Document is created right in the start.
            if (CssStyleSheetAnalyzer.CheckPagesCounterPresence(styleSheet)) {
                cssContext.SetPagesCounterPresent(true);
            }
        }

        /// <summary>
        /// Wraps a
        /// <see cref="iText.StyledXmlParser.Css.Media.CssMediaRule"/>
        /// into the style sheet if the head child element has a media attribute.
        /// </summary>
        /// <param name="headChildElement">the head child element</param>
        /// <param name="styleSheet">the style sheet</param>
        /// <returns>the css style sheet</returns>
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

        /// <summary>Merge parent CSS declarations.</summary>
        /// <param name="styles">the styles map</param>
        /// <param name="cssProperty">the CSS property</param>
        /// <param name="parentPropValue">the parent properties value</param>
        private void MergeParentCssDeclaration(IDictionary<String, String> styles, String cssProperty, String parentPropValue
            , IDictionary<String, String> parentStyles) {
            String childPropValue = styles.Get(cssProperty);
            if ((childPropValue == null && cssInheritance.IsInheritable(cssProperty)) || CssConstants.INHERIT.Equals(childPropValue
                )) {
                if (ValueIsOfMeasurement(parentPropValue, CssConstants.EM) || ValueIsOfMeasurement(parentPropValue, CssConstants
                    .EX) || ValueIsOfMeasurement(parentPropValue, CssConstants.PERCENTAGE) && fontSizeDependentPercentage.
                    Contains(cssProperty)) {
                    float absoluteParentFontSize = CssUtils.ParseAbsoluteLength(parentStyles.Get(CssConstants.FONT_SIZE));
                    // Format to 4 decimal places to prevent differences between Java and C#
                    styles.Put(cssProperty, DecimalFormatUtil.FormatNumber(CssUtils.ParseRelativeValue(parentPropValue, absoluteParentFontSize
                        ), "0.####") + CssConstants.PT);
                }
                else {
                    styles.Put(cssProperty, parentPropValue);
                }
            }
            else {
                if (CssConstants.TEXT_DECORATION.Equals(cssProperty) && !CssConstants.INLINE_BLOCK.Equals(styles.Get(CssConstants
                    .DISPLAY))) {
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

        private static bool ValueIsOfMeasurement(String value, String measurement) {
            if (value == null) {
                return false;
            }
            if (value.EndsWith(measurement) && CssUtils.IsNumericValue(value.JSubstring(0, value.Length - measurement.
                Length).Trim())) {
                return true;
            }
            return false;
        }

        /// <summary>Collects fonts from the style sheet.</summary>
        private void CollectFonts() {
            foreach (CssStatement cssStatement in cssStyleSheet.GetStatements()) {
                CollectFonts(cssStatement);
            }
        }

        /// <summary>
        /// Collects fonts from a
        /// <see cref="iText.StyledXmlParser.Css.CssStatement"/>.
        /// </summary>
        /// <param name="cssStatement">the CSS statement</param>
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
