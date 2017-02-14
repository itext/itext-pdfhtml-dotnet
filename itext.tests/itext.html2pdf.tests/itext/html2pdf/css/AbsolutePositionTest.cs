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
    address: sales@itextpdf.com */
using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Versions.Attributes;
using iText.Kernel;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class AbsolutePositionTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/AbsolutePositionTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/AbsolutePositionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest01.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest01.pdf"
                , sourceFolder + "cmp_absolutePositionTest01.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("Absolute position for elements that break across pages is not supported")]
        public virtual void AbsolutePosition02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest02.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest02.pdf"
                , sourceFolder + "cmp_absolutePositionTest02.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES)]
        public virtual void AbsolutePosition03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest03.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest03.pdf"
                , sourceFolder + "cmp_absolutePositionTest03.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest04.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest04.pdf"
                , sourceFolder + "cmp_absolutePositionTest04.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest05.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest05.pdf"
                , sourceFolder + "cmp_absolutePositionTest05.pdf", destinationFolder, "diff05_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest06.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest06.pdf"
                , sourceFolder + "cmp_absolutePositionTest06.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest07.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest07.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest07.pdf"
                , sourceFolder + "cmp_absolutePositionTest07.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest08.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest08.pdf"
                , sourceFolder + "cmp_absolutePositionTest08.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition09Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest09.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest09.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest09.pdf"
                , sourceFolder + "cmp_absolutePositionTest09.pdf", destinationFolder, "diff09_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AbsolutePosition10Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "absolutePositionTest10.html"), new FileInfo(destinationFolder
                 + "absolutePositionTest10.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "absolutePositionTest10.pdf"
                , sourceFolder + "cmp_absolutePositionTest10.pdf", destinationFolder, "diff10_"));
        }
    }
}
