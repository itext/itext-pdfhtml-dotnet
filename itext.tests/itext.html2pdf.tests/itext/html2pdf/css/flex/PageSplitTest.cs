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

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class PageSplitTest : ExtendedHtmlConversionITextTest {
        // TODO DEVSIX-9473 Fix issues on page split
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/PageSplitTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/PageSplitTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void MixedSizesTest() {
            // TODO DEVSIX-9477 Fix issue on page split
            ConvertToPdfAndCompare("mixedSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignItemsFlexEndTest() {
            ConvertToPdfAndCompare("alignItemsFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentFlexStartTest() {
            ConvertToPdfAndCompare("alignContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentFlexStartWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentFlexStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentFlexEndTest() {
            ConvertToPdfAndCompare("alignContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentFlexEndWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentFlexEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentCenterTest() {
            ConvertToPdfAndCompare("alignContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentCenterWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentCenterWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentStartTest() {
            ConvertToPdfAndCompare("alignContentStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentStartWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentEndTest() {
            ConvertToPdfAndCompare("alignContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentEndWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentStretchTest() {
            ConvertToPdfAndCompare("alignContentStretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentStretchWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentStretchWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("alignContentSpaceBetween", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentSpaceBetweenWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentSpaceBetweenWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("alignContentSpaceAround", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentSpaceAroundWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentSpaceAroundWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("alignContentSpaceEvenly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AlignContentSpaceEvenlyWrapReverseTest() {
            ConvertToPdfAndCompare("alignContentSpaceEvenlyWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentFlexStartTest() {
            ConvertToPdfAndCompare("rowRevAlignContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentFlexStartWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentFlexStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentFlexEndTest() {
            ConvertToPdfAndCompare("rowRevAlignContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentFlexEndWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentFlexEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentCenterTest() {
            ConvertToPdfAndCompare("rowRevAlignContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentCenterWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentCenterWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentStartTest() {
            ConvertToPdfAndCompare("rowRevAlignContentStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentStartWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentEndTest() {
            ConvertToPdfAndCompare("rowRevAlignContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentEndWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentStretchTest() {
            ConvertToPdfAndCompare("rowRevAlignContentStretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentStretchWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentStretchWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("rowRevAlignContentSpaceBetween", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentSpaceBetweenWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentSpaceBetweenWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("rowRevAlignContentSpaceAround", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentSpaceAroundWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentSpaceAroundWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("rowRevAlignContentSpaceEvenly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowRevAlignContentSpaceEvenlyWrapReverseTest() {
            ConvertToPdfAndCompare("rowRevAlignContentSpaceEvenlyWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentFlexStartTest() {
            ConvertToPdfAndCompare("colAlignContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentFlexStartWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentFlexStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentFlexEndTest() {
            ConvertToPdfAndCompare("colAlignContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentFlexEndWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentFlexEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentCenterTest() {
            ConvertToPdfAndCompare("colAlignContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentCenterWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentCenterWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentStartTest() {
            ConvertToPdfAndCompare("colAlignContentStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentStartWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentEndTest() {
            ConvertToPdfAndCompare("colAlignContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentEndWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentStretchTest() {
            ConvertToPdfAndCompare("colAlignContentStretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentStretchWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentStretchWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("colAlignContentSpaceBetween", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentSpaceBetweenWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentSpaceBetweenWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("colAlignContentSpaceAround", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentSpaceAroundWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentSpaceAroundWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("colAlignContentSpaceEvenly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColAlignContentSpaceEvenlyWrapReverseTest() {
            ConvertToPdfAndCompare("colAlignContentSpaceEvenlyWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentFlexStartTest() {
            ConvertToPdfAndCompare("colRevAlignContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentFlexStartWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentFlexStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentFlexEndTest() {
            ConvertToPdfAndCompare("colRevAlignContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentFlexEndWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentFlexEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentCenterTest() {
            ConvertToPdfAndCompare("colRevAlignContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentCenterWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentCenterWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentStartTest() {
            ConvertToPdfAndCompare("colRevAlignContentStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentStartWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentStartWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentEndTest() {
            ConvertToPdfAndCompare("colRevAlignContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentEndWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentEndWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentStretchTest() {
            ConvertToPdfAndCompare("colRevAlignContentStretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentStretchWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentStretchWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("colRevAlignContentSpaceBetween", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentSpaceBetweenWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentSpaceBetweenWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("colRevAlignContentSpaceAround", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentSpaceAroundWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentSpaceAroundWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("colRevAlignContentSpaceEvenly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColRevAlignContentSpaceEvenlyWrapReverseTest() {
            ConvertToPdfAndCompare("colRevAlignContentSpaceEvenlyWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
