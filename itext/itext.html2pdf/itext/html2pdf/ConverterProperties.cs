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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Css.Apply;
using iText.Kernel.Counter.Event;
using iText.Layout.Font;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Resolver.Resource;

namespace iText.Html2pdf {
    /// <summary>
    /// Properties that will be used by the
    /// <see cref="HtmlConverter"/>.
    /// </summary>
    public class ConverterProperties {
        /// <summary>Default maximum number of layouts.</summary>
        private const int DEFAULT_LIMIT_OF_LAYOUTS = 10;

        /// <summary>The media device description.</summary>
        private MediaDeviceDescription mediaDeviceDescription;

        /// <summary>The font provider.</summary>
        private FontProvider fontProvider;

        /// <summary>The tag worker factory.</summary>
        private ITagWorkerFactory tagWorkerFactory;

        /// <summary>The CSS applier factory.</summary>
        private ICssApplierFactory cssApplierFactory;

        /// <summary>The outline handler.</summary>
        private OutlineHandler outlineHandler;

        /// <summary>The base URI.</summary>
        private String baseUri;

        /// <summary>The resource retriever.</summary>
        private IResourceRetriever resourceRetriever;

        /// <summary>Indicates whether an AcroForm should be created.</summary>
        private bool createAcroForm = false;

        /// <summary>Character set used in conversion of input streams</summary>
        private String charset;

        /// <summary>Indicates whether the document should be opened in immediate flush or not</summary>
        private bool immediateFlush = true;

        /// <summary>Indicates whether the document shall process target-counter or not.</summary>
        private bool targetCounterEnabled = false;

        /// <summary>Maximum number of layouts.</summary>
        private int limitOfLayouts = DEFAULT_LIMIT_OF_LAYOUTS;

        /// <summary>Meta info that will be added to the events thrown by html2Pdf.</summary>
        private IMetaInfo metaInfo;

        /// <summary>
        /// Instantiates a new
        /// <see cref="ConverterProperties"/>
        /// instance.
        /// </summary>
        public ConverterProperties() {
        }

        /// <summary>
        /// Instantiates a new
        /// <see cref="ConverterProperties"/>
        /// instance based on another
        /// <see cref="ConverterProperties"/>
        /// instance
        /// (copy constructor).
        /// </summary>
        /// <param name="other">
        /// the other
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public ConverterProperties(iText.Html2pdf.ConverterProperties other) {
            this.mediaDeviceDescription = other.mediaDeviceDescription;
            this.fontProvider = other.fontProvider;
            this.tagWorkerFactory = other.tagWorkerFactory;
            this.cssApplierFactory = other.cssApplierFactory;
            this.baseUri = other.baseUri;
            this.resourceRetriever = other.resourceRetriever;
            this.createAcroForm = other.createAcroForm;
            this.outlineHandler = other.outlineHandler;
            this.charset = other.charset;
            this.metaInfo = other.metaInfo;
            this.targetCounterEnabled = other.targetCounterEnabled;
            this.limitOfLayouts = other.limitOfLayouts;
        }

        /// <summary>Gets the media device description.</summary>
        /// <remarks>
        /// Gets the media device description.
        /// <para />
        /// The properties of the multimedia device are taken into account when creating the SVG and
        /// when processing the properties of the СSS.
        /// </remarks>
        /// <returns>the media device description</returns>
        public virtual MediaDeviceDescription GetMediaDeviceDescription() {
            return mediaDeviceDescription;
        }

        /// <summary>Sets the media device description.</summary>
        /// <remarks>
        /// Sets the media device description.
        /// <para />
        /// The properties of the multimedia device are taken into account when creating the SVG and
        /// when processing the properties of the СSS.
        /// </remarks>
        /// <param name="mediaDeviceDescription">the media device description</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetMediaDeviceDescription(MediaDeviceDescription mediaDeviceDescription
            ) {
            this.mediaDeviceDescription = mediaDeviceDescription;
            return this;
        }

        /// <summary>Gets the font provider.</summary>
        /// <remarks>
        /// Gets the font provider.
        /// <para />
        /// Please note that
        /// <see cref="iText.Layout.Font.FontProvider"/>
        /// instances cannot be reused across several documents
        /// and thus as soon as you set this property, this
        /// <see cref="ConverterProperties"/>
        /// instance becomes only useful
        /// for a single HTML conversion.
        /// </remarks>
        /// <returns>the font provider</returns>
        public virtual FontProvider GetFontProvider() {
            return fontProvider;
        }

        /// <summary>Sets the font provider.</summary>
        /// <remarks>
        /// Sets the font provider.
        /// <para />
        /// Please note that
        /// <see cref="iText.Layout.Font.FontProvider"/>
        /// instances cannot be reused across several documents
        /// and thus as soon as you set this property, this
        /// <see cref="ConverterProperties"/>
        /// instance becomes only useful
        /// for a single HTML conversion.
        /// </remarks>
        /// <param name="fontProvider">the font provider</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetFontProvider(FontProvider fontProvider) {
            this.fontProvider = fontProvider;
            return this;
        }

        /// <summary>Gets maximum number of layouts.</summary>
        /// <returns>layouts limit</returns>
        public virtual int GetLimitOfLayouts() {
            return limitOfLayouts;
        }

        /// <summary>Sets maximum number of layouts.</summary>
        /// <param name="limitOfLayouts">layouts limit</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetLimitOfLayouts(int limitOfLayouts) {
            this.limitOfLayouts = limitOfLayouts;
            return this;
        }

        /// <summary>Checks if target-counter is enabled.</summary>
        /// <returns>true if target-counter shall be processed, false otherwise</returns>
        public virtual bool IsTargetCounterEnabled() {
            return targetCounterEnabled;
        }

        /// <summary>Sets the targetCounterEnabled flag.</summary>
        /// <param name="targetCounterEnabled">true if target-counter shall be processed, false otherwise</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetTargetCounterEnabled(bool targetCounterEnabled) {
            this.targetCounterEnabled = targetCounterEnabled;
            return this;
        }

        /// <summary>Gets the TagWorkerFactory instance.</summary>
        /// <remarks>
        /// Gets the TagWorkerFactory instance.
        /// <para />
        /// The tagWorkerFactory is used to create
        /// <see cref="iText.Html2pdf.Attach.ITagWorker"/>
        /// , which in turn
        /// are used to convert the HTML tags to the PDF elements.
        /// </remarks>
        /// <returns>the TagWorkerFactory</returns>
        public virtual ITagWorkerFactory GetTagWorkerFactory() {
            return tagWorkerFactory;
        }

        /// <summary>Sets the TagWorkerFactory.</summary>
        /// <remarks>
        /// Sets the TagWorkerFactory.
        /// <para />
        /// The tagWorkerFactory is used to create
        /// <see cref="iText.Html2pdf.Attach.ITagWorker"/>
        /// , which in turn
        /// are used to convert the HTML tags to the PDF elements.
        /// </remarks>
        /// <param name="tagWorkerFactory">the TagWorkerFactory</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetTagWorkerFactory(ITagWorkerFactory tagWorkerFactory) {
            this.tagWorkerFactory = tagWorkerFactory;
            return this;
        }

        /// <summary>Gets the CSS applier factory.</summary>
        /// <remarks>
        /// Gets the CSS applier factory.
        /// <para />
        /// The cssApplierFactory is used to create
        /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
        /// , which in turn
        /// are used to convert the CSS properties to the PDF properties.
        /// </remarks>
        /// <returns>the CSS applier factory</returns>
        public virtual ICssApplierFactory GetCssApplierFactory() {
            return cssApplierFactory;
        }

        /// <summary>Sets the CSS applier factory.</summary>
        /// <remarks>
        /// Sets the CSS applier factory.
        /// <para />
        /// The cssApplierFactory is used to create
        /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
        /// , which in turn
        /// are used to convert the CSS properties to the PDF properties.
        /// </remarks>
        /// <param name="cssApplierFactory">the CSS applier factory</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetCssApplierFactory(ICssApplierFactory cssApplierFactory
            ) {
            this.cssApplierFactory = cssApplierFactory;
            return this;
        }

        /// <summary>Gets the base URI.</summary>
        /// <remarks>
        /// Gets the base URI.
        /// <para />
        /// Base URI is used to resolve other URI.
        /// </remarks>
        /// <returns>the base URI</returns>
        public virtual String GetBaseUri() {
            return baseUri;
        }

        /// <summary>Sets the base URI.</summary>
        /// <remarks>
        /// Sets the base URI.
        /// <para />
        /// Base URI is used to resolve other URI.
        /// </remarks>
        /// <param name="baseUri">the base URI</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetBaseUri(String baseUri) {
            this.baseUri = baseUri;
            return this;
        }

        /// <summary>Gets the resource retriever.</summary>
        /// <remarks>
        /// Gets the resource retriever.
        /// <para />
        /// The resourceRetriever is used to retrieve data from resources by URL.
        /// </remarks>
        /// <returns>the resource retriever</returns>
        public virtual IResourceRetriever GetResourceRetriever() {
            return resourceRetriever;
        }

        /// <summary>Sets the resource retriever.</summary>
        /// <remarks>
        /// Sets the resource retriever.
        /// <para />
        /// The resourceRetriever is used to retrieve data from resources by URL.
        /// </remarks>
        /// <param name="resourceRetriever">the resource retriever</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetResourceRetriever(IResourceRetriever resourceRetriever
            ) {
            this.resourceRetriever = resourceRetriever;
            return this;
        }

        /// <summary>Check if the createAcroForm flag is set.</summary>
        /// <remarks>
        /// Check if the createAcroForm flag is set.
        /// <para />
        /// If createAcroForm is set, then when the form is encountered in HTML, AcroForm will be created, otherwise
        /// a visually identical, but not functional element will be created. Please bare in mind that the created
        /// Acroform may visually differ a bit from the HTML one.
        /// </remarks>
        /// <returns>the createAcroForm flag</returns>
        public virtual bool IsCreateAcroForm() {
            return createAcroForm;
        }

        /// <summary>Sets the createAcroForm value.</summary>
        /// <remarks>
        /// Sets the createAcroForm value.
        /// <para />
        /// If createAcroForm is set, then when the form is encountered in HTML, AcroForm will be created, otherwise
        /// a visually identical, but not functional element will be created. Please bare in mind that the created
        /// Acroform may visually differ a bit from the HTML one.
        /// </remarks>
        /// <param name="createAcroForm">true if an AcroForm needs to be created</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetCreateAcroForm(bool createAcroForm) {
            this.createAcroForm = createAcroForm;
            return this;
        }

        /// <summary>Gets the outline handler.</summary>
        /// <remarks>
        /// Gets the outline handler.
        /// <para />
        /// If outlineHandler is specified, then outlines will be created in the PDF
        /// for HTML tags specified in outlineHandler.
        /// <para />
        /// Please note that
        /// <see cref="iText.Html2pdf.Attach.Impl.OutlineHandler"/>
        /// is not thread safe, thus
        /// as soon as you have set this property, this
        /// <see cref="ConverterProperties"/>
        /// instance cannot be used in
        /// converting multiple HTMLs simultaneously.
        /// </remarks>
        /// <returns>the outline handler</returns>
        public virtual OutlineHandler GetOutlineHandler() {
            return outlineHandler;
        }

        /// <summary>Sets the outline handler.</summary>
        /// <remarks>
        /// Sets the outline handler.
        /// <para />
        /// If outlineHandler is specified, then outlines will be created in the PDF
        /// for HTML tags specified in outlineHandler.
        /// <para />
        /// Please note that
        /// <see cref="iText.Html2pdf.Attach.Impl.OutlineHandler"/>
        /// is not thread safe, thus
        /// as soon as you have set this property, this
        /// <see cref="ConverterProperties"/>
        /// instance cannot be used in
        /// converting multiple HTMLs simultaneously.
        /// </remarks>
        /// <param name="outlineHandler">the outline handler</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetOutlineHandler(OutlineHandler outlineHandler) {
            this.outlineHandler = outlineHandler;
            return this;
        }

        /// <summary>Gets the encoding charset.</summary>
        /// <remarks>
        /// Gets the encoding charset.
        /// <para />
        /// Charset is used to correctly decode an HTML file.
        /// </remarks>
        /// <returns>the charset</returns>
        public virtual String GetCharset() {
            return charset;
        }

        /// <summary>Sets the encoding charset.</summary>
        /// <remarks>
        /// Sets the encoding charset.
        /// <para />
        /// Charset is used to correctly decode an HTML file.
        /// </remarks>
        /// <param name="charset">the charset</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetCharset(String charset) {
            this.charset = charset;
            return this;
        }

        /// <summary>Checks if immediateFlush is set.</summary>
        /// <remarks>
        /// Checks if immediateFlush is set.
        /// <para />
        /// This is used for
        /// <see cref="HtmlConverter.ConvertToDocument(System.String, iText.Kernel.Pdf.PdfWriter)"/>
        /// methods and will be
        /// overwritten to false if a page-counter declaration is present in the CSS of the HTML being converted.
        /// Has no effect when used in conjunction with
        /// <see cref="HtmlConverter.ConvertToPdf(System.String, System.IO.Stream)"/>
        /// or
        /// <see cref="HtmlConverter.ConvertToElements(System.String)"/>.
        /// </remarks>
        /// <returns>true if immediateFlush is set, false if not</returns>
        public virtual bool IsImmediateFlush() {
            return immediateFlush;
        }

        /// <summary>Set the immediate flush property of the layout document.</summary>
        /// <remarks>
        /// Set the immediate flush property of the layout document.
        /// <para />
        /// This is used for
        /// <see cref="HtmlConverter.ConvertToDocument(System.String, iText.Kernel.Pdf.PdfWriter)"/>
        /// methods and will be
        /// overwritten to false if a page-counter declaration is present in the CSS of the HTML being converted.
        /// Has no effect when used in conjunction with
        /// <see cref="HtmlConverter.ConvertToPdf(System.String, System.IO.Stream)"/>
        /// or
        /// <see cref="HtmlConverter.ConvertToElements(System.String)"/>.
        /// </remarks>
        /// <param name="immediateFlush">the immediate flush value</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetImmediateFlush(bool immediateFlush) {
            this.immediateFlush = immediateFlush;
            return this;
        }

        /// <summary>Gets html meta info.</summary>
        /// <remarks>
        /// Gets html meta info.
        /// <para />
        /// This meta info will be passed with to
        /// <see cref="iText.Kernel.Counter.EventCounter"/>
        /// with
        /// <see cref="iText.Html2pdf.Events.PdfHtmlEvent"/>
        /// and can be used to determine event origin.
        /// </remarks>
        /// <returns>
        /// converter's
        /// <see cref="iText.Kernel.Counter.Event.IMetaInfo"/>
        /// </returns>
        public virtual IMetaInfo GetEventCountingMetaInfo() {
            return metaInfo;
        }

        /// <summary>Sets html meta info.</summary>
        /// <remarks>
        /// Sets html meta info.
        /// <para />
        /// This meta info will be passed with to
        /// <see cref="iText.Kernel.Counter.EventCounter"/>
        /// with
        /// <see cref="iText.Html2pdf.Events.PdfHtmlEvent"/>
        /// and can be used to determine event origin.
        /// </remarks>
        /// <param name="metaInfo">meta info to set</param>
        /// <returns>
        /// the
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.ConverterProperties SetEventCountingMetaInfo(IMetaInfo metaInfo) {
            this.metaInfo = metaInfo;
            return this;
        }
    }
}
