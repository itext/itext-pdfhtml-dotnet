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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Logs;
using iText.Html2pdf.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
using iText.Svg.Element;
using iText.Svg.Exceptions;
using iText.Svg.Processors;
using iText.Svg.Processors.Impl;
using iText.Svg.Xobject;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>svg</c>
    /// element.
    /// </summary>
    public class SvgTagWorker : ITagWorker {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.SvgTagWorker
            ));

        private Image svgImage;

        private ISvgProcessorResult processingResult;

        /// <summary>
        /// Creates a new
        /// <see cref="SvgTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public SvgTagWorker(IElementNode element, ProcessorContext context) {
            svgImage = null;
            SvgConverterProperties props = ContextMappingHelper.MapToSvgConverterProperties(context);
            try {
                processingResult = new DefaultSvgProcessor().Process((INode)element, props);
            }
            catch (SvgProcessingException spe) {
                LOGGER.LogError(spe, Html2PdfLogMessageConstant.UNABLE_TO_PROCESS_SVG_ELEMENT);
            }
            context.StartProcessingInlineSvg();
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            if (processingResult != null) {
                SvgImageXObject svgImageXObject = new SvgProcessingUtil(context.GetResourceResolver()).CreateXObjectFromProcessingResult
                    (processingResult, context, true);
                svgImage = new SvgImage(svgImageXObject);
                AccessiblePropHelper.TrySetLangAttribute(svgImage, element);
                context.EndProcessingInlineSvg();
            }
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return svgImage;
        }
    }
}
