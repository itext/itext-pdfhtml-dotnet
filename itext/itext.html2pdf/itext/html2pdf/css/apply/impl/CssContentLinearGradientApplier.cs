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
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for linear-gradient elements in content CSS property.
    /// </summary>
    public class CssContentLinearGradientApplier : ICssApplier {
        /// <summary>The default width of the div content in the points, and this will be 300 pixels.</summary>
        private const float DEFAULT_CONTENT_WIDTH_PT = 225;

        /// <summary>The default height of the div content in the points, and this will be 150 pixels.</summary>
        private const float DEFAULT_CONTENT_HEIGHT_PT = 112.5f;

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.ICssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public virtual void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            IDictionary<String, String> cssProps = stylesContainer.GetStyles();
            IPropertyContainer container = tagWorker.GetElementResult();
            if (container != null) {
                if (container is Div) {
                    if (!cssProps.ContainsKey(CssConstants.WIDTH) || CssConstants.AUTO.Equals(cssProps.Get(CssConstants.WIDTH)
                        )) {
                        ((Div)container).SetWidth(DEFAULT_CONTENT_WIDTH_PT);
                    }
                    if (!cssProps.ContainsKey(CssConstants.HEIGHT) || CssConstants.AUTO.Equals(cssProps.Get(CssConstants.HEIGHT
                        ))) {
                        ((Div)container).SetHeight(DEFAULT_CONTENT_HEIGHT_PT);
                    }
                }
                WidthHeightApplierUtil.ApplyWidthHeight(cssProps, context, container);
                BackgroundApplierUtil.ApplyBackground(cssProps, context, container);
            }
        }
    }
}
