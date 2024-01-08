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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Attach.Impl {
    [NUnit.Framework.Category("IntegrationTest")]
    public class OutlineHandlerTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attach/impl/OutlineHandlerTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attach/impl/OutlineHandlerTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DefaultDestinationPrefixTest() {
            IDictionary<String, int?> priorityMappings = new Dictionary<String, int?>();
            priorityMappings.Put("p", 1);
            OutlineHandler outlineHandler = new OutlineHandler().PutAllTagPriorityMappings(priorityMappings);
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetOutlineHandler(outlineHandler
                ));
            context.Reset(new PdfDocument(new PdfWriter(new MemoryStream())));
            IElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.P), TagConstants.P));
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.NORMAL);
            styles.Put(CssConstants.TEXT_TRANSFORM, CssConstants.LOWERCASE);
            // Styles are required in the constructor of the PTagWorker class
            elementNode.SetStyles(styles);
            outlineHandler.AddOutlineAndDestToDocument(new PTagWorker(elementNode, context), elementNode, context);
            PdfOutline pdfOutline = context.GetPdfDocument().GetOutlines(false).GetAllChildren()[0];
            NUnit.Framework.Assert.AreEqual("p1", pdfOutline.GetTitle());
            PdfString pdfStringDest = (PdfString)pdfOutline.GetDestination().GetPdfObject();
            NUnit.Framework.Assert.AreEqual("pdfHTML-iText-outline-1", pdfStringDest.ToUnicodeString());
        }

        [NUnit.Framework.Test]
        public virtual void CustomDestinationPrefixTest() {
            IDictionary<String, int?> priorityMappings = new Dictionary<String, int?>();
            priorityMappings.Put("p", 1);
            OutlineHandler outlineHandler = new OutlineHandler().PutAllTagPriorityMappings(priorityMappings);
            outlineHandler.SetDestinationNamePrefix("prefix-");
            NUnit.Framework.Assert.AreEqual("prefix-", outlineHandler.GetDestinationNamePrefix());
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetOutlineHandler(outlineHandler
                ));
            context.Reset(new PdfDocument(new PdfWriter(new MemoryStream())));
            IElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.P), TagConstants.P));
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.NORMAL);
            styles.Put(CssConstants.TEXT_TRANSFORM, CssConstants.LOWERCASE);
            // Styles are required in the constructor of the PTagWorker class
            elementNode.SetStyles(styles);
            outlineHandler.AddOutlineAndDestToDocument(new PTagWorker(elementNode, context), elementNode, context);
            PdfOutline pdfOutline = context.GetPdfDocument().GetOutlines(false).GetAllChildren()[0];
            NUnit.Framework.Assert.AreEqual("p1", pdfOutline.GetTitle());
            PdfString pdfStringDest = (PdfString)pdfOutline.GetDestination().GetPdfObject();
            NUnit.Framework.Assert.AreEqual("prefix-1", pdfStringDest.ToUnicodeString());
        }

        [NUnit.Framework.Test]
        public virtual void DefaultOutlineHandlerWithHTagHavingIdTest() {
            // TODO DEVSIX-5195 fix cmp after fix is introduced
            String inFile = SOURCE_FOLDER + "defaultOutlineHandlerWithHTagHavingIdTest.html";
            String outFile = DESTINATION_FOLDER + "defaultOutlineHandlerWithHTagHavingIdTest.pdf";
            String cmpFile = SOURCE_FOLDER + "cmp_defaultOutlineHandlerWithHTagHavingIdTest.pdf";
            OutlineHandler outlineHandler = OutlineHandler.CreateStandardHandler();
            HtmlConverter.ConvertToPdf(new FileInfo(inFile), new FileInfo(outFile), new ConverterProperties().SetOutlineHandler
                (outlineHandler));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmpFile, DESTINATION_FOLDER, "diff_defaultOutlineHandlerWithHTagHavingIdTest"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ResetOutlineHandlerTest() {
            OutlineHandler outlineHandler = OutlineHandler.CreateStandardHandler();
            for (int i = 1; i <= 3; i++) {
                String srcHtml = SOURCE_FOLDER + "outlines0" + i + ".html";
                String outPdf = DESTINATION_FOLDER + "outlines0" + i + ".pdf";
                String cmpPdf = SOURCE_FOLDER + "cmp_outlines0" + i + ".pdf";
                HtmlConverter.ConvertToPdf(new FileInfo(srcHtml), new FileInfo(outPdf), new ConverterProperties().SetOutlineHandler
                    (outlineHandler));
                NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER, "diff_"
                     + i));
            }
        }

        [NUnit.Framework.Test]
        public virtual void CapitalHeadingLevelTest() {
            String inFile = SOURCE_FOLDER + "capitalHeadingLevel.html";
            String outFile = DESTINATION_FOLDER + "capitalHeadingLevel.pdf";
            String cmpFile = SOURCE_FOLDER + "cmp_capitalHeadingLevel.pdf";
            OutlineHandler outlineHandler = OutlineHandler.CreateStandardHandler();
            HtmlConverter.ConvertToPdf(new FileInfo(inFile), new FileInfo(outFile), new ConverterProperties().SetOutlineHandler
                (outlineHandler));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFile, cmpFile, DESTINATION_FOLDER, "diff_capitalHeadingLevelOne"
                ));
        }
    }
}
