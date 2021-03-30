/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
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
