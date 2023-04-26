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
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Tagging;
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

        // TODO DEVSIX-2445. Ideally, this should be refactored. For now, I don't see a beautiful way
        //  of passing this information to other workers.
        // Also, we probably should wait a bit until the display support is more or less stable
        internal IDictionary<IPropertyContainer, String> childrenDisplayMap = new Dictionary<IPropertyContainer, String
            >();

        /// <summary>A list of elements belonging to the span.</summary>
        private IList<IPropertyContainer> elements;

        /// <summary>The own leaf elements.</summary>
        private IList<IPropertyContainer> ownLeafElements = new List<IPropertyContainer>();

        /// <summary>The helper object for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>The display value.</summary>
        private readonly String display;

        /// <summary>The text-transform value.</summary>
        private readonly String textTransform;

        /// <summary>
        /// Creates a new
        /// <see cref="SpanTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the processor context</param>
        public SpanTagWorker(IElementNode element, ProcessorContext context) {
            spanWrapper = new SpanWrapper();
            IDictionary<String, String> styles = element.GetStyles();
            inlineHelper = new WaitingInlineElementsHelper(styles == null ? null : styles.Get(CssConstants.WHITE_SPACE
                ), styles == null ? null : styles.Get(CssConstants.TEXT_TRANSFORM));
            display = styles == null ? null : styles.Get(CssConstants.DISPLAY);
            textTransform = styles == null ? null : styles.Get(CssConstants.TEXT_TRANSFORM);
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
            foreach (IPropertyContainer elem in elements) {
                if (elem is IAccessibleElement) {
                    AccessiblePropHelper.TrySetLangAttribute((IAccessibleElement)elem, element);
                }
            }
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
            ICollection<IElement> waitingLeaves = inlineHelper.GetWaitingLeaves();
            SetCapitalizeProperty(waitingLeaves);
            spanWrapper.AddAll(waitingLeaves);
            ownLeafElements.AddAll(waitingLeaves);
            inlineHelper.ClearWaitingLeaves();
        }

        /// <summary>
        /// Sets property that indicates whether the element should be capitalized, for
        /// <see cref="iText.Layout.Element.Text"/>
        /// elements only.
        /// </summary>
        /// <param name="elements">elements to which properties will be added</param>
        private void SetCapitalizeProperty(ICollection<IElement> elements) {
            foreach (IElement iElement in elements) {
                if (iElement is Text) {
                    if (!iElement.HasOwnProperty(Html2PdfProperty.CAPITALIZE_ELEMENT) && CssConstants.CAPITALIZE.Equals(textTransform
                        )) {
                        iElement.SetProperty(Html2PdfProperty.CAPITALIZE_ELEMENT, true);
                    }
                    else {
                        iElement.SetProperty(Html2PdfProperty.CAPITALIZE_ELEMENT, false);
                    }
                }
            }
        }
    }
}
