/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    public class HeightTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/HeightTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/HeightTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest01() {
            String testName = "heightTest01";
            String diffPrefix = "diff01_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest02() {
            String testName = "heightTest02";
            String diffPrefix = "diff02_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest03() {
            String testName = "heightTest03";
            String diffPrefix = "diff03_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest04() {
            String testName = "heightTest04";
            String diffPrefix = "diff04_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest05() {
            String testName = "heightTest05";
            String diffPrefix = "diff05_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1007")]
        public virtual void HeightTest06() {
            String testName = "heightTest06";
            String diffPrefix = "diff06_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest07() {
            String testName = "heightTest07";
            String diffPrefix = "diff07_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest08() {
            String testName = "heightTest08";
            String diffPrefix = "diff08_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest09() {
            String testName = "heightTest09";
            String diffPrefix = "diff09_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest10() {
            String testName = "heightTest10";
            String diffPrefix = "diff10_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest11() {
            String testName = "heightTest11";
            String diffPrefix = "diff11_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest12() {
            String testName = "heightTest12";
            String diffPrefix = "diff12_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest13() {
            String testName = "heightTest13";
            String diffPrefix = "diff13_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest14() {
            String testName = "heightTest14";
            String diffPrefix = "diff14_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest15() {
            String testName = "heightTest15";
            String diffPrefix = "diff15_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest16() {
            String testName = "heightTest16";
            String diffPrefix = "diff16_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightTest17() {
            String testName = "heightTest17";
            String diffPrefix = "diff17_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest01() {
            String testName = "heightWithCollapsingMarginsTest01";
            String diffPrefix = "diffMargins01_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest03() {
            String testName = "heightWithCollapsingMarginsTest03";
            String diffPrefix = "diffMargins03_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest04() {
            String testName = "heightWithCollapsingMarginsTest04";
            String diffPrefix = "diffMargins04_";
            // second paragraph should not be drawn in pdf, as it doesn't fit with it's margins
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightLargerThanMinHeight01() {
            // TODO DEVSIX-1895: height differs from the browser rendering due to incorrect resolving of max-height/height properties
            String testName = "heightLargerThanMinHeight01";
            String diffPrefix = "diffLargerMin01_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeightLesserThanMaxHeight01() {
            // TODO DEVSIX-1895: height differs from the browser rendering due to incorrect resolving of max-height/height properties
            String testName = "heightLesserThanMaxHeight01";
            String diffPrefix = "diffLessMax01_";
            RunTest(testName, diffPrefix);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void RunTest(String testName, String diffPrefix) {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + testName + ".html"), new FileInfo(destinationFolder
                 + testName + ".pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + testName + ".html")
                .AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + testName + ".pdf", sourceFolder
                 + "cmp_" + testName + ".pdf", destinationFolder, diffPrefix));
        }
    }
}
