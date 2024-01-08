/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BoxSizingTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BoxSizingTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BoxSizingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentTest01() {
            ConvertToPdfAndCompare("boxSizingCellContentTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentTest02() {
            ConvertToPdfAndCompare("boxSizingCellContentTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentImgTest() {
            ConvertToPdfAndCompare("boxSizingCellContentImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest01() {
            // TODO: DEVSIX-5468 update cmp file after fixing
            ConvertToPdfAndCompare("boxSizingCellTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest02() {
            ConvertToPdfAndCompare("boxSizingCellTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest03() {
            // TODO: DEVSIX-5468 update cmp file after fixing
            ConvertToPdfAndCompare("boxSizingCellTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFloat01Test() {
            ConvertToPdfAndCompare("boxSizingFloat01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFloat02Test() {
            ConvertToPdfAndCompare("boxSizingFloat02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth01Test() {
            ConvertToPdfAndCompare("boxSizingRelativeWidth01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth02Test() {
            ConvertToPdfAndCompare("boxSizingRelativeWidth02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth03Test() {
            ConvertToPdfAndCompare("boxSizingRelativeWidth03Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv01Test() {
            ConvertToPdfAndCompare("boxSizingDiv01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDivWithImgTest() {
            ConvertToPdfAndCompare("boxSizingDivWithImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv03Test() {
            ConvertToPdfAndCompare("boxSizingDiv03Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv04Test() {
            // Inner div still doesn't fit, because it's height is increased every time page split occurs by margins
            // borders padding. Thus, if parent height was manually fixed to include child with fixed height and if
            // page split occurs - child might not fit.
            ConvertToPdfAndCompare("boxSizingDiv04Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingPara01Test() {
            ConvertToPdfAndCompare("boxSizingPara01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingParaWithImgTest() {
            ConvertToPdfAndCompare("boxSizingParaWithImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingPara03Test() {
            ConvertToPdfAndCompare("boxSizingPara03Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            2)]
        public virtual void BoxSizingTable01Test() {
            ConvertToPdfAndCompare("boxSizingTable01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable02Test() {
            ConvertToPdfAndCompare("boxSizingTable02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTableWithImgTest() {
            ConvertToPdfAndCompare("boxSizingTableWithImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable04Test() {
            ConvertToPdfAndCompare("boxSizingTable04Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable05Test() {
            ConvertToPdfAndCompare("boxSizingTable05Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable06Test() {
            ConvertToPdfAndCompare("boxSizingTable06Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingMinMaxHeight01Test() {
            ConvertToPdfAndCompare("boxSizingMinMaxHeight01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingInlineBlock01Test() {
            ConvertToPdfAndCompare("boxSizingInlineBlock01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingInlineBlock02Test() {
            ConvertToPdfAndCompare("boxSizingInlineBlock02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest01() {
            ConvertToPdfAndCompare("boxSizingFormTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest02() {
            ConvertToPdfAndCompare("boxSizingFormTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest03() {
            // At least in chrome, borders of buttons are always included to width and height (just as with border-box)
            ConvertToPdfAndCompare("boxSizingFormTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingLiTest01() {
            ConvertToPdfAndCompare("boxSizingLiTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingLiTest02() {
            ConvertToPdfAndCompare("boxSizingLiTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingImageTest() {
            ConvertToPdfAndCompare("boxSizingImage", sourceFolder, destinationFolder);
        }
    }
}
