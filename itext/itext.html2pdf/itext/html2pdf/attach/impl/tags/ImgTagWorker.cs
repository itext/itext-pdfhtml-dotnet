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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;
using iText.Svg.Element;
using iText.Svg.Xobject;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>img</c>
    /// element.
    /// </summary>
    public class ImgTagWorker : ITagWorker {
        /// <summary>The logger.</summary>
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.ImgTagWorker
            ));

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
            PdfXObject imageXObject = context.GetResourceResolver().RetrieveImage(src);
            if (imageXObject != null) {
                if (imageXObject is PdfImageXObject) {
                    image = new ImgTagWorker.HtmlImage((PdfImageXObject)imageXObject);
                }
                else {
                    if (imageXObject is SvgImageXObject) {
                        SvgImageXObject svgImageXObject = (SvgImageXObject)imageXObject;
                        // TODO DEVSIX-8829 remove relative sized SVG generating after adding support in object element
                        if (svgImageXObject.IsRelativeSized()) {
                            svgImageXObject.UpdateBBox(null, null);
                            if (context.GetPdfDocument() != null) {
                                svgImageXObject.Generate(context.GetPdfDocument());
                            }
                            svgImageXObject.SetRelativeSized(false);
                        }
                        image = new SvgImage(svgImageXObject);
                    }
                    else {
                        if (imageXObject is PdfFormXObject) {
                            image = new ImgTagWorker.HtmlImage((PdfFormXObject)imageXObject);
                        }
                        else {
                            throw new InvalidOperationException();
                        }
                    }
                }
            }
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
            if (element.GetStyles() != null && CssConstants.ABSOLUTE.Equals(element.GetStyles().Get(CssConstants.POSITION
                ))) {
                // TODO DEVSIX-1393: we don't support absolute positioning in inline context.
                // This workaround allows to identify image as an element which needs to be processed outside of inline context.
                // See AbsoluteReplacedHeight001Test.
                display = CssConstants.BLOCK;
            }
            if (image != null) {
                String altText = element.GetAttribute(AttributeConstants.ALT);
                if (altText != null) {
                    image.GetAccessibilityProperties().SetAlternateDescription(altText);
                }
                AccessiblePropHelper.TrySetLangAttribute(image, element);
            }
            if (image != null) {
                String objectFitValue = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.OBJECT_FIT) : null;
                image.SetObjectFit(GetObjectFitValue(objectFitValue));
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

//\cond DO_NOT_DOCUMENT
        /// <summary>Gets the display value.</summary>
        /// <returns>the display value</returns>
        internal virtual String GetDisplay() {
            return display;
        }
//\endcond

        private ObjectFit GetObjectFitValue(String objectFitValue) {
            if (objectFitValue == null) {
                return ObjectFit.FILL;
            }
            switch (objectFitValue) {
                case CssConstants.CONTAIN: {
                    return ObjectFit.CONTAIN;
                }

                case CssConstants.COVER: {
                    return ObjectFit.COVER;
                }

                case CssConstants.SCALE_DOWN: {
                    return ObjectFit.SCALE_DOWN;
                }

                case CssConstants.NONE: {
                    return ObjectFit.NONE;
                }

                case CssConstants.FILL: {
                    return ObjectFit.FILL;
                }

                default: {
                    LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.UNEXPECTED_VALUE_OF_OBJECT_FIT, objectFitValue
                        ));
                    return ObjectFit.FILL;
                }
            }
        }

        /// <summary>Implementation of the Image class when used in the context of HTML to PDF conversion.</summary>
        private class HtmlImage : Image {
            private const double PX_TO_PT_MULTIPLIER = 0.75;

            /// <summary>
            /// In iText, we use user unit for the image sizes (and by default
            /// one user unit = one point), whereas images are usually measured
            /// in pixels.
            /// </summary>
            private double dimensionMultiplier = 1;

            /// <summary>
            /// Creates a new
            /// <see cref="HtmlImage"/>
            /// instance.
            /// </summary>
            /// <param name="xObject">an Image XObject</param>
            public HtmlImage(PdfImageXObject xObject)
                : base(xObject) {
                this.dimensionMultiplier = PX_TO_PT_MULTIPLIER;
            }

            /// <summary>
            /// Creates a new
            /// <see cref="HtmlImage"/>
            /// instance.
            /// </summary>
            /// <param name="xObject">an Image XObject</param>
            public HtmlImage(PdfFormXObject xObject)
                : base(xObject) {
            }

            /* (non-Javadoc)
            * @see com.itextpdf.layout.element.Image#getImageWidth()
            */
            public override float GetImageWidth() {
                return (float)(xObject.GetWidth() * dimensionMultiplier);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.layout.element.Image#getImageHeight()
            */
            public override float GetImageHeight() {
                return (float)(xObject.GetHeight() * dimensionMultiplier);
            }
        }
    }
}
