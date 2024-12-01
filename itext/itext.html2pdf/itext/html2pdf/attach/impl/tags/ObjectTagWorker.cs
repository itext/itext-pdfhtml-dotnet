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
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.Html2pdf.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Resolver.Resource;
using iText.Svg.Converter;
using iText.Svg.Element;
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
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.ObjectTagWorker
            ));

        /// <summary>Helper for conversion of SVG processing results.</summary>
        private readonly SvgProcessingUtil processUtil;

        /// <summary>An Outcome of the worker.</summary>
        /// <remarks>An Outcome of the worker. The Svg as image.</remarks>
        private Image image;

        /// <summary>Output of SVG processing.</summary>
        /// <remarks>Output of SVG processing.  Intermediate result.</remarks>
        private ISvgProcessorResult res;

        /// <summary>
        /// Creates a new
        /// <see cref="ImgTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ObjectTagWorker(IElementNode element, ProcessorContext context) {
            this.processUtil = new SvgProcessingUtil(context.GetResourceResolver());
            //Retrieve object type
            String type = element.GetAttribute(AttributeConstants.TYPE);
            if (IsSvgImage(type)) {
                String dataValue = element.GetAttribute(AttributeConstants.DATA);
                try {
                    using (Stream svgStream = context.GetResourceResolver().RetrieveResourceAsInputStream(dataValue)) {
                        if (svgStream != null) {
                            SvgConverterProperties props = ContextMappingHelper.MapToSvgConverterProperties(context);
                            if (!ResourceResolver.IsDataSrc(dataValue)) {
                                Uri fullURL = context.GetResourceResolver().ResolveAgainstBaseUri(dataValue);
                                String dir = FileUtil.ParentDirectory(fullURL);
                                props.SetBaseUri(dir);
                            }
                            res = SvgConverter.ParseAndProcess(svgStream, props);
                        }
                    }
                }
                catch (SvgProcessingException spe) {
                    LOGGER.LogError(spe.Message);
                }
                catch (System.IO.IOException ie) {
                    LOGGER.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
                        , context.GetBaseUri(), element.GetAttribute(AttributeConstants.DATA), ie));
                }
                catch (UriFormatException ie) {
                    LOGGER.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
                        , context.GetBaseUri(), element.GetAttribute(AttributeConstants.DATA), ie));
                }
            }
        }

        // TODO DEVSIX-4460. According specs 'At least one of the "data" or "type" attribute MUST be defined.'
        private bool IsSvgImage(String typeAttribute) {
            return AttributeConstants.ObjectTypes.SVGIMAGE.Equals(typeAttribute);
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            // Create Image object
            if (res != null) {
                image = new SvgImage(processUtil.CreateXObjectFromProcessingResult(res, context));
                AccessiblePropHelper.TrySetLangAttribute(image, element);
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
    }
}
