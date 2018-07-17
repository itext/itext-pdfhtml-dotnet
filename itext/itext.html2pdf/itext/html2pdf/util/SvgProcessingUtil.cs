using iText.Html2pdf.Html;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Element;
using iText.StyledXmlParser.Css.Util;
using iText.Svg.Processors;
using iText.Svg.Renderers;
using iText.Svg.Renderers.Impl;

namespace iText.Html2pdf.Util {
    /// <summary>Utility class for handling operations related to SVG</summary>
    public class SvgProcessingUtil {
        /// <summary>
        /// Create an
        /// <c>Image</c>
        /// layout object tied to the passed
        /// <c>PdfDocument</c>
        /// using the SVG processing result.
        /// </summary>
        /// <param name="result">Processing result containing the SVG information</param>
        /// <param name="pdfDocument">pdf that shall contain the image</param>
        /// <returns>image layout object</returns>
        public virtual Image CreateImageFromProcessingResult(ISvgProcessorResult result, PdfDocument pdfDocument) {
            ISvgNodeRenderer topSvgRenderer = result.GetRootRenderer();
            float width = CssUtils.ParseAbsoluteLength(topSvgRenderer.GetAttribute(AttributeConstants.WIDTH));
            float height = CssUtils.ParseAbsoluteLength(topSvgRenderer.GetAttribute(AttributeConstants.HEIGHT));
            PdfFormXObject pdfForm = new PdfFormXObject(new Rectangle(0, 0, width, height));
            PdfCanvas canvas = new PdfCanvas(pdfForm, pdfDocument);
            SvgDrawContext context = new SvgDrawContext(null, result.GetFontProvider());
            context.AddNamedObjects(result.GetNamedObjects());
            context.PushCanvas(canvas);
            ISvgNodeRenderer root = new PdfRootSvgNodeRenderer(topSvgRenderer);
            root.Draw(context);
            return new Image(pdfForm);
        }
    }
}
