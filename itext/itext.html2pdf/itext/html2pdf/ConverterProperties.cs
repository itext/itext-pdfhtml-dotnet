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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Media;
using iText.Layout.Font;

namespace iText.Html2pdf {
    /// <summary>Properties that will be used by the converter.</summary>
    public class ConverterProperties {
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

        /// <summary>Indicates whether an AcroForm should be created.</summary>
        private bool createAcroForm = false;

        /// <summary>Character set used in conversion of input streams</summary>
        private String charset;

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
            this.createAcroForm = other.createAcroForm;
            this.outlineHandler = other.outlineHandler;
            this.charset = other.charset;
        }

        /// <summary>Gets the media device description.</summary>
        /// <returns>the media device description</returns>
        public virtual MediaDeviceDescription GetMediaDeviceDescription() {
            return mediaDeviceDescription;
        }

        /// <summary>Sets the media device description.</summary>
        /// <param name="mediaDeviceDescription">the media device description</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetMediaDeviceDescription(MediaDeviceDescription mediaDeviceDescription
            ) {
            this.mediaDeviceDescription = mediaDeviceDescription;
            return this;
        }

        /// <summary>Gets the font provider.</summary>
        /// <returns>the font provider</returns>
        public virtual FontProvider GetFontProvider() {
            return fontProvider;
        }

        /// <summary>Sets the font provider.</summary>
        /// <remarks>
        /// Sets the font provider. Please note that
        /// <see cref="iText.Layout.Font.FontProvider"/>
        /// instances cannot be reused across several documents
        /// and thus as soon as you set this property, this
        /// <see cref="ConverterProperties"/>
        /// instance becomes only useful for a single
        /// HTML conversion.
        /// </remarks>
        /// <param name="fontProvider">the font provider</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetFontProvider(FontProvider fontProvider) {
            this.fontProvider = fontProvider;
            return this;
        }

        /// <summary>Gets the TagWorkerFactory instance.</summary>
        /// <returns>the TagWorkerFactory</returns>
        public virtual ITagWorkerFactory GetTagWorkerFactory() {
            return tagWorkerFactory;
        }

        /// <summary>Sets the TagWorkerFactory.</summary>
        /// <param name="tagWorkerFactory">the TagWorkerFactory</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetTagWorkerFactory(ITagWorkerFactory tagWorkerFactory) {
            this.tagWorkerFactory = tagWorkerFactory;
            return this;
        }

        /// <summary>Gets the CSS applier factory.</summary>
        /// <returns>the CSS applier factory</returns>
        public virtual ICssApplierFactory GetCssApplierFactory() {
            return cssApplierFactory;
        }

        /// <summary>Sets the CSS applier factory.</summary>
        /// <param name="cssApplierFactory">the CSS applier factory</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetCssApplierFactory(ICssApplierFactory cssApplierFactory
            ) {
            this.cssApplierFactory = cssApplierFactory;
            return this;
        }

        /// <summary>Gets the base URI.</summary>
        /// <returns>the base URI</returns>
        public virtual String GetBaseUri() {
            return baseUri;
        }

        /// <summary>Sets the base URI.</summary>
        /// <param name="baseUri">the base URI</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetBaseUri(String baseUri) {
            this.baseUri = baseUri;
            return this;
        }

        /// <summary>Checks if is an AcroForm needs to be created.</summary>
        /// <returns>true, an AcroForm should be created</returns>
        public virtual bool IsCreateAcroForm() {
            return createAcroForm;
        }

        /// <summary>Sets the createAcroForm value.</summary>
        /// <param name="createAcroForm">true if an AcroForm needs to be created</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetCreateAcroForm(bool createAcroForm) {
            this.createAcroForm = createAcroForm;
            return this;
        }

        /// <summary>Gets the outline handler.</summary>
        /// <returns>the outline handler</returns>
        public virtual OutlineHandler GetOutlineHandler() {
            return outlineHandler;
        }

        /// <summary>Sets the outline handler.</summary>
        /// <remarks>
        /// Sets the outline handler. Please note that
        /// <see cref="iText.Html2pdf.Attach.Impl.OutlineHandler"/>
        /// is not thread safe, thus
        /// as soon as you have set this property, this
        /// <see cref="ConverterProperties"/>
        /// instance cannot be used in converting multiple
        /// HTMLs simultaneously.
        /// </remarks>
        /// <param name="outlineHandler">the outline handler</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetOutlineHandler(OutlineHandler outlineHandler) {
            this.outlineHandler = outlineHandler;
            return this;
        }

        /// <summary>Gets the encoding charset.</summary>
        /// <returns>the charset</returns>
        public virtual String GetCharset() {
            return charset;
        }

        /// <summary>Sets the encoding charset.</summary>
        /// <param name="charset">the charset</param>
        /// <returns>the ConverterProperties instance</returns>
        public virtual iText.Html2pdf.ConverterProperties SetCharset(String charset) {
            this.charset = charset;
            return this;
        }
    }
}
