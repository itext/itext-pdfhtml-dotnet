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
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Logs;
using iText.Svg.Converter;
using iText.Svg.Element;
using iText.Svg.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class SvgTest : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/SvgTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/SvgTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgTest() {
            ConvertAndCompare("inline_svg");
        }

        [NUnit.Framework.Test]
        public virtual void Inline_svg_percent_sizes_quirk_mode_1Test() {
            // TODO DEVSIX-4629 pdfHTML: simulate browsers behavior for quirks and standards modes
            ConvertAndCompare("inline_svg_percent_sizes_quirk_mode_1");
        }

        [NUnit.Framework.Test]
        public virtual void Inline_svg_percent_sizes_quirk_mode_2Test() {
            // TODO DEVSIX-4629 pdfHTML: simulate browsers behavior for quirks and standards modes
            ConvertAndCompare("inline_svg_percent_sizes_quirk_mode_2");
        }

        [NUnit.Framework.Test]
        public virtual void Inline_svg_percent_sizes_quirk_mode_3Test() {
            // TODO DEVSIX-4629 pdfHTML: simulate browsers behavior for quirks and standards modes
            ConvertAndCompare("inline_svg_percent_sizes_quirk_mode_3");
        }

        [NUnit.Framework.Test]
        public virtual void Inline_svg_percent_sizes_standard_mode_1Test() {
            ConvertAndCompare("inline_svg_percent_sizes_standard_mode_1");
        }

        [NUnit.Framework.Test]
        public virtual void Inline_svg_percent_sizes_standard_mode_2Test() {
            ConvertAndCompare("inline_svg_percent_sizes_standard_mode_2");
        }

        [NUnit.Framework.Test]
        public virtual void Inline_svg_percent_sizes_standard_mode_3Test() {
            ConvertAndCompare("inline_svg_percent_sizes_standard_mode_3");
        }

        [NUnit.Framework.Test]
        public virtual void InlineNestedSvgTest() {
            ConvertAndCompare("inline_nested_svg");
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgExternalFontRelativeTest() {
            ConvertAndCompare("inline_svg_external_font_relative");
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgExternalFontUrlTest() {
            ConvertAndCompare("inline_svg_external_font_url");
        }

        [NUnit.Framework.Test]
        public virtual void Convert_inline_Svg_path_in_HTML() {
            ConvertAndCompare("HTML_with_inline_svg_path");
        }

        [NUnit.Framework.Test]
        public virtual void Convert_inline_Svg_polygon_in_HTML() {
            // TODO: DEVSIX-2719 SVG<polygon>: tops of figures are cut
            ConvertAndCompare("HTML_with_inline_svg_polygon");
        }

        [NUnit.Framework.Test]
        public virtual void Convert_namespace_Svg_in_HTML() {
            ConvertAndCompare("namespace_svg");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgCircle() {
            String html = "inline_svg_circle";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"100\" height=\"100\">\n"
                 + "  <circle cx=\"50\" cy=\"50\" r=\"40\" stroke=\"green\" stroke-width=\"4\" fill=\"yellow\" />\n" +
                 "</svg>\n" + "\n" + "</body>\n" + "</html>";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + html + ".pdf", SOURCE_FOLDER
                 + "cmp_" + html + ".pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgRectangle() {
            String html = "inline_svg_rectangle";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"400\" height=\"100\">\n"
                 + "  <rect width=\"400\" height=\"100\" \n" + "  style=\"fill:rgb(0,0,255);stroke-width:10;stroke:rgb(0,0,0)\" />\n"
                 + "Sorry, your browser does not support inline SVG.\n" + "</svg>\n" + " \n" + "</body>\n" + "</html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + html + ".pdf", SOURCE_FOLDER
                 + "cmp_" + html + ".pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgRoundedRectangle() {
            String html = "inline_svg_rounded_rect";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"400\" height=\"180\">\n"
                 + "  <rect x=\"50\" y=\"20\" rx=\"20\" ry=\"20\" width=\"150\" height=\"150\"\n" + "  style=\"fill:red;stroke:black;stroke-width:5;opacity:0.5\" />\n"
                 + "Sorry, your browser does not support inline SVG.\n" + "</svg>\n" + "\n" + "</body>\n" + "</html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + html + ".pdf", SOURCE_FOLDER
                 + "cmp_" + html + ".pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgStar() {
            // TODO: DEVSIX-2719 SVG<polygon>: tops of figures are cut
            String html = "inline_svg_star";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"300\" height=\"200\">\n"
                 + "  <polygon points=\"100,10 40,198 190,78 10,78 160,198\"\n" + "  style=\"fill:lime;stroke:purple;stroke-width:5;fill-rule:evenodd;\" />\n"
                 + "Sorry, your browser does not support inline SVG.\n" + "</svg>\n" + " \n" + "</body>\n" + "</html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + html + ".pdf", SOURCE_FOLDER
                 + "cmp_" + html + ".pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgLogo() {
            String html = "inline_svg_logo";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "  <html>\n" + "  <body>\n" + "\n" + "  <svg height=\"130\" width=\"500\">\n"
                 + "    <defs>\n" + "      <linearGradient id=\"grad1\" x1=\"0%\" y1=\"0%\" x2=\"100%\" y2=\"0%\">\n" 
                + "        <stop offset=\"0%\"\n" + "        style=\"stop-color:rgb(255,255,0);stop-opacity:1\" />\n" 
                + "        <stop offset=\"100%\"\n" + "        style=\"stop-color:rgb(255,0,0);stop-opacity:1\" />\n" 
                + "      </linearGradient>\n" + "    </defs>\n" + "    <ellipse cx=\"100\" cy=\"70\" rx=\"85\" ry=\"55\" fill=\"url(#grad1)\" />\n"
                 + "    <text fill=\"#ffffff\" font-size=\"45\" font-family=\"Verdana\"\n" + "    x=\"50\" y=\"86\">SVG</text>\n"
                 + "  Sorry, your browser does not support inline SVG.\n" + "  </svg>\n" + "\n" + "  </body>\n" + "  </html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + html + ".pdf", SOURCE_FOLDER
                 + "cmp_" + html + ".pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgWithCustomFont() {
            String html = "inline_svg_custom_font";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "  <body>\n" + "    <svg viewBox=\"0 0 240 80\" xmlns=\"http://www.w3.org/2000/svg\">\n"
                 + "      <text x=\"20\" y=\"35\" font-family=\"Noto Sans Mono\" font-size=\"1.5em\">Hello World!</text>\n"
                 + "    </svg>\n" + "  </body>\n" + "</html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + html + ".pdf", SOURCE_FOLDER
                 + "cmp_" + html + ".pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertSvgWithSvgConverterCustomFont() {
            String filename = "svg_custom_font";
            String svg = "<svg viewBox=\"0 0 240 80\" xmlns=\"http://www.w3.org/2000/svg\">\n" + "<text x=\"20\" y=\"35\" font-family=\"Noto Sans Mono\" font-size=\"1.5em\">Hello World!</text>\n"
                 + "</svg>\n";
            Document document = new Document(new PdfDocument(new PdfWriter(DESTINATION_FOLDER + filename + ".pdf")));
            Image image = SvgConverter.ConvertToImage(new MemoryStream(svg.GetBytes(System.Text.Encoding.UTF8)), document
                .GetPdfDocument());
            document.Add(image);
            document.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + filename + ".pdf", SOURCE_FOLDER
                 + "cmp_" + filename + ".pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void ExternalImageSuccessTest() {
            ConvertAndCompare("external_img");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalImageNonExistentRefTest() {
            ConvertAndCompare("external_img_nonExistentRef");
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-3034 Moving the end tag of <object> causes ERROR
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void ExternalObjectSuccessTest() {
            ConvertAndCompare("external_object");
        }

        [NUnit.Framework.Test]
        public virtual void ExternalObjectWithResourceTest() {
            ConvertAndCompare("external_object_with_resource");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 66)]
        public virtual void ExternalObjectWithGoogleCharts() {
            ConvertAndCompare("inlineSvg_googleCharts");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
            )]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalObjectNonExistentRefTest() {
            ConvertAndCompare("external_objectNonExistentRef");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void HtmlWithSvgBackgroundTest() {
            // The diff with browser is expected, because we parse "width: 600;", when
            // browsers ignore such declaration. See htmlWithSvgBackground3Test
            ConvertAndCompare("HTML_with_svg_background");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlWithSvgBackground2Test() {
            ConvertAndCompare("HTML_with_svg_background_2");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlWithSvgBackground3Test() {
            ConvertAndCompare("HTML_with_svg_background_3");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void HtmlWithSvgBackgroundNoViewbox() {
            // The diff with browser is expected, because we parse "width: 600;", when
            // browsers ignore such declaration. See htmlWithSvgBackgroundNoViewbox2
            ConvertAndCompare("Html_with_svg_background_no_viewbox");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlWithSvgBackgroundNoViewbox2() {
            ConvertAndCompare("Html_with_svg_background_no_viewbox_2");
        }

        [NUnit.Framework.Test]
        public virtual void SvgWithoutDimensionsTest() {
            ConvertAndCompare("svg_without_dimensions");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void SvgWithoutDimensionsWithViewboxTest() {
            ConvertAndCompare("svg_without_dimensions_with_viewbox");
        }

        [NUnit.Framework.Test]
        public virtual void SvgWithoutDimensionsImageAndObjectRef() {
            ConvertAndCompare("svgWithoutDimensionsImageAndObjectRef");
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgWithExternalCssTest() {
            ConvertAndCompare("inlineSvgWithExternalCss");
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgStyleResolvingOrder1Test() {
            ConvertAndCompare("inlineSvgStyleResolvingOrder1");
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgStyleResolvingOrder2Test() {
            ConvertAndCompare("inlineSvgStyleResolvingOrder2");
        }

        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.UNMAPPED_TAG, LogLevel = LogLevelConstants.WARN)]
        public virtual void InlineSvgStyleResolvingOrder3Test() {
            ConvertAndCompare("inlineSvgStyleResolvingOrder3");
        }

        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.UNMAPPED_TAG, LogLevel = LogLevelConstants.WARN)]
        public virtual void InlineSvgStyleResolvingOrder4Test() {
            ConvertAndCompare("inlineSvgStyleResolvingOrder4");
        }

        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.UNMAPPED_TAG, LogLevel = LogLevelConstants.WARN)]
        public virtual void InlineSvgStyleResolvingOrder5Test() {
            ConvertAndCompare("inlineSvgStyleResolvingOrder5");
        }

        [NUnit.Framework.Test]
        public virtual void SvgWithEmRemTest() {
            ConvertAndCompare("svgWithEmRem");
        }

        [NUnit.Framework.Test]
        public virtual void SvgRelativeDimenstionsInInlineBlockTest() {
            // TODO DEVSIX-1316 make percent width doesn't affect elements min max width
            ConvertAndCompare("svgRelativeDimenstionsInInlineBlockTest");
        }

        [NUnit.Framework.Test]
        public virtual void SvgRelativeDimenstionsInTableTest() {
            // TODO DEVSIX-1316 make percent width doesn't affect elements min max width
            // TODO DEVSIX-7003 Problem with layouting image with relative size in the table
            ConvertAndCompare("svgRelativeDimenstionsInTableTest");
        }

        [NUnit.Framework.Test]
        public virtual void SvgWidthHeightPercentUnitsTest() {
            ConvertAndCompare("svgWidthHeightPercentUnitsTest");
        }

        [NUnit.Framework.Test]
        public virtual void SvgSimpleWidthHeightPercentUnitsTest() {
            ConvertAndCompare("svgSimpleWidthHeightPercentUnitsTest");
        }

        [NUnit.Framework.Test]
        public virtual void SvgToElementsSimpleWidthHeightPercentUnitsTest() {
            ConvertToElementsAndCompare("svgSimpleWidthHeightPercentUnitsTest");
        }

        [NUnit.Framework.Test]
        public virtual void SvgToLayoutWidthHeightPercentTest() {
            String name = "svgToLayoutWidthHeightPercentTest";
            IList<IElement> elements = HtmlConverter.ConvertToElements(FileUtil.GetInputStreamForFile(SOURCE_FOLDER + 
                name + ".html"));
            Document document = new Document(new PdfDocument(new PdfWriter(DESTINATION_FOLDER + name + ".pdf")));
            SvgImage svgImage = (SvgImage)((BlockElement<Div>)elements[0]).GetChildren()[0];
            Div div = new Div();
            div.SetWidth(100);
            div.SetHeight(300);
            div.SetBorder(new SolidBorder(5));
            div.Add(svgImage);
            document.Add(div);
            document.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + name + ".pdf", SOURCE_FOLDER
                 + "cmp_" + name + ".pdf", DESTINATION_FOLDER, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        public virtual void SvgNestedWidthHeightPercentUnitsTest() {
            ConvertAndCompare("svgNestedWidthHeightPercentUnitsTest");
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgParentWithoutWidthAndHeightTest() {
            ConvertAndCompare("relativeSvgParentWithoutWidthAndHeight");
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgParentIsBoundedTest() {
            ConvertAndCompare("relativeSvgParentIsBounded");
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgParentWithWidthTest() {
            ConvertAndCompare("relativeSvgParentWithWidth");
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgParentWithHeightTest() {
            ConvertAndCompare("relativeSvgParentWithHeight");
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgParentWithWidthViewboxTest() {
            ConvertAndCompare("relativeSvgParentWithWidthViewbox");
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgParentWithHeightViewboxTest() {
            ConvertAndCompare("relativeSvgParentWithHeightViewbox");
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgDifferentGrandparentTest() {
            ConvertAndCompare("relativeSvgDifferentGrandparent");
        }

        private static void ConvertAndCompare(String name) {
            String htmlPath = SOURCE_FOLDER + name + ".html";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(DESTINATION_FOLDER + name + ".pdf"));
            System.Console.Out.WriteLine("Input html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + name + ".pdf", SOURCE_FOLDER
                 + "cmp_" + name + ".pdf", DESTINATION_FOLDER, "diff_" + name + "_"));
        }

        private static void ConvertToElementsAndCompare(String name) {
            IList<IElement> elements = HtmlConverter.ConvertToElements(FileUtil.GetInputStreamForFile(SOURCE_FOLDER + 
                name + ".html"));
            Document document = new Document(new PdfDocument(new PdfWriter(DESTINATION_FOLDER + name + ".pdf")));
            foreach (IElement elem in elements) {
                if (elem is IBlockElement) {
                    document.Add((IBlockElement)elem);
                }
                else {
                    if (elem is Image) {
                        document.Add((Image)elem);
                    }
                    else {
                        if (elem is AreaBreak) {
                            document.Add((AreaBreak)elem);
                        }
                        else {
                            NUnit.Framework.Assert.Fail("The #convertToElements method gave element which is unsupported as root element, it's unexpected."
                                );
                        }
                    }
                }
            }
            document.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + name + ".pdf", SOURCE_FOLDER
                 + "cmp_" + name + ".pdf", DESTINATION_FOLDER, "diff_" + name + "_"));
        }
    }
}
