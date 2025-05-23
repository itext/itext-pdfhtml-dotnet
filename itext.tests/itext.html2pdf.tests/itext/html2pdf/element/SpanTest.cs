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
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class SpanTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/SpanTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/SpanTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        private void TestWithSuffix(String testIndex) {
            String htmlFile = sourceFolder + MessageFormatUtil.Format("spanTest{0}.html", testIndex);
            String pdfFile = destinationFolder + MessageFormatUtil.Format("spanTest{0}.pdf", testIndex);
            String cmpFile = sourceFolder + MessageFormatUtil.Format("cmp_spanTest{0}.pdf", testIndex);
            String diff = MessageFormatUtil.Format("diff{0}_", testIndex);
            HtmlConverter.ConvertToPdf(new FileInfo(htmlFile), new FileInfo(pdfFile));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfFile, cmpFile, destinationFolder, diff
                ));
        }

        private void Test(String testName, bool tagged) {
            String htmlFile = sourceFolder + testName + ".html";
            String pdfFile = destinationFolder + testName + ".pdf";
            String cmpFile = sourceFolder + MessageFormatUtil.Format("cmp_{0}.pdf", testName);
            String diff = MessageFormatUtil.Format("diff_{0}_", testName);
            if (tagged) {
                PdfDocument pdfDocument = new PdfDocument(new PdfWriter(pdfFile));
                pdfDocument.SetTagged();
                using (FileStream fileInputStream = new FileStream(htmlFile, FileMode.Open, FileAccess.Read)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument);
                }
            }
            else {
                HtmlConverter.ConvertToPdf(new FileInfo(htmlFile), new FileInfo(pdfFile));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfFile, cmpFile, destinationFolder, diff
                ));
        }

        private void Test(String testName) {
            Test(testName, false);
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest01() {
            TestWithSuffix("01");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest02() {
            TestWithSuffix("02");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest03() {
            TestWithSuffix("03");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest04() {
            TestWithSuffix("04");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest05() {
            TestWithSuffix("05");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest06() {
            TestWithSuffix("06");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest07() {
            //TODO DEVSIX-2485: Margins-applying currently doesn't work in html way for spans inside other spans.
            TestWithSuffix("07");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest08() {
            TestWithSuffix("08");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest09() {
            TestWithSuffix("09");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest10() {
            TestWithSuffix("10");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest11() {
            TestWithSuffix("11");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest12() {
            TestWithSuffix("12");
        }

        [NUnit.Framework.Test]
        public virtual void SpanTest13() {
            TestWithSuffix("13");
        }

        [NUnit.Framework.Test]
        public virtual void SpanInsideSpanWithBackgroundTest() {
            Test("spanInsideSpanWithBackground");
        }

        [NUnit.Framework.Test]
        public virtual void SpanWithLeftFloatInsideSpanWithBackgroundTest() {
            Test("spanWithLeftFloatInsideSpanWithBackground");
        }

        [NUnit.Framework.Test]
        public virtual void SpanWithFloatsInsideSpanWithBackgroundAndFloatTest() {
            Test("spanWithFloatsInsideSpanWithBackgroundAndFloat");
        }

        [NUnit.Framework.Test]
        public virtual void CommonNestedSpanTest() {
            Test("commonNestedSpanTest");
        }

        // TODO: update cmp files during DEVSIX-2510
        [NUnit.Framework.Test]
        public virtual void SpanTestNestedBlock() {
            Test("spanTestNestedBlock");
        }

        // TODO: update cmp files during DEVSIX-2510
        [NUnit.Framework.Test]
        public virtual void SpanTestNestedInlineBlock() {
            Test("spanTestNestedInlineBlock");
        }

        [NUnit.Framework.Test]
        public virtual void SpanWithDisplayBlockInsideSpanParagraphTest() {
            Test("spanWithDisplayBlockInsideSpanParagraphTest", true);
        }

        [NUnit.Framework.Test]
        public virtual void SpanWithBackgroundImageTest() {
            Test("spanWithBackgroundImageTest");
        }

        [NUnit.Framework.Test]
        public virtual void SpanBorderDottedInsideSolidSpanTest() {
            //TODO DEVSIX-2485: Border-applying currently doesn't work in html way for spans inside other spans.
            Test("spanBorderDottedInsideSolidSpan");
        }

        [NUnit.Framework.Test]
        public virtual void SpanBorderNoneInsideDoubleSpanTest() {
            //TODO DEVSIX-2485: Border-applying currently doesn't work in html way for spans inside other spans.
            Test("spanBorderNoneInsideDoubleSpan");
        }

        [NUnit.Framework.Test]
        public virtual void SpanBorderMixedInsideSolidSpanTest() {
            //TODO DEVSIX-2485: Border-applying currently doesn't work in html way for spans inside other spans.
            Test("spanBorderMixedInsideSolidSpan");
        }

        [NUnit.Framework.Test]
        public virtual void SpanMarginRightInsideSpanTest() {
            //TODO DEVSIX-2485: Margins-applying currently doesn't work in html way for spans inside other spans.
            Test("spanMarginRightInsideSpan");
        }

        [NUnit.Framework.Test]
        public virtual void SpanMarginLeftInsideSpanTest() {
            //TODO DEVSIX-2485: Margins-applying currently doesn't work in html way for spans inside other spans.
            Test("spanMarginLeftInsideSpanTest");
        }

        [NUnit.Framework.Test]
        public virtual void SpanMarginLeftInsideRightSpanTest() {
            //TODO DEVSIX-2485: Margins-applying currently doesn't work in html way for spans inside other spans.
            Test("spanMarginLeftInsideRightSpan");
        }
    }
}
