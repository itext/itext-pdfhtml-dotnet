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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BlockFormattingContextTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BlockFormattingContextTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BlockFormattingContextTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerFloat_floatsAndClear() {
            ConvertToPdfAndCompare("bfcOwnerFloat_floatsAndClear", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerFloat_marginsCollapse() {
            ConvertToPdfAndCompare("bfcOwnerFloat_marginsCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerAbsolute_floatsAndClear() {
            // TODO: DEVSIX-5470
            ConvertToPdfAndCompare("bfcOwnerAbsolute_floatsAndClear", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerAbsolute_marginsCollapse() {
            // Margins don't collapse here which is correct,
            // however absolute positioning works a bit wrong from css point of view.
            ConvertToPdfAndCompare("bfcOwnerAbsolute_marginsCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerInlineBlock_floatsAndClear() {
            ConvertToPdfAndCompare("bfcOwnerInlineBlock_floatsAndClear", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerInlineBlock_marginsCollapse() {
            ConvertToPdfAndCompare("bfcOwnerInlineBlock_marginsCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerOverflowHidden_floatsAndClear() {
            // TODO: DEVSIX-5471
            ConvertToPdfAndCompare("bfcOwnerOverflowHidden_floatsAndClear", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BfcOwnerOverflowHidden_marginsCollapse() {
            ConvertToPdfAndCompare("bfcOwnerOverflowHidden_marginsCollapse", sourceFolder, destinationFolder);
        }
    }
}
