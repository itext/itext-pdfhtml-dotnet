using System;
using System.IO;
using Org.Jsoup.Helper;
using Org.Jsoup.Nodes;
using Org.Jsoup.Safety;

namespace Org.Jsoup {
    /// <summary>The core public access point to the jsoup functionality.</summary>
    /// <author>Jonathan Hedley</author>
    public class Jsoup {
        private Jsoup() {
        }

        /// <summary>Parse HTML into a Document.</summary>
        /// <remarks>Parse HTML into a Document. The parser will make a sensible, balanced document tree out of any HTML.
        ///     </remarks>
        /// <param name="html">HTML to parse</param>
        /// <param name="baseUri">
        /// The URL where the HTML was retrieved from. Used to resolve relative URLs to absolute URLs, that occur
        /// before the HTML declares a
        /// <c>&lt;base href&gt;</c>
        /// tag.
        /// </param>
        /// <returns>sane HTML</returns>
        public static Document Parse(String html, String baseUri) {
            return Org.Jsoup.Parser.Parser.Parse(html, baseUri);
        }

        /// <summary>Parse HTML into a Document, using the provided Parser.</summary>
        /// <remarks>
        /// Parse HTML into a Document, using the provided Parser. You can provide an alternate parser, such as a simple XML
        /// (non-HTML) parser.
        /// </remarks>
        /// <param name="html">HTML to parse</param>
        /// <param name="baseUri">
        /// The URL where the HTML was retrieved from. Used to resolve relative URLs to absolute URLs, that occur
        /// before the HTML declares a
        /// <c>&lt;base href&gt;</c>
        /// tag.
        /// </param>
        /// <param name="parser">
        /// alternate
        /// <see cref="Org.Jsoup.Parser.Parser.XmlParser()">parser</see>
        /// to use.
        /// </param>
        /// <returns>sane HTML</returns>
        public static Document Parse(String html, String baseUri, Org.Jsoup.Parser.Parser parser) {
            return parser.ParseInput(html, baseUri);
        }

        /// <summary>Parse HTML into a Document.</summary>
        /// <remarks>
        /// Parse HTML into a Document. As no base URI is specified, absolute URL detection relies on the HTML including a
        /// <c>&lt;base href&gt;</c>
        /// tag.
        /// </remarks>
        /// <param name="html">HTML to parse</param>
        /// <returns>sane HTML</returns>
        /// <seealso cref="Parse(System.String, System.String)"/>
        public static Document Parse(String html) {
            return Org.Jsoup.Parser.Parser.Parse(html, "");
        }

        /// <summary>
        /// Creates a new
        /// <see cref="Connection"/>
        /// to a URL. Use to fetch and parse a HTML page.
        /// <p>
        /// Use examples:
        /// <ul>
        /// <li><code>Document doc = Jsoup.connect("http://example.com").userAgent("Mozilla").data("name", "jsoup").get();</code></li>
        /// <li><code>Document doc = Jsoup.connect("http://example.com").cookie("auth", "token").post();</code></li>
        /// </ul>
        /// </summary>
        /// <param name="url">
        /// URL to connect to. The protocol must be
        /// <c>http</c>
        /// or
        /// <c>https</c>
        /// .
        /// </param>
        /// <returns>the connection. You can add data, cookies, and headers; set the user-agent, referrer, method; and then execute.
        ///     </returns>
        public static Connection Connect(String url) {
            return HttpConnection.Connect(url);
        }

        /// <summary>Parse the contents of a file as HTML.</summary>
        /// <param name="in">file to load HTML from</param>
        /// <param name="charsetName">
        /// (optional) character set of file contents. Set to
        /// <see langword="null"/>
        /// to determine from
        /// <c>http-equiv</c>
        /// meta tag, if
        /// present, or fall back to
        /// <c>UTF-8</c>
        /// (which is often safe to do).
        /// </param>
        /// <param name="baseUri">The URL where the HTML was retrieved from, to resolve relative links against.</param>
        /// <returns>sane HTML</returns>
        /// <exception cref="System.IO.IOException">if the file could not be found, or read, or if the charsetName is invalid.
        ///     </exception>
        public static Document Parse(FileInfo @in, String charsetName, String baseUri) {
            return DataUtil.Load(@in, charsetName, baseUri);
        }

        /// <summary>Parse the contents of a file as HTML.</summary>
        /// <remarks>Parse the contents of a file as HTML. The location of the file is used as the base URI to qualify relative URLs.
        ///     </remarks>
        /// <param name="in">file to load HTML from</param>
        /// <param name="charsetName">
        /// (optional) character set of file contents. Set to
        /// <see langword="null"/>
        /// to determine from
        /// <c>http-equiv</c>
        /// meta tag, if
        /// present, or fall back to
        /// <c>UTF-8</c>
        /// (which is often safe to do).
        /// </param>
        /// <returns>sane HTML</returns>
        /// <exception cref="System.IO.IOException">if the file could not be found, or read, or if the charsetName is invalid.
        ///     </exception>
        /// <seealso cref="Parse(System.IO.FileInfo, System.String, System.String)"/>
        public static Document Parse(FileInfo @in, String charsetName) {
            return DataUtil.Load(@in, charsetName, @in.FullName);
        }

        /// <summary>Read an input stream, and parse it to a Document.</summary>
        /// <param name="in">input stream to read. Make sure to close it after parsing.</param>
        /// <param name="charsetName">
        /// (optional) character set of file contents. Set to
        /// <see langword="null"/>
        /// to determine from
        /// <c>http-equiv</c>
        /// meta tag, if
        /// present, or fall back to
        /// <c>UTF-8</c>
        /// (which is often safe to do).
        /// </param>
        /// <param name="baseUri">The URL where the HTML was retrieved from, to resolve relative links against.</param>
        /// <returns>sane HTML</returns>
        /// <exception cref="System.IO.IOException">if the file could not be found, or read, or if the charsetName is invalid.
        ///     </exception>
        public static Document Parse(Stream @in, String charsetName, String baseUri) {
            return DataUtil.Load(@in, charsetName, baseUri);
        }

        /// <summary>Read an input stream, and parse it to a Document.</summary>
        /// <remarks>
        /// Read an input stream, and parse it to a Document. You can provide an alternate parser, such as a simple XML
        /// (non-HTML) parser.
        /// </remarks>
        /// <param name="in">input stream to read. Make sure to close it after parsing.</param>
        /// <param name="charsetName">
        /// (optional) character set of file contents. Set to
        /// <see langword="null"/>
        /// to determine from
        /// <c>http-equiv</c>
        /// meta tag, if
        /// present, or fall back to
        /// <c>UTF-8</c>
        /// (which is often safe to do).
        /// </param>
        /// <param name="baseUri">The URL where the HTML was retrieved from, to resolve relative links against.</param>
        /// <param name="parser">
        /// alternate
        /// <see cref="Org.Jsoup.Parser.Parser.XmlParser()">parser</see>
        /// to use.
        /// </param>
        /// <returns>sane HTML</returns>
        /// <exception cref="System.IO.IOException">if the file could not be found, or read, or if the charsetName is invalid.
        ///     </exception>
        public static Document Parse(Stream @in, String charsetName, String baseUri, Org.Jsoup.Parser.Parser parser
            ) {
            return DataUtil.Load(@in, charsetName, baseUri, parser);
        }

        /// <summary>
        /// Parse a fragment of HTML, with the assumption that it forms the
        /// <c>body</c>
        /// of the HTML.
        /// </summary>
        /// <param name="bodyHtml">body HTML fragment</param>
        /// <param name="baseUri">URL to resolve relative URLs against.</param>
        /// <returns>sane HTML document</returns>
        /// <seealso cref="Org.Jsoup.Nodes.Document.Body()"/>
        public static Document ParseBodyFragment(String bodyHtml, String baseUri) {
            return Org.Jsoup.Parser.Parser.ParseBodyFragment(bodyHtml, baseUri);
        }

        /// <summary>
        /// Parse a fragment of HTML, with the assumption that it forms the
        /// <c>body</c>
        /// of the HTML.
        /// </summary>
        /// <param name="bodyHtml">body HTML fragment</param>
        /// <returns>sane HTML document</returns>
        /// <seealso cref="Org.Jsoup.Nodes.Document.Body()"/>
        public static Document ParseBodyFragment(String bodyHtml) {
            return Org.Jsoup.Parser.Parser.ParseBodyFragment(bodyHtml, "");
        }

        /// <summary>Fetch a URL, and parse it as HTML.</summary>
        /// <remarks>
        /// Fetch a URL, and parse it as HTML. Provided for compatibility; in most cases use
        /// <see cref="Connect(System.String)"/>
        /// instead.
        /// <p>
        /// The encoding character set is determined by the content-type header or http-equiv meta tag, or falls back to
        /// <c>UTF-8</c>
        /// .
        /// </remarks>
        /// <param name="url">
        /// URL to fetch (with a GET). The protocol must be
        /// <c>http</c>
        /// or
        /// <c>https</c>
        /// .
        /// </param>
        /// <param name="timeoutMillis">Connection and read timeout, in milliseconds. If exceeded, IOException is thrown.
        ///     </param>
        /// <returns>The parsed HTML.</returns>
        /// <exception cref="Java.Net.MalformedURLException">if the request URL is not a HTTP or HTTPS URL, or is otherwise malformed
        ///     </exception>
        /// <exception cref="HttpStatusException">if the response is not OK and HTTP response errors are not ignored</exception>
        /// <exception cref="UnsupportedMimeTypeException">if the response mime type is not supported and those errors are not ignored
        ///     </exception>
        /// <exception cref="Java.Net.SocketTimeoutException">if the connection times out</exception>
        /// <exception cref="System.IO.IOException">if a connection or read error occurs</exception>
        /// <seealso cref="Connect(System.String)"/>
        public static Document Parse(Uri url, int timeoutMillis) {
            Connection con = HttpConnection.Connect(url);
            con.Timeout(timeoutMillis);
            return con.Get();
        }

        /// <summary>
        /// Get safe HTML from untrusted input HTML, by parsing input HTML and filtering it through a white-list of permitted
        /// tags and attributes.
        /// </summary>
        /// <param name="bodyHtml">input untrusted HTML (body fragment)</param>
        /// <param name="baseUri">URL to resolve relative URLs against</param>
        /// <param name="whitelist">white-list of permitted HTML elements</param>
        /// <returns>safe HTML (body fragment)</returns>
        /// <seealso cref="Org.Jsoup.Safety.Cleaner.Clean(Org.Jsoup.Nodes.Document)"/>
        public static String Clean(String bodyHtml, String baseUri, Whitelist whitelist) {
            Document dirty = ParseBodyFragment(bodyHtml, baseUri);
            Cleaner cleaner = new Cleaner(whitelist);
            Document clean = cleaner.Clean(dirty);
            return clean.Body().Html();
        }

        /// <summary>
        /// Get safe HTML from untrusted input HTML, by parsing input HTML and filtering it through a white-list of permitted
        /// tags and attributes.
        /// </summary>
        /// <param name="bodyHtml">input untrusted HTML (body fragment)</param>
        /// <param name="whitelist">white-list of permitted HTML elements</param>
        /// <returns>safe HTML (body fragment)</returns>
        /// <seealso cref="Org.Jsoup.Safety.Cleaner.Clean(Org.Jsoup.Nodes.Document)"/>
        public static String Clean(String bodyHtml, Whitelist whitelist) {
            return Clean(bodyHtml, "", whitelist);
        }

        /// <summary>
        /// Get safe HTML from untrusted input HTML, by parsing input HTML and filtering it through a white-list of
        /// permitted
        /// tags and attributes.
        /// </summary>
        /// <param name="bodyHtml">input untrusted HTML (body fragment)</param>
        /// <param name="baseUri">URL to resolve relative URLs against</param>
        /// <param name="whitelist">white-list of permitted HTML elements</param>
        /// <param name="outputSettings">document output settings; use to control pretty-printing and entity escape modes
        ///     </param>
        /// <returns>safe HTML (body fragment)</returns>
        /// <seealso cref="Org.Jsoup.Safety.Cleaner.Clean(Org.Jsoup.Nodes.Document)"/>
        public static String Clean(String bodyHtml, String baseUri, Whitelist whitelist, OutputSettings outputSettings
            ) {
            Document dirty = ParseBodyFragment(bodyHtml, baseUri);
            Cleaner cleaner = new Cleaner(whitelist);
            Document clean = cleaner.Clean(dirty);
            clean.OutputSettings(outputSettings);
            return clean.Body().Html();
        }

        /// <summary>Test if the input HTML has only tags and attributes allowed by the Whitelist.</summary>
        /// <remarks>
        /// Test if the input HTML has only tags and attributes allowed by the Whitelist. Useful for form validation. The input HTML should
        /// still be run through the cleaner to set up enforced attributes, and to tidy the output.
        /// </remarks>
        /// <param name="bodyHtml">HTML to test</param>
        /// <param name="whitelist">whitelist to test against</param>
        /// <returns>true if no tags or attributes were removed; false otherwise</returns>
        /// <seealso cref="Clean(System.String, Org.Jsoup.Safety.Whitelist)"></seealso>
        public static bool IsValid(String bodyHtml, Whitelist whitelist) {
            Document dirty = ParseBodyFragment(bodyHtml, "");
            Cleaner cleaner = new Cleaner(whitelist);
            return cleaner.IsValid(dirty);
        }
    }
}
