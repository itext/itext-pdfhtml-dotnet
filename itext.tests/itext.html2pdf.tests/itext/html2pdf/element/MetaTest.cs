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
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class MetaTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/MetaTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/MetaTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Meta01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "metaTest01.html"), new FileInfo(DESTINATION_FOLDER
                 + "metaTest01.pdf"));
            PdfDocumentInfo pdfDocInfo = new PdfDocument(new PdfReader(DESTINATION_FOLDER + "metaTest01.pdf")).GetDocumentInfo
                ();
            CompareTool compareTool = new CompareTool();
            NUnit.Framework.Assert.IsNull(compareTool.CompareByContent(DESTINATION_FOLDER + "metaTest01.pdf", SOURCE_FOLDER
                 + "cmp_metaTest01.pdf", DESTINATION_FOLDER, "diff01_"));
            NUnit.Framework.Assert.IsNull(compareTool.CompareDocumentInfo(DESTINATION_FOLDER + "metaTest01.pdf", SOURCE_FOLDER
                 + "cmp_metaTest01.pdf"));
            NUnit.Framework.Assert.AreEqual(pdfDocInfo.GetMoreInfo("test"), "the test content");
        }

        [NUnit.Framework.Test]
        public virtual void Meta02Test() {
            // In this test we also check that it's not possible to override description name content
            // (which iText converts to pdf's Subject content) with Subject name content
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "metaTest02.html"), new FileInfo(DESTINATION_FOLDER
                 + "metaTest02.pdf"));
            PdfDocumentInfo pdfDocInfo = new PdfDocument(new PdfReader(DESTINATION_FOLDER + "metaTest02.pdf")).GetDocumentInfo
                ();
            CompareTool compareTool = new CompareTool();
            NUnit.Framework.Assert.IsNull(compareTool.CompareByContent(DESTINATION_FOLDER + "metaTest02.pdf", SOURCE_FOLDER
                 + "cmp_metaTest02.pdf", DESTINATION_FOLDER, "diff02_"));
            NUnit.Framework.Assert.IsNull(compareTool.CompareDocumentInfo(DESTINATION_FOLDER + "metaTest02.pdf", SOURCE_FOLDER
                 + "cmp_metaTest02.pdf"));
            NUnit.Framework.Assert.AreEqual(pdfDocInfo.GetAuthor(), "Bruno Lowagie");
            NUnit.Framework.Assert.AreEqual(pdfDocInfo.GetKeywords(), "metadata, keywords, test");
            NUnit.Framework.Assert.AreEqual(pdfDocInfo.GetSubject(), "This is the description of the page");
            NUnit.Framework.Assert.AreEqual(pdfDocInfo.GetMoreInfo("generator"), "Eugenerator Onegenerator");
            NUnit.Framework.Assert.AreEqual(pdfDocInfo.GetMoreInfo("subject"), "Trying to break iText and write pdf's Subject with subject instead of description name"
                );
        }

        [NUnit.Framework.Test]
        public virtual void Meta03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "metaTest03.html"), new FileInfo(DESTINATION_FOLDER
                 + "metaTest03.pdf"));
            CompareTool compareTool = new CompareTool();
            NUnit.Framework.Assert.IsNull(compareTool.CompareByContent(DESTINATION_FOLDER + "metaTest03.pdf", SOURCE_FOLDER
                 + "cmp_metaTest03.pdf", DESTINATION_FOLDER, "diff03_"));
            NUnit.Framework.Assert.IsNull(compareTool.CompareDocumentInfo(DESTINATION_FOLDER + "metaTest03.pdf", SOURCE_FOLDER
                 + "cmp_metaTest03.pdf"));
        }

        [NUnit.Framework.Test]
        public virtual void MetaApplicationNameTest() {
            String srcHtml = SOURCE_FOLDER + "metaApplicationName.html";
            String outPdf = DESTINATION_FOLDER + "metaApplicationName.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_metaApplicationName.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(srcHtml), new FileInfo(outPdf));
            PdfDocumentInfo pdfDocInfo = new PdfDocument(new PdfReader(outPdf)).GetDocumentInfo();
            CompareTool compareTool = new CompareTool();
            NUnit.Framework.Assert.IsNull(compareTool.CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER, "metaAppName_"
                ));
            NUnit.Framework.Assert.AreEqual("iText", pdfDocInfo.GetCreator());
        }
    }
}
