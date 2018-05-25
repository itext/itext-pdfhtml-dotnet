/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

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
using System;
using System.IO;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.Html2pdf.Resolver.Resource;
using iText.Html2pdf.Util;
using iText.IO.Util;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Svg.Converter;
using iText.Svg.Exceptions;
using iText.Svg.Processors;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>img</c>
    /// element.
    /// </summary>
    public class ImgTagWorker : ITagWorker {
        /// <summary>The logger.</summary>
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(ObjectTagWorker));

        /// <summary>The image.</summary>
        private Image image;

        /// <summary>The display value.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="ImgTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ImgTagWorker(IElementNode element, ProcessorContext context) {
            String src = element.GetAttribute(AttributeConstants.SRC);
            if (src != null) {
                if (src.Contains(ResourceResolver.BASE64IDENTIFIER) || context.GetResourceResolver().IsImageTypeSupportedByImageDataFactory
                    (src)) {
                    ProcessAsImage(src, element, context);
                }
                else {
                    byte[] resourceBytes = context.GetResourceResolver().RetrieveBytesFromResource(src);
                    if (resourceBytes != null) {
                        Stream resourceStream = context.GetResourceResolver().RetrieveResourceAsInputStream(src);
                        //Try with svg
                        try {
                            ProcessAsSvg(resourceStream, context);
                        }
                        catch (SvgProcessingException) {
                            LOGGER.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_IMAGE_AS_SVG, src
                                ));
                        }
                        catch (System.IO.IOException) {
                            LOGGER.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI, context.GetBaseUri(), src));
                        }
                    }
                }
            }
            else {
                LOGGER.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
                    , context.GetBaseUri(), src));
            }
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
            // TODO this is a workaround for now Imgto that image is not added as inline
            if (element.GetStyles() != null && CssConstants.ABSOLUTE.Equals(element.GetStyles().Get(CssConstants.POSITION
                ))) {
                display = CssConstants.BLOCK;
            }
            String altText = element.GetAttribute(AttributeConstants.ALT);
            if (altText != null && image != null) {
                image.GetAccessibilityProperties().SetAlternateDescription(altText);
            }
        }

        /// <exception cref="System.IO.IOException"/>
        private void ProcessAsSvg(Stream stream, ProcessorContext context) {
            SvgProcessingUtil processingUtil = new SvgProcessingUtil();
            ISvgProcessorResult res = SvgConverter.ParseAndProcess(stream);
            if (context.GetPdfDocument() != null) {
                image = processingUtil.CreateImageFromProcessingResult(res, context.GetPdfDocument());
            }
        }

        private void ProcessAsImage(String src, IElementNode element, ProcessorContext context) {
            PdfImageXObject imageXObject = context.GetResourceResolver().RetrieveImage(src);
            if (imageXObject != null) {
                image = new ImgTagWorker.HtmlImage(this, imageXObject);
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return image;
        }

        /// <summary>Gets the display value.</summary>
        /// <returns>the display value</returns>
        internal virtual String GetDisplay() {
            return display;
        }

        /// <summary>Implementation of the Image class when used in the context of HTML to PDF conversion.</summary>
        private class HtmlImage : Image {
            /// <summary>
            /// In iText, we use user unit for the image sizes (and by default
            /// one user unit = one point), whereas images are usually measured
            /// in pixels.
            /// </summary>
            private double pxToPt = 0.75;

            /// <summary>
            /// * Creates a new
            /// <see cref="HtmlImage"/>
            /// instance.
            /// </summary>
            /// <param name="xObject">an Image XObject</param>
            public HtmlImage(ImgTagWorker _enclosing, PdfImageXObject xObject)
                : base(xObject) {
                this._enclosing = _enclosing;
            }

            /* (non-Javadoc)
            * @see com.itextpdf.layout.element.Image#getImageWidth()
            */
            public override float GetImageWidth() {
                return (float)(this.xObject.GetWidth() * this.pxToPt);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.layout.element.Image#getImageHeight()
            */
            public override float GetImageHeight() {
                return (float)(this.xObject.GetHeight() * this.pxToPt);
            }

            private readonly ImgTagWorker _enclosing;
        }
    }
}
