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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalAlignmentTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalAlignmentTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalAlignmentTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest01() {
            // TODO: DEVSIX-3757 ('top' and 'bottom' values are not supported)
            ConvertToPdfAndCompare("verticalAlignmentTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest02() {
            ConvertToPdfAndCompare("verticalAlignmentTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest03() {
            ConvertToPdfAndCompare("verticalAlignmentTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentImgTest() {
            // TODO: DEVSIX-3757 ('top' and 'bottom' values are not supported)
            ConvertToPdfAndCompare("verticalAlignmentImgTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest05() {
            ConvertToPdfAndCompare("verticalAlignmentTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest06() {
            ConvertToPdfAndCompare("verticalAlignmentTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest07() {
            ConvertToPdfAndCompare("verticalAlignmentTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest08() {
            ConvertToPdfAndCompare("verticalAlignmentTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest09() {
            ConvertToPdfAndCompare("verticalAlignmentTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest10() {
            // TODO DEVSIX-3757 interesting thing is that vertical alignment increases line height if needed, however itext doesn't in this case
            ConvertToPdfAndCompare("verticalAlignmentTest10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest11() {
            ConvertToPdfAndCompare("verticalAlignmentTest11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest12() {
            ConvertToPdfAndCompare("verticalAlignmentTest12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest13() {
            ConvertToPdfAndCompare("verticalAlignmentTest13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest14() {
            ConvertToPdfAndCompare("verticalAlignmentTest14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest15() {
            ConvertToPdfAndCompare("verticalAlignmentTest15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentCellTest01() {
            ConvertToPdfAndCompare("verticalAlignmentCellTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentCellTest02() {
            ConvertToPdfAndCompare("verticalAlignmentCellTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentCellTest03() {
            ConvertToPdfAndCompare("verticalAlignmentCellTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VAlignAttributeCellTest01() {
            ConvertToPdfAndCompare("vAlignAttributeCellTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignOnNestedInlines01() {
            ConvertToPdfAndCompare("verticalAlignOnNestedInlines01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignOnNestedInlines02() {
            ConvertToPdfAndCompare("verticalAlignOnNestedInlines02", sourceFolder, destinationFolder);
        }
    }
}
