/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>img</c>
    /// element.
    /// </summary>
    public class ImgTagWorker : ITagWorker {
        /// <summary>The logger.</summary>
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.ImgTagWorker
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
            PdfXObject imageXObject = context.GetResourceResolver().RetrieveImageExtended(src);
            if (imageXObject != null) {
                if (imageXObject is PdfImageXObject) {
                    image = new ImgTagWorker.HtmlImage((PdfImageXObject)imageXObject);
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
