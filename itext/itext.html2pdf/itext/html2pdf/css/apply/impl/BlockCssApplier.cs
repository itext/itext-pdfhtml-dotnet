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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for Block elements.
    /// </summary>
    public class BlockCssApplier : ICssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.ICssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public virtual void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            IDictionary<String, String> cssProps = stylesContainer.GetStyles();
            IPropertyContainer container = tagWorker.GetElementResult();
            if (container != null) {
                WidthHeightApplierUtil.ApplyWidthHeight(cssProps, context, container);
                BackgroundApplierUtil.ApplyBackground(cssProps, context, container);
                MarginApplierUtil.ApplyMargins(cssProps, context, container);
                PaddingApplierUtil.ApplyPaddings(cssProps, context, container);
                FontStyleApplierUtil.ApplyFontStyles(cssProps, context, stylesContainer, container);
                BorderStyleApplierUtil.ApplyBorders(cssProps, context, container);
                HyphenationApplierUtil.ApplyHyphenation(cssProps, context, stylesContainer, container);
                PositionApplierUtil.ApplyPosition(cssProps, context, container);
                OpacityApplierUtil.ApplyOpacity(cssProps, context, container);
                PageBreakApplierUtil.ApplyPageBreakProperties(cssProps, context, container);
                OverflowApplierUtil.ApplyOverflow(cssProps, container);
                TransformationApplierUtil.ApplyTransformation(cssProps, context, container);
                OutlineApplierUtil.ApplyOutlines(cssProps, context, container);
                OrphansWidowsApplierUtil.ApplyOrphansAndWidows(cssProps, container);
                VerticalAlignmentApplierUtil.ApplyVerticalAlignmentForBlocks(cssProps, container, IsInlineItem(tagWorker));
                MultiColumnCssApplierUtil.ApplyMultiCol(cssProps, context, container);
                GridApplierUtil.ApplyGridItemProperties(cssProps, container);
                if (IsFlexItem(stylesContainer)) {
                    FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, container);
                }
                else {
                    // Floating doesn't work for flex items.
                    // See CSS Flexible Box Layout Module Level 1 W3C Candidate Recommendation, 19 November 2018,
                    // 3. Flex Containers: the flex and inline-flex display values
                    FloatApplierUtil.ApplyFloating(cssProps, context, container);
                }
            }
        }

        private static bool IsInlineItem(ITagWorker tagWorker) {
            return tagWorker is SpanTagWorker || tagWorker is ImgTagWorker;
        }

        private static bool IsFlexItem(IStylesContainer stylesContainer) {
            if (stylesContainer is JsoupElementNode && ((JsoupElementNode)stylesContainer).ParentNode() is JsoupElementNode
                ) {
                IDictionary<String, String> parentStyles = ((JsoupElementNode)((JsoupElementNode)stylesContainer).ParentNode
                    ()).GetStyles();
                return CssConstants.FLEX.Equals(parentStyles.Get(CssConstants.DISPLAY));
            }
            return false;
        }
    }
}
