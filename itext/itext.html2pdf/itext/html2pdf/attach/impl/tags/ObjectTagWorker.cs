using System;
using System.IO;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.Html2pdf.Util;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Svg.Converter;
using iText.Svg.Exceptions;
using iText.Svg.Processors;
using iText.IO.Util;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>object</c>
    /// element.
    /// </summary>
    public class ObjectTagWorker : ITagWorker {
        /// <summary>The logger.</summary>
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.ObjectTagWorker
            ));

        /// <summary>The Svg as image.</summary>
        private Image image;

        private ISvgProcessorResult res;

        private SvgProcessingUtil processUtil;

        /// <summary>
        /// Creates a new
        /// <see cref="ImgTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ObjectTagWorker(IElementNode element, ProcessorContext context) {
            image = null;
            res = null;
            processUtil = new SvgProcessingUtil();
            //Retrieve object type
            String type = element.GetAttribute(AttributeConstants.TYPE);
            if (IsSvgImage(type)) {
                //Use resource resolver to retrieve the URL
                Stream svgStream = context.GetResourceResolver().RetrieveResourceAsInputStream(element.GetAttribute(AttributeConstants
                    .DATA));
                if (svgStream != null) {
                    try {
                        res = SvgConverter.ParseAndProcess(svgStream);
                    }
                    catch (SvgProcessingException spe) {
                        LOGGER.Error(spe.Message);
                    }
                    catch (System.IO.IOException) {
                        LOGGER.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI, context.GetBaseUri(), element.GetAttribute(AttributeConstants.DATA)));
                    }
                }
            }
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            if (context.GetPdfDocument() != null) {
                PdfDocument document = context.GetPdfDocument();
                //Create Image object
                if (res != null) {
                    image = processUtil.CreateImageFromProcessingResult(res, document);
                }
            }
            else {
                LOGGER.Error(iText.Html2pdf.LogMessageConstant.PDF_DOCUMENT_NOT_PRESENT);
            }
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return image;
        }

        private bool IsSvgImage(String typeAttribute) {
            return typeAttribute.Equals(AttributeConstants.ObjectTypes.SVGIMAGE);
        }
    }
}
