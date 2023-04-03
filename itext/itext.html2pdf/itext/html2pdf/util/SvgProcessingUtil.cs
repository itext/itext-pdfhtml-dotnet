/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Element;
using iText.StyledXmlParser.Resolver.Resource;
using iText.Svg.Converter;
using iText.Svg.Processors;
using iText.Svg.Processors.Impl;
using iText.Svg.Renderers;
using iText.Svg.Renderers.Impl;

namespace iText.Html2pdf.Util {
    /// <summary>Utility class for handling operations related to SVG</summary>
    public class SvgProcessingUtil {
        private ResourceResolver resourceResolver;

        /// <summary>
        /// Creates a new
        /// <see cref="SvgProcessingUtil"/>
        /// instance based on
        /// <see cref="iText.StyledXmlParser.Resolver.Resource.ResourceResolver"/>
        /// which is used to resolve a variety of resources.
        /// </summary>
        /// <param name="resourceResolver">the resource resolver</param>
        public SvgProcessingUtil(ResourceResolver resourceResolver) {
            this.resourceResolver = resourceResolver;
        }

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
            PdfFormXObject xObject = CreateXObjectFromProcessingResult(result, pdfDocument);
            return new Image(xObject);
        }

        /// <summary>
        /// Create an
        /// <see cref="iText.Kernel.Pdf.Xobject.PdfFormXObject"/>
        /// tied to the passed
        /// <c>PdfDocument</c>
        /// using the SVG processing result.
        /// </summary>
        /// <param name="result">Processing result containing the SVG information</param>
        /// <param name="pdfDocument">pdf that shall contain the image</param>
        /// <returns>PdfFormXObject instance</returns>
        public virtual PdfFormXObject CreateXObjectFromProcessingResult(ISvgProcessorResult result, PdfDocument pdfDocument
            ) {
            ISvgNodeRenderer topSvgRenderer = result.GetRootRenderer();
            float width;
            float height;
            float[] wh = SvgConverter.ExtractWidthAndHeight(topSvgRenderer);
            width = wh[0];
            height = wh[1];
            PdfFormXObject pdfForm = new PdfFormXObject(new Rectangle(0, 0, width, height));
            PdfCanvas canvas = new PdfCanvas(pdfForm, pdfDocument);
            ResourceResolver tempResolver = new ResourceResolver(null, resourceResolver.GetRetriever());
            // TODO DEVSIX-4107 pass the resourceResolver variable (not tempResolver variable) to the
            //  SvgDrawContext constructor so that the SVG inside the SVG is processed.
            SvgDrawContext context = new SvgDrawContext(tempResolver, result.GetFontProvider());
            if (result is SvgProcessorResult) {
                context.SetCssContext(((SvgProcessorResult)result).GetContext().GetCssContext());
            }
            context.AddNamedObjects(result.GetNamedObjects());
            context.PushCanvas(canvas);
            ISvgNodeRenderer root = new PdfRootSvgNodeRenderer(topSvgRenderer);
            root.Draw(context);
            return pdfForm;
        }
    }
}
