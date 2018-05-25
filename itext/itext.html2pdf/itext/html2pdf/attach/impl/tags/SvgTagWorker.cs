using System;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html.Node;
using iText.Html2pdf.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.Svg.Exceptions;
using iText.Svg.Processors;
using iText.Svg.Processors.Impl;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>svg</c>
    /// element.
    /// </summary>
    public class SvgTagWorker : ITagWorker {
        internal Image svgImage;

        internal ISvgProcessorResult processingResult;

        /// <summary>The logger.</summary>
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(ObjectTagWorker));

        /// <summary>
        /// Creates a new
        /// <see cref="SvgTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public SvgTagWorker(IElementNode element, ProcessorContext context) {
            svgImage = null;
            try {
                ISvgProcessor proc = new DefaultSvgProcessor();
                //TODO(blocked by DEVSIX-1955, RND-982): uncomment and register in the mapping
                //processingResult = proc.process((INode) element);
                processingResult = null;
            }
            catch (SvgProcessingException pe) {
                LOGGER.Error(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_IMAGE_AS_SVG, pe);
            }
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            if (context.GetPdfDocument() != null && processingResult != null) {
                SvgProcessingUtil util = new SvgProcessingUtil();
                svgImage = util.CreateImageFromProcessingResult(processingResult, context.GetPdfDocument());
            }
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return svgImage;
        }
    }
}
