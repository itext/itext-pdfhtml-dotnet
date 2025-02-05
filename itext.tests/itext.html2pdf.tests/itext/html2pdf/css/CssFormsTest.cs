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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class CssFormsTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssFormsTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssFormsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        public virtual void FormWithIconsTest() {
            RunTest("formWithIcons");
        }

        [NUnit.Framework.Test]
        public virtual void InlineCheckboxesAndRadiosTest() {
            RunTest("inlineCheckboxesAndRadios");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED, Count = 2)]
        public virtual void IconsInHorizontalFormsTest() {
            RunTest("iconsInHorizontalForms");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED, Count = 2)]
        public virtual void IconsInlineFormsTest() {
            RunTest("iconsInlineForms");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        public virtual void MinWidthInlineFormsGroupTest() {
            RunTest("minWidthInlineFormsGroup");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        public virtual void FullWidthFormsGroupTest() {
            RunTest("fullWidthFormsGroup");
        }

        [NUnit.Framework.Test]
        public virtual void DisabledSelectTest() {
            RunTest("disabledSelect");
        }

        [NUnit.Framework.Test]
        public virtual void DisabledInputTest() {
            RunTest("disabledInput");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxAndRadioDisabledTest() {
            RunTest("checkboxAndRadioDisabled");
        }

        [NUnit.Framework.Test]
        public virtual void ReadOnlyInputTest() {
            RunTest("readOnlyInput");
        }

        [NUnit.Framework.Test]
        public virtual void ActiveAndDisabledStateOfButtonTest() {
            RunTest("activeAndDisabledStateOfButton");
        }

        [NUnit.Framework.Test]
        public virtual void BlockLevelButtonsTest() {
            RunTest("blockLevelButtons");
        }

        [NUnit.Framework.Test]
        public virtual void ButtonOnElementsTest() {
            RunTest("buttonOnElements");
        }

        [NUnit.Framework.Test]
        public virtual void ButtonSizesTest() {
            RunTest("buttonSizes");
        }

        [NUnit.Framework.Test]
        public virtual void StyledButtonsTest() {
            RunTest("styledButtons");
        }

        [NUnit.Framework.Test]
        public virtual void IconsInButtonsOfDifferentSizeTest() {
            RunTest("iconsInButtonsOfDifferentSize");
        }

        private void RunTest(String testName) {
            String htmlName = SOURCE_FOLDER + testName + ".html";
            String outFileName = DESTINATION_FOLDER + testName + ".pdf";
            String cmpFileName = SOURCE_FOLDER + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            PrintPathToConsole(htmlName, "html: ");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, DESTINATION_FOLDER
                ));
        }
    }
}
