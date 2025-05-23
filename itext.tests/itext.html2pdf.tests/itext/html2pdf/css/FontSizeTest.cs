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
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontSizeTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontSizeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontSizeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FontSize01Test() {
            ConvertToPdfAndCompare("fontSizeTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FontSize02Test() {
            ConvertToPdfAndCompare("fontSizeTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FontAbsoluteKeywords() {
            ConvertToPdfAndCompare("fontAbsoluteKeywords", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FontRelativeKeywords() {
            ConvertToPdfAndCompare("fontRelativeKeywords", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void SpacesInFontSizeValueTest() {
            ConvertToPdfAndCompare("spacesInFontSizeValueTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DefaultFontDiffFontSizeSpanTest() {
            ConvertToPdfAndCompare("defaultFontDiffFontSizeSpan", sourceFolder, destinationFolder);
        }
    }
}
