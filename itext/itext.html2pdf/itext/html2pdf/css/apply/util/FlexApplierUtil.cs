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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply flex properties.</summary>
    public sealed class FlexApplierUtil {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.FlexApplierUtil
            ));

        private FlexApplierUtil() {
        }

        /// <summary>Applies properties to a flex item.</summary>
        /// <param name="cssProps">the map of the CSS properties</param>
        /// <param name="context">the context of the converter processor</param>
        /// <param name="element">the element to set the properties</param>
        public static void ApplyFlexItemProperties(IDictionary<String, String> cssProps, ProcessorContext context, 
            IPropertyContainer element) {
            element.SetProperty(Property.COLLAPSING_MARGINS, null);
            LogWarningIfThereAreNotSupportedPropertyValues(CreateSupportedFlexItemPropertiesAndValuesMap(), cssProps);
            String flexGrow = cssProps.Get(CommonCssConstants.FLEX_GROW);
            if (flexGrow != null) {
                float? flexGrowValue = CssDimensionParsingUtils.ParseFloat(flexGrow);
                element.SetProperty(Property.FLEX_GROW, flexGrowValue);
            }
            String flexShrink = cssProps.Get(CommonCssConstants.FLEX_SHRINK);
            if (flexShrink != null) {
                float? flexShrinkValue = CssDimensionParsingUtils.ParseFloat(flexShrink);
                element.SetProperty(Property.FLEX_SHRINK, flexShrinkValue);
            }
            String flexBasis = cssProps.Get(CommonCssConstants.FLEX_BASIS);
            if (flexBasis != null && !CommonCssConstants.AUTO.Equals(flexBasis)) {
                if (!CommonCssConstants.CONTENT.Equals(flexBasis)) {
                    float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
                    float rem = context.GetCssContext().GetRootFontSize();
                    UnitValue flexBasisAbsoluteLength = CssDimensionParsingUtils.ParseLengthValueToPt(flexBasis, em, rem);
                    element.SetProperty(Property.FLEX_BASIS, flexBasisAbsoluteLength);
                }
                else {
                    // The case when we don't set the flex-basis property should be identified
                    // as flex-basis: content
                    LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, 
                        CommonCssConstants.FLEX_BASIS, CommonCssConstants.CONTENT));
                }
            }
        }

        /// <summary>Applies properties to a flex container.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">the element</param>
        public static void ApplyFlexContainerProperties(IDictionary<String, String> cssProps, IPropertyContainer element
            ) {
            LogWarningIfThereAreNotSupportedPropertyValues(CreateSupportedFlexContainerPropertiesAndValuesMap(), cssProps
                );
            ApplyAlignItems(cssProps, element);
            ApplyJustifyContent(cssProps, element);
            ApplyAlignContent(cssProps, element);
            ApplyWrap(cssProps, element);
            ApplyDirection(cssProps, element);
        }

        private static void ApplyWrap(IDictionary<String, String> cssProps, IPropertyContainer element) {
            String wrapString = cssProps.Get(CommonCssConstants.FLEX_WRAP);
            if (wrapString != null) {
                FlexWrapPropertyValue wrap;
                switch (wrapString) {
                    case CommonCssConstants.WRAP: {
                        wrap = FlexWrapPropertyValue.WRAP;
                        break;
                    }

                    case CommonCssConstants.WRAP_REVERSE: {
                        wrap = FlexWrapPropertyValue.WRAP_REVERSE;
                        break;
                    }

                    case CommonCssConstants.NOWRAP: {
                        wrap = FlexWrapPropertyValue.NOWRAP;
                        break;
                    }

                    default: {
                        wrap = FlexWrapPropertyValue.NOWRAP;
                        break;
                    }
                }
                element.SetProperty(Property.FLEX_WRAP, wrap);
            }
        }

        private static void ApplyDirection(IDictionary<String, String> cssProps, IPropertyContainer element) {
            String directionString = cssProps.Get(CommonCssConstants.FLEX_DIRECTION);
            if (directionString != null) {
                FlexDirectionPropertyValue direction;
                switch (directionString) {
                    case CommonCssConstants.ROW: {
                        direction = FlexDirectionPropertyValue.ROW;
                        break;
                    }

                    case CommonCssConstants.ROW_REVERSE: {
                        direction = FlexDirectionPropertyValue.ROW_REVERSE;
                        break;
                    }

                    case CommonCssConstants.COLUMN: {
                        direction = FlexDirectionPropertyValue.COLUMN;
                        break;
                    }

                    case CommonCssConstants.COLUMN_REVERSE: {
                        direction = FlexDirectionPropertyValue.COLUMN_REVERSE;
                        break;
                    }

                    default: {
                        direction = FlexDirectionPropertyValue.ROW;
                        break;
                    }
                }
                element.SetProperty(Property.FLEX_DIRECTION, direction);
            }
        }

        private static void ApplyAlignItems(IDictionary<String, String> cssProps, IPropertyContainer element) {
            String alignItemsString = cssProps.Get(CommonCssConstants.ALIGN_ITEMS);
            if (alignItemsString != null) {
                AlignmentPropertyValue alignItems;
                switch (alignItemsString) {
                    case CommonCssConstants.NORMAL: {
                        alignItems = AlignmentPropertyValue.NORMAL;
                        break;
                    }

                    case CommonCssConstants.START: {
                        alignItems = AlignmentPropertyValue.START;
                        break;
                    }

                    case CommonCssConstants.END: {
                        alignItems = AlignmentPropertyValue.END;
                        break;
                    }

                    case CommonCssConstants.FLEX_START: {
                        alignItems = AlignmentPropertyValue.FLEX_START;
                        break;
                    }

                    case CommonCssConstants.FLEX_END: {
                        alignItems = AlignmentPropertyValue.FLEX_END;
                        break;
                    }

                    case CommonCssConstants.CENTER: {
                        alignItems = AlignmentPropertyValue.CENTER;
                        break;
                    }

                    case CommonCssConstants.SELF_START: {
                        alignItems = AlignmentPropertyValue.SELF_START;
                        break;
                    }

                    case CommonCssConstants.SELF_END: {
                        alignItems = AlignmentPropertyValue.SELF_END;
                        break;
                    }

                    case CommonCssConstants.STRETCH: {
                        alignItems = AlignmentPropertyValue.STRETCH;
                        break;
                    }

                    default: {
                        LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, 
                            CommonCssConstants.ALIGN_ITEMS, alignItemsString));
                        alignItems = AlignmentPropertyValue.STRETCH;
                        break;
                    }
                }
                element.SetProperty(Property.ALIGN_ITEMS, alignItems);
            }
        }

        private static void ApplyJustifyContent(IDictionary<String, String> cssProps, IPropertyContainer element) {
            String justifyContentString = cssProps.Get(CommonCssConstants.JUSTIFY_CONTENT);
            if (justifyContentString != null) {
                JustifyContent justifyContent;
                switch (justifyContentString) {
                    case CommonCssConstants.NORMAL: {
                        justifyContent = JustifyContent.NORMAL;
                        break;
                    }

                    case CommonCssConstants.START: {
                        justifyContent = JustifyContent.START;
                        break;
                    }

                    case CommonCssConstants.END: {
                        justifyContent = JustifyContent.END;
                        break;
                    }

                    case CommonCssConstants.FLEX_END: {
                        justifyContent = JustifyContent.FLEX_END;
                        break;
                    }

                    case CommonCssConstants.SELF_START: {
                        justifyContent = JustifyContent.SELF_START;
                        break;
                    }

                    case CommonCssConstants.SELF_END: {
                        justifyContent = JustifyContent.SELF_END;
                        break;
                    }

                    case CommonCssConstants.LEFT: {
                        justifyContent = JustifyContent.LEFT;
                        break;
                    }

                    case CommonCssConstants.RIGHT: {
                        justifyContent = JustifyContent.RIGHT;
                        break;
                    }

                    case CommonCssConstants.CENTER: {
                        justifyContent = JustifyContent.CENTER;
                        break;
                    }

                    case CommonCssConstants.STRETCH: {
                        justifyContent = JustifyContent.STRETCH;
                        break;
                    }

                    case CommonCssConstants.FLEX_START: {
                        justifyContent = JustifyContent.FLEX_START;
                        break;
                    }

                    default: {
                        LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, 
                            CommonCssConstants.JUSTIFY_CONTENT, justifyContentString));
                        justifyContent = JustifyContent.FLEX_START;
                        break;
                    }
                }
                element.SetProperty(Property.JUSTIFY_CONTENT, justifyContent);
            }
        }

        private static void ApplyAlignContent(IDictionary<String, String> cssProps, IPropertyContainer element) {
            String alignContentString = cssProps.Get(CommonCssConstants.ALIGN_CONTENT);
            if (alignContentString != null) {
                AlignContentPropertyValue alignContent;
                switch (alignContentString) {
                    case CommonCssConstants.FLEX_START: {
                        alignContent = AlignContentPropertyValue.FLEX_START;
                        break;
                    }

                    case CommonCssConstants.FLEX_END: {
                        alignContent = AlignContentPropertyValue.FLEX_END;
                        break;
                    }

                    case CommonCssConstants.CENTER: {
                        alignContent = AlignContentPropertyValue.CENTER;
                        break;
                    }

                    case CommonCssConstants.SPACE_BETWEEN: {
                        alignContent = AlignContentPropertyValue.SPACE_BETWEEN;
                        break;
                    }

                    case CommonCssConstants.SPACE_AROUND: {
                        alignContent = AlignContentPropertyValue.SPACE_AROUND;
                        break;
                    }

                    case CommonCssConstants.SPACE_EVENLY: {
                        alignContent = AlignContentPropertyValue.SPACE_EVENLY;
                        break;
                    }

                    case CommonCssConstants.STRETCH: {
                        alignContent = AlignContentPropertyValue.STRETCH;
                        break;
                    }

                    default: {
                        alignContent = AlignContentPropertyValue.NORMAL;
                        break;
                    }
                }
                element.SetProperty(Property.ALIGN_CONTENT, alignContent);
            }
        }

        private static void LogWarningIfThereAreNotSupportedPropertyValues(IDictionary<String, ICollection<String>
            > supportedPairs, IDictionary<String, String> cssProps) {
            foreach (KeyValuePair<String, ICollection<String>> entry in supportedPairs) {
                String supportedPair = entry.Key;
                ICollection<String> supportedValues = entry.Value;
                String propertyValue = cssProps.Get(supportedPair);
                if (propertyValue != null && !supportedValues.Contains(propertyValue)) {
                    LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, 
                        supportedPair, propertyValue));
                }
            }
        }

        private static IDictionary<String, ICollection<String>> CreateSupportedFlexItemPropertiesAndValuesMap() {
            IDictionary<String, ICollection<String>> supportedPairs = new Dictionary<String, ICollection<String>>();
            ICollection<String> supportedAlignSelfValues = new HashSet<String>();
            supportedAlignSelfValues.Add(CommonCssConstants.AUTO);
            supportedPairs.Put(CommonCssConstants.ALIGN_SELF, supportedAlignSelfValues);
            ICollection<String> supportedOrderValues = new HashSet<String>();
            supportedPairs.Put(CommonCssConstants.ORDER, supportedOrderValues);
            return supportedPairs;
        }

        private static IDictionary<String, ICollection<String>> CreateSupportedFlexContainerPropertiesAndValuesMap
            () {
            IDictionary<String, ICollection<String>> supportedPairs = new Dictionary<String, ICollection<String>>();
            ICollection<String> supportedFlexDirectionValues = new HashSet<String>();
            supportedFlexDirectionValues.Add(CommonCssConstants.ROW);
            supportedFlexDirectionValues.Add(CommonCssConstants.ROW_REVERSE);
            supportedFlexDirectionValues.Add(CommonCssConstants.COLUMN);
            supportedFlexDirectionValues.Add(CommonCssConstants.COLUMN_REVERSE);
            supportedPairs.Put(CommonCssConstants.FLEX_DIRECTION, supportedFlexDirectionValues);
            ICollection<String> supportedAlignContentValues = new HashSet<String>();
            supportedAlignContentValues.Add(CommonCssConstants.STRETCH);
            supportedAlignContentValues.Add(CommonCssConstants.NORMAL);
            supportedAlignContentValues.Add(CommonCssConstants.FLEX_START);
            supportedAlignContentValues.Add(CommonCssConstants.FLEX_END);
            supportedAlignContentValues.Add(CommonCssConstants.CENTER);
            supportedAlignContentValues.Add(CommonCssConstants.SPACE_AROUND);
            supportedAlignContentValues.Add(CommonCssConstants.SPACE_BETWEEN);
            supportedAlignContentValues.Add(CommonCssConstants.SPACE_EVENLY);
            supportedPairs.Put(CommonCssConstants.ALIGN_CONTENT, supportedAlignContentValues);
            ICollection<String> supportedRowGapValues = new HashSet<String>();
            supportedRowGapValues.Add(CommonCssConstants.NORMAL);
            supportedPairs.Put(CommonCssConstants.ROW_GAP, supportedRowGapValues);
            ICollection<String> supportedColumnGapValues = new HashSet<String>();
            supportedColumnGapValues.Add(CommonCssConstants.NORMAL);
            supportedPairs.Put(CommonCssConstants.COLUMN_GAP, supportedColumnGapValues);
            return supportedPairs;
        }
    }
}
