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
    public class TableTest : ExtendedITextTest {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/../../resources/itext/html2pdf/element/TableTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/TableTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
                );
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloTableDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_table.html"), new FileInfo(destinationFolder
                 + "hello_table.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_table.pdf", sourceFolder
                 + "cmp_hello_table.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloTableHeaderFooterDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_table_header_footer.html"), new FileInfo(destinationFolder
                 + "hello_table_header_footer.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_table_header_footer.pdf"
                , sourceFolder + "cmp_hello_table_header_footer.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloTableColspanDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_table_colspan.html"), new FileInfo(destinationFolder
                 + "hello_table_colspan.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_table_colspan.pdf"
                , sourceFolder + "cmp_hello_table_colspan.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloTableRowspanDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_table_rowspan.html"), new FileInfo(destinationFolder
                 + "hello_table_rowspan.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_table_rowspan.pdf"
                , sourceFolder + "cmp_hello_table_rowspan.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloTableColspanRowspanDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_table_colspan_rowspan.html"), new FileInfo(destinationFolder
                 + "hello_table_colspan_rowspan.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_table_colspan_rowspan.pdf"
                , sourceFolder + "cmp_hello_table_colspan_rowspan.pdf", destinationFolder, "diff05_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableCssPropsTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "tableCssPropsTest01.html"), new FileInfo(destinationFolder
                 + "tableCssPropsTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "tableCssPropsTest01.pdf"
                , sourceFolder + "cmp_tableCssPropsTest01.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TableCssPropsTest02() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "tableCssPropsTest02.html"), new FileInfo(destinationFolder
                 + "tableCssPropsTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "tableCssPropsTest02.pdf"
                , sourceFolder + "cmp_tableCssPropsTest02.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DefaultTableTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "defaultTable.html"), new FileInfo(destinationFolder
                 + "defaultTable.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "defaultTable.pdf", sourceFolder
                 + "cmp_defaultTable.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TextInTableAndRowTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "textInTableAndRow.html"), new FileInfo(destinationFolder
                 + "textInTableAndRow.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "textInTableAndRow.pdf"
                , sourceFolder + "cmp_textInTableAndRow.pdf", destinationFolder, "diff09_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ThTagTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "thTag.html"), new FileInfo(destinationFolder + "thTag.pdf"
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "thTag.pdf", sourceFolder
                 + "cmp_thTag.pdf", destinationFolder, "diff10_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BrInTdTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "brInTd.html"), new FileInfo(destinationFolder + "brInTd.pdf"
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "brInTd.pdf", sourceFolder
                 + "cmp_brInTd.pdf", destinationFolder, "diff12_"));
        }
    }
}
