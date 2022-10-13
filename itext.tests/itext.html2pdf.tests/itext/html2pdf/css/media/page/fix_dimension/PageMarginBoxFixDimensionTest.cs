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

namespace iText.Html2pdf.Css.Media.Page.Fix_dimension {
    [NUnit.Framework.Category("Integration test")]
    public class PageMarginBoxFixDimensionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/fix_dimension/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/fix_dimension/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        // Top margin box tests
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPxTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPx", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPtTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPt", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPercent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixInTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixIn", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixCmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixCm", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixMmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixMm", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPcTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPc", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixEmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixEm", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixExTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixEx", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyRightFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyRightFixPercent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyCenterFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyCenterFixPercent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixCenterAndRightTest() {
            ConvertToPdfAndCompare("topFixCenterAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndFixCenterTest() {
            ConvertToPdfAndCompare("topFixLeftAndFixCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndRight() {
            ConvertToPdfAndCompare("topFixLeftAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndFixCenterAndFixRight() {
            ConvertToPdfAndCompare("topFixLeftAndFixCenterAndFixRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixTopTest() {
            ConvertToPdfAndCompare("leftOnlyFixTop", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixCenterTest() {
            ConvertToPdfAndCompare("leftOnlyFixCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixBottomTest() {
            ConvertToPdfAndCompare("leftOnlyFixBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndFixCenterTest() {
            ConvertToPdfAndCompare("leftTopAndFixCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndFixBottomTest() {
            ConvertToPdfAndCompare("leftTopAndFixBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftFixCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftFixCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftFixTopAndFixCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftFixTopAndFixCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PageMarginFont() {
            ConvertToPdfAndCompare("pageMarginFont", sourceFolder, destinationFolder);
        }
    }
}
