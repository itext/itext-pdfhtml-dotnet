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
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Resolve {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlStylesToCssConverterIntegrationTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css" + "/HtmlStylesToCssConverterIntegrationTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css" + "/HtmlStylesToCssConverterIntegrationTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ObjectTagWidthAndHeightTest() {
            ConvertToPdfAndCompare("objectTagWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.URL_IS_NOT_CLOSED_IN_CSS_EXPRESSION
            )]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.URL_IS_EMPTY_IN_CSS_EXPRESSION)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.WAS_NOT_ABLE_TO_DEFINE_BACKGROUND_CSS_SHORTHAND_PROPERTIES
            )]
        [NUnit.Framework.Test]
        public virtual void NotClosedUrlTest() {
            String cmpPdfPath = SOURCE_FOLDER + "cmp_notClosedUrl.pdf";
            String outPdfPath = DESTINATION_FOLDER + "notClosedUrl.pdf";
            String input = "PDF TEST<p></p><div\n" + "style=\"background:url(http://google.com/\">X</div>";
            HtmlConverter.ConvertToPdf(input, new PdfWriter(outPdfPath));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, DESTINATION_FOLDER
                ));
        }
    }
}
