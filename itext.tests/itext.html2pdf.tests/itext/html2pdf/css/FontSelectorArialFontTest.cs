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
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontSelectorArialFontTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontSelectorArialFontTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontSelectorArialFontTest/";

        public const String SOURCE_HTML_NAME = "arialTest";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
            CreateDestinationFolder(sourceFolder);
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
            fontProvider.GetFontSet().AddFont(sourceFolder + "FreeSans.ttf", null, "Arial");
            ConverterProperties converterProperties = new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription
                (MediaType.PRINT)).SetFontProvider(fontProvider);
            RunTest(fileName, converterProperties);
        }

        private void RunTest(String name, ConverterProperties converterProperties) {
            String htmlPath = sourceFolder + SOURCE_HTML_NAME + ".html";
            String pdfPath = destinationFolder + name + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, destinationFolder, diffPrefix
                ));
        }
    }
}
