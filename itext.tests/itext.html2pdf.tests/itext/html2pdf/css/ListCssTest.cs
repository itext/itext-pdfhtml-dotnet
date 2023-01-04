/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
