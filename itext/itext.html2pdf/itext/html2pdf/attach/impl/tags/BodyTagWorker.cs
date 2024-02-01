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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Html;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>body</c>
    /// element.
    /// </summary>
    public class BodyTagWorker : DivTagWorker {
        /// <summary>The parent tag worker.</summary>
        private ITagWorker parentTagWorker;

        /// <summary>The lang attribute value.</summary>
        private String lang;

        /// <summary>
        /// Creates a new
        /// <see cref="BodyTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public BodyTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
            parentTagWorker = context.GetState().Empty() ? null : context.GetState().Top();
            PdfDocument pdfDocument = context.GetPdfDocument();
            if (pdfDocument != null) {
                lang = element.GetAttribute(AttributeConstants.LANG);
                if (lang != null) {
                    pdfDocument.GetCatalog().SetLang(new PdfString(lang, PdfEncodings.UNICODE_BIG));
                }
            }
            else {
                lang = element.GetLang();
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            if (parentTagWorker == null) {
                base.ProcessEnd(element, context);
                if (context.GetPdfDocument() == null) {
                    foreach (IElement child in ((Div)base.GetElementResult()).GetChildren()) {
                        if (child is IAccessibleElement) {
                            AccessiblePropHelper.TrySetLangAttribute((IAccessibleElement)child, lang);
                        }
                    }
                }
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override bool ProcessContent(String content, ProcessorContext context) {
            if (parentTagWorker == null) {
                return base.ProcessContent(content, context);
            }
            else {
                return parentTagWorker.ProcessContent(content, context);
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            if (parentTagWorker == null) {
                return base.ProcessTagChild(childTagWorker, context);
            }
            else {
                return parentTagWorker.ProcessTagChild(childTagWorker, context);
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public override IPropertyContainer GetElementResult() {
            return parentTagWorker == null ? base.GetElementResult() : parentTagWorker.GetElementResult();
        }
    }
}
