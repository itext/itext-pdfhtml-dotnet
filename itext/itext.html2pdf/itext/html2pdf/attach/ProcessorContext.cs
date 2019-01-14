/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Resolver.Font;
using iText.Html2pdf.Resolver.Form;
using iText.Html2pdf.Resolver.Resource;
using iText.IO.Font;
using iText.Kernel.Counter.Event;
using iText.Kernel.Pdf;
using iText.Layout.Font;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Resolver.Resource;

namespace iText.Html2pdf.Attach {
    /// <summary>Keeps track of the context of the processor.</summary>
    public class ProcessorContext {
        /// <summary>The font provider.</summary>
        private FontProvider fontProvider;

        /// <summary>Temporary set of fonts used in the PDF.</summary>
        private FontSet tempFonts;

        /// <summary>The resource resolver.</summary>
        private ResourceResolver resourceResolver;

        /// <summary>The device description.</summary>
        private MediaDeviceDescription deviceDescription;

        /// <summary>The tag worker factory.</summary>
        private ITagWorkerFactory tagWorkerFactory;

        /// <summary>The CSS applier factory.</summary>
        private ICssApplierFactory cssApplierFactory;

        /// <summary>The base URI.</summary>
        private String baseUri;

        /// <summary>Indicates whether an AcroForm needs to be created.</summary>
        private bool createAcroForm;

        /// <summary>The form field name resolver.</summary>
        private FormFieldNameResolver formFieldNameResolver;

        /// <summary>The radio check resolver.</summary>
        private RadioCheckResolver radioCheckResolver;

        /// <summary>The outline handler.</summary>
        private OutlineHandler outlineHandler;

        /// <summary>Indicates whether the document should be opened in immediate flush or not</summary>
        private bool immediateFlush;

        /// <summary>The state.</summary>
        private State state;

        /// <summary>The CSS context.</summary>
        private CssContext cssContext;

        /// <summary>The link context</summary>
        private LinkContext linkContext;

        /// <summary>The PDF document.</summary>
        private PdfDocument pdfDocument;

        /// <summary>The Processor meta info</summary>
        private IMetaInfo metaInfo;

        /// <summary>Internal state variable to keep track of whether the processor is currently inside an inlineSvg</summary>
        private bool processingInlineSvg;

        /// <summary>
        /// Instantiates a new
        /// <see cref="ProcessorContext"/>
        /// instance.
        /// </summary>
        /// <param name="converterProperties">
        /// a
        /// <see cref="iText.Html2pdf.ConverterProperties"/>
        /// instance
        /// </param>
        public ProcessorContext(ConverterProperties converterProperties) {
            // Variable fields
            if (converterProperties == null) {
                converterProperties = new ConverterProperties();
            }
            state = new State();
            deviceDescription = converterProperties.GetMediaDeviceDescription();
            if (deviceDescription == null) {
                deviceDescription = MediaDeviceDescription.GetDefault();
            }
            fontProvider = converterProperties.GetFontProvider();
            if (fontProvider == null) {
                fontProvider = new DefaultFontProvider();
            }
            tagWorkerFactory = converterProperties.GetTagWorkerFactory();
            if (tagWorkerFactory == null) {
                tagWorkerFactory = DefaultTagWorkerFactory.GetInstance();
            }
            cssApplierFactory = converterProperties.GetCssApplierFactory();
            if (cssApplierFactory == null) {
                cssApplierFactory = DefaultCssApplierFactory.GetInstance();
            }
            baseUri = converterProperties.GetBaseUri();
            if (baseUri == null) {
                baseUri = "";
            }
            outlineHandler = converterProperties.GetOutlineHandler();
            if (outlineHandler == null) {
                outlineHandler = new OutlineHandler();
            }
            resourceResolver = new HtmlResourceResolver(baseUri, this);
            cssContext = new CssContext();
            linkContext = new LinkContext();
            createAcroForm = converterProperties.IsCreateAcroForm();
            formFieldNameResolver = new FormFieldNameResolver();
            radioCheckResolver = new RadioCheckResolver();
            immediateFlush = converterProperties.IsImmediateFlush();
            metaInfo = converterProperties.GetEventCountingMetaInfo();
            processingInlineSvg = false;
        }

        /// <summary>Sets the font provider.</summary>
        /// <param name="fontProvider">the new font provider</param>
        public virtual void SetFontProvider(FontProvider fontProvider) {
            this.fontProvider = fontProvider;
        }

        /// <summary>Gets the state.</summary>
        /// <returns>the state</returns>
        public virtual State GetState() {
            return state;
        }

        /// <summary>Gets the PDF document.</summary>
        /// <returns>the PDF document</returns>
        public virtual PdfDocument GetPdfDocument() {
            return pdfDocument;
        }

        /// <summary>Gets the font provider.</summary>
        /// <returns>the font provider</returns>
        public virtual FontProvider GetFontProvider() {
            return fontProvider;
        }

        /// <summary>Gets the temporary set of fonts.</summary>
        /// <returns>the set of fonts</returns>
        public virtual FontSet GetTempFonts() {
            return tempFonts;
        }

        /// <summary>Gets the resource resolver.</summary>
        /// <returns>the resource resolver</returns>
        public virtual ResourceResolver GetResourceResolver() {
            return resourceResolver;
        }

        /// <summary>Gets the device description.</summary>
        /// <returns>the device description</returns>
        public virtual MediaDeviceDescription GetDeviceDescription() {
            return deviceDescription;
        }

        /// <summary>Gets the tag worker factory.</summary>
        /// <returns>the tag worker factory</returns>
        public virtual ITagWorkerFactory GetTagWorkerFactory() {
            return tagWorkerFactory;
        }

        /// <summary>Gets the CSS applier factory.</summary>
        /// <returns>the CSS applier factory</returns>
        public virtual ICssApplierFactory GetCssApplierFactory() {
            return cssApplierFactory;
        }

        /// <summary>Gets the CSS context.</summary>
        /// <returns>the CSS context</returns>
        public virtual CssContext GetCssContext() {
            return cssContext;
        }

        /// <summary>Gets the link context.</summary>
        /// <returns>the link context</returns>
        public virtual LinkContext GetLinkContext() {
            return linkContext;
        }

        /// <summary>Checks if is an AcroForm needs to be created.</summary>
        /// <returns>true, an AcroForm should be created</returns>
        public virtual bool IsCreateAcroForm() {
            return createAcroForm;
        }

        /// <summary>Gets the form field name resolver.</summary>
        /// <returns>the form field name resolver</returns>
        public virtual FormFieldNameResolver GetFormFieldNameResolver() {
            return formFieldNameResolver;
        }

        /// <summary>Gets the radio check resolver.</summary>
        /// <returns>the radio check resolver</returns>
        public virtual RadioCheckResolver GetRadioCheckResolver() {
            return radioCheckResolver;
        }

        /// <summary>Gets the outline handler.</summary>
        /// <returns>the outline handler</returns>
        public virtual OutlineHandler GetOutlineHandler() {
            return outlineHandler;
        }

        /// <summary>Add temporary font from @font-face.</summary>
        /// <param name="fontInfo">the font info</param>
        /// <param name="alias">the alias</param>
        public virtual void AddTemporaryFont(FontInfo fontInfo, String alias) {
            if (tempFonts == null) {
                tempFonts = new FontSet();
            }
            tempFonts.AddFont(fontInfo, alias);
        }

        /// <summary>Add temporary font from @font-face.</summary>
        /// <param name="fontProgram">the font program</param>
        /// <param name="encoding">the encoding</param>
        /// <param name="alias">the alias</param>
        public virtual void AddTemporaryFont(FontProgram fontProgram, String encoding, String alias) {
            if (tempFonts == null) {
                tempFonts = new FontSet();
            }
            tempFonts.AddFont(fontProgram, encoding, alias);
        }

        /// <summary>Add temporary font from @font-face.</summary>
        /// <param name="fontProgram">the font program</param>
        /// <param name="encoding">the encoding</param>
        /// <param name="alias">the alias</param>
        /// <param name="unicodeRange">the unicode range</param>
        public virtual void AddTemporaryFont(FontProgram fontProgram, String encoding, String alias, Range unicodeRange
            ) {
            if (tempFonts == null) {
                tempFonts = new FontSet();
            }
            tempFonts.AddFont(fontProgram, encoding, alias, unicodeRange);
        }

        /// <summary>Check fonts in font provider and temporary font set.</summary>
        /// <returns>true, if there is at least one font either in FontProvider or temporary FontSet.</returns>
        /// <seealso cref="AddTemporaryFont(iText.Layout.Font.FontInfo, System.String)"/>
        /// <seealso cref="AddTemporaryFont(iText.IO.Font.FontProgram, System.String, System.String)"/>
        public virtual bool HasFonts() {
            return !fontProvider.GetFontSet().IsEmpty() || (tempFonts != null && !tempFonts.IsEmpty());
        }

        /// <summary>Resets the context.</summary>
        public virtual void Reset() {
            this.pdfDocument = null;
            this.state = new State();
            this.resourceResolver.ResetCache();
            this.cssContext = new CssContext();
            this.linkContext = new LinkContext();
            this.formFieldNameResolver.Reset();
            //Reset font provider. PdfFonts shall be reseted.
            this.fontProvider.Reset();
            this.tempFonts = null;
            this.outlineHandler.Reset();
            this.processingInlineSvg = false;
        }

        /// <summary>Resets the context, and assigns a new PDF document.</summary>
        /// <param name="pdfDocument">the new PDF document for the context</param>
        public virtual void Reset(PdfDocument pdfDocument) {
            Reset();
            this.pdfDocument = pdfDocument;
        }

        /// <summary>Gets the baseURI: the URI which has been set manually or the directory of the html file in case when baseURI hasn't been set manually.
        ///     </summary>
        /// <returns>the baseUri</returns>
        public virtual String GetBaseUri() {
            return baseUri;
        }

        /// <summary>Checks if immediateFlush is set</summary>
        /// <returns>true if immediateFlush is set, false if not.</returns>
        public virtual bool IsImmediateFlush() {
            return immediateFlush;
        }

        /// <summary>Gets html meta info.</summary>
        /// <remarks>
        /// Gets html meta info. This meta info will be passed with to
        /// <see cref="iText.Kernel.Counter.EventCounter"/>
        /// with
        /// <see cref="iText.Html2pdf.Events.PdfHtmlEvent"/>
        /// and can be used to determine event origin.
        /// </remarks>
        /// <returns>html meta info</returns>
        public virtual IMetaInfo GetEventCountingMetaInfo() {
            return metaInfo;
        }

        /// <summary>Check if the processor is currently processing an inline svg</summary>
        /// <returns>True if the processor is processing an inline Svg, false otherwise.</returns>
        public virtual bool IsProcessingInlineSvg() {
            return processingInlineSvg;
        }

        /// <summary>Set the processor to processing Inline Svg state</summary>
        public virtual void StartProcessingInlineSvg() {
            processingInlineSvg = true;
        }

        /// <summary>End the processing Svg State</summary>
        public virtual void EndProcessingInlineSvg() {
            processingInlineSvg = false;
        }
    }
}
