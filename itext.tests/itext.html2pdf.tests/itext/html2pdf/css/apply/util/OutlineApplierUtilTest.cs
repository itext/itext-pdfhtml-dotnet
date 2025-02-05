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
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Test;

namespace iText.Html2pdf.Css.Apply.Util {
    [NUnit.Framework.Category("UnitTest")]
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
