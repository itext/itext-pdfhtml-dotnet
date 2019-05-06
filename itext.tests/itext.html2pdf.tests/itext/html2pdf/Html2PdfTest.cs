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
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    public class Html2PdfTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/Html2PdfTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/Html2PdfTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloWorldParagraphTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_paragraph.html"), new FileInfo(destinationFolder
                 + "hello_paragraph.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "hello_paragraph.html"
                ).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_paragraph.pdf"
                , sourceFolder + "cmp_hello_paragraph.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloParagraphTableTest() {
            // TODO DEVSIX-1124
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_paragraph_table.html"), new FileInfo(destinationFolder
                 + "hello_paragraph_table.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "hello_paragraph_table.html"
                ).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_paragraph_table.pdf"
                , sourceFolder + "cmp_hello_paragraph_table.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloMalformedDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_malformed.html"), new FileInfo(destinationFolder
                 + "hello_malformed.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "hello_malformed.html"
                ).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_malformed.pdf"
                , sourceFolder + "cmp_hello_malformed.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloParagraphJunkSpacesDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_paragraph_junk_spaces.html"), new FileInfo(destinationFolder
                 + "hello_paragraph_junk_spaces.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "hello_paragraph_junk_spaces.html"
                ).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_paragraph_junk_spaces.pdf"
                , sourceFolder + "cmp_hello_paragraph_junk_spaces.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloParagraphNestedInTableDocumentTest() {
            // TODO DEVSIX-1124
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_paragraph_nested_in_table.html"), new FileInfo
                (destinationFolder + "hello_paragraph_nested_in_table.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "hello_paragraph_nested_in_table.html"
                ).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_paragraph_nested_in_table.pdf"
                , sourceFolder + "cmp_hello_paragraph_nested_in_table.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloParagraphWithSpansDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_paragraph_with_span.html"), new FileInfo(destinationFolder
                 + "hello_paragraph_with_span.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "hello_paragraph_with_span.html"
                ).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_paragraph_with_span.pdf"
                , sourceFolder + "cmp_hello_paragraph_with_span.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HelloDivDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_div.html"), new FileInfo(destinationFolder +
                 "hello_div.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "hello_div.html").AbsolutePath
                 + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_div.pdf", sourceFolder
                 + "cmp_hello_div.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ABlockInPTagTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "aBlockInPTag.html"), new FileInfo(destinationFolder
                 + "aBlockInPTag.pdf"));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceFolder + "aBlockInPTag.html"
                ).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "aBlockInPTag.pdf", sourceFolder
                 + "cmp_aBlockInPTag.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 8)]
        public virtual void Base64svgTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "objectTag_base64svg.html"), new FileInfo(destinationFolder
                 + "objectTag_base64svg.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "objectTag_base64svg.pdf"
                , sourceFolder + "cmp_objectTag_base64svg.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI, Count = 
            1)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void HtmlObjectIncorrectBase64Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "objectTag_incorrectBase64svg.html"), new FileInfo(
                destinationFolder + "objectTag_incorrectBase64svg.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "objectTag_incorrectBase64svg.pdf"
                , sourceFolder + "cmp_objectTag_incorrectBase64svg.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_IT_S_TEXT_CONTENT, Count = 1)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void HtmlObjectAltTextTest() {
            //update after DEVSIX-1346
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "objectTag_altText.html"), new FileInfo(destinationFolder
                 + "objectTag_altText.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "objectTag_altText.pdf"
                , sourceFolder + "cmp_objectTag_altText.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 8)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void HtmlObjectNestedObjectTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "objectTag_nestedTag.html"), new FileInfo(destinationFolder
                 + "objectTag_nestedTag.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "objectTag_nestedTag.pdf"
                , sourceFolder + "cmp_objectTag_nestedTag.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 8)]
        public virtual void HtmlImgBase64SVGTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imgTag_base64svg.html"), new FileInfo(destinationFolder
                 + "imgTag_base64svg.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imgTag_base64svg.pdf"
                , sourceFolder + "cmp_imgTag_base64svg.pdf", destinationFolder, "diff01_"));
        }
    }
}
