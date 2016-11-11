using System;

namespace Org.Jsoup {
    /// <summary>Text utils to ease testing</summary>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public class TextUtil {
        public static String StripNewlines(String text) {
            text = iText.IO.Util.StringUtil.ReplaceAll(text, "\\n\\s*", "");
            return text;
        }
    }
}
