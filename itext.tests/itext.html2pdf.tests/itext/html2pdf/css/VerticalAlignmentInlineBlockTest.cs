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
    public class VerticalAlignmentInlineBlockTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalAlignmentInlineBlockTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalAlignmentInlineBlockTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBaselineAlignmentTest() {
            ConvertToPdfAndCompare("baseline", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBaselineAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("baseline-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBottomAlignmentTest() {
            ConvertToPdfAndCompare("bottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBottomAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("bottom-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMiddleAlignmentTest() {
            ConvertToPdfAndCompare("middle", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMiddleAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("middle-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckTopAlignmentTest() {
            ConvertToPdfAndCompare("top", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckTopAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("top-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMixedAlignmentTest() {
            ConvertToPdfAndCompare("mixed", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMixedAlignment2Test() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("mixed2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMixedAlignment3Test() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("mixed3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckCustomerExampleTest() {
            ConvertToPdfAndCompare("customerExample", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckSingleImageTest() {
            ConvertToPdfAndCompare("singleimage", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckElementsInDivAlignmentTest() {
            ConvertToPdfAndCompare("ElementsInDiv", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckSpanAlignmentTest() {
            ConvertToPdfAndCompare("span", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckStyledElementsAlignmentTest() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("styleAlignment", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckUnorderedListAlignmentTest() {
            ConvertToPdfAndCompare("unorderedList", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OrderedListAlignmentTest() {
            ConvertToPdfAndCompare("orderedList", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableAlignmentTest() {
            ConvertToPdfAndCompare("table", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonAlignmentTest() {
            ConvertToPdfAndCompare("button", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FormAlignmentTest() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("form", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderAlignmentTest() {
            ConvertToPdfAndCompare("header", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphAlignmentTest() {
            ConvertToPdfAndCompare("paragraph", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AllStylesTest() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("AllStyles", sourceFolder, destinationFolder);
        }
    }
}
