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
using iText.IO.Codec;
using iText.IO.Image;
using iText.IO.Log;
using iText.IO.Util;
using iText.Kernel.Pdf.Xobject;

namespace iText.Html2pdf.Resolver.Resource {
    public class ResourceResolver {
        private UriResolver uriResolver;

        private SimpleImageCache imageCache;

        /// <summary>
        /// Creates
        /// <see cref="ResourceResolver"/>
        /// instance. If
        /// <paramref name="baseUri"/>
        /// is a string that represents an absolute URI with any schema
        /// except "file" - resources url values will be resolved exactly as "new URL(baseUrl, uriString)". Otherwise base URI
        /// will be handled as path in local file system.
        /// <p>
        /// The main difference between those two is handling of the relative URIs of resources with slashes in the beginning
        /// of them (e.g. "/test/uri", or "//itextpdf.com/example_resources/logo.img"): if base URI is handled as local file
        /// system path, then in those cases resources URIs will be simply concatenated to the base path, rather than processed
        /// with URI resolution rules (See RFC 3986 "5.4.  Reference Resolution Examples"). However absolute resource URIs will
        /// be processed correctly.
        /// </p>
        /// <p>
        /// If empty string or relative URI string is passed as base URI, then it will be resolved against current working
        /// directory of this application instance.
        /// </p>
        /// </summary>
        /// <param name="baseUri">base URI against which all relative resource URIs will be resolved.</param>
        public ResourceResolver(String baseUri) {
            // TODO handle <base href=".."> tag?
            // TODO provide a way to configure capacity, manually reset or disable the image cache?
            this.uriResolver = new UriResolver(baseUri);
            this.imageCache = new SimpleImageCache();
        }

        /// <summary>
        /// Retrieve
        /// <see cref="iText.Kernel.Pdf.Xobject.PdfImageXObject"/>
        /// .
        /// </summary>
        /// <param name="src">either link to file or base64 encoded stream.</param>
        /// <returns>PdfImageXObject on success, otherwise null.</returns>
        public virtual PdfImageXObject RetrieveImage(String src) {
            if (src.Contains("base64")) {
                try {
                    String fixedSrc = iText.IO.Util.StringUtil.ReplaceAll(src, "\\s", "");
                    fixedSrc = fixedSrc.Substring(fixedSrc.IndexOf("base64", StringComparison.Ordinal) + 7);
                    PdfImageXObject imageXObject = imageCache.GetImage(fixedSrc);
                    if (imageXObject == null) {
                        imageXObject = new PdfImageXObject(ImageDataFactory.Create(System.Convert.FromBase64String(fixedSrc)));
                        imageCache.PutImage(fixedSrc, imageXObject);
                    }
                    return imageXObject;
                }
                catch (Exception) {
                }
            }
            try {
                Uri url = uriResolver.ResolveAgainstBaseUri(src);
                url = UrlUtil.GetFinalURL(url);
                String imageResolvedSrc = url.ToExternalForm();
                PdfImageXObject imageXObject = imageCache.GetImage(imageResolvedSrc);
                if (imageXObject == null) {
                    imageXObject = new PdfImageXObject(ImageDataFactory.Create(url));
                    imageCache.PutImage(imageResolvedSrc, imageXObject);
                }
                return imageXObject;
            }
            catch (Exception e) {
                ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Resolver.Resource.ResourceResolver));
                logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI, 
                    uriResolver.GetBaseUri(), src), e);
                return null;
            }
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual Stream RetrieveStyleSheet(String uri) {
            return iText.IO.Util.UrlUtil.OpenStream(uriResolver.ResolveAgainstBaseUri(uri));
        }

        /// <summary>Retrieve bytes.</summary>
        /// <param name="src">either link to file or base64 encoded stream.</param>
        /// <returns>byte[] on success, otherwise null.</returns>
        public virtual byte[] RetrieveStream(String src) {
            if (src.Contains("base64")) {
                try {
                    String fixedSrc = iText.IO.Util.StringUtil.ReplaceAll(src, "\\s", "");
                    fixedSrc = fixedSrc.Substring(fixedSrc.IndexOf("base64", StringComparison.Ordinal) + 7);
                    return System.Convert.FromBase64String(fixedSrc);
                }
                catch (Exception) {
                }
            }
            try {
                return StreamUtil.InputStreamToArray(iText.IO.Util.UrlUtil.OpenStream(uriResolver.ResolveAgainstBaseUri(src
                    )));
            }
            catch (Exception e) {
                ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Resolver.Resource.ResourceResolver));
                logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
                    , uriResolver.GetBaseUri(), src), e);
                return null;
            }
        }

        /// <exception cref="Java.Net.MalformedURLException"/>
        public virtual Uri ResolveAgainstBaseUri(String uri) {
            return uriResolver.ResolveAgainstBaseUri(uri);
        }

        public virtual void ResetCache() {
            imageCache.Reset();
        }
    }
}
