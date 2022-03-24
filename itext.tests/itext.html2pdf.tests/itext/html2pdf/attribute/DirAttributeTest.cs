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
using iText.Test.Attributes;

namespace iText.Html2pdf.Attribute {
    public class DirAttributeTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/DirAttributeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/DirAttributeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        // TODO DEVSIX-5034 Direction of the contents of description list items with dir = "rtl" is wrong
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 8)]
        public virtual void DifferentDirsOfDlsTest() {
            ConvertToPdfAndCompare("differentDirsOfDls", sourceFolder, destinationFolder, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 18)]
        public virtual void DifferentDirsOfOrderedListsTest() {
            ConvertToPdfAndCompare("differentDirsOfOrderedLists", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 18)]
        public virtual void DifferentDirsOfUnorderedListsTest() {
            ConvertToPdfAndCompare("differentDirsOfUnorderedLists", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        //TODO: DEVSIX-2438 html2Pdf: float + rtl works incorrectly for element placement
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void FloatedTableInRtlDocumentTest() {
            ConvertToPdfAndCompare("floatedTableInRtlDocument", sourceFolder, destinationFolder, false);
        }

        [NUnit.Framework.Test]
        //TODO: DEVSIX-2435 Process several elements which do not respect the specified direction
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 4)]
        public virtual void ParagraphsOfDifferentDirsWithImageTest() {
            ConvertToPdfAndCompare("paragraphsOfDifferentDirsWithImage", sourceFolder, destinationFolder, false);
        }

        [NUnit.Framework.Test]
        //TODO: DEVSIX-2435 Process several elements which do not respect the specified direction
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 4)]
        public virtual void RtlDirectionOfLinkTest() {
            ConvertToPdfAndCompare("rtlDirectionOfLink", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        //TODO: DEVSIX-2435 Process several elements which do not respect the specified direction
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 26)]
        public virtual void RtlDirectionOfListInsideListTest() {
            ConvertToPdfAndCompare("rtlDirectionOfListInsideList", sourceFolder, destinationFolder, false);
        }

        [NUnit.Framework.Test]
        //TODO: DEVSIX-2435 Process several elements which do not respect the specified direction
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 4)]
        public virtual void RtlDirectionOfSpanTest() {
            ConvertToPdfAndCompare("rtlDirectionOfSpan", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RtlDir01Test() {
            ConvertToPdfAndCompare("rtlDirTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RtlDir02Test() {
            ConvertToPdfAndCompare("rtlDirTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void SpansOfDifferentDirsInsideParagraphTest() {
            //TODO DEVSIX-2437 dir ltr is ignored in rtl documents
            ConvertToPdfAndCompare("spansOfDifferentDirsInsideParagraph", sourceFolder, destinationFolder, false);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-3069 pdfHTML: RTL tables are not aligned correctly if there is no enough space
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 32)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void TableAlignedToWrongSideInCaseOfNotEnoughSpaceTest() {
            ConvertToPdfAndCompare("tableAlignedToWrongSideInCaseOfNotEnoughSpace", sourceFolder, destinationFolder);
        }
    }
}
