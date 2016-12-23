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

namespace iText.Html2pdf.Element {
    public class ColColgroupTest : ExtendedITextTest {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/../../resources/itext/html2pdf/element/ColColgroupTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ColColgroupTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
                );
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleBackgroundTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "simpleBackgroundTest.html"), new FileInfo(destinationFolder
                 + "simpleBackgroundTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "simpleBackgroundTest.pdf"
                , sourceFolder + "cmp_simpleBackgroundTest.pdf", destinationFolder, "diff_simpleBackgroundTest_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTdColspanTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "simpleTdColspanTest.html"), new FileInfo(destinationFolder
                 + "simpleTdColspanTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "simpleTdColspanTest.pdf"
                , sourceFolder + "cmp_simpleTdColspanTest.pdf", destinationFolder, "diff_simpleTdColspanTest_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTdRowspanTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "simpleTdRowspanTest.html"), new FileInfo(destinationFolder
                 + "simpleTdRowspanTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "simpleTdRowspanTest.pdf"
                , sourceFolder + "cmp_simpleTdRowspanTest.pdf", destinationFolder, "diff_simpleTdRowspanTest_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTdColspanRowspanTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "simpleTdColspanRowspanTest.html"), new FileInfo(destinationFolder
                 + "simpleTdColspanRowspanTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "simpleTdColspanRowspanTest.pdf"
                , sourceFolder + "cmp_simpleTdColspanRowspanTest.pdf", destinationFolder, "diff_simpleTdColspanRowspanTest_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ComplexColspanRowspanTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "complexColspanRowspanTest.html"), new FileInfo(destinationFolder
                 + "complexColspanRowspanTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "complexColspanRowspanTest.pdf"
                , sourceFolder + "cmp_complexColspanRowspanTest.pdf", destinationFolder, "diff_complexColspanRowspanTest_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleWidthTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "simpleWidthTest.html"), new FileInfo(destinationFolder
                 + "simpleWidthTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "simpleWidthTest.pdf"
                , sourceFolder + "cmp_simpleWidthTest.pdf", destinationFolder, "diff_simpleWidthTest_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void WidthColOverridedTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "widthColOverridedTest.html"), new FileInfo(destinationFolder
                 + "widthColOverridedTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "widthColOverridedTest.pdf"
                , sourceFolder + "cmp_widthColOverridedTest.pdf", destinationFolder, "diff_widthColOverridedTest_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void WidthColgroupOverridedTest() {
            //In this test we use FireFox behavior that treat <colgroup> and <col> tags equally and don't override colgroup's width value with smaller one in case of width set on <td>
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "widthColgroupOverridedTest.html"), new FileInfo(destinationFolder
                 + "widthColgroupOverridedTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "widthColgroupOverridedTest.pdf"
                , sourceFolder + "cmp_widthColgroupOverridedTest.pdf", destinationFolder, "diff_widthColgroupOverridedTest_"
                ));
        }
    }
}
