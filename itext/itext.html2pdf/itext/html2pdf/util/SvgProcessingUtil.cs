/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
