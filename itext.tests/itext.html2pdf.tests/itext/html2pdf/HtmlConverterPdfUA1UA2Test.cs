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
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Logs;
using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Pdfua.Exceptions;
using iText.Pdfua.Logs;
using iText.Test;
using iText.Test.Attributes;
using iText.Test.Pdfa;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterPdfUA1UA2Test : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf" + "/HtmlConverterPdfUA1UA2Test/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterPdfUA1UA2Test/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        public static Object[] ConformanceLevels() {
            return new Object[] { PdfUAConformance.PDF_UA_1, PdfUAConformance.PDF_UA_2 };
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SimpleLinkTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "simpleLink.html";
            String cmpFile = SOURCE_FOLDER + "cmp_simpleLinkUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "simpleLinkUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void BackwardLinkTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "backwardLink.html";
            String cmpFile = SOURCE_FOLDER + "cmp_backwardLinkUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "backwardLinkUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ImageLinkTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "imageLink.html";
            String cmpFile = SOURCE_FOLDER + "cmp_imageLinkUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "imageLinkUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ExternalLinkTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "externalLink.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_externalLinkUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "externalLinkUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpPdf, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SimpleOutlineTest(PdfUAConformance conformance) {
            if (conformance == PdfUAConformance.PDF_UA_1) {
                String sourceHtmlUa1 = SOURCE_FOLDER + "simpleOutlineUa1.html";
                String cmpPdfUa1 = SOURCE_FOLDER + "cmp_simpleOutlineUa1.pdf";
                String destinationPdfUa1 = DESTINATION_FOLDER + "simpleOutlineUa1.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtmlUa1, destinationPdfUa1, cmpPdfUa1, null, true, null);
            }
            if (conformance == PdfUAConformance.PDF_UA_2) {
                String sourceHtmlUa2 = SOURCE_FOLDER + "simpleOutlineUa2.html";
                String cmpPdfUa2 = SOURCE_FOLDER + "cmp_simpleOutlineUa2.pdf";
                String destinationPdfUa2 = DESTINATION_FOLDER + "simpleOutlineUa2.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtmlUa2, destinationPdfUa2, cmpPdfUa2, null, true, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void UnsupportedGlyphTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "unsupportedGlyph.html";
            String expectedUaMessage = MessageFormatUtil.Format(PdfUAExceptionMessageConstants.GLYPH_IS_NOT_DEFINED_OR_WITHOUT_UNICODE
                , '中');
            if (conformance == PdfUAConformance.PDF_UA_1) {
                String cmpPdfUa1 = SOURCE_FOLDER + "cmp_unsupportedGlyphUa1.pdf";
                String destinationPdfUa1 = DESTINATION_FOLDER + "unsupportedGlyphUa1.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa1, cmpPdfUa1, null, false, expectedUaMessage
                    );
            }
            if (conformance == PdfUAConformance.PDF_UA_2) {
                String cmpPdfUa2 = SOURCE_FOLDER + "cmp_unsupportedGlyphUa2.pdf";
                String destinationPdfUa2 = DESTINATION_FOLDER + "unsupportedGlyphUa2.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa2, cmpPdfUa2, null, false, expectedUaMessage
                    );
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void EmptyElementsTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "emptyElements.html";
            String cmpFile = SOURCE_FOLDER + "cmp_emptyElementsUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "emptyElementsUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void BoxSizingInlineBlockTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "boxSizingInlineBlock.html";
            String cmpFile = SOURCE_FOLDER + "cmp_boxSizingInlineBlockUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "boxSizingInlineBlockUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void DivInButtonTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "divInButton.html";
            String cmpFile = SOURCE_FOLDER + "cmp_divInButtonUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "divInButtonUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void HeadingInButtonTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "headingInButton.html";
            String cmpFile = SOURCE_FOLDER + "cmp_headingInButtonUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "headingInButtonUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ParagraphsInHeadingsTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "paragraphsInHeadings.html";
            String cmpFile = SOURCE_FOLDER + "cmp_paragraphsInHeadingsUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "paragraphsInHeadingsUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void PageBreakAfterAvoidTest(PdfUAConformance conformance) {
            // TODO DEVSIX-8864 PDF 2.0: Destination in GoTo action is not a structure destination
            String sourceHtml = SOURCE_FOLDER + "pageBreakAfterAvoid.html";
            if (conformance == PdfUAConformance.PDF_UA_1) {
                String cmpPdfUa1 = SOURCE_FOLDER + "cmp_pageBreakAfterAvoidUa1.pdf";
                String destinationPdfUa1 = DESTINATION_FOLDER + "pageBreakAfterAvoidUa1.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa1, cmpPdfUa1, null, true, null);
            }
            if (conformance == PdfUAConformance.PDF_UA_2) {
                String cmpPdfUa2 = SOURCE_FOLDER + "cmp_pageBreakAfterAvoidUa2.pdf";
                String destinationPdfUa2 = DESTINATION_FOLDER + "pageBreakAfterAvoidUa2.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa2, cmpPdfUa2, null, false, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void LinkWithPageBreakBeforeTest(PdfUAConformance conformance) {
            // TODO DEVSIX-8864 PDF 2.0: Destination in GoTo action is not a structure destination
            String sourceHtml = SOURCE_FOLDER + "linkWithPageBreakBefore.html";
            if (conformance == PdfUAConformance.PDF_UA_1) {
                String cmpPdfUa1 = SOURCE_FOLDER + "cmp_linkWithPageBreakBeforeUa1.pdf";
                String destinationPdfUa1 = DESTINATION_FOLDER + "linkWithPageBreakBeforeUa1.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa1, cmpPdfUa1, null, true, null);
            }
            if (conformance == PdfUAConformance.PDF_UA_2) {
                String cmpPdfUa2 = SOURCE_FOLDER + "cmp_linkWithPageBreakBeforeUa2.pdf";
                String destinationPdfUa2 = DESTINATION_FOLDER + "linkWithPageBreakBeforeUa2.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa2, cmpPdfUa2, null, false, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void EmptyHtmlTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "emptyHtml.html";
            String cmpFile = SOURCE_FOLDER + "cmp_emptyHtmlUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "emptyHtmlUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void InputWithTitleTagTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "inputWithTitleTag.html";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetCreateAcroForm(true);
            if (conformance == PdfUAConformance.PDF_UA_1) {
                String cmpPdfUa1 = SOURCE_FOLDER + "cmp_inputWithTitleTagUa1.pdf";
                String destinationPdfUa1 = DESTINATION_FOLDER + "inputWithTitleTagUa1.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa1, cmpPdfUa1, converterProperties, 
                    true, null);
            }
            if (conformance == PdfUAConformance.PDF_UA_2) {
                String cmpPdfUa2 = SOURCE_FOLDER + "cmp_inputWithTitleTagUa2.pdf";
                String destinationPdfUa2 = DESTINATION_FOLDER + "inputWithTitleTagUa2.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa2, cmpPdfUa2, converterProperties, 
                    true, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SvgBase64Test(PdfUAConformance conformance) {
            // TODO DDEVSIX-9036 current VeraPdf version behaves incorrectly.
            String sourceHtml = SOURCE_FOLDER + "svgBase64.html";
            if (conformance == PdfUAConformance.PDF_UA_1) {
                String cmpPdfUa1 = SOURCE_FOLDER + "cmp_svgBase64Ua1.pdf";
                String destinationPdfUa1 = DESTINATION_FOLDER + "svgBase64Ua1.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa1, cmpPdfUa1, null, false, null);
            }
            if (conformance == PdfUAConformance.PDF_UA_2) {
                String cmpPdfUa2 = SOURCE_FOLDER + "cmp_svgBase64Ua2.pdf";
                String destinationPdfUa2 = DESTINATION_FOLDER + "svgBase64Ua2.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa2, cmpPdfUa2, null, false, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void PngInDivStyleTest(PdfUAConformance conformance) {
            // TODO DDEVSIX-9036 current VeraPdf version behaves incorrectly.
            // Investigate why VeraPdf doesn't complain about the missing tag.
            String sourceHtml = SOURCE_FOLDER + "pngInDivStyle.html";
            if (conformance == PdfUAConformance.PDF_UA_1) {
                String cmpPdfUa1 = SOURCE_FOLDER + "cmp_pngInDivStyleUa1.pdf";
                String destinationPdfUa1 = DESTINATION_FOLDER + "pngInDivStyleUa1.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa1, cmpPdfUa1, null, true, null);
            }
            if (conformance == PdfUAConformance.PDF_UA_2) {
                String cmpPdfUa2 = SOURCE_FOLDER + "cmp_pngInDivStyleUa2.pdf";
                String destinationPdfUa2 = DESTINATION_FOLDER + "pngInDivStyleUa2.pdf";
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdfUa2, cmpPdfUa2, null, true, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SvgAlternativeDescription(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "svgSimpleAlternateDescription.html";
            String cmpFile = SOURCE_FOLDER + "cmp_svgSimpleAlternateDescriptionUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "svgSimpleAlternateDescriptionUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        [LogMessage(PdfUALogMessageConstants.PAGE_FLUSHING_DISABLED, Count = 1)]
        public virtual void ExtensiveRepairTaggingStructRepairTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "tagStructureFixes.html";
            String cmpFile = SOURCE_FOLDER + "cmp_tagStructureFixesUA" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "tagStructureFixesUA" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void InputFieldsUA2Test(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "input.html";
            String cmpFile = SOURCE_FOLDER + "cmp_inputUA" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "inputUA" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        [LogMessage(PdfUALogMessageConstants.PAGE_FLUSHING_DISABLED, Count = 1)]
        public virtual void TableUa2Test(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "table.html";
            String cmpFile = SOURCE_FOLDER + "cmp_tableUA" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "tableUA" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ComplexParagraphStructure(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "complexParagraphStructure.html";
            String cmpFile = SOURCE_FOLDER + "cmp_complexParagraphStructureUA" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "complexParagraphStructureUA" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpFile, null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void EmptyTableDataCellTest(PdfUAConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "emptyTableDataCell.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_emptyTableDataCellUa" + conformance.GetPart() + ".pdf";
            String destinationPdf = DESTINATION_FOLDER + "emptyTableDataCellUa" + conformance.GetPart() + ".pdf";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, destinationPdf, cmpPdf, null, true, null);
        }

        [NUnit.Framework.Test]
        public virtual void DuplicateConformanceLevelAAndUAThrows() {
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfUAConformance(PdfUAConformance.PDF_UA_1);
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_4);
            PdfWriter dummy = new PdfWriter(new ByteArrayOutputStream());
            Exception e = NUnit.Framework.Assert.Catch(typeof(Html2PdfException), () => {
                HtmlConverter.ConvertToPdf("<h1>Let's gooooo</h1>", dummy, converterProperties);
            }
            );
            NUnit.Framework.Assert.AreEqual(Html2PdfLogMessageConstant.PDF_A_AND_PDF_UA_CONFORMANCE_CANNOT_BE_USED_TOGETHER
                , e.Message);
        }

        private void ConvertToUaAndCheckCompliance(PdfUAConformance conformance, String sourceHtml, String destinationPdf
            , String cmpPdf, ConverterProperties converterProperties, bool isExpectedOk, String expectedErrorMessage
            ) {
            if (converterProperties == null) {
                converterProperties = new ConverterProperties();
            }
            converterProperties.SetPdfUAConformance(conformance);
            converterProperties.SetBaseUri(SOURCE_FOLDER);
            WriterProperties writerProperties = new WriterProperties();
            if (conformance == PdfUAConformance.PDF_UA_2) {
                writerProperties.SetPdfVersion(PdfVersion.PDF_2_0);
            }
            FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read);
            using (PdfWriter pdfWriter = new PdfWriter(destinationPdf, writerProperties)) {
                if (expectedErrorMessage == null) {
                    HtmlConverter.ConvertToPdf(fileInputStream, pdfWriter, converterProperties);
                    CompareAndCheckCompliance(destinationPdf, cmpPdf, isExpectedOk);
                    return;
                }
                ConverterProperties finalConverterProperties = converterProperties;
                Exception e = NUnit.Framework.Assert.Catch(typeof(PdfUAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(fileInputStream, pdfWriter, finalConverterProperties);
                }
                );
                NUnit.Framework.Assert.AreEqual(expectedErrorMessage, e.Message);
            }
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
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareXmp(destinationPdf, cmpPdf, true));
        }
    }
}
