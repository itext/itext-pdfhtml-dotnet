using System;
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Attach.Impl {
    public class OutlineHandlerTest : ExtendedHtmlConversionITextTest {
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
            NUnit.Framework.Assert.AreEqual("pdfHTML-iText-outline-pdfHTML-iText-outline-1", pdfStringDest.ToUnicodeString
                ());
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
            NUnit.Framework.Assert.AreEqual("prefix-prefix-1", pdfStringDest.ToUnicodeString());
        }
    }
}
