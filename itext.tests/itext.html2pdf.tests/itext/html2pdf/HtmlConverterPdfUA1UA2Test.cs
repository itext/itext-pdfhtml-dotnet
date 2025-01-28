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
using System.IO;
using iText.Commons.Utils;
using iText.Html2pdf.Attach.Impl;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Kernel.XMP;
using iText.Layout.Font;
using iText.Pdfua;
using iText.Pdfua.Exceptions;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;
using iText.Test.Attributes;
using iText.Test.Pdfa;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterPdfUA1UA2Test : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/HtmlConverterPdfUA1UA2Test/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterPdfUA1UA2Test/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleLinkTest() {
            // TODO DEVSIX-8679 Apply links alternate description according to UA standard
            String sourceHtml = SOURCE_FOLDER + "simpleLink.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_simpleLinkUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_simpleLinkUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "simpleLink.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "simpleLink.pdf";
            String expectedUa1Message = MessageFormatUtil.Format(PdfUAExceptionMessageConstants.ANNOTATION_OF_TYPE_0_SHOULD_HAVE_CONTENTS_OR_ALT_KEY
                , PdfName.Link.GetValue());
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, false, expectedUa1Message);
            // Expected valid UA-2 document because PDF/UA-2 does not require Contents in Link annotations
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, true);
        }

        [NUnit.Framework.Test]
        public virtual void BackwardLinkTest() {
            // TODO DEVSIX-8679 Apply links alternate description according to UA standard
            String sourceHtml = SOURCE_FOLDER + "backwardLink.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_backwardLinkUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_backwardLinkUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "backwardLinkUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "backwardLinkUa2.pdf";
            String expectedUa1Message = MessageFormatUtil.Format(PdfUAExceptionMessageConstants.ANNOTATION_OF_TYPE_0_SHOULD_HAVE_CONTENTS_OR_ALT_KEY
                , PdfName.Link.GetValue());
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, false, expectedUa1Message);
            // Expected valid UA-2 document because PDF/UA-2 does not require Contents in Link annotations
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, true);
        }

        [NUnit.Framework.Test]
        public virtual void ImageLinkTest() {
            // TODO DEVSIX-8679 Apply links alternate description according to UA standard
            String sourceHtml = SOURCE_FOLDER + "imageLink.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_imageLinkUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_imageLinkUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "imageLinkUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "imageLinkUa2.pdf";
            String expectedUa1Message = MessageFormatUtil.Format(PdfUAExceptionMessageConstants.ANNOTATION_OF_TYPE_0_SHOULD_HAVE_CONTENTS_OR_ALT_KEY
                , PdfName.Link.GetValue());
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, false, expectedUa1Message);
            // Expected valid UA-2 document because PDF/UA-2 does not require Contents in Link annotations
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleOutlineTest() {
            // TODO DEVSIX-8476 PDF 2.0 doesn't allow P tag be a child of H tag
            String sourceHtmlUa1 = SOURCE_FOLDER + "simpleOutlineUa1.html";
            String sourceHtmlUa2 = SOURCE_FOLDER + "simpleOutlineUa2.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_simpleOutlineUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_simpleOutlineUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "simpleOutlineUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "simpleOutlineUa2.pdf";
            ConvertToUa1AndCheckCompliance(sourceHtmlUa1, destinationPdfUa1, cmpPdfUa1, true, null);
            ConvertToUa2AndCheckCompliance(sourceHtmlUa2, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        // TODO DEVSIX-8706 Incorrect tagging structure when using one span with glyph that doesn't have a mapping in the font
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ATTEMPT_TO_CREATE_A_TAG_FOR_FINISHED_HINT)]
        public virtual void UnsupportedGlyphTest() {
            String sourceHtml = SOURCE_FOLDER + "unsupportedGlyph.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_unsupportedGlyphUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_unsupportedGlyphUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "unsupportedGlyphUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "unsupportedGlyphUa2.pdf";
            String expectedUa1Message = MessageFormatUtil.Format(PdfUAExceptionMessageConstants.GLYPH_IS_NOT_DEFINED_OR_WITHOUT_UNICODE
                , '中');
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, false, expectedUa1Message);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyElementsTest() {
            // TODO DEVSIX-8862 PDF 2.0 does not allow DIV, P tags to be children of the P tag
            String sourceHtml = SOURCE_FOLDER + "emptyElements.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_emptyElementsUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_emptyElementsUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "emptyElementsUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "emptyElementsUa2.pdf";
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, true, null);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingInlineBlockTest() {
            // TODO DEVSIX-8862 PDF 2.0 does not allow DIV, P tags to be children of the P tag
            String sourceHtml = SOURCE_FOLDER + "boxSizingInlineBlock.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_boxSizingInlineBlockUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_boxSizingInlineBlockUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "boxSizingInlineBlockUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "boxSizingInlineBlockUa2.pdf";
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, true, null);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        public virtual void DivInButtonTest() {
            // TODO DEVSIX-8863 PDF 2.0 does not allow P, Hn tags to be children of the Form tag
            String sourceHtml = SOURCE_FOLDER + "divInButton.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_divInButtonUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_divInButtonUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "divInButtonUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "divInButtonUa2.pdf";
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, true, null);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        public virtual void HeadingInButtonTest() {
            // TODO DEVSIX-8863 PDF 2.0 does not allow P, Hn tags to be children of the Form tag
            // TODO DEVSIX-8476 PDF 2.0 doesn't allow P tag be a child of H tag
            String sourceHtml = SOURCE_FOLDER + "headingInButton.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_headingInButtonUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_headingInButtonUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "headingInButtonUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "headingInButtonUa2.pdf";
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, true, null);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAfterAvoidTest() {
            // TODO DEVSIX-8864 PDF 2.0: Destination in GoTo action is not a structure destination
            String sourceHtml = SOURCE_FOLDER + "pageBreakAfterAvoid.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_pageBreakAfterAvoidUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_pageBreakAfterAvoidUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "pageBreakAfterAvoidUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "pageBreakAfterAvoidUa2.pdf";
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, true, null);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        public virtual void LinkWithPageBreakBeforeTest() {
            // TODO DEVSIX-8864 PDF 2.0: Destination in GoTo action is not a structure destination
            // TODO DEVSIX-8476 PDF 2.0 doesn't allow P tag be a child of H tag
            // TODO DEVSIX-8679 Apply links alternate description according to UA standard
            String sourceHtml = SOURCE_FOLDER + "linkWithPageBreakBefore.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_linkWithPageBreakBeforeUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_linkWithPageBreakBeforeUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "linkWithPageBreakBeforeUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "linkWithPageBreakBeforeUa2.pdf";
            String expectedUa1Message = MessageFormatUtil.Format(PdfUAExceptionMessageConstants.ANNOTATION_OF_TYPE_0_SHOULD_HAVE_CONTENTS_OR_ALT_KEY
                , PdfName.Link.GetValue());
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, false, expectedUa1Message);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyHtmlTest() {
            // TODO DEVSIX-8865 PDF document does not contain Document tag if it does not contain any content
            String sourceHtml = SOURCE_FOLDER + "emptyHtml.html";
            String cmpPdfUa1 = SOURCE_FOLDER + "cmp_emptyHtmlUa1.pdf";
            String cmpPdfUa2 = SOURCE_FOLDER + "cmp_emptyHtmlUa2.pdf";
            String destinationPdfUa1 = DESTINATION_FOLDER + "emptyHtmlUa1.pdf";
            String destinationPdfUa2 = DESTINATION_FOLDER + "emptyHtmlUa2.pdf";
            ConvertToUa1AndCheckCompliance(sourceHtml, destinationPdfUa1, cmpPdfUa1, true, null);
            ConvertToUa2AndCheckCompliance(sourceHtml, destinationPdfUa2, cmpPdfUa2, false);
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

        private void ConvertToUa1AndCheckCompliance(String sourceHtml, String destinationPdf, String cmpPdf, bool 
            isExpectedOk, String expectedErrorMessage) {
            PdfDocument pdfDocument = new PdfUADocument(new PdfWriter(destinationPdf), new PdfUAConfig(PdfUAConformance
                .PDF_UA_1, "simple doc", "eng"));
            ConverterProperties converterProperties = new ConverterProperties();
            FontProvider fontProvider = new BasicFontProvider(false, true, false);
            converterProperties.SetFontProvider(fontProvider);
            converterProperties.SetBaseUri(SOURCE_FOLDER);
            converterProperties.SetOutlineHandler(OutlineHandler.CreateStandardHandler());
            if (expectedErrorMessage != null) {
                Exception e = NUnit.Framework.Assert.Catch(typeof(PdfUAConformanceException), () => HtmlConverter.ConvertToPdf
                    (new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties));
                NUnit.Framework.Assert.AreEqual(expectedErrorMessage, e.Message);
            }
            else {
                HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                    );
                CompareAndCheckCompliance(destinationPdf, cmpPdf, isExpectedOk);
            }
        }

        private void ConvertToUa2AndCheckCompliance(String sourceHtml, String destinationPdf, String cmpPdf, bool 
            isExpectedOk) {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion
                (PdfVersion.PDF_2_0)));
            CreateSimplePdfUA2Document(pdfDocument);
            ConverterProperties converterProperties = new ConverterProperties();
            FontProvider fontProvider = new BasicFontProvider(false, true, false);
            converterProperties.SetFontProvider(fontProvider);
            converterProperties.SetBaseUri(SOURCE_FOLDER);
            converterProperties.SetOutlineHandler(OutlineHandler.CreateStandardHandler());
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            CompareAndCheckCompliance(destinationPdf, cmpPdf, isExpectedOk);
        }
    }
}
