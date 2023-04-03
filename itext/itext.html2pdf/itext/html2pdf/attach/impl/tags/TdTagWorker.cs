/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
            int? colspan = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.COLSPAN));
            int? rowspan = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.ROWSPAN));
            colspan = colspan != null ? colspan : 1;
            rowspan = rowspan != null ? rowspan : 1;
            cell = new Cell((int)rowspan, (int)colspan);
            cell.SetPadding(0);
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
            AccessiblePropHelper.TrySetLangAttribute(cell, element);
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
