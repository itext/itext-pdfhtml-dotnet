/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS

    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://itextpdf.com/terms-of-use/

    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.

    In accordance with Section 7(b) of the GNU Affero General Public License,
    a covered work must retain the producer line in every PDF that is created
    or manipulated using iText.

    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the iText software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving PDFs on the fly in a web application, shipping iText with a closed
    source product.

    For more information, please contact iText Software Corp. at this
    address: sales@itextpdf.com */
using System;
using System.IO;

namespace iText.Html2pdf.Resolver.Resource {
    public class UriResolver {
        private Uri baseUrl;

        private bool isLocal;

        public UriResolver(String baseUri) {
            ResolveBaseUrlOrPath(baseUri);
        }

        public virtual String GetBaseUri() {
            return baseUrl.ToExternalForm();
        }

        /// <exception cref="Java.Net.MalformedURLException"/>
        public virtual Uri ResolveAgainstBaseUri(String uriString) {
            Uri resolvedUrl = null;
            if (isLocal) {
                // remove leading slashes in order to always concatenate such resource URIs: we don't want to scatter all
                // resources around the file system even if on web page the path started with '\'
                uriString = uriString.ReplaceFirst("/*\\\\*", "");
                if (!uriString.StartsWith("file:")) {
                    try {
                        String path = System.IO.Path.Combine(uriString);
                        // In general this check is for windows only, in order to handle paths like "c:/temp/img.jpg".
                        // What concerns unix paths, we already removed leading slashes,
                        // therefore we can't meet here an absolute path.
                        if (Path.IsPathRooted(path)) {
                            resolvedUrl = new Uri(path);
                        }
                    }
                    catch (Exception) {
                    }
                }
            }
            if (resolvedUrl == null) {
                resolvedUrl = new Uri(baseUrl, uriString);
            }
            return resolvedUrl;
        }

        private void ResolveBaseUrlOrPath(String @base) {
            baseUrl = BaseUriAsUrl(@base);
            if (baseUrl == null) {
                baseUrl = UriAsFileUrl(@base);
            }
            if (baseUrl == null) {
                // TODO Html2PdfException?
                throw new ArgumentException(String.Format("Invalid base URI: {0}", @base));
            }
        }

        private Uri BaseUriAsUrl(String baseUriString) {
            Uri baseAsUrl = null;
            try {
                Uri baseUri = new Uri(baseUriString.Replace(" ", "%20"));
                if (Path.IsPathRooted(baseUri.AbsolutePath)) {
                    baseAsUrl = baseUri;
                    if ("file".Equals(baseUri.Scheme)) {
                        baseAsUrl = new Uri(NormalizeFilePath(baseAsUrl.AbsolutePath));
                        isLocal = true;
                    }
                }
            }
            catch (Exception) {
            }
            return baseAsUrl;
        }

        private Uri UriAsFileUrl(String baseUriString) {
            if (baseUriString.Length == 0) {
                isLocal = true;
                return new Uri(Directory.GetCurrentDirectory() + "/");
            }
            Uri baseAsFileUrl = null;
            try {
                baseAsFileUrl = new Uri(NormalizeFilePath(Path.GetFullPath(baseUriString)));
                isLocal = true;
            }
            catch (Exception) {
            }
            return baseAsFileUrl;
        }

        private static string NormalizeFilePath(String baseUriString) {
            string path;
            if (Directory.Exists(baseUriString) && !baseUriString.EndsWith("/") && !baseUriString.EndsWith("\\")) {
                path = baseUriString + "/";
            } else {
                path = baseUriString;
            }
            return path;
        }
    }
}
