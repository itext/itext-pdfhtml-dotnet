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
using Common.Logging;
using iText.Html2pdf;
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
using iText.IO.Font;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Font;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Runtime.CompilerServices;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Html2pdf.Events;
using Versions.Attributes;
using iText.Kernel;
using iText.Kernel.Counter;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>The default implementation to process HTML.</summary>
    public class DefaultHtmlProcessor : IHtmlProcessor {
        /// <summary>The logger instance.</summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.DefaultHtmlProcessor
            ));

        /// <summary>Set of tags that do not map to any tag worker and that are deliberately excluded from the logging.
        ///     </summary>
        private static readonly ICollection<String> ignoredTags = JavaCollectionsUtil.UnmodifiableSet(new HashSet<
            String>(iText.IO.Util.JavaUtil.ArraysAsList(
            TagConstants.HEAD, 
            TagConstants.STYLE, 
            // <tbody> is not supported via tag workers. Styles will be propagated anyway (most of them, but not all)
            // TODO in scope of DEVSIX-4258 we might want to introduce a tag worker for <tbody> and remove it from here
            TagConstants.TBODY)
            ));

        /// <summary>Set of tags to which we do not want to apply CSS to and that are deliberately excluded from the logging
        ///     </summary>
        private static readonly ICollection<String> ignoredCssTags = JavaCollectionsUtil.UnmodifiableSet(new HashSet
            <String>(iText.IO.Util.JavaUtil.ArraysAsList(
                TagConstants.BR, 
                TagConstants.LINK, 
                TagConstants.META, 
                TagConstants.TITLE,
                // Content from <tr> is thrown upwards to parent, in other cases CSS is inherited anyway
                TagConstants.TR)));

        /// <summary>Set of tags that might be not processed by some tag workers and that are deliberately excluded from the logging.
        ///     </summary>
        private static readonly ICollection<String> ignoredChildTags = JavaCollectionsUtil.UnmodifiableSet(new HashSet
            <String>(iText.IO.Util.JavaUtil.ArraysAsList(
                TagConstants.BODY, 
                TagConstants.LINK, 
                TagConstants.META, 
                TagConstants.SCRIPT,
                TagConstants.TITLE  // TODO implement
            )));

        /// <summary>The processor context.</summary>
        private ProcessorContext context;

        /// <summary>A list of parent objects that result from parsing the HTML.</summary>
        private IList<IPropertyContainer> roots;

        /// <summary>The CSS resolver.</summary>
        private ICssResolver cssResolver;

        /// <summary>Instantiates a new default html processor.</summary>
        /// <param name="converterProperties">the converter properties</param>
        public DefaultHtmlProcessor(ConverterProperties converterProperties) {
            this.context = new ProcessorContext(converterProperties);
        }


        /// <summary>Sets properties to top-level layout elements converted from HTML.</summary>
        /// <remarks>Sets properties to top-level layout elements converted from HTML. This enables features set by user via HTML
        /// converter API and also changes properties defaults to the ones specific to HTML-like behavior.</remarks>
        /// <param name="cssProperties">HTML document-level css properties.</param>
        /// <param name="context">processor context specific to the current HTML conversion.</param>
        /// <param name="propertyContainer">top-level layout element converted from HTML.</param>
        public static void SetConvertedRootElementProperties(IDictionary<string, string> cssProperties,
            ProcessorContext context, IPropertyContainer propertyContainer)
        {
            propertyContainer.SetProperty(Property.COLLAPSING_MARGINS, true);
            propertyContainer.SetProperty(Property.RENDERING_MODE, RenderingMode.HTML_MODE);
            propertyContainer.SetProperty(Property.FONT_PROVIDER, context.GetFontProvider());
            if (context.GetTempFonts() != null)
            {
                propertyContainer.SetProperty(Property.FONT_SET, context.GetTempFonts());
            }

            // TODO DEVSIX-2534
            IList<String> fontFamilies =
                FontFamilySplitter.SplitFontFamily(cssProperties.Get(CommonCssConstants.FONT_FAMILY));
            if (fontFamilies != null && !propertyContainer.HasOwnProperty(Property.FONT))
            {
                propertyContainer.SetProperty(Property.FONT, fontFamilies.ToArray(new String[0]));
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.IHtmlProcessor#processElements(com.itextpdf.html2pdf.html.node.INode)
        */
        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual IList<IElement> ProcessElements(INode root) {

            try 
            {
                String licenseKeyClassName = "iText.License.LicenseKey, itext.licensekey";
                String licenseKeyProductClassName = "iText.License.LicenseKeyProduct, itext.licensekey";
                String licenseKeyFeatureClassName = "iText.License.LicenseKeyProductFeature, itext.licensekey";
                String checkLicenseKeyMethodName = "ScheduledCheck";
                Type licenseKeyClass = GetClass(licenseKeyClassName);
                if ( licenseKeyClass != null ) 
                {                
                    Type licenseKeyProductClass = GetClass(licenseKeyProductClassName);
                    Type licenseKeyProductFeatureClass = GetClass(licenseKeyFeatureClassName);
                    Array array = Array.CreateInstance(licenseKeyProductFeatureClass, 0);
                    object[] objects = new object[]
                    {
                        Html2PdfProductInfo.PRODUCT_NAME,
                        Html2PdfProductInfo.MAJOR_VERSION,
                        Html2PdfProductInfo.MINOR_VERSION,
                        array
                    };
                    Object productObject = System.Activator.CreateInstance(licenseKeyProductClass, objects);
                    MethodInfo m = licenseKeyClass.GetMethod(checkLicenseKeyMethodName);
                    m.Invoke(System.Activator.CreateInstance(licenseKeyClass), new object[] {productObject});
                }   
            } 
            catch ( Exception e ) 
            {
                if ( !Kernel.Version.IsAGPLVersion() )
                {
                    throw;
                }
            }
            context.Reset();
            roots = new List<IPropertyContainer>();
            cssResolver = new DefaultCssResolver(root, context);
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
            EventCounterHandler.GetInstance().OnEvent(PdfHtmlEvent.CONVERT, context.GetEventCountingMetaInfo(), GetType());
            return elements;
        }

        private static Type GetClass(string className)
        {
            String licenseKeyClassFullName = null;
            Assembly assembly = typeof(DefaultHtmlProcessor).GetAssembly();
            Attribute keyVersionAttr = assembly.GetCustomAttribute(typeof(KeyVersionAttribute));
            if (keyVersionAttr is KeyVersionAttribute)
            {
                String keyVersion = ((KeyVersionAttribute)keyVersionAttr).KeyVersion;
                String format = "{0}, Version={1}, Culture=neutral, PublicKeyToken=8354ae6d2174ddca";
                licenseKeyClassFullName = String.Format(format, className, keyVersion);
            }
            Type type = null;
            if (licenseKeyClassFullName != null)
            {
                String fileLoadExceptionMessage = null;
                try
                {
                    type = System.Type.GetType(licenseKeyClassFullName);
                }
                catch (FileLoadException fileLoadException)
                {
                    fileLoadExceptionMessage = fileLoadException.Message;
                }
                if (type == null)
                {
                    try
                    {
                        type = System.Type.GetType(className);
                    }
                    catch
                    {
                        // empty
                    }
                    if (type == null && fileLoadExceptionMessage != null) {
                        LogManager.GetLogger(typeof(DefaultHtmlProcessor)).Error(fileLoadExceptionMessage);
                    }
                }
            }
            return type;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.IHtmlProcessor#processDocument(com.itextpdf.html2pdf.html.node.INode, com.itextpdf.kernel.pdf.PdfDocument)
        */
        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual Document ProcessDocument(INode root, PdfDocument pdfDocument) {

            try 
            {
                String licenseKeyClassName = "iText.License.LicenseKey, itext.licensekey";
                String licenseKeyProductClassName = "iText.License.LicenseKeyProduct, itext.licensekey";
                String licenseKeyFeatureClassName = "iText.License.LicenseKeyProductFeature, itext.licensekey";
                String checkLicenseKeyMethodName = "ScheduledCheck";
                Type licenseKeyClass = GetClass(licenseKeyClassName);
                if ( licenseKeyClass != null ) 
                {                
                    Type licenseKeyProductClass = GetClass(licenseKeyProductClassName);
                    Type licenseKeyProductFeatureClass = GetClass(licenseKeyFeatureClassName);
                    Array array = Array.CreateInstance(licenseKeyProductFeatureClass, 0);
                    object[] objects = new object[]
                    {
                        Html2PdfProductInfo.PRODUCT_NAME,
                        Html2PdfProductInfo.MAJOR_VERSION,
                        Html2PdfProductInfo.MINOR_VERSION,
                        array
                    };
                    Object productObject = System.Activator.CreateInstance(licenseKeyProductClass, objects);
                    MethodInfo m = licenseKeyClass.GetMethod(checkLicenseKeyMethodName);
                    m.Invoke(System.Activator.CreateInstance(licenseKeyClass), new object[] {productObject});
                }   
            } 
            catch ( Exception e ) 
            {
                if ( !Kernel.Version.IsAGPLVersion() )
                {
                    throw;
                }
            }
            context.Reset(pdfDocument);
            if (!context.HasFonts()) {
                throw new Html2PdfException(Html2PdfException.FontProviderContainsZeroFonts);
            }
            roots = new List<IPropertyContainer>();
            cssResolver = new DefaultCssResolver(root, context);
            context.GetLinkContext().ScanForIds(root);
            AddFontFaceFonts();
            root = FindHtmlNode(root);
            Visit(root);
            Document doc = (Document)roots[0];
            // TODO DEVSIX-4261 more precise check if a counter was actually added to the document
            if (context.GetCssContext().IsPagesCounterPresent() && doc.GetRenderer() is HtmlDocumentRenderer) {
                doc.Relayout();
            }
            cssResolver = null;
            roots = null;
            EventCounterHandler.GetInstance().OnEvent(PdfHtmlEvent.CONVERT, context.GetEventCountingMetaInfo(), GetType());
            return doc;
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
                        logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, (element)
                            .Name()));
                    }
                }
                else {
                    context.GetState().Push(tagWorker);
                }
                if (tagWorker is HtmlTagWorker) {
                    ((HtmlTagWorker)tagWorker).ProcessPageRules(node, cssResolver, context);
                }

                if (TagConstants.BODY.Equals(element.Name()) || TagConstants.HTML.Equals(element.Name())) {
                    RunApplier(element, tagWorker);
                }

                context.GetOutlineHandler().AddOutlineAndDestToDocument(tagWorker, element, context);
                
                VisitPseudoElement(element, tagWorker, CssConstants.BEFORE);
                VisitPseudoElement(element, tagWorker, CssConstants.PLACEHOLDER);
                foreach (INode childNode in element.ChildNodes()) {
                    if (!context.IsProcessingInlineSvg()) {
                        Visit(childNode);
                    }
                }
                VisitPseudoElement(element, tagWorker, CssConstants.AFTER);
                if (tagWorker != null) {
                    tagWorker.ProcessEnd(element, context);
                    LinkHelper.CreateDestination(tagWorker, element, context);
                    context.GetOutlineHandler().SetDestinationToElement(tagWorker, element);
                    context.GetState().Pop();
                    if (!TagConstants.BODY.Equals(element.Name()) && !TagConstants.HTML.Equals(element.Name()))
                        RunApplier(element, tagWorker);
                    if (!context.GetState().Empty()) {
                        PageBreakApplierUtil.AddPageBreakElementBefore(context, context.GetState().Top(), element, tagWorker);
                        tagWorker = ProcessRunningElement(tagWorker, element, context);
                        bool childProcessed = context.GetState().Top().ProcessTagChild(tagWorker, context);
                        PageBreakApplierUtil.AddPageBreakElementAfter(context, context.GetState().Top(), element, tagWorker);
                        if (!childProcessed && !ignoredChildTags.Contains(element.Name())) {
                            logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER
                                , context.GetState().Top().GetType().FullName, tagWorker.GetType().FullName));
                        }
                    }
                    else {
                        if (tagWorker.GetElementResult() != null) {
                            roots.Add(tagWorker.GetElementResult());
                        }
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
                                logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_IT_S_TEXT_CONTENT
                                    , context.GetState().Top().GetType().FullName));
                            }
                        }
                        else {
                            logger.Error(iText.Html2pdf.LogMessageConstant.NO_CONSUMER_FOUND_FOR_CONTENT);
                        }
                    }
                }
            }
        }

        private void RunApplier(IElementNode element, ITagWorker tagWorker) {
            ICssApplier cssApplier = context.GetCssApplierFactory().GetCssApplier(element);
            if (cssApplier == null)
            {
                if (!ignoredCssTags.Contains(element.Name()))
                {
                    logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.NO_CSS_APPLIER_FOUND_FOR_TAG, element
                        .Name()));
                }
            }
            else
            {
                cssApplier.Apply(context, element, tagWorker);
            }
        }

        private ITagWorker ProcessRunningElement(ITagWorker tagWorker, IElementNode element, ProcessorContext context) {
            String runningPrefix = CssConstants.RUNNING + "(";
            String positionVal;
            int endBracketInd;
            if (element.GetStyles() == null
                    || (positionVal = element.GetStyles().Get(CssConstants.POSITION)) == null
                    || !positionVal.StartsWith(runningPrefix)
                    // closing bracket should be there and there should be at least one symbol between brackets
                    || (endBracketInd = positionVal.IndexOf(")", StringComparison.Ordinal)) <= runningPrefix.Length) {
                return tagWorker;
            }
    
            String runningElemName = positionVal.JSubstring(runningPrefix.Length, endBracketInd).Trim();
            if (String.IsNullOrEmpty(runningElemName)) {
                return tagWorker;
            }
    
            // TODO For now the whole ITagWorker of the running element is preserved inside RunningElementContainer
            // for the sake of future processing in page margin box. This is somewhat a workaround and storing
            // tag workers might be easily seen as something undesirable, however at least for now it seems to be
            // most suitable solution because:
            // - in any case, processing of the whole running element with it's children should be done in
            //   "normal flow", i.e. in DefaultHtmlProcessor, based on the spec that says that element should be
            //   processed as it was still in the same position in DOM, but visually as if "display: none" was set.
            // - the whole process would need to be repeated in PageContextProcessor again, so it's a double work;
            //   also currently there is still no convenient way for unifying the processing here and in
            //   PageContextProcessor, currently only running elements require processing of the whole hierarchy of
            //   children outside of the default DOM processing and also it's unclear whether this code would be suitable
            //   for the simplified approach of processing all other children of page margin boxes.
            // - ITagWorker is only publicly passed to the constructor, but there is no exposed way to get it out of
            //   RunningElementContainer, so it would be fairly easy to change this approach in future if needed.
            RunningElementContainer runningElementContainer = new RunningElementContainer(element, tagWorker);
            context.GetCssContext().GetRunningManager().AddRunningElement(runningElemName, runningElementContainer);
    
            return new RunningElementTagWorker(runningElementContainer);
        }

        /// <summary>Adds @font-face fonts to the FontProvider.</summary>
        private void AddFontFaceFonts() {
            //TODO Shall we add getFonts() to ICssResolver?
            if (cssResolver is DefaultCssResolver) {
                foreach (CssFontFaceRule fontFace in ((DefaultCssResolver)cssResolver).GetFonts()) {
                    bool findSupportedSrc = false;
                    IList<CssDeclaration> declarations = fontFace.GetProperties();
                    FontFace ff = FontFace.Create(fontFace.GetProperties());
                    if (ff != null) {
                        foreach (FontFace.FontFaceSrc src in ff.GetSources()) {
                            if (CreateFont(ff.GetFontFamily(), src, ResolveUnicodeRange(declarations))) {
                                findSupportedSrc = true;
                                break;
                            }
                        }
                    }
                    if (!findSupportedSrc) {
                        logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_FONT, fontFace)
                            );
                    }
                }
            }
        }

        private Range ResolveUnicodeRange(IList<CssDeclaration> declarations) {
            Range range = null;
            foreach (CssDeclaration descriptor in declarations) {
                if ("unicode-range".Equals(descriptor.GetProperty())) {
                    range = CssUtils.ParseUnicodeRange(descriptor.GetExpression());
                }
            }
            return range;
        }



        /// <summary>Creates a font and adds it to the context.</summary>
        /// <param name="fontFamily">the font family</param>
        /// <param name="src">the source of the font</param>
        /// <param name="src">the unicode range</param>
        /// <returns>true, if successful</returns>
        private bool CreateFont(String fontFamily, FontFace.FontFaceSrc src, Range uniRange) {
            if (!SupportedFontFormat(src.format)) {
                return false;
            }
            else {
                if (src.isLocal) {
                    // to method with lazy initialization
                    ICollection<FontInfo> fonts = context.GetFontProvider().GetFontSet().Get(src.src);
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
                        byte[] bytes = context.GetResourceResolver().RetrieveBytesFromResource(src.src);
                        if (bytes != null) {
                            FontProgram fp = FontProgramFactory.CreateFont(bytes, false);
                            context.AddTemporaryFont(fp, PdfEncodings.IDENTITY_H, fontFamily, uniRange);
                            return true;
                        }
                    }
                    catch (Exception) {
                    }
                    return false;
                }
            }
        }

        /// <summary>Checks whether in general we support requested font format.</summary>
        /// <param name="format">
        /// 
        /// <see cref="FontFace.FontFormat"/>
        /// </param>
        /// <returns>true, if supported or unrecognized.</returns>
        private bool SupportedFontFormat(FontFace.FontFormat format) {
            switch (format) {
                case FontFace.FontFormat.None:
                case FontFace.FontFormat.TrueType:
                case FontFace.FontFormat.OpenType:
                case FontFace.FontFormat.WOFF:
                case FontFace.FontFormat.WOFF2: {
                    return true;
                }

                default: {
                    return false;
                }
            }
        }

        /// <summary>Processes a pseudo element (before and after CSS).</summary>
        /// <param name="node">the node</param>
        /// <param name="pseudoElementName">the pseudo element name</param>
        private void VisitPseudoElement(IElementNode node, ITagWorker tagWorker, String pseudoElementName) {
            switch (pseudoElementName) {
                case CssConstants.BEFORE:
                case CssConstants.AFTER:
                    if (!CssPseudoElementUtil.HasBeforeAfterElements(node)) {
                        return;
                    }

                    break;
                case CssConstants.PLACEHOLDER:
                    if (!(TagConstants.INPUT.Equals(node.Name()) || TagConstants.TEXTAREA.Equals(node.Name())) // TODO DEVSIX-1944: Resolve the issue and remove the line
                        || null == tagWorker
                        || !(tagWorker.GetElementResult() is IPlaceholderable)
                        || null == ((IPlaceholderable) tagWorker.GetElementResult()).GetPlaceholder()) {
                        return;
                    }

                    break;
                default:
                    return;
            }
            Visit(new CssPseudoElementNode(node, pseudoElementName));
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
            return element is CssPseudoElementNode &&
                   CssConstants.PLACEHOLDER.Equals(((CssPseudoElementNode) element).GetPseudoElementName());
        }
    }
}
