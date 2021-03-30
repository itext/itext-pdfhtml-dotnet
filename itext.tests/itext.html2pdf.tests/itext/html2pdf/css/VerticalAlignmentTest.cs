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

namespace iText.Html2pdf.Css {
    public class VerticalAlignmentTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalAlignmentTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalAlignmentTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest01() {
            // TODO: DEVSIX-3757 ('top' and 'bottom' values are not supported)
            ConvertToPdfAndCompare("verticalAlignmentTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest02() {
            ConvertToPdfAndCompare("verticalAlignmentTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest03() {
            ConvertToPdfAndCompare("verticalAlignmentTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentImgTest() {
            // TODO: DEVSIX-3757 ('top' and 'bottom' values are not supported)
            ConvertToPdfAndCompare("verticalAlignmentImgTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest05() {
            ConvertToPdfAndCompare("verticalAlignmentTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest06() {
            ConvertToPdfAndCompare("verticalAlignmentTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest07() {
            ConvertToPdfAndCompare("verticalAlignmentTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest08() {
            ConvertToPdfAndCompare("verticalAlignmentTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest09() {
            ConvertToPdfAndCompare("verticalAlignmentTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest10() {
            // TODO interesting thing is that vertical alignment increases line height if needed, however itext doesn't in this case 
            ConvertToPdfAndCompare("verticalAlignmentTest10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest11() {
            ConvertToPdfAndCompare("verticalAlignmentTest11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentTest12() {
            ConvertToPdfAndCompare("verticalAlignmentTest12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1750")]
        public virtual void VerticalAlignmentTest13() {
            ConvertToPdfAndCompare("verticalAlignmentTest13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1750")]
        public virtual void VerticalAlignmentTest14() {
            ConvertToPdfAndCompare("verticalAlignmentTest14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1750")]
        public virtual void VerticalAlignmentTest15() {
            ConvertToPdfAndCompare("verticalAlignmentTest15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentCellTest01() {
            ConvertToPdfAndCompare("verticalAlignmentCellTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentCellTest02() {
            ConvertToPdfAndCompare("verticalAlignmentCellTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignmentCellTest03() {
            ConvertToPdfAndCompare("verticalAlignmentCellTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VAlignAttributeCellTest01() {
            ConvertToPdfAndCompare("vAlignAttributeCellTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignOnNestedInlines01() {
            ConvertToPdfAndCompare("verticalAlignOnNestedInlines01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalAlignOnNestedInlines02() {
            ConvertToPdfAndCompare("verticalAlignOnNestedInlines02", sourceFolder, destinationFolder);
        }
    }
}
