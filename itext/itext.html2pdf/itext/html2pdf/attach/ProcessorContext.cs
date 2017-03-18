/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Resolver.Font;
using iText.Html2pdf.Resolver.Resource;
using iText.Kernel.Pdf;
using iText.Layout.Font;

namespace iText.Html2pdf.Attach {
    public class ProcessorContext {
        private FontProvider fontProvider;

        private IList<FontInfo> tempFonts;

        private ResourceResolver resourceResolver;

        private MediaDeviceDescription deviceDescription;

        private ITagWorkerFactory tagWorkerFactory;

        private ICssApplierFactory cssApplierFactory;

        private String baseUri;

        private State state;

        private CssContext cssContext;

        private PdfDocument pdfDocument;

        public ProcessorContext(ConverterProperties converterProperties) {
            // Variable fields
            if (converterProperties == null) {
                converterProperties = new ConverterProperties();
            }
            state = new State();
            deviceDescription = converterProperties.GetMediaDeviceDescription();
            if (deviceDescription == null) {
                deviceDescription = MediaDeviceDescription.CreateDefault();
            }
            fontProvider = converterProperties.GetFontProvider();
            if (fontProvider == null) {
                fontProvider = new DefaultFontProvider();
            }
            tempFonts = new List<FontInfo>();
            tagWorkerFactory = converterProperties.GetTagWorkerFactory();
            if (tagWorkerFactory == null) {
                tagWorkerFactory = new DefaultTagWorkerFactory();
            }
            cssApplierFactory = converterProperties.GetCssApplierFactory();
            if (cssApplierFactory == null) {
                cssApplierFactory = new DefaultCssApplierFactory();
            }
            baseUri = converterProperties.GetBaseUri();
            if (baseUri == null) {
                baseUri = "";
            }
            resourceResolver = new ResourceResolver(baseUri);
            cssContext = new CssContext();
        }

        public virtual void SetFontProvider(FontProvider fontProvider) {
            this.fontProvider = fontProvider;
        }

        public virtual State GetState() {
            return state;
        }

        public virtual PdfDocument GetPdfDocument() {
            return pdfDocument;
        }

        public virtual FontProvider GetFontProvider() {
            return fontProvider;
        }

        public virtual ResourceResolver GetResourceResolver() {
            return resourceResolver;
        }

        public virtual MediaDeviceDescription GetDeviceDescription() {
            return deviceDescription;
        }

        public virtual ITagWorkerFactory GetTagWorkerFactory() {
            return tagWorkerFactory;
        }

        public virtual ICssApplierFactory GetCssApplierFactory() {
            return cssApplierFactory;
        }

        public virtual CssContext GetCssContext() {
            return cssContext;
        }

        public virtual void AddTemporaryFont(FontInfo fontInfo) {
            tempFonts.Add(fontInfo);
        }

        public virtual void Reset() {
            this.pdfDocument = null;
            this.state = new State();
            this.resourceResolver.ResetCache();
            this.cssContext = new CssContext();
            ResetTemporaryFonts();
        }

        public virtual void Reset(PdfDocument pdfDocument) {
            Reset();
            this.pdfDocument = pdfDocument;
        }

        private void ResetTemporaryFonts() {
            foreach (FontInfo fi in tempFonts) {
                fontProvider.GetFontSet().Remove(fi);
            }
            tempFonts.Clear();
        }
    }
}
