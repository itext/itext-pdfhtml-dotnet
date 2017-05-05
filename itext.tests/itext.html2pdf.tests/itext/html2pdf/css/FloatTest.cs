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

namespace iText.Html2pdf.Css {
    public class FloatTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FloatTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FloatTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float01Test.html"), new FileInfo(destinationFolder
                 + "float01Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float01Test.pdf", sourceFolder
                 + "cmp_float01Test.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float02Test.html"), new FileInfo(destinationFolder
                 + "float02Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float02Test.pdf", sourceFolder
                 + "cmp_float02Test.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float03Test.html"), new FileInfo(destinationFolder
                 + "float03Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float03Test.pdf", sourceFolder
                 + "cmp_float03Test.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float04Test.html"), new FileInfo(destinationFolder
                 + "float04Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float04Test.pdf", sourceFolder
                 + "cmp_float04Test.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float05Test.html"), new FileInfo(destinationFolder
                 + "float05Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float05Test.pdf", sourceFolder
                 + "cmp_float05Test.pdf", destinationFolder, "diff05_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float06Test.html"), new FileInfo(destinationFolder
                 + "float06Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float06Test.pdf", sourceFolder
                 + "cmp_float06Test.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float07Test.html"), new FileInfo(destinationFolder
                 + "float07Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float07Test.pdf", sourceFolder
                 + "cmp_float07Test.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float08Test.html"), new FileInfo(destinationFolder
                 + "float08Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float08Test.pdf", sourceFolder
                 + "cmp_float08Test.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float09Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float09Test.html"), new FileInfo(destinationFolder
                 + "float09Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float09Test.pdf", sourceFolder
                 + "cmp_float09Test.pdf", destinationFolder, "diff09_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float10Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float10Test.html"), new FileInfo(destinationFolder
                 + "float10Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float10Test.pdf", sourceFolder
                 + "cmp_float10Test.pdf", destinationFolder, "diff10_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float11Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float11Test.html"), new FileInfo(destinationFolder
                 + "float11Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float11Test.pdf", sourceFolder
                 + "cmp_float11Test.pdf", destinationFolder, "diff11_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float12Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float12Test.html"), new FileInfo(destinationFolder
                 + "float12Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float12Test.pdf", sourceFolder
                 + "cmp_float12Test.pdf", destinationFolder, "diff12_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float13Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float13Test.html"), new FileInfo(destinationFolder
                 + "float13Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float13Test.pdf", sourceFolder
                 + "cmp_float13Test.pdf", destinationFolder, "diff13_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("In this test css property overflow: hidden is ignored by iText. This leads to invalid results. Perhaps, one day it will be fixed"
            )]
        public virtual void Float14Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float14Test.html"), new FileInfo(destinationFolder
                 + "float14Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float14Test.pdf", sourceFolder
                 + "cmp_float14Test.pdf", destinationFolder, "diff14_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float15Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float15Test.html"), new FileInfo(destinationFolder
                 + "float15Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float15Test.pdf", sourceFolder
                 + "cmp_float15Test.pdf", destinationFolder, "diff15_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float16Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float16Test.html"), new FileInfo(destinationFolder
                 + "float16Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float16Test.pdf", sourceFolder
                 + "cmp_float16Test.pdf", destinationFolder, "diff16_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float17Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float17Test.html"), new FileInfo(destinationFolder
                 + "float17Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float17Test.pdf", sourceFolder
                 + "cmp_float17Test.pdf", destinationFolder, "diff17_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float18Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float18Test.html"), new FileInfo(destinationFolder
                 + "float18Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float18Test.pdf", sourceFolder
                 + "cmp_float18Test.pdf", destinationFolder, "diff18_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float19Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float19Test.html"), new FileInfo(destinationFolder
                 + "float19Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float19Test.pdf", sourceFolder
                 + "cmp_float19Test.pdf", destinationFolder, "diff19_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float20Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float20Test.html"), new FileInfo(destinationFolder
                 + "float20Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float20Test.pdf", sourceFolder
                 + "cmp_float20Test.pdf", destinationFolder, "diff20_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float21Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float21Test.html"), new FileInfo(destinationFolder
                 + "float21Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float21Test.pdf", sourceFolder
                 + "cmp_float21Test.pdf", destinationFolder, "diff21_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float22Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float22Test.html"), new FileInfo(destinationFolder
                 + "float22Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float22Test.pdf", sourceFolder
                 + "cmp_float22Test.pdf", destinationFolder, "diff22_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float23Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float23Test.html"), new FileInfo(destinationFolder
                 + "float23Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float23Test.pdf", sourceFolder
                 + "cmp_float23Test.pdf", destinationFolder, "diff23_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float24Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float24Test.html"), new FileInfo(destinationFolder
                 + "float24Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float24Test.pdf", sourceFolder
                 + "cmp_float24Test.pdf", destinationFolder, "diff24_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float25Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float25Test.html"), new FileInfo(destinationFolder
                 + "float25Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float25Test.pdf", sourceFolder
                 + "cmp_float25Test.pdf", destinationFolder, "diff25_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Float26Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "float26Test.html"), new FileInfo(destinationFolder
                 + "float26Test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "float26Test.pdf", sourceFolder
                 + "cmp_float26Test.pdf", destinationFolder, "diff26_"));
        }
    }
}
