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
using iText.Html2pdf.Css;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>Container class for grouping necessary values used in dimension calculation</summary>
    internal abstract class DimensionContainer {
        internal float dimension;

        internal float minDimension;

        internal float maxDimension;

        internal float minContentDimension;

        internal float maxContentDimension;

        internal DimensionContainer() {
            dimension = -1;
            minDimension = 0;
            minContentDimension = 0;
            maxDimension = float.MaxValue;
            maxContentDimension = float.MaxValue;
        }

        /// <summary>Check if this dimension is auto</summary>
        /// <returns>True if the dimension is to be automatically calculated, false if it was set via a property</returns>
        internal virtual bool IsAutoDimension() {
            return dimension == -1;
        }

        internal virtual float ParseDimension(CssContextNode node, String content, float maxAvailableDimension, float
             additionalWidthFix) {
            float fontSize = CssDimensionParsingUtils.ParseAbsoluteFontSize(node.GetStyles().Get(CssConstants.FONT_SIZE
                ));
            UnitValue unitValue = CssDimensionParsingUtils.ParseLengthValueToPt(content, fontSize, 0);
            if (unitValue == null) {
                return 0;
            }
            if (unitValue.IsPointValue()) {
                return unitValue.GetValue() + additionalWidthFix;
            }
            return maxAvailableDimension * unitValue.GetValue() / 100f;
        }
    }
}
