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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Logs;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for page margin box containers.
    /// </summary>
    public class PageMarginBoxCssApplier : ICssApplier {
        /// <summary>Parses the page and margin boxes properties (like margins, paddings, etc).</summary>
        /// <param name="styles">
        /// a
        /// <see cref="System.Collections.IDictionary{K, V}"/>
        /// containing the styles
        /// </param>
        /// <param name="em">a measurement expressed in em</param>
        /// <param name="rem">a measurement expressed in rem (root em)</param>
        /// <param name="defaultValues">the default values</param>
        /// <param name="containingBlock">the containing block</param>
        /// <param name="topPropName">the top prop name</param>
        /// <param name="rightPropName">the right prop name</param>
        /// <param name="bottomPropName">the bottom prop name</param>
        /// <param name="leftPropName">the left prop name</param>
        /// <returns>an array with a top, right, bottom, and top float value</returns>
        public static float[] ParseBoxProps(IDictionary<String, String> styles, float em, float rem, float[] defaultValues
            , Rectangle containingBlock, String topPropName, String rightPropName, String bottomPropName, String leftPropName
            ) {
            String topStr = styles.Get(topPropName);
            String rightStr = styles.Get(rightPropName);
            String bottomStr = styles.Get(bottomPropName);
            String leftStr = styles.Get(leftPropName);
            float? top = ParseBoxValue(topStr, em, rem, containingBlock.GetHeight());
            float? right = ParseBoxValue(rightStr, em, rem, containingBlock.GetWidth());
            float? bottom = ParseBoxValue(bottomStr, em, rem, containingBlock.GetHeight());
            float? left = ParseBoxValue(leftStr, em, rem, containingBlock.GetWidth());
            return new float[] { top != null ? (float)top : defaultValues[0], right != null ? (float)right : defaultValues
                [1], bottom != null ? (float)bottom : defaultValues[2], left != null ? (float)left : defaultValues[3] };
        }

        public virtual void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            IDictionary<String, String> boxStyles = stylesContainer.GetStyles();
            IPropertyContainer marginBox = tagWorker.GetElementResult();
            BackgroundApplierUtil.ApplyBackground(boxStyles, context, marginBox);
            FontStyleApplierUtil.ApplyFontStyles(boxStyles, context, stylesContainer, marginBox);
            BorderStyleApplierUtil.ApplyBorders(boxStyles, context, marginBox);
            VerticalAlignmentApplierUtil.ApplyVerticalAlignmentForCells(boxStyles, context, marginBox);
            // Set overflow to HIDDEN if it's not explicitly set in css in order to avoid overlapping with page content.
            String overflow = CssConstants.OVERFLOW_VALUES.Contains(boxStyles.Get(CssConstants.OVERFLOW)) ? boxStyles.
                Get(CssConstants.OVERFLOW) : null;
            String overflowX = CssConstants.OVERFLOW_VALUES.Contains(boxStyles.Get(CssConstants.OVERFLOW_X)) ? boxStyles
                .Get(CssConstants.OVERFLOW_X) : overflow;
            if (overflowX == null || CssConstants.HIDDEN.Equals(overflowX)) {
                marginBox.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.HIDDEN);
            }
            else {
                marginBox.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            }
            String overflowY = CssConstants.OVERFLOW_VALUES.Contains(boxStyles.Get(CssConstants.OVERFLOW_Y)) ? boxStyles
                .Get(CssConstants.OVERFLOW_Y) : overflow;
            if (overflowY == null || CssConstants.HIDDEN.Equals(overflowY)) {
                marginBox.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.HIDDEN);
            }
            else {
                marginBox.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);
            }
            //TODO DEVSIX-7024 Support outlines for page margin boxes
            OutlineApplierUtil.ApplyOutlines(boxStyles, context, marginBox);
            marginBox.SetProperty(Property.FONT_PROVIDER, context.GetFontProvider());
            marginBox.SetProperty(Property.FONT_SET, context.GetTempFonts());
            if (!(stylesContainer is PageMarginBoxContextNode)) {
                ILogger logger = ITextLogManager.GetLogger(typeof(PageMarginBoxCssApplier));
                logger.LogWarning(Html2PdfLogMessageConstant.PAGE_MARGIN_BOX_SOME_PROPERTIES_NOT_PROCESSED);
                return;
            }
            float availableWidth = ((PageMarginBoxContextNode)stylesContainer).GetContainingBlockForMarginBox().GetWidth
                ();
            float availableHeight = ((PageMarginBoxContextNode)stylesContainer).GetContainingBlockForMarginBox().GetHeight
                ();
            MarginApplierUtil.ApplyMargins(boxStyles, context, marginBox, availableHeight, availableWidth);
            PaddingApplierUtil.ApplyPaddings(boxStyles, context, marginBox, availableHeight, availableWidth);
        }

        /// <summary>Parses the box value.</summary>
        /// <param name="em">a measurement expressed in em</param>
        /// <param name="rem">a measurement expressed in rem (root em)</param>
        /// <param name="dimensionSize">the dimension size</param>
        /// <returns>a float value</returns>
        private static float? ParseBoxValue(String valString, float em, float rem, float dimensionSize) {
            UnitValue marginUnitVal = CssDimensionParsingUtils.ParseLengthValueToPt(valString, em, rem);
            if (marginUnitVal != null) {
                if (marginUnitVal.IsPointValue()) {
                    return marginUnitVal.GetValue();
                }
                if (marginUnitVal.IsPercentValue()) {
                    return marginUnitVal.GetValue() * dimensionSize / 100;
                }
            }
            return null;
        }
    }
}
