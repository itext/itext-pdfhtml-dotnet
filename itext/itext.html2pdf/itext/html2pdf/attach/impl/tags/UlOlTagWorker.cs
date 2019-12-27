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
            //In the case of an ordered list, see if the start attribute can be found
            if (element.GetAttribute(AttributeConstants.START) != null) {
                int? startValue = CssUtils.ParseInteger(element.GetAttribute(AttributeConstants.START));
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
            return list;
        }

        /// <summary>Processes an unlabeled list item.</summary>
        private void ProcessUnlabeledListItem() {
            Paragraph p = inlineHelper.CreateParagraphContainer();
            inlineHelper.FlushHangingLeaves(p);
            if (p.GetChildren().Count > 0) {
                AddUnlabeledListItem(p);
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
