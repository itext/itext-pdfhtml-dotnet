/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
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
    address: sales@itextpdf.com */
using System;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Layout.Font;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Versions.Attributes;
using iText.Kernel;
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

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DroidSerifWebFontTest() {
            RunTest("droidSerifWebFontTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalFontTest() {
            RunTest("droidSerifLocalFontTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalLocalFontTest() {
            RunTest("droidSerifLocalLocalFontTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaFontTest() {
            RunTest("droidSerifLocalWithMediaFontTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaRuleFontTest() {
            RunTest("droidSerifLocalWithMediaRuleFontTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaRuleFontTest2() {
            RunTest("droidSerifLocalWithMediaRuleFontTest2");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        public virtual void FontFaceGrammarTest() {
            RunTest("fontFaceGrammarTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DroidSerifLocalWithMediaRuleFontTest3() {
            String name = "droidSerifLocalWithMediaRuleFontTest";
            String htmlPath = sourceFolder + name + ".html";
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(htmlPath).AbsolutePath + "\n");
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

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FontFaceWoffTest01() {
            RunTest("fontFaceWoffTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1368: font-face alias is not recognized correctly by font selector if it is specified in quotes"
            )]
        public virtual void FontFaceWoffTest02() {
            RunTest("fontFaceWoffTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("contains ttc font")]
        public virtual void FontFaceTtcTest() {
            RunTest("fontFaceTtcTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FontFaceWoff2SimpleTest() {
            RunTest("fontFaceWoff2SimpleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("contains ttc font")]
        public virtual void FontFaceWoff2TtcTest() {
            RunTest("fontFaceWoff2TtcTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void W3cProblemTest01() {
            //TODO: In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
            RunTest("w3cProblemTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-444")]
        public virtual void W3cProblemTest02() {
            //TODO: In w3c test suite this font is labeled as invalid though and its loading failed in browser, though iText parses its as correct one and LOADS!
            RunTest("w3cProblemTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void W3cProblemTest03() {
            //TODO: silently omitted, decompression should fail.
            RunTest("w3cProblemTest03");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void W3cProblemTest04() {
            //TODO: silently omitted, decompression should fail. Browser loads font but don't draw glyph.
            RunTest("w3cProblemTest04");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void W3cProblemTest05() {
            //TODO: In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
            RunTest("w3cProblemTest05");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void W3cProblemTest06() {
            //TODO: In w3c test suite this font is labeled as invalid though it correctly parsers both in browser and iText
            RunTest("w3cProblemTest06");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-444")]
        public virtual void W3cProblemTest07() {
            //TODO: In w3c test suite this font is labeled as invalid though and its loading failed in browser, though iText parses its as correct one and LOADS!
            RunTest("w3cProblemTest07");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void IncorrectFontNameTest() {
            //TODO: browser can load font with @font-face family name different form family name stored in font metadata, but iText should have them equal in order to work.
            RunTest("incorrectFontNameTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String name) {
            String htmlPath = sourceFolder + name + ".html";
            String pdfPath = destinationFolder + name + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(htmlPath).AbsolutePath + "\n");
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
