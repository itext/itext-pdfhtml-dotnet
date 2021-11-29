/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
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

        private void RunTest(String testName) {
            String htmlName = SOURCE_FOLDER + testName + ".html";
            String outFileName = DESTINATION_FOLDER + testName + ".pdf";
            String cmpFileName = SOURCE_FOLDER + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlName) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, DESTINATION_FOLDER
                ));
        }
    }
}
