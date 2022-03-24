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
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class TextPropertiesTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/TextPropertiesTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/TextPropertiesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignTest01() {
            ConvertToPdfAndCompare("textAlignTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignTest02() {
            ConvertToPdfAndCompare("textAlignTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignJustifyTest() {
            ConvertToPdfAndCompare("textAlignJustifyTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void JustifiedTextWithCharSpacingTest() {
            ConvertToPdfAndCompare("justifiedTextWithCharSpacingTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void JustifiedTextWithCharAndWordSpacingTest() {
            ConvertToPdfAndCompare("justifiedTextWithCharAndWordSpacingTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void JustifiedTextWithCharPositiveAndWordSpacingNegativeTest() {
            ConvertToPdfAndCompare("justifiedTextWithCharPositiveAndWordSpacingNegativeTest", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void JustifiedTextWithCharAndWordSpacingNegativeTest() {
            ConvertToPdfAndCompare("justifiedTextWithCharAndWordSpacingNegativeTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.TEXT_DECORATION_BLINK_NOT_SUPPORTED)]
        public virtual void TextDecorationTest01() {
            ConvertToPdfAndCompare("textDecorationTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void LetterSpacingTest01() {
            ConvertToPdfAndCompare("letterSpacingTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void LetterSpacingWithInvalidValuesTest() {
            ConvertToPdfAndCompare("letterSpacingWithInvalidValues", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void WordSpacingTest01() {
            ConvertToPdfAndCompare("wordSpacingTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightTest01() {
            ConvertToPdfAndCompare("lineHeightTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightTest02() {
            ConvertToPdfAndCompare("lineHeightTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightTest03() {
            ConvertToPdfAndCompare("lineHeightTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightInHyperlinkTest() {
            ConvertToPdfAndCompare("lineHeightInHyperlink", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceTest01() {
            ConvertToPdfAndCompare("whiteSpaceTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextTransformTest01() {
            ConvertToPdfAndCompare("textTransformTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextTransform02Test() {
            ConvertToPdfAndCompare("textTransformTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceTest02() {
            ConvertToPdfAndCompare("whiteSpaceTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceTest03() {
            ConvertToPdfAndCompare("whiteSpaceTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void CheckWhiteSpaceCss() {
            //TODO: fix after DEVSIX-2447. To reproduce without error, remove "white-space: pre;" (pre-wrap, pre-line)
            ConvertToPdfAndCompare("checkWhiteSpaceCss", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapBasicTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapBasicTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapBackgroundTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapBackgroundTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapShortTextTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapShortTextTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapShortTextTest02() {
            // TODO DEVSIX-2443: /r itext doesn't collapse and treats as new-line
            ConvertToPdfAndCompare("whiteSpaceNowrapShortTextTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapLongTextTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapLongTextTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapLongTextTest02() {
            // TODO DEVSIX-2443: /r itext doesn't collapse and treats as new-line
            ConvertToPdfAndCompare("whiteSpaceNowrapLongTextTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void WhiteSpaceNowrapTableCellTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapTableCellTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapTableCellTest02() {
            ConvertToPdfAndCompare("whiteSpaceNowrapTableCellTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void WhiteSpaceNowrapTableCellTest03() {
            ConvertToPdfAndCompare("whiteSpaceNowrapTableCellTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapTextAlignTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapTextAlignTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapTextAlignTest02() {
            ConvertToPdfAndCompare("whiteSpaceNowrapTextAlignTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapTextAlignTest03() {
            ConvertToPdfAndCompare("whiteSpaceNowrapTextAlignTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapTextAlignTest04() {
            ConvertToPdfAndCompare("whiteSpaceNowrapTextAlignTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapImageTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapImageTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapImageTest02() {
            ConvertToPdfAndCompare("whiteSpaceNowrapImageTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapImageTest03() {
            ConvertToPdfAndCompare("whiteSpaceNowrapImageTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapImageTest04() {
            ConvertToPdfAndCompare("whiteSpaceNowrapImageTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapInlineBlockTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapInlineBlockTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapInlineBlockTest02() {
            ConvertToPdfAndCompare("whiteSpaceNowrapInlineBlockTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapInlineBlockTest03() {
            // TODO DEVSIX-4600 ignores nowrap on inline elements
            ConvertToPdfAndCompare("whiteSpaceNowrapInlineBlockTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapSequentialTest01() {
            // TODO DEVSIX-4600 ignores nowrap on inline elements
            ConvertToPdfAndCompare("whiteSpaceNowrapSequentialTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapSequentialTest02() {
            // TODO DEVSIX-4600 ignores nowrap on inline elements
            ConvertToPdfAndCompare("whiteSpaceNowrapSequentialTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapSequentialTest03() {
            // TODO DEVSIX-4600 ignores nowrap on inline elements
            ConvertToPdfAndCompare("whiteSpaceNowrapSequentialTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapSequentialTest04() {
            // TODO DEVSIX-4600 ignores nowrap on inline elements
            ConvertToPdfAndCompare("whiteSpaceNowrapSequentialTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapSequentialTest05() {
            // TODO DEVSIX-4600 ignores nowrap on inline elements
            ConvertToPdfAndCompare("whiteSpaceNowrapSequentialTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapSequentialTest06() {
            ConvertToPdfAndCompare("whiteSpaceNowrapSequentialTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceNowrapNestedTest01() {
            ConvertToPdfAndCompare("whiteSpaceNowrapNestedTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest01() {
            ConvertToPdfAndCompare("enspEmspThinspTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest02() {
            ConvertToPdfAndCompare("enspEmspThinspTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest03() {
            ConvertToPdfAndCompare("enspEmspThinspTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest04() {
            ConvertToPdfAndCompare("enspEmspThinspTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest05() {
            ConvertToPdfAndCompare("enspEmspThinspTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest06() {
            ConvertToPdfAndCompare("enspEmspThinspTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest07() {
            ConvertToPdfAndCompare("enspEmspThinspTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EnspEmspThinspTest08() {
            // TODO DEVSIX-1442
            ConvertToPdfAndCompare("enspEmspThinspTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1442")]
        public virtual void EnspEmspThinspTest09() {
            ConvertToPdfAndCompare("enspEmspThinspTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1851")]
        public virtual void WordCharSpacingJustifiedTest01() {
            ConvertToPdfAndCompare("wordCharSpacingJustifiedTest01", sourceFolder, destinationFolder);
        }
    }
}
