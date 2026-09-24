/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Kernel.Pdf;

namespace iText.Html2pdf.Css.Verticaltext {
    [NUnit.Framework.Category("IntegrationTest")]
    public class CombineUprightTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/verticaltext/CombineUprightTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/verticaltext/CombineUprightTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CombineRunLengthsTest() {
            Compare("combineRunLengths");
        }

        [NUnit.Framework.Test]
        public virtual void CombineOrientationsTest() {
            Compare("combineOrientations");
        }

        [NUnit.Framework.Test]
        public virtual void CombineInheritanceTest() {
            Compare("combineInheritance");
        }

        [NUnit.Framework.Test]
        public virtual void CombineWrappingTest() {
            Compare("combineWrapping");
        }

        [NUnit.Framework.Test]
        public virtual void CombineInlineStylesTest() {
            Compare("combineInlineStyles");
        }

        [NUnit.Framework.Test]
        public virtual void CombineHorizontalTest() {
            Compare("combineHorizontal");
        }

        private void Compare(String name) {
            ConvertToPdfAndCompare(name, SOURCE_FOLDER, DESTINATION_FOLDER);
            using (PdfDocument pdf = new PdfDocument(new PdfReader(DESTINATION_FOLDER + name + ".pdf"))) {
                NUnit.Framework.Assert.AreEqual(1, pdf.GetNumberOfPages(), name + " should fit on one A4 page");
            }
        }
    }
}
