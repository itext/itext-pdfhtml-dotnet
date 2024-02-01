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
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ObjectFitTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/ObjectFitTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/ObjectFitTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ObjectFitCasesTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "objectFit_test.html"), new FileInfo(destinationFolder
                 + "objectFit_test.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "objectFit_test.pdf", 
                sourceFolder + "cmp_objectFit_test.pdf", destinationFolder, "diff18_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNEXPECTED_VALUE_OF_OBJECT_FIT)]
        public virtual void ObjectFitUnexpectedValueTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "objectFit_test_unexpected.html"), new FileInfo(destinationFolder
                 + "objectFit_test_unexpected.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "objectFit_test_unexpected.pdf"
                , sourceFolder + "cmp_objectFit_test_unexpected.pdf", destinationFolder, "diff18_"));
        }
    }
}
