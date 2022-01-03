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
    public class HeightTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/HeightTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/HeightTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest01() {
            ConvertToPdfAndCompare("heightTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest02() {
            ConvertToPdfAndCompare("heightTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest03() {
            ConvertToPdfAndCompare("heightTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest04() {
            ConvertToPdfAndCompare("heightTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest05() {
            ConvertToPdfAndCompare("heightTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1007")]
        public virtual void HeightTest06() {
            ConvertToPdfAndCompare("heightTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest07() {
            ConvertToPdfAndCompare("heightTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest08() {
            ConvertToPdfAndCompare("heightTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest09() {
            ConvertToPdfAndCompare("heightTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest10() {
            ConvertToPdfAndCompare("heightTest10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest11() {
            ConvertToPdfAndCompare("heightTest11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest12() {
            ConvertToPdfAndCompare("heightTest12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest13() {
            ConvertToPdfAndCompare("heightTest13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest14() {
            ConvertToPdfAndCompare("heightTest14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest15() {
            ConvertToPdfAndCompare("heightTest15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest16() {
            ConvertToPdfAndCompare("heightTest16", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest17() {
            ConvertToPdfAndCompare("heightTest17", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest01() {
            ConvertToPdfAndCompare("heightWithCollapsingMarginsTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest02() {
            ConvertToPdfAndCompare("heightWithCollapsingMarginsTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest03() {
            ConvertToPdfAndCompare("heightWithCollapsingMarginsTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest04() {
            // second paragraph should not be drawn in pdf, as it doesn't fit with it's margins
            ConvertToPdfAndCompare("heightWithCollapsingMarginsTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightWithCollapsingMarginsTest05() {
            ConvertToPdfAndCompare("heightWithCollapsingMarginsTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightLargerThanMinHeight01() {
            // TODO DEVSIX-1895: height differs from the browser rendering due to incorrect resolving of max-height/height properties
            ConvertToPdfAndCompare("heightLargerThanMinHeight01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightLesserThanMaxHeight01() {
            // TODO DEVSIX-1895: height differs from the browser rendering due to incorrect resolving of max-height/height properties
            ConvertToPdfAndCompare("heightLesserThanMaxHeight01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeightNumberWithoutUnitTest() {
            // TODO DEVSIX-6078 print log message about invalid height
            ConvertToPdfAndCompare("heightNumberWithoutUnit", sourceFolder, destinationFolder);
        }
    }
}
