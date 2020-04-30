using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Parse;
using iText.Test;

namespace iText.Html2pdf.Css.Util {
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
