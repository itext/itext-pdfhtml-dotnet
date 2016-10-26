using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Org.Jsoup.Nodes;
using Org.Jsoup.Parser;
using iText.IO.Util;

namespace Org.Jsoup.Helper {
    /// <summary>
    /// Implementation of
    /// <see cref="Org.Jsoup.Connection"/>
    /// .
    /// </summary>
    /// <seealso cref="Org.Jsoup.Jsoup.Connect(System.String)"/>
    public class HttpConnection : Connection {
        public const String CONTENT_ENCODING = "Content-Encoding";

        internal const String CONTENT_TYPE = "Content-Type";

        internal const String MULTIPART_FORM_DATA = "multipart/form-data";

        internal const String FORM_URL_ENCODED = "application/x-www-form-urlencoded";

        internal const int HTTP_TEMP_REDIR = 307;

        // http/1.1 temporary redirect, not in Java's set.
        public static Connection Connect(String url) {
            Connection con = new Org.Jsoup.Helper.HttpConnection();
            con.Url(url);
            return con;
        }

        public static Connection Connect(Uri url) {
            Connection con = new Org.Jsoup.Helper.HttpConnection();
            con.Url(url);
            return con;
        }

        internal static String EncodeUrl(String url) {
            if (url == null) {
                return null;
            }
            return iText.IO.Util.StringUtil.ReplaceAll(url, " ", "%20");
        }

        internal static String EncodeMimeName(String val) {
            if (val == null) {
                return null;
            }
            return iText.IO.Util.StringUtil.ReplaceAll(val, "\"", "%22");
        }

        private IRequest req;

        private IResponse res;

        private HttpConnection() {
            req = new Org.Jsoup.Helper.Request();
            res = new Org.Jsoup.Helper.Response();
        }

        public virtual Connection Url(Uri url) {
            req.Url(url);
            return this;
        }

        public virtual Connection Url(String url) {
            Validate.NotEmpty(url, "Must supply a valid URL");
            try {
                req.Url(new Uri(Org.Jsoup.Helper.HttpConnection.EncodeUrl(url)));
            }
            catch (MalformedURLException e) {
                throw new ArgumentException("Malformed URL: " + url, e);
            }
            return this;
        }

        public virtual Connection Proxy(WebProxy proxy) {
            req.Proxy(proxy);
            return this;
        }

        public virtual Connection Proxy(String host, int port) {
            req.Proxy(host, port);
            return this;
        }

        public virtual Connection UserAgent(String userAgent) {
            Validate.NotNull(userAgent, "User agent must not be null");
            req.Header("User-Agent", userAgent);
            return this;
        }

        public virtual Connection Timeout(int millis) {
            req.Timeout(millis);
            return this;
        }

        public virtual Connection MaxBodySize(int bytes) {
            req.MaxBodySize(bytes);
            return this;
        }

        public virtual Connection FollowRedirects(bool followRedirects) {
            req.FollowRedirects(followRedirects);
            return this;
        }

        public virtual Connection Referrer(String referrer) {
            Validate.NotNull(referrer, "Referrer must not be null");
            req.Header("Referer", referrer);
            return this;
        }

        public virtual Connection Method(Org.Jsoup.Method method) {
            req.Method(method);
            return this;
        }

        public virtual Connection IgnoreHttpErrors(bool ignoreHttpErrors) {
            req.IgnoreHttpErrors(ignoreHttpErrors);
            return this;
        }

        public virtual Connection IgnoreContentType(bool ignoreContentType) {
            req.IgnoreContentType(ignoreContentType);
            return this;
        }

        public virtual Connection ValidateTLSCertificates(bool value) {
            req.ValidateTLSCertificates(value);
            return this;
        }

        public virtual Connection Data(String key, String value) {
            req.Data(KeyVal.Create(key, value));
            return this;
        }

        public virtual Connection Data(String key, String filename, Stream inputStream) {
            req.Data(KeyVal.Create(key, filename, inputStream));
            return this;
        }

        public virtual Connection Data(IDictionary<String, String> data) {
            Validate.NotNull(data, "Data map must not be null");
            foreach (KeyValuePair<String, String> entry in data) {
                req.Data(KeyVal.Create(entry.Key, entry.Value));
            }
            return this;
        }

        public virtual Connection Data(params String[] keyvals) {
            Validate.NotNull(keyvals, "Data key value pairs must not be null");
            Validate.IsTrue(keyvals.Length % 2 == 0, "Must supply an even number of key value pairs");
            for (int i = 0; i < keyvals.Length; i += 2) {
                String key = keyvals[i];
                String value = keyvals[i + 1];
                Validate.NotEmpty(key, "Data key must not be empty");
                Validate.NotNull(value, "Data value must not be null");
                req.Data(KeyVal.Create(key, value));
            }
            return this;
        }

        public virtual Connection Data(ICollection<IKeyVal> data) {
            Validate.NotNull(data, "Data collection must not be null");
            foreach (IKeyVal entry in data) {
                req.Data(entry);
            }
            return this;
        }

        public virtual IKeyVal Data(String key) {
            Validate.NotEmpty(key, "Data key must not be empty");
            foreach (IKeyVal keyVal in Request().Data()) {
                if (keyVal.Key().Equals(key)) {
                    return keyVal;
                }
            }
            return null;
        }

        public virtual Connection RequestBody(String body) {
            req.RequestBody(body);
            return this;
        }

        public virtual Connection Header(String name, String value) {
            req.Header(name, value);
            return this;
        }

        public virtual Connection Cookie(String name, String value) {
            req.Cookie(name, value);
            return this;
        }

        public virtual Connection Cookies(IDictionary<String, String> cookies) {
            Validate.NotNull(cookies, "Cookie map must not be null");
            foreach (KeyValuePair<String, String> entry in cookies) {
                req.Cookie(entry.Key, entry.Value);
            }
            return this;
        }

        public virtual Connection Parser(Org.Jsoup.Parser.Parser parser) {
            req.Parser(parser);
            return this;
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual Document Get() {
            req.Method(Org.Jsoup.Method.GET);
            Execute();
            return res.Parse();
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual Document Post() {
            req.Method(Org.Jsoup.Method.POST);
            Execute();
            return res.Parse();
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual IResponse Execute() {
            res = Org.Jsoup.Helper.Response.Execute(req);
            return res;
        }

        public virtual IRequest Request() {
            return req;
        }

        public virtual Connection Request(IRequest request) {
            req = request;
            return this;
        }

        public virtual IResponse Response() {
            return res;
        }

        public virtual Connection Response(IResponse response) {
            res = response;
            return this;
        }

        public virtual Connection PostDataCharset(String charset) {
            req.PostDataCharset(charset);
            return this;
        }

        // ensures we don't get an "accept-encoding" and a "Accept-Encoding"
        // remove is case insensitive too
        // ensures correct case
        // quick evals for common case of title case, lower case, then scan for mixed
        // nullable
        // called parser(...) vs initialized in ctor
        // 1MB
        /*
        * Matches XML content types (like text/xml, application/xhtml+xml;charset=UTF8, etc)
        */
        // set up the request for execution
        // redirect if there's a location header (from 3xx, or 201 etc)
        // always redirect with a get. any data param from original req are dropped.
        // fix broken Location: http:/temp/AAG_New/en/index.php
        // add response cookies to request (for e.g. login posts)
        // check that we can handle the returned content type; if not, abort before fetching it
        // switch to the XML parser if content type is xml and not parser not explicitly set
        // only flip it if a HttpConnection.Request (i.e. don't presume other impls want it):
        // may be null, readInputStream deals with it
        // -1 means unknown, chunked. sun throws an IO exception on 500 response with no content when trying to read body
        // per Java's documentation, this is not necessary, and precludes keepalives. However in practise,
        // connection errors will not be released quickly enough and can cause a too many open files error.
        // update charset from meta-equiv, possibly
        // charset gets set from header on execute, and from meta-equiv on parse. parse may not have happened yet
        // set up connection defaults, and details from request
        // don't rely on native redirection support
        // Create a trust manager that does not validate certificate chains
        // Install the all-trusting trust manager
        // Create an ssl socket factory with our all-trusting manager
        // set up url, method, header, cookies
        // if from a redirect, map previous response cookies into this response
        // the default sun impl of conn.getHeaderFields() returns header values out of order
        // skip http1.1 line
        // http/1.1 line
        // ignores path, date, domain, validateTLSCertificates et al. req'd?
        // name not blank, value not null
        // combine same header names with comma: http://www.w3.org/Protocols/rfc2616/rfc2616-sec4.html#sec4.2
        // boundary will be set if we're in multipart mode
        // encodes " to %22
        // flush
        // data will be in query string, we're sending a plaintext body
        // regular form data (application/x-www-form-urlencoded)
        // todo: spec says only ascii, no escaping / encoding defined. validate on set? or escape somehow here?
        // for get url reqs, serialise the data map into the url
        // reconstitute the query, ready for appends
        // includes host, port
        // moved into url as get params
        internal static bool NeedsMultipart(IRequest req) {
            // multipart mode, for files. add the header if we see something with an inputstream, and return a non-null boundary
            bool needsMulti = false;
            foreach (IKeyVal keyVal in req.Data()) {
                if (keyVal.HasInputStream()) {
                    needsMulti = true;
                    break;
                }
            }
            return needsMulti;
        }
    }

    public abstract class Base<T> : IBase<T>
        where T : IBase<T> {
        internal Uri url;

        internal Org.Jsoup.Method method;

        internal IDictionary<String, String> headers;

        internal IDictionary<String, String> cookies;

        internal Base() {
            headers = new LinkedDictionary<String, String>();
            cookies = new LinkedDictionary<String, String>();
        }

        public virtual Uri Url() {
            return url;
        }

        public virtual T Url(Uri url) {
            Validate.NotNull(url, "URL must not be null");
            this.url = url;
            return (T)this;
        }

        public virtual Org.Jsoup.Method Method() {
            return method;
        }

        public virtual T Method(Org.Jsoup.Method method) {
            Validate.NotNull(method, "Method must not be null");
            this.method = method;
            return (T)this;
        }

        public virtual String Header(String name) {
            Validate.NotNull(name, "Header name must not be null");
            return GetHeaderCaseInsensitive(name);
        }

        public virtual T Header(String name, String value) {
            Validate.NotEmpty(name, "Header name must not be empty");
            Validate.NotNull(value, "Header value must not be null");
            RemoveHeader(name);
            headers[name] = value;
            return (T)this;
        }

        public virtual bool HasHeader(String name) {
            Validate.NotEmpty(name, "Header name must not be empty");
            return GetHeaderCaseInsensitive(name) != null;
        }

        /// <summary>Test if the request has a header with this value (case insensitive).</summary>
        public virtual bool HasHeaderWithValue(String name, String value) {
            return HasHeader(name) && Header(name).EqualsIgnoreCase(value);
        }

        public virtual T RemoveHeader(String name) {
            Validate.NotEmpty(name, "Header name must not be empty");
            KeyValuePair<String, String> entry;
            if (TryScanHeaders(name, out entry)) {
                headers.Remove(entry.Key);
            }
            return (T)this;
        }

        public virtual IDictionary<String, String> Headers() {
            return headers;
        }

        private String GetHeaderCaseInsensitive(String name) {
            Validate.NotNull(name, "Header name must not be null");
            String value = headers.Get(name);
            if (value == null) {
                value = headers.Get(name.ToLower(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (value == null) {
                KeyValuePair<String, String> entry;
                if (TryScanHeaders(name, out entry)) {
                    value = entry.Value;
                }
            }
            return value;
        }

        private bool TryScanHeaders(String name, out KeyValuePair<String, String> result) {
            String lc = name.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            foreach (KeyValuePair<String, String> entry in headers) {
                if (entry.Key.ToLower(System.Globalization.CultureInfo.InvariantCulture).Equals(lc)) {
                    result = entry;
                    return true;
                }
            }
            result = new KeyValuePair<String, String>("", "");
            return false;
        }

        public virtual String Cookie(String name) {
            Validate.NotEmpty(name, "Cookie name must not be empty");
            return cookies.Get(name);
        }

        public virtual T Cookie(String name, String value) {
            Validate.NotEmpty(name, "Cookie name must not be empty");
            Validate.NotNull(value, "Cookie value must not be null");
            cookies[name] = value;
            return (T)this;
        }

        public virtual bool HasCookie(String name) {
            Validate.NotEmpty(name, "Cookie name must not be empty");
            return cookies.ContainsKey(name);
        }

        public virtual T RemoveCookie(String name) {
            Validate.NotEmpty(name, "Cookie name must not be empty");
            cookies.JRemove(name);
            return (T)this;
        }

        public virtual IDictionary<String, String> Cookies() {
            return cookies;
        }
    }

    public class Request : Base<IRequest>, IRequest {
        private WebProxy proxy;

        private int timeoutMilliseconds;

        private int maxBodySizeBytes;

        private bool followRedirects;

        private ICollection<IKeyVal> data;

        private String body = null;

        private bool ignoreHttpErrors = false;

        private bool ignoreContentType = false;

        private Org.Jsoup.Parser.Parser parser;

        internal bool parserDefined = false;

        private bool validateTSLCertificates = true;

        private String postDataCharset = DataUtil.defaultCharset;

        internal Request() {
            timeoutMilliseconds = 3000;
            maxBodySizeBytes = 1024 * 1024;
            followRedirects = true;
            data = new List<IKeyVal>();
            method = Org.Jsoup.Method.GET;
            headers["Accept-Encoding"] = "gzip";
            parser = Org.Jsoup.Parser.Parser.HtmlParser();
        }

        public virtual WebProxy Proxy() {
            return proxy;
        }

        public virtual IRequest Proxy(WebProxy proxy) {
            this.proxy = proxy;
            return this;
        }

        public virtual IRequest Proxy(String host, int port) {
            this.proxy = new WebProxy(host, port);
            return this;
        }

        public virtual int Timeout() {
            return timeoutMilliseconds;
        }

        public virtual IRequest Timeout(int millis) {
            Validate.IsTrue(millis >= 0, "Timeout milliseconds must be 0 (infinite) or greater");
            timeoutMilliseconds = millis;
            return this;
        }

        public virtual int MaxBodySize() {
            return maxBodySizeBytes;
        }

        public virtual IRequest MaxBodySize(int bytes) {
            Validate.IsTrue(bytes >= 0, "maxSize must be 0 (unlimited) or larger");
            maxBodySizeBytes = bytes;
            return this;
        }

        public virtual bool FollowRedirects() {
            return followRedirects;
        }

        public virtual IRequest FollowRedirects(bool followRedirects) {
            this.followRedirects = followRedirects;
            return this;
        }

        public virtual bool IgnoreHttpErrors() {
            return ignoreHttpErrors;
        }

        public virtual bool ValidateTLSCertificates() {
            return validateTSLCertificates;
        }

        public virtual void ValidateTLSCertificates(bool value) {
            validateTSLCertificates = value;
        }

        public virtual IRequest IgnoreHttpErrors(bool ignoreHttpErrors) {
            this.ignoreHttpErrors = ignoreHttpErrors;
            return this;
        }

        public virtual bool IgnoreContentType() {
            return ignoreContentType;
        }

        public virtual IRequest IgnoreContentType(bool ignoreContentType) {
            this.ignoreContentType = ignoreContentType;
            return this;
        }

        public virtual IRequest Data(IKeyVal keyval) {
            Validate.NotNull(keyval, "Key val must not be null");
            data.Add(keyval);
            return this;
        }

        public virtual ICollection<IKeyVal> Data() {
            return data;
        }

        public virtual IRequest RequestBody(String body) {
            this.body = body;
            return this;
        }

        public virtual String RequestBody() {
            return body;
        }

        public virtual IRequest Parser(Org.Jsoup.Parser.Parser parser) {
            this.parser = parser;
            parserDefined = true;
            return this;
        }

        public virtual Org.Jsoup.Parser.Parser Parser() {
            return parser;
        }

        public virtual IRequest PostDataCharset(String charset) {
            Validate.NotNull(charset, "Charset must not be null");
            if (!PortUtil.CharsetIsSupported(charset)) {
                throw new ArgumentException(charset);
            }
            this.postDataCharset = charset;
            return this;
        }

        public virtual String PostDataCharset() {
            return postDataCharset;
        }
    }

    public class Response : Base<IResponse>, IResponse {
        private const int MAX_REDIRECTS = 20;

        private const String LOCATION = "Location";

        private int statusCode;

        private String statusMessage;

        private ByteBuffer byteData;

        private String charset;

        private String contentType;

        private bool executed = false;

        private int numRedirects = 0;

        private IRequest req;

        private static readonly Regex xmlContentTypeRxp = iText.IO.Util.StringUtil.RegexCompile("(application|text)/\\w*\\+?xml.*"
            );

        internal Response()
            : base() {
        }

        /// <exception cref="System.IO.IOException"/>
        private Response(Org.Jsoup.Helper.Response previousResponse)
            : base() {
            if (previousResponse != null) {
                numRedirects = previousResponse.numRedirects + 1;
                if (numRedirects >= MAX_REDIRECTS) {
                    throw new System.IO.IOException(String.Format("Too many redirects occurred trying to load URL {0}", previousResponse
                        .Url()));
                }
            }
        }

        /// <exception cref="System.IO.IOException"/>
        internal static Org.Jsoup.Helper.Response Execute(IRequest req) {
            return Execute(req, null);
        }

        /// <exception cref="System.IO.IOException"/>
        internal static Org.Jsoup.Helper.Response Execute(IRequest req, Org.Jsoup.Helper.Response previousResponse
            ) {
            Validate.NotNull(req, "Request must not be null");
            String protocol = req.Url().Scheme;
            if (!protocol.Equals("http") && !protocol.Equals("https")) {
                throw new MalformedURLException("Only http & https protocols supported");
            }
            bool methodHasBody = req.Method().HasBody();
            bool hasRequestBody = req.RequestBody() != null;
            if (!methodHasBody) {
                Validate.IsFalse(hasRequestBody, "Cannot set a request body for HTTP method " + req.Method());
            }
            String mimeBoundary = null;
            if (req.Data().Count > 0 && (!methodHasBody || hasRequestBody)) {
                SerialiseRequestUrl(req);
            }
            else {
                if (methodHasBody) {
                    mimeBoundary = SetOutputContentType(req);
                }
            }
            HttpWebRequest conn = CreateConnection(req);
            Org.Jsoup.Helper.Response res;
            try {
                conn.Connect();
                if (conn.GetDoOutput()) {
                    WritePost(req, conn.GetOutputStream(), mimeBoundary);
                }
                int status = conn.GetResponseCode();
                res = new Org.Jsoup.Helper.Response(previousResponse);
                res.SetupFromConnection(conn, previousResponse);
                res.req = req;
                if (res.HasHeader(LOCATION) && req.FollowRedirects()) {
                    if (status != HttpConnection.HTTP_TEMP_REDIR) {
                        req.Method(Org.Jsoup.Method.GET);
                        req.Data().Clear();
                    }
                    String location = res.Header(LOCATION);
                    if (location != null && location.StartsWith("http:/") && location[6] != '/') {
                        location = location.Substring(6);
                    }
                    req.Url(Org.Jsoup.Helper.StringUtil.Resolve(req.Url(), Org.Jsoup.Helper.HttpConnection.EncodeUrl(location)
                        ));
                    foreach (KeyValuePair<String, String> cookie in res.cookies) {
                        req.Cookie(cookie.Key, cookie.Value);
                    }
                    return Execute(req, res);
                }
                if ((status < 200 || status >= 400) && !req.IgnoreHttpErrors()) {
                    throw new HttpStatusException("HTTP error fetching URL", status, req.Url().ToString());
                }
                String contentType = res.ContentType();
                if (contentType != null && !req.IgnoreContentType() && !contentType.StartsWith("text/") && !iText.IO.Util.StringUtil.Match
                    (xmlContentTypeRxp, contentType).Success) {
                    throw new UnsupportedMimeTypeException("Unhandled content type. Must be text/*, application/xml, or application/xhtml+xml"
                        , contentType, req.Url().ToString());
                }
                if (contentType != null && iText.IO.Util.StringUtil.Match(xmlContentTypeRxp, contentType).Success) {
                    if (req is Request && !((Request)req).parserDefined) {
                        req.Parser(Org.Jsoup.Parser.Parser.XmlParser());
                    }
                }
                res.charset = DataUtil.GetCharsetFromContentType(res.contentType);
                if (conn.GetContentLength() != 0 && req.Method() != Org.Jsoup.Method.HEAD) {
                    Stream bodyStream = null;
                    try {
                        bodyStream = conn.GetErrorStream() != null ? conn.GetErrorStream() : conn.GetInputStream();
                        if (res.HasHeaderWithValue(HttpConnection.CONTENT_ENCODING, "gzip")) {
                            bodyStream = new GZIPInputStream(bodyStream);
                        }
                        res.byteData = DataUtil.ReadToByteBuffer(bodyStream, req.MaxBodySize());
                    }
                    finally {
                        if (bodyStream != null) {
                            bodyStream.Close();
                        }
                    }
                }
                else {
                    res.byteData = Org.Jsoup.Helper.ByteBuffer.EmptyByteBuffer();
                }
            }
            finally {
                conn.Disconnect();
            }
            res.executed = true;
            return res;
        }

        public virtual int StatusCode() {
            return statusCode;
        }

        public virtual String StatusMessage() {
            return statusMessage;
        }

        public virtual String Charset() {
            return charset;
        }

        public virtual String ContentType() {
            return contentType;
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual Document Parse() {
            Validate.IsTrue(executed, "Request must be executed (with .execute(), .get(), or .post() before parsing response"
                );
            Document doc = DataUtil.ParseByteData(byteData, charset, url.ToExternalForm(), req.Parser());
            byteData.Rewind();
            charset = doc.OutputSettings().Charset().BodyName;
            return doc;
        }

        public virtual String Body() {
            Validate.IsTrue(executed, "Request must be executed (with .execute(), .get(), or .post() before getting response body"
                );
            String body;
            if (charset == null) {
                body = System.Text.Encoding.GetEncoding(DataUtil.defaultCharset).Decode(byteData).ToString();
            }
            else {
                body = System.Text.Encoding.GetEncoding(charset).Decode(byteData).ToString();
            }
            byteData.Rewind();
            return body;
        }

        public virtual byte[] BodyAsBytes() {
            Validate.IsTrue(executed, "Request must be executed (with .execute(), .get(), or .post() before getting response body"
                );
            return ((byte[])byteData.Array());
        }

        /// <exception cref="System.IO.IOException"/>
        private static HttpWebRequest CreateConnection(IRequest req) {
            HttpWebRequest conn = (HttpWebRequest) WebRequest.Create(req.Url());
            conn.Proxy = req.Proxy();
            conn.Method = req.Method().Name();
            conn.AllowAutoRedirect = false;
            conn.Timeout = req.Timeout();
            conn.ReadWriteTimeout = req.Timeout();

            //if (!req.ValidateTLSCertificates()) {
            //    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
            //}

            //if (req.Method().HasBody()) {
            //    conn.SetDoOutput(true);
            //}

            if (req.Cookies().Count > 0) {
                conn.Headers.Add(HttpRequestHeader.Cookie, GetRequestCookieString(req));
            }
            AddHeaders(conn, req.Headers());
            return conn;
        }

        private static void AddHeaders(HttpWebRequest request, IDictionary<String, String> headers) {
            var tmp = new Dictionary<String, String>(headers);

            if (tmp.ContainsKey("Accept")) {
                request.Accept = tmp.JRemove("Accept");
            }
            if (tmp.ContainsKey("Connection")) {
                request.Connection = tmp.JRemove("Connection");
            }
            if (tmp.ContainsKey("Referer")) {
                request.Accept = tmp.JRemove("Referer");
            }
            if (tmp.ContainsKey("User-agent")) {
                request.UserAgent = tmp.JRemove("User-agent");
            }

            foreach (var entry in tmp) {
                request.Headers[entry.Key] = entry.Value;
            }
        }

        /// <exception cref="System.IO.IOException"/>
        private void SetupFromConnection(HttpWebRequest conn, IResponse previousResponse) {
            method = Org.Jsoup.Method.ValueOf(conn.GetRequestMethod());
            url = conn.GetURL();
            statusCode = conn.GetResponseCode();
            statusMessage = conn.GetResponseMessage();
            contentType = conn.GetContentType();
            IDictionary<String, IList<String>> resHeaders = CreateHeaderMap(conn);
            ProcessResponseHeaders(resHeaders);
            if (previousResponse != null) {
                foreach (KeyValuePair<String, String> prevCookie in previousResponse.Cookies()) {
                    if (!HasCookie(prevCookie.Key)) {
                        Cookie(prevCookie.Key, prevCookie.Value);
                    }
                }
            }
        }

        private static LinkedDictionary<String, IList<String>> CreateHeaderMap(HttpWebRequest conn) {
            LinkedDictionary<String, IList<String>> headers = new LinkedDictionary<String, IList<String>>();
            int i = 0;
            while (true) {
                String key = conn.GetHeaderFieldKey(i);
                String val = conn.GetHeaderField(i);
                if (key == null && val == null) {
                    break;
                }
                i++;
                if (key == null || val == null) {
                    continue;
                }
                if (headers.Contains(key)) {
                    headers.Get(key).Add(val);
                }
                else {
                    List<String> vals = new List<String>();
                    vals.Add(val);
                    headers[key] = vals;
                }
            }
            return headers;
        }

        internal virtual void ProcessResponseHeaders(IDictionary<String, IList<String>> resHeaders) {
            foreach (KeyValuePair<String, IList<String>> entry in resHeaders) {
                String name = entry.Key;
                if (name == null) {
                    continue;
                }
                IList<String> values = entry.Value;
                if (name.EqualsIgnoreCase("Set-Cookie")) {
                    foreach (String value in values) {
                        if (value == null) {
                            continue;
                        }
                        TokenQueue cd = new TokenQueue(value);
                        String cookieName = cd.ChompTo("=").Trim();
                        String cookieVal = cd.ConsumeTo(";").Trim();
                        if (cookieName.Length > 0) {
                            Cookie(cookieName, cookieVal);
                        }
                    }
                }
                else {
                    if (values.Count == 1) {
                        Header(name, values[0]);
                    }
                    else {
                        if (values.Count > 1) {
                            StringBuilder accum = new StringBuilder();
                            for (int i = 0; i < values.Count; i++) {
                                String val = values[i];
                                if (i != 0) {
                                    accum.Append(", ");
                                }
                                accum.Append(val);
                            }
                            Header(name, accum.ToString());
                        }
                    }
                }
            }
        }

        private static String SetOutputContentType(IRequest req) {
            String bound = null;
            if (Org.Jsoup.Helper.HttpConnection.NeedsMultipart(req)) {
                bound = DataUtil.MimeBoundary();
                req.Header(HttpConnection.CONTENT_TYPE, HttpConnection.MULTIPART_FORM_DATA + "; boundary=" + bound);
            }
            else {
                req.Header(HttpConnection.CONTENT_TYPE, HttpConnection.FORM_URL_ENCODED + "; charset=" + req.PostDataCharset
                    ());
            }
            return bound;
        }

        /// <exception cref="System.IO.IOException"/>
        private static void WritePost(IRequest req, Stream outputStream, String bound) {
            ICollection<IKeyVal> data = req.Data();
            BufferedWriter w = new BufferedWriter(new StreamWriter(outputStream, req.PostDataCharset()));
            if (bound != null) {
                foreach (IKeyVal keyVal in data) {
                    w.Write("--");
                    w.Write(bound);
                    w.Write("\r\n");
                    w.Write("Content-Disposition: form-data; name=\"");
                    w.Write(Org.Jsoup.Helper.HttpConnection.EncodeMimeName(keyVal.Key()));
                    w.Write("\"");
                    if (keyVal.HasInputStream()) {
                        w.Write("; filename=\"");
                        w.Write(Org.Jsoup.Helper.HttpConnection.EncodeMimeName(keyVal.Value()));
                        w.Write("\"\r\nContent-Type: application/octet-stream\r\n\r\n");
                        w.Flush();
                        DataUtil.CrossStreams(keyVal.InputStream(), outputStream);
                        outputStream.Flush();
                    }
                    else {
                        w.Write("\r\n\r\n");
                        w.Write(keyVal.Value());
                    }
                    w.Write("\r\n");
                }
                w.Write("--");
                w.Write(bound);
                w.Write("--");
            }
            else {
                if (req.RequestBody() != null) {
                    w.Write(req.RequestBody());
                }
                else {
                    bool first = true;
                    foreach (IKeyVal keyVal in data) {
                        if (!first) {
                            w.Append('&');
                        }
                        else {
                            first = false;
                        }
                        w.Write(Org.Jsoup.PortUtil.UrlEncode(keyVal.Key(), req.PostDataCharset()));
                        w.Write('=');
                        w.Write(Org.Jsoup.PortUtil.UrlEncode(keyVal.Value(), req.PostDataCharset()));
                    }
                }
            }
            w.Close();
        }

        private static String GetRequestCookieString(IRequest req) {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<String, String> cookie in req.Cookies()) {
                if (!first) {
                    sb.Append("; ");
                }
                else {
                    first = false;
                }
                sb.Append(cookie.Key).Append('=').Append(cookie.Value);
            }
            return sb.ToString();
        }

        /// <exception cref="System.IO.IOException"/>
        private static void SerialiseRequestUrl(IRequest req) {
            Uri @in = req.Url();
            StringBuilder url = new StringBuilder();
            bool first = true;
            url.Append(@in.Scheme).Append("://").Append(@in.Authority).Append(@in.AbsolutePath).Append("?");
            if (@in.Query != null) {
                url.Append(@in.Query);
                first = false;
            }
            foreach (IKeyVal keyVal in req.Data()) {
                Validate.IsFalse(keyVal.HasInputStream(), "InputStream data not supported in URL query string.");
                if (!first) {
                    url.Append('&');
                }
                else {
                    first = false;
                }
                url.Append(Org.Jsoup.PortUtil.UrlEncode(keyVal.Key(), DataUtil.defaultCharset)).Append('=').Append(Org.Jsoup.PortUtil.UrlEncode
                    (keyVal.Value(), DataUtil.defaultCharset));
            }
            req.Url(new Uri(url.ToString()));
            req.Data().Clear();
        }
    }

    public class KeyVal : IKeyVal {
        private String key;

        private String value;

        private Stream stream;

        public static Org.Jsoup.Helper.KeyVal Create(String key, String value) {
            return (Org.Jsoup.Helper.KeyVal)new Org.Jsoup.Helper.KeyVal().Key(key).Value(value);
        }

        public static Org.Jsoup.Helper.KeyVal Create(String key, String filename, Stream stream) {
            return (Org.Jsoup.Helper.KeyVal)new Org.Jsoup.Helper.KeyVal().Key(key).Value(filename).InputStream(stream);
        }

        private KeyVal() {
        }

        public virtual IKeyVal Key(String key) {
            Validate.NotEmpty(key, "Data key must not be empty");
            this.key = key;
            return this;
        }

        public virtual String Key() {
            return key;
        }

        public virtual IKeyVal Value(String value) {
            Validate.NotNull(value, "Data value must not be null");
            this.value = value;
            return this;
        }

        public virtual String Value() {
            return value;
        }

        public virtual IKeyVal InputStream(Stream inputStream) {
            Validate.NotNull(value, "Data input stream must not be null");
            this.stream = inputStream;
            return this;
        }

        public virtual Stream InputStream() {
            return stream;
        }

        public virtual bool HasInputStream() {
            return stream != null;
        }

        public override String ToString() {
            return key + "=" + value;
        }
    }
}
