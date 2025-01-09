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
using iText.Html2pdf.Attach;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Resolver.Resource;
using iText.Svg.Converter;
using iText.Svg.Element;
using iText.Svg.Processors;
using iText.Svg.Renderers;
using iText.Svg.Utils;
using iText.Svg.Xobject;

namespace iText.Html2pdf.Util {
    /// <summary>Utility class for handling operations related to SVG</summary>
    public class SvgProcessingUtil {
        private readonly ResourceResolver resourceResolver;

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
        [Obsolete]
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
        [Obsolete]
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
        [Obsolete]
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

        /// <summary>
        /// Create an
        /// <see cref="iText.Kernel.Pdf.Xobject.PdfFormXObject"/>
        /// tied to the passed
        /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>
        /// using the SVG processing result.
        /// </summary>
        /// <param name="result">processing result containing the SVG information</param>
        /// <param name="context">html2pdf processor context</param>
        /// <returns>
        /// new
        /// <see cref="iText.Svg.Xobject.SvgImageXObject"/>
        /// instance
        /// </returns>
        public virtual SvgImageXObject CreateXObjectFromProcessingResult(ISvgProcessorResult result, ProcessorContext
             context) {
            float em = context.GetCssContext().GetCurrentFontSize();
            SvgDrawContext svgContext = new SvgDrawContext(resourceResolver, result.GetFontProvider());
            svgContext.GetCssContext().SetRootFontSize(context.GetCssContext().GetRootFontSize());
            if (IsSvgRelativeSized(result.GetRootRenderer(), context)) {
                return new SvgImageXObject(result, svgContext, em, context.GetPdfDocument());
            }
            else {
                Rectangle bbox = SvgCssUtils.ExtractWidthAndHeight(result.GetRootRenderer(), em, svgContext);
                SvgImageXObject svgImageXObject = new SvgImageXObject(bbox, result, resourceResolver);
                if (context.GetPdfDocument() != null) {
                    svgImageXObject.Generate(context.GetPdfDocument());
                }
                return svgImageXObject;
            }
        }

        private static bool IsSvgRelativeSized(ISvgNodeRenderer rootRenderer, ProcessorContext context) {
            float em = context.GetCssContext().GetCurrentFontSize();
            float rem = context.GetCssContext().GetRootFontSize();
            String widthStr = rootRenderer.GetAttribute(CommonCssConstants.WIDTH);
            UnitValue width = CssDimensionParsingUtils.ParseLengthValueToPt(widthStr, em, rem);
            String heightStr = rootRenderer.GetAttribute(CommonCssConstants.HEIGHT);
            UnitValue height = CssDimensionParsingUtils.ParseLengthValueToPt(heightStr, em, rem);
            return width == null || width.IsPercentValue() || height == null || height.IsPercentValue();
        }
    }
}
