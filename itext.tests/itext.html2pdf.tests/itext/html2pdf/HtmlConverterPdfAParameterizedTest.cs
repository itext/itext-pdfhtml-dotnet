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
using System.Collections.Generic;
using System.IO;
using iText.Commons.Utils;
using iText.Kernel.Pdf;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterPdfAParameterizedTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/HtmlConverterPdfAParameterizedTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterPdfAParameterizedTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        // TODO DEVSIX-2449 z-index is not supported (zindex.html)
        public static IEnumerable<Object[]> RotationRelatedProperties() {
            return JavaUtil.ArraysAsList(new Object[][] { new Object[] { "images.html", "pdfA4BasicImageTest", PdfAConformance
                .PDF_A_4 }, new Object[] { "imageJpeg2000.html", "pdfA4Jpeg2000Test", PdfAConformance.PDF_A_4 }, new Object
                [] { "basicForms.html", "pdfA4FormsTest", PdfAConformance.PDF_A_4 }, new Object[] { "basicTable.html", 
                "pdfA4TableTest", PdfAConformance.PDF_A_4 }, new Object[] { "basicOutlines.html", "pdfA4OutlinesTest", 
                PdfAConformance.PDF_A_4 }, new Object[] { "opacity.html", "pdfA4OpacityTest", PdfAConformance.PDF_A_4 }
                , new Object[] { "svg.html", "pdfA4SvgTest", PdfAConformance.PDF_A_4 }, new Object[] { "float.html", "pdfA4FloatTest"
                , PdfAConformance.PDF_A_4 }, new Object[] { "flex.html", "pdfA4FlexTest", PdfAConformance.PDF_A_4 }, new 
                Object[] { "list.html", "pdfA4ListTest", PdfAConformance.PDF_A_4 }, new Object[] { "borderTransparency.html"
                , "pdfA4BorderTransparencyTest", PdfAConformance.PDF_A_4 }, new Object[] { "positionAbsolute.html", "pdfA4PositionAbsoluteTest"
                , PdfAConformance.PDF_A_4 }, new Object[] { "positionAbsoluteOpacity.html", "pdfA4PositionAbsoluteOpacityTest"
                , PdfAConformance.PDF_A_4 }, new Object[] { "zIndex.html", "pdfA4ZIndexTest", PdfAConformance.PDF_A_4 }
                , new Object[] { "images.html", "pdfA3BasicImageTest", PdfAConformance.PDF_A_3U }, new Object[] { "imageJpeg2000.html"
                , "pdfA3Jpeg2000Test", PdfAConformance.PDF_A_3U }, new Object[] { "basicForms.html", "pdfA3FormsTest", 
                PdfAConformance.PDF_A_3U }, new Object[] { "basicTable.html", "pdfA3TableTest", PdfAConformance.PDF_A_3U
                 }, new Object[] { "basicOutlines.html", "pdfA3OutlinesTest", PdfAConformance.PDF_A_3U }, new Object[]
                 { "opacity.html", "pdfA3Opacity", PdfAConformance.PDF_A_3U }, new Object[] { "svg.html", "pdfA3SvgTest"
                , PdfAConformance.PDF_A_3U }, new Object[] { "float.html", "pdfA3FloatTest", PdfAConformance.PDF_A_3U }
                , new Object[] { "flex.html", "pdfA3FlexTest", PdfAConformance.PDF_A_3U }, new Object[] { "list.html", 
                "pdfA3ListTest", PdfAConformance.PDF_A_3U }, new Object[] { "borderTransparency.html", "pdfA3BorderTransparencyTest"
                , PdfAConformance.PDF_A_3U }, new Object[] { "positionAbsolute.html", "pdfA3PositionAbsoluteTest", PdfAConformance
                .PDF_A_3U }, new Object[] { "positionAbsoluteOpacity.html", "pdfA3PositionAbsoluteOpacityTest", PdfAConformance
                .PDF_A_3U }, new Object[] { "zIndex.html", "pdfA3ZIndexTest", PdfAConformance.PDF_A_3U }, new Object[]
                 { "images.html", "pdfA2BasicImageTest", PdfAConformance.PDF_A_2B }, new Object[] { "imageJpeg2000.html"
                , "pdfA2Jpeg2000Test", PdfAConformance.PDF_A_2B }, new Object[] { "basicForms.html", "pdfA2FormsTest", 
                PdfAConformance.PDF_A_2B }, new Object[] { "basicTable.html", "pdfA2TableTest", PdfAConformance.PDF_A_2B
                 }, new Object[] { "basicOutlines.html", "pdfA2OutlinesTest", PdfAConformance.PDF_A_2B }, new Object[]
                 { "opacity.html", "pdfA2OpacityTest", PdfAConformance.PDF_A_2B }, new Object[] { "svg.html", "pdfA2SvgTest"
                , PdfAConformance.PDF_A_2B }, new Object[] { "float.html", "pdfA2FloatTest", PdfAConformance.PDF_A_2B }
                , new Object[] { "flex.html", "pdfA2FlexTest", PdfAConformance.PDF_A_2B }, new Object[] { "list.html", 
                "pdfA2ListTest", PdfAConformance.PDF_A_2B }, new Object[] { "borderTransparency.html", "pdfA2BorderTransparencyTest"
                , PdfAConformance.PDF_A_2B }, new Object[] { "positionAbsolute.html", "pdfA2PositionAbsoluteTest", PdfAConformance
                .PDF_A_2B }, new Object[] { "positionAbsoluteOpacity.html", "pdfA2PositionAbsoluteOpacityTest", PdfAConformance
                .PDF_A_2B }, new Object[] { "zIndex.html", "pdfA2ZIndexTest", PdfAConformance.PDF_A_2B }, new Object[]
                 { "imageJpeg2000.html", "pdfA1Jpeg2000Test", PdfAConformance.PDF_A_1B }, new Object[] { "basicForms.html"
                , "pdfA1FormsTest", PdfAConformance.PDF_A_1B }, new Object[] { "basicTable.html", "pdfA1TableTest", PdfAConformance
                .PDF_A_1B }, new Object[] { "basicOutlines.html", "pdfA1OutlinesTest", PdfAConformance.PDF_A_1B }, new 
                Object[] { "svg.html", "pdfA1SvgTest", PdfAConformance.PDF_A_1B }, new Object[] { "float.html", "pdfA1FloatTest"
                , PdfAConformance.PDF_A_1B }, new Object[] { "flex.html", "pdfA1FlexTest", PdfAConformance.PDF_A_1B }, 
                new Object[] { "list.html", "pdfA1ListTest", PdfAConformance.PDF_A_1B }, new Object[] { "positionAbsolute.html"
                , "pdfA1PositionAbsoluteTest", PdfAConformance.PDF_A_1B }, new Object[] { "zIndex.html", "pdfA1ZIndexTest"
                , PdfAConformance.PDF_A_1B } });
        }

        [NUnit.Framework.TestCaseSource("RotationRelatedProperties")]
        public virtual void ConvertToPdfA4Test(Object htmlName, Object testName, PdfAConformance conformanceLevel) {
            String sourceHtml = SOURCE_FOLDER + htmlName;
            String destinationPdf = DESTINATION_FOLDER + testName + ".pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_" + testName + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            converterProperties.SetPdfAConformance(conformanceLevel);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new FileStream(destinationPdf, FileMode.Create), converterProperties
                    );
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }
    }
}
