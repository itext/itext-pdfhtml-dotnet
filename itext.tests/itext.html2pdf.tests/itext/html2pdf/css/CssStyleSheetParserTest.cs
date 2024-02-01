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
using System.IO;
using iText.IO.Util;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Parse;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("UnitTest")]
    public class CssStyleSheetParserTest : ExtendedITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssStyleSheetParserTest/";

        private const String DEFAULT_CSS_PATH = "iText.Html2Pdf.default.css";

        [NUnit.Framework.Test]
        public virtual void TestDefaultCss() {
            String cmpFile = sourceFolder + "cmp_default.css";
            CssStyleSheet styleSheet = CssStyleSheetParser.Parse(ResourceUtil.GetResourceStream(DEFAULT_CSS_PATH));
            NUnit.Framework.Assert.AreEqual(GetCssFileContents(cmpFile), styleSheet.ToString());
        }

        private String GetCssFileContents(String filePath) {
            byte[] bytes = StreamUtil.InputStreamToArray(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            String content = iText.Commons.Utils.JavaUtil.GetStringForBytes(bytes);
            content = content.Trim();
            content = content.Replace("\r\n", "\n");
            return content;
        }
    }
}
