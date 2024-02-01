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

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class OptGroupTest : ExtendedHtmlConversionITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/OptGroupTest/";

        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/OptGroupTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupBasicTest01() {
            ConvertToPdfAndCompare("optGroupBasicTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupBasicTest02() {
            ConvertToPdfAndCompare("optGroupBasicTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupEmptyTest01() {
            ConvertToPdfAndCompare("optGroupEmptyTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupNestedTest01() {
            ConvertToPdfAndCompare("optGroupNestedTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupNestedTest02() {
            ConvertToPdfAndCompare("optGroupNestedTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupNoSelectTest01() {
            ConvertToPdfAndCompare("optGroupNoSelectTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupStylesTest01() {
            ConvertToPdfAndCompare("optGroupStylesTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupHeightTest01() {
            ConvertToPdfAndCompare("optGroupHeightTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupWidthTest01() {
            ConvertToPdfAndCompare("optGroupWidthTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupWidthTest02() {
            // TODO DEVSIX-2444 select props parsing essentially neglects whitespace pre
            ConvertToPdfAndCompare("optGroupWidthTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupOverflowTest01() {
            ConvertToPdfAndCompare("optGroupOverflowTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupOverflowTest02() {
            ConvertToPdfAndCompare("optGroupOverflowTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptGroupPseudoTest01() {
            ConvertToPdfAndCompare("optGroupPseudoTest01", sourceFolder, destinationFolder);
        }
    }
}
