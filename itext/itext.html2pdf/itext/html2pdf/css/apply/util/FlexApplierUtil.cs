/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using Microsoft.Extensions.Logging;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.IO;
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
            if (flexBasis == null || CommonCssConstants.AUTO.Equals(flexBasis)) {
                // TODO DEVSIX-5003 use height as the main size if flex-direction: column.
                // we use main size property as a flex-basis value (when flex-basis: auto) in
                // corresponding with documentation https://www.w3.org/TR/css-flexbox-1/#valdef-flex-flex-basis
                String flexElementWidth = cssProps.Get(CommonCssConstants.WIDTH);
                if (flexElementWidth != null) {
                    float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
                    float rem = context.GetCssContext().GetRootFontSize();
                    UnitValue flexElementWidthAbsoluteLength = CssDimensionParsingUtils.ParseLengthValueToPt(flexElementWidth, 
                        em, rem);
                    element.SetProperty(Property.FLEX_BASIS, flexElementWidthAbsoluteLength);
                }
            }
            else {
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
            supportedPairs.Put(CommonCssConstants.FLEX_DIRECTION, supportedFlexDirectionValues);
            ICollection<String> supportedFlexWrapValues = new HashSet<String>();
            supportedFlexWrapValues.Add(CommonCssConstants.NOWRAP);
            supportedPairs.Put(CommonCssConstants.FLEX_WRAP, supportedFlexWrapValues);
            ICollection<String> supportedAlignContentValues = new HashSet<String>();
            supportedAlignContentValues.Add(CommonCssConstants.STRETCH);
            supportedAlignContentValues.Add(CommonCssConstants.NORMAL);
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
