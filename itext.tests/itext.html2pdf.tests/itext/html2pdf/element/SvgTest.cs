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
using iText.Html2pdf.Logs;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Svg.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class SvgTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/SvgTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/SvgTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgTest() {
            String name = "inline_svg";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        public virtual void InlineNestedSvgTest() {
            String name = "inline_nested_svg";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgExternalFontRelativeTest() {
            String name = "inline_svg_external_font_relative";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgExternalFontUrlTest() {
            // TODO DEVSIX-2264 external font loading in SVG via @import
            String name = "inline_svg_external_font_url";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void Convert_inline_Svg_path_in_HTML() {
            String name = "HTML_with_inline_svg_path";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void Convert_inline_Svg_polygon_in_HTML() {
            // TODO: Update cmp_ file when DEVSIX-2719 resolved
            String name = "HTML_with_inline_svg_polygon";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void Convert_namespace_Svg_in_HTML() {
            String name = "namespace_svg";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgCircle() {
            String html = "inline_svg_circle";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"100\" height=\"100\">\n"
                 + "  <circle cx=\"50\" cy=\"50\" r=\"40\" stroke=\"green\" stroke-width=\"4\" fill=\"yellow\" />\n" +
                 "</svg>\n" + "\n" + "</body>\n" + "</html>";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + html + ".pdf", sourceFolder
                 + "cmp_" + html + ".pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgRectangle() {
            String html = "inline_svg_rectangle";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"400\" height=\"100\">\n"
                 + "  <rect width=\"400\" height=\"100\" \n" + "  style=\"fill:rgb(0,0,255);stroke-width:10;stroke:rgb(0,0,0)\" />\n"
                 + "Sorry, your browser does not support inline SVG.\n" + "</svg>\n" + " \n" + "</body>\n" + "</html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + html + ".pdf", sourceFolder
                 + "cmp_" + html + ".pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgRoundedRectangle() {
            String html = "inline_svg_rounded_rect";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"400\" height=\"180\">\n"
                 + "  <rect x=\"50\" y=\"20\" rx=\"20\" ry=\"20\" width=\"150\" height=\"150\"\n" + "  style=\"fill:red;stroke:black;stroke-width:5;opacity:0.5\" />\n"
                 + "Sorry, your browser does not support inline SVG.\n" + "</svg>\n" + "\n" + "</body>\n" + "</html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + html + ".pdf", sourceFolder
                 + "cmp_" + html + ".pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgStar() {
            // TODO: Update cmp_ file when DEVSIX-2719 resolved
            String html = "inline_svg_star";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "<html>\n" + "<body>\n" + "\n" + "<svg width=\"300\" height=\"200\">\n"
                 + "  <polygon points=\"100,10 40,198 190,78 10,78 160,198\"\n" + "  style=\"fill:lime;stroke:purple;stroke-width:5;fill-rule:evenodd;\" />\n"
                 + "Sorry, your browser does not support inline SVG.\n" + "</svg>\n" + " \n" + "</body>\n" + "</html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + html + ".pdf", sourceFolder
                 + "cmp_" + html + ".pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertInlineSvgLogo() {
            String html = "inline_svg_logo";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + html + ".pdf"));
            pdfDoc.AddNewPage();
            String string_file = "<!DOCTYPE html>\n" + "  <html>\n" + "  <body>\n" + "\n" + "  <svg height=\"130\" width=\"500\">\n"
                 + "    <defs>\n" + "      <linearGradient id=\"grad1\" x1=\"0%\" y1=\"0%\" x2=\"100%\" y2=\"0%\">\n" 
                + "        <stop offset=\"0%\"\n" + "        style=\"stop-color:rgb(255,255,0);stop-opacity:1\" />\n" 
                + "        <stop offset=\"100%\"\n" + "        style=\"stop-color:rgb(255,0,0);stop-opacity:1\" />\n" 
                + "      </linearGradient>\n" + "    </defs>\n" + "    <ellipse cx=\"100\" cy=\"70\" rx=\"85\" ry=\"55\" fill=\"url(#grad1)\" />\n"
                 + "    <text fill=\"#ffffff\" font-size=\"45\" font-family=\"Verdana\"\n" + "    x=\"50\" y=\"86\">SVG</text>\n"
                 + "  Sorry, your browser does not support inline SVG.\n" + "  </svg>\n" + "\n" + "  </body>\n" + "  </html>\n";
            HtmlConverter.ConvertToPdf(string_file, pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + html + ".pdf", sourceFolder
                 + "cmp_" + html + ".pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ExternalImageSuccessTest() {
            String name = "external_img";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalImageNonExistentRefTest() {
            String name = "external_img_nonExistentRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        //TODO update after DEVSIX-3034
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void ExternalObjectSuccessTest() {
            String name = "external_object";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        public virtual void ExternalObjectWithResourceTest() {
            //TODO update after DEVSIX-2239
            String name = "external_object_with_resource";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 66)]
        public virtual void ExternalObjectWithGoogleCharts() {
            //TODO update after DEVSIX-2239
            String name = "inlineSvg_googleCharts";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
            )]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalObjectNonExistentRefTest() {
            String name = "external_objectNonExistentRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        //TODO: Update cmp_ file when DEVSIX-2731 resolved
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void HtmlWithSvgBackground() {
            String name = "HTML_with_svg_background";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        //TODO: Update cmp_ file when DEVSIX-2731 resolved
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void HtmlWithSvgBackgroundNoViewbox() {
            String name = "Html_with_svg_background_no_viewbox";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.MISSING_WIDTH)]
        [LogMessage(SvgLogMessageConstant.MISSING_HEIGHT)]
        public virtual void SvgWithoutDimensionsTest() {
            String name = "svg_without_dimensions";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        public virtual void SvgWithoutDimensionsWithViewboxTest() {
            String name = "svg_without_dimensions_with_viewbox";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.MISSING_WIDTH, Count = 2)]
        [LogMessage(SvgLogMessageConstant.MISSING_HEIGHT, Count = 2)]
        public virtual void SvgWithoutDimensionsImageAndObjectRef() {
            String name = "svgWithoutDimensionsImageAndObjectRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }
    }
}
