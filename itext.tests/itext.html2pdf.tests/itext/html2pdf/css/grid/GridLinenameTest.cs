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
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridLinenameTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridLinenameTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridLinenameTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinenamesCombinedTest() {
            RunTest("linenamesCombined");
        }

        [NUnit.Framework.Test]
        public virtual void LinenameSpanTest() {
            RunTest("linenameSpan");
        }

        [NUnit.Framework.Test]
        public virtual void LinenameNthTest() {
            RunTest("linenameNth");
        }

        [NUnit.Framework.Test]
        public virtual void DuplicateLineNamesTest() {
            RunTest("duplicateLineNames");
        }

        [NUnit.Framework.Test]
        public virtual void LinenameGridAreaTest() {
            RunTest("linenameGridArea");
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentSpanTest() {
            RunTest("customIndentSpan");
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentTrickySpanTest() {
            RunTest("customIndentTrickySpan");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasNamesTest() {
            RunTest("templateAreasNames");
        }

        [NUnit.Framework.Test]
        public virtual void LinenameRepeatTest() {
            RunTest("linenameRepeat");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.LINENAMES_ARE_NOT_SUPPORTED_WITHIN_AUTO_REPEAT)]
        public virtual void LinenameAutoRepeatTest() {
            RunTest("linenameAutoRepeat");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
