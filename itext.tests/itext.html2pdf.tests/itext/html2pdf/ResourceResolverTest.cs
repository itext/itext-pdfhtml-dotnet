/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using Versions.Attributes;
using iText.Kernel;
using iText.Test;
using iText.Test.Attributes;
using iText.Html2pdf.Resolver.Resource;
using iText.Html2pdf.Attach;
using iText.Kernel.Pdf.Xobject;
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

        private static readonly String bLogoCorruptedData =
            "data:image/png;base64,,,iVBORw0KGgoAAAANSUhEUgAAAVoAAAAxCAMAAACsy5FpAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAqUExURQAAAPicJAdJdQdJdQdJdficJjBUbPicJgdJdQdJdficJficJQdJdficJlrFe50AAAAMdFJOUwCBe8I/Phe+65/saIJg0K4AAAMOSURBVHja7ZvbmqsgDIU5Bo/v/7q7/WZXsQYNuGy1muuZFH7DIiSglFLU6pZUbGQQNvXpNcC4caoNRvNxOuDUdf80HXk3VYewKp516DHWxuOc/0ye/U00duAwU+/qkWzfh9F9hzIHJxuzNa+fsa4I7Ihx+H+qUFN/sKVhzP7lH+a+qwY1gJHtmwFDPBHK1wLLjLOGTb2jIWhHScAF7RgOGod2CAGTFB8J2JodJ3Dq5kNow95oH3BdtsjGHE6LVu+P9iG5UlVwNjXOndGeRWuZEBBJLtWcMMK11nFoDfDL4TOEMUu0K/leIpNNpUrYFVsrDi2Mbb1DXqv5PV4quWzKHikJKq99utTsoI1dsMjBkr2dctoAMO3XQS2ogrNrJ5vH1OvtU6/ddIPR0k1g9K++bcSKo6Htf8wbdxpK2rnRigJRqAU3WiEylzzVlubCF0TLb/pTyZXH9o1WoKLVoKK8yBbUHS6IdjksZYpxo82WXIzIXhptYtmDRPbQaDXiPBZaaQl26ZBI6pfQ+gZ00A3CxkH6COo2rIwjom12KM/IJRehBUdF2wLrtUWS+56P/Q7aPUrheYnYRpE9LtrwSbSp7cxuJnv1qCWzk9AeEy3t0MAp2ccq93NogWHry3QWowqHPDK0mPSr8aXZAWQzO+hB17ebb9P5ZbDCu2obJPeiNQQWbAUse10VbbKqSLm9yRutQGT/8wO0G6+LdvV2Aaq0eDW0kmI3SHKvhZZkESnoTd5o5SIr+gb0A2g9wGQi67KUw5wdLajNEHymyCqo5B4RLawWHp10XcEC528suBOjJVwDZ2iOca9lBNsSl4jZE6Ntd6jXmtKVzeiIOy/aDzwTydmPZpJrzov2A89EsrKod8mVoq1y0LbsE02Zf/sVQSAObXa5ZSq5UkGoZw9LlqwRNkai5ZT7rRXyHkJgQqioSBipgjhGHPdMYy3hbLx8UDbDPTatndyeeW1HpaXtodxYyUO+zmoDUWjeUnHRB7d5E/KQnazRs0VdbWjI/EluloPnb26+KXIGI+e+7CBt/wAetDeCKwxY6QAAAABJRU5ErkJggg==";

        private static readonly String bLogo =
            "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAVoAAAAxCAMAAACsy5FpAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAqUExURQAAAPicJAdJdQdJdQdJdficJjBUbPicJgdJdQdJdficJficJQdJdficJlrFe50AAAAMdFJOUwCBe8I/Phe+65/saIJg0K4AAAMOSURBVHja7ZvbmqsgDIU5Bo/v/7q7/WZXsQYNuGy1muuZFH7DIiSglFLU6pZUbGQQNvXpNcC4caoNRvNxOuDUdf80HXk3VYewKp516DHWxuOc/0ye/U00duAwU+/qkWzfh9F9hzIHJxuzNa+fsa4I7Ihx+H+qUFN/sKVhzP7lH+a+qwY1gJHtmwFDPBHK1wLLjLOGTb2jIWhHScAF7RgOGod2CAGTFB8J2JodJ3Dq5kNow95oH3BdtsjGHE6LVu+P9iG5UlVwNjXOndGeRWuZEBBJLtWcMMK11nFoDfDL4TOEMUu0K/leIpNNpUrYFVsrDi2Mbb1DXqv5PV4quWzKHikJKq99utTsoI1dsMjBkr2dctoAMO3XQS2ogrNrJ5vH1OvtU6/ddIPR0k1g9K++bcSKo6Htf8wbdxpK2rnRigJRqAU3WiEylzzVlubCF0TLb/pTyZXH9o1WoKLVoKK8yBbUHS6IdjksZYpxo82WXIzIXhptYtmDRPbQaDXiPBZaaQl26ZBI6pfQ+gZ00A3CxkH6COo2rIwjom12KM/IJRehBUdF2wLrtUWS+56P/Q7aPUrheYnYRpE9LtrwSbSp7cxuJnv1qCWzk9AeEy3t0MAp2ccq93NogWHry3QWowqHPDK0mPSr8aXZAWQzO+hB17ebb9P5ZbDCu2obJPeiNQQWbAUse10VbbKqSLm9yRutQGT/8wO0G6+LdvV2Aaq0eDW0kmI3SHKvhZZkESnoTd5o5SIr+gb0A2g9wGQi67KUw5wdLajNEHymyCqo5B4RLawWHp10XcEC528suBOjJVwDZ2iOca9lBNsSl4jZE6Ntd6jXmtKVzeiIOy/aDzwTydmPZpJrzov2A89EsrKod8mVoq1y0LbsE02Zf/sVQSAObXa5ZSq5UkGoZw9LlqwRNkai5ZT7rRXyHkJgQqioSBipgjhGHPdMYy3hbLx8UDbDPTatndyeeW1HpaXtodxYyUO+zmoDUWjeUnHRB7d5E/KQnazRs0VdbWjI/EluloPnb26+KXIGI+e+7CBt/wAetDeCKwxY6QAAAABJRU5ErkJggg==";


        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass()
        {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_EXTERNAL_CSS_FILE, Count = 1)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI,
            Count = 1)]
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest07A.html",
                    FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff07A_"));
            }
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest07B()
        {
            String outPdf = destinationFolder + "resourceResolverTest07B.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07B.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "#r%e%25s@o%urces/resourceResolverTest07B.html"),
                new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diff07B_"
            ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public virtual void ResourceResolverTest07C()
        {
            String outPdf = destinationFolder + "resourceResolverTest07C.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07C.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "#r%e%25s@o%urces/resourceResolverTest07C.html"),
                new FileInfo(outPdf
                ), new ConverterProperties().SetBaseUri(sourceFolder + "#r%e%25s@o%urces/.."));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diff07C_"
            ));
        }

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

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest01()
        {
            String outPdf = destinationFolder + "ResourceResolverHtmlWithSvgTest01.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest01.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "ResourceResolverHtmlWithSvgTest01.html"),
                new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diff01_"
            ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI,
            Count = 2)]

        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void ResourceResolverHtmlWithSvgTest02()
        {
            String baseUri = sourceFolder + "%23r%e%2525s@o%25urces/";

            String outPdf = destinationFolder + "ResourceResolverHtmlWithSvgTest02.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest02.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "ResourceResolverHtmlWithSvgTest02.html",
                    FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff02_"));
            }
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest03()
        {
            String baseUri = sourceFolder + "%23r%e%2525s@o%25urces/";

            String outPdf = destinationFolder + "ResourceResolverHtmlWithSvgTest03.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest03.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "ResourceResolverHtmlWithSvgTest03.html",
                    FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff03_"));
            }
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest04()
        {
            String baseUri = sourceFolder;

            String outPdf = destinationFolder + "ResourceResolverHtmlWithSvgTest04.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest04.pdf";
            using (
                FileStream fileInputStream = new FileStream(sourceFolder + "ResourceResolverHtmlWithSvgTest04.html",
                    FileMode.Open,
                    FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create))
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream,
                    new ConverterProperties().SetBaseUri(baseUri));
                NUnit.Framework.Assert.IsNull(
                    new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff04_"));
            }
        }

        [NUnit.Framework.Test]
        //TODO: update after DEVSIX-2239 fix
        public virtual void ResourceResolverCssWithSvg()
        {
            String outPdf = destinationFolder + "ResourceResolverCssWithSvg.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverCssWithSvg.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "ResourceResolverCssWithSvg.html"), new FileInfo(outPdf
            ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diffCss_"
            ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]

        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        public virtual void ResourceResolverHtmlWithSvgDifferentLevels()
        {
            String outPdf = destinationFolder + "ResourceResolverHtmlWithSvgDifferentLevels.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgDifferentLevels.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "ResourceResolverHtmlWithSvgDifferentLevels.html"), new FileInfo(outPdf
            ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder,
                "diffsvgLevels_"
            ));
        }

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

        [NUnit.Framework.Test]
        // TODO DEVSIX-1595
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12A.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12B.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12C.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12D.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12E.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest12F.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16A.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16B.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16C.html",
                    FileMode.Open,
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
                FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest16D.html",
                    FileMode.Open,
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
        [NUnit.Framework.Ignore(
            "The path to the image shall be changed to reference some available shared file in order to run the test correctly.")]
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
        public void ResourceResolverSvgWithImageInlineTest() {
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverSvgWithImageInline.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverSvgWithImageInline.pdf";
            String inHtml = sourceFolder + "resourceResolverSvgWithImageInline.html";
            using ( FileStream fileInputStream =  new FileStream(inHtml, FileMode.Open, FileAccess.Read), 
                    fileOutputStream = new FileStream(outPdf, FileMode.Create)) 
            {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri));
            }
            NUnit.Framework.Assert.IsNull(
                new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffInlineSvg_"));
        }

        [NUnit.Framework.Test]
        public void ResourceResolverSvgWithImageBackgroundTest() {
            //Browsers do not render this
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverSvgWithImageBackground.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverSvgWithImageBackground.pdf";
            String inHtml = sourceFolder + "resourceResolverSvgWithImageBackground.html";
            using ( FileStream fileInputStream =  new FileStream(inHtml, FileMode.Open, FileAccess.Read), 
                fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri));
            }
            NUnit.Framework.Assert.IsNull(
                new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffSvgWithImg_"));
        }

        [NUnit.Framework.Test]
        public void ResourceResolverSvgWithImageObjectTest() {
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverSvgWithImageObject.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverSvgWithImageObject.pdf";
            String inHtml = sourceFolder + "resourceResolverSvgWithImageObject.html";
            using ( FileStream fileInputStream =  new FileStream(inHtml, FileMode.Open, FileAccess.Read), 
                fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri));
            }
            NUnit.Framework.Assert.IsNull(
                new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff18_"));
        }

        private HtmlResourceResolver CreateResolver() {
            ConverterProperties cp = new ConverterProperties();
            cp.SetBaseUri(sourceFolder);
            return new HtmlResourceResolver(sourceFolder, new ProcessorContext(cp));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI, Count = 1)]
        public void RetrieveImageExtendedNullTest() {
            HtmlResourceResolver resourceResolver = CreateResolver();
            PdfXObject image = resourceResolver.RetrieveImageExtended(null);
            NUnit.Framework.Assert.IsNull(image);
        }

        [NUnit.Framework.Test]
        public void RetrieveImageExtendedBase64Test() {
            HtmlResourceResolver resourceResolver = CreateResolver();
            PdfXObject image = resourceResolver.RetrieveImageExtended(bLogo);
            NUnit.Framework.Assert.NotNull(image);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI, Count = 1)]
        public void RetrieveImageExtendedIncorrectBase64Test() {
            HtmlResourceResolver resourceResolver = CreateResolver();
            PdfXObject image = resourceResolver.RetrieveImageExtended(bLogoCorruptedData);
            NUnit.Framework.Assert.IsNull(image);
        }

        [NUnit.Framework.Test]
         [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI, Count = 1)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER,
            Count = 1)]
        public void ResourceResolverIncorrectSyntaxTest() {
			//this test is inconsistent with java
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverIncorrectSyntaxObject.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverIncorrectSyntaxObject.pdf";
            String inHtml = sourceFolder + "resourceResolverIncorrectSyntaxObject.html";
            using ( FileStream fileInputStream =  new FileStream(inHtml, FileMode.Open, FileAccess.Read), 
                fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffIncorrectSyntax_"));
        }

        // TODO test with absolute http links for resources?
        // TODO test with http base URI?
    }
}
