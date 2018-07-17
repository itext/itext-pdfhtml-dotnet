using System;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
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
        private Image svgImage;

        private ISvgProcessorResult processingResult;

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
                SvgConverterProperties props = new SvgConverterProperties().SetBaseUri(context.GetBaseUri());
                processingResult = new DefaultSvgProcessor().Process((INode)element, props);
                context.StartProcessingInlineSvg();
            }
            catch (SvgProcessingException pe) {
                LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.SvgTagWorker)).Error(iText.Html2pdf.LogMessageConstant
                    .UNABLE_TO_PROCESS_IMAGE_AS_SVG, pe);
            }
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            if (context.GetPdfDocument() != null && processingResult != null) {
                SvgProcessingUtil util = new SvgProcessingUtil();
                svgImage = util.CreateImageFromProcessingResult(processingResult, context.GetPdfDocument());
                context.EndProcessingInlineSvg();
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
