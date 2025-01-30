/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.IO;
using System.Text.RegularExpressions;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Util;
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

        private static readonly Regex SVG_IDENTIFIER_PATTERN = iText.Commons.Utils.StringUtil.RegexCompile(",[\\s]*(<svg )"
            );

        private readonly ProcessorContext context;

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
            if (src != null && src.Trim().StartsWith(SVG_PREFIX) && iText.Commons.Utils.Matcher.Match(SVG_IDENTIFIER_PATTERN
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
            String fixedSrc = iText.Commons.Utils.StringUtil.ReplaceAll(src, "\\s", "");
            if (fixedSrc.StartsWith(SVG_PREFIX)) {
                fixedSrc = fixedSrc.Substring(fixedSrc.IndexOf(BASE64_IDENTIFIER, StringComparison.Ordinal) + BASE64_IDENTIFIER
                    .Length + 1);
                try {
                    using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(fixedSrc))) {
                        PdfFormXObject xObject = iText.Html2pdf.Resolver.Resource.HtmlResourceResolver.ProcessAsSvg(stream, context
                            , null);
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
                    PdfFormXObject xObject = iText.Html2pdf.Resolver.Resource.HtmlResourceResolver.ProcessAsSvg(stream, context
                        , null);
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
            SvgProcessingUtil processingUtil = new SvgProcessingUtil(context.GetResourceResolver());
            return processingUtil.CreateXObjectFromProcessingResult(res, context, true);
        }
    }
}
