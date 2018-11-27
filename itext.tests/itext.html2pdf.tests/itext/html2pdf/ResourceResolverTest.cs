/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2018 iText Group NV
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
    address: sales@itextpdf.com */
using System;
using System.IO;
using iText.Kernel.Utils;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Versions.Attributes;
using iText.Kernel;
using iText.Test;
using iText.Test.Attributes;
using NUnit.Framework;

namespace iText.Html2pdf
{
    public class ResourceResolverTest : ExtendedITextTest
    {
        public static readonly String sourceFolder =
            TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext.CurrentContext.TestDirectory) +
            "/resources/itext/html2pdf/ResourceResolverTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
                                                          + "/test/itext/html2pdf/ResourceResolverTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass()
        {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_EXTERNAL_CSS_FILE, Count = 1)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI, Count = 1)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void ResourceResolverTest03()
        {
            String baseUri = sourceFolder + "res";
            String outPdf = destinationFolder + "resourceResolverTest03.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest03.pdf";

            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest03.html", FileMode.Open,
                        FileAccess.Read),
                    fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                    "diff03_"
                ));
            }
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest07()
        {
            String outPdf = destinationFolder + "resourceResolverTest07.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceResolverTest07.html"), new FileInfo(outPdf
            ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diff07_"
            ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest07A()
        {
            String baseUri = sourceFolder + "%23r%e%2525s@o%25urces/";

            String outPdf = destinationFolder + "resourceResolverTest07A.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07A.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest07A.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff07A_"));
            }
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest07B()
        {
            String outPdf = destinationFolder + "resourceResolverTest07B.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07B.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "#r%e%25s@o%urces/resourceResolverTest07B.html"), new FileInfo(outPdf
            ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diff07B_"
            ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public virtual void ResourceResolverTest07C()
        {
            String outPdf = destinationFolder + "resourceResolverTest07C.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07C.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "#r%e%25s@o%urces/resourceResolverTest07C.html"), new FileInfo(outPdf
            ), new ConverterProperties().SetBaseUri(sourceFolder + "#r%e%25s@o%urces/.."));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diff07C_"
            ));
        }


        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest09()
        {
            String outPdf = destinationFolder + "resourceResolverTest09.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest09.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceResolverTest09.html"), new FileInfo(outPdf
            ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diff09_"
            ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest10()
        {
            String outPdf = destinationFolder + "resourceResolverTest10.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest10.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest10.html", FileMode.Open,
                        FileAccess.Read),
                    fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri("%homepath%"));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff10_"));
            }
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest11()
        {
            String outPdf = destinationFolder + "resourceResolverTest11.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest11.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest11.html", FileMode.Open,
                        FileAccess.Read),
                    fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri("https://en.wikipedia.org/wiki/Welsh_Corgi"));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff11_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest12A()
        {
            String baseUri = sourceFolder + "path%with%spaces/";

            String outPdf = destinationFolder + "resourceResolverTest12A.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest12A.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12A.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff12A_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest12B()
        {
            String baseUri = sourceFolder + "path%25with%25spaces/";

            String outPdf = destinationFolder + "resourceResolverTest12B.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest12B.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12B.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff12B_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest12C()
        {
            String baseUri = sourceFolder + "path%2525with%2525spaces/";

            String outPdf = destinationFolder + "resourceResolverTest12C.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest12C.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12C.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff12C_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest12D()
        {
            String baseUri = sourceFolder + "path with spaces/";

            String outPdf = destinationFolder + "resourceResolverTest12D.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest12D.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12D.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff12D_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest12E()
        {
            String baseUri = sourceFolder + "path%20with%20spaces/";

            String outPdf = destinationFolder + "resourceResolverTest12E.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest12E.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12E.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff12E_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest12F()
        {
            String baseUri = sourceFolder + "path%2520with%2520spaces/";

            String outPdf = destinationFolder + "resourceResolverTest12F.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest12F.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12F.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff12F_"));
            }
        }

        [NUnit.Framework.Test]
        public void ResourceResolverTest13()
        {
            String baseUri = sourceFolder;

            String outPdf = destinationFolder + "resourceResolverTest13.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest13.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest13.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff13_"));
            }
        }

        [NUnit.Framework.Test]
        public void ResourceResolverTest15()
        {
            String baseUri = sourceFolder;

            String outPdf = destinationFolder + "resourceResolverTest15.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest15.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest15.html", FileMode.Open,
                        FileAccess.Read),
                    fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff15_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest16A()
        {
            String baseUri = sourceFolder + "path/with/spaces/";

            String outPdf = destinationFolder + "resourceResolverTest16A.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest16A.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16A.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff16A_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest16B()
        {
            String baseUri = sourceFolder + "path%2Fwith%2Fspaces/";

            String outPdf = destinationFolder + "resourceResolverTest16B.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest16B.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16B.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff16B_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest16C()
        {
            String baseUri = sourceFolder + "path%252Fwith%252Fspaces/";

            String outPdf = destinationFolder + "resourceResolverTest16C.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest16C.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16C.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff16C_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public void ResourceResolverTest16D()
        {
            String baseUri = sourceFolder + "path%25252Fwith%25252Fspaces/";

            String outPdf = destinationFolder + "resourceResolverTest16D.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest16D.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16D.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff16D_"));
            }
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("The path to the image shall be changed to reference some available shared file in order to run the test correctly.")]
        public void ResourceResolverTest17()
        {
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverTest17.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest17.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest17.html", FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff17_"));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        public void ResourceResolverTest18()
        {
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverTest18.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest18.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest18.html", FileMode.Open,
                        FileAccess.Read),
                    fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff18_"));
            }
        }


        // TODO test with absolute http links for resources?
        // TODO test with http base URI?
    }
}
