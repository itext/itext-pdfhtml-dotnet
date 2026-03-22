/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Commons.Utils;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Logs;
using iText.IO.Source;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Pdfua.Exceptions;
using iText.Pdfua.Logs;
using iText.Test;
using iText.Test.Attributes;
using iText.Test.Pdfa;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterWithConformanceTest : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf" + "/HtmlConverterWithConformanceTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterWithConformanceTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        public static IList<PdfConformance> ConformanceLevels() {
            return JavaUtil.ArraysAsList(PdfConformance.PDF_UA_1, PdfConformance.PDF_UA_2, PdfConformance.WELL_TAGGED_PDF_FOR_ACCESSIBILITY
                , PdfConformance.WELL_TAGGED_PDF_FOR_REUSE);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SimpleLinkTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "simpleLink.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "simpleLink", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void BackwardLinkTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "backwardLink.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "backwardLink", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void LongLinkBrokenAcrossPagesTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "longLinkBrokenAcrossPages.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "longLinkBrokenAcrossPages", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ImageLinkTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "imageLink.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "imageLink", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ExternalLinkTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "externalLink.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "externalLink", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SimpleOutlineTest(PdfConformance conformance) {
            if (conformance == PdfConformance.PDF_UA_1) {
                String sourceHtmlUa1 = SOURCE_FOLDER + "simpleOutlineUa1.html";
                ConvertToUaAndCheckCompliance(conformance, sourceHtmlUa1, "simpleOutline", null, true, null);
            }
            else {
                String sourceHtmlUa2 = SOURCE_FOLDER + "simpleOutlineUa2.html";
                ConvertToUaAndCheckCompliance(conformance, sourceHtmlUa2, "simpleOutline", null, true, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void UnsupportedGlyphTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "unsupportedGlyph.html";
            String expectedUaMessage = MessageFormatUtil.Format(PdfUAExceptionMessageConstants.GLYPH_IS_NOT_DEFINED_OR_WITHOUT_UNICODE
                , '中');
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "unsupportedGlyph", null, false, expectedUaMessage);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void EmptyElementsTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "emptyElements.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "emptyElements", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void BoxSizingInlineBlockTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "boxSizingInlineBlock.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "boxSizingInlineBlock", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void DivInButtonTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "divInButton.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "divInButton", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void HeadingInButtonTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "headingInButton.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "headingInButton", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ParagraphsInHeadingsTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "paragraphsInHeadings.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "paragraphsInHeadings", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void PageBreakAfterAvoidTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "pageBreakAfterAvoid.html";
            if (conformance == PdfConformance.PDF_UA_1) {
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, "pageBreakAfterAvoid", null, true, null);
            }
            else {
                // Both structure destination and page destination are not created, because content is not rendered.
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, "pageBreakAfterAvoid", null, false, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.NAME_ALREADY_EXISTS_IN_THE_NAME_TREE)]
        public virtual void LinkWithPageBreakBeforeTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "linkWithPageBreakBefore.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "linkWithPageBreakBefore", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void EmptyHtmlTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "emptyHtml.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "emptyHtml", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void InputWithTitleTagTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "inputWithTitleTag.html";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetCreateAcroForm(true);
            if (conformance == PdfConformance.PDF_UA_1) {
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, "inputWithTitleTag", converterProperties, true, null
                    );
            }
            else {
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, "inputWithTitleTag", converterProperties, true, null
                    );
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SvgBase64Test(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "svgBase64.html";
            if (conformance == PdfConformance.PDF_UA_1) {
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, "svgBase64", null, true, null);
            }
            else {
                ConvertToUaAndCheckCompliance(conformance, sourceHtml, "svgBase64", null, true, null);
            }
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void PngInDivStyleTest(PdfConformance conformance) {
            // TODO DEVSIX-9580 current VeraPdf version behaves incorrectly.
            // Investigate why VeraPdf doesn't complain about the missing tag.
            String sourceHtml = SOURCE_FOLDER + "pngInDivStyle.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "pngInDivStyle", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void SvgAlternativeDescription(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "svgSimpleAlternateDescription.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "svgSimpleAlternateDescription", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        [LogMessage(PdfUALogMessageConstants.PAGE_FLUSHING_DISABLED)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.NAME_ALREADY_EXISTS_IN_THE_NAME_TREE, Count = 12)]
        public virtual void ExtensiveRepairTaggingStructRepairTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "tagStructureFixes.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "tagStructureFixes", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void InputFieldsUA2Test(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "input.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "input", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void FlexTagsUA2Test(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "flexTagsUA2.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "flexTags", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        [LogMessage(PdfUALogMessageConstants.PAGE_FLUSHING_DISABLED, Count = 1)]
        public virtual void TableUa2Test(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "table.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "table", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ComplexParagraphStructure(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "complexParagraphStructure.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "complexParagraphStructure", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void EmptyTableDataCellTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "emptyTableDataCell.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "emptyTableDataCell", null, true, null);
        }

        [NUnit.Framework.TestCaseSource("ConformanceLevels")]
        public virtual void ZeroFontSizeTest(PdfConformance conformance) {
            String sourceHtml = SOURCE_FOLDER + "zeroFontSize.html";
            ConvertToUaAndCheckCompliance(conformance, sourceHtml, "zeroFontSize", null, true, null);
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

        [NUnit.Framework.Test]
        public virtual void DuplicateConformanceLevelWtpdfandUAThrows() {
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfUAConformance(PdfUAConformance.PDF_UA_1);
            converterProperties.SetWtPdfConformance(WellTaggedPdfConformance.FOR_REUSE);
            PdfWriter dummy = new PdfWriter(new ByteArrayOutputStream());
            Exception e = NUnit.Framework.Assert.Catch(typeof(Html2PdfException), () => {
                HtmlConverter.ConvertToPdf("<h1>Let's gooooo</h1>", dummy, converterProperties);
            }
            );
            NUnit.Framework.Assert.AreEqual(Html2PdfLogMessageConstant.PDF_A_AND_PDF_UA_CONFORMANCE_CANNOT_BE_USED_TOGETHER
                , e.Message);
        }

        private void ConvertToUaAndCheckCompliance(PdfConformance conformance, String sourceHtml, String fileName, 
            ConverterProperties converterProperties, bool isExpectedOk, String expectedErrorMessage) {
            if (converterProperties == null) {
                converterProperties = new ConverterProperties();
            }
            if (conformance.IsWtpdf()) {
                converterProperties.SetWtPdfConformance(conformance.GetWtpdfConformances()[0]);
            }
            else {
                converterProperties.SetPdfUAConformance(conformance.GetUAConformance());
            }
            converterProperties.SetBaseUri(SOURCE_FOLDER);
            WriterProperties writerProperties = new WriterProperties();
            if (conformance.ConformsTo(PdfConformance.PDF_UA_2, PdfConformance.WELL_TAGGED_PDF_FOR_ACCESSIBILITY, PdfConformance
                .WELL_TAGGED_PDF_FOR_REUSE)) {
                writerProperties.SetPdfVersion(PdfVersion.PDF_2_0);
            }
            String destinationPdf = DESTINATION_FOLDER + fileName + ".pdf";
            String postFix = "";
            if (conformance.IsWtpdf()) {
                postFix = "_wtpdf_" + conformance.GetWtpdfConformances()[0].ToString();
            }
            else {
                postFix = "Ua" + conformance.GetUAConformance().GetPart();
            }
            String cmpPdf = SOURCE_FOLDER + "cmp_" + fileName + postFix + ".pdf";
            FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read);
            using (PdfWriter pdfWriter = new PdfWriter(destinationPdf, writerProperties)) {
                if (expectedErrorMessage == null) {
                    HtmlConverter.ConvertToPdf(fileInputStream, pdfWriter, converterProperties);
                    CompareAndCheckCompliance(destinationPdf, cmpPdf, isExpectedOk);
                    return;
                }
                ConverterProperties finalConverterProperties = converterProperties;
                Exception e = NUnit.Framework.Assert.Catch(typeof(PdfException), () => {
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
