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
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class BoxSizingTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BoxSizingTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BoxSizingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentTest01() {
            RunTest("boxSizingCellContentTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentTest02() {
            RunTest("boxSizingCellContentTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest01() {
            // TODO Result of processing of this html is different from what browsers show.
            // Height of cells is always border-box-like at least if DOCTYPE "html" is not specified.
            // See also boxSizingCellTest03.
            RunTest("boxSizingCellTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest02() {
            RunTest("boxSizingCellTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest03() {
            // This test is exactly the same as boxSizingCellTest01, except DOCTYPE "html" is used:
            // cells height is different in browsers depending on box-sizing.
            // TODO: we don't include half of the borders in height calculation when border-box is set
            // because we apply borders on table level. However, this seems to be not very important for heights,
            // height will only be bigger and it's not that crucial in comparison to width calculations.
            RunTest("boxSizingCellTest03");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingFloat01Test() {
            RunTest("boxSizingFloat01Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingFloat02Test() {
            RunTest("boxSizingFloat02Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth01Test() {
            RunTest("boxSizingRelativeWidth01Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth02Test() {
            RunTest("boxSizingRelativeWidth02Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth03Test() {
            RunTest("boxSizingRelativeWidth03Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv01Test() {
            RunTest("boxSizingDiv01Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv03Test() {
            RunTest("boxSizingDiv03Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv04Test() {
            // TODO inner div still doesn't fit, because it's height is increased every time page split occurs by margins borders padding
            // Thus, if parent height was manually fixed to include child with fixed height and if page split occurs - child might not fit.
            RunTest("boxSizingDiv04Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingPara01Test() {
            RunTest("boxSizingPara01Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingPara03Test() {
            RunTest("boxSizingPara03Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 2)]
        public virtual void BoxSizingTable01Test() {
            RunTest("boxSizingTable01Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingTable02Test() {
            RunTest("boxSizingTable02Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingTable04Test() {
            RunTest("boxSizingTable04Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingTable05Test() {
            RunTest("boxSizingTable05Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingTable06Test() {
            RunTest("boxSizingTable06Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingMinMaxHeight01Test() {
            RunTest("boxSizingMinMaxHeight01Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingInlineBlock01Test() {
            RunTest("boxSizingInlineBlock01Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingInlineBlock02Test() {
            RunTest("boxSizingInlineBlock02Test");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest01() {
            RunTest("boxSizingFormTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest02() {
            RunTest("boxSizingFormTest02");
        }

        // TODO we don't apply height to textarea yet
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest03() {
            RunTest("boxSizingFormTest03");
        }

        // TODO at least in chrome, borders of buttons are always included to width and height (just as with border-box)
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingLiTest01() {
            RunTest("boxSizingLiTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BoxSizingLiTest02() {
            RunTest("boxSizingLiTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String testName) {
            String htmlName = sourceFolder + testName + ".html";
            String outFileName = destinationFolder + testName + ".pdf";
            String cmpFileName = sourceFolder + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff_" + testName + "_"));
        }
    }
}
