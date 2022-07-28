/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("Integration test")]
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
