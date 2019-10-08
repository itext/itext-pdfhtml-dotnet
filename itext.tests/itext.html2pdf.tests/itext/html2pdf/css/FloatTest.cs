/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using iText.IO.Util;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Css.Util;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class FloatTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FloatTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FloatTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Float01Test() {
            RunTest("float01Test", "diff01_");
        }

        [NUnit.Framework.Test]
        public virtual void Float02Test() {
            RunTest("float02Test", "diff02_");
        }

        [NUnit.Framework.Test]
        public virtual void Float03Test() {
            RunTest("float03Test", "diff03_");
        }

        [NUnit.Framework.Test]
        public virtual void Float04Test() {
            RunTest("float04Test", "diff04_");
        }

        [NUnit.Framework.Test]
        public virtual void Float05Test() {
            RunTest("float05Test", "diff05_");
        }

        [NUnit.Framework.Test]
        public virtual void Float06Test() {
            RunTest("float06Test", "diff07_");
        }

        [NUnit.Framework.Test]
        public virtual void Float07Test() {
            RunTest("float07Test", "diff08_");
        }

        [NUnit.Framework.Test]
        public virtual void Float08Test() {
            RunTest("float08Test", "diff27_");
        }

        [NUnit.Framework.Test]
        public virtual void Float09Test() {
            RunTest("float09Test", "diff09_");
        }

        [NUnit.Framework.Test]
        public virtual void Float10Test() {
            RunTest("float10Test", "diff10_");
        }

        [NUnit.Framework.Test]
        public virtual void Float11Test() {
            RunTest("float11Test", "diff11_");
        }

        [NUnit.Framework.Test]
        public virtual void Float12Test() {
            RunTest("float12Test", "diff12_");
        }

        [NUnit.Framework.Test]
        public virtual void Float13Test() {
            RunTest("float13Test", "diff13_");
        }

        [NUnit.Framework.Test]
        public virtual void Float14Test() {
            RunTest("float14Test", "diff14_");
        }

        [NUnit.Framework.Test]
        public virtual void Float15Test() {
            RunTest("float15Test", "diff15_");
        }

        [NUnit.Framework.Test]
        public virtual void Float16Test() {
            // TODO DEVSIX-1730: at the moment we always wrap inline text in paragraphs, thus when we process next floating element it's always on next line
            // see also float50Test and float51Test
            // TODO as a possible solution in future we might consider adding floats blocks as inlines-blocks in inline helper
            RunTest("float16Test", "diff16_");
        }

        [NUnit.Framework.Test]
        public virtual void Float17Test() {
            RunTest("float17Test", "diff17_");
        }

        [NUnit.Framework.Test]
        public virtual void Float19Test() {
            RunTest("float19Test", "diff19_");
        }

        [NUnit.Framework.Test]
        public virtual void Float20Test() {
            RunTest("float20Test", "diff20_");
        }

        [NUnit.Framework.Test]
        public virtual void Float21Test() {
            RunTest("float21Test", "diff21_");
        }

        [NUnit.Framework.Test]
        public virtual void Float22Test() {
            RunTest("float22Test", "diff22_");
        }

        [NUnit.Framework.Test]
        public virtual void Float23Test() {
            RunTest("float23Test", "diff23_");
        }

        [NUnit.Framework.Test]
        public virtual void Float24Test() {
            RunTest("float24Test", "diff24_");
        }

        [NUnit.Framework.Test]
        public virtual void Float25Test() {
            // TODO DEVSIX-1730: at the moment we always wrap inline text in paragraphs, thus when we process next floating element it's always on next line
            // see also float50Test and float51Test
            RunTest("float25Test", "diff25_");
        }

        [NUnit.Framework.Test]
        public virtual void Float26Test() {
            RunTest("float26Test", "diff26_");
        }

        [NUnit.Framework.Test]
        public virtual void Float27Test() {
            RunTest("float27Test", "diff27_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float28Test() {
            // TODO DEVSIX-1269
            RunTest("float28Test", "diff28_");
        }

        [NUnit.Framework.Test]
        public virtual void Float29Test() {
            RunTest("float29Test", "diff29_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float30Test() {
            // TODO DEVSIX-1269 and DEVSIX-1270
            RunTest("float30Test", "diff30_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float31Test() {
            // TODO DEVSIX-1269 and DEVSIX-1270
            RunTest("float31Test", "diff31_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float32Test() {
            // TODO DEVSIX-1269
            RunTest("float32Test", "diff32_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float33Test() {
            RunTest("float33Test", "diff33_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float34Test() {
            // TODO DEVSIX-1269
            RunTest("float34Test", "diff34_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float35Test() {
            // TODO DEVSIX-1269
            RunTest("float35Test", "diff35_");
        }

        [NUnit.Framework.Test]
        public virtual void Float36Test() {
            // TODO DEVSIX-1730: at the moment we always wrap inline text in paragraphs, thus when we process next floating element it's always on next line
            // see also float50Test and float51Test
            RunTest("float36Test", "diff36_");
        }

        [NUnit.Framework.Test]
        public virtual void Float37Test() {
            RunTest("float37Test", "diff37_");
        }

        [NUnit.Framework.Test]
        public virtual void Float38Test() {
            RunTest("float38Test", "diff38_");
        }

        [NUnit.Framework.Test]
        public virtual void Float39Test() {
            RunTest("float39Test", "diff39_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1269")]
        public virtual void Float40Test() {
            // TODO DEVSIX-1269
            RunTest("float40Test", "diff40_");
        }

        [NUnit.Framework.Test]
        public virtual void Float41Test() {
            RunTest("float41Test", "diff41_");
        }

        [NUnit.Framework.Test]
        public virtual void Float42Test() {
            RunTest("float42Test", "diff42_");
        }

        [NUnit.Framework.Test]
        public virtual void Float43Test() {
            RunTest("float43Test", "diff43_");
        }

        [NUnit.Framework.Test]
        public virtual void Float44Test() {
            RunTest("float44Test", "diff44_");
        }

        [NUnit.Framework.Test]
        public virtual void Float45Test() {
            RunTest("float45Test", "diff45_");
        }

        [NUnit.Framework.Test]
        public virtual void Float46Test() {
            RunTest("float46Test", "diff46_");
        }

        [NUnit.Framework.Test]
        public virtual void Float47Test() {
            RunTest("float47Test", "diff47_");
        }

        [NUnit.Framework.Test]
        public virtual void Float48Test() {
            RunTest("float48Test", "diff48_");
        }

        [NUnit.Framework.Test]
        public virtual void Float49Test() {
            RunTest("float49Test", "diff49_");
        }

        [NUnit.Framework.Test]
        public virtual void Float50Test() {
            // TODO DEVSIX-1730: at the moment we always wrap inline text in paragraphs, thus we process this test exactly like in float51Test
            RunTest("float50Test", "diff50_");
        }

        [NUnit.Framework.Test]
        public virtual void Float51Test() {
            RunTest("float51Test", "diff51_");
        }

        [NUnit.Framework.Test]
        public virtual void Float54Test() {
            RunTest("float54Test", "diff54_");
        }

        [NUnit.Framework.Test]
        public virtual void Float55Test() {
            RunTest("float55Test", "diff55_");
        }

        [NUnit.Framework.Test]
        public virtual void Float57Test() {
            RunTest("float57Test", "diff57_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1372")]
        public virtual void Float58Test() {
            RunTest("float58Test", "diff58_");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES, Count = 1)]
        public virtual void Float60Test() {
            RunTest("float60Test", "diff60_");
        }

        [NUnit.Framework.Test]
        public virtual void Float61Test() {
            RunTest("float61Test", "diff61_");
        }

        [NUnit.Framework.Test]
        public virtual void Float66Test() {
            RunTest("float66Test", "diff66_");
        }

        [NUnit.Framework.Test]
        public virtual void Float67Test() {
            RunTest("float67Test", "diff67_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables01Test() {
            RunTest("floatAndTables01Test", "diffTables01_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables02Test() {
            RunTest("floatAndTables02Test", "diffTables02_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables03Test() {
            RunTest("floatAndTables03Test", "diffTables03_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables04Test() {
            RunTest("floatAndTables04Test", "diffTables04_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables05Test() {
            RunTest("floatAndTables05Test", "diffTables05_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables06Test() {
            RunTest("floatAndTables06Test", "diffTables06_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables07Test() {
            RunTest("floatAndTables07Test", "diffTables07_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables08Test() {
            RunTest("floatAndTables08Test", "diffTables08_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables09Test() {
            RunTest("floatAndTables09Test", "diffTables09_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatAndTables10Test() {
            RunTest("floatAndTables10Test", "diffTables10_");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1316")]
        public virtual void FloatImage14Test() {
            RunTest("floatImage14Test", "diffImages14_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatInline09Test() {
            // TODO DEVSIX-1269
            RunTest("floatInline09Test", "diffImages09_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatInline17Test() {
            RunTest("floatInline17Test", "diffImages17_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatFixedWidthOverflow01Test() {
            RunTest("floatFixedWidthOverflow01Test", "diffWidthOverflow01_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatFixedWidthOverflow02Test() {
            RunTest("floatFixedWidthOverflow02Test", "diffWidthOverflow02_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatContentOverflow01Test() {
            RunTest("floatContentOverflow01Test", "diffContentOverflow01_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatFixedWidthNested01Test() {
            RunTest("floatFixedWidthNested01Test", "diffWidthOverflowNested01_");
        }

        [NUnit.Framework.Test]
        public virtual void NestedFloat01Test() {
            RunTest("nestedFloat01Test", "diffNested01_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatImageTableTest() {
            // TODO fix cmp file after DEVSIX-1933
            RunTest("floatImageTableTest", "diffFloatImageTableTest_");
        }

        [NUnit.Framework.Test]
        public virtual void NewPageFloatTest() {
            //TODO Test file to be updated in DEVSIX-2231
            RunTest("newPageFloatTest", "diff_newPageFloat_");
        }

        [NUnit.Framework.Test]
        public virtual void FloatingDivBottomBorderTest() {
            //TODO This test should fail after the fix in DEVSIX-2335
            RunTest("floatingDivBottomBorderTest", "diff_BottomBorderTest_");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES, Count = 1)]
        public virtual void FloatCaption01Test() {
            RunTest("floatCaption01Test", "diff_floatCaption01Test_");
        }

        private void RunTest(String testName, String diff) {
            String htmlName = sourceFolder + testName + ".html";
            String outFileName = destinationFolder + testName + ".pdf";
            String cmpFileName = sourceFolder + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(htmlName).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , diff));
        }

        [NUnit.Framework.Test]
        public virtual void ResponsiveIText() {
            PageSize[] pageSizes = new PageSize[] { null, new PageSize(PageSize.A3.GetHeight(), PageSize.A4.GetHeight(
                )), new PageSize(760, PageSize.A4.GetHeight()), new PageSize(PageSize.A5.GetWidth(), PageSize.A4.GetHeight
                ()) };
            String htmlSource = sourceFolder + "responsiveIText.html";
            foreach (PageSize pageSize in pageSizes) {
                float? pxWidth = null;
                if (pageSize != null) {
                    pxWidth = CssUtils.ParseAbsoluteLength(pageSize.GetWidth().ToString());
                }
                String outName = "responsiveIText" + (pxWidth != null ? "_" + (int)(float)pxWidth : "") + ".pdf";
                PdfWriter writer = new PdfWriter(destinationFolder + outName);
                PdfDocument pdfDoc = new PdfDocument(writer);
                ConverterProperties converterProperties = new ConverterProperties();
                if (pageSize != null) {
                    pdfDoc.SetDefaultPageSize(pageSize);
                    MediaDeviceDescription mediaDescription = new MediaDeviceDescription(MediaType.SCREEN);
                    mediaDescription.SetWidth((float)pxWidth);
                    converterProperties.SetMediaDeviceDescription(mediaDescription);
                }
                using (FileStream fileInputStream = new FileStream(htmlSource, FileMode.Open, FileAccess.Read)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, pdfDoc, converterProperties);
                }
                pdfDoc.Close();
            }
            foreach (PageSize pageSize in pageSizes) {
                float? pxWidth = null;
                if (pageSize != null) {
                    pxWidth = CssUtils.ParseAbsoluteLength(pageSize.GetWidth().ToString());
                }
                String outName = "responsiveIText" + (pxWidth != null ? "_" + (int)(float)pxWidth : "") + ".pdf";
                String cmpName = "cmp_" + outName;
                NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + outName, sourceFolder
                     + cmpName, destinationFolder, "diffResponsive_"));
            }
        }
    }
}
