/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using System.Text.RegularExpressions;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// <see cref="iText.Html2pdf.Attach.ITagWorker"/>
    /// implementation for elements with
    /// <c>display: flex</c>.
    /// </summary>
    public class DisplayFlexTagWorker : ITagWorker, IDisplayAware {
        private static readonly Regex ANY_SYMBOL_PATTERN;

        private readonly Div flexContainer;

        private readonly WaitingInlineElementsHelper inlineHelper;

        static DisplayFlexTagWorker() {
            ANY_SYMBOL_PATTERN = iText.Commons.Utils.StringUtil.RegexCompile("\\S+");
        }

        /// <summary>
        /// Creates instance of
        /// <see cref="DisplayFlexTagWorker"/>.
        /// </summary>
        /// <param name="element">the element with defined styles</param>
        /// <param name="context">the context of the converter processor</param>
        public DisplayFlexTagWorker(IElementNode element, ProcessorContext context) {
            flexContainer = new Div();
            flexContainer.SetNextRenderer(new FlexContainerRenderer(flexContainer));
            IDictionary<String, String> styles = element.GetStyles();
            inlineHelper = new WaitingInlineElementsHelper(styles == null ? null : styles.Get(CssConstants.WHITE_SPACE
                ), styles == null ? null : styles.Get(CssConstants.TEXT_TRANSFORM));
            AccessiblePropHelper.TrySetLangAttribute(flexContainer, element);
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            if (InlineHelperContainsText()) {
                AddInlineWaitingLeavesToFlexContainer();
            }
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            if (iText.Commons.Utils.Matcher.Match(ANY_SYMBOL_PATTERN, content).Find()) {
                inlineHelper.Add(content);
            }
            return true;
        }

        public virtual IPropertyContainer GetElementResult() {
            return flexContainer;
        }

        public virtual String GetDisplay() {
            return CssConstants.FLEX;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            IPropertyContainer element = childTagWorker.GetElementResult();
            if (childTagWorker is BrTagWorker) {
                inlineHelper.Add((ILeafElement)element);
            }
            else {
                if (InlineHelperContainsText()) {
                    AddInlineWaitingLeavesToFlexContainer();
                }
                if (element is IBlockElement) {
                    flexContainer.Add((IBlockElement)element);
                }
                else {
                    if (element is Image) {
                        flexContainer.Add((Image)element);
                    }
                    else {
                        if (element is AreaBreak) {
                            flexContainer.Add((AreaBreak)element);
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void AddInlineWaitingLeavesToFlexContainer() {
            inlineHelper.FlushHangingLeaves(flexContainer);
            inlineHelper.ClearWaitingLeaves();
        }

        private bool InlineHelperContainsText() {
            bool containsText = false;
            foreach (IElement element in inlineHelper.GetWaitingLeaves()) {
                if (element is iText.Layout.Element.Text && iText.Commons.Utils.Matcher.Match(ANY_SYMBOL_PATTERN, ((iText.Layout.Element.Text
                    )element).GetText()).Find()) {
                    containsText = true;
                }
            }
            return containsText;
        }
    }
}
