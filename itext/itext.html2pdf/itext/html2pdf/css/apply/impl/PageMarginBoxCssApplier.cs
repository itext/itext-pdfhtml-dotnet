using System;
using System.Collections.Generic;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html.Node;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Properties;

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
        /// <param name="defaultValue">the default value</param>
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
            // TODO outlines are currently not supported for page margin boxes, because of the outlines handling specificity (they are handled on renderer's parent level)
            OutlineApplierUtil.ApplyOutlines(boxStyles, context, marginBox);
            marginBox.SetProperty(Property.FONT_PROVIDER, context.GetFontProvider());
            marginBox.SetProperty(Property.FONT_SET, context.GetTempFonts());
            if (!(stylesContainer is PageMarginBoxContextNode)) {
                ILog logger = LogManager.GetLogger(typeof(PageMarginBoxCssApplier));
                logger.Warn(iText.Html2pdf.LogMessageConstant.PAGE_MARGIN_BOX_SOME_PROPERTIES_NOT_PROCESSED);
                return;
            }
            PageMarginBoxContextNode pageMarginBoxContextNode = (PageMarginBoxContextNode)stylesContainer;
            float em = CssUtils.ParseAbsoluteLength(boxStyles.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            float[] boxMargins = ParseBoxProps(boxStyles, em, rem, new float[] { 0, 0, 0, 0 }, pageMarginBoxContextNode
                .GetContainingBlockForMarginBox(), CssConstants.MARGIN_TOP, CssConstants.MARGIN_RIGHT, CssConstants.MARGIN_BOTTOM
                , CssConstants.MARGIN_LEFT);
            float[] boxPaddings = ParseBoxProps(boxStyles, em, rem, new float[] { 0, 0, 0, 0 }, pageMarginBoxContextNode
                .GetContainingBlockForMarginBox(), CssConstants.PADDING_TOP, CssConstants.PADDING_RIGHT, CssConstants.
                PADDING_BOTTOM, CssConstants.PADDING_LEFT);
            SetUnitPointValueProperties(marginBox, new int[] { Property.MARGIN_TOP, Property.MARGIN_RIGHT, Property.MARGIN_BOTTOM
                , Property.MARGIN_LEFT }, boxMargins);
            SetUnitPointValueProperties(marginBox, new int[] { Property.PADDING_TOP, Property.PADDING_RIGHT, Property.
                PADDING_BOTTOM, Property.PADDING_LEFT }, boxPaddings);
            float[] boxBorders = GetBordersWidth(marginBox);
            float marginBorderPaddingWidth = boxMargins[1] + boxMargins[3] + boxBorders[1] + boxBorders[3] + boxPaddings
                [1] + boxPaddings[3];
            float marginBorderPaddingHeight = boxMargins[0] + boxMargins[2] + boxBorders[0] + boxBorders[2] + boxPaddings
                [0] + boxPaddings[2];
            // TODO DEVSIX-1050: improve width/height calculation according to "5.3. Computing Page-margin Box Dimensions", take into account height and width properties
            float width = pageMarginBoxContextNode.GetPageMarginBoxRectangle().GetWidth() - marginBorderPaddingWidth;
            float height = pageMarginBoxContextNode.GetPageMarginBoxRectangle().GetHeight() - marginBorderPaddingHeight;
            SetUnitPointValueProperty(marginBox, Property.WIDTH, width);
            SetUnitPointValueProperty(marginBox, Property.HEIGHT, height);
        }

        private static void SetUnitPointValueProperties(IPropertyContainer container, int[] properties, float[] values
            ) {
            for (int i = 0; i < properties.Length; ++i) {
                SetUnitPointValueProperty(container, properties[i], values[i]);
            }
        }

        private static void SetUnitPointValueProperty(IPropertyContainer container, int property, float value) {
            UnitValue marginUV = UnitValue.CreatePointValue(value);
            container.SetProperty(property, marginUV);
        }

        /// <summary>Parses the box value.</summary>
        /// <param name="em">a measurement expressed in em</param>
        /// <param name="rem">a measurement expressed in rem (root em)</param>
        /// <param name="dimensionSize">the dimension size</param>
        /// <returns>a float value</returns>
        private static float? ParseBoxValue(String valString, float em, float rem, float dimensionSize) {
            UnitValue marginUnitVal = CssUtils.ParseLengthValueToPt(valString, em, rem);
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

        private static float[] GetBordersWidth(IPropertyContainer container) {
            Border border = container.GetProperty<Border>(Property.BORDER);
            Border topBorder = container.GetProperty<Border>(Property.BORDER_TOP);
            Border rightBorder = container.GetProperty<Border>(Property.BORDER_RIGHT);
            Border bottomBorder = container.GetProperty<Border>(Property.BORDER_BOTTOM);
            Border leftBorder = container.GetProperty<Border>(Property.BORDER_LEFT);
            Border[] borders = new Border[] { topBorder, rightBorder, bottomBorder, leftBorder };
            if (!container.HasProperty(Property.BORDER_TOP)) {
                borders[0] = border;
            }
            if (!container.HasProperty(Property.BORDER_RIGHT)) {
                borders[1] = border;
            }
            if (!container.HasProperty(Property.BORDER_BOTTOM)) {
                borders[2] = border;
            }
            if (!container.HasProperty(Property.BORDER_LEFT)) {
                borders[3] = border;
            }
            return new float[] { borders[0] != null ? borders[0].GetWidth() : 0, borders[1] != null ? borders[1].GetWidth
                () : 0, borders[2] != null ? borders[2].GetWidth() : 0, borders[3] != null ? borders[3].GetWidth() : 0
                 };
        }
    }
}
