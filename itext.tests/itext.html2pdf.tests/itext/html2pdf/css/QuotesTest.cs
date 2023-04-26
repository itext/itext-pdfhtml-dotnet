/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
    public class QuotesTest : ExtendedHtmlConversionITextTest {
        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/QuotesTest/";

        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/QuotesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DepthTest01() {
            ConvertToPdfAndCompare("depthTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DepthTest02() {
            ConvertToPdfAndCompare("depthTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DepthTest03() {
            ConvertToPdfAndCompare("depthTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedTest() {
            ConvertToPdfAndCompare("escapedTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void NoQuoteTest() {
            ConvertToPdfAndCompare("noQuoteTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ValuesTest() {
            ConvertToPdfAndCompare("valuesTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        //attr() is not supported in quotes property in browsers
        [LogMessage(Html2PdfLogMessageConstant.QUOTES_PROPERTY_INVALID)]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void AttrTest() {
            ConvertToPdfAndCompare("attrTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.QUOTES_PROPERTY_INVALID, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void ErrorTest() {
            ConvertToPdfAndCompare("errorTest", sourceFolder, destinationFolder);
        }
    }
}
