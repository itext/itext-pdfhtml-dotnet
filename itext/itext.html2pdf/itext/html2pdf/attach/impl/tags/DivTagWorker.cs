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
using System.Collections.Generic;
using iText.Forms.Form.Element;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>div</c>
    /// element.
    /// </summary>
    public class DivTagWorker : ITagWorker, IDisplayAware {
        /// <summary>Column container element.</summary>
        protected internal MulticolContainer multicolContainer;

        /// <summary>The div element.</summary>
        private Div div;

        /// <summary>Helper class for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>The display value.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="DivTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public DivTagWorker(IElementNode element, ProcessorContext context) {
            div = new Div();
            IDictionary<String, String> styles = element.GetStyles();
            if (styles != null && (styles.ContainsKey(CssConstants.COLUMN_COUNT) || styles.ContainsKey(CssConstants.COLUMN_WIDTH
                ))) {
                multicolContainer = new MulticolContainer();
                multicolContainer.Add(div);
            }
            inlineHelper = new WaitingInlineElementsHelper(styles == null ? null : styles.Get(CssConstants.WHITE_SPACE
                ), styles == null ? null : styles.Get(CssConstants.TEXT_TRANSFORM));
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
            AccessiblePropHelper.TrySetLangAttribute(div, element);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            inlineHelper.FlushHangingLeaves(div);
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
            IPropertyContainer element = childTagWorker.GetElementResult();
            if (childTagWorker is BrTagWorker) {
                inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                return true;
            }
            else {
                if (childTagWorker is IDisplayAware && CssConstants.INLINE_BLOCK.Equals(((IDisplayAware)childTagWorker).GetDisplay
                    ()) && childTagWorker.GetElementResult() is IBlockElement) {
                    inlineHelper.Add((IBlockElement)childTagWorker.GetElementResult());
                    return true;
                }
                else {
                    if (childTagWorker is SpanTagWorker) {
                        bool allChildrenProcessed = true;
                        foreach (IPropertyContainer childElement in ((SpanTagWorker)childTagWorker).GetAllElements()) {
                            if (childElement is ILeafElement) {
                                inlineHelper.Add((ILeafElement)childElement);
                            }
                            else {
                                if (childElement is IBlockElement && CssConstants.INLINE_BLOCK.Equals(((SpanTagWorker)childTagWorker).GetElementDisplay
                                    (childElement))) {
                                    inlineHelper.Add((IBlockElement)childElement);
                                }
                                else {
                                    if (childElement is IElement) {
                                        allChildrenProcessed = AddBlockChild((IElement)childElement) && allChildrenProcessed;
                                    }
                                }
                            }
                        }
                        processed = allChildrenProcessed;
                    }
                    else {
                        if (element is IFormField && !(childTagWorker is IDisplayAware && CssConstants.BLOCK.Equals(((IDisplayAware
                            )childTagWorker).GetDisplay()))) {
                            inlineHelper.Add((IBlockElement)element);
                            processed = true;
                        }
                        else {
                            if (element is AreaBreak) {
                                PostProcessInlineGroup();
                                div.Add((AreaBreak)element);
                                processed = true;
                            }
                            else {
                                if (childTagWorker is ImgTagWorker && element is IElement && !CssConstants.BLOCK.Equals(((ImgTagWorker)childTagWorker
                                    ).GetDisplay())) {
                                    inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                                    processed = true;
                                }
                                else {
                                    if (element is IElement) {
                                        processed = AddBlockChild((IElement)element);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return processed;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return multicolContainer == null ? div : multicolContainer;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.IDisplayAware#getDisplay()
        */
        public virtual String GetDisplay() {
            return display;
        }

        /// <summary>Adds a child element to the div block.</summary>
        /// <param name="element">the element</param>
        /// <returns>true, if successful</returns>
        protected internal virtual bool AddBlockChild(IElement element) {
            PostProcessInlineGroup();
            bool processed = false;
            if (element is IBlockElement) {
                div.Add(((IBlockElement)element));
                processed = true;
            }
            else {
                if (element is Image) {
                    div.Add((Image)element);
                    processed = true;
                }
            }
            return processed;
        }

        /// <summary>Post-processes the hanging leaves of the waiting inline elements.</summary>
        protected internal virtual void PostProcessInlineGroup() {
            inlineHelper.FlushHangingLeaves(div);
        }
    }
}
