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
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class DisplayTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/DisplayTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/DisplayTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayTable01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_table01.html"), new FileInfo(destinationFolder
                 + "display_table01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table01.pdf"
                , sourceFolder + "cmp_display_table01.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable02Test() {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + "display_table02.pdf"));
            pdfDoc.SetDefaultPageSize(new PageSize(1500, 842));
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + "display_table02.html", FileMode.Open, FileAccess.Read
                ), pdfDoc);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table02.pdf"
                , sourceFolder + "cmp_display_table02.pdf", destinationFolder, "diff20_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayTable03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_table03.html"), new FileInfo(destinationFolder
                 + "display_table03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table03.pdf"
                , sourceFolder + "cmp_display_table03.pdf", destinationFolder, "diff21_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayTable04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_table04.html"), new FileInfo(destinationFolder
                 + "display_table04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table04.pdf"
                , sourceFolder + "cmp_display_table04.pdf", destinationFolder, "diff22_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_table05.html"), new FileInfo(destinationFolder
                 + "display_table05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table05.pdf"
                , sourceFolder + "cmp_display_table05.pdf", destinationFolder, "diff23_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayTable06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_table06.html"), new FileInfo(destinationFolder
                 + "display_table06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table06.pdf"
                , sourceFolder + "cmp_display_table06.pdf", destinationFolder, "diff24_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void DisplayTable07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_table07.html"), new FileInfo(destinationFolder
                 + "display_table07.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table07.pdf"
                , sourceFolder + "cmp_display_table07.pdf", destinationFolder, "diff24_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayTable08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_table08.html"), new FileInfo(destinationFolder
                 + "display_table08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table08.pdf"
                , sourceFolder + "cmp_display_table08.pdf", destinationFolder, "diff25_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInline01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline01.html"), new FileInfo(destinationFolder
                 + "display_inline01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline01.pdf"
                , sourceFolder + "cmp_display_inline01.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block01.html"), new FileInfo(destinationFolder
                 + "display_inline-block01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block01.pdf"
                , sourceFolder + "cmp_display_inline-block01.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.INLINE_BLOCK_ELEMENT_WILL_BE_CLIPPED)]
        public virtual void DisplayInlineBlock02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block02.html"), new FileInfo(destinationFolder
                 + "display_inline-block02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block02.pdf"
                , sourceFolder + "cmp_display_inline-block02.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block03.html"), new FileInfo(destinationFolder
                 + "display_inline-block03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block03.pdf"
                , sourceFolder + "cmp_display_inline-block03.pdf", destinationFolder, "diff05_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block04.html"), new FileInfo(destinationFolder
                 + "display_inline-block04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block04.pdf"
                , sourceFolder + "cmp_display_inline-block04.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block05.html"), new FileInfo(destinationFolder
                 + "display_inline-block05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block05.pdf"
                , sourceFolder + "cmp_display_inline-block05.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block06.html"), new FileInfo(destinationFolder
                 + "display_inline-block06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block06.pdf"
                , sourceFolder + "cmp_display_inline-block06.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block07.html"), new FileInfo(destinationFolder
                 + "display_inline-block07.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block07.pdf"
                , sourceFolder + "cmp_display_inline-block07.pdf", destinationFolder, "diff09_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block08.html"), new FileInfo(destinationFolder
                 + "display_inline-block08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block08.pdf"
                , sourceFolder + "cmp_display_inline-block08.pdf", destinationFolder, "diff10_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock09Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block09.html"), new FileInfo(destinationFolder
                 + "display_inline-block09.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block09.pdf"
                , sourceFolder + "cmp_display_inline-block09.pdf", destinationFolder, "diff11_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock10Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block10.html"), new FileInfo(destinationFolder
                 + "display_inline-block10.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block10.pdf"
                , sourceFolder + "cmp_display_inline-block10.pdf", destinationFolder, "diff12_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock11Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block11.html"), new FileInfo(destinationFolder
                 + "display_inline-block11.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block11.pdf"
                , sourceFolder + "cmp_display_inline-block11.pdf", destinationFolder, "diff13_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock12Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block12.html"), new FileInfo(destinationFolder
                 + "display_inline-block12.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block12.pdf"
                , sourceFolder + "cmp_display_inline-block12.pdf", destinationFolder, "diff14_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock13Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block13.html"), new FileInfo(destinationFolder
                 + "display_inline-block13.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block13.pdf"
                , sourceFolder + "cmp_display_inline-block13.pdf", destinationFolder, "diff15_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock14Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block14.html"), new FileInfo(destinationFolder
                 + "display_inline-block14.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block14.pdf"
                , sourceFolder + "cmp_display_inline-block14.pdf", destinationFolder, "diff16_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock15Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block15.html"), new FileInfo(destinationFolder
                 + "display_inline-block15.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block15.pdf"
                , sourceFolder + "cmp_display_inline-block15.pdf", destinationFolder, "diff17_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock16Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block16.html"), new FileInfo(destinationFolder
                 + "display_inline-block16.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block16.pdf"
                , sourceFolder + "cmp_display_inline-block16.pdf", destinationFolder, "diff18_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock17Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "display_inline-block17.html"), new FileInfo(destinationFolder
                 + "display_inline-block17.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_inline-block17.pdf"
                , sourceFolder + "cmp_display_inline-block17.pdf", destinationFolder, "diff19_"));
        }
    }
}
