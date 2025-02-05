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
using iText.Html2pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;

namespace iText.Html2pdf.Css {
    /// <summary>
    /// This test handles the case where a br tag causes the pdf to no longer be pdf/A compliant
    /// The underlying problem turns out to be that the inserted Text IElement has no font, and uses the default (Helvetica) font.
    /// </summary>
    /// <remarks>
    /// This test handles the case where a br tag causes the pdf to no longer be pdf/A compliant
    /// The underlying problem turns out to be that the inserted Text IElement has no font, and uses the default (Helvetica) font.
    /// The font does not get embedded, and as such, it breaks the compliancy.
    /// </remarks>
    [NUnit.Framework.Category("IntegrationTest")]
    public class BrTagTest : ExtendedITextTest {
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
        }

        [NUnit.Framework.Test]
        public virtual void Test() {
            String input = "<html>\n" + "<head><title>Test</title></head>" + "<body style=\"font-family: FreeSans;\">"
                 + "<h1>Test</h1>" + "<br />" + "<p>Hello World</p>" + "</body>" + "</html>";
            IList<IElement> elements = HtmlConverter.ConvertToElements(input);
            NUnit.Framework.Assert.AreEqual(3, elements.Count);
            NUnit.Framework.Assert.IsTrue(elements[1] is Paragraph);
            NUnit.Framework.Assert.AreEqual(1, ((Paragraph)elements[1]).GetChildren().Count);
            IElement iElement = ((Paragraph)elements[1]).GetChildren()[0];
            NUnit.Framework.Assert.AreEqual(new String[] { "freesans" }, iElement.GetProperty<String[]>(Property.FONT)
                );
        }
    }
}
