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
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css.W3c {
    /// <seealso><a href="https://github.com/w3c/csswg-test">https://github.com/w3c/csswg-test</a></seealso>
    [NUnit.Framework.Category("IntegrationTest")]
    public abstract class W3CCssTest : ExtendedITextTest {
        private static readonly String baseSourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/w3c/";

        private static readonly String baseDestinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/w3c/";

        protected internal abstract String GetHtmlFileName();

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
        }

        [NUnit.Framework.SetUp]
        public virtual void Before() {
            CreateDestinationFolder(GetDestinationFolder());
        }

        [NUnit.Framework.Test]
        public virtual void Test() {
            String sourceFolder = GetSourceFolder();
            String destinationFolder = GetDestinationFolder();
            String htmlFilePath = sourceFolder + GetHtmlFileName();
            String outFilePath = destinationFolder + GetOutPdfFileName();
            String cmpFilePath = sourceFolder + GetOutPdfFileName();
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlFilePath) + "\n");
            HtmlConverter.ConvertToPdf(new FileInfo(htmlFilePath), new FileInfo(outFilePath), GetConverterProperties()
                );
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFilePath, cmpFilePath, destinationFolder
                , "diff_"));
        }

        protected internal virtual ConverterProperties GetConverterProperties() {
            return null;
        }

        private String GetDestinationFolder() {
            String localPackage = GetLocalPackage();
            return baseDestinationFolder + localPackage + System.IO.Path.DirectorySeparatorChar + GetTestClassName() +
                 System.IO.Path.DirectorySeparatorChar;
        }

        private String GetSourceFolder() {
            String localPackage = GetLocalPackage();
            return baseSourceFolder + localPackage + System.IO.Path.DirectorySeparatorChar;
        }

        private String GetTestClassName() {
            return GetType().Name;
        }

        private String GetLocalPackage() {
            String packageName = GetType().Namespace.ToString();
            String basePackageName = typeof(W3CCssTest).Namespace.ToString();
            return packageName.Substring(basePackageName.Length).Replace('.', System.IO.Path.DirectorySeparatorChar);
        }

        private String GetOutPdfFileName() {
            String htmlFileName = GetHtmlFileName();
            return iText.Commons.Utils.StringUtil.ReplaceAll(htmlFileName, "\\.[a-zA-Z]+?$", ".pdf");
        }
    }
}
