using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Test;

namespace iText.Html2pdf.Css.Apply.Util {
    [NUnit.Framework.Category("Unit test")]
    public class OutlineApplierUtilTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void GrooveBorderColorTest() {
            Border border = OutlineApplierUtil.GetCertainBorder("10px", "groove", "device-cmyk(0, 81%, 81%, 30%", 12.0f
                , 12.0f);
            Color expected = new DeviceCmyk(0, 81, 81, 30);
            NUnit.Framework.Assert.AreEqual(expected, border.GetColor());
        }

        [NUnit.Framework.Test]
        public virtual void RidgeBorderColorTest() {
            Border border = OutlineApplierUtil.GetCertainBorder("10px", "ridge", "device-cmyk(0, 81%, 81%, 30%", 12.0f
                , 12.0f);
            Color expected = new DeviceCmyk(0, 81, 81, 30);
            NUnit.Framework.Assert.AreEqual(expected, border.GetColor());
        }

        [NUnit.Framework.Test]
        public virtual void InsetBorderColorTest() {
            Border border = OutlineApplierUtil.GetCertainBorder("10px", "inset", "device-cmyk(0, 81%, 81%, 30%", 12.0f
                , 12.0f);
            Color expected = new DeviceCmyk(0, 81, 81, 30);
            NUnit.Framework.Assert.AreEqual(expected, border.GetColor());
        }

        [NUnit.Framework.Test]
        public virtual void OutsetBorderColorTest() {
            Border border = OutlineApplierUtil.GetCertainBorder("10px", "outset", "device-cmyk(0, 81%, 81%, 30%", 12.0f
                , 12.0f);
            Color expected = new DeviceCmyk(0, 81, 81, 30);
            NUnit.Framework.Assert.AreEqual(expected, border.GetColor());
        }
    }
}
