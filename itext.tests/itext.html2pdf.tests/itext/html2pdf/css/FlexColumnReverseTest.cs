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
    public class FlexColumnReverseTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexColumnReverseTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FlexColumnReverseTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignIItemsCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignIItemsCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentCenter", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentFlexEnd", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentFlexStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentStart", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentFlexEnd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentFlexStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentStart", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentStartMaxSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentStartMaxSize", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentStartMinSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentStartMinSize", sourceFolder, destinationFolder);
        }
    }
}
