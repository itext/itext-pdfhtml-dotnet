namespace Org.Jsoup.Nodes {
    /// <summary>Tests for Attributes.</summary>
    /// <author>Jonathan Hedley</author>
    public class AttributesTest {
        [NUnit.Framework.Test]
        public virtual void Html() {
            Attributes a = new Attributes();
            a.Put("Tot", "a&p");
            a.Put("Hello", "There");
            a.Put("data-name", "Jsoup");
            NUnit.Framework.Assert.AreEqual(3, a.Size());
            NUnit.Framework.Assert.IsTrue(a.HasKey("tot"));
            NUnit.Framework.Assert.IsTrue(a.HasKey("Hello"));
            NUnit.Framework.Assert.IsTrue(a.HasKey("data-name"));
            NUnit.Framework.Assert.AreEqual(1, a.Dataset().Count);
            NUnit.Framework.Assert.AreEqual("Jsoup", a.Dataset().Get("name"));
            NUnit.Framework.Assert.AreEqual("a&p", a.Get("tot"));
            NUnit.Framework.Assert.AreEqual(" tot=\"a&amp;p\" hello=\"There\" data-name=\"Jsoup\"", a.Html());
            NUnit.Framework.Assert.AreEqual(a.Html(), a.ToString());
        }
    }
}
