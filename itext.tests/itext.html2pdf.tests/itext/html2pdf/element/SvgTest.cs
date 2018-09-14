/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using iText.Kernel.Utils;
using iText.Svg.Exceptions;
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

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineSvgTest() {
            String name = "inline_svg";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineNestedSvgTest() {
            String name = "inline_nested_svg";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineSvgExternalFontRelativeTest() {
            String name = "inline_svg_external_font_relative";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineSvgExternalFontUrlTest() {
            // TODO RND-1042 external font loading in SVG via @import
            String name = "inline_svg_external_font_url";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_PARSING_COULD_NOT_MAP_NODE)]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 4)]
        public virtual void ExternalImageSuccessTest() {
            String name = "external_img";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalImageNonExistentRefTest() {
            String name = "external_img_nonExistentRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_PARSING_COULD_NOT_MAP_NODE)]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 4)]
        public virtual void ExternalObjectSuccessTest() {
            String name = "external_object";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalObjectNonExistentRefTest() {
            String name = "external_objectNonExistentRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
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

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.MISSING_WIDTH)]
        [LogMessage(SvgLogMessageConstant.MISSING_HEIGHT)]
        public virtual void SvgWithoutDimensionsWithViewboxTest() {
            String name = "svg_without_dimensions_with_viewbox";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.MISSING_WIDTH, Count = 2)]
        [LogMessage(SvgLogMessageConstant.MISSING_HEIGHT, Count = 2)]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 6)]
        public virtual void SvgWithoutDimensionsImageAndObjectRef() {
            String name = "SvgWithoutDimensionsImageAndObjectRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }
    }
}
