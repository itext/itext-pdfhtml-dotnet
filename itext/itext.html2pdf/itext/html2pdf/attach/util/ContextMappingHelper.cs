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
using iText.Html2pdf.Attach;
using iText.Svg.Processors.Impl;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>
    /// Utility class which helps to map
    /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>
    /// properties on
    /// <see cref="iText.Svg.Processors.Impl.SvgConverterProperties"/>.
    /// </summary>
    public class ContextMappingHelper {
        /// <summary>
        /// Map
        /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>
        /// properties to
        /// <see cref="iText.Svg.Processors.Impl.SvgConverterProperties"/>.
        /// </summary>
        /// <param name="context">
        /// 
        /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>
        /// instance
        /// </param>
        /// <returns>
        /// 
        /// <see cref="iText.Svg.Processors.Impl.SvgConverterProperties"/>
        /// filled with necessary data for svg convertion
        /// </returns>
        public static SvgConverterProperties MapToSvgConverterProperties(ProcessorContext context) {
            SvgConverterProperties svgConverterProperties = new SvgConverterProperties();
            svgConverterProperties.SetFontProvider(context.GetFontProvider()).SetBaseUri(context.GetBaseUri()).SetMediaDeviceDescription
                (context.GetDeviceDescription()).SetResourceRetriever(context.GetResourceResolver().GetRetriever()).SetCssStyleSheet
                (context.GetCssStyleSheet());
            return svgConverterProperties;
        }
    }
}
