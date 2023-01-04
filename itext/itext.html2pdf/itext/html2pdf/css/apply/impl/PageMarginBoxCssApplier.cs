/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
Authors: iText Software.

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
