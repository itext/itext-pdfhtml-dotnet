using System;
using iText.IO.Util;

namespace Org.Jsoup.Helper {
    public class StringUtilTest {
        [NUnit.Framework.Test]
        public virtual void Join() {
            NUnit.Framework.Assert.AreEqual("", Org.Jsoup.Helper.StringUtil.Join(JavaUtil.ArraysAsList(""), " "));
            NUnit.Framework.Assert.AreEqual("one", Org.Jsoup.Helper.StringUtil.Join(JavaUtil.ArraysAsList("one"), " ")
                );
            NUnit.Framework.Assert.AreEqual("one two three", Org.Jsoup.Helper.StringUtil.Join(JavaUtil.ArraysAsList("one"
                , "two", "three"), " "));
        }

        [NUnit.Framework.Test]
        public virtual void Padding() {
            NUnit.Framework.Assert.AreEqual("", Org.Jsoup.Helper.StringUtil.Padding(0));
            NUnit.Framework.Assert.AreEqual(" ", Org.Jsoup.Helper.StringUtil.Padding(1));
            NUnit.Framework.Assert.AreEqual("  ", Org.Jsoup.Helper.StringUtil.Padding(2));
            NUnit.Framework.Assert.AreEqual("               ", Org.Jsoup.Helper.StringUtil.Padding(15));
        }

        [NUnit.Framework.Test]
        public virtual void IsBlank() {
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsBlank(null));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsBlank(""));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsBlank("      "));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsBlank("   \r\n  "));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsBlank("hello"));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsBlank("   hello   "));
        }

        [NUnit.Framework.Test]
        public virtual void IsNumeric() {
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsNumeric(null));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsNumeric(" "));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsNumeric("123 546"));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsNumeric("hello"));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsNumeric("123.334"));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsNumeric("1"));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsNumeric("1234"));
        }

        [NUnit.Framework.Test]
        public virtual void IsWhitespace() {
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsWhitespace('\t'));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsWhitespace('\n'));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsWhitespace('\r'));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsWhitespace('\f'));
            NUnit.Framework.Assert.IsTrue(Org.Jsoup.Helper.StringUtil.IsWhitespace(' '));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsWhitespace('\u00a0'));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsWhitespace('\u2000'));
            NUnit.Framework.Assert.IsFalse(Org.Jsoup.Helper.StringUtil.IsWhitespace('\u3000'));
        }

        [NUnit.Framework.Test]
        public virtual void NormaliseWhiteSpace() {
            NUnit.Framework.Assert.AreEqual(" ", Org.Jsoup.Helper.StringUtil.NormaliseWhitespace("    \r \n \r\n"));
            NUnit.Framework.Assert.AreEqual(" hello there ", Org.Jsoup.Helper.StringUtil.NormaliseWhitespace("   hello   \r \n  there    \n"
                ));
            NUnit.Framework.Assert.AreEqual("hello", Org.Jsoup.Helper.StringUtil.NormaliseWhitespace("hello"));
            NUnit.Framework.Assert.AreEqual("hello there", Org.Jsoup.Helper.StringUtil.NormaliseWhitespace("hello\nthere"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void NormaliseWhiteSpaceHandlesHighSurrogates() {
            String test71540chars = "\ud869\udeb2\u304b\u309a  1";
            String test71540charsExpectedSingleWhitespace = "\ud869\udeb2\u304b\u309a 1";
            NUnit.Framework.Assert.AreEqual(test71540charsExpectedSingleWhitespace, Org.Jsoup.Helper.StringUtil.NormaliseWhitespace
                (test71540chars));
            String extractedText = Org.Jsoup.Jsoup.Parse(test71540chars).Text();
            NUnit.Framework.Assert.AreEqual(test71540charsExpectedSingleWhitespace, extractedText);
        }

        [NUnit.Framework.Test]
        public virtual void ResolvesRelativeUrls() {
            NUnit.Framework.Assert.AreEqual("http://example.com/one/two?three", Org.Jsoup.Helper.StringUtil.Resolve("http://example.com"
                , "./one/two?three"));
            NUnit.Framework.Assert.AreEqual("http://example.com/one/two?three", Org.Jsoup.Helper.StringUtil.Resolve("http://example.com?one"
                , "./one/two?three"));
            NUnit.Framework.Assert.AreEqual("http://example.com/one/two?three#four", Org.Jsoup.Helper.StringUtil.Resolve
                ("http://example.com", "./one/two?three#four"));
            NUnit.Framework.Assert.AreEqual("https://example.com/one", Org.Jsoup.Helper.StringUtil.Resolve("http://example.com/"
                , "https://example.com/one"));
            NUnit.Framework.Assert.AreEqual("http://example.com/one/two.html", Org.Jsoup.Helper.StringUtil.Resolve("http://example.com/two/"
                , "../one/two.html"));
            NUnit.Framework.Assert.AreEqual("https://example2.com/one", Org.Jsoup.Helper.StringUtil.Resolve("https://example.com/"
                , "//example2.com/one"));
            NUnit.Framework.Assert.AreEqual("https://example.com:8080/one", Org.Jsoup.Helper.StringUtil.Resolve("https://example.com:8080"
                , "./one"));
            NUnit.Framework.Assert.AreEqual("https://example2.com/one", Org.Jsoup.Helper.StringUtil.Resolve("http://example.com/"
                , "https://example2.com/one"));
            NUnit.Framework.Assert.AreEqual("https://example.com/one", Org.Jsoup.Helper.StringUtil.Resolve("wrong", "https://example.com/one"
                ));
            NUnit.Framework.Assert.AreEqual("https://example.com/one", Org.Jsoup.Helper.StringUtil.Resolve("https://example.com/one"
                , ""));
            NUnit.Framework.Assert.AreEqual("", Org.Jsoup.Helper.StringUtil.Resolve("wrong", "also wrong"));
            NUnit.Framework.Assert.AreEqual("ftp://example.com/one", Org.Jsoup.Helper.StringUtil.Resolve("ftp://example.com/two/"
                , "../one"));
            NUnit.Framework.Assert.AreEqual("ftp://example.com/one/two.c", Org.Jsoup.Helper.StringUtil.Resolve("ftp://example.com/one/"
                , "./two.c"));
            NUnit.Framework.Assert.AreEqual("ftp://example.com/one/two.c", Org.Jsoup.Helper.StringUtil.Resolve("ftp://example.com/one/"
                , "two.c"));
        }
    }
}
