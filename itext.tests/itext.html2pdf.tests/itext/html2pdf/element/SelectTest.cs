/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Layout.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class SelectTest : ExtendedITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/SelectTest/";

        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/SelectTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest01() {
            RunTest("selectBasicTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest02() {
            RunTest("selectBasicTest02");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest03() {
            RunTest("selectBasicTest03");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest04() {
            RunTest("selectBasicTest04");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest05() {
            RunTest("selectBasicTest05");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest06() {
            RunTest("selectBasicTest06");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest07() {
            RunTest("selectBasicTest07");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest08() {
            RunTest("selectBasicTest08");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest09() {
            RunTest("selectBasicTest09");
        }

        [NUnit.Framework.Test]
        public virtual void SelectBasicTest10() {
            RunTest("selectBasicTest10");
        }

        [NUnit.Framework.Test]
        public virtual void SelectEmptyTest01() {
            RunTest("selectEmptyTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectMultipleSizeTest01() {
            RunTest("selectMultipleSizeTest01");
        }

        [NUnit.Framework.Test]
        [LogMessage(FormsLogMessageConstants.DUPLICATE_EXPORT_VALUE, Count = 2)]
        public virtual void SelectPlaceholderTest01() {
            RunTest("selectPlaceholderTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectStylesTest01() {
            RunTest("selectStylesTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectStylesTest02() {
            RunTest("selectStylesTest02");
        }

        [NUnit.Framework.Test]
        public virtual void SelectWidthTest01() {
            RunTest("selectWidthTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectWidthTest02() {
            RunTest("selectWidthTest02");
        }

        [NUnit.Framework.Test]
        public virtual void SelectWidthTest03() {
            RunTest("selectWidthTest03");
        }

        [NUnit.Framework.Test]
        public virtual void SelectWidthTest04() {
            RunTest("selectWidthTest04");
        }

        [NUnit.Framework.Test]
        public virtual void SelectWidthTest05() {
            RunTest("selectWidthTest05");
        }

        [NUnit.Framework.Test]
        public virtual void SelectRelativeWidthTest01() {
            RunTest("selectRelativeWidthTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectRelativeWidthTest02() {
            RunTest("selectRelativeWidthTest02");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void SelectMinMaxWidthCalculationTest01() {
            RunTest("selectMinMaxWidthCalculationTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectHeightTest01() {
            RunTest("selectHeightTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectOverflowTest01() {
            RunTest("selectOverflowTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectOverflowTest02() {
            RunTest("selectOverflowTest02");
        }

        [NUnit.Framework.Test]
        public virtual void SelectOverflowTest03() {
            RunTest("selectOverflowTest03");
        }

        [NUnit.Framework.Test]
        public virtual void SelectOverflowTest04() {
            RunTest("selectOverflowTest04");
        }

        [NUnit.Framework.Test]
        public virtual void SelectOverflowTest05() {
            RunTest("selectOverflowTest05");
        }

        [NUnit.Framework.Test]
        public virtual void SelectOverflowTest06() {
            RunTest("selectOverflowTest06");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest01() {
            RunTest("selectSizeBasedHeightTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest02() {
            RunTest("selectSizeBasedHeightTest02");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest03() {
            // TODO DEVSIX-1895: height of fourth select element differs from the browser rendering due to incorrect resolving of max-height/height properties
            RunTest("selectSizeBasedHeightTest03");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest04() {
            RunTest("selectSizeBasedHeightTest04");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest05() {
            RunTest("selectSizeBasedHeightTest05");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest06() {
            RunTest("selectSizeBasedHeightTest06");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest07() {
            RunTest("selectSizeBasedHeightTest07");
        }

        [NUnit.Framework.Test]
        public virtual void SelectSizeBasedHeightTest08() {
            RunTest("selectSizeBasedHeightTest08");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void SelectPseudoTest01() {
            // pseudo elements are not supported for select element. The same behaviour is in Mozilla Firefox.
            RunTest("selectPseudoTest01");
        }

        [NUnit.Framework.Test]
        public virtual void SelectNotFittingTest01() {
            RunTest("selectNotFittingTest01");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void SelectNotFittingTest02() {
            RunTest("selectNotFittingTest02");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void SelectNotFittingTest03() {
            RunTest("selectNotFittingTest03");
        }

        private void RunTest(String name) {
            String htmlPath = sourceFolder + name + ".html";
            String outPdfPath = destinationFolder + name + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String diff = "diff_" + name + "_";
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outPdfPath));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                , diff));
        }
    }
}
