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
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Parse;
using iText.Test;

namespace iText.Html2pdf.Css.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class CssStyleSheetAnalyzerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void SimpleNegativeTest() {
            CssStyleSheet styleSheet = CssStyleSheetParser.Parse("* { color: red; }");
            NUnit.Framework.Assert.IsFalse(CssStyleSheetAnalyzer.CheckPagesCounterPresence(styleSheet));
        }

        [NUnit.Framework.Test]
        public virtual void PagesInCounterSimpleTest() {
            CssStyleSheet styleSheet = CssStyleSheetParser.Parse(".x::before { content: counter(pages) }");
            NUnit.Framework.Assert.IsTrue(CssStyleSheetAnalyzer.CheckPagesCounterPresence(styleSheet));
        }

        [NUnit.Framework.Test]
        public virtual void PagesInCountersSimpleTest() {
            CssStyleSheet styleSheet = CssStyleSheetParser.Parse(".x::before { content: counters(pages,'.') }");
            NUnit.Framework.Assert.IsTrue(CssStyleSheetAnalyzer.CheckPagesCounterPresence(styleSheet));
        }

        [NUnit.Framework.Test]
        public virtual void PagesInCounterSpacesPresenceTest() {
            CssStyleSheet styleSheet = CssStyleSheetParser.Parse(".x::before { content: counter( pages ) }");
            NUnit.Framework.Assert.IsTrue(CssStyleSheetAnalyzer.CheckPagesCounterPresence(styleSheet));
        }

        [NUnit.Framework.Test]
        public virtual void PagesInCountersSpacesPresenceTest() {
            CssStyleSheet styleSheet = CssStyleSheetParser.Parse(".x::before { content: counters( pages,'.') }");
            NUnit.Framework.Assert.IsTrue(CssStyleSheetAnalyzer.CheckPagesCounterPresence(styleSheet));
        }

        [NUnit.Framework.Test]
        public virtual void CounterWithoutPagesTest() {
            CssStyleSheet styleSheet = CssStyleSheetParser.Parse(".x::before { content: counter(othercounter) }");
            NUnit.Framework.Assert.IsFalse(CssStyleSheetAnalyzer.CheckPagesCounterPresence(styleSheet));
        }
    }
}
