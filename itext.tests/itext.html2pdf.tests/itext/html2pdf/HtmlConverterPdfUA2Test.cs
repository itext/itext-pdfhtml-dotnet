/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using System.IO;
using iText.Html2pdf.Attach.Impl;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Kernel.XMP;
using iText.Layout.Font;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;
using iText.Test.Attributes;
using iText.Test.Pdfa;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterPdfUA2Test : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/HtmlConverterPdfUA2Test/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterPdfUA2Test/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleLinkTest() {
            String sourceHtml = SOURCE_FOLDER + "simpleLink.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simpleLink.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simpleLink.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion
                (PdfVersion.PDF_2_0)));
            CreateSimplePdfUA2Document(pdfDocument);
            ConverterProperties converterProperties = new ConverterProperties();
            FontProvider fontProvider = new BasicFontProvider(false, true, false);
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            CompareAndCheckCompliance(destinationPdf, cmpPdf, true);
        }

        [NUnit.Framework.Test]
        public virtual void BackwardLinkTest() {
            String sourceHtml = SOURCE_FOLDER + "backwardLink.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_backwardLink.pdf";
            String destinationPdf = DESTINATION_FOLDER + "backwardLink.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion
                (PdfVersion.PDF_2_0)));
            CreateSimplePdfUA2Document(pdfDocument);
            ConverterProperties converterProperties = new ConverterProperties();
            FontProvider fontProvider = new BasicFontProvider(false, true, false);
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            CompareAndCheckCompliance(destinationPdf, cmpPdf, true);
        }

        [NUnit.Framework.Test]
        public virtual void ImageLinkTest() {
            String sourceHtml = SOURCE_FOLDER + "imageLink.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_imageLink.pdf";
            String destinationPdf = DESTINATION_FOLDER + "imageLink.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion
                (PdfVersion.PDF_2_0)));
            CreateSimplePdfUA2Document(pdfDocument);
            ConverterProperties converterProperties = new ConverterProperties();
            FontProvider fontProvider = new BasicFontProvider(false, true, false);
            converterProperties.SetFontProvider(fontProvider);
            converterProperties.SetBaseUri(SOURCE_FOLDER);
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            CompareAndCheckCompliance(destinationPdf, cmpPdf, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleOutlineTest() {
            // TODO DEVSIX-8476 PDF 2.0 doesn't allow P tag be a child of H tag
            String sourceHtml = SOURCE_FOLDER + "simpleOutline.html";
            String destinationPdf = DESTINATION_FOLDER + "simpleOutline.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_simpleOutline.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion
                (PdfVersion.PDF_2_0)));
            CreateSimplePdfUA2Document(pdfDocument);
            ConverterProperties converterProperties = new ConverterProperties();
            FontProvider fontProvider = new BasicFontProvider(false, true, false);
            converterProperties.SetFontProvider(fontProvider);
            converterProperties.SetOutlineHandler(OutlineHandler.CreateStandardHandler());
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            CompareAndCheckCompliance(destinationPdf, cmpPdf, false);
        }

        [NUnit.Framework.Test]
        // TODO DEVSIX-8707 Handle html2pdf pdfua conversion handle missing glyphs
        // TODO DEVSIX-8706 Incorrect tagging structure when using one span with glyph that doesn't have a mapping in the font
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ATTEMPT_TO_CREATE_A_TAG_FOR_FINISHED_HINT)]
        public virtual void UnsupportedGlyphTest() {
            String sourceHtml = SOURCE_FOLDER + "unsupportedGlyph.html";
            String destinationPdf = DESTINATION_FOLDER + "unsupportedGlyph.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_unsupportedGlyph.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion
                (PdfVersion.PDF_2_0)));
            CreateSimplePdfUA2Document(pdfDocument);
            ConverterProperties converterProperties = new ConverterProperties();
            FontProvider fontProvider = new BasicFontProvider(false, true, false);
            converterProperties.SetFontProvider(fontProvider);
            converterProperties.SetOutlineHandler(OutlineHandler.CreateStandardHandler());
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            CompareAndCheckCompliance(destinationPdf, cmpPdf, false);
        }

        private void CreateSimplePdfUA2Document(PdfDocument pdfDocument) {
            byte[] bytes = File.ReadAllBytes(System.IO.Path.Combine(SOURCE_FOLDER + "simplePdfUA2.xmp"));
            XMPMeta xmpMeta = XMPMetaFactory.Parse(new MemoryStream(bytes));
            pdfDocument.SetXmpMetadata(xmpMeta);
            pdfDocument.SetTagged();
            pdfDocument.GetCatalog().SetViewerPreferences(new PdfViewerPreferences().SetDisplayDocTitle(true));
            pdfDocument.GetCatalog().SetLang(new PdfString("en-US"));
            PdfDocumentInfo info = pdfDocument.GetDocumentInfo();
            info.SetTitle("PdfUA2 Title");
        }

        private static void CompareAndCheckCompliance(String destinationPdf, String cmpPdf, bool isExpectedOk) {
            if (isExpectedOk) {
                NUnit.Framework.Assert.IsNull(new VeraPdfValidator().Validate(destinationPdf));
            }
            else {
                new VeraPdfValidator().ValidateFailure(destinationPdf);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, DESTINATION_FOLDER
                , "diff_simple_"));
        }
    }
}
