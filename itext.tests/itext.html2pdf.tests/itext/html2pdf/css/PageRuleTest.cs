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
using NUnit.Framework;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Html;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Node;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class PageRuleTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/PageRuleTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/PageRuleTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void MarksCropCrossPageRuleTest() {
            RunTest("marksCropCrossPageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void MarksCropPageRuleTest() {
            RunTest("marksCropPageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void MarksCrossPageRuleTest() {
            RunTest("marksCrossPageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void MarksInvalidPageRuleTest() {
            RunTest("marksInvalidPageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void MarksNonePageRuleTest() {
            RunTest("marksNonePageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void PaddingPageRuleTest() {
            RunTest("paddingPageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void CompoundSizePageRuleTest() {
            RunTest("compoundSizePageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void BleedPageRuleTest() {
            RunTest("bleedPageRuleTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.PAGE_SIZE_VALUE_IS_INVALID, Count = 3)]
        public virtual void InvalidCompoundSizePageRuleTest() {
            RunTest("invalidCompoundSizePageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void NotAllMarginsPageRuleTest() {
            RunTest("notAllMarginsPageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void FirstLeftRightPageRuleTest() {
            RunTest("firstLeftRightPageRuleTest");
        }

        [NUnit.Framework.Test]
        public virtual void MarksBleedPageRuleTest() {
            RunTest("marksBleedPageRuleTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 3)]
        public virtual void MarginBoxTest01() {
            RunTest("marginBoxTest01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTest02() {
            RunTest("marginBoxTest02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTest03() {
            RunTest("marginBoxTest03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTest04() {
            RunTest("marginBoxTest04");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTestImg01() {
            RunTest("marginBoxTestImg01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTestImg02() {
            RunTest("marginBoxTestImg02");
        }

        [NUnit.Framework.Test]
        public virtual void BigImageOnPageMarginTest01() {
            RunTest("bigImageOnPageMarginTest01");
        }

        [NUnit.Framework.Test]
        public virtual void BigImageOnPageMarginTest02() {
            RunTest("bigImageOnPageMarginTest02");
        }

        [NUnit.Framework.Test]
        public virtual void BigImageOnPageMarginTest03() {
            RunTest("bigImageOnPageMarginTest03", new ConverterProperties().SetTagWorkerFactory(new PageRuleTest.PageMarginBoxImagesTagWorkerFactory
                ()));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 4, LogLevel = LogLevelConstants.WARN)]
        public virtual void LinearGradientOnPageMarginWithAutoWidthAndHeightTest() {
            RunTest("linearGradientOnPageMarginWithAutoWidthAndHeightTest", new ConverterProperties().SetTagWorkerFactory
                (new PageRuleTest.PageMarginBoxImagesTagWorkerFactory()));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 2, LogLevel = LogLevelConstants.WARN)]
        public virtual void LinearGradientOnPageMarginWithPercentWidthAndHeightTest() {
            RunTest("linearGradientOnPageMarginWithPercentWidthAndHeightTest", new ConverterProperties().SetTagWorkerFactory
                (new PageRuleTest.PageMarginBoxImagesTagWorkerFactory()));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.CLIP_ELEMENT, Count = 2, LogLevel = LogLevelConstants.WARN)]
        public virtual void LinearGradientOnPageMarginWithWidthAndHeightTest() {
            RunTest("linearGradientOnPageMarginWithWidthAndHeightTest", new ConverterProperties().SetTagWorkerFactory(
                new PageRuleTest.PageMarginBoxImagesTagWorkerFactory()));
        }

        private class PageMarginBoxImagesTagWorkerFactory : DefaultTagWorkerFactory {
            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                if (tag.Name().Equals(PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG)) {
                    return new PageRuleTest.PageMarginBoxImagesWorker(tag, context);
                }
                return base.GetCustomTagWorker(tag, context);
            }
        }

        private class PageMarginBoxImagesWorker : PageMarginBoxWorker {
            public PageMarginBoxImagesWorker(IElementNode element, ProcessorContext context)
                : base(element, context) {
            }

            public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
                if (childTagWorker.GetElementResult() is Image) {
                    // TODO Since iText 7.2 release it is ("it will be" for now, see PageMarginBoxDummyElement class) possible
                    // to get current page margin box name and dimensions from the "element" IElementNode passed to the constructor of this tag worker.
                    ((Image)childTagWorker.GetElementResult()).SetAutoScale(true);
                }
                return base.ProcessTagChild(childTagWorker, context);
            }
        }

        [NUnit.Framework.Test]
        public virtual void BigTextOnPageMarginTest01() {
            RunTest("bigTextOnPageMarginTest01");
        }

        [NUnit.Framework.Test]
        public virtual void BigTextOnPageMarginTest02() {
            RunTest("bigTextOnPageMarginTest02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxOverflowPropertyTest01() {
            RunTest("marginBoxOverflowPropertyTest01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxOverflowPropertyTest02() {
            RunTest("marginBoxOverflowPropertyTest02", new ConverterProperties().SetCssApplierFactory(new PageRuleTest.PageMarginsOverflowCssApplierFactory
                ()));
        }

        private class PageMarginsOverflowCssApplierFactory : DefaultCssApplierFactory {
            public override ICssApplier GetCustomCssApplier(IElementNode tag) {
                if (PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG.Equals(tag.Name())) {
                    return new PageRuleTest.CustomOverflowPageMarginBoxCssApplier();
                }
                return base.GetCustomCssApplier(tag);
            }
        }

        private class CustomOverflowPageMarginBoxCssApplier : PageMarginBoxCssApplier {
            public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
                ) {
                IDictionary<String, String> styles = stylesContainer.GetStyles();
                if (styles.Get(CssConstants.OVERFLOW) == null) {
                    styles.Put(CssConstants.OVERFLOW, CommonCssConstants.VISIBLE);
                }
                base.Apply(context, stylesContainer, tagWorker);
            }
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxOutlinePropertyTest01() {
            // TODO Outlines are currently not supported for page margin boxes, because of the outlines handling specificity (they are handled on renderer's parent level).
            //      See com.itextpdf.html2pdf.attach.impl.layout.PageContextProcessor.
            RunTest("marginBoxOutlinePropertyTest01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTest01() {
            RunTest("marginBoxRunningTest01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTest02() {
            RunTest("marginBoxRunningTest02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTest03() {
            RunTest("marginBoxRunningTest03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTest04() {
            // TODO This tests shows wrong result, because running element name is custom-ident which shall be case sensitive, while iText treats it as case-insensitive.
            RunTest("marginBoxRunningTest04");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTest05() {
            RunTest("marginBoxRunningTest05");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTest06() {
            RunTest("marginBoxRunningTest06");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CONTENT_PROPERTY_INVALID)]
        public virtual void MarginBoxRunningTest07() {
            RunTest("marginBoxRunningTest07");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTest08() {
            RunTest("marginBoxRunningTest08");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest01() {
            RunTest("marginBoxRunningOverrideTest01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest02() {
            RunTest("marginBoxRunningOverrideTest02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest03() {
            RunTest("marginBoxRunningOverrideTest03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest04() {
            RunTest("marginBoxRunningOverrideTest04");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest05() {
            RunTest("marginBoxRunningOverrideTest05");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest06() {
            RunTest("marginBoxRunningOverrideTest06");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest07() {
            RunTest("marginBoxRunningOverrideTest07");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOverrideTest08() {
            RunTest("marginBoxRunningOverrideTest08");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningImg01() {
            RunTest("marginBoxRunningImg01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningImg02() {
            RunTest("marginBoxRunningImg02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningTable01() {
            RunTest("marginBoxRunningTable01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningLink01() {
            RunTest("marginBoxRunningLink01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningLink02() {
            RunTest("marginBoxRunningLink02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningLink03() {
            RunTest("marginBoxRunningLink03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningElements01() {
            RunTest("marginBoxRunningElements01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningElements02() {
            RunTest("marginBoxRunningElements02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningElements03() {
            RunTest("marginBoxRunningElements03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningElements04() {
            RunTest("marginBoxRunningElements04");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent01() {
            RunTest("marginBoxRunningParent01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent02() {
            RunTest("marginBoxRunningParent02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent03() {
            RunTest("marginBoxRunningParent03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent04() {
            RunTest("marginBoxRunningParent04");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent05() {
            RunTest("marginBoxRunningParent05");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent06() {
            RunTest("marginBoxRunningParent06");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent07() {
            RunTest("marginBoxRunningParent07");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningParent08() {
            RunTest("marginBoxRunningParent08");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningPageBreak01() {
            RunTest("marginBoxRunningPageBreak01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningPageBreak02() {
            RunTest("marginBoxRunningPageBreak02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningPageBreak03() {
            RunTest("marginBoxRunningPageBreak03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningPageBreakAvoid01() {
            RunTest("marginBoxRunningPageBreakAvoid01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOutlines01() {
            RunTest("marginBoxRunningOutlines01", new ConverterProperties().SetOutlineHandler(OutlineHandler.CreateStandardHandler
                ()));
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningOutlines02() {
            RunTest("marginBoxRunningOutlines02", new ConverterProperties().SetOutlineHandler(OutlineHandler.CreateStandardHandler
                ()));
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningQuotes01() {
            RunTest("marginBoxRunningQuotes01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningQuotes02() {
            RunTest("marginBoxRunningQuotes02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningQuotes03() {
            RunTest("marginBoxRunningQuotes03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningQuotes04() {
            RunTest("marginBoxRunningQuotes04");
        }

        [NUnit.Framework.Test]
        public virtual void CheckMarginBoxFixDimension() {
            RunTest("checkMarginBoxFixDimension");
        }

        [NUnit.Framework.Test]
        public virtual void CheckMarginBoxMaxMinDimension() {
            RunTest("checkMarginBoxMaxMinDimension");
        }

        [NUnit.Framework.Test]
        public virtual void CheckMarginBoxMarginPaddings() {
            RunTest("checkMarginBoxMarginPaddings");
        }

        [NUnit.Framework.Test]
        public virtual void MediaAppliedToRunningElementsProperties() {
            MediaDeviceDescription printMediaDevice = new MediaDeviceDescription("print");
            ConverterProperties converterProperties = new ConverterProperties().SetMediaDeviceDescription(printMediaDevice
                );
            RunTest("mediaAppliedToRunningElementsProperties", converterProperties);
        }

        [NUnit.Framework.Test]
        public virtual void MediaNotAppliedToRunningElementsProperties() {
            RunTest("mediaNotAppliedToRunningElementsProperties");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTaggedTest01() {
            RunTest("marginBoxTaggedTest01", null, true);
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTaggedTest02() {
            RunTest("marginBoxTaggedTest02", null, true);
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTaggedTest03() {
            RunTest("marginBoxTaggedTest03", new ConverterProperties().SetTagWorkerFactory(new PageRuleTest.TaggedPageMarginBoxTagWorkerFactory
                ()), true);
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxTaggedTest04() {
            RunTest("marginBoxTaggedTest04", new ConverterProperties().SetTagWorkerFactory(new PageRuleTest.TaggedPageMarginBoxTagWorkerFactory
                ()), true);
        }

        private class TaggedPageMarginBoxTagWorkerFactory : DefaultTagWorkerFactory {
            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                if (tag.Name().Equals(PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG)) {
                    return new PageRuleTest.TaggedPageMarginBoxWorker(tag, context);
                }
                return base.GetCustomTagWorker(tag, context);
            }
        }

        private class TaggedPageMarginBoxWorker : PageMarginBoxWorker {
            public TaggedPageMarginBoxWorker(IElementNode element, ProcessorContext context)
                : base(element, context) {
            }

            public override void ProcessEnd(IElementNode element, ProcessorContext context) {
                base.ProcessEnd(element, context);
                if (GetElementResult() is IAccessibleElement) {
                    ((IAccessibleElement)GetElementResult()).GetAccessibilityProperties().SetRole(StandardRoles.DIV);
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningNoImmediateFlush01() {
            String name = "marginBoxRunningNoImmediateFlush01";
            String htmlPath = SOURCE_FOLDER + name + ".html";
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            Document doc = HtmlConverter.ConvertToDocument(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), new 
                PdfWriter(pdfPath), converterProperties);
            doc.Close();
            CompareResult(name);
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningNoImmediateFlush02() {
            String name = "marginBoxRunningNoImmediateFlush02";
            String htmlPath = SOURCE_FOLDER + name + ".html";
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            Document doc = HtmlConverter.ConvertToDocument(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), new 
                PdfWriter(pdfPath), converterProperties);
            doc.SetMargins(120f, 120f, 120f, 120f);
            doc.Relayout();
            doc.Relayout();
            // relayouting second time in order to fix total number of pages
            doc.Close();
            CompareResult(name);
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningNoImmediateFlush03() {
            String name = "marginBoxRunningNoImmediateFlush03";
            String htmlPath = SOURCE_FOLDER + name + ".html";
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            Document doc = HtmlConverter.ConvertToDocument(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), new 
                PdfWriter(pdfPath), converterProperties);
            // TODO This is kinda a workaround, because calling document.close() would close the whole document,
            // which would forbid any further operations with it, however in html2pdf some things are waiting for document to be closed and finished:
            // - adding last waiting element (connected with keep_with_previous functionality);
            // - drawing margin boxes for the last page.
            doc.GetRenderer().Close();
            int pagesNum = doc.GetPdfDocument().GetNumberOfPages();
            NUnit.Framework.Assert.IsTrue(pagesNum > 1);
            int k = 1;
            for (int i = pagesNum; i > 0; --i) {
                doc.GetPdfDocument().MovePage(pagesNum, k++);
            }
            doc.GetPdfDocument().Close();
            // closing PdfDocument directly, in order to avoid second call for document renderer closing
            CompareResult(name);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.REMOVING_PAGE_HAS_ALREADY_BEEN_FLUSHED, Count = 6)]
        public virtual void MarginBoxRunningNoImmediateFlush04() {
            String name = "marginBoxRunningNoImmediateFlush04";
            String htmlPath = SOURCE_FOLDER + name + ".html";
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            Document doc = HtmlConverter.ConvertToDocument(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), new 
                PdfWriter(pdfPath), converterProperties);
            doc.Flush();
            for (int i = 1; i <= doc.GetPdfDocument().GetNumberOfPages(); ++i) {
                doc.GetPdfDocument().GetPage(i).Flush();
            }
            doc.Relayout();
            doc.Close();
            CompareResult(name);
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxRunningNoImmediateFlush05() {
            String name = "marginBoxRunningNoImmediateFlush05";
            String htmlPath = SOURCE_FOLDER + name + ".html";
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            converterProperties.SetTagWorkerFactory(new PageRuleTest.CustomFlushingTagWorkerFactory());
            HtmlConverter.ConvertToPdf(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), new PdfWriter(pdfPath
                ), converterProperties);
            CompareResult(name);
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxMultilineTest01() {
            RunTest("marginBoxMultilineTest01");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxMultilineTest02() {
            RunTest("marginBoxMultilineTest02");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxMultilineTest03() {
            RunTest("marginBoxMultilineTest03");
        }

        [NUnit.Framework.Test]
        public virtual void MarginBoxMultilineTest04() {
            RunTest("marginBoxMultilineTest04");
        }

        [NUnit.Framework.Test]
        public virtual void WrongPageRuleCssStructureTest() {
            NUnit.Framework.Assert.That(() =>  {
                RunTest("wrongPageRuleCssStructureTest");
            }
            , NUnit.Framework.Throws.InstanceOf<Exception>())
;
        }

        [NUnit.Framework.Test]
        public virtual void PageCountTestTableAlignLeft() {
            NUnit.Framework.Assert.That(() =>  {
                //TODO: DEVSIX-1570, SUP-2322. Remove junitExpectedException after the fix.
                String expectedText = "Page 1 of 3";
                String nameAlignLeft = "htmlWithTableAlignLeft.html";
                String pdfOutputName = DESTINATION_FOLDER + nameAlignLeft + ".pdf";
                FileInfo pdfOutputAlignLeft = new FileInfo(pdfOutputName);
                HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + nameAlignLeft), pdfOutputAlignLeft);
                PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfOutputAlignLeft));
                String textFromPage = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(1));
                // Print the output file. No comparison will be made on the following line
                ExtendedITextTest.PrintOutputPdfNameAndDir(pdfOutputName);
                NUnit.Framework.Assert.IsTrue(textFromPage.Contains(expectedText), "Page doesn't contain text " + expectedText
                    );
            }
            , NUnit.Framework.Throws.InstanceOf<AssertionException>())
;
        }

        [NUnit.Framework.Test]
        public virtual void PageCountTestTableFloatLeft() {
            NUnit.Framework.Assert.That(() =>  {
                //TODO: DEVSIX-1570, SUP-2322. Remove junitExpectedException after the fix.
                String expectedText = "Page 3 of 3";
                String nameFloatLeft = "htmlWithTableFloatLeft.html";
                String pdfOutputName = DESTINATION_FOLDER + nameFloatLeft + ".pdf";
                FileInfo pdfOutputFloatLeft = new FileInfo(pdfOutputName);
                HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + nameFloatLeft), pdfOutputFloatLeft);
                PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfOutputFloatLeft));
                String textFromPage = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(3));
                // Print the output file. No comparison will be made on the following line
                ExtendedITextTest.PrintOutputPdfNameAndDir(pdfOutputName);
                NUnit.Framework.Assert.IsTrue(textFromPage.Contains(expectedText), "Page doesn't contain text " + expectedText
                    );
            }
            , NUnit.Framework.Throws.InstanceOf<AssertionException>())
;
        }

        private class CustomFlushingTagWorkerFactory : DefaultTagWorkerFactory {
            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                if (tag.Name().Equals(TagConstants.HTML)) {
                    return new PageRuleTest.CustomFlushingHtmlTagWorker(tag, context);
                }
                return base.GetCustomTagWorker(tag, context);
            }
        }

        private class CustomFlushingHtmlTagWorker : HtmlTagWorker {
            public CustomFlushingHtmlTagWorker(IElementNode tag, ProcessorContext context)
                : base(tag, context) {
            }

            public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
                Document document = (Document)GetElementResult();
                if (document.GetPdfDocument().GetNumberOfPages() % 3 == 0) {
                    document.Flush();
                    for (int i = 1; i < document.GetPdfDocument().GetNumberOfPages(); ++i) {
                        document.GetPdfDocument().GetPage(i).Flush();
                    }
                }
                return base.ProcessTagChild(childTagWorker, context);
            }
        }

        private void RunTest(String name) {
            RunTest(name, null);
        }

        private void RunTest(String name, ConverterProperties converterProperties) {
            try {
                RunTest(name, converterProperties, false);
            }
            catch (Exception e) {
                throw new Exception(e.Message, e);
            }
        }

        private void RunTest(String name, ConverterProperties converterProperties, bool isTagged) {
            String htmlPath = SOURCE_FOLDER + name + ".html";
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            String cmpPdfPath = SOURCE_FOLDER + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            FileInfo outFile = new FileInfo(pdfPath);
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            if (converterProperties == null) {
                converterProperties = new ConverterProperties();
            }
            if (converterProperties.GetBaseUri() == null) {
                converterProperties.SetBaseUri(UrlUtil.GetFileUriString(htmlPath));
            }
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFile));
            if (isTagged) {
                pdfDocument.SetTagged();
            }
            HtmlConverter.ConvertToPdf(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), pdfDocument, converterProperties
                );
            CompareTool compareTool = new CompareTool();
            if (isTagged) {
                compareTool.CompareTagStructures(pdfPath, cmpPdfPath);
            }
            NUnit.Framework.Assert.IsNull(compareTool.CompareByContent(pdfPath, cmpPdfPath, DESTINATION_FOLDER, diffPrefix
                ));
        }

        private void CompareResult(String name) {
            String pdfPath = DESTINATION_FOLDER + name + ".pdf";
            String cmpPdfPath = SOURCE_FOLDER + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, DESTINATION_FOLDER, 
                diffPrefix));
        }
    }
}
