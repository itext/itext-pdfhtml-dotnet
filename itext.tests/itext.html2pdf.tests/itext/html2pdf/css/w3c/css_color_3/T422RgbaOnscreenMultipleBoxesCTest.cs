/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_color_3 {
    public class T422RgbaOnscreenMultipleBoxesCTest : W3CCssTest {
        // In pdf, if several layers of the same color are drawn one atop another, then in case of transparency
        // this place would look more "saturated", than a single layer of the transparent color. However, this is not
        // true for the css, apparently.
        protected internal override String GetHtmlFileName() {
            return "t422-rgba-onscreen-multiple-boxes-c.xht";
        }

        [NUnit.Framework.Test]
        public override void Test() {
            base.Test();
        }
    }
}
