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
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class OverflowTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/OverflowTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/OverflowTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivOverflowXOverflowYHiddenTest() {
            ConvertToPdfAndCompare("divOverflowXOverflowYHidden", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphOverflowVisibleTest() {
            ConvertToPdfAndCompare("paragraphOverflowVisible", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivOverflowHiddenParagraphOverflowVisibleTest() {
            ConvertToPdfAndCompare("divOverflowHiddenParagraphOverflowVisible", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivOverflowXOverflowYVisibleTest() {
            ConvertToPdfAndCompare("divOverflowXOverflowYVisible", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableOverflowXOverflowYVisibleTest() {
            ConvertToPdfAndCompare("tableOverflowXOverflowYVisible", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphOverflowXOverflowYVisibleTest() {
            //TODO DEVSIX-5208 Overflown content should be placed over the content of backgrounds and borders of other elements
            ConvertToPdfAndCompare("paragraphOverflowXOverflowYVisible", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UlOverflowXOverflowYVisibleTest() {
            //TODO DEVSIX-5208 Overflown content should be placed over the content of backgrounds and borders of other elements
            ConvertToPdfAndCompare("ulOverflowXOverflowYVisible", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableOverflowHiddenTest() {
            ConvertToPdfAndCompare("tableOverflowHidden", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivOverflowScrollTest() {
            ConvertToPdfAndCompare("divOverflowScroll", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivOverflowAutoTest() {
            ConvertToPdfAndCompare("divOverflowAuto", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowVisibleContentShouldBeSplitBetweenPagesTest() {
            //TODO DEVSIX-5211 Overflown due to 'overflow: visible' content should be wrapped to the next page
            ConvertToPdfAndCompare("overflowVisibleContentShouldBeSplitBetweenPages", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void OverflowAndAlignment01() {
            ConvertToPdfAndCompare("overflowAndAlignment01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowAndAlignment02() {
            ConvertToPdfAndCompare("overflowAndAlignment02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayOverflowAutoScroll() {
            ConvertToPdfAndCompare("displayOverflowAutoScroll", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowYVisibleOverflowXAllValuesTest() {
            //TODO DEVSIX-5212 CSS parsing: implement correct handling of css tokens with escaped code point
            RunTest("overflowYVisibleOverflowXAllValues", new PageSize(1200, 1400));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowYHiddenOverflowXAllValuesTest() {
            //TODO DEVSIX-5212 CSS parsing: implement correct handling of css tokens with escaped code point
            RunTest("overflowYHiddenOverflowXAllValues", new PageSize(1200, 1400));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowYScrollOverflowXAllValuesTest() {
            //TODO DEVSIX-5212 CSS parsing: implement correct handling of css tokens with escaped code point
            RunTest("overflowYScrollOverflowXAllValues", new PageSize(1200, 1400));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowYAutoOverflowXAllValues() {
            //TODO DEVSIX-5212 CSS parsing: implement correct handling of css tokens with escaped code point
            RunTest("overflowYAutoOverflowXAllValues", new PageSize(1200, 1400));
        }

        private void RunTest(String testName, PageSize pageSize) {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + testName + ".pdf"));
            if (null != pageSize) {
                pdfDocument.SetDefaultPageSize(pageSize);
            }
            HtmlConverter.ConvertToPdf(new FileStream(SOURCE_FOLDER + testName + ".html", FileMode.Open, FileAccess.Read
                ), pdfDocument, new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(SOURCE_FOLDER + testName + ".html"
                ) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + testName + ".pdf", SOURCE_FOLDER
                 + "cmp_" + testName + ".pdf", DESTINATION_FOLDER, "diff_" + testName));
        }
    }
}
