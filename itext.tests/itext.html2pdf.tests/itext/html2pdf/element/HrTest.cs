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
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HrTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/HrTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/HrTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HrHelloTest() {
            RunHrTest("00");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest01() {
            RunHrTest("01");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest02() {
            RunHrTest("02");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest03() {
            RunHrTest("03");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest04() {
            RunHrTest("04");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest05() {
            RunHrTest("05");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest06() {
            RunHrTest("06");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest07() {
            RunHrTest("07");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest08() {
            RunHrTest("08");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest09() {
            RunHrTest("09");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest10() {
            RunHrTest("10");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest11() {
            RunHrTest("11");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest12() {
            RunHrTest("12");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest13() {
            //box-shadow property is not supported in iText
            RunHrTest("13");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest14() {
            RunHrTest("14");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest15() {
            RunHrTest("15");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest16() {
            RunHrTest("16");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest17() {
            RunHrTest("17");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest18() {
            RunHrTest("18");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest19() {
            RunHrTest("19");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest20() {
            RunHrTest("20");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest21() {
            RunHrTest("21");
        }

        private void RunHrTest(String id) {
            String htmlPath = sourceFolder + "hrTest" + id + ".html";
            String outPdfPath = destinationFolder + "hrTest" + id + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_hrTest" + id + ".pdf";
            String diff = "diff" + id + "_";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outPdfPath));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                , diff));
        }
    }
}
