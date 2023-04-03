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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css.Media.Page {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlPageMarginBoxImageSizeTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/HtmlPageMarginBoxImageSizeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/HtmlPageMarginBoxImageSizeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void InitDestinationFolder() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BigImagesInTopAndBottomPageMarginsTest() {
            String outPdf = destinationFolder + "topAndBottomPageMargins.pdf";
            String cmpPdf = sourceFolder + "cmp_topAndBottomPageMargins.pdf";
            String htmlSource = sourceFolder + "topAndBottomPageMargins.html";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlSource), new FileInfo(outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void BigImagesInRightAndLeftPageMarginsTest() {
            String outPdf = destinationFolder + "leftAndRightPageMargins.pdf";
            String cmpPdf = sourceFolder + "cmp_leftAndRightPageMargins.pdf";
            String htmlSource = sourceFolder + "leftAndRightPageMargins.html";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlSource), new FileInfo(outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff_"
                ));
        }
    }
}
