/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
    public class FontSelectorTimesFontTest : ExtendedFontPropertiesTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontSelectorTimesFontTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontSelectorTimesFontTest/";

        private static String[] FONT_WEIGHTS = new String[] { "normal", "bold", "100", "300", "500", "600", "700", 
            "900" };

        // TODO DEVSIX-2114 Add bolder/lighter font-weights once they are supported
        private static String[] FONT_STYLES = new String[] { "normal", "italic", "oblique" };

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TimesFontFamilyTest() {
            String text = "quick brown fox jumps over the lazy dog";
            String[] fontFamilies = new String[] { "times", "roman", "times roman", "times-roman", "times bold", "times-bold"
                , "times-italic", "times-italic", "times roman italic", "times-roman italic", "times roman bold", "times-roman bold"
                 };
            String fileName = "timesFontFamilyTest";
            String htmlString = BuildDocumentTree(fontFamilies, FONT_WEIGHTS, FONT_STYLES, null, text);
            RunTest(htmlString, sourceFolder, destinationFolder, fileName, fileName);
        }

        [NUnit.Framework.Test]
        public virtual void TimesFontFamilyTest02() {
            String text = "quick brown fox jumps over the lazy dog";
            String[] fontFamilies = new String[] { "times", "new", "roman", "times new", "times roman", "new roman", "times new roman"
                , "times new toman", "times mew roman", "mimes new roman" };
            String fileName = "timesFontFamilyTest02";
            String htmlString = BuildDocumentTree(fontFamilies, new String[] { "normal" }, new String[] { "normal" }, 
                null, text);
            RunTest(htmlString, sourceFolder, destinationFolder, fileName, fileName);
        }
    }
}
