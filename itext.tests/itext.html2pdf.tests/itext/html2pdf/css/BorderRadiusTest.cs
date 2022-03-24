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
    public class BorderRadiusTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BorderRadiusTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BorderRadiusTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius01Test() {
            ConvertToPdfAndCompare("borderRadiusTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius02Test() {
            ConvertToPdfAndCompare("borderRadiusTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius03Test() {
            ConvertToPdfAndCompare("borderRadiusTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius04Test() {
            ConvertToPdfAndCompare("borderRadiusTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius05Test() {
            ConvertToPdfAndCompare("borderRadiusTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius06Test() {
            ConvertToPdfAndCompare("borderRadiusTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius07Test() {
            ConvertToPdfAndCompare("borderRadiusTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius08Test() {
            ConvertToPdfAndCompare("borderRadiusTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius09Test() {
            ConvertToPdfAndCompare("borderRadiusTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius10Test() {
            ConvertToPdfAndCompare("borderRadiusTest10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius11Test() {
            ConvertToPdfAndCompare("borderRadiusTest11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12Test() {
            ConvertToPdfAndCompare("borderRadiusTest12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12ImageTest() {
            ConvertToPdfAndCompare("borderRadiusTest12Image", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12ATest() {
            ConvertToPdfAndCompare("borderRadiusTest12A", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius12BTest() {
            ConvertToPdfAndCompare("borderRadiusTest12B", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius13Test() {
            ConvertToPdfAndCompare("borderRadiusTest13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius14Test() {
            ConvertToPdfAndCompare("borderRadiusTest14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius15Test() {
            ConvertToPdfAndCompare("borderRadiusTest15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius16Test() {
            ConvertToPdfAndCompare("borderRadiusTest16", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius17Test() {
            ConvertToPdfAndCompare("borderRadiusTest17", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadius18Test() {
            ConvertToPdfAndCompare("borderRadiusTest18", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadiusInlineElementTest01() {
            ConvertToPdfAndCompare("borderRadiusInlineElementTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBorderRadiusTest01() {
            ConvertToPdfAndCompare("imageBorderRadiusTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadiusInlineSpanElementTest01() {
            //TODO: update after DEVSIX-2018, DEVSIX-1191 closing
            ConvertToPdfAndCompare("borderRadiusInlineSpanElementTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderRadiusInlineDivElementTest01() {
            //TODO: update after DEVSIX-2018, DEVSIX-1191 closing
            ConvertToPdfAndCompare("borderRadiusInlineDivElementTest01", sourceFolder, destinationFolder);
        }
    }
}
