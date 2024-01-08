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
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>p</c>
    /// element.
    /// </summary>
    /// <remarks>
    /// TagWorker class for the
    /// <c>p</c>
    /// element.
    /// <para />
    /// This is how this worker processes the &lt;p&gt; tag:
    /// <list type="bullet">
    /// <item><description> if the worker meets a text or an inline element, it processes them with a help of
    /// the
    /// <see cref="iText.Html2pdf.Attach.Util.WaitingInlineElementsHelper"/>
    /// instance
    /// </description></item>
    /// <item><description> if the worker meets a block element without inline displaying or
    /// an inline element with the
    /// <c>display: block</c>
    /// style, it wraps all the content which hasn't been handled yet
    /// into a
    /// <c>com.itextpdf.layout.element.Paragraph</c>
    /// object and adds this paragraph to the resultant
    /// <c>com.itextpdf.layout.element.Div</c>
    /// object
    /// </description></item>
    /// </list>
    /// </remarks>
    public class PTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The latest paragraph object inside tag.</summary>
        private Paragraph lastParagraph;

        /// <summary>The container which handles the elements that are present in the &lt;p&gt; tag.</summary>
        private Div elementsContainer;

        /// <summary>Container for the result in case of multicol layouting</summary>
        protected internal MulticolContainer multicolContainer;

        /// <summary>Helper class for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>The display value.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="PTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public PTagWorker(IElementNode element, ProcessorContext context) {
            lastParagraph = new Paragraph();
            if (element.GetStyles().Get(CssConstants.COLUMN_COUNT) != null || element.GetStyles().Get(CssConstants.COLUMN_WIDTH
                ) != null) {
                multicolContainer = new MulticolContainer();
                multicolContainer.Add(lastParagraph);
            }
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            inlineHelper.FlushHangingLeaves(lastParagraph);
            if (elementsContainer != null) {
                AccessiblePropHelper.TrySetLangAttribute(elementsContainer, element);
            }
            else {
                AccessiblePropHelper.TrySetLangAttribute(lastParagraph, element);
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
            // Child might be inline, however still have display:block; it behaves like a block,
            // however p includes it in own occupied area
            IPropertyContainer element = childTagWorker.GetElementResult();
            if (childTagWorker is ImgTagWorker && CssConstants.BLOCK.Equals(((ImgTagWorker)childTagWorker).GetDisplay(
                ))) {
                IPropertyContainer propertyContainer = childTagWorker.GetElementResult();
                ProcessBlockElement((Image)propertyContainer);
                return true;
            }
            else {
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
                        if (IsBlockWithDisplay(childTagWorker, element, CssConstants.INLINE_BLOCK, false)) {
                            inlineHelper.Add((IBlockElement)element);
                            return true;
                        }
                        else {
                            if (IsBlockWithDisplay(childTagWorker, element, CssConstants.BLOCK, false)) {
                                IPropertyContainer propertyContainer = childTagWorker.GetElementResult();
                                ProcessBlockElement((IBlockElement)propertyContainer);
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
                                            if (IsBlockWithDisplay(childTagWorker, propertyContainer, CssConstants.INLINE_BLOCK, true)) {
                                                inlineHelper.Add((IBlockElement)propertyContainer);
                                            }
                                            else {
                                                if (IsBlockWithDisplay(childTagWorker, propertyContainer, CssConstants.BLOCK, true)) {
                                                    ProcessBlockElement((IBlockElement)propertyContainer);
                                                }
                                                else {
                                                    allChildrenProcessed = false;
                                                }
                                            }
                                        }
                                    }
                                    return allChildrenProcessed;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            if (multicolContainer == null) {
                return null == elementsContainer ? (IPropertyContainer)lastParagraph : (IPropertyContainer)elementsContainer;
            }
            return multicolContainer;
        }

        public virtual String GetDisplay() {
            return display;
        }

        private void ProcessBlockElement(IElement propertyContainer) {
            if (elementsContainer == null) {
                elementsContainer = new Div();
                elementsContainer.Add(lastParagraph);
                if (multicolContainer != null) {
                    multicolContainer.GetChildren().Clear();
                    multicolContainer.Add(elementsContainer);
                }
            }
            inlineHelper.FlushHangingLeaves(lastParagraph);
            if (propertyContainer is Image) {
                elementsContainer.Add((Image)propertyContainer);
            }
            else {
                elementsContainer.Add((IBlockElement)propertyContainer);
            }
            lastParagraph = new Paragraph();
            elementsContainer.Add(lastParagraph);
        }

        private bool IsBlockWithDisplay(ITagWorker childTagWorker, IPropertyContainer element, String displayMode, 
            bool isChild) {
            if (isChild) {
                return element is IBlockElement && displayMode.Equals(((SpanTagWorker)childTagWorker).GetElementDisplay(element
                    ));
            }
            else {
                return element is IBlockElement && childTagWorker is IDisplayAware && displayMode.Equals(((IDisplayAware)childTagWorker
                    ).GetDisplay());
            }
        }
    }
}
