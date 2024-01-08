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
    public class LabelTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/LabelTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/LabelTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Label01Test() {
            ConvertToPdfAndCompare("labelTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LabelDisplayBlock01Test() {
            ConvertToPdfAndCompare("labelDisplayBlockTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LabelDisplayBlock02Test() {
            ConvertToPdfAndCompare("labelDisplayBlockTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LabelBackground01Test() {
            ConvertToPdfAndCompare("labelBackground01Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LabelInlineBlockRelativeWidthTest() {
            ConvertToPdfAndCompare("labelInlineBlockRelativeWidthTest", sourceFolder, destinationFolder);
        }
    }
}
