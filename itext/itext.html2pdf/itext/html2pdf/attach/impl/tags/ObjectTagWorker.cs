/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using System;
using System.IO;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.Html2pdf.Util;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
using iText.Svg.Converter;
using iText.Svg.Exceptions;
using iText.Svg.Processors;
using iText.Svg.Processors.Impl;

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
                try {
                    using (Stream svgStream = context.GetResourceResolver().RetrieveResourceAsInputStream(element.GetAttribute
                        (AttributeConstants.DATA))) {
                        if (svgStream != null) {
                            try {
                                SvgConverterProperties svgConverterProperties = new SvgConverterProperties();
                                svgConverterProperties.SetBaseUri(context.GetBaseUri()).SetFontProvider(context.GetFontProvider()).SetMediaDeviceDescription
                                    (context.GetDeviceDescription());
                                res = SvgConverter.ParseAndProcess(svgStream, svgConverterProperties);
                            }
                            catch (SvgProcessingException spe) {
                                LOGGER.Error(spe.Message);
                            }
                        }
                    }
                }
                catch (System.IO.IOException) {
                    LOGGER.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
                        , context.GetBaseUri(), element.GetAttribute(AttributeConstants.DATA)));
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
