/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class InfiniteLoopTest : ExtendedITextTest {
        private static readonly String SRC = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/InfiniteLoopTest/";

        private static readonly String DEST = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/test/itext/html2pdf/css/InfiniteLoopTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DEST);
        }

        [NUnit.Framework.Test]
        public virtual void TooBigHtmlThrowsTest() {
            String testName = "tooBigHtml";
            DocumentProperties documentProperties = new DocumentProperties();
            documentProperties.RegisterDependency(typeof(LayoutInfiniteLoopResolver), () => new LayoutInfiniteLoopResolver
                (9));
            NUnit.Framework.Assert.Catch(typeof(PdfException), () => HtmlConverter.ConvertToPdf(FileUtil.GetInputStreamForFile
                (SRC + testName + ".html"), new PdfDocument(new PdfWriter(DEST + testName + ".pdf"), documentProperties
                )));
        }
    }
}
