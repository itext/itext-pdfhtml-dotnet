/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply vertical alignment values.</summary>
    public class VerticalAlignmentApplierUtil {
        /// <summary>The Constant ASCENDER_COEFFICIENT.</summary>
        private const double ASCENDER_COEFFICIENT = 0.8;

        /// <summary>The Constant DESCENDER_COEFFICIENT.</summary>
        private const double DESCENDER_COEFFICIENT = 0.2;

        /// <summary>
        /// Creates a new
        /// <see cref="VerticalAlignmentApplierUtil"/>.
        /// </summary>
        private VerticalAlignmentApplierUtil() {
        }

        /// <summary>Applies vertical alignment to cells.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyVerticalAlignmentForCells(IDictionary<String, String> cssProps, ProcessorContext context
            , IPropertyContainer element) {
            String vAlignVal = cssProps.Get(CssConstants.VERTICAL_ALIGN);
            if (vAlignVal != null) {
                // In layout, 'top' is the default behaviour for cells;
                // 'baseline' is not supported at the moment on layout level, so it defaults to value 'top';
                // all other possible values except 'middle' and 'bottom' do not apply to cells; 'baseline' is applied instead.
                if (CssConstants.MIDDLE.Equals(vAlignVal)) {
                    element.SetProperty(Property.VERTICAL_ALIGNMENT, VerticalAlignment.MIDDLE);
                }
                else {
                    if (CssConstants.BOTTOM.Equals(vAlignVal)) {
                        element.SetProperty(Property.VERTICAL_ALIGNMENT, VerticalAlignment.BOTTOM);
                    }
                }
            }
        }

        /// <summary>Apply vertical alignment to inline elements.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="childElements">the child elements</param>
        public static void ApplyVerticalAlignmentForInlines(IDictionary<String, String> cssProps, ProcessorContext
             context, IStylesContainer stylesContainer, IList<IPropertyContainer> childElements) {
            String vAlignVal = cssProps.Get(CssConstants.VERTICAL_ALIGN);
            if (vAlignVal != null) {
                // TODO DEVSIX-1750 for inline images and tables (inline-blocks) v-align is not supported
                float textRise = 0;
                // TODO DEVSIX-3757 'top' and 'bottom' values are not supported;
                // 'top' and 'bottom' require information of actual line height, therefore should be applied at layout level;
                // 'sub', 'super' calculations are based on the behaviour of the common browsers (+33% and -20% shift accordingly from the parent's font size);
                // 'middle', 'text-top', 'text-bottom' calculations are based on the approximate assumptions that x-height is 0.5 of the font size
                // and descender and ascender heights are 0.2 and 0.8 of the font size accordingly.
                if (CssConstants.SUB.Equals(vAlignVal) || CssConstants.SUPER.Equals(vAlignVal)) {
                    textRise = CalcTextRiseForSupSub(stylesContainer, vAlignVal);
                }
                else {
                    if (CssConstants.MIDDLE.Equals(vAlignVal)) {
                        textRise = CalcTextRiseForMiddle(stylesContainer);
                    }
                    else {
                        if (CssConstants.TEXT_TOP.Equals(vAlignVal)) {
                            textRise = CalcTextRiseForTextTop(stylesContainer, context.GetCssContext().GetRootFontSize());
                        }
                        else {
                            if (CssConstants.TEXT_BOTTOM.Equals(vAlignVal)) {
                                textRise = CalcTextRiseForTextBottom(stylesContainer, context.GetCssContext().GetRootFontSize());
                            }
                            else {
                                if (CssUtils.IsMetricValue(vAlignVal)) {
                                    textRise = CssUtils.ParseAbsoluteLength(vAlignVal);
                                }
                                else {
                                    if (vAlignVal.EndsWith(CssConstants.PERCENTAGE)) {
                                        textRise = CalcTextRiseForPercentageValue(stylesContainer, context.GetCssContext().GetRootFontSize(), vAlignVal
                                            );
                                    }
                                }
                            }
                        }
                    }
                }
                if (textRise != 0) {
                    foreach (IPropertyContainer element in childElements) {
                        if (element is Text) {
                            float? effectiveTr = element.GetProperty<float?>(Property.TEXT_RISE);
                            if (effectiveTr != null) {
                                effectiveTr += textRise;
                            }
                            else {
                                effectiveTr = textRise;
                            }
                            element.SetProperty(Property.TEXT_RISE, effectiveTr);
                        }
                        else {
                            if (element is IBlockElement) {
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Calculates the text rise value for &lt;sup&gt; and &lt;sub&gt; tags.</summary>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="vAlignVal">the vertical alignment value</param>
        /// <returns>the calculated text rise</returns>
        private static float CalcTextRiseForSupSub(IStylesContainer stylesContainer, String vAlignVal) {
            float parentFontSize = GetParentFontSize(stylesContainer);
            String superscriptPosition = "33%";
            String subscriptPosition = "-20%";
            String relativeValue = CssConstants.SUPER.Equals(vAlignVal) ? superscriptPosition : subscriptPosition;
            return CssUtils.ParseRelativeValue(relativeValue, parentFontSize);
        }

        /// <summary>Calculates the text rise for middle alignment.</summary>
        /// <param name="stylesContainer">the styles container</param>
        /// <returns>the calculated text rise</returns>
        private static float CalcTextRiseForMiddle(IStylesContainer stylesContainer) {
            String ownFontSizeStr = stylesContainer.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            float parentFontSize = GetParentFontSize(stylesContainer);
            double fontMiddleCoefficient = 0.3;
            float elementMidPoint = (float)(fontSize * fontMiddleCoefficient);
            // shift to element mid point from the baseline
            float xHeight = parentFontSize / 4;
            return xHeight - elementMidPoint;
        }

        /// <summary>Calculates the text rise for top alignment.</summary>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="rootFontSize">the root font size</param>
        /// <returns>the calculated text rise</returns>
        private static float CalcTextRiseForTextTop(IStylesContainer stylesContainer, float rootFontSize) {
            String ownFontSizeStr = stylesContainer.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            String lineHeightStr = stylesContainer.GetStyles().Get(CssConstants.LINE_HEIGHT);
            float lineHeightActualValue = GetLineHeightActualValue(fontSize, rootFontSize, lineHeightStr);
            float parentFontSize = GetParentFontSize(stylesContainer);
            float elementTopEdge = (float)(fontSize * ASCENDER_COEFFICIENT + (lineHeightActualValue - fontSize) / 2);
            float parentTextTop = (float)(parentFontSize * ASCENDER_COEFFICIENT);
            return parentTextTop - elementTopEdge;
        }

        /// <summary>Calculates the text rise for bottom alignment.</summary>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="rootFontSize">the root font size</param>
        /// <returns>the calculated text rise</returns>
        private static float CalcTextRiseForTextBottom(IStylesContainer stylesContainer, float rootFontSize) {
            String ownFontSizeStr = stylesContainer.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            String lineHeightStr = stylesContainer.GetStyles().Get(CssConstants.LINE_HEIGHT);
            float lineHeightActualValue = GetLineHeightActualValue(fontSize, rootFontSize, lineHeightStr);
            float parentFontSize = GetParentFontSize(stylesContainer);
            float elementBottomEdge = (float)(fontSize * DESCENDER_COEFFICIENT + (lineHeightActualValue - fontSize) / 
                2);
            float parentTextBottom = (float)(parentFontSize * DESCENDER_COEFFICIENT);
            return elementBottomEdge - parentTextBottom;
        }

        /// <summary>Calculates text rise for percentage value text rise.</summary>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="rootFontSize">the root font size</param>
        /// <param name="vAlignVal">the vertical alignment value</param>
        /// <returns>the calculated text rise</returns>
        private static float CalcTextRiseForPercentageValue(IStylesContainer stylesContainer, float rootFontSize, 
            String vAlignVal) {
            String ownFontSizeStr = stylesContainer.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            String lineHeightStr = stylesContainer.GetStyles().Get(CssConstants.LINE_HEIGHT);
            float lineHeightActualValue = GetLineHeightActualValue(fontSize, rootFontSize, lineHeightStr);
            return CssUtils.ParseRelativeValue(vAlignVal, lineHeightActualValue);
        }

        /// <summary>Gets the actual value of the line height.</summary>
        /// <param name="fontSize">the font size</param>
        /// <param name="rootFontSize">the root font size</param>
        /// <param name="lineHeightStr">
        /// the line height as a
        /// <see cref="System.String"/>
        /// </param>
        /// <returns>
        /// the actual line height as a
        /// <c>float</c>
        /// </returns>
        private static float GetLineHeightActualValue(float fontSize, float rootFontSize, String lineHeightStr) {
            float lineHeightActualValue;
            if (lineHeightStr != null) {
                if (CssConstants.NORMAL.Equals(lineHeightStr) || CssConstants.AUTO.Equals(lineHeightStr)) {
                    lineHeightActualValue = (float)(fontSize * 1.2);
                }
                else {
                    UnitValue lineHeightValue = CssUtils.ParseLengthValueToPt(lineHeightStr, fontSize, rootFontSize);
                    if (CssUtils.IsNumericValue(lineHeightStr)) {
                        lineHeightActualValue = fontSize * lineHeightValue.GetValue();
                    }
                    else {
                        if (lineHeightValue.IsPointValue()) {
                            lineHeightActualValue = lineHeightValue.GetValue();
                        }
                        else {
                            lineHeightActualValue = fontSize * lineHeightValue.GetValue() / 100;
                        }
                    }
                }
            }
            else {
                lineHeightActualValue = (float)(fontSize * 1.2);
            }
            return lineHeightActualValue;
        }

        /// <summary>Gets the parent font size.</summary>
        /// <param name="stylesContainer">the styles container</param>
        /// <returns>the parent font size</returns>
        private static float GetParentFontSize(IStylesContainer stylesContainer) {
            float parentFontSize;
            if (stylesContainer is INode && ((IElementNode)stylesContainer).ParentNode() is IStylesContainer) {
                INode parent = ((IElementNode)stylesContainer).ParentNode();
                String parentFontSizeStr = ((IStylesContainer)parent).GetStyles().Get(CssConstants.FONT_SIZE);
                parentFontSize = CssUtils.ParseAbsoluteLength(parentFontSizeStr);
            }
            else {
                // let's take own font size for this unlikely case
                String ownFontSizeStr = stylesContainer.GetStyles().Get(CssConstants.FONT_SIZE);
                parentFontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            }
            return parentFontSize;
        }
    }
}
