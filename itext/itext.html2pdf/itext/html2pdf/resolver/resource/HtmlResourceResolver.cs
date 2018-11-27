using System;
using System.IO;
using iText.Html2pdf.Attach;
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
        private const String SVG_BASE64_PREFIX = "data:image/svg+xml";

        private ProcessorContext context;

        /// <summary>
        /// Creates
        /// <see cref="HtmlResourceResolver"/>
        /// instance. If
        /// <paramref name="baseUri"/>
        /// is a string that represents an absolute URI with any schema
        /// except "file" - resources url values will be resolved exactly as "new URL(baseUrl, uriString)". Otherwise base URI
        /// will be handled as path in local file system.
        /// <p>
        /// If empty string or relative URI string is passed as base URI, then it will be resolved against current working
        /// directory of this application instance.
        /// </p>
        /// </summary>
        /// <param name="baseUri">base URI against which all relative resource URIs will be resolved.</param>
        /// <param name="context">
        /// 
        /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>
        /// instance for the current HTML to PDF conversion process
        /// </param>
        public HtmlResourceResolver(String baseUri, ProcessorContext context)
            : base(baseUri) {
            this.context = context;
        }

        protected override PdfXObject TryResolveBase64ImageSource(String src) {
            String fixedSrc = iText.IO.Util.StringUtil.ReplaceAll(src, "\\s", "");
            if (fixedSrc.StartsWith(SVG_BASE64_PREFIX)) {
                fixedSrc = fixedSrc.Substring(fixedSrc.IndexOf(BASE64IDENTIFIER, StringComparison.Ordinal) + 7);
                try {
                    PdfFormXObject xObject = ProcessAsSvg(new MemoryStream(Convert.FromBase64String(fixedSrc)), context);
                    if (xObject != null) {
                        return xObject;
                    }
                }
                catch (Exception) {
                }
            }
            return base.TryResolveBase64ImageSource(src);
        }

        /// <exception cref="System.Exception"/>
        protected override PdfXObject CreateImageByUrl(Uri url) {
            try {
                return base.CreateImageByUrl(url);
            }
            catch (Exception) {
                using (Stream @is = UrlUtil.OpenStream(url)) {
                    return ProcessAsSvg(@is, context);
                }
            }
        }

        /// <exception cref="System.IO.IOException"/>
        private PdfFormXObject ProcessAsSvg(Stream stream, ProcessorContext context) {
            SvgProcessingUtil processingUtil = new SvgProcessingUtil();
            SvgConverterProperties svgConverterProperties = new SvgConverterProperties();
            svgConverterProperties.SetBaseUri(context.GetBaseUri()).SetFontProvider(context.GetFontProvider()).SetMediaDeviceDescription
                (context.GetDeviceDescription());
            ISvgProcessorResult res = SvgConverter.ParseAndProcess(stream, svgConverterProperties);
            if (context.GetPdfDocument() != null) {
                return processingUtil.CreateXObjectFromProcessingResult(res, context.GetPdfDocument());
            }
            else {
                return null;
            }
        }
    }
}
