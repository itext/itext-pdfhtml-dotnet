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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Logs;
using iText.StyledXmlParser.Css.Media;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class PageBreakTest : ExtendedHtmlConversionITextTest {
        //Member of testing class. Add if it isn't there.
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/PageBreakTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/PageBreakTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-4521: test currently results in endless loop")]
        public virtual void BreakInsideAndBreakAfterTest() {
            RunTest("breakInsideAndBreakAfter");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAfter01Test() {
            RunTest("page-break-after01");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAfter02Test() {
            RunTest("page-break-after02");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAfter03Test() {
            RunTest("page-break-after03");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAfter04Test() {
            RunTest("page-break-after04");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAfter05Test() {
            RunTest("page-break-after05");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakBefore01Test() {
            RunTest("page-break-before01");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakBefore02Test() {
            RunTest("page-break-before02");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakBefore03Test() {
            RunTest("page-break-before03");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakBefore04Test() {
            RunTest("page-break-before04");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakBeforeAfter01Test() {
            RunTest("page-break-before-after01");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakBeforeAfter02Test() {
            RunTest("page-break-before-after02");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAfterTable01Test() {
            RunTest("page-break-after-table01");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakBeforeTable01Test() {
            RunTest("page-break-before-table01");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakInsideAvoidInParaTest() {
            RunTest("pageBreakInsideAvoidInPara");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakInsideAvoidWithFloatsWidth100PercentAndInnerClearBothTest() {
            RunTest("pageBreakInsideAvoidWithFloatsWidth100PercentAndInnerClearBothTest");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakInsideAvoidWithFloatsWidth100PercentAndInnerClearBothWithTextTest() {
            RunTest("pageBreakInsideAvoidWithFloatsWidth100PercentAndInnerClearBothWithTextTest");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakInsideAvoidWithFloatsWidth100PercentAndInnerDivWithShortTextTest() {
            // TODO: DEVSIX-4720 short text div invalid layout on page break
            RunTest("pageBreakInsideAvoidWithFloatsWidth100PercentAndInnerDivWithShortTextTest");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakInsideAvoidWithFloatsWidth100PercentAndInnerDivsWithShortAndLongTextsTest() {
            RunTest("pageBreakInsideAvoidWithFloatsWidth100PercentAndInnerDivsWithShortAndLongTextsTest");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakInsideAvoidWithFloatsWidth80PercentAndInnerDivWithShortTextTest() {
            // TODO: DEVSIX-4720 short text div invalid layout on page break
            // TODO: DEVSIX-1270 simple text layout to the left of the left float
            RunTest("pageBreakInsideAvoidWithFloatsWidth80PercentAndInnerDivWithShortTextTest");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakInsideAvoidWithFloatsWidth80PercentAndInnerDivsWithShortAndLongTextsTest() {
            // TODO: DEVSIX-1270 simple text layout to the left of the left float
            RunTest("pageBreakInsideAvoidWithFloatsWidth80PercentAndInnerDivsWithShortAndLongTextsTest");
        }

        [NUnit.Framework.Test]
        public virtual void PageBreakAlwaysInsidePageBreakAvoidTest() {
            RunTest("pageBreakAlwaysInsidePageBreakAvoidTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        public virtual void PageBreakInConstrainedDivTest() {
            NUnit.Framework.Assert.That(() =>  {
                /* Test will fail after fix in DEVSIX-2024 */
                RunTest("pageBreakInConstrainedDivTest");
            }
            , NUnit.Framework.Throws.InstanceOf<NotSupportedException>())
;
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void PageBreakInsideAvoidInTdWithBrInsideTest() {
            // TODO: DEVSIX-5263 inconsistent behavior when page-break-inside: avoid set in td and td contains inline elements
            ConvertToElements("pageBreakInsideAvoidInTdWithBrInside");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void PageBreakInsideAvoidInTdWithSpanInsideTest() {
            // TODO: DEVSIX-5263 inconsistent behavior when page-break-inside: avoid set in td and td contains inline elements
            ConvertToElements("pageBreakInsideAvoidInTdWithSpanInside");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void PageBreakInsideAvoidInTdWithHeadingsTest() {
            // TODO: DEVSIX-5263 inconsistent behavior when page-break-inside: avoid set in td and td contains inline elements
            ConvertToElements("pageBreakInsideAvoidInTdWithHeadings");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void PageBreakInsideAvoidInTdWithParaTest() {
            // TODO: DEVSIX-5263 inconsistent behavior when page-break-inside: avoid set in td and td contains inline elements
            ConvertToElements("pageBreakInsideAvoidInTdWithPara");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void PageBreakInsideAvoidInTdWithTableTest() {
            // TODO: DEVSIX-5263 inconsistent behavior when page-break-inside: avoid set in td and td contains inline elements
            ConvertToElements("pageBreakInsideAvoidInTdWithTable");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void PageBreakInsideAvoidInTdWithDivTest() {
            // TODO: DEVSIX-5263 inconsistent behavior when page-break-inside: avoid set in td and td contains inline elements
            ConvertToElements("pageBreakInsideAvoidInTdWithDiv");
        }

        private void RunTest(String name) {
            String htmlPath = sourceFolder + name + ".html";
            String pdfPath = destinationFolder + name + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), new ConverterProperties().SetMediaDeviceDescription
                (new MediaDeviceDescription(MediaType.PRINT)));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, destinationFolder, diffPrefix
                ));
        }

        private void ConvertToElements(String name) {
            String html = sourceFolder + name + ".html";
            String output = destinationFolder + name + ".pdf";
            String cmp = sourceFolder + "cmp_" + name + ".pdf";
            using (FileStream fileStream = new FileStream(html, FileMode.Open, FileAccess.Read)) {
                using (PdfDocument pdf = new PdfDocument(new PdfWriter(output))) {
                    using (Document document = new Document(pdf, PageSize.LETTER)) {
                        document.SetMargins(55, 56, 57, 45.35f);
                        IList<IElement> elements = HtmlConverter.ConvertToElements(fileStream);
                        foreach (IElement element in elements) {
                            document.Add((IBlockElement)element);
                        }
                    }
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(output, cmp, destinationFolder));
        }
    }
}
