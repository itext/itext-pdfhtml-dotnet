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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>li</c>
    /// element.
    /// </summary>
    public class LiTagWorker : ITagWorker {
        /// <summary>The list item.</summary>
        protected internal ListItem listItem;

        /// <summary>The list.</summary>
        protected internal List list;

        /// <summary>The inline helper.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>
        /// Creates a new
        /// <see cref="LiTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public LiTagWorker(IElementNode element, ProcessorContext context) {
            listItem = new ListItem();
            if (element.GetAttribute(AttributeConstants.VALUE) != null) {
                int? indexValue = (int?)CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.VALUE
                    ));
                if (indexValue != null) {
                    listItem.SetListSymbolOrdinalValue(indexValue.Value);
                }
            }
            if (!(context.GetState().Top() is UlOlTagWorker)) {
                listItem.SetProperty(Property.LIST_SYMBOL_POSITION, ListSymbolPosition.INSIDE);
                float em = CssDimensionParsingUtils.ParseAbsoluteLength(element.GetStyles().Get(CssConstants.FONT_SIZE));
                if (TagConstants.LI.Equals(element.Name())) {
                    ListStyleApplierUtil.SetDiscStyle(listItem, em);
                }
                else {
                    listItem.SetProperty(Property.LIST_SYMBOL, null);
                }
                list = new List();
                list.Add(listItem);
            }
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
            AccessiblePropHelper.TrySetLangAttribute(listItem, element);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            inlineHelper.FlushHangingLeaves(listItem);
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
            IPropertyContainer element = childTagWorker.GetElementResult();
            if (element is ILeafElement) {
                inlineHelper.Add((ILeafElement)element);
                return true;
            }
            else {
                if (childTagWorker is IDisplayAware && (CssConstants.INLINE_BLOCK.Equals(((IDisplayAware)childTagWorker).GetDisplay
                    ()) || CssConstants.INLINE.Equals(((IDisplayAware)childTagWorker).GetDisplay())) && element is IBlockElement
                    ) {
                    inlineHelper.Add((IBlockElement)element);
                    return true;
                }
                else {
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
                                    allChildrenProcessed = ProcessChild(propertyContainer) && allChildrenProcessed;
                                }
                            }
                        }
                        return allChildrenProcessed;
                    }
                    else {
                        return ProcessChild(childTagWorker.GetElementResult());
                    }
                }
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return list != null ? (IPropertyContainer)list : listItem;
        }

        /// <summary>Processes a child.</summary>
        /// <param name="propertyContainer">the property container</param>
        /// <returns>true, if successful</returns>
        private bool ProcessChild(IPropertyContainer propertyContainer) {
            inlineHelper.FlushHangingLeaves(listItem);
            if (propertyContainer is Image) {
                listItem.Add((Image)propertyContainer);
                return true;
            }
            else {
                if (propertyContainer is IBlockElement) {
                    listItem.Add((IBlockElement)propertyContainer);
                    return true;
                }
            }
            return false;
        }
    }
}
