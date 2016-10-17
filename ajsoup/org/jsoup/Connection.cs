using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Org.Jsoup.Nodes;

namespace Org.Jsoup {
    /// <summary>A Connection provides a convenient interface to fetch content from the web, and parse them into Documents.
    ///     </summary>
    /// <remarks>
    /// A Connection provides a convenient interface to fetch content from the web, and parse them into Documents.
    /// <p>
    /// To get a new Connection, use
    /// <see cref="Jsoup.Connect(System.String)"/>
    /// . Connections contain
    /// <see cref="IRequest"/>
    /// and
    /// <see cref="IResponse"/>
    /// objects. The request objects are reusable as prototype requests.
    /// </p>
    /// <p>
    /// Request configuration can be made using either the shortcut methods in Connection (e.g.
    /// <see cref="UserAgent(System.String)"/>
    /// ),
    /// or by methods in the Connection.Request object directly. All request configuration must be made before the request is
    /// executed.
    /// </p>
    /// </remarks>
    public interface Connection {
        /// <summary>Set the request URL to fetch.</summary>
        /// <remarks>Set the request URL to fetch. The protocol must be HTTP or HTTPS.</remarks>
        /// <param name="url">URL to connect to</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Url(Uri url);

        /// <summary>Set the request URL to fetch.</summary>
        /// <remarks>Set the request URL to fetch. The protocol must be HTTP or HTTPS.</remarks>
        /// <param name="url">URL to connect to</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Url(String url);

        /// <summary>Set the proxy to use for this request.</summary>
        /// <remarks>Set the proxy to use for this request. Set to <code>null</code> to disable.</remarks>
        /// <param name="proxy">proxy to use</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Proxy(WebProxy proxy);

        /// <summary>Set the HTTP proxy to use for this request.</summary>
        /// <param name="host">the proxy hostname</param>
        /// <param name="port">the proxy port</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Proxy(String host, int port);

        /// <summary>Set the request user-agent header.</summary>
        /// <param name="userAgent">user-agent to use</param>
        /// <returns>this Connection, for chaining</returns>
        Connection UserAgent(String userAgent);

        /// <summary>Set the request timeouts (connect and read).</summary>
        /// <remarks>
        /// Set the request timeouts (connect and read). If a timeout occurs, an IOException will be thrown. The default
        /// timeout is 3 seconds (3000 millis). A timeout of zero is treated as an infinite timeout.
        /// </remarks>
        /// <param name="millis">number of milliseconds (thousandths of a second) before timing out connects or reads.
        ///     </param>
        /// <returns>this Connection, for chaining</returns>
        Connection Timeout(int millis);

        /// <summary>
        /// Set the maximum bytes to read from the (uncompressed) connection into the body, before the connection is closed,
        /// and the input truncated.
        /// </summary>
        /// <remarks>
        /// Set the maximum bytes to read from the (uncompressed) connection into the body, before the connection is closed,
        /// and the input truncated. The default maximum is 1MB. A max size of zero is treated as an infinite amount (bounded
        /// only by your patience and the memory available on your machine).
        /// </remarks>
        /// <param name="bytes">number of bytes to read from the input before truncating</param>
        /// <returns>this Connection, for chaining</returns>
        Connection MaxBodySize(int bytes);

        /// <summary>Set the request referrer (aka "referer") header.</summary>
        /// <param name="referrer">referrer to use</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Referrer(String referrer);

        /// <summary>Configures the connection to (not) follow server redirects.</summary>
        /// <remarks>Configures the connection to (not) follow server redirects. By default this is <b>true</b>.</remarks>
        /// <param name="followRedirects">true if server redirects should be followed.</param>
        /// <returns>this Connection, for chaining</returns>
        Connection FollowRedirects(bool followRedirects);

        /// <summary>Set the request method to use, GET or POST.</summary>
        /// <remarks>Set the request method to use, GET or POST. Default is GET.</remarks>
        /// <param name="method">HTTP request method</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Method(Org.Jsoup.Method method);

        /// <summary>Configures the connection to not throw exceptions when a HTTP error occurs.</summary>
        /// <remarks>
        /// Configures the connection to not throw exceptions when a HTTP error occurs. (4xx - 5xx, e.g. 404 or 500). By
        /// default this is <b>false</b>; an IOException is thrown if an error is encountered. If set to <b>true</b>, the
        /// response is populated with the error body, and the status message will reflect the error.
        /// </remarks>
        /// <param name="ignoreHttpErrors">- false (default) if HTTP errors should be ignored.</param>
        /// <returns>this Connection, for chaining</returns>
        Connection IgnoreHttpErrors(bool ignoreHttpErrors);

        /// <summary>Ignore the document's Content-Type when parsing the response.</summary>
        /// <remarks>
        /// Ignore the document's Content-Type when parsing the response. By default this is <b>false</b>, an unrecognised
        /// content-type will cause an IOException to be thrown. (This is to prevent producing garbage by attempting to parse
        /// a JPEG binary image, for example.) Set to true to force a parse attempt regardless of content type.
        /// </remarks>
        /// <param name="ignoreContentType">
        /// set to true if you would like the content type ignored on parsing the response into a
        /// Document.
        /// </param>
        /// <returns>this Connection, for chaining</returns>
        Connection IgnoreContentType(bool ignoreContentType);

        /// <summary>Disable/enable TSL certificates validation for HTTPS requests.</summary>
        /// <remarks>
        /// Disable/enable TSL certificates validation for HTTPS requests.
        /// <p>
        /// By default this is <b>true</b>; all
        /// connections over HTTPS perform normal validation of certificates, and will abort requests if the provided
        /// certificate does not validate.
        /// </p>
        /// <p>
        /// Some servers use expired, self-generated certificates; or your JDK may not
        /// support SNI hosts. In which case, you may want to enable this setting.
        /// </p>
        /// <p>
        /// <b>Be careful</b> and understand why you need to disable these validations.
        /// </p>
        /// </remarks>
        /// <param name="value">if should validate TSL (SSL) certificates. <b>true</b> by default.</param>
        /// <returns>this Connection, for chaining</returns>
        Connection ValidateTLSCertificates(bool value);

        /// <summary>Add a request data parameter.</summary>
        /// <remarks>
        /// Add a request data parameter. Request parameters are sent in the request query string for GETs, and in the
        /// request body for POSTs. A request may have multiple values of the same name.
        /// </remarks>
        /// <param name="key">data key</param>
        /// <param name="value">data value</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Data(String key, String value);

        /// <summary>Add an input stream as a request data paramater.</summary>
        /// <remarks>
        /// Add an input stream as a request data paramater. For GETs, has no effect, but for POSTS this will upload the
        /// input stream.
        /// </remarks>
        /// <param name="key">data key (form item name)</param>
        /// <param name="filename">
        /// the name of the file to present to the remove server. Typically just the name, not path,
        /// component.
        /// </param>
        /// <param name="inputStream">
        /// the input stream to upload, that you probably obtained from a
        /// <see cref="System.IO.FileStream"/>
        /// .
        /// You must close the InputStream in a
        /// <c>finally</c>
        /// block.
        /// </param>
        /// <returns>this Connections, for chaining</returns>
        Connection Data(String key, String filename, Stream inputStream);

        /// <summary>Adds all of the supplied data to the request data parameters</summary>
        /// <param name="data">collection of data parameters</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Data(ICollection<IKeyVal> data);

        /// <summary>Adds all of the supplied data to the request data parameters</summary>
        /// <param name="data">map of data parameters</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Data(IDictionary<String, String> data);

        /// <summary>Add a number of request data parameters.</summary>
        /// <remarks>
        /// Add a number of request data parameters. Multiple parameters may be set at once, e.g.: <code>.data("name",
        /// "jsoup", "language", "Java", "language", "English");</code> creates a query string like:
        /// <code>
        /// <literal>?name=jsoup&language=Java&language=English</literal>
        /// </code>
        /// </remarks>
        /// <param name="keyvals">a set of key value pairs.</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Data(params String[] keyvals);

        /// <summary>Get the data KeyVal for this key, if any</summary>
        /// <param name="key">the data key</param>
        /// <returns>null if not set</returns>
        IKeyVal Data(String key);

        /// <summary>Set a POST (or PUT) request body.</summary>
        /// <remarks>
        /// Set a POST (or PUT) request body. Useful when a server expects a plain request body, not a set for URL
        /// encoded form key/value pairs. E.g.:
        /// <code><pre>Jsoup.connect(url)
        /// .requestBody(json)
        /// .header("Content-Type", "application/json")
        /// .post();</pre></code>
        /// If any data key/vals are supplied, they will be sent as URL query params.
        /// </remarks>
        /// <returns>this Request, for chaining</returns>
        Connection RequestBody(String body);

        /// <summary>Set a request header.</summary>
        /// <param name="name">header name</param>
        /// <param name="value">header value</param>
        /// <returns>this Connection, for chaining</returns>
        /// <seealso cref="IBase{T}.Headers()"/>
        Connection Header(String name, String value);

        /// <summary>Set a cookie to be sent in the request.</summary>
        /// <param name="name">name of cookie</param>
        /// <param name="value">value of cookie</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Cookie(String name, String value);

        /// <summary>Adds each of the supplied cookies to the request.</summary>
        /// <param name="cookies">
        /// map of cookie name
        /// <literal>-&gt;</literal>
        /// value pairs
        /// </param>
        /// <returns>this Connection, for chaining</returns>
        Connection Cookies(IDictionary<String, String> cookies);

        /// <summary>Provide an alternate parser to use when parsing the response to a Document.</summary>
        /// <remarks>
        /// Provide an alternate parser to use when parsing the response to a Document. If not set, defaults to the HTML
        /// parser, unless the response content-type is XML, in which case the XML parser is used.
        /// </remarks>
        /// <param name="parser">alternate parser</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Parser(Org.Jsoup.Parser.Parser parser);

        /// <summary>Sets the default post data character set for x-www-form-urlencoded post data</summary>
        /// <param name="charset">character set to encode post data</param>
        /// <returns>this Connection, for chaining</returns>
        Connection PostDataCharset(String charset);

        /// <summary>Execute the request as a GET, and parse the result.</summary>
        /// <returns>parsed Document</returns>
        /// <exception cref="Java.Net.MalformedURLException">if the request URL is not a HTTP or HTTPS URL, or is otherwise malformed
        ///     </exception>
        /// <exception cref="HttpStatusException">if the response is not OK and HTTP response errors are not ignored</exception>
        /// <exception cref="UnsupportedMimeTypeException">if the response mime type is not supported and those errors are not ignored
        ///     </exception>
        /// <exception cref="Java.Net.SocketTimeoutException">if the connection times out</exception>
        /// <exception cref="System.IO.IOException">on error</exception>
        Document Get();

        /// <summary>Execute the request as a POST, and parse the result.</summary>
        /// <returns>parsed Document</returns>
        /// <exception cref="Java.Net.MalformedURLException">if the request URL is not a HTTP or HTTPS URL, or is otherwise malformed
        ///     </exception>
        /// <exception cref="HttpStatusException">if the response is not OK and HTTP response errors are not ignored</exception>
        /// <exception cref="UnsupportedMimeTypeException">if the response mime type is not supported and those errors are not ignored
        ///     </exception>
        /// <exception cref="Java.Net.SocketTimeoutException">if the connection times out</exception>
        /// <exception cref="System.IO.IOException">on error</exception>
        Document Post();

        /// <summary>Execute the request.</summary>
        /// <returns>a response object</returns>
        /// <exception cref="Java.Net.MalformedURLException">if the request URL is not a HTTP or HTTPS URL, or is otherwise malformed
        ///     </exception>
        /// <exception cref="HttpStatusException">if the response is not OK and HTTP response errors are not ignored</exception>
        /// <exception cref="UnsupportedMimeTypeException">if the response mime type is not supported and those errors are not ignored
        ///     </exception>
        /// <exception cref="Java.Net.SocketTimeoutException">if the connection times out</exception>
        /// <exception cref="System.IO.IOException">on error</exception>
        IResponse Execute();

        /// <summary>Get the request object associated with this connection</summary>
        /// <returns>request</returns>
        IRequest Request();

        /// <summary>Set the connection's request</summary>
        /// <param name="request">new request object</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Request(IRequest request);

        /// <summary>Get the response, once the request has been executed</summary>
        /// <returns>response</returns>
        IResponse Response();

        /// <summary>Set the connection's response</summary>
        /// <param name="response">new response</param>
        /// <returns>this Connection, for chaining</returns>
        Connection Response(IResponse response);
    }

    /// <summary>GET and POST http methods.</summary>
    public class Method {
        public static Org.Jsoup.Method GET = new Org.Jsoup.Method(false, "GET");

        public static Org.Jsoup.Method POST = new Org.Jsoup.Method(true, "POST");

        public static Org.Jsoup.Method PUT = new Org.Jsoup.Method(true, "PUT");

        public static Org.Jsoup.Method DELETE = new Org.Jsoup.Method(false, "DELETE");

        public static Org.Jsoup.Method PATCH = new Org.Jsoup.Method(true, "PATCH");

        public static Org.Jsoup.Method HEAD = new Org.Jsoup.Method(false, "HEAD");

        public static Org.Jsoup.Method OPTIONS = new Org.Jsoup.Method(false, "OPTIONS");

        public static Org.Jsoup.Method TRACE = new Org.Jsoup.Method(false, "TRACE");

        private static IDictionary<String, Org.Jsoup.Method> cash;

        static Method() {
            cash = new Dictionary<String, Org.Jsoup.Method>();
            cash[GET.name] = GET;
            cash[POST.name] = POST;
            cash[PUT.name] = PUT;
            cash[DELETE.name] = DELETE;
            cash[PATCH.name] = PATCH;
            cash[HEAD.name] = HEAD;
            cash[OPTIONS.name] = OPTIONS;
            cash[TRACE.name] = TRACE;
        }

        private readonly bool hasBody;

        private readonly String name;

        internal Method(bool hasBody, String name) {
            this.hasBody = hasBody;
            this.name = name;
        }

        public static Org.Jsoup.Method ValueOf(String name) {
            return cash.Get(name);
        }

        /// <summary>Check if this HTTP method has/needs a request body</summary>
        /// <returns>if body needed</returns>
        public bool HasBody() {
            return hasBody;
        }

        public String Name() {
            return name;
        }
    }

    /// <summary>Common methods for Requests and Responses</summary>
    /// 
    public interface IBase<T>
        where T : IBase<T> {
        /// <summary>Get the URL</summary>
        /// <returns>URL</returns>
        Uri Url();

        /// <summary>Set the URL</summary>
        /// <param name="url">new URL</param>
        /// <returns>this, for chaining</returns>
        T Url(Uri url);

        /// <summary>Get the request method</summary>
        /// <returns>method</returns>
        Org.Jsoup.Method Method();

        /// <summary>Set the request method</summary>
        /// <param name="method">new method</param>
        /// <returns>this, for chaining</returns>
        T Method(Org.Jsoup.Method method);

        /// <summary>Get the value of a header.</summary>
        /// <remarks>
        /// Get the value of a header. This is a simplified header model, where a header may only have one value.
        /// <p>
        /// Header names are case insensitive.
        /// </p>
        /// </remarks>
        /// <param name="name">name of header (case insensitive)</param>
        /// <returns>value of header, or null if not set.</returns>
        /// <seealso cref="IBase<T>{T}.HasHeader(System.String)"/>
        /// <seealso cref="IBase<T>{T}.Cookie(System.String)"/>
        String Header(String name);

        /// <summary>Set a header.</summary>
        /// <remarks>Set a header. This method will overwrite any existing header with the same case insensitive name.
        ///     </remarks>
        /// <param name="name">Name of header</param>
        /// <param name="value">Value of header</param>
        /// <returns>this, for chaining</returns>
        T Header(String name, String value);

        /// <summary>Check if a header is present</summary>
        /// <param name="name">name of header (case insensitive)</param>
        /// <returns>if the header is present in this request/response</returns>
        bool HasHeader(String name);

        /// <summary>Check if a header is present, with the given value</summary>
        /// <param name="name">header name (case insensitive)</param>
        /// <param name="value">value (case insensitive)</param>
        /// <returns>if the header and value pair are set in this req/res</returns>
        bool HasHeaderWithValue(String name, String value);

        /// <summary>Remove a header by name</summary>
        /// <param name="name">name of header to remove (case insensitive)</param>
        /// <returns>this, for chaining</returns>
        T RemoveHeader(String name);

        /// <summary>Retrieve all of the request/response headers as a map</summary>
        /// <returns>headers</returns>
        IDictionary<String, String> Headers();

        /// <summary>Get a cookie value by name from this request/response.</summary>
        /// <remarks>
        /// Get a cookie value by name from this request/response.
        /// <p>
        /// Response objects have a simplified cookie model. Each cookie set in the response is added to the response
        /// object's cookie key=value map. The cookie's path, domain, and expiry date are ignored.
        /// </p>
        /// </remarks>
        /// <param name="name">name of cookie to retrieve.</param>
        /// <returns>value of cookie, or null if not set</returns>
        String Cookie(String name);

        /// <summary>Set a cookie in this request/response.</summary>
        /// <param name="name">name of cookie</param>
        /// <param name="value">value of cookie</param>
        /// <returns>this, for chaining</returns>
        T Cookie(String name, String value);

        /// <summary>Check if a cookie is present</summary>
        /// <param name="name">name of cookie</param>
        /// <returns>if the cookie is present in this request/response</returns>
        bool HasCookie(String name);

        /// <summary>Remove a cookie by name</summary>
        /// <param name="name">name of cookie to remove</param>
        /// <returns>this, for chaining</returns>
        T RemoveCookie(String name);

        /// <summary>Retrieve all of the request/response cookies as a map</summary>
        /// <returns>cookies</returns>
        IDictionary<String, String> Cookies();
    }

    /// <summary>Represents a HTTP request.</summary>
    public interface IRequest : IBase<IRequest> {
        /// <summary>Get the proxy used for this request.</summary>
        /// <returns>the proxy; <code>null</code> if not enabled.</returns>
        WebProxy Proxy();

        /// <summary>Update the proxy for this request.</summary>
        /// <param name="proxy">the proxy ot use; <code>null</code> to disable.</param>
        /// <returns>this Request, for chaining</returns>
        IRequest Proxy(WebProxy proxy);

        /// <summary>Set the HTTP proxy to use for this request.</summary>
        /// <param name="host">the proxy hostname</param>
        /// <param name="port">the proxy port</param>
        /// <returns>this Connection, for chaining</returns>
        IRequest Proxy(String host, int port);

        /// <summary>Get the request timeout, in milliseconds.</summary>
        /// <returns>the timeout in milliseconds.</returns>
        int Timeout();

        /// <summary>Update the request timeout.</summary>
        /// <param name="millis">timeout, in milliseconds</param>
        /// <returns>this Request, for chaining</returns>
        IRequest Timeout(int millis);

        /// <summary>Get the maximum body size, in bytes.</summary>
        /// <returns>the maximum body size, in bytes.</returns>
        int MaxBodySize();

        /// <summary>Update the maximum body size, in bytes.</summary>
        /// <param name="bytes">maximum body size, in bytes.</param>
        /// <returns>this Request, for chaining</returns>
        IRequest MaxBodySize(int bytes);

        /// <summary>Get the current followRedirects configuration.</summary>
        /// <returns>true if followRedirects is enabled.</returns>
        bool FollowRedirects();

        /// <summary>Configures the request to (not) follow server redirects.</summary>
        /// <remarks>Configures the request to (not) follow server redirects. By default this is <b>true</b>.</remarks>
        /// <param name="followRedirects">true if server redirects should be followed.</param>
        /// <returns>this Request, for chaining</returns>
        IRequest FollowRedirects(bool followRedirects);

        /// <summary>Get the current ignoreHttpErrors configuration.</summary>
        /// <returns>
        /// true if errors will be ignored; false (default) if HTTP errors will cause an IOException to be
        /// thrown.
        /// </returns>
        bool IgnoreHttpErrors();

        /// <summary>Configures the request to ignore HTTP errors in the response.</summary>
        /// <param name="ignoreHttpErrors">set to true to ignore HTTP errors.</param>
        /// <returns>this Request, for chaining</returns>
        IRequest IgnoreHttpErrors(bool ignoreHttpErrors);

        /// <summary>Get the current ignoreContentType configuration.</summary>
        /// <returns>
        /// true if invalid content-types will be ignored; false (default) if they will cause an IOException to
        /// be thrown.
        /// </returns>
        bool IgnoreContentType();

        /// <summary>Configures the request to ignore the Content-Type of the response.</summary>
        /// <param name="ignoreContentType">set to true to ignore the content type.</param>
        /// <returns>this Request, for chaining</returns>
        IRequest IgnoreContentType(bool ignoreContentType);

        /// <summary>Get the current state of TLS (SSL) certificate validation.</summary>
        /// <returns>true if TLS cert validation enabled</returns>
        bool ValidateTLSCertificates();

        /// <summary>Set TLS certificate validation.</summary>
        /// <param name="value">set false to ignore TLS (SSL) certificates</param>
        void ValidateTLSCertificates(bool value);

        /// <summary>Add a data parameter to the request</summary>
        /// <param name="keyval">data to add.</param>
        /// <returns>this Request, for chaining</returns>
        IRequest Data(IKeyVal keyval);

        /// <summary>Get all of the request's data parameters</summary>
        /// <returns>collection of keyvals</returns>
        ICollection<IKeyVal> Data();

        /// <summary>Set a POST (or PUT) request body.</summary>
        /// <remarks>
        /// Set a POST (or PUT) request body. Useful when a server expects a plain request body, not a set for URL
        /// encoded form key/value pairs. E.g.:
        /// <code><pre>Jsoup.connect(url)
        /// .requestBody(json)
        /// .header("Content-Type", "application/json")
        /// .post();</pre></code>
        /// If any data key/vals are supplied, they will be sent as URL query params.
        /// </remarks>
        /// <returns>this Request, for chaining</returns>
        IRequest RequestBody(String body);

        /// <summary>Get the current request body.</summary>
        /// <returns>null if not set.</returns>
        String RequestBody();

        /// <summary>Specify the parser to use when parsing the document.</summary>
        /// <param name="parser">parser to use.</param>
        /// <returns>this Request, for chaining</returns>
        IRequest Parser(Org.Jsoup.Parser.Parser parser);

        /// <summary>Get the current parser to use when parsing the document.</summary>
        /// <returns>current Parser</returns>
        Org.Jsoup.Parser.Parser Parser();

        /// <summary>Sets the post data character set for x-www-form-urlencoded post data</summary>
        /// <param name="charset">character set to encode post data</param>
        /// <returns>this Request, for chaining</returns>
        IRequest PostDataCharset(String charset);

        /// <summary>Gets the post data character set for x-www-form-urlencoded post data</summary>
        /// <returns>character set to encode post data</returns>
        String PostDataCharset();
    }

    /// <summary>Represents a HTTP response.</summary>
    public interface IResponse : IBase<IResponse> {
        /// <summary>Get the status code of the response.</summary>
        /// <returns>status code</returns>
        int StatusCode();

        /// <summary>Get the status message of the response.</summary>
        /// <returns>status message</returns>
        String StatusMessage();

        /// <summary>Get the character set name of the response.</summary>
        /// <returns>character set name</returns>
        String Charset();

        /// <summary>Get the response content type (e.g.</summary>
        /// <remarks>Get the response content type (e.g. "text/html");</remarks>
        /// <returns>the response content type</returns>
        String ContentType();

        /// <summary>Parse the body of the response as a Document.</summary>
        /// <returns>a parsed Document</returns>
        /// <exception cref="System.IO.IOException">on error</exception>
        Document Parse();

        /// <summary>Get the body of the response as a plain string.</summary>
        /// <returns>body</returns>
        String Body();

        /// <summary>Get the body of the response as an array of bytes.</summary>
        /// <returns>body bytes</returns>
        byte[] BodyAsBytes();
    }

    /// <summary>A Key Value tuple.</summary>
    public interface IKeyVal {
        /// <summary>Update the key of a keyval</summary>
        /// <param name="key">new key</param>
        /// <returns>this KeyVal, for chaining</returns>
        IKeyVal Key(String key);

        /// <summary>Get the key of a keyval</summary>
        /// <returns>the key</returns>
        String Key();

        /// <summary>Update the value of a keyval</summary>
        /// <param name="value">the new value</param>
        /// <returns>this KeyVal, for chaining</returns>
        IKeyVal Value(String value);

        /// <summary>Get the value of a keyval</summary>
        /// <returns>the value</returns>
        String Value();

        /// <summary>Add or update an input stream to this keyVal</summary>
        /// <param name="inputStream">new input stream</param>
        /// <returns>this KeyVal, for chaining</returns>
        IKeyVal InputStream(Stream inputStream);

        /// <summary>Get the input stream associated with this keyval, if any</summary>
        /// <returns>input stream if set, or null</returns>
        Stream InputStream();

        /// <summary>Does this keyval have an input stream?</summary>
        /// <returns>true if this keyval does indeed have an input stream</returns>
        bool HasInputStream();
    }
}
