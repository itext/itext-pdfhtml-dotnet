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
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgNoRepeatTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgNoRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRoundTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgRound", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgSpaceTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRepeatXTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgRepeatX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRepeatYTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgRepeatY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRepeatTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("linearGradientBckgRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgNoRepeatTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("linearGradientBckgNoRepeat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRoundTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("linearGradientBckgRound", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgSpaceTest() {
            //TODO: DEVSIX-1708 update cmp file
            ConvertToPdfAndCompare("linearGradientBckgSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRepeatXTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("linearGradientBckgRepeatX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientBckgRepeatYTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("linearGradientBckgRepeatY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRepeatAndSpaceTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgRepeatAndSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRoundAndSpaceTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgRoundAndSpace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatAndBckgPositionXTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRepeatAndBckgPositionX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatAndBckgPositionYTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRepeatAndBckgPositionY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundSpaceAndBckgPositionXTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRoundSpaceAndBckgPositionX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceRoundAndBckgPositionYTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgSpaceRoundAndBckgPositionY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatXAndBckgPositionYTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRepeatXAndBckgPositionY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRepeatYAndBckgPositionXTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRepeatYAndBckgPositionX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundAndBckgPositionTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRoundAndBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundAndNegativeBckgPositionTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRoundAndNegativeBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceAndBckgPositionTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgSpaceAndBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceAndBckgPositionPageSeparationTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgSpaceAndBckgPositionPageSeparation", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgSpaceAndNegativeBckgPositionTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgSpaceAndNegativeBckgPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundRemainsLessHalfOfImageTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRoundRemainsLessHalfOfImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundRemainsMoreHalfOfImageTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRoundRemainsMoreHalfOfImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BckgRoundCompressAndStretchImageTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("bckgRoundCompressAndStretchImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgRoundBckgSizeLessThanImageTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgRoundBckgSizeLessThanImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBckgSpaceBckgSizeLessThanImageTest() {
            //TODO: DEVSIX-4370 update cmp file
            ConvertToPdfAndCompare("imageBckgSpaceBckgSizeLessThanImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
