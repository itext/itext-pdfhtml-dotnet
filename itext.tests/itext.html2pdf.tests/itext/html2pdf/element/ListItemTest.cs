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
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ListItemTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ListItemTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ListItemTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 14)]
        public virtual void RtlListItemInsideLtrOrderedListTest() {
            ConvertToPdfAndCompare("rtlListItemInsideLtrOrderedListTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void ListItemWithDifferentDirAndPositionInsideTest() {
            ConvertToPdfAndCompare("listItemWithDifferentDirAndPositionInsideTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 12)]
        public virtual void RtlListItemInsideLtrUnorderedListTest() {
            ConvertToPdfAndCompare("rtlListItemInsideLtrUnorderedListTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 12)]
        public virtual void DrawBulletRtlTest() {
            ConvertToPdfAndCompare("drawBulletRtl", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void DrawBulletLtrTest() {
            ConvertToPdfAndCompare("drawBulletLtr", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 8)]
        public virtual void BulletsAreNotDrawnAsTheyAreInPageMarginsTest() {
            ConvertToPdfAndCompare("bulletsAreNotDrawnAsTheyAreInPageMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 20)]
        public virtual void RltListItemWithDifferentMarginsTest() {
            ConvertToPdfAndCompare("rltListItemWithDifferentMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void DiffListItemsInsideDiffListsWithDiffDirectionsWithoutWidthTest() {
            ConvertToPdfAndCompare("diffListItemsInsideDiffListsWithDiffDirectionsWithoutWidth", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ListItemWithBlockDisplayTest() {
            ConvertToPdfAndCompare("listItemWithBlockDisplay", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
