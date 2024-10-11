/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Pdf.Tagutils;
using iText.Kernel.Utils;
using iText.Layout.Element;
using iText.Test.Attributes;

namespace iText.Html2pdf.Attribute {
    [NUnit.Framework.Category("IntegrationTest")]
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
            String html = sourceFolder + "langAttrInElementForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInElementForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("en", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInvalidTagsTest() {
            String html = sourceFolder + "langAttrInvalidTagsTest.html";
            String outFile = destinationFolder + "langAttrInvalidTagsTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("a-DE", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("DE", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(3, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("de-419-DE", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(4, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("en-gb", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(5, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("fr-brai", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(6, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("fr-BRAI", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrEmptyTagTest() {
            String html = sourceFolder + "langAttrEmptyTagTest.html";
            String outFile = destinationFolder + "langAttrEmptyTagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(0);
            NUnit.Framework.Assert.AreEqual("", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(1).MoveToKid(0).MoveToKid(StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(1).MoveToKid(2).MoveToKid(StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2).MoveToKid(0).MoveToKid(StandardRoles.TD);
            NUnit.Framework.Assert.AreEqual("", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2).MoveToKid(1).MoveToKid(StandardRoles.TD);
            NUnit.Framework.Assert.AreEqual("", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrRegionSubtagTest() {
            String html = sourceFolder + "langAttrRegionSubtagTest.html";
            String outFile = destinationFolder + "langAttrRegionSubtagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("de-DE", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("en-US", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(3, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("es-419", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrScriptSubtagTest() {
            String html = sourceFolder + "langAttrScriptSubtagTest.html";
            String outFile = destinationFolder + "langAttrScriptSubtagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("fr-Brai", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("ru-Latn", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(3, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("sr-Cyrl", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrScriptRegionSubtagTest() {
            String html = sourceFolder + "langAttrScriptRegionSubtagTest.html";
            String outFile = destinationFolder + "langAttrScriptRegionSubtagTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(1, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("zh-Hans-CN", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P);
            NUnit.Framework.Assert.AreEqual("ru-Cyrl-BY", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInSvgForTaggedPdfTest() {
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
            NUnit.Framework.Assert.AreEqual("en", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(2, StandardRoles.P).MoveToKid(StandardRoles.FIGURE);
            NUnit.Framework.Assert.AreEqual("fr", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot().MoveToKid(3, StandardRoles.P).MoveToKid(StandardRoles.FIGURE);
            NUnit.Framework.Assert.AreEqual("ru", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInListsForTaggedPdfTest() {
            String html = sourceFolder + "langAttrInListsForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInListsForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(0, StandardRoles.L);
            NUnit.Framework.Assert.AreEqual("en", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToKid(2, StandardRoles.LI).MoveToKid(0, StandardRoles.LBODY);
            NUnit.Framework.Assert.AreEqual("", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot();
            tagPointer.MoveToKid(1, StandardRoles.L);
            tagPointer.GetProperties().GetLanguage();
            NUnit.Framework.Assert.AreEqual("de", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot();
            tagPointer.MoveToKid(2, StandardRoles.L).MoveToKid(0, StandardRoles.LI).MoveToKid(0, StandardRoles.LBODY);
            NUnit.Framework.Assert.AreEqual("en", tagPointer.GetProperties().GetLanguage());
            tagPointer.MoveToRoot();
            tagPointer.MoveToKid(2, StandardRoles.L).MoveToKid(1, StandardRoles.LI).MoveToKid(0, StandardRoles.LBODY);
            NUnit.Framework.Assert.AreEqual("de", tagPointer.GetProperties().GetLanguage());
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInListWithBeforeStyleForTaggedPdfTest() {
            String html = sourceFolder + "langAttrInListWithBeforeStyleForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInListWithBeforeStyleForTaggedPdfTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, null);
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            TagTreePointer tagPointer = new TagTreePointer(document);
            tagPointer.MoveToKid(0, StandardRoles.L).MoveToKid(1, StandardRoles.LI).MoveToKid(0, StandardRoles.LBODY);
            NUnit.Framework.Assert.AreEqual("de", tagPointer.GetProperties().GetLanguage());
            IList<String> kidsRoles = tagPointer.MoveToKid(StandardRoles.P).GetKidsRoles();
            NUnit.Framework.Assert.IsTrue(StandardRoles.SPAN.Equals(kidsRoles[0]) && StandardRoles.SPAN.Equals(kidsRoles
                [1]));
            NUnit.Framework.Assert.IsNull(document.GetCatalog().GetLang());
            document.Close();
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ATTEMPT_TO_CREATE_A_TAG_FOR_FINISHED_HINT, Count = 1)]
        public virtual void LangAttrInDivAndSpanForTagPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInDivAndSpanForTagPdfTest");
            NUnit.Framework.Assert.AreEqual("ru", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInDivAndSpanForConvertToElementsMethodTest() {
            String html = sourceFolder + "langAttrInDivAndSpanForTagPdfTest.html";
            IList<IElement> elemList = HtmlConverter.ConvertToElements(new FileStream(html, FileMode.Open, FileAccess.Read
                ));
            Div div = (Div)elemList[0];
            NUnit.Framework.Assert.AreEqual("la", div.GetAccessibilityProperties().GetLanguage());
            Paragraph p = (Paragraph)div.GetChildren()[0];
            NUnit.Framework.Assert.IsNull(p.GetAccessibilityProperties().GetLanguage());
            div = (Div)div.GetChildren()[1];
            NUnit.Framework.Assert.AreEqual("en", div.GetAccessibilityProperties().GetLanguage());
            div = (Div)elemList[1];
            NUnit.Framework.Assert.AreEqual("ru", div.GetAccessibilityProperties().GetLanguage());
            p = (Paragraph)div.GetChildren()[0];
            NUnit.Framework.Assert.IsNull(p.GetAccessibilityProperties().GetLanguage());
            div = (Div)div.GetChildren()[1];
            NUnit.Framework.Assert.IsNull(div.GetAccessibilityProperties().GetLanguage());
            p = (Paragraph)elemList[2];
            Text text = (Text)p.GetChildren()[0];
            NUnit.Framework.Assert.IsNull(text.GetAccessibilityProperties().GetLanguage());
            text = (Text)p.GetChildren()[1];
            NUnit.Framework.Assert.AreEqual("ru", text.GetAccessibilityProperties().GetLanguage());
            text = (Text)p.GetChildren()[2];
            NUnit.Framework.Assert.AreEqual("en", text.GetAccessibilityProperties().GetLanguage());
            text = (Text)p.GetChildren()[3];
            NUnit.Framework.Assert.AreEqual("ru", text.GetAccessibilityProperties().GetLanguage());
            text = (Text)p.GetChildren()[4];
            NUnit.Framework.Assert.IsNull(text.GetAccessibilityProperties().GetLanguage());
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInFormFieldsetAndLegendForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInFormFieldsetAndLegendForTaggedPdfTest");
            NUnit.Framework.Assert.IsNull(doc.GetCatalog().GetLang());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInInputAndTextareaForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInInputAndTextareaForTaggedPdfTest");
            NUnit.Framework.Assert.AreEqual("da", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInButtonForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInButtonForTaggedPdfTest");
            NUnit.Framework.Assert.AreEqual("da", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInInputAndTextareaForTaggedPdfWithActoformTest() {
            String html = sourceFolder + "langAttrInInputAndTextareaForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInInputAndTextareaForTaggedPdfWithActoformTest.pdf";
            String cmp = sourceFolder + "cmp_langAttrInInputAndTextareaForTaggedPdfWithActoformTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetCreateAcroForm(true);
            converterProperties.SetBaseUri(sourceFolder);
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            //compareByContent is used here to check the complete logical structure tree to notice all the differences.
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmp, destinationFolder, "diff_forms"
                ));
            NUnit.Framework.Assert.AreEqual("da", document.GetCatalog().GetLang().ToUnicodeString());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInButtonForTaggedPdfWithActoformTest() {
            String html = sourceFolder + "langAttrInButtonForTaggedPdfTest.html";
            String outFile = destinationFolder + "langAttrInButtonForTaggedPdfWithActoformTest.pdf";
            String cmp = sourceFolder + "cmp_langAttrInButtonForTaggedPdfWithActoformTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetCreateAcroForm(true);
            converterProperties.SetBaseUri(sourceFolder);
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            //compareByContent is used here to check the complete logical structure tree to notice all the differences.
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmp, destinationFolder, "diff_forms"
                ));
            NUnit.Framework.Assert.AreEqual("da", document.GetCatalog().GetLang().ToUnicodeString());
            document.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInTableForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInTableForTaggedPdfTest");
            NUnit.Framework.Assert.AreEqual("da", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInTableWithColgroupForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInTableWithColgroupForTaggedPdfTest");
            NUnit.Framework.Assert.IsNull(doc.GetCatalog().GetLang());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInTableWithColgroupTheadAndTfootForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInTableWithColgroupTheadAndTfootForTaggedPdfTest");
            NUnit.Framework.Assert.AreEqual("da", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInTableWithTheadAndColgroupForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInTableWithTheadAndColgroupForTaggedPdfTest");
            NUnit.Framework.Assert.AreEqual("da", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInTableWithTheadTfootWithLangForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInTableWithTheadTfootWithLangForTaggedPdfTest");
            NUnit.Framework.Assert.AreEqual("da", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInTableWithTheadWithLangAndTfootForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInTableWithTheadWithLangAndTfootForTaggedPdfTest");
            NUnit.Framework.Assert.AreEqual("da", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInSelectsWithLangAndOneOptionForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInSelectsWithLangAndOneOptionForTaggedPdfTest");
            NUnit.Framework.Assert.IsNull(doc.GetCatalog().GetLang());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInSelectsWithSeveralOptionForTaggedPdfTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInSelectsWithSeveralOptionForTaggedPdfTest");
            NUnit.Framework.Assert.IsNull(doc.GetCatalog().GetLang());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInHtmlWithLangBodyWithoutLangTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInHtmlWithLangBodyWithoutLangTest");
            NUnit.Framework.Assert.AreEqual("ru", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInHtmlWithLangBodyWithoutLangForConvertToElementsMethodTest() {
            String html = sourceFolder + "langAttrInHtmlWithLangBodyWithoutLangTest.html";
            IList<IElement> elemList = HtmlConverter.ConvertToElements(new FileStream(html, FileMode.Open, FileAccess.Read
                ));
            Paragraph p = (Paragraph)elemList[0];
            NUnit.Framework.Assert.AreEqual("ru", p.GetAccessibilityProperties().GetLanguage());
            p = (Paragraph)elemList[1];
            NUnit.Framework.Assert.AreEqual("en", p.GetAccessibilityProperties().GetLanguage());
            Div div = (Div)elemList[2];
            NUnit.Framework.Assert.AreEqual("ru", div.GetAccessibilityProperties().GetLanguage());
            p = (Paragraph)elemList[3];
            NUnit.Framework.Assert.AreEqual("ru", p.GetAccessibilityProperties().GetLanguage());
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInHtmlWithoutLangBodyWithLangTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInHtmlWithoutLangBodyWithLangTest");
            NUnit.Framework.Assert.AreEqual("ru", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInHtmlWithLangBodyWithLangTest() {
            PdfDocument doc = CompareResultWithDocument("langAttrInHtmlWithLangBodyWithLangTest");
            NUnit.Framework.Assert.AreEqual("by", doc.GetCatalog().GetLang().ToUnicodeString());
            doc.Close();
        }

        [NUnit.Framework.Test]
        public virtual void LangAttrInHtmlWithLangBodyWithLangForConvertToElementsMethodTest() {
            String html = sourceFolder + "langAttrInHtmlWithLangBodyWithLangTest.html";
            IList<IElement> elemList = HtmlConverter.ConvertToElements(new FileStream(html, FileMode.Open, FileAccess.Read
                ));
            Paragraph p = (Paragraph)elemList[0];
            NUnit.Framework.Assert.AreEqual("by", p.GetAccessibilityProperties().GetLanguage());
            p = (Paragraph)elemList[1];
            NUnit.Framework.Assert.AreEqual("en", p.GetAccessibilityProperties().GetLanguage());
            Div div = (Div)elemList[2];
            NUnit.Framework.Assert.AreEqual("by", div.GetAccessibilityProperties().GetLanguage());
            p = (Paragraph)elemList[3];
            NUnit.Framework.Assert.AreEqual("by", p.GetAccessibilityProperties().GetLanguage());
        }

        private PdfDocument CompareResultWithDocument(String fileName) {
            String html = sourceFolder + fileName + ".html";
            String outFile = destinationFolder + fileName + ".pdf";
            String cmp = sourceFolder + "cmp_" + fileName + ".pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(html, FileMode.Open, FileAccess.Read), pdfDocument, new ConverterProperties
                ().SetBaseUri(sourceFolder));
            PrintOutputPdfNameAndDir(outFile);
            PdfDocument document = new PdfDocument(new PdfReader(outFile));
            // compareByContent is used here to check the complete logical structure tree to notice all the differences.
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmp, destinationFolder, "diff_test"
                ));
            return document;
        }
    }
}
