/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
Authors: iText Software.

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
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Pdf.Tagutils;
using iText.Kernel.Utils;

namespace iText.Html2pdf.Attribute {
    public class LangAttributeTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/LangAttributeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/LangAttributeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInElementForTaggedPdfTest() {
            //TODO: DEVSIX-3243 Assert exact language
            String html = sourceFolder + "langAttrInElementForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInElementForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInHtmlTagForTaggedPdfTest() {
            //TODO: DEVSIX-3243 Assert exact language
            String html = sourceFolder + "langAttrInHtmlTagForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInHtmlTagForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInvalidTagsTest() {
            //TODO: DEVSIX-3243 Assert exact language
            String html = sourceFolder + "langAttrInvalidTagsTest.html";
            String outFile = destinationFolder + "langAttrInvalidTagsTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(3, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(4, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(5, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(6, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrEmptyTagTest() {
            //TODO: DEVSIX-3243 Assert exact language
            String html = sourceFolder + "langAttrEmptyTagTest.html";
            String outFile = destinationFolder + "langAttrEmptyTagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrRegionSubtagTest() {
            //TODO: DEVSIX-3243 Assert exact language
            String html = sourceFolder + "langAttrRegionSubtagTest.html";
            String outFile = destinationFolder + "langAttrRegionSubtagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(3, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrScriptSubtagTest() {
            //TODO: DEVSIX-3243 Assert exact language
            String html = sourceFolder + "langAttrScriptSubtagTest.html";
            String outFile = destinationFolder + "langAttrScriptSubtagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(3, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrScriptRegionSubtagTest() {
            //TODO: DEVSIX-3243 Assert exact language
            String html = sourceFolder + "langAttrScriptRegionSubtagTest.html";
            String outFile = destinationFolder + "langAttrScriptRegionSubtagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInTableForTaggedPdfTest() {
            //TODO: after DEVSIX-3243 fixed need to assert exact language and check assertEquals
            String html = sourceFolder + "langAttrInTableForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInTableForTaggedPdfTest.pdf";
            String cmp = sourceFolder + "cmp_langAttrInTableForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            //compareByContent is used here to check the complete logical structure tree to notice all the differences.
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmp, destinationFolder, "diff_Table"
                ));
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            pdfDocument.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInSvgForTaggedPdfTest() {
            //TODO: after DEVSIX-3243 fixed need to assert exact language and check assertEquals
            String html = sourceFolder + "langAttrInSvgForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInSvgForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, new ConverterProperties
                ().SetBaseUri(sourceFolder));
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(StandardRoles.FIGURE);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToParent().MoveToKid(2, StandardRoles.P).MoveToKid(StandardRoles.FIGURE);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInListsForTaggedPdfTest() {
            //TODO: after DEVSIX-3243 fixed need to assert exact language and check assertEquals
            String html = sourceFolder + "langAttrInListsForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInListsForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(0, StandardRoles.L);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToParent();
            tagPointer.MoveToKid(1, StandardRoles.L);
            tagPointer.GetProperties().GetLanguage();
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToParent();
            tagPointer.MoveToKid(2, StandardRoles.L).MoveToKid(0, StandardRoles.LI);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot();
            tagPointer.MoveToKid(2, StandardRoles.L).MoveToKid(1, StandardRoles.LI);
            NUnit.Framework.Assert.IsNull(tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInDivAndSpanForTagPdfTest() {
            //TODO: after DEVSIX-3243 fixed need to assert exact language and check assertEquals
            String html = sourceFolder + "langAttrInDivAndSpanForTagPdfTest.html";
            String outFile = destinationFolder + "langAttrInDivAndSpanForTagPdfTest.pdf";
            String cmp = sourceFolder + "cmp_langAttrInDivAndSpanForTagPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            //compareByContent is used here to check the complete logical structure tree to notice all the differences.
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmp, destinationFolder, "diff_Table"
                ));
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            pdfDocument.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInFormForTaggedPdfTest() {
            //TODO: after DEVSIX-3243 fixed need to assert exact language and check assertEquals
            String html = sourceFolder + "langAttrInFormForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInFormForTaggedPdfTest.pdf";
            String cmp = sourceFolder + "cmp_langAttrInFormForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, new ConverterProperties
                ().SetBaseUri(sourceFolder));
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            //compareByContent is used here to check the complete logical structure tree to notice all the differences.
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmp, destinationFolder, "diff_forms"
                ));
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }
    }
}
