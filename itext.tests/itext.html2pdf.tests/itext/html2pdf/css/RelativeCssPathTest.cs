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
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class RelativeCssPathTest : ExtendedITextTest {
        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/RelativeCssPathTest/";

        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/RelativeCssPathTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeCssPath01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "css_relative.html"), new FileInfo(destinationFolder
                 + "css_relative.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "css_relative.pdf", sourceFolder
                 + "cmp_css_relative.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void RelativeCssPath02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "css_relative_base64.html"), new FileInfo(destinationFolder
                 + "css_relative_base64.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "css_relative_base64.pdf"
                , sourceFolder + "cmp_css_relative_base64.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void RelativeImports01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "root/html/test.html"), new FileInfo(destinationFolder
                 + "relativeImportsTest.pdf"), new ConverterProperties().SetBaseUri(sourceFolder + "root/html/"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "relativeImportsTest.pdf"
                , sourceFolder + "cmp_relativeImportsTest.pdf", destinationFolder));
        }
    }
}
