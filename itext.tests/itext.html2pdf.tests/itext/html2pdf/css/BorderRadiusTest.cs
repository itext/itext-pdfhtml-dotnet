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
    public class BorderRadiusTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BorderRadiusTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BorderRadiusTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest01.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest01.pdf"
                , sourceFolder + "cmp_borderRadiusTest01.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest02.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest02.pdf"
                , sourceFolder + "cmp_borderRadiusTest02.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest03.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest03.pdf"
                , sourceFolder + "cmp_borderRadiusTest03.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest04.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest04.pdf"
                , sourceFolder + "cmp_borderRadiusTest04.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest05.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest05.pdf"
                , sourceFolder + "cmp_borderRadiusTest05.pdf", destinationFolder, "diff05_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest06.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest06.pdf"
                , sourceFolder + "cmp_borderRadiusTest06.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest07.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest07.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest07.pdf"
                , sourceFolder + "cmp_borderRadiusTest07.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest08.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest08.pdf"
                , sourceFolder + "cmp_borderRadiusTest08.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius09Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest09.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest09.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest09.pdf"
                , sourceFolder + "cmp_borderRadiusTest09.pdf", destinationFolder, "diff09_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius10Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest10.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest10.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest10.pdf"
                , sourceFolder + "cmp_borderRadiusTest10.pdf", destinationFolder, "diff10_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius11Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest11.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest11.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest11.pdf"
                , sourceFolder + "cmp_borderRadiusTest11.pdf", destinationFolder, "diff11_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius12Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest12.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest12.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest12.pdf"
                , sourceFolder + "cmp_borderRadiusTest12.pdf", destinationFolder, "diff12_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BorderRadius13Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "borderRadiusTest13.html"), new FileInfo(destinationFolder
                 + "borderRadiusTest13.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "borderRadiusTest13.pdf"
                , sourceFolder + "cmp_borderRadiusTest13.pdf", destinationFolder, "diff13_"));
        }
    }
}
