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
    public class TextPropertiesTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/TextPropertiesTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/TextPropertiesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TextAlignTest01() {
            RunTest("textAlignTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.TEXT_DECORATION_BLINK_NOT_SUPPORTED)]
        public virtual void TextDecorationTest01() {
            RunTest("textDecorationTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LetterSpacingTest01() {
            RunTest("letterSpacingTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void WordSpacingTest01() {
            RunTest("wordSpacingTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LineHeightTest01() {
            RunTest("lineHeightTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LineHeightTest02() {
            RunTest("lineHeightTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LineHeightTest03() {
            RunTest("lineHeightTest03");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void WhiteSpaceTest01() {
            RunTest("whiteSpaceTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TextTransformTest01() {
            RunTest("textTransformTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TextTransform02Test() {
            RunTest("textTransformTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void WhiteSpaceTest02() {
            RunTest("whiteSpaceTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest01() {
            RunTest("enspEmspThinspTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest02() {
            RunTest("enspEmspThinspTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest03() {
            RunTest("enspEmspThinspTest03");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest04() {
            RunTest("enspEmspThinspTest04");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest05() {
            RunTest("enspEmspThinspTest05");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest06() {
            RunTest("enspEmspThinspTest06");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest07() {
            RunTest("enspEmspThinspTest07");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest08() {
            // TODO DEVSIX-1442
            RunTest("enspEmspThinspTest08");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1442")]
        public virtual void EnspEmspThinspTest09() {
            RunTest("enspEmspThinspTest09");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1851")]
        public virtual void WordCharSpacingJustifiedTest01() {
            RunTest("wordCharSpacingJustifiedTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String testName) {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + testName + ".html"), new FileInfo(destinationFolder
                 + testName + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + testName + ".pdf", sourceFolder
                 + "cmp_" + testName + ".pdf", destinationFolder, "diff_" + testName));
        }
    }
}
