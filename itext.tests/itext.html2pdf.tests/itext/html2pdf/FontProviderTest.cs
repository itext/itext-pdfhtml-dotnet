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
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    // Actually the results are invalid because there is no pdfCalligraph.
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontProviderTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/FontProviderTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/FontProviderTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 4)]
        public virtual void HebrewTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "hebrew.html"), new FileInfo(DESTINATION_FOLDER + 
                "hebrew.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "hebrew.pdf", SOURCE_FOLDER
                 + "cmp_hebrew.pdf", DESTINATION_FOLDER, "diffHebrew_"));
        }

        [NUnit.Framework.Test]
        public virtual void DevanagariTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "devanagari.html"), new FileInfo(DESTINATION_FOLDER
                 + "devanagari.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "devanagari.pdf", SOURCE_FOLDER
                 + "cmp_devanagari.pdf", DESTINATION_FOLDER, "diffDevanagari_"));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertStandardFonts() {
            //For more specific tests see FontSelectorTimesFontTest in html2pdf and FontSelectorHelveticaFontTest in html2pdf-private
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "convertStandardFonts.html"), new FileInfo(DESTINATION_FOLDER
                 + "convertStandardFonts.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "convertStandardFonts.pdf"
                , SOURCE_FOLDER + "cmp_convertStandardFonts.pdf", DESTINATION_FOLDER, "difffontstand_"));
        }

        [NUnit.Framework.Test]
        public virtual void NotoSansMonoItalicTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "notoSansMonoItalic.html"), new FileInfo(DESTINATION_FOLDER
                 + "notoSansMonoItalic.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "notoSansMonoItalic.pdf"
                , SOURCE_FOLDER + "cmp_notoSansMonoItalic.pdf", DESTINATION_FOLDER, "diffnotoSansMonoItalic_"));
        }

        [NUnit.Framework.Test]
        public virtual void NotoSansMonoBoldItalicTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "notoSansMonoBoldItalic.html"), new FileInfo(DESTINATION_FOLDER
                 + "notoSansMonoBoldItalic.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "notoSansMonoBoldItalic.pdf"
                , SOURCE_FOLDER + "cmp_notoSansMonoBoldItalic.pdf", DESTINATION_FOLDER, "diffnotoSansMonoBoldItalic_")
                );
        }

        [NUnit.Framework.Test]
        public virtual void ComparatorErrorTest() {
            // TODO: DEVSIX-4017 (Combination of default and pdfCalligraph fonts with italic style and '"courier new", courier,
            // monospace' family reproduces comparator exception. Update test after fixing.)
            ConverterProperties properties = new ConverterProperties();
            FontProvider pro = new DefaultFontProvider();
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansArabic-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansArabic-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansGurmukhi-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansGurmukhi-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansMyanmar-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansMyanmar-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansOriya-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSansOriya-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifBengali-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifBengali-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifDevanagari-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifDevanagari-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifGujarati-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifGujarati-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifHebrew-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifHebrew-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifKannada-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifKannada-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifKhmer-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifKhmer-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifMalayalam-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifMalayalam-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifMyanmar-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifMyanmar-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifTamil-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifTamil-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifTelugu-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifTelugu-Bold.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifThai-Regular.ttf"));
            pro.AddFont(FontProgramFactory.CreateFont(SOURCE_FOLDER + "NotoSerifThai-Bold.ttf"));
            properties.SetFontProvider(pro);
            bool isExceptionThrown = false;
            try {
                HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "comparatorError.html"), new FileInfo(DESTINATION_FOLDER
                     + "comparatorError.pdf"), properties);
            }
            catch (ArgumentException e) {
                NUnit.Framework.Assert.AreEqual("Comparison method violates its general contract!", e.Message);
                isExceptionThrown = true;
            }
            if (!isExceptionThrown) {
                NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "comparatorError.pdf"
                    , SOURCE_FOLDER + "cmp_comparatorError.pdf", DESTINATION_FOLDER));
            }
        }

        [NUnit.Framework.Test]
        public virtual void DifferentFontFamiliesTest() {
            ConverterProperties properties = new ConverterProperties();
            FontProvider fontProvider = new DefaultFontProvider(false, false, false);
            fontProvider.AddDirectory(SOURCE_FOLDER + "Lato_fonts");
            properties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "differentFontFamilies.html"), new FileInfo(DESTINATION_FOLDER
                 + "differentFontFamilies.pdf"), properties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "differentFontFamilies.pdf"
                , SOURCE_FOLDER + "cmp_differentFontFamilies.pdf", DESTINATION_FOLDER, "diff_"));
        }
    }
}
