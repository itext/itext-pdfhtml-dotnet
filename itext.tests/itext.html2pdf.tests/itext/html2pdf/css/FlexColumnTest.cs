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
    public class FlexColumnTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexColumnTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FlexColumnTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignIItemsCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignIItemsCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentFlexEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentFlexEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentFlexStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentStartMaxSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentStartMaxSize", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentStartMinSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentStartMinSize", sourceFolder, destinationFolder);
        }
    }
}
