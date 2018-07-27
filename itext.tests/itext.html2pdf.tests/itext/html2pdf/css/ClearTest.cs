/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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

namespace iText.Html2pdf.Css {
    public class ClearTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/ClearTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/ClearTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear02Test.html"), new FileInfo(destinationFolder
                 + "clear02Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear02Test.pdf", sourceFolder
                 + "cmp_clear02Test.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear03Test.html"), new FileInfo(destinationFolder
                 + "clear03Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear03Test.pdf", sourceFolder
                 + "cmp_clear03Test.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear04Test.html"), new FileInfo(destinationFolder
                 + "clear04Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear04Test.pdf", sourceFolder
                 + "cmp_clear04Test.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear06Test.html"), new FileInfo(destinationFolder
                 + "clear06Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear06Test.pdf", sourceFolder
                 + "cmp_clear06Test.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear07Test.html"), new FileInfo(destinationFolder
                 + "clear07Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear07Test.pdf", sourceFolder
                 + "cmp_clear07Test.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Clear08Test() {
            // TODO behaving differently from browser in some cases of selfcollapsing margins
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear08Test.html"), new FileInfo(destinationFolder
                 + "clear08Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear08Test.pdf", sourceFolder
                 + "cmp_clear08Test.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear09Test() {
            // TODO behaving differently from browser in some cases of selfcollapsing margins
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear09Test.html"), new FileInfo(destinationFolder
                 + "clear09Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear09Test.pdf", sourceFolder
                 + "cmp_clear09Test.pdf", destinationFolder, "diff09_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear10Test() {
            // TODO behaving differently from browser in some cases of selfcollapsing margins
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear10Test.html"), new FileInfo(destinationFolder
                 + "clear10Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear10Test.pdf", sourceFolder
                 + "cmp_clear10Test.pdf", destinationFolder, "dif10_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Clear11Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "clear11Test.html"), new FileInfo(destinationFolder
                 + "clear11Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "clear11Test.pdf", sourceFolder
                 + "cmp_clear11Test.pdf", destinationFolder, "dif11_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String testName, String diff) {
            String htmlName = sourceFolder + testName + ".html";
            String outFileName = destinationFolder + testName + ".pdf";
            String cmpFileName = sourceFolder + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , diff));
        }
    }
}
