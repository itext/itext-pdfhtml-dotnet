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
            Print("Fetching %s...", url);
            Document doc = Org.Jsoup.Jsoup.Connect(url).Get();
            Elements links = doc.Select("a[href]");
            Elements media = doc.Select("[src]");
            Elements imports = doc.Select("link[href]");
            Print("\nMedia: (%d)", media.Count);
            foreach (Element src in media) {
                if (src.TagName().Equals("img")) {
                    Print(" * %s: <%s> %sx%s (%s)", src.TagName(), src.Attr("abs:src"), src.Attr("width"), src.Attr("height"), 
                        Trim(src.Attr("alt"), 20));
                }
                else {
                    Print(" * %s: <%s>", src.TagName(), src.Attr("abs:src"));
                }
            }
            Print("\nImports: (%d)", imports.Count);
            foreach (Element link in imports) {
                Print(" * %s <%s> (%s)", link.TagName(), link.Attr("abs:href"), link.Attr("rel"));
            }
            Print("\nLinks: (%d)", links.Count);
            foreach (Element link_1 in links) {
                Print(" * a: <%s>  (%s)", link_1.Attr("abs:href"), Trim(link_1.Text(), 35));
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
