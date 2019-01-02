using System;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
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

        internal virtual float ParseDimension(CssContextNode node, String content, float maxAvailableDimension) {
            float fontsize = FontStyleApplierUtil.ParseAbsoluteFontSize(node.GetStyles().Get(CssConstants.FONT_SIZE));
            UnitValue unitValue = CssUtils.ParseLengthValueToPt(content, fontsize, 0);
            if (unitValue == null) {
                return 0;
            }
            if (unitValue.IsPointValue()) {
                return unitValue.GetValue();
            }
            return maxAvailableDimension * unitValue.GetValue() / 100f;
        }
    }
}
