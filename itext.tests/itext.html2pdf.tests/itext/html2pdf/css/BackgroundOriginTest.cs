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
    public class BackgroundOriginTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BackgroundOriginTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BackgroundOriginTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundColorContentBoxTest() {
            ConvertToPdfAndCompare("backgroundColorContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundGradientBorderBoxTest() {
            ConvertToPdfAndCompare("backgroundGradientBorderBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundGradientColorTest() {
            ConvertToPdfAndCompare("backgroundGradientColor", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundGradientContentBoxTest() {
            ConvertToPdfAndCompare("backgroundGradientContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundGradientPaddingBoxTest() {
            ConvertToPdfAndCompare("backgroundGradientPaddingBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageBorderBoxTest() {
            ConvertToPdfAndCompare("backgroundImageBorderBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageColorTest() {
            ConvertToPdfAndCompare("backgroundImageColor", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageContentBoxTest() {
            ConvertToPdfAndCompare("backgroundImageContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagePaddingBoxTest() {
            ConvertToPdfAndCompare("backgroundImagePaddingBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageContentBoxCleanTest() {
            ConvertToPdfAndCompare("backgroundImageContentBoxClean", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageNoBorderTest() {
            ConvertToPdfAndCompare("backgroundImageNoBorder", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageNoPaddingsTest() {
            ConvertToPdfAndCompare("backgroundImageNoPaddings", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageBorderTopTest() {
            ConvertToPdfAndCompare("backgroundImageBorderTop", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagePaddingLeftTest() {
            ConvertToPdfAndCompare("backgroundImagePaddingLeft", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientDiffBordersBorderBoxTest() {
            ConvertToPdfAndCompare("linearGradientDiffBordersBorderBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientDiffPaddingsTest() {
            ConvertToPdfAndCompare("linearGradientDiffPaddings", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImagePositionBorderBoxTest() {
            ConvertToPdfAndCompare("imagePositionBorderBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImagePositionPaddingBoxTest() {
            ConvertToPdfAndCompare("imagePositionPaddingBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImagePositionContentBoxTest() {
            ConvertToPdfAndCompare("imagePositionContentBox", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImagePositionNumberValuesTest() {
            ConvertToPdfAndCompare("imagePositionNumberValues", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImageRepeatXTest() {
            ConvertToPdfAndCompare("imageRepeatX", sourceFolder, destinationFolder);
        }
    }
}
