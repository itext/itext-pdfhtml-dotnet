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
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class BoxSizingTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BoxSizingTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BoxSizingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentTest01() {
            ConvertToPdfAndCompare("boxSizingCellContentTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentTest02() {
            ConvertToPdfAndCompare("boxSizingCellContentTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellContentImgTest() {
            ConvertToPdfAndCompare("boxSizingCellContentImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest01() {
            // TODO: DEVSIX-5468 update cmp file after fixing
            ConvertToPdfAndCompare("boxSizingCellTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest02() {
            ConvertToPdfAndCompare("boxSizingCellTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingCellTest03() {
            // TODO: DEVSIX-5468 update cmp file after fixing
            ConvertToPdfAndCompare("boxSizingCellTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFloat01Test() {
            ConvertToPdfAndCompare("boxSizingFloat01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFloat02Test() {
            ConvertToPdfAndCompare("boxSizingFloat02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth01Test() {
            ConvertToPdfAndCompare("boxSizingRelativeWidth01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth02Test() {
            ConvertToPdfAndCompare("boxSizingRelativeWidth02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingRelativeWidth03Test() {
            ConvertToPdfAndCompare("boxSizingRelativeWidth03Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv01Test() {
            ConvertToPdfAndCompare("boxSizingDiv01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDivWithImgTest() {
            ConvertToPdfAndCompare("boxSizingDivWithImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv03Test() {
            ConvertToPdfAndCompare("boxSizingDiv03Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingDiv04Test() {
            // Inner div still doesn't fit, because it's height is increased every time page split occurs by margins
            // borders padding. Thus, if parent height was manually fixed to include child with fixed height and if
            // page split occurs - child might not fit.
            ConvertToPdfAndCompare("boxSizingDiv04Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingPara01Test() {
            ConvertToPdfAndCompare("boxSizingPara01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingParaWithImgTest() {
            ConvertToPdfAndCompare("boxSizingParaWithImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingPara03Test() {
            ConvertToPdfAndCompare("boxSizingPara03Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 2)]
        public virtual void BoxSizingTable01Test() {
            ConvertToPdfAndCompare("boxSizingTable01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable02Test() {
            ConvertToPdfAndCompare("boxSizingTable02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTableWithImgTest() {
            ConvertToPdfAndCompare("boxSizingTableWithImg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable04Test() {
            ConvertToPdfAndCompare("boxSizingTable04Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable05Test() {
            ConvertToPdfAndCompare("boxSizingTable05Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingTable06Test() {
            ConvertToPdfAndCompare("boxSizingTable06Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingMinMaxHeight01Test() {
            ConvertToPdfAndCompare("boxSizingMinMaxHeight01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingInlineBlock01Test() {
            ConvertToPdfAndCompare("boxSizingInlineBlock01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingInlineBlock02Test() {
            ConvertToPdfAndCompare("boxSizingInlineBlock02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest01() {
            ConvertToPdfAndCompare("boxSizingFormTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest02() {
            ConvertToPdfAndCompare("boxSizingFormTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingFormTest03() {
            // At least in chrome, borders of buttons are always included to width and height (just as with border-box)
            ConvertToPdfAndCompare("boxSizingFormTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingLiTest01() {
            ConvertToPdfAndCompare("boxSizingLiTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingLiTest02() {
            ConvertToPdfAndCompare("boxSizingLiTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BoxSizingImageTest() {
            ConvertToPdfAndCompare("boxSizingImage", sourceFolder, destinationFolder);
        }
    }
}
