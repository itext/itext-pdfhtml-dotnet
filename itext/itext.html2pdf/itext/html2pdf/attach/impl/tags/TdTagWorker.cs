/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>td</c>
    /// element.
    /// </summary>
    public class TdTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The cell.</summary>
        private Cell cell;

        /// <summary>The inline helper.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>The display.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="TdTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public TdTagWorker(IElementNode element, ProcessorContext context) {
            int? colspan = CssUtils.ParseInteger(element.GetAttribute(AttributeConstants.COLSPAN));
            int? rowspan = CssUtils.ParseInteger(element.GetAttribute(AttributeConstants.ROWSPAN));
            colspan = colspan != null ? colspan : 1;
            rowspan = rowspan != null ? rowspan : 1;
            cell = new Cell((int)rowspan, (int)colspan);
            cell.SetPadding(0);
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            inlineHelper.FlushHangingLeaves(cell);
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
            if (childTagWorker is IDisplayAware && CssConstants.INLINE_BLOCK.Equals(((IDisplayAware)childTagWorker).GetDisplay
                ()) && childTagWorker.GetElementResult() is IBlockElement) {
                inlineHelper.Add((IBlockElement)childTagWorker.GetElementResult());
                processed = true;
            }
            else {
                if (childTagWorker is SpanTagWorker) {
                    bool allChildrenProcesssed = true;
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
                                allChildrenProcesssed = ProcessChild(propertyContainer) && allChildrenProcesssed;
                            }
                        }
                    }
                    processed = allChildrenProcesssed;
                }
                else {
                    if (childTagWorker.GetElementResult() is ILeafElement) {
                        inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                        processed = true;
                    }
                    else {
                        processed = ProcessChild(childTagWorker.GetElementResult());
                    }
                }
            }
            return processed;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return cell;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.IDisplayAware#getDisplay()
        */
        public virtual String GetDisplay() {
            return display;
        }

        /// <summary>Processes a child element.</summary>
        /// <param name="propertyContainer">the property container</param>
        /// <returns>true, if successful</returns>
        private bool ProcessChild(IPropertyContainer propertyContainer) {
            bool processed = false;
            inlineHelper.FlushHangingLeaves(cell);
            if (propertyContainer is IBlockElement) {
                cell.Add((IBlockElement)propertyContainer);
                processed = true;
            }
            return processed;
        }
    }
}
