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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ColumnCountTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/ColumnCountTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/ColumnCountTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicArticleTest() {
            ConvertToPdfAndCompare("basicArticleTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivTest() {
            ConvertToPdfAndCompare("basicDivTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivWithImageTest() {
            ConvertToPdfAndCompare("basicDivWithImageTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicPTest() {
            ConvertToPdfAndCompare("basicPTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFormTest() {
            ConvertToPdfAndCompare("basicFormTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicUlTest() {
            ConvertToPdfAndCompare("basicUlTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicOlTest() {
            ConvertToPdfAndCompare("basicOlTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicTableTest() {
            ConvertToPdfAndCompare("basicTableTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicSectionTest() {
            ConvertToPdfAndCompare("basicSectionTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivMultiPageDocumentsTest() {
            ConvertToPdfAndCompare("basicDivMultiPageTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFormMultiPageDocumentsTest() {
            ConvertToPdfAndCompare("basicFormMultiPageTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDisplayPropertyTest() {
            ConvertToPdfAndCompare("basicDisplayPropertyTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDisplayPropertyWithNestedColumnsTest() {
            ConvertToPdfAndCompare("basicDisplayPropertyWithNestedColumnsTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFloatPropertyTest() {
            ConvertToPdfAndCompare("basicFloatPropertyTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFlexPropertyTest() {
            ConvertToPdfAndCompare("basicFlexPropertyTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
