/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using iText.Html2pdf.Css.Util;
using iText.IO.Log;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply a width or a height to an element.</summary>
    public sealed class WidthHeightApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.WidthHeightApplierUtil
            ));

        /// <summary>
        /// Creates a new
        /// <see cref="WidthHeightApplierUtil"/>
        /// instance.
        /// </summary>
        private WidthHeightApplierUtil() {
        }

        /// <summary>Applies a width or a height to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyWidthHeight(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            String widthVal = cssProps.Get(CssConstants.WIDTH);
            if (!CssConstants.AUTO.Equals(widthVal) && widthVal != null) {
                UnitValue width = CssUtils.ParseLengthValueToPt(widthVal, em, rem);
                element.SetProperty(Property.WIDTH, width);
            }
            String minWidthVal = cssProps.Get(CssConstants.MIN_WIDTH);
            if (!CssConstants.AUTO.Equals(minWidthVal) && minWidthVal != null) {
                UnitValue minWidth = CssUtils.ParseLengthValueToPt(minWidthVal, em, rem);
                element.SetProperty(Property.MIN_WIDTH, minWidth);
            }
            String maxWidthVal = cssProps.Get(CssConstants.MAX_WIDTH);
            if (!CssConstants.AUTO.Equals(maxWidthVal) && maxWidthVal != null) {
                UnitValue maxWidth = CssUtils.ParseLengthValueToPt(maxWidthVal, em, rem);
                element.SetProperty(Property.MAX_WIDTH, maxWidth);
            }
            // TODO consider display css property
            bool applyToTable = element is Table;
            bool applyToCell = element is Cell;
            UnitValue height = null;
            String heightVal = cssProps.Get(CssConstants.HEIGHT);
            if (heightVal != null) {
                if (!CssConstants.AUTO.Equals(heightVal)) {
                    height = CssUtils.ParseLengthValueToPt(heightVal, em, rem);
                    if (height != null) {
                        // For tables, height does not have any effect. The height value will be used when
                        // calculating effective min height value below
                        if (!applyToTable && !applyToCell) {
                            element.SetProperty(Property.HEIGHT, height);
                        }
                    }
                }
            }
            String maxHeightVal = cssProps.Get(CssConstants.MAX_HEIGHT);
            float maxHeightToApply = 0;
            UnitValue maxHeight = new UnitValue(UnitValue.POINT, 0);
            if (maxHeightVal != null) {
                maxHeight = CssUtils.ParseLengthValueToPt(maxHeightVal, em, rem);
                if (maxHeight != null) {
                    // For tables and cells, max height does not have any effect. See also comments below when MIN_HEIGHT is applied.
                    if (!applyToTable && !applyToCell) {
                        maxHeightToApply = maxHeight.GetValue();
                    }
                }
            }
            if (maxHeightToApply > 0) {
                element.SetProperty(Property.MAX_HEIGHT, maxHeight);
            }
            String minHeightVal = cssProps.Get(CssConstants.MIN_HEIGHT);
            float minHeightToApply = 0;
            UnitValue minHeight = new UnitValue(UnitValue.POINT, 0);
            if (minHeightVal != null) {
                minHeight = CssUtils.ParseLengthValueToPt(minHeightVal, em, rem);
                if (minHeight != null) {
                    // For cells, min height does not have any effect. See also comments below when MIN_HEIGHT is applied.
                    if (!applyToCell) {
                        minHeightToApply = minHeight.GetValue();
                    }
                }
            }
            // About tables:
            // The height of a table is given by the 'height' property for the 'table' or 'inline-table' element.
            // A value of 'auto' means that the height is the sum of the row heights plus any cell spacing or borders.
            // Any other value is treated as a minimum height. CSS 2.1 does not define how extra space is distributed when
            // the 'height' property causes the table to be taller than it otherwise would be.
            // About cells:
            // The height of a 'table-row' element's box is the maximum of the row's computed 'height', the computed 'height' of each cell in the row,
            // and the minimum height (MIN) required by the cells. MIN depends on cell box heights and cell box alignment.
            // In CSS 2.1, the height of a cell box is the minimum height required by the content.
            if ((applyToTable || applyToCell) && height != null && height.GetValue() > minHeightToApply) {
                minHeightToApply = height.GetValue();
                if (minHeightToApply > 0) {
                    element.SetProperty(Property.MIN_HEIGHT, height);
                }
            }
            else {
                if (minHeightToApply > 0) {
                    element.SetProperty(Property.MIN_HEIGHT, minHeight);
                }
            }
            if (CssConstants.BORDER_BOX.Equals(cssProps.Get(CssConstants.BOX_SIZING))) {
                element.SetProperty(Property.BOX_SIZING, BoxSizingPropertyValue.BORDER_BOX);
            }
        }
    }
}
