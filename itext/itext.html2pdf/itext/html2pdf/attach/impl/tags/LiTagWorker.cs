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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for the <code>li</code> element.</summary>
    public class LiTagWorker : ITagWorker {
        /// <summary>The list item.</summary>
        protected internal ListItem listItem;

        /// <summary>The list.</summary>
        protected internal List list;

        /// <summary>The inline helper.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>Creates a new <code>LiTagWorker</code> instance.</summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public LiTagWorker(IElementNode element, ProcessorContext context) {
            listItem = new ListItem();
            if (!(context.GetState().Top() is UlOlTagWorker)) {
                listItem.SetProperty(Property.LIST_SYMBOL_POSITION, ListSymbolPosition.INSIDE);
                float em = CssUtils.ParseAbsoluteLength(element.GetStyles().Get(CssConstants.FONT_SIZE));
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
