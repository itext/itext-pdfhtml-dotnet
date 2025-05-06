/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.IO;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Pdfa;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    // Android-Conversion-Skip-Line (TODO DEVSIX-7372 investigate why a few tests related to PdfA in iTextCore and PdfHtml were cut)
    [NUnit.Framework.Category("IntegrationTest")]
    public class ListTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ListTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ListTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest01() {
            ConvertToPdfAndCompare("listTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest02() {
            ConvertToPdfAndCompare("listTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest03() {
            ConvertToPdfAndCompare("listTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest04() {
            ConvertToPdfAndCompare("listTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NOT_SUPPORTED_LIST_STYLE_TYPE, Count = 32)]
        public virtual void ListTest05() {
            ConvertToPdfAndCompare("listTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest06() {
            ConvertToPdfAndCompare("listTest06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest07() {
            ConvertToPdfAndCompare("listTest07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest08() {
            ConvertToPdfAndCompare("listTest08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest09() {
            ConvertToPdfAndCompare("listTest09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest10() {
            ConvertToPdfAndCompare("listTest10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest11() {
            ConvertToPdfAndCompare("listTest11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest12() {
            ConvertToPdfAndCompare("listTest12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest13() {
            ConvertToPdfAndCompare("listTest13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest14() {
            ConvertToPdfAndCompare("listTest14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest15() {
            ConvertToPdfAndCompare("listTest15", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest16() {
            ConvertToPdfAndCompare("listTest16", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest17() {
            ConvertToPdfAndCompare("listTest17", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest18() {
            ConvertToPdfAndCompare("listTest18", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest19() {
            ConvertToPdfAndCompare("listTest19", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListTest20() {
            ConvertToPdfAndCompare("listTest20", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListLiValuePropertyTest() {
            ConvertToPdfAndCompare("listLiValuePropertyTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListStartPropertyTest() {
            ConvertToPdfAndCompare("listStartPropertyTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListItemValueTest() {
            ConvertToPdfAndCompare("listItemValueTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListItemValueTest01() {
            ConvertToPdfAndCompare("listItemValueTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListItemValueTest02() {
            ConvertToPdfAndCompare("listItemValueTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DescendingListTest() {
            ConvertToPdfAndCompare("descendingListTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListItemAbsolutePositionTest() {
            //TODO DEVSIX-2431 Positioned elements (e.g. absolute positioning) are lost when block is split across pages
            ConvertToPdfAndCompare("list-item-absolute", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CheckOrderedListStartAndValue() {
            //TODO: update after fix of DEVSIX-2537
            //http://www.timrivera.com/tests/ol-start.html
            ConvertToPdfAndCompare("checkOrderedListStartAndValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CheckOrderedListNestedLists() {
            //TODO: update after fix of DEVSIX-2538
            String expectedMessage = MessageFormatUtil.Format("The parameter must be a positive integer");
            Exception exception = NUnit.Framework.Assert.Catch(typeof(ArgumentException), () => ConvertToPdfAndCompare
                ("checkOrderedListNestedLists", SOURCE_FOLDER, DESTINATION_FOLDER));
            NUnit.Framework.Assert.AreEqual(expectedMessage, exception.Message);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 6)]
        public virtual void ListsWithInlineChildren() {
            //TODO: update after DEVSIX-2093, DEVSIX-2092, DEVSIX-2091 fixes
            ConvertToPdfAndCompare("listsWithInlineChildren", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InlineWithInlineBlockAsLiChildTest() {
            ConvertToPdfAndCompare("inlineWithInlineBlockAsLiChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertingListOver2PagesTest() {
            ConvertToPdfAndCompare("listOver2Pages", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListSymbolWithoutTextSplitTest() {
            ConvertToPdfAndCompare("listSymbolWithoutTextSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListWithImagesTest() {
            ConvertToPdfAndCompare("listWithImages", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // Android-Conversion-Skip-Block-Start (TODO DEVSIX-7372 investigate why a few tests related to PdfA in iTextCore and PdfHtml were cut)
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NOT_SUPPORTED_LIST_STYLE_TYPE, Count = 32)]
        public virtual void ListToPdfaTest() {
            Stream @is = new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read
                );
            PdfADocument pdfADocument = new PdfADocument(new PdfWriter(DESTINATION_FOLDER + "listToPdfa.pdf"), PdfAConformance
                .PDF_A_1B, new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
            using (FileStream fileInputStream = new FileStream(SOURCE_FOLDER + "listToPdfa.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfADocument, new ConverterProperties().SetMediaDeviceDescription
                    (new MediaDeviceDescription(MediaType.PRINT)).SetFontProvider(new BasicFontProvider(false, true, false
                    )));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "listToPdfa.pdf", SOURCE_FOLDER
                 + "cmp_listToPdfa.pdf", DESTINATION_FOLDER, "diff99_"));
        }
        // Android-Conversion-Skip-Block-End
    }
}
