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
    address: sales@itextpdf.com */
using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Html.Node;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class HtmlTagWorker : ITagWorker {
        private Document document;

        private WaitingInlineElementsHelper inlineHelper;

        public HtmlTagWorker(IElementNode element, ProcessorContext context) {
            bool immediateFlush = !context.GetCssContext().IsPagesCounterPresent();
            PdfDocument pdfDocument = context.GetPdfDocument();
            document = new Document(pdfDocument, pdfDocument.GetDefaultPageSize(), immediateFlush);
            document.SetRenderer(new HtmlDocumentRenderer(document, immediateFlush));
            document.SetProperty(Property.COLLAPSING_MARGINS, true);
            document.SetFontProvider(context.GetFontProvider());
            String fontFamily = element.GetStyles().Get(CssConstants.FONT_FAMILY);
            document.SetProperty(Property.FONT, fontFamily);
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            inlineHelper.FlushHangingLeaves(document);
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            inlineHelper.Add(content);
            return true;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            bool processed;
            if (childTagWorker is SpanTagWorker) {
                bool allChildrenProcessed = true;
                foreach (IPropertyContainer propertyContainer in ((SpanTagWorker)childTagWorker).GetAllElements()) {
                    if (propertyContainer is ILeafElement) {
                        inlineHelper.Add((ILeafElement)propertyContainer);
                    }
                    else {
                        allChildrenProcessed = ProcessBlockChild(propertyContainer) && allChildrenProcessed;
                    }
                }
                processed = allChildrenProcessed;
            }
            else {
                if (childTagWorker.GetElementResult() is FormField) {
                    inlineHelper.Add((FormField)childTagWorker.GetElementResult());
                    return true;
                }
                else {
                    if (childTagWorker.GetElementResult() is AreaBreak) {
                        PostProcessInlineGroup();
                        document.Add((AreaBreak)childTagWorker.GetElementResult());
                        processed = true;
                    }
                    else {
                        return ProcessBlockChild(childTagWorker.GetElementResult());
                    }
                }
            }
            return processed;
        }

        public virtual IPropertyContainer GetElementResult() {
            return document;
        }

        public virtual void ProcessPageRules(INode rootNode, ICssResolver cssResolver, ProcessorContext context) {
            ((HtmlDocumentRenderer)document.GetRenderer()).ProcessPageRules(rootNode, cssResolver, context);
        }

        private bool ProcessBlockChild(IPropertyContainer element) {
            PostProcessInlineGroup();
            if (element is IBlockElement) {
                document.Add((IBlockElement)element);
                return true;
            }
            else {
                if (element is Image) {
                    document.Add((Image)element);
                }
            }
            return true;
        }

        private void PostProcessInlineGroup() {
            inlineHelper.FlushHangingLeaves(document);
        }
    }
}
