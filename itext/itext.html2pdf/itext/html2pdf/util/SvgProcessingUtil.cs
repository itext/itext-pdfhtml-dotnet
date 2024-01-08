/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Element;
using iText.StyledXmlParser.Resolver.Resource;
using iText.Svg.Converter;
using iText.Svg.Element;
using iText.Svg.Processors;
using iText.Svg.Renderers;
using iText.Svg.Xobject;

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
        /// Create
        /// <c>SvgImage</c>
        /// layout object tied to the passed
        /// <c>PdfDocument</c>
        /// using the SVG processing result.
        /// </summary>
        /// <param name="result">processing result containing the SVG information</param>
        /// <param name="pdfDocument">pdf that shall contain the image</param>
        /// <returns>SVG image layout object</returns>
        public virtual Image CreateImageFromProcessingResult(ISvgProcessorResult result, PdfDocument pdfDocument) {
            SvgImageXObject xObject = (SvgImageXObject)CreateXObjectFromProcessingResult(result, pdfDocument);
            return new SvgImage(xObject);
        }

        /// <summary>
        /// Create
        /// <c>SvgImage</c>
        /// layout object using the SVG processing result.
        /// </summary>
        /// <param name="result">processing result containing the SVG information</param>
        /// <returns>SVG image layout object</returns>
        public virtual Image CreateSvgImageFromProcessingResult(ISvgProcessorResult result) {
            return CreateImageFromProcessingResult(result, null);
        }

        /// <summary>
        /// Create an
        /// <see cref="iText.Kernel.Pdf.Xobject.PdfFormXObject"/>
        /// tied to the passed
        /// <c>PdfDocument</c>
        /// using the SVG processing result.
        /// </summary>
        /// <param name="result">processing result containing the SVG information</param>
        /// <param name="pdfDocument">pdf that shall contain the SVG image</param>
        /// <returns>
        /// 
        /// <see cref="iText.Svg.Xobject.SvgImageXObject"/>
        /// instance
        /// </returns>
        public virtual PdfFormXObject CreateXObjectFromProcessingResult(ISvgProcessorResult result, PdfDocument pdfDocument
            ) {
            ISvgNodeRenderer topSvgRenderer = result.GetRootRenderer();
            float width;
            float height;
            float[] wh = SvgConverter.ExtractWidthAndHeight(topSvgRenderer);
            width = wh[0];
            height = wh[1];
            SvgImageXObject svgImageXObject = new SvgImageXObject(new Rectangle(0, 0, width, height), result, resourceResolver
                );
            if (pdfDocument != null) {
                svgImageXObject.Generate(pdfDocument);
            }
            return svgImageXObject;
        }
    }
}
