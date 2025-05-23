/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>Utility class to apply column-count values.</summary>
    public sealed class MultiColumnCssApplierUtil {
        private MultiColumnCssApplierUtil() {
        }

        /// <summary>Apply column-count to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the Processor context</param>
        /// <param name="element">the styles container</param>
        public static void ApplyMultiCol(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            int? columnCount = CssDimensionParsingUtils.ParseInteger(cssProps.Get(CssConstants.COLUMN_COUNT));
            if (columnCount != null) {
                element.SetProperty(Property.COLUMN_COUNT, columnCount);
            }
            float emValue = CssDimensionParsingUtils.ParseAbsoluteFontSize(cssProps.Get(CssConstants.FONT_SIZE));
            float remValue = context.GetCssContext().GetRootFontSize();
            UnitValue width = CssDimensionParsingUtils.ParseLengthValueToPt(cssProps.Get(CssConstants.COLUMN_WIDTH), emValue
                , remValue);
            if (width != null) {
                element.SetProperty(Property.COLUMN_WIDTH, width.GetValue());
            }
            if (!element.HasProperty(Property.COLUMN_WIDTH) && !element.HasProperty(Property.COLUMN_COUNT)) {
                if (CommonCssConstants.AUTO.Equals(cssProps.Get(CssConstants.COLUMN_COUNT)) || CommonCssConstants.AUTO.Equals
                    (cssProps.Get(CssConstants.COLUMN_WIDTH))) {
                    element.SetProperty(Property.COLUMN_COUNT, 1);
                }
                else {
                    return;
                }
            }
            UnitValue gap = CssDimensionParsingUtils.ParseLengthValueToPt(cssProps.Get(CssConstants.COLUMN_GAP), emValue
                , remValue);
            if (gap != null) {
                element.SetProperty(Property.COLUMN_GAP, gap.GetValue());
            }
            //Set default colum-gap to 1em
            if (!element.HasProperty(Property.COLUMN_GAP)) {
                element.SetProperty(Property.COLUMN_GAP, CssDimensionParsingUtils.ParseRelativeValue("1em", emValue));
            }
            Border borderFromCssProperties = BorderStyleApplierUtil.GetCertainBorder(cssProps.Get(CssConstants.COLUMN_RULE_WIDTH
                ), cssProps.Get(CssConstants.COLUMN_RULE_STYLE), GetColumnGapColorOrDefault(cssProps), emValue, remValue
                );
            element.SetProperty(Property.COLUMN_GAP_BORDER, borderFromCssProperties);
        }

        private static String GetColumnGapColorOrDefault(IDictionary<String, String> styles) {
            String borderColor = styles.Get(CssConstants.COLUMN_RULE_COLOR);
            if (borderColor == null || CommonCssConstants.CURRENTCOLOR.Equals(borderColor)) {
                borderColor = styles.Get(CommonCssConstants.COLOR);
            }
            return borderColor;
        }
    }
}
