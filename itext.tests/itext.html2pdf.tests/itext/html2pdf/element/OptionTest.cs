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
using iText.Forms.Logs;
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class OptionTest : ExtendedHtmlConversionITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/OptionTest/";

        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/OptionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionBasicTest01() {
            ConvertToPdfAndCompare("optionBasicTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionBasicTest02() {
            ConvertToPdfAndCompare("optionBasicTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionEmptyTest01() {
            ConvertToPdfAndCompare("optionEmptyTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(FormsLogMessageConstants.DUPLICATE_EXPORT_VALUE, Count = 1)]
        public virtual void OptionLabelValueTest01() {
            ConvertToPdfAndCompare("optionLabelValueTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionStylesTest01() {
            ConvertToPdfAndCompare("optionStylesTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionStylesTest02() {
            ConvertToPdfAndCompare("optionStylesTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionHeightTest01() {
            ConvertToPdfAndCompare("optionHeightTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionWidthTest01() {
            ConvertToPdfAndCompare("optionWidthTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionOverflowTest01() {
            ConvertToPdfAndCompare("optionOverflowTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionOverflowTest02() {
            ConvertToPdfAndCompare("optionOverflowTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionPseudoTest01() {
            ConvertToPdfAndCompare("optionPseudoTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OptionPseudoTest02() {
            ConvertToPdfAndCompare("optionPseudoTest02", sourceFolder, destinationFolder);
        }
    }
}
