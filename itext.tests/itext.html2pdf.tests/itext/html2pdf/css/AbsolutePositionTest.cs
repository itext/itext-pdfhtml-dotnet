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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("Integration test")]
    public class AbsolutePositionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/AbsolutePositionTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/AbsolutePositionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition01Test() {
            ConvertToPdfAndCompare("absolutePositionTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1616: Absolute position for elements that break across pages is not supported"
            )]
        public virtual void AbsolutePosition02Test() {
            ConvertToPdfAndCompare("absolutePositionTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition03Test() {
            ConvertToPdfAndCompare("absolutePositionTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition04Test() {
            ConvertToPdfAndCompare("absolutePositionTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition05Test() {
            ConvertToPdfAndCompare("absolutePositionTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition06Test() {
            ConvertToPdfAndCompare("absolutePositionTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition07Test() {
            ConvertToPdfAndCompare("absolutePositionTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition08Test() {
            ConvertToPdfAndCompare("absolutePositionTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED, Count = 1)]
        public virtual void AbsolutePosition09Test() {
            ConvertToPdfAndCompare("absolutePositionTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition10Test() {
            ConvertToPdfAndCompare("absolutePositionTest10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition11Test() {
            ConvertToPdfAndCompare("absolutePositionTest11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition12Test() {
            ConvertToPdfAndCompare("absolutePositionTest12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition13Test() {
            ConvertToPdfAndCompare("absolutePositionTest13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition14Test() {
            ConvertToPdfAndCompare("absolutePositionTest14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition15Test() {
            ConvertToPdfAndCompare("absolutePositionTest15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest16() {
            ConvertToPdfAndCompare("absolutePositionTest16", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest17() {
            ConvertToPdfAndCompare("absolutePositionTest17", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Ignore("DEVSIX-1818")]
        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest18() {
            ConvertToPdfAndCompare("absolutePositionTest18", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosNoTopBottomTest01() {
            // TODO DEVSIX-1950
            ConvertToPdfAndCompare("absPosNoTopBottomTest01", sourceFolder, destinationFolder);
        }
    }
}
