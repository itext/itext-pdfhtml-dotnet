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
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for Span elements.
    /// </summary>
    public class SpanTagCssApplier : ICssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.ICssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public virtual void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            SpanTagWorker spanTagWorker = (SpanTagWorker)tagWorker;
            IDictionary<String, String> cssStyles = stylesContainer.GetStyles();
            foreach (IPropertyContainer child in spanTagWorker.GetOwnLeafElements()) {
                // Workaround for form fields so that SpanTagCssApplier does not apply its font-size to the child.
                // Form fields have their own CSS applier
                if (!(child is IFormField)) {
                    ApplyChildElementStyles(child, cssStyles, context, stylesContainer);
                }
            }
            VerticalAlignmentApplierUtil.ApplyVerticalAlignmentForInlines(cssStyles, context, stylesContainer, spanTagWorker
                .GetAllElements());
            if (cssStyles.ContainsKey(CssConstants.OPACITY)) {
                foreach (IPropertyContainer elem in spanTagWorker.GetAllElements()) {
                    if (elem is Text && !elem.HasProperty(Property.OPACITY)) {
                        OpacityApplierUtil.ApplyOpacity(cssStyles, context, elem);
                    }
                }
            }
            String floatVal = cssStyles.Get(CssConstants.FLOAT);
            if (floatVal != null && !CssConstants.NONE.Equals(floatVal)) {
                foreach (IPropertyContainer elem in spanTagWorker.GetAllElements()) {
                    FloatPropertyValue? kidFloatVal = elem.GetProperty<FloatPropertyValue?>(Property.FLOAT);
                    if (kidFloatVal == null || FloatPropertyValue.NONE.Equals(kidFloatVal)) {
                        FloatApplierUtil.ApplyFloating(cssStyles, context, elem);
                    }
                }
            }
            if (spanTagWorker.GetAllElements() != null) {
                foreach (IPropertyContainer child in spanTagWorker.GetAllElements()) {
                    FloatPropertyValue? kidFloatVal = child.GetProperty<FloatPropertyValue?>(Property.FLOAT);
                    if (child is Text && !child.HasOwnProperty(Property.BACKGROUND) && (kidFloatVal == null || FloatPropertyValue
                        .NONE.Equals(kidFloatVal))) {
                        BackgroundApplierUtil.ApplyBackground(cssStyles, context, child);
                    }
                }
            }
        }

        /// <summary>Applies styles to child elements.</summary>
        /// <param name="element">the element</param>
        /// <param name="css">the CSS mapping</param>
        /// <param name="context">the processor context</param>
        /// <param name="stylesContainer">the styles container</param>
        protected internal virtual void ApplyChildElementStyles(IPropertyContainer element, IDictionary<String, String
            > css, ProcessorContext context, IStylesContainer stylesContainer) {
            FontStyleApplierUtil.ApplyFontStyles(css, context, stylesContainer, element);
            BackgroundApplierUtil.ApplyBackground(css, context, element);
            BorderStyleApplierUtil.ApplyBorders(css, context, element);
            OutlineApplierUtil.ApplyOutlines(css, context, element);
            HyphenationApplierUtil.ApplyHyphenation(css, context, stylesContainer, element);
            MarginApplierUtil.ApplyMargins(css, context, element);
            PositionApplierUtil.ApplyPosition(css, context, element);
            FloatApplierUtil.ApplyFloating(css, context, element);
            PaddingApplierUtil.ApplyPaddings(css, context, element);
        }
    }
}
