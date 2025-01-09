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
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class DirectionTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/DirectionTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/DirectionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void SimpleLtrDocTest() {
            NUnit.Framework.Assert.IsTrue(GetTextFromDocument(ConvertToHtmlDocument("SimpleLtrDoc"), 1).Contains("123456789."
                ));
        }

        [NUnit.Framework.Test]
        public virtual void SimpleLtrElementDocTest() {
            NUnit.Framework.Assert.IsTrue(GetTextFromDocument(ConvertToHtmlDocument("SimpleLtrElementDoc"), 1).Contains
                ("123456789."));
        }

        //TODO DEVSIX-1920: RTL ignored. Change test after fix
        [NUnit.Framework.Test]
        public virtual void SimpleRtlElementDocTest() {
            NUnit.Framework.Assert.IsFalse(GetTextFromDocument(ConvertToHtmlDocument("SimpleRtlElementDoc"), 1).Contains
                (".Right to left text"));
        }

        //TODO DEVSIX-2437 : Change test after fix
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 4, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void LtrInRtlDocTest() {
            NUnit.Framework.Assert.IsFalse(GetTextFromDocument(ConvertToHtmlDocument("LtrInRtlDoc"), 1).Contains("!Right to left text"
                ));
        }

        //TODO DEVSIX-2437 : Change test after fix
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 4, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void RtlInLtrDocTest() {
            NUnit.Framework.Assert.IsFalse(GetTextFromDocument(ConvertToHtmlDocument("RtlInLtrDoc"), 1).Contains("!Right to left text"
                ));
        }

        //TODO DEVSIX-3069: Change test after fix
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 34, LogLevel = LogLevelConstants
            .WARN)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            1, LogLevel = LogLevelConstants.WARN)]
        public virtual void BigTableTest() {
            ConvertToPdfAndCompare("TooLargeTable", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        private PdfDocument ConvertToHtmlDocument(String fileName) {
            String sourceHtml = SOURCE_FOLDER + fileName + ".html";
            String destPdf = DESTINATION_FOLDER + fileName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceHtml), new FileInfo(destPdf));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceHtml) + "\n" + "Out pdf: "
                 + UrlUtil.GetNormalizedFileUriString(destPdf));
            return new PdfDocument(new PdfReader(destPdf));
        }

        private String GetTextFromDocument(PdfDocument document, int pageNum) {
            return PdfTextExtractor.GetTextFromPage(document.GetPage(pageNum));
        }
    }
}
