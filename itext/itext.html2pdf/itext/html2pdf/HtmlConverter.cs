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
using System.IO;
using iText.Commons.Actions.Contexts;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Logs;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.Pdfa;
using iText.Pdfua;
using iText.StyledXmlParser;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup;
using iText.StyledXmlParser.Resolver.Font;

namespace iText.Html2pdf {
    /// <summary>The HtmlConverter is the class you will use most when converting HTML to PDF.</summary>
    /// <remarks>
    /// The HtmlConverter is the class you will use most when converting HTML to PDF.
    /// It contains a series of static methods that accept HTML as a
    /// <see cref="System.String"/>
    /// ,
    /// <see cref="System.IO.FileInfo"/>
    /// , or
    /// <see cref="System.IO.Stream"/>
    /// , and convert it to PDF in the
    /// form of an
    /// <see cref="System.IO.Stream"/>
    /// ,
    /// <see cref="System.IO.FileInfo"/>
    /// , or a series of
    /// iText elements. It's also possible to write to a
    /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
    /// or
    /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
    /// instance.
    /// </remarks>
    public class HtmlConverter {
        /// <summary>Instantiates a new HtmlConverter instance.</summary>
        private HtmlConverter() {
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to an
        /// <see cref="System.IO.Stream"/>
        /// containing PDF.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfStream">
        /// the PDF as an
        /// <see cref="System.IO.Stream"/>
        /// </param>
        public static void ConvertToPdf(String html, Stream pdfStream) {
            ConvertToPdf(html, pdfStream, null);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to an
        /// <see cref="System.IO.Stream"/>
        /// containing PDF, using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfStream">
        /// the PDF as an
        /// <see cref="System.IO.Stream"/>
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(String html, Stream pdfStream, ConverterProperties converterProperties) {
            ConvertToPdf(html, new PdfWriter(pdfStream), converterProperties);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to PDF by writing PDF content
        /// to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// instance.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfWriter">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(String html, PdfWriter pdfWriter) {
            ConvertToPdf(html, pdfWriter, null);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to PDF by writing PDF content
        /// to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// instance, using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfWriter">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// instance
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(String html, PdfWriter pdfWriter, ConverterProperties converterProperties) {
            DocumentProperties documentProperties = new DocumentProperties().SetEventCountingMetaInfo(ResolveMetaInfo(
                converterProperties));
            PdfDocument document = CreateCorrectDocument(converterProperties, documentProperties, pdfWriter);
            converterProperties = SetupCustomConversionProperties(document, converterProperties);
            ConvertToPdf(html, document, converterProperties);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to objects that
        /// will be added to a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// , using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// instance
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(String html, PdfDocument pdfDocument, ConverterProperties converterProperties
            ) {
            Document document = ConvertToDocument(html, pdfDocument, converterProperties);
            document.SetProperty(Property.META_INFO, new MetaInfoContainer(ResolveMetaInfo(converterProperties)));
            document.Close();
        }

        /// <summary>
        /// Converts HTML stored in a
        /// <see cref="System.IO.FileInfo"/>
        /// to a PDF
        /// <see cref="System.IO.FileInfo"/>.
        /// </summary>
        /// <param name="htmlFile">
        /// the
        /// <see cref="System.IO.FileInfo"/>
        /// containing the source HTML
        /// </param>
        /// <param name="pdfFile">
        /// the
        /// <see cref="System.IO.FileInfo"/>
        /// containing the resulting PDF
        /// </param>
        public static void ConvertToPdf(FileInfo htmlFile, FileInfo pdfFile) {
            ConvertToPdf(htmlFile, pdfFile, null);
        }

        /// <summary>
        /// Converts HTML stored in a
        /// <see cref="System.IO.FileInfo"/>
        /// to a PDF
        /// <see cref="System.IO.FileInfo"/>
        /// ,
        /// using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="htmlFile">
        /// the
        /// <see cref="System.IO.FileInfo"/>
        /// containing the source HTML
        /// </param>
        /// <param name="pdfFile">
        /// the
        /// <see cref="System.IO.FileInfo"/>
        /// containing the resulting PDF
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(FileInfo htmlFile, FileInfo pdfFile, ConverterProperties converterProperties
            ) {
            if (converterProperties == null) {
                String baseUri = FileUtil.GetParentDirectoryUri(htmlFile);
                converterProperties = new ConverterProperties().SetBaseUri(baseUri);
            }
            else {
                if (converterProperties.GetBaseUri() == null) {
                    String baseUri = FileUtil.GetParentDirectoryUri(htmlFile);
                    converterProperties = new ConverterProperties(converterProperties).SetBaseUri(baseUri);
                }
            }
            using (FileStream fileInputStream = new FileStream(htmlFile.FullName, FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(pdfFile.FullName, FileMode.Create)) {
                    ConvertToPdf(fileInputStream, fileOutputStream, converterProperties);
                }
            }
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to a PDF written to
        /// an
        /// <see cref="System.IO.Stream"/>.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// for the resulting PDF
        /// </param>
        public static void ConvertToPdf(Stream htmlStream, Stream pdfStream) {
            ConvertToPdf(htmlStream, pdfStream, null);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to a PDF written to
        /// an
        /// <see cref="System.IO.Stream"/>.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// for the resulting PDF
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(Stream htmlStream, Stream pdfStream, ConverterProperties converterProperties
            ) {
            ConvertToPdf(htmlStream, new PdfWriter(pdfStream), converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to objects that
        /// will be added to a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(Stream htmlStream, PdfDocument pdfDocument) {
            ConvertToPdf(htmlStream, pdfDocument, null);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to content that
        /// will be written to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfWriter">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// containing the resulting PDF
        /// </param>
        public static void ConvertToPdf(Stream htmlStream, PdfWriter pdfWriter) {
            ConvertToPdf(htmlStream, new PdfDocument(pdfWriter, new DocumentProperties().SetEventCountingMetaInfo(CreatePdf2HtmlMetaInfo
                ())));
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to content that
        /// will be written to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// , using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfWriter">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// containing the resulting PDF
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(Stream htmlStream, PdfWriter pdfWriter, ConverterProperties converterProperties
            ) {
            DocumentProperties props = new DocumentProperties().SetEventCountingMetaInfo(ResolveMetaInfo(converterProperties
                ));
            PdfDocument document = CreateCorrectDocument(converterProperties, props, pdfWriter);
            ConvertToPdf(htmlStream, document, converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to objects that
        /// will be added to a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// , using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// instance
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        public static void ConvertToPdf(Stream htmlStream, PdfDocument pdfDocument, ConverterProperties converterProperties
            ) {
            converterProperties = SetupCustomConversionProperties(pdfDocument, converterProperties);
            Document document = ConvertToDocument(htmlStream, pdfDocument, converterProperties);
            IMetaInfo metaInfo = ResolveMetaInfo(converterProperties);
            document.SetProperty(Property.META_INFO, new MetaInfoContainer(metaInfo));
            document.Close();
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to content that
        /// will be written to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// , returning a
        /// <see cref="iText.Layout.Document"/>
        /// instance.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfWriter">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// containing the resulting PDF
        /// </param>
        /// <returns>
        /// a
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </returns>
        public static Document ConvertToDocument(String html, PdfWriter pdfWriter) {
            return ConvertToDocument(html, pdfWriter, null);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to content that
        /// will be written to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// , returning a
        /// <see cref="iText.Layout.Document"/>
        /// instance.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfWriter">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// containing the resulting PDF
        /// </param>
        /// <returns>
        /// a
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </returns>
        public static Document ConvertToDocument(Stream htmlStream, PdfWriter pdfWriter) {
            return ConvertToDocument(htmlStream, pdfWriter, null);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to content that
        /// will be written to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// , using specific
        /// <see cref="ConverterProperties"/>
        /// , returning a
        /// <see cref="iText.Layout.Document"/>
        /// instance.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfWriter">the pdf writer</param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>
        /// a
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </returns>
        public static Document ConvertToDocument(String html, PdfWriter pdfWriter, ConverterProperties converterProperties
            ) {
            return ConvertToDocument(html, new PdfDocument(pdfWriter), converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to content that
        /// will be written to a
        /// <see cref="iText.Kernel.Pdf.PdfWriter"/>
        /// , using specific
        /// <see cref="ConverterProperties"/>
        /// , returning a
        /// <see cref="iText.Layout.Document"/>
        /// instance.
        /// </summary>
        /// <param name="htmlStream">the html stream</param>
        /// <param name="pdfWriter">the pdf writer</param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>
        /// a
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </returns>
        public static Document ConvertToDocument(Stream htmlStream, PdfWriter pdfWriter, ConverterProperties converterProperties
            ) {
            return ConvertToDocument(htmlStream, new PdfDocument(pdfWriter), converterProperties);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to objects that
        /// will be added to a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// , using specific
        /// <see cref="ConverterProperties"/>
        /// ,
        /// returning a
        /// <see cref="iText.Layout.Document"/>
        /// instance.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// instance
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>
        /// a
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </returns>
        public static Document ConvertToDocument(String html, PdfDocument pdfDocument, ConverterProperties converterProperties
            ) {
            if (pdfDocument.GetReader() != null) {
                throw new Html2PdfException(Html2PdfException.PDF_DOCUMENT_SHOULD_BE_IN_WRITING_MODE);
            }
            converterProperties = SetupCustomConversionProperties(pdfDocument, converterProperties);
            IXmlParser parser = new JsoupHtmlParser();
            IDocumentNode doc = parser.Parse(html);
            return Attacher.Attach(doc, pdfDocument, converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to objects that
        /// will be added to a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// , using specific
        /// <see cref="ConverterProperties"/>
        /// ,
        /// returning a
        /// <see cref="iText.Layout.Document"/>
        /// instance.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// instance
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>
        /// a
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </returns>
        public static Document ConvertToDocument(Stream htmlStream, PdfDocument pdfDocument, ConverterProperties converterProperties
            ) {
            if (pdfDocument.GetReader() != null) {
                throw new Html2PdfException(Html2PdfException.PDF_DOCUMENT_SHOULD_BE_IN_WRITING_MODE);
            }
            converterProperties = SetupCustomConversionProperties(pdfDocument, converterProperties);
            IXmlParser parser = new JsoupHtmlParser();
            IDocumentNode doc = parser.Parse(htmlStream, converterProperties.GetCharset());
            return Attacher.Attach(doc, pdfDocument, converterProperties);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to a
        /// <see cref="System.Collections.IList{E}"/>
        /// of
        /// iText objects (
        /// <see cref="iText.Layout.Element.IElement"/>
        /// instances).
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <returns>a list of iText building blocks</returns>
        public static IList<IElement> ConvertToElements(String html) {
            return ConvertToElements(html, null);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to a
        /// <see cref="System.Collections.IList{E}"/>
        /// of
        /// iText objects (
        /// <see cref="iText.Layout.Element.IElement"/>
        /// instances).
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <returns>a list of iText building blocks</returns>
        public static IList<IElement> ConvertToElements(Stream htmlStream) {
            return ConvertToElements(htmlStream, null);
        }

        /// <summary>
        /// Converts a
        /// <see cref="System.String"/>
        /// containing HTML to a
        /// <see cref="System.Collections.IList{E}"/>
        /// of
        /// iText objects (
        /// <see cref="iText.Layout.Element.IElement"/>
        /// instances), using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="html">
        /// the html in the form of a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>a list of iText building blocks</returns>
        public static IList<IElement> ConvertToElements(String html, ConverterProperties converterProperties) {
            converterProperties = SetupCustomConversionProperties(null, converterProperties);
            IXmlParser parser = new JsoupHtmlParser();
            IDocumentNode doc = parser.Parse(html);
            return Attacher.Attach(doc, converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an
        /// <see cref="System.IO.Stream"/>
        /// to a
        /// <see cref="System.Collections.IList{E}"/>
        /// of
        /// iText objects (
        /// <see cref="iText.Layout.Element.IElement"/>
        /// instances), using specific
        /// <see cref="ConverterProperties"/>.
        /// </summary>
        /// <param name="htmlStream">
        /// the
        /// <see cref="System.IO.Stream"/>
        /// with the source HTML
        /// </param>
        /// <param name="converterProperties">
        /// a
        /// <see cref="ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>a list of iText building blocks</returns>
        public static IList<IElement> ConvertToElements(Stream htmlStream, ConverterProperties converterProperties
            ) {
            converterProperties = SetupCustomConversionProperties(null, converterProperties);
            IXmlParser parser = new JsoupHtmlParser();
            IDocumentNode doc = parser.Parse(htmlStream, converterProperties.GetCharset());
            return Attacher.Attach(doc, converterProperties);
        }

//\cond DO_NOT_DOCUMENT
        internal static IMetaInfo CreatePdf2HtmlMetaInfo() {
            return new HtmlConverter.HtmlMetaInfo();
        }
//\endcond

        private static PdfDocument CreateCorrectDocument(ConverterProperties converterProps, DocumentProperties documentProps
            , PdfWriter pdfWriter) {
            if (converterProps != null) {
                if (converterProps.GetPdfUaConformance() != null && converterProps.GetPdfAConformance() != null) {
                    throw new Html2PdfException(Html2PdfLogMessageConstant.PDF_A_AND_PDF_UA_CONFORMANCE_CANNOT_BE_USED_TOGETHER
                        );
                }
                if (converterProps.GetPdfAConformance() != null) {
                    return new PdfADocument(pdfWriter, converterProps.GetPdfAConformance(), converterProps.GetDocumentOutputIntent
                        (), documentProps);
                }
                if (converterProps.GetPdfUaConformance() != null) {
                    return new PdfUADocument(pdfWriter, documentProps, new PdfUAConfig(converterProps.GetPdfUaConformance(), "Title"
                        , "en"));
                }
            }
            return new PdfDocument(pdfWriter, documentProps);
        }

        private static IMetaInfo ResolveMetaInfo(ConverterProperties converterProperties) {
            return converterProperties == null ? CreatePdf2HtmlMetaInfo() : converterProperties.GetEventMetaInfo();
        }

        private static ConverterProperties SetupCustomConversionProperties(PdfDocument document, ConverterProperties
             properties) {
            if (properties == null) {
                properties = new ConverterProperties();
            }
            bool needsForcedEmbeddedFonts = document is PdfADocument || document is PdfUADocument || properties.GetPdfUaConformance
                () != null || properties.GetPdfAConformance() != null;
            if (needsForcedEmbeddedFonts && properties.GetFontProvider() == null) {
                BasicFontProvider embeddedFontProvider = new BasicFontProvider(false, true, false);
                properties.SetFontProvider(embeddedFontProvider);
            }
            if ((document is PdfUADocument || properties.GetPdfUaConformance() != null) && properties.GetOutlineHandler
                () == null) {
                properties.SetOutlineHandler(OutlineHandler.CreateStandardHandler());
            }
            if (properties.GetPdfAConformance() != null && "A".Equals(properties.GetPdfAConformance().GetLevel())) {
                document.SetTagged();
            }
            return properties;
        }

        private class HtmlMetaInfo : IMetaInfo {
        }
    }
}
