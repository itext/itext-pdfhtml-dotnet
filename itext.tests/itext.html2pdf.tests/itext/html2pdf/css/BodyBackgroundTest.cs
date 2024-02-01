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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BodyBackgroundTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BodyBackgroundTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BodyBackgroundTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithBorderTest() {
            ConvertToPdfAndCompare("bodyBorder", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithHeightTest() {
            ConvertToPdfAndCompare("bodyHeight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithMarginTest() {
            ConvertToPdfAndCompare("bodyMargin", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithMarginNegativeTest() {
            ConvertToPdfAndCompare("bodyMarginNegative", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FirstElementHasMarginTest() {
            ConvertToPdfAndCompare("firstElementHasMargin", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MarginNegativeTest() {
            ConvertToPdfAndCompare("marginNegative", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MultiplePageContentTest() {
            ConvertToPdfAndCompare("multiplePageContent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PositionAbsoluteTest() {
            ConvertToPdfAndCompare("positionAbsolute", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PositionFixedTest() {
            ConvertToPdfAndCompare("positionFixed", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BordersAndBackgroundsTest() {
            //todo DEVSIX-4732 html borders
            ConvertToPdfAndCompare("bordersAndBackgrounds", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BordersAndBackgroundsWithPagePropertiesTest() {
            //todo DEVSIX-4732 html borders
            ConvertToPdfAndCompare("bordersAndBackgroundsWithPageProperties", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BordersAndBackgroundsWithPageMarksTest() {
            //todo DEVSIX-4732 html borders
            ConvertToPdfAndCompare("bordersAndBackgroundsWithPageMarks", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FloatRightElementTest() {
            ConvertToPdfAndCompare("floatRightElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TransformRotateElementTest() {
            ConvertToPdfAndCompare("transformRotateElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneElementTest() {
            ConvertToPdfAndCompare("displayNoneElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWithFixedHeightElementTest() {
            ConvertToPdfAndCompare("overflowWithFixedHeightElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisibilityHiddenElementTest() {
            ConvertToPdfAndCompare("visibilityHiddenElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PagePropertyWithBackgroundImageTest() {
            ConvertToPdfAndCompare("pagePropertyBackgroundImage", sourceFolder, destinationFolder);
        }
    }
}
