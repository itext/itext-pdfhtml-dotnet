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

namespace iText.Html2pdf.Css.W3c {
    /// <seealso><a href="https://github.com/w3c/csswg-test">https://github.com/w3c/csswg-test</a></seealso>
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
