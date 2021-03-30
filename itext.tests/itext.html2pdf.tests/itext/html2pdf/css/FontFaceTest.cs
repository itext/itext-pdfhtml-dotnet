/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.StyledXmlParser.Css.Media;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class FontFaceTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontFaceTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontFaceTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
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
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
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
            NUnit.Framework.Assert.AreEqual(Html2PdfException.FontProviderContainsZeroFonts, exception, "Font Provider with zero fonts shall fail"
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
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_FONT)]
        public virtual void FontFaceTtcTest() {
            RunTest("fontFaceTtcTest");
        }

        [NUnit.Framework.Test]
        public virtual void FontFaceWoff2SimpleTest() {
            RunTest("fontFaceWoff2SimpleTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_FONT)]
        public virtual void FontFaceWoff2TtcTest() {
            RunTest("fontFaceWoff2TtcTest");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest01() {
            //TODO: In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
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
            //TODO: silently omitted, decompression should fail.
            //See HeaderFlavor001Test in io for decompression details
            RunTest("w3cProblemTest03");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.FONT_SUBSET_ISSUE)]
        public virtual void W3cProblemTest04() {
            //TODO: silently omitted, decompression should fail. Browser loads font but don't draw glyph.
            //See HeaderFlavor002Test in io for decompression details
            //NOTE, iText fails on subsetting as expected.
            RunTest("w3cProblemTest04");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest05() {
            //TODO: In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
            //See HeaderReserved001Test in io for decompression details
            RunTest("w3cProblemTest05");
        }

        [NUnit.Framework.Test]
        public virtual void W3cProblemTest06() {
            //TODO: In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
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
        public virtual void CorrectUrlWithUsedUnicodeRangeTest() {
            //TODO: update after DEVSIX-2052
            RunTest("correctUrlWithUsedUnicodeRangeTest");
        }

        [NUnit.Framework.Test]
        public virtual void CorrectUnicodeRangeSignificantTest() {
            //TODO: update after DEVSIX-2052
            RunTest("correctUnicodeRangeSignificantTest");
        }

        private void RunTest(String name) {
            String htmlPath = sourceFolder + name + ".html";
            String pdfPath = destinationFolder + name + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            ConverterProperties converterProperties = new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription
                (MediaType.PRINT)).SetFontProvider(new DefaultFontProvider());
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), converterProperties);
            NUnit.Framework.Assert.IsFalse(converterProperties.GetFontProvider().GetFontSet().Contains("droid serif"), 
                "Temporary font was found.");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, destinationFolder, diffPrefix
                ));
        }
    }
}
