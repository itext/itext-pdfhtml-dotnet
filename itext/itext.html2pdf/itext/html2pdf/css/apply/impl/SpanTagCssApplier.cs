/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Commons.Internal.Runtime;
using iText.Forms.Form.Element;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
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
            ApplyMarginsPaddingsBordersStyles(spanTagWorker.GetAllElements(), cssStyles, context);
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
            OutlineApplierUtil.ApplyOutlines(css, context, element);
            HyphenationApplierUtil.ApplyHyphenation(css, context, stylesContainer, element);
            PositionApplierUtil.ApplyPosition(css, context, element);
            FloatApplierUtil.ApplyFloating(css, context, element);
        }

        protected internal virtual void ApplyMarginsPaddingsBordersStyles(IList<IPropertyContainer> allChildren, IDictionary
            <String, String> css, ProcessorContext context) {
            if (allChildren != null && !allChildren.IsEmpty()) {
                IPropertyContainer firstChild = allChildren[0];
                IPropertyContainer lastChild = allChildren[allChildren.Count - 1];
                // Apply left margin to the first child, apply right margin to the last child.
                ApplyPropertiesCertainPropertyValue(firstChild, css, Property.MARGIN_LEFT, true, (newStyles) => MarginApplierUtil
                    .ApplyMargins(newStyles, context, firstChild), CommonCssConstants.MARGIN_LEFT);
                ApplyPropertiesCertainPropertyValue(lastChild, css, Property.MARGIN_RIGHT, true, (newStyles) => MarginApplierUtil
                    .ApplyMargins(newStyles, context, lastChild), CommonCssConstants.MARGIN_RIGHT);
                // Apply left padding to the first child, apply right padding to the last child.
                ApplyPropertiesCertainPropertyValue(firstChild, css, Property.PADDING_LEFT, true, (newStyles) => PaddingApplierUtil
                    .ApplyPaddings(newStyles, context, firstChild), CommonCssConstants.PADDING_LEFT);
                ApplyPropertiesCertainPropertyValue(lastChild, css, Property.PADDING_RIGHT, true, (newStyles) => PaddingApplierUtil
                    .ApplyPaddings(newStyles, context, lastChild), CommonCssConstants.PADDING_RIGHT);
                // Apply top and bottom paddings to all the children.
                IDictionary<String, String> topAndBottomPaddingStyles = new Dictionary<String, String>();
                topAndBottomPaddingStyles.Put(CommonCssConstants.PADDING_TOP, css.Get(CommonCssConstants.PADDING_TOP));
                topAndBottomPaddingStyles.Put(CommonCssConstants.PADDING_BOTTOM, css.Get(CommonCssConstants.PADDING_BOTTOM
                    ));
                topAndBottomPaddingStyles.Put(CommonCssConstants.FONT_SIZE, css.Get(CommonCssConstants.FONT_SIZE));
                foreach (IPropertyContainer child in allChildren) {
                    PaddingApplierUtil.ApplyPaddings(topAndBottomPaddingStyles, context, child);
                }
                // Apply border-left, as well as left radii to the first child.
                ApplyPropertiesCertainPropertyValue(firstChild, css, Property.BORDER_LEFT, false, (newStyles) => BorderStyleApplierUtil
                    .ApplyBorders(newStyles, context, firstChild), CommonCssConstants.BORDER_LEFT_STYLE, CommonCssConstants
                    .BORDER_LEFT_COLOR, CommonCssConstants.BORDER_LEFT_WIDTH);
                ApplyPropertiesCertainPropertyValue(firstChild, css, Property.BORDER_TOP_LEFT_RADIUS, false, (newStyles) =>
                     BorderStyleApplierUtil.ApplyBorders(newStyles, context, firstChild), CommonCssConstants.BORDER_TOP_LEFT_RADIUS
                    );
                ApplyPropertiesCertainPropertyValue(firstChild, css, Property.BORDER_BOTTOM_LEFT_RADIUS, false, (newStyles
                    ) => BorderStyleApplierUtil.ApplyBorders(newStyles, context, firstChild), CommonCssConstants.BORDER_BOTTOM_LEFT_RADIUS
                    );
                // Apply border-right, as well as right radii to the last child.
                ApplyPropertiesCertainPropertyValue(lastChild, css, Property.BORDER_RIGHT, false, (newStyles) => BorderStyleApplierUtil
                    .ApplyBorders(newStyles, context, lastChild), CommonCssConstants.BORDER_RIGHT_STYLE, CommonCssConstants
                    .BORDER_RIGHT_COLOR, CommonCssConstants.BORDER_RIGHT_WIDTH);
                ApplyPropertiesCertainPropertyValue(lastChild, css, Property.BORDER_TOP_RIGHT_RADIUS, false, (newStyles) =>
                     BorderStyleApplierUtil.ApplyBorders(newStyles, context, lastChild), CommonCssConstants.BORDER_TOP_RIGHT_RADIUS
                    );
                ApplyPropertiesCertainPropertyValue(lastChild, css, Property.BORDER_BOTTOM_RIGHT_RADIUS, false, (newStyles
                    ) => BorderStyleApplierUtil.ApplyBorders(newStyles, context, lastChild), CommonCssConstants.BORDER_BOTTOM_RIGHT_RADIUS
                    );
                // Apply top and bottom borders to all the children.
                IDictionary<String, String> topAndBottomBorderStyles = new Dictionary<String, String>(css);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_LEFT_STYLE);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_LEFT_COLOR);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_LEFT_WIDTH);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_RIGHT_STYLE);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_RIGHT_COLOR);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_RIGHT_WIDTH);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_TOP_LEFT_RADIUS);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_TOP_RIGHT_RADIUS);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_BOTTOM_LEFT_RADIUS);
                topAndBottomBorderStyles.JRemove(CommonCssConstants.BORDER_BOTTOM_RIGHT_RADIUS);
                IDictionary<String, String> topBorderStyles = new Dictionary<String, String>(topAndBottomBorderStyles);
                topBorderStyles.JRemove(CommonCssConstants.BORDER_BOTTOM_STYLE);
                topBorderStyles.JRemove(CommonCssConstants.BORDER_BOTTOM_WIDTH);
                topBorderStyles.JRemove(CommonCssConstants.BORDER_BOTTOM_COLOR);
                IDictionary<String, String> bottomBorderStyles = new Dictionary<String, String>(topAndBottomBorderStyles);
                topBorderStyles.JRemove(CommonCssConstants.BORDER_TOP_STYLE);
                topBorderStyles.JRemove(CommonCssConstants.BORDER_TOP_WIDTH);
                topBorderStyles.JRemove(CommonCssConstants.BORDER_TOP_COLOR);
                foreach (IPropertyContainer child in allChildren) {
                    if (!child.HasProperty(Property.BORDER_TOP)) {
                        BorderStyleApplierUtil.ApplyBorders(topBorderStyles, context, child);
                    }
                    if (!child.HasProperty(Property.BORDER_BOTTOM)) {
                        BorderStyleApplierUtil.ApplyBorders(bottomBorderStyles, context, child);
                    }
                }
            }
        }

        private static void ApplyPropertiesCertainPropertyValue(IPropertyContainer child, IDictionary<String, String
            > styles, int property, bool combineValues, Action<IDictionary<String, String>> operation, params String
            [] propertyNames) {
            IDictionary<String, String> stylesWithRequiredValue = new Dictionary<String, String>(propertyNames.Length);
            foreach (String propertyName in propertyNames) {
                stylesWithRequiredValue.Put(propertyName, styles.Get(propertyName));
            }
            // Commonly useful properties.
            stylesWithRequiredValue.Put(CommonCssConstants.FONT_SIZE, styles.Get(CommonCssConstants.FONT_SIZE));
            stylesWithRequiredValue.Put(CommonCssConstants.COLOR, styles.Get(CommonCssConstants.COLOR));
            if (combineValues) {
                UnitValue previousPropertyValue = child.GetProperty<UnitValue>(property);
                child.DeleteOwnProperty(property);
                operation(stylesWithRequiredValue);
                UnitValue currentPropertyValue = child.GetProperty<UnitValue>(property);
                if (previousPropertyValue != null) {
                    if (currentPropertyValue == null || previousPropertyValue.IsPercentValue() || currentPropertyValue.IsPercentValue
                        ()) {
                        child.SetProperty(property, previousPropertyValue);
                    }
                    else {
                        child.SetProperty(property, UnitValue.CreatePointValue(previousPropertyValue.GetValue() + currentPropertyValue
                            .GetValue()));
                    }
                }
            }
            else {
                operation(stylesWithRequiredValue);
            }
        }
    }
}
