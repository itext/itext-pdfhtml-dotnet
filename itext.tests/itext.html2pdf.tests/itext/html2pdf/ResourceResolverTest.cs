/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
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

namespace iText.Html2pdf {
    public class ResourceResolverTest : ExtendedITextTest {
        public static readonly String sourceFolder = TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext.CurrentContext.TestDirectory) + "/resources/itext/html2pdf/ResourceResolverTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/ResourceResolverTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest03() {
            String baseUri = sourceFolder + "res";
            String outPdf = destinationFolder + "resourceResolverTest03.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest03.pdf";

            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest03.html", FileMode.Open, FileAccess.Read),
                fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff03_"
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
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff07_"
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
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff09_"
            ));
        }



        // TODO test with absolute http links for resources?
        // TODO test with http base URI?
    }
}
