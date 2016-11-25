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
    public class TextPropertiesTest : ExtendedITextTest {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/../../resources/itext/html2pdf/css/TextPropertiesTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/TextPropertiesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
                );
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TextAlign01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "textAlignTest01.html"), new FileInfo(destinationFolder
                 + "textAlignTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "textAlignTest01.pdf"
                , sourceFolder + "cmp_textAlignTest01.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage("text-decoration: blink not supported")]
        public virtual void TextDecoration01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "textDecorationTest01.html"), new FileInfo(destinationFolder
                 + "textDecorationTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "textDecorationTest01.pdf"
                , sourceFolder + "cmp_textDecorationTest01.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage("text-indent in percents is not supported")]
        public virtual void TextIndent01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "textIndentTest01.html"), new FileInfo(destinationFolder
                 + "textIndentTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "textIndentTest01.pdf"
                , sourceFolder + "cmp_textIndentTest01.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LetterSpacing01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "letterSpacingTest01.html"), new FileInfo(destinationFolder
                 + "letterSpacingTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "letterSpacingTest01.pdf"
                , sourceFolder + "cmp_letterSpacingTest01.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void WordSpacing01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordSpacingTest01.html"), new FileInfo(destinationFolder
                 + "wordSpacingTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordSpacingTest01.pdf"
                , sourceFolder + "cmp_wordSpacingTest01.pdf", destinationFolder, "diff05_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LineHeight01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "lineHeightTest01.html"), new FileInfo(destinationFolder
                 + "lineHeightTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "lineHeightTest01.pdf"
                , sourceFolder + "cmp_lineHeightTest01.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Direction01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "directionTest01.html"), new FileInfo(destinationFolder
                 + "directionTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "directionTest01.pdf"
                , sourceFolder + "cmp_directionTest01.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void WhiteSpace01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "whiteSpaceTest01.html"), new FileInfo(destinationFolder
                 + "whiteSpaceTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "whiteSpaceTest01.pdf"
                , sourceFolder + "cmp_whiteSpaceTest01.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TextTransform01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "textTransformTest01.html"), new FileInfo(destinationFolder
                 + "textTransformTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "textTransformTest01.pdf"
                , sourceFolder + "cmp_textTransformTest01.pdf", destinationFolder, "diff09_"));
        }
    }
}
