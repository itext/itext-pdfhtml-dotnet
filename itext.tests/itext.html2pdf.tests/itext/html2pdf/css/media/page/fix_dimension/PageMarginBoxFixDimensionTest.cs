/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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

namespace iText.Html2pdf.Css.Media.Page.Fix_dimension {
    [NUnit.Framework.Category("IntegrationTest")]
    public class PageMarginBoxFixDimensionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/fix_dimension/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/fix_dimension/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        // Top margin box tests
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPxTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPx", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPtTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPt", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPercent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixInTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixIn", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixCmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixCm", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixMmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixMm", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPcTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPc", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixEmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixEm", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixExTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixEx", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyRightFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyRightFixPercent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyCenterFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyCenterFixPercent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixCenterAndRightTest() {
            ConvertToPdfAndCompare("topFixCenterAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndFixCenterTest() {
            ConvertToPdfAndCompare("topFixLeftAndFixCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndRight() {
            ConvertToPdfAndCompare("topFixLeftAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndFixCenterAndFixRight() {
            ConvertToPdfAndCompare("topFixLeftAndFixCenterAndFixRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixTopTest() {
            ConvertToPdfAndCompare("leftOnlyFixTop", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixCenterTest() {
            ConvertToPdfAndCompare("leftOnlyFixCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixBottomTest() {
            ConvertToPdfAndCompare("leftOnlyFixBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndFixCenterTest() {
            ConvertToPdfAndCompare("leftTopAndFixCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndFixBottomTest() {
            ConvertToPdfAndCompare("leftTopAndFixBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftFixCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftFixCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftFixTopAndFixCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftFixTopAndFixCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PageMarginFont() {
            ConvertToPdfAndCompare("pageMarginFont", sourceFolder, destinationFolder);
        }
    }
}
