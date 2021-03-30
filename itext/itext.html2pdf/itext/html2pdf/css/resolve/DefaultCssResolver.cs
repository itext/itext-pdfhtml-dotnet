/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using iText.StyledXmlParser.Util;

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

        /// <summary>Gets the list of fonts.</summary>
        /// <returns>
        /// the list of
        /// <see cref="iText.StyledXmlParser.Css.CssFontFaceRule"/>
        /// instances
        /// </returns>
        public virtual IList<CssFontFaceRule> GetFonts() {
            return fonts;
        }

        /// <summary>Resolves content and counter(s) styles of a node given the passed context.</summary>
        /// <param name="node">the node</param>
        /// <param name="context">the CSS context (RootFontSize, etc.)</param>
        public virtual void ResolveContentAndCountersStyles(INode node, CssContext context) {
            IDictionary<String, String> elementStyles = ResolveElementsStyles(node);
            CounterProcessorUtil.ProcessCounters(elementStyles, context);
            ResolveContentProperty(elementStyles, node, context);
        }

        /// <summary><inheritDoc/></summary>
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
            IDictionary<String, String> elementStyles = ResolveElementsStyles(element);
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
                    ICollection<IStyleInheritance> inheritanceRules = new HashSet<IStyleInheritance>();
                    inheritanceRules.Add(cssInheritance);
                    foreach (KeyValuePair<String, String> entry in parentStyles) {
                        elementStyles = StyleUtil.MergeParentStyleDeclaration(elementStyles, entry.Key, entry.Value, parentStyles.
                            Get(CommonCssConstants.FONT_SIZE), inheritanceRules);
                        // If the parent has display: flex, the flex item is blockified
                        // no matter what display value is set for it.
                        // See CSS Flexible Box Layout Module Level 1,
                        // W3C Candidate Recommendation, 19 November 2018: 4. Flex Items.
                        if (IsFlexItem(entry, elementStyles.Get(CssConstants.DISPLAY))) {
                            elementStyles.Put(CssConstants.DISPLAY, CssConstants.BLOCK);
                        }
                    }
                    parentFontSizeStr = parentStyles.Get(CssConstants.FONT_SIZE);
                }
            }
            String elementFontSize = elementStyles.Get(CssConstants.FONT_SIZE);
            if (CssTypesValidationUtils.IsRelativeValue(elementFontSize) || CssConstants.LARGER.Equals(elementFontSize
                ) || CssConstants.SMALLER.Equals(elementFontSize)) {
                float baseFontSize;
                if (CssTypesValidationUtils.IsRemValue(elementFontSize)) {
                    baseFontSize = context.GetRootFontSize();
                }
                else {
                    if (parentFontSizeStr == null) {
                        baseFontSize = CssDimensionParsingUtils.ParseAbsoluteFontSize(CssDefaults.GetDefaultValue(CssConstants.FONT_SIZE
                            ));
                    }
                    else {
                        baseFontSize = CssDimensionParsingUtils.ParseAbsoluteLength(parentFontSizeStr);
                    }
                }
                float absoluteFontSize = CssDimensionParsingUtils.ParseRelativeFontSize(elementFontSize, baseFontSize);
                // Format to 4 decimal places to prevent differences between Java and C#
                elementStyles.Put(CssConstants.FONT_SIZE, DecimalFormatUtil.FormatNumber(absoluteFontSize, "0.####") + CssConstants
                    .PT);
            }
            else {
                elementStyles.Put(CssConstants.FONT_SIZE, Convert.ToString(CssDimensionParsingUtils.ParseAbsoluteFontSize(
                    elementFontSize), System.Globalization.CultureInfo.InvariantCulture) + CssConstants.PT);
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
            CounterProcessorUtil.ProcessCounters(elementStyles, context);
            ResolveContentProperty(elementStyles, element, context);
            return elementStyles;
        }

        private IDictionary<String, String> ResolveElementsStyles(INode element) {
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
            return CssStyleSheet.ExtractStylesFromRuleSets(ruleSets);
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
            if (contentContainer is IElementNode) {
                context.GetCounterManager().AddTargetCounterIfRequired((IElementNode)contentContainer);
                context.GetCounterManager().AddTargetCountersIfRequired((IElementNode)contentContainer);
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
                        if (CssUtils.IsStyleSheetLink(headChildElement)) {
                            String styleSheetUri = headChildElement.GetAttribute(AttributeConstants.HREF);
                            try {
                                using (Stream stream = resourceResolver.RetrieveResourceAsInputStream(styleSheetUri)) {
                                    if (stream != null) {
                                        CssStyleSheet styleSheet = CssStyleSheetParser.Parse(stream, resourceResolver.ResolveAgainstBaseUri(styleSheetUri
                                            ).ToExternalForm());
                                        styleSheet = WrapStyleSheetInMediaQueryIfNecessary(headChildElement, styleSheet);
                                        cssStyleSheet.AppendCssStyleSheet(styleSheet);
                                    }
                                }
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
            EnablePagesCounterIfMentioned(cssStyleSheet, cssContext);
            EnableNonPageTargetCounterIfMentioned(cssStyleSheet, cssContext);
        }

        private static bool IsFlexItem(KeyValuePair<String, String> parentEntry, String currentElementDisplay) {
            return CssConstants.DISPLAY.Equals(parentEntry.Key) && CssConstants.FLEX.Equals(parentEntry.Value) && !CssConstants
                .FLEX.Equals(currentElementDisplay);
        }

        /// <summary>Check if a non-page(s) target-counter(s) is mentioned and enables it.</summary>
        /// <param name="styleSheet">the stylesheet to analyze</param>
        /// <param name="cssContext">the CSS context</param>
        private static void EnableNonPageTargetCounterIfMentioned(CssStyleSheet styleSheet, CssContext cssContext) {
            if (CssStyleSheetAnalyzer.CheckNonPagesTargetCounterPresence(styleSheet)) {
                cssContext.SetNonPagesTargetCounterPresent(true);
            }
        }

        /// <summary>Check if a pages counter is mentioned and enables it.</summary>
        /// <param name="styleSheet">the stylesheet to analyze</param>
        /// <param name="cssContext">the CSS context</param>
        private static void EnablePagesCounterIfMentioned(CssStyleSheet styleSheet, CssContext cssContext) {
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
