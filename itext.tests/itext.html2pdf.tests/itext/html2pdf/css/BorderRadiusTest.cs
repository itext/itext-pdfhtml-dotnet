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
    public class BorderRadiusTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BorderRadiusTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BorderRadiusTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius01Test() {
            ConvertToPdfAndCompare("borderRadiusTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius02Test() {
            ConvertToPdfAndCompare("borderRadiusTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius03Test() {
            ConvertToPdfAndCompare("borderRadiusTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius04Test() {
            ConvertToPdfAndCompare("borderRadiusTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius05Test() {
            ConvertToPdfAndCompare("borderRadiusTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius06Test() {
            ConvertToPdfAndCompare("borderRadiusTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius07Test() {
            ConvertToPdfAndCompare("borderRadiusTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius08Test() {
            ConvertToPdfAndCompare("borderRadiusTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius09Test() {
            ConvertToPdfAndCompare("borderRadiusTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius10Test() {
            ConvertToPdfAndCompare("borderRadiusTest10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius11Test() {
            ConvertToPdfAndCompare("borderRadiusTest11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12Test() {
            ConvertToPdfAndCompare("borderRadiusTest12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12ImageTest() {
            ConvertToPdfAndCompare("borderRadiusTest12Image", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12ATest() {
            ConvertToPdfAndCompare("borderRadiusTest12A", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12BTest() {
            ConvertToPdfAndCompare("borderRadiusTest12B", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius13Test() {
            ConvertToPdfAndCompare("borderRadiusTest13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius14Test() {
            ConvertToPdfAndCompare("borderRadiusTest14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius15Test() {
            ConvertToPdfAndCompare("borderRadiusTest15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius16Test() {
            ConvertToPdfAndCompare("borderRadiusTest16", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius17Test() {
            ConvertToPdfAndCompare("borderRadiusTest17", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius18Test() {
            ConvertToPdfAndCompare("borderRadiusTest18", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadiusInlineElementTest01() {
            ConvertToPdfAndCompare("borderRadiusInlineElementTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBorderRadiusTest01() {
            ConvertToPdfAndCompare("imageBorderRadiusTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadiusInlineSpanElementTest01() {
            //TODO: update after DEVSIX-2018, DEVSIX-1191 closing
            ConvertToPdfAndCompare("borderRadiusInlineSpanElementTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadiusInlineDivElementTest01() {
            //TODO: update after DEVSIX-2018, DEVSIX-1191 closing
            ConvertToPdfAndCompare("borderRadiusInlineDivElementTest01", sourceFolder, destinationFolder);
        }
    }
}
