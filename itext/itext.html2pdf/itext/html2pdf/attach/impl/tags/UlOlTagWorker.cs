/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>ul</c>
    /// and
    /// <c>ol</c>
    /// elements.
    /// </summary>
    public class UlOlTagWorker : ITagWorker {
        /// <summary>The list object.</summary>
        private List list;

        protected internal MulticolContainer multicolContainer;

        /// <summary>Helper class for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>
        /// Creates a new
        /// <see cref="UlOlTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public UlOlTagWorker(IElementNode element, ProcessorContext context) {
            list = new List().SetListSymbol("");
            if (element.GetStyles().Get(CssConstants.COLUMN_COUNT) != null || element.GetStyles().ContainsKey(CssConstants
                .COLUMN_WIDTH)) {
                multicolContainer = new MulticolContainer();
                multicolContainer.Add(list);
            }
            //In the case of an ordered list, see if the start attribute can be found
            if (element.GetAttribute(AttributeConstants.START) != null) {
                int? startValue = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.START));
                if (startValue != null) {
                    list.SetItemStartIndex((int)startValue);
                }
            }
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
            AccessiblePropHelper.TrySetLangAttribute(list, element);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            ProcessUnlabeledListItem();
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
            IPropertyContainer child = childTagWorker.GetElementResult();
            if (child is ILeafElement) {
                inlineHelper.Add((ILeafElement)child);
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
                                allChildrenProcessed = AddBlockChild(propertyContainer) && allChildrenProcessed;
                            }
                        }
                    }
                    return allChildrenProcessed;
                }
                else {
                    if (childTagWorker is IDisplayAware && CssConstants.INLINE_BLOCK.Equals(((IDisplayAware)childTagWorker).GetDisplay
                        ()) && childTagWorker.GetElementResult() is IBlockElement) {
                        inlineHelper.Add((IBlockElement)childTagWorker.GetElementResult());
                        return true;
                    }
                    else {
                        return AddBlockChild(child);
                    }
                }
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return multicolContainer == null ? (IPropertyContainer)list : (IPropertyContainer)multicolContainer;
        }

        /// <summary>Processes an unlabeled list item.</summary>
        private void ProcessUnlabeledListItem() {
            AnonymousBox ab = new AnonymousBox();
            inlineHelper.FlushHangingLeaves(ab);
            if (ab.GetChildren().Count > 0) {
                AddUnlabeledListItem(ab);
            }
        }

        /// <summary>Adds an unlabeled list item.</summary>
        /// <param name="item">the item</param>
        private void AddUnlabeledListItem(IBlockElement item) {
            ListItem li = new ListItem();
            li.Add(item);
            li.SetProperty(Property.LIST_SYMBOL, null);
            list.Add(li);
        }

        /// <summary>Adds a child.</summary>
        /// <param name="child">the child</param>
        /// <returns>true, if successful</returns>
        private bool AddBlockChild(IPropertyContainer child) {
            ProcessUnlabeledListItem();
            if (child is ListItem) {
                list.Add((ListItem)child);
                return true;
            }
            else {
                if (child is IBlockElement) {
                    AddUnlabeledListItem((IBlockElement)child);
                    return true;
                }
            }
            return false;
        }
    }
}
