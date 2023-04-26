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
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ListStyleImageLinearGradientTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/ListStyleImageLinearGradientTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/ListStyleImageLinearGradientTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientInListStyleTest() {
            RunTest("linearGradientInListStyle");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientTypeTest() {
            RunTest("linearGradientType");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 3)]
        public virtual void InvalidLinearGradientTypeTest() {
            RunTest("invalidLinearGradientType");
        }

        [NUnit.Framework.Test]
        public virtual void RepeatingLinearGradientTypeTest() {
            RunTest("repeatingLinearGradientType");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientWithEmRemValuesTest() {
            RunTest("linearGradientWithEmRemValues");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentLinearGradientsInElementsTest() {
            RunTest("differentLinearGradientsInElements");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientInDifferentElementsTest() {
            RunTest("linearGradientInDifferentElements");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientDifferentFontSizeTest() {
            RunTest("linearGradientDifferentFontSize");
        }

        private void RunTest(String testName) {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + testName + ".html"), new FileInfo(DESTINATION_FOLDER
                 + testName + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + testName + ".pdf", SOURCE_FOLDER
                 + "cmp_" + testName + ".pdf", DESTINATION_FOLDER, "diff_" + testName));
        }
    }
}
