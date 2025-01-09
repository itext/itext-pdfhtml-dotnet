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
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class RelativeCssPathTest : ExtendedITextTest {
        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/RelativeCssPathTest/";

        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/RelativeCssPathTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeCssPath01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "css_relative.html"), new FileInfo(DESTINATION_FOLDER
                 + "css_relative.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "css_relative.pdf", 
                SOURCE_FOLDER + "cmp_css_relative.pdf", DESTINATION_FOLDER, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void RelativeCssPath02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "css_relative_base64.html"), new FileInfo(DESTINATION_FOLDER
                 + "css_relative_base64.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "css_relative_base64.pdf"
                , SOURCE_FOLDER + "cmp_css_relative_base64.pdf", DESTINATION_FOLDER, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void RelativeImportsTest() {
            RunTestFromRoot("relativeImports");
        }

        [NUnit.Framework.Test]
        public virtual void ExternalCssLoopTest() {
            RunTestFromRoot("externalCssLoop");
        }

        [NUnit.Framework.Test]
        public virtual void TwoExternalCssTest() {
            RunTestFromRoot("twoExternalCss");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI, LogLevel = LogLevelConstants
            .ERROR)]
        public virtual void WrongNestedExternalCssTest() {
            RunTestFromRoot("wrongNestedExternalCss");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.IMPORT_MUST_COME_BEFORE, LogLevel
             = LogLevelConstants.WARN)]
        public virtual void StyleBeforeImportTest() {
            RunTestFromRoot("styleBeforeImport");
        }

        private void RunTestFromRoot(String testName) {
            String html = SOURCE_FOLDER + "root/html/" + testName + ".html";
            String pdf = DESTINATION_FOLDER + testName + ".pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(html), new FileInfo(pdf), new ConverterProperties().SetBaseUri(SOURCE_FOLDER
                 + "root/html/"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdf, cmpPdf, DESTINATION_FOLDER));
        }
    }
}
