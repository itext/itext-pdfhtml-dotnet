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
using iText.Forms.Logs;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ParagraphTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ParagraphTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ParagraphTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphTest01.html"), new FileInfo(DESTINATION_FOLDER
                 + "paragraphTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphTest01.pdf", DESTINATION_FOLDER, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithBordersTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithBordersTest01.html"), new FileInfo(DESTINATION_FOLDER
                 + "paragraphWithBordersTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithBordersTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithBordersTest01.pdf", DESTINATION_FOLDER, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithMarginsTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithMarginsTest01.html"), new FileInfo(DESTINATION_FOLDER
                 + "paragraphWithMarginsTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithMarginsTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithMarginsTest01.pdf", DESTINATION_FOLDER, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithPaddingTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithPaddingTest01.html"), new FileInfo(DESTINATION_FOLDER
                 + "paragraphWithPaddingTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithPaddingTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithPaddingTest01.pdf", DESTINATION_FOLDER, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithFontAttributesTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithFontAttributesTest01.html"), new FileInfo
                (DESTINATION_FOLDER + "paragraphWithFontAttributesTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithFontAttributesTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithFontAttributesTest01.pdf", DESTINATION_FOLDER, "diff05_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithNonBreakableSpaceTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithNonBreakableSpaceTest01.html"), new 
                FileInfo(DESTINATION_FOLDER + "paragraphWithNonBreakableSpaceTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithNonBreakableSpaceTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithNonBreakableSpaceTest01.pdf", DESTINATION_FOLDER, "diff06_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithNonBreakableSpaceTest02() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithNonBreakableSpaceTest02.html"), new 
                FileInfo(DESTINATION_FOLDER + "paragraphWithNonBreakableSpaceTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithNonBreakableSpaceTest02.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithNonBreakableSpaceTest02.pdf", DESTINATION_FOLDER, "diff07_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithNonBreakableSpaceTest03() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithNonBreakableSpaceTest03.html"), new 
                FileInfo(DESTINATION_FOLDER + "paragraphWithNonBreakableSpaceTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithNonBreakableSpaceTest03.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithNonBreakableSpaceTest03.pdf", DESTINATION_FOLDER, "diff08_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphInTablePercentTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphInTablePercentTest01.html"), new FileInfo
                (DESTINATION_FOLDER + "paragraphInTablePercentTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphInTablePercentTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphInTablePercentTest01.pdf", DESTINATION_FOLDER, "diff09_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(FormsLogMessageConstants.DUPLICATE_EXPORT_VALUE, Count = 2)]
        public virtual void ParagraphWithButtonInputLabelSelectTextareaTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithButtonInputLabelSelectTextareaTest.html"
                ), new FileInfo(DESTINATION_FOLDER + "paragraphWithButtonInputLabelSelectTextareaTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithButtonInputLabelSelectTextareaTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithButtonInputLabelSelectTextareaTest.pdf", DESTINATION_FOLDER, "diff11_"
                ));
        }

        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void ParagraphWithBdoBrImgMapQSubSupTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithBdoBrImgMapQSubSupTest.html"), new FileInfo
                (DESTINATION_FOLDER + "paragraphWithBdoBrImgMapQSubSupTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithBdoBrImgMapQSubSupTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithBdoBrImgMapQSubSupTest.pdf", DESTINATION_FOLDER, "diff12_"));
        }

        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void ParagraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.html"
                ), new FileInfo(DESTINATION_FOLDER + "paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.pdf", DESTINATION_FOLDER, "diff13_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithAParagraphSpanDivTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithAParagraphSpanDivTest.html"), new FileInfo
                (DESTINATION_FOLDER + "paragraphWithAParagraphSpanDivTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithAParagraphSpanDivTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithAParagraphSpanDivTest.pdf", DESTINATION_FOLDER, "diff14_"));
        }

        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void ParagraphWithBBigISmallTtStrongTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithBBigISmallTtStrongTest.html"), new FileInfo
                (DESTINATION_FOLDER + "paragraphWithBBigISmallTtStrongTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithBBigISmallTtStrongTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithBBigISmallTtStrongTest.pdf", DESTINATION_FOLDER, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithPDisplayTableTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithPDisplayTableTest.html"), new FileInfo
                (DESTINATION_FOLDER + "paragraphWithPDisplayTableTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithPDisplayTableTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithPDisplayTableTest.pdf", DESTINATION_FOLDER, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithDifferentSpansTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithDifferentSpansTest.html"), new FileInfo
                (DESTINATION_FOLDER + "paragraphWithDifferentSpansTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithDifferentSpansTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithDifferentSpansTest.pdf", DESTINATION_FOLDER, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithDifferentBlocksAndDisplaysTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithDifferentBlocksAndDisplaysTest.html"
                ), new FileInfo(DESTINATION_FOLDER + "paragraphWithDifferentBlocksAndDisplaysTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithDifferentBlocksAndDisplaysTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithDifferentBlocksAndDisplaysTest.pdf", DESTINATION_FOLDER, "diff15_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithLabelSpanDisplayBlockTest() {
            //TODO: update after DEVSIX-2619 fix
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithLabelSpanDisplayBlockTest.html"), new 
                FileInfo(DESTINATION_FOLDER + "paragraphWithLabelSpanDisplayBlockTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithLabelSpanDisplayBlockTest.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithLabelSpanDisplayBlockTest.pdf", DESTINATION_FOLDER, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithImageTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithImageTest01.html"), new FileInfo(DESTINATION_FOLDER
                 + "paragraphWithImageTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithImageTest01.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithImageTest01.pdf", DESTINATION_FOLDER, "diff_paragraphWithImageTest01_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithImageTest01RTL() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "paragraphWithImageTest01RTL.html"), new FileInfo(
                DESTINATION_FOLDER + "paragraphWithImageTest01RTL.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "paragraphWithImageTest01RTL.pdf"
                , SOURCE_FOLDER + "cmp_paragraphWithImageTest01RTL.pdf", DESTINATION_FOLDER, "diff_paragraphWithImageTest01_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void HelloWorldParagraphTest() {
            ConvertToPdfAndCompare("hello_paragraph", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphTableTest() {
            ConvertToPdfAndCompare("hello_paragraph_table", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphJunkSpacesDocumentTest() {
            ConvertToPdfAndCompare("hello_paragraph_junk_spaces", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphNestedInTableDocumentTest() {
            ConvertToPdfAndCompare("hello_paragraph_nested_in_table", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphWithSpansDocumentTest() {
            ConvertToPdfAndCompare("hello_paragraph_with_span", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ABlockInPTagTest() {
            ConvertToPdfAndCompare("aBlockInPTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void NotAvailableImageDisplayBlockTest() {
            ConvertToPdfAndCompare("notAvailableImageDisplayBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
