/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Actions;
using iText.Commons.Actions.Sequence;
using iText.Commons.Utils;
using iText.Forms.Form.Element;
using iText.Html2pdf;
using iText.Html2pdf.Actions.Events;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Font;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Font;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Util;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>The default implementation to process HTML.</summary>
    public class DefaultHtmlProcessor : IHtmlProcessor {
        /// <summary>The logger instance.</summary>
        private static readonly ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.DefaultHtmlProcessor
            ));

        /// <summary>Set of tags that do not map to any tag worker and that are deliberately excluded from the logging.
        ///     </summary>
        private static readonly ICollection<String> ignoredTags = JavaCollectionsUtil.UnmodifiableSet(new HashSet<
            String>(JavaUtil.ArraysAsList(TagConstants.HEAD, TagConstants.STYLE, TagConstants.TBODY)));

        // <tbody> is not supported via tag workers. Styles will be propagated anyway (most of them, but not all)
        // TODO in scope of DEVSIX-4258 we might want to introduce a tag worker for <tbody> and remove it from here
        /// <summary>Set of tags to which we do not want to apply CSS to and that are deliberately excluded from the logging
        ///     </summary>
        private static readonly ICollection<String> ignoredCssTags = JavaCollectionsUtil.UnmodifiableSet(new HashSet
            <String>(JavaUtil.ArraysAsList(TagConstants.BR, TagConstants.LINK, TagConstants.META, TagConstants.TITLE
            , TagConstants.TR)));

        /// <summary>Set of tags that might be not processed by some tag workers and that are deliberately excluded from the logging.
        ///     </summary>
        private static readonly ICollection<String> ignoredChildTags = JavaCollectionsUtil.UnmodifiableSet(new HashSet
            <String>(JavaUtil.ArraysAsList(TagConstants.BODY, TagConstants.LINK, TagConstants.META, TagConstants.SCRIPT
            , TagConstants.TITLE)));

        /// <summary>The processor context.</summary>
        private ProcessorContext context;

        /// <summary>A list of parent objects that result from parsing the HTML.</summary>
        private IList<IPropertyContainer> roots;

        /// <summary>The CSS resolver.</summary>
        private ICssResolver cssResolver;

        /// <summary>Instantiates a new default html processor.</summary>
        /// <param name="converterProperties">the converter properties</param>
        public DefaultHtmlProcessor(ConverterProperties converterProperties) {
            this.context = ProcessorContextCreator.CreateProcessorContext(converterProperties);
        }

        /// <summary>Sets properties to top-level layout elements converted from HTML.</summary>
        /// <remarks>
        /// Sets properties to top-level layout elements converted from HTML.
        /// This enables features set by user via HTML converter API and also changes properties defaults
        /// to the ones specific to HTML-like behavior.
        /// </remarks>
        /// <param name="cssProperties">HTML document-level css properties.</param>
        /// <param name="context">processor context specific to the current HTML conversion.</param>
        /// <param name="propertyContainer">top-level layout element converted from HTML.</param>
        public static void SetConvertedRootElementProperties(IDictionary<String, String> cssProperties, ProcessorContext
             context, IPropertyContainer propertyContainer) {
            propertyContainer.SetProperty(Property.COLLAPSING_MARGINS, true);
            propertyContainer.SetProperty(Property.RENDERING_MODE, RenderingMode.HTML_MODE);
            propertyContainer.SetProperty(Property.FONT_PROVIDER, context.GetFontProvider());
            if (context.GetTempFonts() != null) {
                propertyContainer.SetProperty(Property.FONT_SET, context.GetTempFonts());
            }
            // TODO DEVSIX-2534
            IList<String> fontFamilies = FontFamilySplitterUtil.SplitFontFamily(cssProperties.Get(CssConstants.FONT_FAMILY
                ));
            if (fontFamilies != null && !propertyContainer.HasOwnProperty(Property.FONT)) {
                propertyContainer.SetProperty(Property.FONT, fontFamilies.ToArray(new String[0]));
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.IHtmlProcessor#processElements(com.itextpdf.html2pdf.html.node.INode)
        */
        public virtual IList<IElement> ProcessElements(INode root) {
            SequenceId sequenceId = new SequenceId();
            EventManager.GetInstance().OnEvent(PdfHtmlProductEvent.CreateConvertHtmlEvent(sequenceId, context.GetMetaInfoContainer
                ().GetMetaInfo()));
            context.Reset();
            roots = new List<IPropertyContainer>();
            cssResolver = new DefaultCssResolver(root, context);
            context.SetCssStyleSheet(((DefaultCssResolver)cssResolver).GetCssStyleSheet());
            context.GetLinkContext().ScanForIds(root);
            AddFontFaceFonts();
            IElementNode html = FindHtmlNode(root);
            IElementNode body = FindBodyNode(root);
            // Force resolve styles to fetch default font size etc
            html.SetStyles(cssResolver.ResolveStyles(html, context.GetCssContext()));
            // visit body
            Visit(body);
            Div bodyDiv = (Div)roots[0];
            IList<IElement> elements = new List<IElement>();
            // re-resolve body element styles in order to use them in top-level elements properties setting
            body.SetStyles(cssResolver.ResolveStyles(body, context.GetCssContext()));
            foreach (IPropertyContainer propertyContainer in bodyDiv.GetChildren()) {
                if (propertyContainer is IElement) {
                    SetConvertedRootElementProperties(body.GetStyles(), context, propertyContainer);
                    elements.Add((IElement)propertyContainer);
                }
            }
            cssResolver = null;
            roots = null;
            foreach (IElement element in elements) {
                UpdateSequenceId(element, sequenceId);
            }
            return elements;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.IHtmlProcessor#processDocument(com.itextpdf.html2pdf.html.node.INode, com.itextpdf.kernel.pdf.PdfDocument)
        */
        public virtual Document ProcessDocument(INode root, PdfDocument pdfDocument) {
            EventManager.GetInstance().OnEvent(PdfHtmlProductEvent.CreateConvertHtmlEvent(pdfDocument.GetDocumentIdWrapper
                (), context.GetMetaInfoContainer().GetMetaInfo()));
            context.Reset(pdfDocument);
            if (!context.HasFonts()) {
                throw new Html2PdfException(Html2PdfException.FONT_PROVIDER_CONTAINS_ZERO_FONTS);
            }
            roots = new List<IPropertyContainer>();
            cssResolver = new DefaultCssResolver(root, context);
            context.SetCssStyleSheet(((DefaultCssResolver)cssResolver).GetCssStyleSheet());
            context.GetLinkContext().ScanForIds(root);
            AddFontFaceFonts();
            root = FindHtmlNode(root);
            if (context.GetCssContext().IsNonPagesTargetCounterPresent()) {
                VisitToProcessCounters(root);
                context.GetCssContext().GetCounterManager().ClearManager();
            }
            Visit(root);
            HtmlDocument doc = (HtmlDocument)roots[0];
            // TODO DEVSIX-4261 more precise check if a counter was actually added to the document
            if (context.GetCssContext().IsPagesCounterPresent()) {
                if (doc.GetRenderer() is HtmlDocumentRenderer) {
                    ((HtmlDocumentRenderer)doc.GetRenderer()).ProcessWaitingElement();
                    int counter = 0;
                    do {
                        ++counter;
                        doc.Relayout();
                        if (counter >= context.GetLimitOfLayouts()) {
                            logger.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.EXCEEDED_THE_MAXIMUM_NUMBER_OF_RELAYOUTS
                                ));
                            break;
                        }
                    }
                    while (((DocumentRenderer)doc.GetRenderer()).IsRelayoutRequired());
                }
                else {
                    logger.LogWarning(Html2PdfLogMessageConstant.CUSTOM_RENDERER_IS_SET_FOR_HTML_DOCUMENT);
                }
            }
            cssResolver = null;
            roots = null;
            return doc;
        }

        /// <summary>Recursively processes a node to preprocess target-counters.</summary>
        /// <param name="node">the node</param>
        private void VisitToProcessCounters(INode node) {
            if (node is IElementNode) {
                IElementNode element = (IElementNode)node;
                if (cssResolver is DefaultCssResolver) {
                    ((DefaultCssResolver)cssResolver).ResolveContentAndCountersStyles(node, context.GetCssContext());
                }
                CounterProcessorUtil.StartProcessingCounters(context.GetCssContext(), element);
                VisitToProcessCounters(CreatePseudoElement(element, null, CssConstants.BEFORE));
                foreach (INode childNode in element.ChildNodes()) {
                    if (!context.IsProcessingInlineSvg()) {
                        VisitToProcessCounters(childNode);
                    }
                }
                VisitToProcessCounters(CreatePseudoElement(element, null, CssConstants.AFTER));
                CounterProcessorUtil.EndProcessingCounters(context.GetCssContext(), element);
            }
        }

        /// <summary>Recursively processes a node converting HTML into PDF using tag workers.</summary>
        /// <param name="node">the node</param>
        private void Visit(INode node) {
            if (node is IElementNode) {
                IElementNode element = (IElementNode)node;
                element.SetStyles(cssResolver.ResolveStyles(element, context.GetCssContext()));
                if (!IsDisplayable(element)) {
                    return;
                }
                ITagWorker tagWorker = context.GetTagWorkerFactory().GetTagWorker(element, context);
                if (tagWorker == null) {
                    if (!ignoredTags.Contains(element.Name())) {
                        logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, element.Name(
                            )));
                    }
                }
                else {
                    context.GetState().Push(tagWorker);
                }
                if (context.GetState().GetStack().Count == 1 && tagWorker != null && tagWorker.GetElementResult() != null) {
                    tagWorker.GetElementResult().SetProperty(Property.META_INFO, new MetaInfoContainer(context.GetMetaInfoContainer
                        ().GetMetaInfo()));
                }
                if (tagWorker is HtmlTagWorker) {
                    ((HtmlTagWorker)tagWorker).ProcessPageRules(node, cssResolver, context);
                }
                if (TagConstants.BODY.Equals(element.Name()) || TagConstants.HTML.Equals(element.Name())) {
                    RunApplier(element, tagWorker);
                }
                context.GetOutlineHandler().AddOutlineAndDestToDocument(tagWorker, element, context);
                TextDecorationApplierUtil.PropagateTextDecorationProperties(element);
                CounterProcessorUtil.StartProcessingCounters(context.GetCssContext(), element);
                Visit(CreatePseudoElement(element, tagWorker, CssConstants.BEFORE));
                Visit(CreatePseudoElement(element, tagWorker, CssConstants.PLACEHOLDER));
                foreach (INode childNode in element.ChildNodes()) {
                    if (!context.IsProcessingInlineSvg()) {
                        Visit(childNode);
                    }
                }
                Visit(CreatePseudoElement(element, tagWorker, CssConstants.AFTER));
                CounterProcessorUtil.EndProcessingCounters(context.GetCssContext(), element);
                if (tagWorker != null) {
                    tagWorker.ProcessEnd(element, context);
                    LinkHelper.CreateDestination(tagWorker, element, context);
                    context.GetOutlineHandler().SetDestinationToElement(tagWorker, element);
                    context.GetState().Pop();
                    if (!TagConstants.BODY.Equals(element.Name()) && !TagConstants.HTML.Equals(element.Name())) {
                        RunApplier(element, tagWorker);
                    }
                    if (!context.GetState().Empty()) {
                        PageBreakApplierUtil.AddPageBreakElementBefore(context, context.GetState().Top(), element, tagWorker);
                        tagWorker = ProcessRunningElement(tagWorker, element, context);
                        bool childProcessed = context.GetState().Top().ProcessTagChild(tagWorker, context);
                        PageBreakApplierUtil.AddPageBreakElementAfter(context, context.GetState().Top(), element, tagWorker);
                        if (!childProcessed && !ignoredChildTags.Contains(element.Name())) {
                            logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, 
                                context.GetState().Top().GetType().FullName, tagWorker.GetType().FullName));
                        }
                    }
                    else {
                        if (tagWorker.GetElementResult() != null) {
                            roots.Add(tagWorker.GetElementResult());
                        }
                    }
                    if (tagWorker.GetElementResult() != null && context.IsContinuousContainerEnabled()) {
                        tagWorker.GetElementResult().SetProperty(Property.COLLAPSING_MARGINS, false);
                        tagWorker.GetElementResult().SetProperty(Property.TREAT_AS_CONTINUOUS_CONTAINER, true);
                    }
                }
                element.SetStyles(null);
            }
            else {
                if (node is ITextNode) {
                    String content = ((ITextNode)node).WholeText();
                    if (content != null) {
                        if (!context.GetState().Empty()) {
                            bool contentProcessed = context.GetState().Top().ProcessContent(content, context);
                            if (!contentProcessed) {
                                logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_IT_S_TEXT_CONTENT
                                    , context.GetState().Top().GetType().FullName));
                            }
                        }
                        else {
                            logger.LogError(Html2PdfLogMessageConstant.NO_CONSUMER_FOUND_FOR_CONTENT);
                        }
                    }
                }
            }
        }

        private void RunApplier(IElementNode element, ITagWorker tagWorker) {
            ICssApplier cssApplier = context.GetCssApplierFactory().GetCssApplier(element);
            if (cssApplier == null) {
                if (!ignoredCssTags.Contains(element.Name())) {
                    logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.NO_CSS_APPLIER_FOUND_FOR_TAG, element.
                        Name()));
                }
            }
            else {
                cssApplier.Apply(context, element, tagWorker);
            }
        }

        private ITagWorker ProcessRunningElement(ITagWorker tagWorker, IElementNode element, ProcessorContext context
            ) {
            String runningPrefix = CssConstants.RUNNING + "(";
            String positionVal;
            int endBracketInd;
            if (element.GetStyles() == null || (positionVal = element.GetStyles().Get(CssConstants.POSITION)) == null 
                || !positionVal.StartsWith(runningPrefix) || 
                        // closing bracket should be there and there should be at least one symbol between brackets
                        (endBracketInd = positionVal.IndexOf(")", StringComparison.Ordinal)) <= runningPrefix.Length) {
                return tagWorker;
            }
            String runningElemName = positionVal.JSubstring(runningPrefix.Length, endBracketInd).Trim();
            if (String.IsNullOrEmpty(runningElemName)) {
                return tagWorker;
            }
            RunningElementContainer runningElementContainer = new RunningElementContainer(element, tagWorker);
            context.GetCssContext().GetRunningManager().AddRunningElement(runningElemName, runningElementContainer);
            return new RunningElementTagWorker(runningElementContainer);
        }

        /// <summary>Adds @font-face fonts to the FontProvider.</summary>
        private void AddFontFaceFonts() {
            if (cssResolver is DefaultCssResolver) {
                foreach (CssFontFaceRule fontFace in ((DefaultCssResolver)cssResolver).GetFonts()) {
                    bool findSupportedSrc = false;
                    IList<CssDeclaration> declarations = fontFace.GetProperties();
                    CssFontFace ff = CssFontFace.Create(declarations);
                    if (ff != null) {
                        foreach (CssFontFace.CssFontFaceSrc src in ff.GetSources()) {
                            if (CreateFont(ff.GetFontFamily(), src, fontFace.ResolveUnicodeRange())) {
                                findSupportedSrc = true;
                                break;
                            }
                        }
                    }
                    if (!findSupportedSrc) {
                        logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_FONT, fontFace));
                    }
                }
            }
        }

        /// <summary>Creates a font and adds it to the context.</summary>
        /// <param name="fontFamily">the font family</param>
        /// <param name="src">the source of the font</param>
        /// <param name="unicodeRange">the unicode range</param>
        /// <returns>true, if successful</returns>
        private bool CreateFont(String fontFamily, CssFontFace.CssFontFaceSrc src, Range unicodeRange) {
            if (!CssFontFace.IsSupportedFontFormat(src.GetFormat())) {
                return false;
            }
            else {
                if (src.IsLocal()) {
                    // to method with lazy initialization
                    ICollection<FontInfo> fonts = context.GetFontProvider().GetFontSet().Get(src.GetSrc());
                    if (fonts.Count > 0) {
                        foreach (FontInfo fi in fonts) {
                            context.AddTemporaryFont(fi, fontFamily);
                        }
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    try {
                        // Cache at resource resolver level only, at font level we will create font in any case.
                        // The instance of fontProgram will be collected by GC if the is no need in it.
                        byte[] bytes = context.GetResourceResolver().RetrieveBytesFromResource(src.GetSrc());
                        if (bytes != null) {
                            FontProgram fp = FontProgramFactory.CreateFont(bytes, false);
                            context.AddTemporaryFont(fp, PdfEncodings.IDENTITY_H, fontFamily, unicodeRange);
                            return true;
                        }
                    }
                    catch (Exception) {
                    }
                    return false;
                }
            }
        }

        /// <summary>Creates a pseudo element (before and after CSS).</summary>
        /// <param name="node">the node</param>
        /// <param name="tagWorker">the tagWorker</param>
        /// <param name="pseudoElementName">the pseudo element name</param>
        /// <returns>created pseudo element</returns>
        private static CssPseudoElementNode CreatePseudoElement(IElementNode node, ITagWorker tagWorker, String pseudoElementName
            ) {
            switch (pseudoElementName) {
                case CssConstants.BEFORE:
                case CssConstants.AFTER: {
                    if (!CssPseudoElementUtil.HasBeforeAfterElements(node)) {
                        return null;
                    }
                    break;
                }

                case CssConstants.PLACEHOLDER: {
                    if (!(TagConstants.INPUT.Equals(node.Name()) || TagConstants.TEXTAREA.Equals(node.Name())) || null == 
                                        // TODO DEVSIX-1944: Resolve the issue and remove the line
                                        tagWorker || !(tagWorker.GetElementResult() is IPlaceholderable) || null == ((IPlaceholderable)tagWorker.GetElementResult
                        ()).GetPlaceholder()) {
                        return null;
                    }
                    break;
                }

                default: {
                    return null;
                }
            }
            return new CssPseudoElementNode(node, pseudoElementName);
        }

        /// <summary>Find an element in a node.</summary>
        /// <param name="node">the node</param>
        /// <param name="tagName">the tag name</param>
        /// <returns>the element node</returns>
        private IElementNode FindElement(INode node, String tagName) {
            LinkedList<INode> q = new LinkedList<INode>();
            q.Add(node);
            while (!q.IsEmpty()) {
                INode currentNode = q.JGetFirst();
                q.RemoveFirst();
                if (currentNode is IElementNode && ((IElementNode)currentNode).Name().Equals(tagName)) {
                    return (IElementNode)currentNode;
                }
                foreach (INode child in currentNode.ChildNodes()) {
                    if (child is IElementNode) {
                        q.Add(child);
                    }
                }
            }
            return null;
        }

        /// <summary>Find the HTML node.</summary>
        /// <param name="node">the node</param>
        /// <returns>the i element node</returns>
        private IElementNode FindHtmlNode(INode node) {
            return FindElement(node, TagConstants.HTML);
        }

        /// <summary>Find the BODY node.</summary>
        /// <param name="node">the node</param>
        /// <returns>the i element node</returns>
        private IElementNode FindBodyNode(INode node) {
            return FindElement(node, TagConstants.BODY);
        }

        /// <summary>Checks if an element should be displayed.</summary>
        /// <param name="element">the element</param>
        /// <returns>true, if the element should be displayed</returns>
        private bool IsDisplayable(IElementNode element) {
            if (element != null && element.GetStyles() != null && CssConstants.NONE.Equals(element.GetStyles().Get(CssConstants
                .DISPLAY))) {
                return false;
            }
            if (IsPlaceholder(element)) {
                return true;
            }
            if (element is CssPseudoElementNode) {
                if (element.ChildNodes().IsEmpty()) {
                    return false;
                }
                bool hasStyles = element.GetStyles() != null;
                String positionVal = hasStyles ? element.GetStyles().Get(CssConstants.POSITION) : null;
                String displayVal = hasStyles ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
                bool containsNonEmptyChildNode = false;
                bool containsElementNode = false;
                for (int i = 0; i < element.ChildNodes().Count; i++) {
                    if (element.ChildNodes()[i] is ITextNode) {
                        containsNonEmptyChildNode = true;
                        break;
                    }
                    else {
                        if (element.ChildNodes()[i] is IElementNode) {
                            containsElementNode = true;
                        }
                    }
                }
                return containsElementNode || containsNonEmptyChildNode || CssConstants.ABSOLUTE.Equals(positionVal) || CssConstants
                    .FIXED.Equals(positionVal) || displayVal != null && !CssConstants.INLINE.Equals(displayVal);
            }
            return element != null;
        }

        private bool IsPlaceholder(IElementNode element) {
            return element is CssPseudoElementNode && CssConstants.PLACEHOLDER.Equals(((CssPseudoElementNode)element).
                GetPseudoElementName());
        }

        private static void UpdateSequenceId(IElement element, SequenceId sequenceId) {
            if (element is AbstractIdentifiableElement) {
                AbstractIdentifiableElement identifiableElement = (AbstractIdentifiableElement)element;
                if (SequenceIdManager.GetSequenceId(identifiableElement) == sequenceId) {
                    // potential cyclic reference case: element has been processed already
                    return;
                }
                SequenceIdManager.SetSequenceId(identifiableElement, sequenceId);
                if (identifiableElement is IAbstractElement) {
                    IAbstractElement abstractElement = (IAbstractElement)identifiableElement;
                    UpdateChildren(abstractElement.GetChildren(), sequenceId);
                }
            }
        }

        private static void UpdateChildren(IList<IElement> children, SequenceId sequenceId) {
            if (children == null) {
                return;
            }
            foreach (IElement child in children) {
                UpdateSequenceId(child, sequenceId);
            }
        }
    }
}
