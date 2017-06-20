/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public class PageBreakApplierUtil {
        private PageBreakApplierUtil() {
        }

        public static void ApplyPageBreakProperties(IDictionary<String, String> cssProps, ProcessorContext context
            , IPropertyContainer element) {
            ApplyPageBreakInside(cssProps, context, element);
            ApplyKeepWithNext(cssProps, context, element);
        }

        /* Handles left, right, always cases. Avoid is handled at different time along with other css property application */
        public static void AddPageBreakElementBefore(ProcessorContext context, ITagWorker parentTagWorker, IElementNode
             childElement, ITagWorker childTagWorker) {
            // Applies to block-level elements
            if (CssConstants.BLOCK.Equals(childElement.GetStyles().Get(CssConstants.DISPLAY)) || childElement.GetStyles
                ().Get(CssConstants.DISPLAY) == null && childTagWorker.GetElementResult() is IBlockElement) {
                String pageBreakBeforeVal = childElement.GetStyles().Get(CssConstants.PAGE_BREAK_BEFORE);
                HtmlPageBreak breakBefore = CreateHtmlPageBreak(pageBreakBeforeVal);
                if (breakBefore != null) {
                    parentTagWorker.ProcessTagChild(new PageBreakApplierUtil.HtmlPageBreakWorker(breakBefore), context);
                }
            }
        }

        /* Handles left, right, always cases. Avoid is handled at different time along with other css property application */
        public static void AddPageBreakElementAfter(ProcessorContext context, ITagWorker parentTagWorker, IElementNode
             childElement, ITagWorker childTagWorker) {
            // Applies to block-level elements
            if (CssConstants.BLOCK.Equals(childElement.GetStyles().Get(CssConstants.DISPLAY)) || childElement.GetStyles
                ().Get(CssConstants.DISPLAY) == null && childTagWorker.GetElementResult() is IBlockElement) {
                String pageBreakAfterVal = childElement.GetStyles().Get(CssConstants.PAGE_BREAK_AFTER);
                HtmlPageBreak breakAfter = CreateHtmlPageBreak(pageBreakAfterVal);
                if (breakAfter != null) {
                    parentTagWorker.ProcessTagChild(new PageBreakApplierUtil.HtmlPageBreakWorker(breakAfter), context);
                }
            }
        }

        private static HtmlPageBreak CreateHtmlPageBreak(String pageBreakAfterVal) {
            HtmlPageBreak pageBreak = null;
            if (CssConstants.ALWAYS.Equals(pageBreakAfterVal)) {
                pageBreak = new HtmlPageBreak(HtmlPageBreakType.ALWAYS);
            }
            else {
                if (CssConstants.LEFT.Equals(pageBreakAfterVal)) {
                    pageBreak = new HtmlPageBreak(HtmlPageBreakType.LEFT);
                }
                else {
                    if (CssConstants.RIGHT.Equals(pageBreakAfterVal)) {
                        pageBreak = new HtmlPageBreak(HtmlPageBreakType.RIGHT);
                    }
                }
            }
            return pageBreak;
        }

        private static void ApplyKeepWithNext(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String pageBreakBefore = cssProps.Get(CssConstants.PAGE_BREAK_BEFORE);
            String pageBreakAfter = cssProps.Get(CssConstants.PAGE_BREAK_AFTER);
            if (CssConstants.AVOID.Equals(pageBreakAfter)) {
                element.SetProperty(Property.KEEP_WITH_NEXT, true);
            }
            if (CssConstants.AVOID.Equals(pageBreakBefore)) {
                element.SetProperty(Html2PdfProperty.KEEP_WITH_PREVIOUS, true);
            }
        }

        private static void ApplyPageBreakInside(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            // TODO A potential page break location is typically under the influence of the parent element's 'page-break-inside' property,
            // the 'page-break-after' property of the preceding element, and the 'page-break-before' property of the following element.
            // When these properties have values other than 'auto', the values 'always', 'left', and 'right' take precedence over 'avoid'.
            String pageBreakInsideVal = cssProps.Get(CssConstants.PAGE_BREAK_INSIDE);
            if (CssConstants.AVOID.Equals(pageBreakInsideVal)) {
                element.SetProperty(Property.KEEP_TOGETHER, true);
            }
        }

        private class HtmlPageBreakWorker : ITagWorker {
            private HtmlPageBreak pageBreak;

            internal HtmlPageBreakWorker(HtmlPageBreak pageBreak) {
                this.pageBreak = pageBreak;
            }

            public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            }

            public virtual bool ProcessContent(String content, ProcessorContext context) {
                throw new InvalidOperationException();
            }

            public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
                throw new InvalidOperationException();
            }

            public virtual IPropertyContainer GetElementResult() {
                return pageBreak;
            }
        }
    }
}
