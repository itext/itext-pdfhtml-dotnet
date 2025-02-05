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
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Media.Page.Max_dimension {
    [NUnit.Framework.Category("IntegrationTest")]
    public class PageMarginBoxMaxDimensionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/max_dimension/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/max_dimension/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        // Top margin box tests
        [NUnit.Framework.Test]
        public virtual void TopOnlyMaxLeftTest() {
            ConvertToPdfAndCompare("topOnlyMaxLeft", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyMaxRightTest() {
            ConvertToPdfAndCompare("topOnlyMaxRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyMaxCenterTest() {
            ConvertToPdfAndCompare("topOnlyMaxCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopMaxCenterAndRightTest() {
            ConvertToPdfAndCompare("topMaxCenterAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopMaxLeftAndCenterTest() {
            ConvertToPdfAndCompare("topMaxLeftAndCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopMaxLeftAndRight() {
            ConvertToPdfAndCompare("topMaxLeftAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopMaxLeftAndMaxCenterAndMaxRight() {
            ConvertToPdfAndCompare("topMaxLeftAndMaxCenterAndMaxRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        [NUnit.Framework.Test]
        public virtual void LeftOnlyMaxTopTest() {
            ConvertToPdfAndCompare("leftOnlyMaxTop", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyMaxCenterTest() {
            ConvertToPdfAndCompare("leftOnlyMaxCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyMaxBottomTest() {
            ConvertToPdfAndCompare("leftOnlyMaxBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndMaxCenterTest() {
            ConvertToPdfAndCompare("leftTopAndMaxCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndMaxBottomTest() {
            ConvertToPdfAndCompare("leftTopAndMaxBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftMaxCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftMaxCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftMaxTopAndMaxCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftMaxTopAndMaxCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PageMarginFont() {
            ConvertToPdfAndCompare("pageMarginFont", sourceFolder, destinationFolder);
        }
    }
}
