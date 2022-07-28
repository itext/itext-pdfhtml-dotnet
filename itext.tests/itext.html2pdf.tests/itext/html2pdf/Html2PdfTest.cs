/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("Integration test")]
    public class Html2PdfTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/Html2PdfTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/Html2PdfTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloWorldParagraphTest() {
            ConvertToPdfAndCompare("hello_paragraph", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphTableTest() {
            ConvertToPdfAndCompare("hello_paragraph_table", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloMalformedDocumentTest() {
            ConvertToPdfAndCompare("hello_malformed", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphJunkSpacesDocumentTest() {
            ConvertToPdfAndCompare("hello_paragraph_junk_spaces", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphNestedInTableDocumentTest() {
            ConvertToPdfAndCompare("hello_paragraph_nested_in_table", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloParagraphWithSpansDocumentTest() {
            ConvertToPdfAndCompare("hello_paragraph_with_span", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HelloDivDocumentTest() {
            ConvertToPdfAndCompare("hello_div", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ABlockInPTagTest() {
            ConvertToPdfAndCompare("aBlockInPTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Base64svgTest() {
            ConvertToPdfAndCompare("objectTag_base64svg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
            , Count = 1)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void HtmlObjectIncorrectBase64Test() {
            ConvertToPdfAndCompare("objectTag_incorrectBase64svg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO: update after DEVSIX-1346
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_IT_S_TEXT_CONTENT, Count = 1)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void HtmlObjectAltTextTest() {
            ConvertToPdfAndCompare("objectTag_altText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void HtmlObjectNestedObjectTest() {
            ConvertToPdfAndCompare("objectTag_nestedTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlImgBase64SVGTest() {
            ConvertToPdfAndCompare("imgTag_base64svg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
