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
    public class BorderTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BorderTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BorderTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border01Test() {
            ConvertToPdfAndCompare("border01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 4)]
        public virtual void Border02Test() {
            ConvertToPdfAndCompare("border02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border03Test() {
            ConvertToPdfAndCompare("border03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border04Test() {
            ConvertToPdfAndCompare("border04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void Border05Test() {
            ConvertToPdfAndCompare("border05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border06Test() {
            ConvertToPdfAndCompare("border06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border07Test() {
            ConvertToPdfAndCompare("border07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border08Test() {
            ConvertToPdfAndCompare("border08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void Border09Test() {
            ConvertToPdfAndCompare("border09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void Border10Test() {
            ConvertToPdfAndCompare("border10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DTest01() {
            ConvertToPdfAndCompare("border3DTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DTest02() {
            ConvertToPdfAndCompare("border3DTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderTransparencyTest01() {
            ConvertToPdfAndCompare("borderTransparencyTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderTransparencyTest02() {
            ConvertToPdfAndCompare("borderTransparencyTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleOverlayingInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleOverlayingInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDoubleValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDoubleValueInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDottedValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDottedValueInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDashedValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDashedValueInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTrDifferentTypesTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTrDifferentTypes", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTheadTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideThead", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTbodyTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideTbody", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTfootTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideTfoot", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInsideTableElementsTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInsideTableElements", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTRLengthUnitsTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTRLengthUnits", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTrColorValuesTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTrColorValues", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-2857 update cmp file after fix
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        public virtual void BorderStyleInTRwithTHTest() {
            ConvertToPdfAndCompare("borderStyleInTRwithTH", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTRSeparateBorderCollapseTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTRSeparateBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TbodyBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("tbodyBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TheadBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("theadBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TfootBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("tfootBorderCollapse", sourceFolder, destinationFolder);
        }
    }
}
