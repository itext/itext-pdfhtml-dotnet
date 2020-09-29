/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

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
    public class BackgroundRepeatTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BackgroundRepeatTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BackgroundRepeatTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRepeatTest() {
            ConvertToPdfAndCompare("imageBckgRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgNoRepeatTest() {
            ConvertToPdfAndCompare("imageBckgNoRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRoundTest() {
            ConvertToPdfAndCompare("imageBckgRound", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgSpaceTest() {
            ConvertToPdfAndCompare("imageBckgSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgSpaceWithBigBorderTest() {
            // TODO DEVSIX-2105
            ConvertToPdfAndCompare("imageBckgSpaceWithBigBorder", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRepeatXTest() {
            ConvertToPdfAndCompare("imageBckgRepeatX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRepeatYTest() {
            ConvertToPdfAndCompare("imageBckgRepeatY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRepeatTest() {
            ConvertToPdfAndCompare("linearGradientBckgRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgNoRepeatTest() {
            ConvertToPdfAndCompare("linearGradientBckgNoRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRoundTest() {
            ConvertToPdfAndCompare("linearGradientBckgRound", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgSpaceTest() {
            ConvertToPdfAndCompare("linearGradientBckgSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRepeatXTest() {
            ConvertToPdfAndCompare("linearGradientBckgRepeatX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRepeatYTest() {
            ConvertToPdfAndCompare("linearGradientBckgRepeatY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRepeatAndSpaceTest() {
            ConvertToPdfAndCompare("imageBckgRepeatAndSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRoundAndSpaceTest() {
            ConvertToPdfAndCompare("imageBckgRoundAndSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatAndBckgPositionXTest() {
            ConvertToPdfAndCompare("bckgRepeatAndBckgPositionX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatAndBckgPositionYTest() {
            ConvertToPdfAndCompare("bckgRepeatAndBckgPositionY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundSpaceAndBckgPositionXTest() {
            ConvertToPdfAndCompare("bckgRoundSpaceAndBckgPositionX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceRoundAndBckgPositionYTest() {
            ConvertToPdfAndCompare("bckgSpaceRoundAndBckgPositionY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatXAndBckgPositionYTest() {
            ConvertToPdfAndCompare("bckgRepeatXAndBckgPositionY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatYAndBckgPositionXTest() {
            ConvertToPdfAndCompare("bckgRepeatYAndBckgPositionX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundAndBckgPositionTest() {
            ConvertToPdfAndCompare("bckgRoundAndBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundAndNegativeBckgPositionTest() {
            ConvertToPdfAndCompare("bckgRoundAndNegativeBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceAndBckgPositionTest() {
            ConvertToPdfAndCompare("bckgSpaceAndBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceAndBckgPositionPageSeparationTest() {
            ConvertToPdfAndCompare("bckgSpaceAndBckgPositionPageSeparation", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceAndNegativeBckgPositionTest() {
            ConvertToPdfAndCompare("bckgSpaceAndNegativeBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceAndBckgPositionAdvancedTest() {
            ConvertToPdfAndCompare("bckgSpaceAndBckgPositionAdvanced", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BcgRoundAndBckgSizeAutoAndContainAdvancedTest() {
            ConvertToPdfAndCompare("bcgRoundAndBckgSizeAutoAndContainAdvanced", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundRemainsLessHalfOfImageTest() {
            ConvertToPdfAndCompare("bckgRoundRemainsLessHalfOfImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundRemainsMoreHalfOfImageTest() {
            ConvertToPdfAndCompare("bckgRoundRemainsMoreHalfOfImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundCompressAndStretchImageTest() {
            ConvertToPdfAndCompare("bckgRoundCompressAndStretchImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRoundBckgSizeLessThanImageTest() {
            ConvertToPdfAndCompare("imageBckgRoundBckgSizeLessThanImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgSpaceBckgSizeLessThanImageTest() {
            ConvertToPdfAndCompare("imageBckgSpaceBckgSizeLessThanImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
