/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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

namespace iText.Html2pdf.Css {
    public class OverflowWrapTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/OverflowWrapTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/OverflowWrapTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapCommonScenarioTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapCommonScenario.html"), new FileInfo(destinationFolder
                 + "overflowWrapCommonScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapCommonScenario.pdf"
                , sourceFolder + "cmp_overflowWrapCommonScenario.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowXOverflowWrapTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowXOverflowWrap.html"), new FileInfo(destinationFolder
                 + "overflowXOverflowWrap.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowXOverflowWrap.pdf"
                , sourceFolder + "cmp_overflowXOverflowWrap.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceAndOverflowWrapTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "whiteSpaceAndOverflowWrap.html"), new FileInfo(destinationFolder
                 + "whiteSpaceAndOverflowWrap.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "whiteSpaceAndOverflowWrap.pdf"
                , sourceFolder + "cmp_whiteSpaceAndOverflowWrap.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapAndFloatTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapAndFloat.html"), new FileInfo(destinationFolder
                 + "overflowWrapAndFloat.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapAndFloat.pdf"
                , sourceFolder + "cmp_overflowWrapAndFloat.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 3)]
        public virtual void OverflowWrapTableScenarioTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapTableScenario.html"), new FileInfo(destinationFolder
                 + "overflowWrapTableScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapTableScenario.pdf"
                , sourceFolder + "cmp_overflowWrapTableScenario.pdf", destinationFolder));
        }
    }
}
