/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using iText.Html2pdf.Resolver.Font;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Pdfa;
using iText.StyledXmlParser.Css.Media;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class ListTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ListTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ListTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest01() {
            RunTest("listTest01");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest02() {
            RunTest("listTest02");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest03() {
            RunTest("listTest03");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest04() {
            RunTest("listTest04");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NOT_SUPPORTED_LIST_STYLE_TYPE, Count = 32)]
        public virtual void ListTest05() {
            RunTest("listTest05");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest06() {
            RunTest("listTest06");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest07() {
            RunTest("listTest07");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest08() {
            RunTest("listTest08");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest09() {
            RunTest("listTest09");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest10() {
            RunTest("listTest10");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest11() {
            RunTest("listTest11");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest12() {
            RunTest("listTest12");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest13() {
            RunTest("listTest13");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest14() {
            RunTest("listTest14");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest15() {
            RunTest("listTest15");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest16() {
            RunTest("listTest16");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest17() {
            RunTest("listTest17");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest18() {
            RunTest("listTest18");
        }

        [NUnit.Framework.Test]
        public virtual void ListTest19() {
            RunTest("listTest19");
        }

        [NUnit.Framework.Test]
        public virtual void ListLiValuePropertyTest() {
            RunTest("listLiValuePropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ListStartPropertyTest() {
            RunTest("listStartPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ListItemValueTest() {
            RunTest("listItemValueTest");
        }

        [NUnit.Framework.Test]
        public virtual void ListItemValueTest01() {
            RunTest("listItemValueTest01");
        }

        [NUnit.Framework.Test]
        public virtual void ListItemValueTest02() {
            RunTest("listItemValueTest02");
        }

        [NUnit.Framework.Test]
        public virtual void DescendingListTest() {
            RunTest("descendingListTest");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-2431")]
        public virtual void ListItemAbsolutePositionTest() {
            RunTest("list-item-absolute");
        }

        [NUnit.Framework.Test]
        public virtual void CheckOrderedListStartAndValue() {
            //TODO: update after fix of DEVSIX-2537
            //http://www.timrivera.com/tests/ol-start.html
            RunTest("checkOrderedListStartAndValue");
        }

        [NUnit.Framework.Test]
        public virtual void CheckOrderedListNestedLists() {
            NUnit.Framework.Assert.That(() =>  {
                //TODO: update after fix of DEVSIX-2538
                RunTest("checkOrderedListNestedLists");
            }
            , NUnit.Framework.Throws.InstanceOf<ArgumentException>().With.Message.EqualTo(MessageFormatUtil.Format("The parameter must be a positive integer")))
;
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 6)]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES, Count = 12, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void ListsWithInlineChildren() {
            //TODO: update after DEVSIX-2093, DEVSIX-2092, DEVSIX-2091 fixes
            RunTest("listsWithInlineChildren");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NOT_SUPPORTED_LIST_STYLE_TYPE, Count = 32)]
        public virtual void ListToPdfaTest() {
            Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read);
            PdfADocument pdfADocument = new PdfADocument(new PdfWriter(destinationFolder + "listToPdfa.pdf"), PdfAConformanceLevel
                .PDF_A_1B, new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
            using (FileStream fileInputStream = new FileStream(sourceFolder + "listToPdfa.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfADocument, new ConverterProperties().SetMediaDeviceDescription
                    (new MediaDeviceDescription(MediaType.PRINT)).SetFontProvider(new DefaultFontProvider(false, true, false
                    )));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listToPdfa.pdf", sourceFolder
                 + "cmp_listToPdfa.pdf", destinationFolder, "diff99_"));
        }

        private void RunTest(String testName) {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + testName + ".html"), new FileInfo(destinationFolder
                 + testName + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + testName + ".pdf", sourceFolder
                 + "cmp_" + testName + ".pdf", destinationFolder, "diff_" + testName));
        }
    }
}
