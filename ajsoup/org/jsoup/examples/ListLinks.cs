using System;
using Org.Jsoup.Helper;
using Org.Jsoup.Nodes;
using Org.Jsoup.Select;

namespace Org.Jsoup.Examples {
    /// <summary>Example program to list links from a URL.</summary>
    public class ListLinks {
        /// <exception cref="System.IO.IOException"/>
        public static void Main(String[] args) {
            Validate.IsTrue(args.Length == 1, "usage: supply url to fetch");
            String url = args[0];
            Print("Fetching {0}...", url);
            Document doc = Org.Jsoup.Jsoup.Connect(url).Get();
            Elements links = doc.Select("a[href]");
            Elements media = doc.Select("[src]");
            Elements imports = doc.Select("link[href]");
            Print("\nMedia: ({0})", media.Count);
            foreach (Element src in media) {
                if (src.TagName().Equals("img")) {
                    Print(" * {0}: <{1}> {2}x{3} ({4})", src.TagName(), src.Attr("abs:src"), src.Attr("width"), src.Attr("height"
                        ), Trim(src.Attr("alt"), 20));
                }
                else {
                    Print(" * {0}: <{1}>", src.TagName(), src.Attr("abs:src"));
                }
            }
            Print("\nImports: ({0})", imports.Count);
            foreach (Element link in imports) {
                Print(" * {0} <{1}> ({2})", link.TagName(), link.Attr("abs:href"), link.Attr("rel"));
            }
            Print("\nLinks: ({0})", links.Count);
            foreach (Element link_1 in links) {
                Print(" * a: <{0}>  ({1})", link_1.Attr("abs:href"), Trim(link_1.Text(), 35));
            }
        }

        private static void Print(String msg, params Object[] args) {
            System.Console.Out.WriteLine(String.Format(msg, args));
        }

        private static String Trim(String s, int width) {
            if (s.Length > width) {
                return s.JSubstring(0, width - 1) + ".";
            }
            else {
                return s;
            }
        }
    }
}
