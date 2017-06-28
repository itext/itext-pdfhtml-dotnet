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
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;
using iText.Layout;
using iText.Layout.Element;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for the <code>div</code> element.</summary>
    public class DivTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The div element.</summary>
        private Div div;

        /// <summary>Helper class for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>The display value.</summary>
        private String display;

        /// <summary>Creates a new <code>DivTagWorker</code> instance.</summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public DivTagWorker(IElementNode element, ProcessorContext context) {
            div = new Div();
            IDictionary<String, String> styles = element.GetStyles();
            inlineHelper = new WaitingInlineElementsHelper(styles == null ? null : styles.Get(CssConstants.WHITE_SPACE
                ), styles == null ? null : styles.Get(CssConstants.TEXT_TRANSFORM));
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
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
                if (inlineHelper.GetSanitizedWaitingLeaves().Count == 1 && inlineHelper.GetSanitizedWaitingLeaves()[0] is 
                    Image) {
                    // TODO This is a workaround for case of single image to set leading to 1
                    PostProcessInlineGroup();
                }
                inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
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
                            if (childElement is IElement) {
                                allChildrenProcessed = AddBlockChild((IElement)childElement) && allChildrenProcessed;
                            }
                        }
                    }
                    processed = allChildrenProcessed;
                }
                else {
                    if (element is IFormField) {
                        if (childTagWorker is IDisplayAware && CssConstants.BLOCK.Equals(((IDisplayAware)childTagWorker).GetDisplay
                            ())) {
                            // TODO make IFormField implement IBlockElement and add it directly to div without inline helper.
                            // TODO Requires refactoring of AbstractOneLineTextFieldRenderer (calculate baselines properly)
                            PostProcessInlineGroup();
                            inlineHelper.Add((ILeafElement)element);
                            PostProcessInlineGroup();
                        }
                        else {
                            inlineHelper.Add((IFormField)element);
                        }
                        processed = true;
                    }
                    else {
                        if (element is AreaBreak) {
                            PostProcessInlineGroup();
                            div.Add((AreaBreak)element);
                            processed = true;
                        }
                        else {
                            if (childTagWorker is ImgTagWorker && element is IElement) {
                                if (CssConstants.BLOCK.Equals(((ImgTagWorker)childTagWorker).GetDisplay())) {
                                    processed = AddBlockChild((IElement)element);
                                }
                                else {
                                    if (childTagWorker.GetElementResult() is Image) {
                                        inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                                        processed = true;
                                    }
                                }
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
            return processed;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return div;
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
        private bool AddBlockChild(IElement element) {
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
        private void PostProcessInlineGroup() {
            inlineHelper.FlushHangingLeaves(div);
        }
    }
}
