using System;
using System.Text;
using Org.Jsoup.Helper;
using Org.Jsoup.Nodes;
using Org.Jsoup.Select;

namespace Org.Jsoup.Examples {
    /// <summary>HTML to plain-text.</summary>
    /// <remarks>
    /// HTML to plain-text. This example program demonstrates the use of jsoup to convert HTML input to lightly-formatted
    /// plain-text. That is divergent from the general goal of jsoup's .text() methods, which is to get clean data from a
    /// scrape.
    /// <p>
    /// Note that this is a fairly simplistic formatter -- for real world use you'll want to embrace and extend.
    /// </p>
    /// <p>
    /// To invoke from the command line, assuming you've downloaded the jsoup jar to your current directory:</p>
    /// <p><code>java -cp jsoup.jar org.jsoup.examples.HtmlToPlainText url [selector]</code></p>
    /// where <i>url</i> is the URL to fetch, and <i>selector</i> is an optional CSS selector.
    /// </remarks>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public class HtmlToPlainText {
        private const String userAgent = "Mozilla/5.0 (jsoup)";

        private const int timeout = 5 * 1000;

        /// <exception cref="System.IO.IOException"/>
        public static void Main(params String[] args) {
            Validate.IsTrue(args.Length == 1 || args.Length == 2, "usage: java -cp jsoup.jar org.jsoup.examples.HtmlToPlainText url [selector]"
                );
            String url = args[0];
            String selector = args.Length == 2 ? args[1] : null;
            // fetch the specified URL and parse to a HTML DOM
            Document doc = Org.Jsoup.Jsoup.Connect(url).UserAgent(userAgent).Timeout(timeout).Get();
            HtmlToPlainText formatter = new HtmlToPlainText();
            if (selector != null) {
                Elements elements = doc.Select(selector);
                // get each element that matches the CSS selector
                foreach (Element element in elements) {
                    String plainText = formatter.GetPlainText(element);
                    // format that element to plain text
                    System.Console.Out.WriteLine(plainText);
                }
            }
            else {
                // format the whole doc
                String plainText = formatter.GetPlainText(doc);
                System.Console.Out.WriteLine(plainText);
            }
        }

        /// <summary>Format an Element to plain-text</summary>
        /// <param name="element">the root element to format</param>
        /// <returns>formatted text</returns>
        public virtual String GetPlainText(Element element) {
            HtmlToPlainText.FormattingVisitor formatter = new HtmlToPlainText.FormattingVisitor(this);
            NodeTraversor traversor = new NodeTraversor(formatter);
            traversor.Traverse(element);
            // walk the DOM, and call .head() and .tail() for each node
            return formatter.ToString();
        }

        private class FormattingVisitor : NodeVisitor {
            private const int maxWidth = 80;

            private int width = 0;

            private StringBuilder accum = new StringBuilder();

            // the formatting rules, implemented in a breadth-first DOM traverse
            // holds the accumulated text
            // hit when the node is first seen
            public virtual void Head(Node node, int depth) {
                String name = node.NodeName();
                if (node is TextNode) {
                    this.Append(((TextNode)node).Text());
                }
                else {
                    // TextNodes carry all user-readable text in the DOM.
                    if (name.Equals("li")) {
                        this.Append("\n * ");
                    }
                    else {
                        if (name.Equals("dt")) {
                            this.Append("  ");
                        }
                        else {
                            if (Org.Jsoup.Helper.StringUtil.In(name, "p", "h1", "h2", "h3", "h4", "h5", "tr")) {
                                this.Append("\n");
                            }
                        }
                    }
                }
            }

            // hit when all of the node's children (if any) have been visited
            public virtual void Tail(Node node, int depth) {
                String name = node.NodeName();
                if (Org.Jsoup.Helper.StringUtil.In(name, "br", "dd", "dt", "p", "h1", "h2", "h3", "h4", "h5")) {
                    this.Append("\n");
                }
                else {
                    if (name.Equals("a")) {
                        this.Append(String.Format(" <{0}>", node.AbsUrl("href")));
                    }
                }
            }

            // appends text to the string builder with a simple word wrap method
            private void Append(String text) {
                if (text.StartsWith("\n")) {
                    this.width = 0;
                }
                // reset counter if starts with a newline. only from formats above, not in natural text
                if (text.Equals(" ") && (this.accum.Length == 0 || Org.Jsoup.Helper.StringUtil.In(this.accum.ToString().Substring
                    (this.accum.Length - 1), " ", "\n"))) {
                    return;
                }
                // don't accumulate long runs of empty spaces
                if (text.Length + this.width > HtmlToPlainText.FormattingVisitor.maxWidth) {
                    // won't fit, needs to wrap
                    String[] words = text.Split("\\s+");
                    for (int i = 0; i < words.Length; i++) {
                        String word = words[i];
                        bool last = i == words.Length - 1;
                        if (!last) {
                            // insert a space if not the last word
                            word = word + " ";
                        }
                        if (word.Length + this.width > HtmlToPlainText.FormattingVisitor.maxWidth) {
                            // wrap and reset counter
                            this.accum.Append("\n").Append(word);
                            this.width = word.Length;
                        }
                        else {
                            this.accum.Append(word);
                            this.width += word.Length;
                        }
                    }
                }
                else {
                    // fits as is, without need to wrap text
                    this.accum.Append(text);
                    this.width += text.Length;
                }
            }

            public override String ToString() {
                return this.accum.ToString();
            }

            internal FormattingVisitor(HtmlToPlainText _enclosing) {
                this._enclosing = _enclosing;
            }

            private readonly HtmlToPlainText _enclosing;
        }
    }
}
