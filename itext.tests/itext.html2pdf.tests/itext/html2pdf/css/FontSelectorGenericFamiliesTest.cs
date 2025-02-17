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
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontSelectorGenericFamiliesTest : ExtendedITextTest {
        //TODO(DEVSIX-1034): serif, sans-serif font families are not supported
        //TODO(DEVSIX-1036): cursive, fantasy, system-ui font-families are not supported
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontSelectorGenericFamiliesTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontSelectorGenericFamiliesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void StandardFontsTest() {
            RunTest("standardFonts", new BasicFontProvider(true, false, false));
        }

        [NUnit.Framework.Test]
        public virtual void EmbeddedFontsTest() {
            RunTest("embeddedFonts", new BasicFontProvider(false, true, false));
        }

        public virtual void RunTest(String testName, FontProvider fontProvider) {
            String outPdf = destinationFolder + testName + ".pdf";
            String cmpPdf = sourceFolder + "cmp_" + testName + ".pdf";
            String srcHtml = sourceFolder + "genericFontFamilies.html";
            ConverterProperties properties = new ConverterProperties().SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(srcHtml), new FileInfo(outPdf), properties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff_"
                 + testName + "_"));
        }
    }
}
