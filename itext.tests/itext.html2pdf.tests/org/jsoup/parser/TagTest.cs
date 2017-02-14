namespace Org.Jsoup.Parser {
    /// <summary>Tag tests.</summary>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public class TagTest {
        [NUnit.Framework.Test]
        public virtual void IsCaseInsensitive() {
            Org.Jsoup.Parser.Tag p1 = Org.Jsoup.Parser.Tag.ValueOf("P");
            Org.Jsoup.Parser.Tag p2 = Org.Jsoup.Parser.Tag.ValueOf("p");
            NUnit.Framework.Assert.AreEqual(p1, p2);
        }

        [NUnit.Framework.Test]
        public virtual void Trims() {
            Org.Jsoup.Parser.Tag p1 = Org.Jsoup.Parser.Tag.ValueOf("p");
            Org.Jsoup.Parser.Tag p2 = Org.Jsoup.Parser.Tag.ValueOf(" p ");
            NUnit.Framework.Assert.AreEqual(p1, p2);
        }

        [NUnit.Framework.Test]
        public virtual void Equality() {
            Org.Jsoup.Parser.Tag p1 = Org.Jsoup.Parser.Tag.ValueOf("p");
            Org.Jsoup.Parser.Tag p2 = Org.Jsoup.Parser.Tag.ValueOf("p");
            NUnit.Framework.Assert.IsTrue(p1.Equals(p2));
            NUnit.Framework.Assert.IsTrue(p1 == p2);
        }

        [NUnit.Framework.Test]
        public virtual void DivSemantics() {
            Org.Jsoup.Parser.Tag div = Org.Jsoup.Parser.Tag.ValueOf("div");
            NUnit.Framework.Assert.IsTrue(div.IsBlock());
            NUnit.Framework.Assert.IsTrue(div.FormatAsBlock());
        }

        [NUnit.Framework.Test]
        public virtual void PSemantics() {
            Org.Jsoup.Parser.Tag p = Org.Jsoup.Parser.Tag.ValueOf("p");
            NUnit.Framework.Assert.IsTrue(p.IsBlock());
            NUnit.Framework.Assert.IsFalse(p.FormatAsBlock());
        }

        [NUnit.Framework.Test]
        public virtual void ImgSemantics() {
            Org.Jsoup.Parser.Tag img = Org.Jsoup.Parser.Tag.ValueOf("img");
            NUnit.Framework.Assert.IsTrue(img.IsInline());
            NUnit.Framework.Assert.IsTrue(img.IsSelfClosing());
            NUnit.Framework.Assert.IsFalse(img.IsBlock());
        }

        [NUnit.Framework.Test]
        public virtual void DefaultSemantics() {
            Org.Jsoup.Parser.Tag foo = Org.Jsoup.Parser.Tag.ValueOf("foo");
            // not defined
            Org.Jsoup.Parser.Tag foo2 = Org.Jsoup.Parser.Tag.ValueOf("FOO");
            NUnit.Framework.Assert.AreEqual(foo, foo2);
            NUnit.Framework.Assert.IsTrue(foo.IsInline());
            NUnit.Framework.Assert.IsTrue(foo.FormatAsBlock());
        }
    }
}
