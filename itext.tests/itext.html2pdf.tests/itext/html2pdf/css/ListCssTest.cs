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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ListCssTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/ListCSSTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/ListCSSTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListCSSStartTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "orderedList.html"), new FileInfo(DESTINATION_FOLDER
                 + "orderedList01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "orderedList01.pdf", 
                SOURCE_FOLDER + "cmp_orderedList01.pdf", DESTINATION_FOLDER, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void LowercaseATypeTest() {
            ConvertToPdfAndCompare("aType", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UppercaseATypeTest() {
            ConvertToPdfAndCompare("aAType", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LowercaseITypeTest() {
            ConvertToPdfAndCompare("iType", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UppercaseITypeTest() {
            ConvertToPdfAndCompare("iIType", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DigitTypeTest() {
            ConvertToPdfAndCompare("1Type", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO: DEVSIX-6128 NullPointerException when trying to convert html with non-existing ol type.
        [NUnit.Framework.Test]
        public virtual void UnsupportedType() {
            NUnit.Framework.Assert.Catch(typeof(NullReferenceException), () => ConvertToPdfAndCompare("unsupportedType"
                , SOURCE_FOLDER, DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void HorizontalDescriptionListTest() {
            ConvertToPdfAndCompare("horizontalDescriptionList", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
