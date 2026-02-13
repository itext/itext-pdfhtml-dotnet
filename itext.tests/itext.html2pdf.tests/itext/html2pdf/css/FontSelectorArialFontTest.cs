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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontSelectorArialFontTest : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontSelectorArialFontTest/";

        private static readonly String FONT_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontSelectorArialFontTest/";

        public const String SOURCE_HTML_NAME = "arialTest";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
            CreateDestinationFolder(SOURCE_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TestArial() {
            String fileName = "testArial";
            ConverterProperties converterProperties = new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription
                (MediaType.PRINT)).SetFontProvider(new BasicFontProvider());
            RunTest(fileName, converterProperties);
        }

        [NUnit.Framework.Test]
        public virtual void TestArialWithHelveticaAsAnAlias() {
            String fileName = "testArialWithHelveticaAsAnAlias";
            FontProvider fontProvider = new BasicFontProvider();
            fontProvider.GetFontSet().AddFont(FONT_FOLDER + "FreeSans.ttf", null, "Arial");
            ConverterProperties converterProperties = new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription
                (MediaType.PRINT)).SetFontProvider(fontProvider);
            RunTest(fileName, converterProperties);
        }

        private void RunTest(String name, ConverterProperties converterProperties) {
            String htmlPath = SOURCE_FOLDER + SOURCE_HTML_NAME + ".html";
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            String cmpPdfPath = SOURCE_FOLDER + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, DESTINATION_FOLDER, 
                diffPrefix));
        }
    }
}
