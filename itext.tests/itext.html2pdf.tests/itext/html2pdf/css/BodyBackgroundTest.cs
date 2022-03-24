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

namespace iText.Html2pdf.Css {
    public class BodyBackgroundTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BodyBackgroundTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BodyBackgroundTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithBorderTest() {
            ConvertToPdfAndCompare("bodyBorder", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithHeightTest() {
            ConvertToPdfAndCompare("bodyHeight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithMarginTest() {
            ConvertToPdfAndCompare("bodyMargin", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyWithMarginNegativeTest() {
            ConvertToPdfAndCompare("bodyMarginNegative", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FirstElementHasMarginTest() {
            ConvertToPdfAndCompare("firstElementHasMargin", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MarginNegativeTest() {
            ConvertToPdfAndCompare("marginNegative", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void MultiplePageContentTest() {
            ConvertToPdfAndCompare("multiplePageContent", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PositionAbsoluteTest() {
            ConvertToPdfAndCompare("positionAbsolute", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PositionFixedTest() {
            ConvertToPdfAndCompare("positionFixed", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BordersAndBackgroundsTest() {
            //todo DEVSIX-4732 html borders
            ConvertToPdfAndCompare("bordersAndBackgrounds", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BordersAndBackgroundsWithPagePropertiesTest() {
            //todo DEVSIX-4732 html borders
            ConvertToPdfAndCompare("bordersAndBackgroundsWithPageProperties", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BordersAndBackgroundsWithPageMarksTest() {
            //todo DEVSIX-4732 html borders
            ConvertToPdfAndCompare("bordersAndBackgroundsWithPageMarks", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FloatRightElementTest() {
            ConvertToPdfAndCompare("floatRightElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TransformRotateElementTest() {
            ConvertToPdfAndCompare("transformRotateElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneElementTest() {
            ConvertToPdfAndCompare("displayNoneElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWithFixedHeightElementTest() {
            ConvertToPdfAndCompare("overflowWithFixedHeightElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisibilityHiddenElementTest() {
            ConvertToPdfAndCompare("visibilityHiddenElement", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PagePropertyWithBackgroundImageTest() {
            ConvertToPdfAndCompare("pagePropertyBackgroundImage", sourceFolder, destinationFolder);
        }
    }
}
