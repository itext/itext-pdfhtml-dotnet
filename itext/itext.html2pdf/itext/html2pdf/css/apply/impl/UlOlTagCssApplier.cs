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
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for Ul en Ol elements.
    /// </summary>
    public class UlOlTagCssApplier : BlockCssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.impl.BlockCssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            if (!(tagWorker.GetElementResult() is List || tagWorker.GetElementResult() is MulticolContainer)) {
                return;
            }
            IDictionary<String, String> css = stylesContainer.GetStyles();
            IPropertyContainer list = tagWorker.GetElementResult();
            if (CssConstants.INSIDE.Equals(css.Get(CssConstants.LIST_STYLE_POSITION))) {
                list.SetProperty(Property.LIST_SYMBOL_POSITION, ListSymbolPosition.INSIDE);
            }
            else {
                list.SetProperty(Property.LIST_SYMBOL_POSITION, ListSymbolPosition.OUTSIDE);
            }
            ListStyleApplierUtil.ApplyListStyleTypeProperty(stylesContainer, css, context, list);
            ListStyleApplierUtil.ApplyListStyleImageProperty(css, context, list);
            MultiColumnCssApplierUtil.ApplyMultiCol(css, context, list);
            base.Apply(context, stylesContainer, tagWorker);
            // process the padding considering the direction
            bool isRtl = BaseDirection.RIGHT_TO_LEFT.Equals(list.GetProperty<BaseDirection?>(Property.BASE_DIRECTION));
            if ((isRtl && !list.HasProperty(Property.PADDING_RIGHT)) || (!isRtl && !list.HasProperty(Property.PADDING_LEFT
                ))) {
                float em = CssDimensionParsingUtils.ParseAbsoluteLength(css.Get(CssConstants.FONT_SIZE));
                float rem = context.GetCssContext().GetRootFontSize();
                UnitValue startPadding = CssDimensionParsingUtils.ParseLengthValueToPt(css.Get(CssConstants.PADDING_INLINE_START
                    ), em, rem);
                list.SetProperty(isRtl ? Property.PADDING_RIGHT : Property.PADDING_LEFT, startPadding);
            }
        }
    }
}
