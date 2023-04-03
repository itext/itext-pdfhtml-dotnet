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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BackgroundClipTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BackgroundClipTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BackgroundClipTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorBorderBoxTest() {
            ConvertToPdfAndCompare("backgroundColorBorderBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorContentBoxTest() {
            ConvertToPdfAndCompare("backgroundColorContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorContentBox1Test() {
            ConvertToPdfAndCompare("backgroundColorContentBox1", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorContentBoxMarginsTest() {
            ConvertToPdfAndCompare("backgroundColorContentBoxMargins", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorDefaultTest() {
            ConvertToPdfAndCompare("backgroundColorDefault", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorPaddingBoxTest() {
            ConvertToPdfAndCompare("backgroundColorPaddingBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageBorderBoxTest() {
            ConvertToPdfAndCompare("backgroundImageBorderBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageColorContentBoxTest() {
            ConvertToPdfAndCompare("backgroundImageColorContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageColorDefaultTest() {
            ConvertToPdfAndCompare("backgroundImageColorDefault", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageContentBoxTest() {
            ConvertToPdfAndCompare("backgroundImageContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageDiffBordersPaddingsTest() {
            ConvertToPdfAndCompare("backgroundImageDiffBordersPaddings", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageDefaultTest() {
            ConvertToPdfAndCompare("backgroundImageDefault", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagePaddingBoxTest() {
            ConvertToPdfAndCompare("backgroundImagePaddingBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundMultiImageTest() {
            ConvertToPdfAndCompare("backgroundMultiImage", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundMultiImageColorTest() {
            ConvertToPdfAndCompare("backgroundMultiImageColor", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundMultiImageColor2Test() {
            ConvertToPdfAndCompare("backgroundMultiImageColor2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundMultiImageColor3Test() {
            ConvertToPdfAndCompare("backgroundMultiImageColor3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorMultipleClipTest() {
            ConvertToPdfAndCompare("backgroundColorMultipleClip", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorBorderRadiusTest() {
            // TODO DEVSIX-4525 border-radius works incorrectly with background-clip
            ConvertToPdfAndCompare("backgroundColorBorderRadius", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageBorderRadiusTest() {
            // TODO DEVSIX-4525 border-radius works incorrectly with background-clip
            ConvertToPdfAndCompare("backgroundImageBorderRadius", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundGradientBorderBoxTest() {
            ConvertToPdfAndCompare("backgroundGradientBorderBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundGradientPaddingBoxTest() {
            ConvertToPdfAndCompare("backgroundGradientPaddingBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundGradientContentBoxTest() {
            ConvertToPdfAndCompare("backgroundGradientContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MultiImageColorCommaTest() {
            ConvertToPdfAndCompare("multiImageColorComma", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MultiImageColorNoCommaTest() {
            ConvertToPdfAndCompare("multiImageColorNoComma", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MultiImageColorNoneCommaTest() {
            ConvertToPdfAndCompare("multiImageColorNoneComma", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MultiImageColorNoneNoCommaTest() {
            ConvertToPdfAndCompare("multiImageColorNoneNoComma", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagePositionContentBoxTest() {
            ConvertToPdfAndCompare("backgroundImagePositionContentBox", sourceFolder, destinationFolder);
        }
    }
}
