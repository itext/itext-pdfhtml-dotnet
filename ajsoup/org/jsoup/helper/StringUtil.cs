using System;
using System.Collections;
using System.Text;
using Java.Net;

namespace Org.Jsoup.Helper {
    /// <summary>A minimal String utility class.</summary>
    /// <remarks>A minimal String utility class. Designed for internal jsoup use only.</remarks>
    public sealed class StringUtil {
        private static readonly String[] padding = new String[] { "", " ", "  ", "   ", "    ", "     ", "      ", 
            "       ", "        ", "         ", "          " };

        // memoised padding up to 10
        /// <summary>Join a collection of strings by a seperator</summary>
        /// <param name="strings">collection of string objects</param>
        /// <param name="sep">string to place between strings</param>
        /// <returns>joined string</returns>
        public static String Join(ICollection strings, String sep) {
            return Join(strings.Iterator(), sep);
        }

        /// <summary>Join a collection of strings by a seperator</summary>
        /// <param name="strings">iterator of string objects</param>
        /// <param name="sep">string to place between strings</param>
        /// <returns>joined string</returns>
        public static String Join(IEnumerator strings, String sep) {
            if (!strings.HasNext()) {
                return "";
            }
            String start = strings.Next().ToString();
            if (!strings.HasNext()) {
                // only one, avoid builder
                return start;
            }
            StringBuilder sb = new StringBuilder(64).Append(start);
            while (strings.HasNext()) {
                sb.Append(sep);
                sb.Append(strings.Next());
            }
            return sb.ToString();
        }

        /// <summary>Returns space padding</summary>
        /// <param name="width">amount of padding desired</param>
        /// <returns>string of spaces * width</returns>
        public static String Padding(int width) {
            if (width < 0) {
                throw new ArgumentException("width must be > 0");
            }
            if (width < padding.Length) {
                return padding[width];
            }
            char[] @out = new char[width];
            for (int i = 0; i < width; i++) {
                @out[i] = ' ';
            }
            return @out.ToString();
        }

        /// <summary>Tests if a string is blank: null, emtpy, or only whitespace (" ", \r\n, \t, etc)</summary>
        /// <param name="string">string to test</param>
        /// <returns>if string is blank</returns>
        public static bool IsBlank(String @string) {
            if (@string == null || @string.Length == 0) {
                return true;
            }
            int l = @string.Length;
            for (int i = 0; i < l; i++) {
                if (!Org.Jsoup.Helper.StringUtil.IsWhitespace(@string.CodePointAt(i))) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Tests if a string is numeric, i.e.</summary>
        /// <remarks>Tests if a string is numeric, i.e. contains only digit characters</remarks>
        /// <param name="string">string to test</param>
        /// <returns>true if only digit chars, false if empty or null or contains non-digit chrs</returns>
        public static bool IsNumeric(String @string) {
            if (@string == null || @string.Length == 0) {
                return false;
            }
            int l = @string.Length;
            for (int i = 0; i < l; i++) {
                if (!char.IsDigit(@string.CodePointAt(i))) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Tests if a code point is "whitespace" as defined in the HTML spec.</summary>
        /// <param name="c">code point to test</param>
        /// <returns>true if code point is whitespace, false otherwise</returns>
        public static bool IsWhitespace(int c) {
            return c == ' ' || c == '\t' || c == '\n' || c == '\f' || c == '\r';
        }

        /// <summary>
        /// Normalise the whitespace within this string; multiple spaces collapse to a single, and all whitespace characters
        /// (e.g.
        /// </summary>
        /// <remarks>
        /// Normalise the whitespace within this string; multiple spaces collapse to a single, and all whitespace characters
        /// (e.g. newline, tab) convert to a simple space
        /// </remarks>
        /// <param name="string">content to normalise</param>
        /// <returns>normalised string</returns>
        public static String NormaliseWhitespace(String @string) {
            StringBuilder sb = new StringBuilder(@string.Length);
            AppendNormalisedWhitespace(sb, @string, false);
            return sb.ToString();
        }

        /// <summary>After normalizing the whitespace within a string, appends it to a string builder.</summary>
        /// <param name="accum">builder to append to</param>
        /// <param name="string">string to normalize whitespace within</param>
        /// <param name="stripLeading">set to true if you wish to remove any leading whitespace</param>
        public static void AppendNormalisedWhitespace(StringBuilder accum, String @string, bool stripLeading) {
            bool lastWasWhite = false;
            bool reachedNonWhite = false;
            int len = @string.Length;
            int c;
            for (int i = 0; i < len; i += Org.Jsoup.PortUtil.CharCount(c)) {
                c = @string.CodePointAt(i);
                if (IsWhitespace(c)) {
                    if ((stripLeading && !reachedNonWhite) || lastWasWhite) {
                        continue;
                    }
                    accum.Append(' ');
                    lastWasWhite = true;
                }
                else {
                    accum.AppendCodePoint(c);
                    lastWasWhite = false;
                    reachedNonWhite = true;
                }
            }
        }

        public static bool In(String needle, params String[] haystack) {
            foreach (String hay in haystack) {
                if (hay.Equals(needle)) {
                    return true;
                }
            }
            return false;
        }

        public static bool InSorted(String needle, String[] haystack) {
            return iText.IO.Util.JavaUtil.ArraysBinarySearch(haystack, needle) >= 0;
        }

        /// <summary>Create a new absolute URL, from a provided existing absolute URL and a relative URL component.</summary>
        /// <param name="base">the existing absolulte base URL</param>
        /// <param name="relUrl">the relative URL to resolve. (If it's already absolute, it will be returned)</param>
        /// <returns>the resolved absolute URL</returns>
        /// <exception cref="Java.Net.MalformedURLException">if an error occurred generating the URL</exception>
        public static Uri Resolve(Uri @base, String relUrl) {
            // workaround: java resolves '//path/file + ?foo' to '//path/?foo', not '//path/file?foo' as desired
            if (relUrl.StartsWith("?")) {
                relUrl = @base.GetPath() + relUrl;
            }
            // workaround: //example.com + ./foo = //example.com/./foo, not //example.com/foo
            if (relUrl.IndexOf('.') == 0 && @base.GetFile().IndexOf('/') != 0) {
                @base = new Uri(@base.GetProtocol(), @base.GetHost(), @base.GetPort(), "/" + @base.GetFile());
            }
            return new Uri(@base, relUrl);
        }

        /// <summary>Create a new absolute URL, from a provided existing absolute URL and a relative URL component.</summary>
        /// <param name="baseUrl">the existing absolute base URL</param>
        /// <param name="relUrl">the relative URL to resolve. (If it's already absolute, it will be returned)</param>
        /// <returns>an absolute URL if one was able to be generated, or the empty string if not</returns>
        public static String Resolve(String baseUrl, String relUrl) {
            Uri @base;
            try {
                try {
                    @base = new Uri(baseUrl);
                }
                catch (MalformedURLException) {
                    // the base is unsuitable, but the attribute/rel may be abs on its own, so try that
                    Uri abs = new Uri(relUrl);
                    return abs.ToExternalForm();
                }
                return Resolve(@base, relUrl).ToExternalForm();
            }
            catch (MalformedURLException) {
                return "";
            }
        }
    }
}
