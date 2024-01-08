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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Forms.Form.Element;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>html</c>
    /// element.
    /// </summary>
    public class HtmlTagWorker : ITagWorker {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.HtmlTagWorker
            ));

        /// <summary>The iText document instance.</summary>
        private Document document;

        /// <summary>Helper class for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>
        /// Creates a new
        /// <see cref="HtmlTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public HtmlTagWorker(IElementNode element, ProcessorContext context) {
            // TODO DEVSIX-4261 more precise check if a counter was actually added to the document
            bool immediateFlush = context.IsImmediateFlush() && !context.GetCssContext().IsPagesCounterPresent() && !context
                .IsCreateAcroForm();
            if (context.IsImmediateFlush() && context.IsCreateAcroForm()) {
                LOGGER.LogInformation(Html2PdfLogMessageConstant.IMMEDIATE_FLUSH_DISABLED);
            }
            PdfDocument pdfDocument = context.GetPdfDocument();
            document = new HtmlDocument(pdfDocument, pdfDocument.GetDefaultPageSize(), immediateFlush);
            document.SetRenderer(new HtmlDocumentRenderer(document, immediateFlush));
            DefaultHtmlProcessor.SetConvertedRootElementProperties(element.GetStyles(), context, document);
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
            String lang = element.GetAttribute(AttributeConstants.LANG);
            if (lang != null) {
                pdfDocument.GetCatalog().SetLang(new PdfString(lang, PdfEncodings.UNICODE_BIG));
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            inlineHelper.FlushHangingLeaves(document);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            inlineHelper.Add(content);
            return true;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            bool processed = false;
            if (childTagWorker is SpanTagWorker) {
                bool allChildrenProcessed = true;
                foreach (IPropertyContainer propertyContainer in ((SpanTagWorker)childTagWorker).GetAllElements()) {
                    if (propertyContainer is ILeafElement) {
                        inlineHelper.Add((ILeafElement)propertyContainer);
                    }
                    else {
                        if (propertyContainer is IBlockElement && CssConstants.INLINE_BLOCK.Equals(((SpanTagWorker)childTagWorker)
                            .GetElementDisplay(propertyContainer))) {
                            inlineHelper.Add((IBlockElement)propertyContainer);
                        }
                        else {
                            allChildrenProcessed = ProcessBlockChild(propertyContainer) && allChildrenProcessed;
                        }
                    }
                }
                processed = allChildrenProcessed;
            }
            else {
                if (childTagWorker.GetElementResult() is IFormField && !(childTagWorker is IDisplayAware && CssConstants.BLOCK
                    .Equals(((IDisplayAware)childTagWorker).GetDisplay()))) {
                    inlineHelper.Add((IBlockElement)childTagWorker.GetElementResult());
                    processed = true;
                }
                else {
                    if (childTagWorker.GetElementResult() is AreaBreak) {
                        PostProcessInlineGroup();
                        document.Add((AreaBreak)childTagWorker.GetElementResult());
                        processed = true;
                    }
                    else {
                        if (childTagWorker is IDisplayAware && CssConstants.INLINE_BLOCK.Equals(((IDisplayAware)childTagWorker).GetDisplay
                            ()) && childTagWorker.GetElementResult() is IBlockElement) {
                            inlineHelper.Add((IBlockElement)childTagWorker.GetElementResult());
                            processed = true;
                        }
                        else {
                            if (childTagWorker is BrTagWorker) {
                                inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                                processed = true;
                            }
                            else {
                                if (childTagWorker is ImgTagWorker && childTagWorker.GetElementResult() is IElement && !CssConstants.BLOCK
                                    .Equals(((ImgTagWorker)childTagWorker).GetDisplay())) {
                                    inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                                    processed = true;
                                }
                                else {
                                    if (childTagWorker.GetElementResult() != null) {
                                        processed = ProcessBlockChild(childTagWorker.GetElementResult());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return processed;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return document;
        }

        /// <summary>Processes the page rules.</summary>
        /// <param name="rootNode">the root node</param>
        /// <param name="cssResolver">the css resolver</param>
        /// <param name="context">the context</param>
        public virtual void ProcessPageRules(INode rootNode, ICssResolver cssResolver, ProcessorContext context) {
            ((HtmlDocumentRenderer)document.GetRenderer()).ProcessPageRules(rootNode, cssResolver, context);
        }

        /// <summary>Processes a block child.</summary>
        /// <param name="element">the element</param>
        /// <returns>true, if successful</returns>
        private bool ProcessBlockChild(IPropertyContainer element) {
            PostProcessInlineGroup();
            if (element is IBlockElement) {
                document.Add((IBlockElement)element);
                return true;
            }
            if (element is Image) {
                document.Add((Image)element);
                return true;
            }
            return false;
        }

        /// <summary>Post-processes the hanging leaves of the waiting inline elements.</summary>
        private void PostProcessInlineGroup() {
            inlineHelper.FlushHangingLeaves(document);
        }
    }
}
