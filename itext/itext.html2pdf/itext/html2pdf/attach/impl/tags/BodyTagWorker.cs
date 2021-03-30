/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

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
