/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
address: sales@itextpdf.com
*/
using System;
using System.IO;
using System.Text.RegularExpressions;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Util;
using iText.IO.Codec;
using iText.IO.Util;
using iText.Kernel.Pdf.Xobject;
using iText.StyledXmlParser.Resolver.Resource;
using iText.Svg.Converter;
using iText.Svg.Processors;
using iText.Svg.Processors.Impl;

namespace iText.Html2pdf.Resolver.Resource {
    /// <summary>
    /// Extends
    /// <see cref="iText.StyledXmlParser.Resolver.Resource.ResourceResolver"/>
    /// to also support SVG images
    /// </summary>
    public class HtmlResourceResolver : ResourceResolver {
        private const String SVG_PREFIX = "data:image/svg+xml";

        private static readonly Regex SVG_IDENTIFIER_PATTERN = iText.IO.Util.StringUtil.RegexCompile(",[\\s]*(<svg )"
            );

        private ProcessorContext context;

        /// <summary>
        /// Creates a new
        /// <see cref="HtmlResourceResolver"/>
        /// instance.
        /// </summary>
        /// <remarks>
        /// Creates a new
        /// <see cref="HtmlResourceResolver"/>
        /// instance.
        /// If
        /// <paramref name="baseUri"/>
        /// is a string that represents an absolute URI with any schema
        /// except "file" - resources url values will be resolved exactly as "new URL(baseUrl, uriString)". Otherwise base URI
        /// will be handled as path in local file system.
        /// <para />
        /// If empty string or relative URI string is passed as base URI, then it will be resolved against current working
        /// directory of this application instance.
        /// </remarks>
        /// <param name="baseUri">base URI against which all relative resource URIs will be resolved</param>
        /// <param name="context">
        /// 
        /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>
        /// instance for the current HTML to PDF conversion process
        /// </param>
        public HtmlResourceResolver(String baseUri, ProcessorContext context)
            : this(baseUri, context, null) {
        }

        /// <summary>
        /// Creates a new
        /// <see cref="HtmlResourceResolver"/>
        /// instance.
        /// </summary>
        /// <remarks>
        /// Creates a new
        /// <see cref="HtmlResourceResolver"/>
        /// instance.
        /// If
        /// <paramref name="baseUri"/>
        /// is a string that represents an absolute URI with any schema
        /// except "file" - resources url values will be resolved exactly as "new URL(baseUrl, uriString)". Otherwise base URI
        /// will be handled as path in local file system.
        /// <para />
        /// If empty string or relative URI string is passed as base URI, then it will be resolved against current working
        /// directory of this application instance.
        /// </remarks>
        /// <param name="baseUri">base URI against which all relative resource URIs will be resolved</param>
        /// <param name="context">
        /// 
        /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>
        /// instance for the current HTML to PDF conversion process
        /// </param>
        /// <param name="retriever">the resource retriever with the help of which data from resources will be retrieved
        ///     </param>
        public HtmlResourceResolver(String baseUri, ProcessorContext context, IResourceRetriever retriever)
            : base(baseUri, retriever) {
            this.context = context;
        }

        public override PdfXObject RetrieveImage(String src) {
            if (src != null && src.Trim().StartsWith(SVG_PREFIX) && iText.Events.Util.Matcher.Match(SVG_IDENTIFIER_PATTERN
                , src).Find()) {
                PdfXObject imageXObject = TryResolveSvgImageSource(src);
                if (imageXObject != null) {
                    return imageXObject;
                }
            }
            return base.RetrieveImage(src);
        }

        /// <summary>
        /// Retrieve image as either
        /// <see cref="iText.Kernel.Pdf.Xobject.PdfImageXObject"/>
        /// , or
        /// <see cref="iText.Kernel.Pdf.Xobject.PdfFormXObject"/>.
        /// </summary>
        /// <param name="src">either link to file or base64 encoded stream</param>
        /// <returns>PdfXObject on success, otherwise null</returns>
        protected override PdfXObject TryResolveBase64ImageSource(String src) {
            String fixedSrc = iText.IO.Util.StringUtil.ReplaceAll(src, "\\s", "");
            if (fixedSrc.StartsWith(SVG_PREFIX)) {
                fixedSrc = fixedSrc.Substring(fixedSrc.IndexOf(BASE64_IDENTIFIER, StringComparison.Ordinal) + BASE64_IDENTIFIER
                    .Length + 1);
                try {
                    using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(fixedSrc))) {
                        PdfFormXObject xObject = ProcessAsSvg(stream, context, null);
                        if (xObject != null) {
                            return xObject;
                        }
                    }
                }
                catch (Exception) {
                }
            }
            return base.TryResolveBase64ImageSource(src);
        }

        protected override PdfXObject CreateImageByUrl(Uri url) {
            try {
                return base.CreateImageByUrl(url);
            }
            catch (Exception) {
                using (Stream @is = GetRetriever().GetInputStreamByUrl(url)) {
                    return @is == null ? null : iText.Html2pdf.Resolver.Resource.HtmlResourceResolver.ProcessAsSvg(@is, context
                        , FileUtil.ParentDirectory(url));
                }
            }
        }

        private PdfXObject TryResolveSvgImageSource(String src) {
            try {
                using (MemoryStream stream = new MemoryStream(src.GetBytes(System.Text.Encoding.UTF8))) {
                    PdfFormXObject xObject = ProcessAsSvg(stream, context, null);
                    if (xObject != null) {
                        return xObject;
                    }
                }
            }
            catch (Exception) {
            }
            //Logs an error in a higher-level method if null is returned
            return null;
        }

        private static PdfFormXObject ProcessAsSvg(Stream stream, ProcessorContext context, String parentDir) {
            SvgConverterProperties svgConverterProperties = ContextMappingHelper.MapToSvgConverterProperties(context);
            if (parentDir != null) {
                svgConverterProperties.SetBaseUri(parentDir);
            }
            ISvgProcessorResult res = SvgConverter.ParseAndProcess(stream, svgConverterProperties);
            if (context.GetPdfDocument() != null) {
                SvgProcessingUtil processingUtil = new SvgProcessingUtil(context.GetResourceResolver());
                return processingUtil.CreateXObjectFromProcessingResult(res, context.GetPdfDocument());
            }
            else {
                return null;
            }
        }
    }
}
