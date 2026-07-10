/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Html2pdf;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Selector.Item {
    [NUnit.Framework.Category("UnitTest")]
    public class AttributeValueAsLiteralTest : ExtendedHtmlConversionITextTest {
        [NUnit.Framework.Test]
        public virtual void ValueIsLiteralNotEqualTest() {
            String html = "<html>" + "<head>" + "<style>" + "[data-info~=\"a.c\"] { color:red; }" + "</style>" + "</head>"
                 + "<body>" + "<p data-info=\" abc\">Text</p>" + "</body>" + "</html>";
            IList<IElement> elements = HtmlConverter.ConvertToElements(html);
            Paragraph p = (Paragraph)elements[0];
            TransparentColor color = p.GetProperty<TransparentColor>(Property.FONT_COLOR);
            NUnit.Framework.Assert.IsNull(color);
        }

        [NUnit.Framework.Test]
        public virtual void ValueIsLiteralEqualTest() {
            String html = "<html>" + "<head>" + "<style>" + "[data-info~=\"a]c\"] { color:red; }" + "</style>" + "</head>"
                 + "<body>" + "<p data-info=\" a]c\">Text</p>" + "</body>" + "</html>";
            IList<IElement> elements = HtmlConverter.ConvertToElements(html);
            Paragraph p = (Paragraph)elements[0];
            TransparentColor color = p.GetProperty<TransparentColor>(Property.FONT_COLOR);
            NUnit.Framework.Assert.AreEqual(new float[] { 1f, 0f, 0f }, color.GetColor().GetColorValue());
        }
    }
}
