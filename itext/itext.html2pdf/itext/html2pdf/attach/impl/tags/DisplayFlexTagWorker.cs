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
