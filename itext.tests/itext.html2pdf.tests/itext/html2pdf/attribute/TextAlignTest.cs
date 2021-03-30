/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

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

namespace iText.Html2pdf.Attribute {
    public class TextAlignTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/TextAlignTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/TextAlignTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignLeftTest() {
            ConvertToPdfAndCompare("textAlignLeft", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignRightTest() {
            ConvertToPdfAndCompare("textAlignRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignCenterTest() {
            ConvertToPdfAndCompare("textAlignCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignJustifyTest() {
            ConvertToPdfAndCompare("textAlignJustify", sourceFolder, destinationFolder);
        }
    }
}
