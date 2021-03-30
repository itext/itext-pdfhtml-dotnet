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
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    // Actually the results are invalid because there is no pdfCalligraph.
    public class FontProviderTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/FontProviderTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/FontProviderTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 4)]
        public virtual void HebrewTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hebrew.html"), new FileInfo(destinationFolder + "hebrew.pdf"
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hebrew.pdf", sourceFolder
                 + "cmp_hebrew.pdf", destinationFolder, "diffHebrew_"));
        }

        [NUnit.Framework.Test]
        public virtual void DevanagariTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "devanagari.html"), new FileInfo(destinationFolder 
                + "devanagari.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "devanagari.pdf", sourceFolder
                 + "cmp_devanagari.pdf", destinationFolder, "diffDevanagari_"));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertStandardFonts() {
            //For more specific tests see FontSelectorTimesFontTest in html2pdf and FontSelectorHelveticaFontTest in html2pdf-private
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "convertStandardFonts.html"), new FileInfo(destinationFolder
                 + "convertStandardFonts.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "convertStandardFonts.pdf"
                , sourceFolder + "cmp_convertStandardFonts", destinationFolder, "difffontstand_"));
        }

        [NUnit.Framework.Test]
        public virtual void NotoSansMonoItalicTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "notoSansMonoItalic.html"), new FileInfo(destinationFolder
                 + "notoSansMonoItalic.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "notoSansMonoItalic.pdf"
                , sourceFolder + "cmp_notoSansMonoItalic.pdf", destinationFolder, "diffnotoSansMonoItalic_"));
        }

        [NUnit.Framework.Test]
        public virtual void NotoSansMonoBoldItalicTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "notoSansMonoBoldItalic.html"), new FileInfo(destinationFolder
                 + "notoSansMonoBoldItalic.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "notoSansMonoBoldItalic.pdf"
                , sourceFolder + "cmp_notoSansMonoBoldItalic.pdf", destinationFolder, "diffnotoSansMonoBoldItalic_"));
        }
    }
}
