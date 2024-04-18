/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Logs;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.Layout.Font.Selectorstrategy;
using iText.StyledXmlParser.Css.Media;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontFaceTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontFaceTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontFaceTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_FONT)]
        [NUnit.Framework.Test]
        public virtual void EmptyFontDefinitionTest() {
            RunTest("emptyWebFontCssTest");
        }

        [NUnit.Framework.Test]
        public virtual void DroidSerifWebFontTest() {
            RunTest("droidSerifWebFontTest");
        }

        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalFontTest() {
            RunTest("droidSerifLocalFontTest");
        }

        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalLocalFontTest() {
            RunTest("droidSerifLocalLocalFontTest");
        }

        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaFontTest() {
            RunTest("droidSerifLocalWithMediaFontTest");
        }

        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaRuleFontTest() {
            RunTest("droidSerifLocalWithMediaRuleFontTest");
        }

        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaRuleFontTest2() {
            RunTest("droidSerifLocalWithMediaRuleFontTest2");
        }

        [NUnit.Framework.Test]
        public virtual void FontSelectorTest01() {
            RunTest("fontSelectorTest01");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        public virtual void FontFaceGrammarTest() {
            RunTest("fontFaceGrammarTest");
        }

        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaRuleFontTest3() {
            String name = "droidSerifLocalWithMediaRuleFontTest";
            String htmlPath = sourceFolder + name + ".html";
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            ConverterProperties converterProperties = new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription
                (MediaType.PRINT)).SetFontProvider(new FontProvider());
            String exception = null;
            try {
                HtmlConverter.ConvertToPdf(htmlPath, new MemoryStream(), converterProperties);
            }
            catch (Exception e) {
                exception = e.Message;
            }
            NUnit.Framework.Assert.AreEqual(Html2PdfException.FONT_PROVIDER_CONTAINS_ZERO_FONTS, exception, "Font Provider with zero fonts shall fail"
                );
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceWoffTest01() {
            RunTest("fontFaceWoffTest01");
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceWoffTest02() {
            RunTest("fontFaceWoffTest02");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_FONT)]
        public virtual void FontFaceTtcTest() {
            RunTest("fontFaceTtcTest");
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceWoff2SimpleTest() {
            RunTest("fontFaceWoff2SimpleTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_FONT)]
        public virtual void FontFaceWoff2TtcTest() {
            RunTest("fontFaceWoff2TtcTest");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest01() {
            //In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
            //See BlocksMetadataPadding001Test in io for decompression details
            RunTest("w3cProblemTest01");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest02() {
            try {
                RunTest("w3cProblemTest02");
            }
            catch (OverflowException) {
                return;
            }
            NUnit.Framework.Assert.Fail("In w3c test suite this font is labeled as invalid, " + "so the invalid negative value is expected while creating a glyph."
                );
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest03() {
            //Silently omitted, decompression should fail.
            //See HeaderFlavor001Test in io for decompression details
            RunTest("w3cProblemTest03");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.FONT_SUBSET_ISSUE)]
        public virtual void W3cProblemTest04() {
            //Silently omitted, decompression should fail. Browser loads font but don't draw glyph.
            //See HeaderFlavor002Test in io for decompression details
            //NOTE, iText fails on subsetting as expected.
            RunTest("w3cProblemTest04");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest05() {
            //In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
            //See HeaderReserved001Test in io for decompression details
            RunTest("w3cProblemTest05");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest06() {
            //In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
            //See TabledataHmtxTransform003Test in io for decompression details
            RunTest("w3cProblemTest06");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest07() {
            try {
                RunTest("w3cProblemTest07");
            }
            catch (OverflowException) {
                return;
            }
            NUnit.Framework.Assert.Fail("In w3c test suite this font is labeled as invalid, " + "so the invalid negative value is expected while creating a glyph."
                );
        }

        [NUnit.Framework.Test]
        public virtual void IncorrectFontNameTest01() {
            RunTest("incorrectFontNameTest01");
        }

        [NUnit.Framework.Test]
        public virtual void IncorrectFontNameTest02() {
            RunTest("incorrectFontNameTest02");
        }

        [NUnit.Framework.Test]
        public virtual void IncorrectFontNameTest03() {
            //Checks that font used in previous two files is correct
            RunTest("incorrectFontNameTest03");
        }

        [NUnit.Framework.Test]
        public virtual void IncorrectFontNameTest04() {
            RunTest("incorrectFontNameTest04");
        }

        [NUnit.Framework.Test]
        public virtual void CannotProcessSpecifiedFontTest01() {
            RunTest("cannotProcessSpecifiedFontTest01");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1759")]
        public virtual void FontFamilyTest01() {
            RunTest("fontFamilyTest01");
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceFontWeightTest() {
            //TODO DEVSIX-2122
            RunTest("fontFaceFontWeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceFontWeightWrongTest() {
            //TODO DEVSIX-2122
            RunTest("fontFaceFontWeightWrongWeightsTest");
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceFontWeightInvalidTest() {
            //TODO DEVSIX-2122
            RunTest("fontFaceFontWeightInvalidWeightsTest");
        }

        [NUnit.Framework.Test]
        public virtual void TexFonts01() {
            RunTest("texFonts01");
        }

        [NUnit.Framework.Test]
        public virtual void CorrectUrlWithNotUsedUnicodeRangeTest() {
            //TODO: update/refactor after DEVSIX-2054 fix
            RunTest("correctUrlWithNotUsedUnicodeRangeTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_FONT)]
        public virtual void DoNotDownloadUnusedFontTest() {
            // TODO DEVSIX-2054
            RunTest("doNotDownloadUnusedFontTest");
        }

        [NUnit.Framework.Test]
        public virtual void CorrectUrlWithUsedUnicodeRangeTest() {
            FontProvider fontProvider = new DefaultFontProvider();
            fontProvider.SetFontSelectorStrategyFactory(new BestMatchFontSelectorStrategy.BestMatchFontSelectorStrategyFactory
                ());
            RunTest("correctUrlWithUsedUnicodeRangeTest", fontProvider);
        }

        [NUnit.Framework.Test]
        public virtual void CorrectUnicodeRangeSignificantTest() {
            FontProvider fontProvider = new DefaultFontProvider();
            fontProvider.SetFontSelectorStrategyFactory(new BestMatchFontSelectorStrategy.BestMatchFontSelectorStrategyFactory
                ());
            RunTest("correctUnicodeRangeSignificantTest", fontProvider);
        }

        [NUnit.Framework.Test]
        public virtual void OverwrittenUnicodeRangeTextInLineTest() {
            RunTest("overwrittenUnicodeRangeTextInLineTest");
        }

        [NUnit.Framework.Test]
        public virtual void OverwrittenUnicodeRangeTextInSomeLinesTest() {
            RunTest("overwrittenUnicodeRangeTextInSomeLinesTest");
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceWithUnicodeRangeTest() {
            RunTest("fontFaceWithUnicodeRangeTest");
        }

        [NUnit.Framework.Test]
        public virtual void IncorrectUnicodeRangesTest() {
            RunTest("incorrectUnicodeRangesTest");
        }

        [NUnit.Framework.Test]
        public virtual void UnusedFontWithUnicodeRangeTest() {
            RunTest("unusedFontWithUnicodeRangeTest");
        }

        private void RunTest(String name, FontProvider fontProvider) {
            String htmlPath = sourceFolder + name + ".html";
            String pdfPath = destinationFolder + name + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            ConverterProperties converterProperties = new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription
                (MediaType.PRINT)).SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), converterProperties);
            NUnit.Framework.Assert.IsFalse(converterProperties.GetFontProvider().GetFontSet().Contains("droid serif"), 
                "Temporary font was found.");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, destinationFolder, diffPrefix
                ));
        }

        private void RunTest(String name) {
            RunTest(name, new DefaultFontProvider());
        }
    }
}
