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
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>span</c>
    /// tag.
    /// </summary>
    public class SpanTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The span wrapper.</summary>
        internal SpanWrapper spanWrapper;

        internal IDictionary<IPropertyContainer, String> childrenDisplayMap = new Dictionary<IPropertyContainer, String
            >();

        /// <summary>A list of elements belonging to the span.</summary>
        private IList<IPropertyContainer> elements;

        /// <summary>The own leaf elements.</summary>
        private IList<IPropertyContainer> ownLeafElements = new List<IPropertyContainer>();

        /// <summary>The helper object for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>The display value.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="SpanTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the processor context</param>
        public SpanTagWorker(IElementNode element, ProcessorContext context) {
            // TODO ideally, this should be refactored. For now, I don't see a beautiful way of passing this information to other workers.
            // Also, we probably should wait a bit until the display support is more or less stable
            spanWrapper = new SpanWrapper();
            IDictionary<String, String> styles = element.GetStyles();
            inlineHelper = new WaitingInlineElementsHelper(styles == null ? null : styles.Get(CssConstants.WHITE_SPACE
                ), styles == null ? null : styles.Get(CssConstants.TEXT_TRANSFORM));
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            if (inlineHelper.GetWaitingLeaves().IsEmpty() && spanWrapper.GetElements().IsEmpty()) {
                inlineHelper.Add("");
            }
            FlushInlineHelper();
            elements = spanWrapper.GetElements();
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
                FlushInlineHelper();
                spanWrapper.Add((ILeafElement)element);
                ownLeafElements.Add(element);
                return true;
            }
            else {
                if (childTagWorker is iText.Html2pdf.Attach.Impl.Tags.SpanTagWorker) {
                    FlushInlineHelper();
                    spanWrapper.Add(((iText.Html2pdf.Attach.Impl.Tags.SpanTagWorker)childTagWorker).spanWrapper);
                    childrenDisplayMap.AddAll(((iText.Html2pdf.Attach.Impl.Tags.SpanTagWorker)childTagWorker).childrenDisplayMap
                        );
                    return true;
                }
                else {
                    if (childTagWorker.GetElementResult() is IBlockElement) {
                        if (childTagWorker is IDisplayAware) {
                            String display = ((IDisplayAware)childTagWorker).GetDisplay();
                            childrenDisplayMap.Put(childTagWorker.GetElementResult(), display);
                        }
                        FlushInlineHelper();
                        spanWrapper.Add((IBlockElement)childTagWorker.GetElementResult());
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>Gets all the elements in the span.</summary>
        /// <returns>a list of elements</returns>
        public virtual IList<IPropertyContainer> GetAllElements() {
            return elements;
        }

        /// <summary>Gets the span's own leaf elements.</summary>
        /// <returns>the own leaf elements</returns>
        public virtual IList<IPropertyContainer> GetOwnLeafElements() {
            return ownLeafElements;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return null;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.IDisplayAware#getDisplay()
        */
        public virtual String GetDisplay() {
            return display;
        }

        /// <summary>
        /// The child shall be one from
        /// <see cref="GetAllElements()"/>
        /// list.
        /// </summary>
        internal virtual String GetElementDisplay(IPropertyContainer child) {
            return childrenDisplayMap.Get(child);
        }

        /// <summary>Flushes the waiting leaf elements.</summary>
        private void FlushInlineHelper() {
            spanWrapper.AddAll(inlineHelper.GetWaitingLeaves());
            ownLeafElements.AddAll(inlineHelper.GetWaitingLeaves());
            inlineHelper.ClearWaitingLeaves();
        }
    }
}
