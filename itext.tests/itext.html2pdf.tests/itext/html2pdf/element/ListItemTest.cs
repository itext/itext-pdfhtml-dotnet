/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
Authors: iText Software.

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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class ListItemTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ListItemTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ListItemTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 14)]
        public virtual void RtlListItemInsideLtrOrderedListTest() {
            String name = "rtlListItemInsideLtrOrderedListTest";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void ListItemWithDifferentDirAndPositionInsideTest() {
            String name = "listItemWithDifferentDirAndPositionInsideTest";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 12)]
        public virtual void RtlListItemInsideLtrUnorderedListTest() {
            String name = "rtlListItemInsideLtrUnorderedListTest";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 12)]
        public virtual void DrawBulletRtlTest() {
            String name = "drawBulletRtl";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void DrawBulletLtrTest() {
            String name = "drawBulletLtr";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 8)]
        public virtual void BulletsAreNotDrawnAsTheyAreInPageMarginsTest() {
            String name = "bulletsAreNotDrawnAsTheyAreInPageMargins";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 20)]
        public virtual void RltListItemWithDifferentMarginsTest() {
            String name = "rltListItemWithDifferentMargins";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void DiffListItemsInsideDiffListsWithDiffDirectionsWithoutWidthTest() {
            String name = "diffListItemsInsideDiffListsWithDiffDirectionsWithoutWidth";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void ListItemWithBlockDisplayTest() {
            String name = "listItemWithBlockDisplay";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }
    }
}
