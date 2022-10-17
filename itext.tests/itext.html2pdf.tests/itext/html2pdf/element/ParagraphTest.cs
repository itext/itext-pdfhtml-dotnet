/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ParagraphTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ParagraphTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ParagraphTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphTest01.html"), new FileInfo(destinationFolder
                 + "paragraphTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphTest01.pdf"
                , sourceFolder + "cmp_paragraphTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithBordersTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithBordersTest01.html"), new FileInfo(destinationFolder
                 + "paragraphWithBordersTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithBordersTest01.pdf"
                , sourceFolder + "cmp_paragraphWithBordersTest01.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithMarginsTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithMarginsTest01.html"), new FileInfo(destinationFolder
                 + "paragraphWithMarginsTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithMarginsTest01.pdf"
                , sourceFolder + "cmp_paragraphWithMarginsTest01.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithPaddingTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithPaddingTest01.html"), new FileInfo(destinationFolder
                 + "paragraphWithPaddingTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithPaddingTest01.pdf"
                , sourceFolder + "cmp_paragraphWithPaddingTest01.pdf", destinationFolder, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithFontAttributesTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithFontAttributesTest01.html"), new FileInfo
                (destinationFolder + "paragraphWithFontAttributesTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithFontAttributesTest01.pdf"
                , sourceFolder + "cmp_paragraphWithFontAttributesTest01.pdf", destinationFolder, "diff05_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithNonBreakableSpaceTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithNonBreakableSpaceTest01.html"), new FileInfo
                (destinationFolder + "paragraphWithNonBreakableSpaceTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithNonBreakableSpaceTest01.pdf"
                , sourceFolder + "cmp_paragraphWithNonBreakableSpaceTest01.pdf", destinationFolder, "diff06_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithNonBreakableSpaceTest02() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithNonBreakableSpaceTest02.html"), new FileInfo
                (destinationFolder + "paragraphWithNonBreakableSpaceTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithNonBreakableSpaceTest02.pdf"
                , sourceFolder + "cmp_paragraphWithNonBreakableSpaceTest02.pdf", destinationFolder, "diff07_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithNonBreakableSpaceTest03() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithNonBreakableSpaceTest03.html"), new FileInfo
                (destinationFolder + "paragraphWithNonBreakableSpaceTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithNonBreakableSpaceTest03.pdf"
                , sourceFolder + "cmp_paragraphWithNonBreakableSpaceTest03.pdf", destinationFolder, "diff08_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphInTablePercentTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphInTablePercentTest01.html"), new FileInfo
                (destinationFolder + "paragraphInTablePercentTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphInTablePercentTest01.pdf"
                , sourceFolder + "cmp_paragraphInTablePercentTest01.pdf", destinationFolder, "diff09_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithButtonInputLabelSelectTextareaTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithButtonInputLabelSelectTextareaTest.html"
                ), new FileInfo(destinationFolder + "paragraphWithButtonInputLabelSelectTextareaTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithButtonInputLabelSelectTextareaTest.pdf"
                , sourceFolder + "cmp_paragraphWithButtonInputLabelSelectTextareaTest.pdf", destinationFolder, "diff11_"
                ));
        }

        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void ParagraphWithBdoBrImgMapQSubSupTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithBdoBrImgMapQSubSupTest.html"), new FileInfo
                (destinationFolder + "paragraphWithBdoBrImgMapQSubSupTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithBdoBrImgMapQSubSupTest.pdf"
                , sourceFolder + "cmp_paragraphWithBdoBrImgMapQSubSupTest.pdf", destinationFolder, "diff12_"));
        }

        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void ParagraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.html"
                ), new FileInfo(destinationFolder + "paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.pdf"
                , sourceFolder + "cmp_paragraphWithAbbrAcronymCireCodeDfnEmKbdSampVarTest.pdf", destinationFolder, "diff13_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithAParagraphSpanDivTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithAParagraphSpanDivTest.html"), new FileInfo
                (destinationFolder + "paragraphWithAParagraphSpanDivTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithAParagraphSpanDivTest.pdf"
                , sourceFolder + "cmp_paragraphWithAParagraphSpanDivTest.pdf", destinationFolder, "diff14_"));
        }

        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 2)]
        [NUnit.Framework.Test]
        public virtual void ParagraphWithBBigISmallTtStrongTest() {
            //TODO: update after DEVSIX-2445 fix
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithBBigISmallTtStrongTest.html"), new FileInfo
                (destinationFolder + "paragraphWithBBigISmallTtStrongTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithBBigISmallTtStrongTest.pdf"
                , sourceFolder + "cmp_paragraphWithBBigISmallTtStrongTest.pdf", destinationFolder, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithPDisplayTableTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithPDisplayTableTest.html"), new FileInfo
                (destinationFolder + "paragraphWithPDisplayTableTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithPDisplayTableTest.pdf"
                , sourceFolder + "cmp_paragraphWithPDisplayTableTest.pdf", destinationFolder, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithDifferentSpansTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithDifferentSpansTest.html"), new FileInfo
                (destinationFolder + "paragraphWithDifferentSpansTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithDifferentSpansTest.pdf"
                , sourceFolder + "cmp_paragraphWithDifferentSpansTest.pdf", destinationFolder, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithDifferentBlocksAndDisplaysTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithDifferentBlocksAndDisplaysTest.html")
                , new FileInfo(destinationFolder + "paragraphWithDifferentBlocksAndDisplaysTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithDifferentBlocksAndDisplaysTest.pdf"
                , sourceFolder + "cmp_paragraphWithDifferentBlocksAndDisplaysTest.pdf", destinationFolder, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithLabelSpanDisplayBlockTest() {
            //TODO: update after DEVSIX-2619 fix
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithLabelSpanDisplayBlockTest.html"), new 
                FileInfo(destinationFolder + "paragraphWithLabelSpanDisplayBlockTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithLabelSpanDisplayBlockTest.pdf"
                , sourceFolder + "cmp_paragraphWithLabelSpanDisplayBlockTest.pdf", destinationFolder, "diff15_"));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithImageTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithImageTest01.html"), new FileInfo(destinationFolder
                 + "paragraphWithImageTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithImageTest01.pdf"
                , sourceFolder + "cmp_paragraphWithImageTest01.pdf", destinationFolder, "diff_paragraphWithImageTest01_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphWithImageTest01RTL() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "paragraphWithImageTest01RTL.html"), new FileInfo(destinationFolder
                 + "paragraphWithImageTest01RTL.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "paragraphWithImageTest01RTL.pdf"
                , sourceFolder + "cmp_paragraphWithImageTest01RTL.pdf", destinationFolder, "diff_paragraphWithImageTest01_"
                ));
        }
    }
}
