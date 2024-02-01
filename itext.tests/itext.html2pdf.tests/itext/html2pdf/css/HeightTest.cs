/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
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

        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        [NUnit.Framework.Test]
        public virtual void EmptyTableOnCustomPageSizedDocumentTest() {
            ConvertToPdfAndCompare("emptyTableOnCustomPageSizedDocument", sourceFolder, destinationFolder);
        }
    }
}
