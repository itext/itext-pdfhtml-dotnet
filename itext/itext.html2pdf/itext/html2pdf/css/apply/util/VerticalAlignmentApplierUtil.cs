/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html.Node;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public class VerticalAlignmentApplierUtil {
        private const double ASCENDER_COEFFICIENT = 0.8;

        private const double DESCENDER_COEFFICIENT = 0.2;

        private VerticalAlignmentApplierUtil() {
        }

        public static void ApplyVerticalAlignment(IDictionary<String, String> cssProps, ProcessorContext context, 
            IElementNode elementNode, IPropertyContainer element) {
            String vAlignVal = cssProps.Get(CssConstants.VERTICAL_ALIGN);
            if (vAlignVal != null) {
                // TODO for inline images and tables (inline-blocks) v-align is not supported 
                if (element is Cell) {
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
                else {
                    if (element is Text) {
                        // TODO 'top' and 'bottom' values are not supported;
                        // 'top' and 'bottom' require information of actual line height, therefore should be applied at layout level;
                        // 'sub', 'super' calculations are based on the behaviour of the common browsers (+33% and -20% shift accordingly from the parent's font size);
                        // 'middle', 'text-top', 'text-bottom' calculations are based on the approximate assumptions that x-height is 0.5 of the font size
                        // and descender and ascender heights are 0.2 and 0.8 of the font size accordingly.
                        if (CssConstants.SUB.Equals(vAlignVal) || CssConstants.SUPER.Equals(vAlignVal)) {
                            element.SetProperty(Property.TEXT_RISE, CalcTextRiseForSupSub(elementNode, vAlignVal));
                        }
                        else {
                            if (CssConstants.MIDDLE.Equals(vAlignVal)) {
                                element.SetProperty(Property.TEXT_RISE, CalcTextRiseForMiddle(elementNode));
                            }
                            else {
                                if (CssConstants.TEXT_TOP.Equals(vAlignVal)) {
                                    element.SetProperty(Property.TEXT_RISE, CalcTextRiseForTextTop(elementNode));
                                }
                                else {
                                    if (CssConstants.TEXT_BOTTOM.Equals(vAlignVal)) {
                                        element.SetProperty(Property.TEXT_RISE, CalcTextRiseForTextBottom(elementNode));
                                    }
                                    else {
                                        if (CssUtils.IsMetricValue(vAlignVal)) {
                                            element.SetProperty(Property.TEXT_RISE, CssUtils.ParseAbsoluteLength(vAlignVal));
                                        }
                                        else {
                                            if (vAlignVal.EndsWith(CssConstants.PERCENTAGE)) {
                                                element.SetProperty(Property.TEXT_RISE, CalcTextRiseForPercentageValue(elementNode, vAlignVal));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static float CalcTextRiseForSupSub(IElementNode elementNode, String vAlignVal) {
            float parentFontSize = GetParentFontSize(elementNode);
            String superscriptPosition = "33%";
            String subscriptPosition = "-20%";
            String relativeValue = CssConstants.SUPER.Equals(vAlignVal) ? superscriptPosition : subscriptPosition;
            return CssUtils.ParseRelativeValue(relativeValue, parentFontSize);
        }

        private static float CalcTextRiseForMiddle(IElementNode elementNode) {
            String ownFontSizeStr = elementNode.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            float parentFontSize = GetParentFontSize(elementNode);
            double fontMiddleCoefficient = 0.3;
            float elementMidPoint = (float)(fontSize * fontMiddleCoefficient);
            // shift to element mid point from the baseline
            float xHeight = parentFontSize / 4;
            return xHeight - elementMidPoint;
        }

        private static float CalcTextRiseForTextTop(IElementNode elementNode) {
            String ownFontSizeStr = elementNode.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            String lineHeightStr = elementNode.GetStyles().Get(CssConstants.LINE_HEIGHT);
            float lineHeightActualValue = GetLineHeightActualValue(fontSize, lineHeightStr);
            float parentFontSize = GetParentFontSize(elementNode);
            float elementTopEdge = (float)(fontSize * ASCENDER_COEFFICIENT + (lineHeightActualValue - fontSize) / 2);
            float parentTextTop = (float)(parentFontSize * ASCENDER_COEFFICIENT);
            return parentTextTop - elementTopEdge;
        }

        private static float CalcTextRiseForTextBottom(IElementNode elementNode) {
            String ownFontSizeStr = elementNode.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            String lineHeightStr = elementNode.GetStyles().Get(CssConstants.LINE_HEIGHT);
            float lineHeightActualValue = GetLineHeightActualValue(fontSize, lineHeightStr);
            float parentFontSize = GetParentFontSize(elementNode);
            float elementBottomEdge = (float)(fontSize * DESCENDER_COEFFICIENT + (lineHeightActualValue - fontSize) / 
                2);
            float parentTextBottom = (float)(parentFontSize * DESCENDER_COEFFICIENT);
            return elementBottomEdge - parentTextBottom;
        }

        private static float CalcTextRiseForPercentageValue(IElementNode elementNode, String vAlignVal) {
            String ownFontSizeStr = elementNode.GetStyles().Get(CssConstants.FONT_SIZE);
            float fontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            String lineHeightStr = elementNode.GetStyles().Get(CssConstants.LINE_HEIGHT);
            float lineHeightActualValue = GetLineHeightActualValue(fontSize, lineHeightStr);
            return CssUtils.ParseRelativeValue(vAlignVal, lineHeightActualValue);
        }

        private static float GetLineHeightActualValue(float fontSize, String lineHeightStr) {
            float lineHeightActualValue;
            if (lineHeightStr != null) {
                UnitValue lineHeightValue = CssUtils.ParseLengthValueToPt(lineHeightStr, fontSize);
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
            else {
                lineHeightActualValue = (float)(fontSize * 1.2);
            }
            return lineHeightActualValue;
        }

        private static float GetParentFontSize(IElementNode elementNode) {
            float parentFontSize;
            if (elementNode.ParentNode() is IElementNode) {
                String parentFontSizeStr = ((IElementNode)elementNode.ParentNode()).GetStyles().Get(CssConstants.FONT_SIZE
                    );
                parentFontSize = CssUtils.ParseAbsoluteLength(parentFontSizeStr);
            }
            else {
                // let's take own font size for this unlikely case
                String ownFontSizeStr = elementNode.GetStyles().Get(CssConstants.FONT_SIZE);
                parentFontSize = CssUtils.ParseAbsoluteLength(ownFontSizeStr);
            }
            return parentFontSize;
        }
    }
}
