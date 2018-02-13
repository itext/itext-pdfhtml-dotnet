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
address: sales@itextpdf.com
*/
using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    public class CssNthChildSelectorTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssNthChildSelectorTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssNthChildSelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NthChildEvenTest() {
            String outPdf = destinationFolder + "resourceNthChildEvenTest.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceNthChildEvenTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceNthChildEvenTest.html"), new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffNthChildEven_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NthChildExpressionTest() {
            String outPdf = destinationFolder + "resourceNthChildExpressionTest.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceNthChildExpressionTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceNthChildExpressionTest.html"), new FileInfo
                (outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffNthChildExpression_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NthOfTypeNegativeExpressionTest() {
            String outPdf = destinationFolder + "resourceNthOfTypeNegativeExpressionTest.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceNthOfTypeNegativeExpressionTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceNthOfTypeNegativeExpressionTest.html"), new 
                FileInfo(outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffNthOfTypeNegativeExpression_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NthChildIntegerTest() {
            String outPdf = destinationFolder + "resourceNthChildIntegerTest.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceNthChildIntegerTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceNthChildIntegerTest.html"), new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffNthChildInteger_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FirstChildTest() {
            String outPdf = destinationFolder + "resourceFirstChildTest.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceFirstChildTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceFirstChildTest.html"), new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffFirstChild_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LastChildTest() {
            String outPdf = destinationFolder + "resourceLastChildTest.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceLastChildTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceLastChildTest.html"), new FileInfo(outPdf)
                );
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffLastChild_"
                ));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NotExpressionChildTest() {
            String outPdf = destinationFolder + "resourceNotExpressionChildTest.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceNotExpressionChildTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceNotExpressionChildTest.html"), new FileInfo
                (outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffNotExpChild_"
                ));
        }
    }
}
