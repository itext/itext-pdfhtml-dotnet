using System;
using Org.Jsoup;

namespace Org.Jsoup.Nodes {
    public class AttributeTest {
        [NUnit.Framework.Test]
        public virtual void Html() {
            Org.Jsoup.Nodes.Attribute attr = new Org.Jsoup.Nodes.Attribute("key", "value &");
            NUnit.Framework.Assert.AreEqual("key=\"value &amp;\"", attr.Html());
            NUnit.Framework.Assert.AreEqual(attr.Html(), attr.ToString());
        }

        [NUnit.Framework.Test]
        public virtual void TestWithSupplementaryCharacterInAttributeKeyAndValue() {
            String s = new String(PortUtil.ToChars(135361));
            Org.Jsoup.Nodes.Attribute attr = new Org.Jsoup.Nodes.Attribute(s, "A" + s + "B");
            NUnit.Framework.Assert.AreEqual(s + "=\"A" + s + "B\"", attr.Html());
            NUnit.Framework.Assert.AreEqual(attr.Html(), attr.ToString());
        }
    }
}
