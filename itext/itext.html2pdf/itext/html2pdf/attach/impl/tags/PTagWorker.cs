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
using System.Collections.Generic;
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
    /// <p>
    /// This is how this worker processes the &lt;p&gt; tag:
    /// <ul>
    /// <li> if the worker meets a text or an inline element, it processes them with a help of
    /// the
    /// <see cref="iText.Html2pdf.Attach.Util.WaitingInlineElementsHelper"/>
    /// instance</li>
    /// <li> if the worker meets a block element without inline displaying or
    /// an inline element with the
    /// <c>display: block</c>
    /// style, it wraps all the content which hasn't been handled yet
    /// into a
    /// <c>com.itextpdf.layout.element.Paragraph</c>
    /// object and adds this paragraph to the
    /// <see cref="elements">list of elements</see>
    /// ,
    /// which are going to be wrapped into a
    /// <c>com.itextpdf.layout.element.Div</c>
    /// object on
    /// <see cref="ProcessEnd(iText.StyledXmlParser.Node.IElementNode, iText.Html2pdf.Attach.ProcessorContext)"/>
    /// </li>
    /// </ul>
    /// </summary>
    public class PTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The latest paragraph object inside tag.</summary>
        private Paragraph lastParagraph;

        /// <summary>List of block elements that contained in the &lt;p&gt; tag.</summary>
        private IList<IBlockElement> elements;

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
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            inlineHelper.FlushHangingLeaves(lastParagraph);
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
            // TODO child might be inline, however still have display:block; it behaves like a block, however p includes it in own occupied area
            IPropertyContainer element = childTagWorker.GetElementResult();
            if (element is ILeafElement) {
                inlineHelper.Add((ILeafElement)element);
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
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            if (elements == null) {
                return lastParagraph;
            }
            Div div = new Div();
            foreach (IBlockElement element in elements) {
                div.Add(element);
            }
            return div;
        }

        public virtual String GetDisplay() {
            return display;
        }

        private void ProcessBlockElement(IBlockElement propertyContainer) {
            if (elements == null) {
                elements = new List<IBlockElement>();
                elements.Add(lastParagraph);
            }
            inlineHelper.FlushHangingLeaves(lastParagraph);
            elements.Add(propertyContainer);
            lastParagraph = new Paragraph();
            elements.Add(lastParagraph);
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
