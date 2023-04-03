/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Layout.Borders;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for Td elements.
    /// </summary>
    public class TdTagCssApplier : BlockCssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.impl.BlockCssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker worker) {
            base.Apply(context, stylesContainer, worker);
            IPropertyContainer cell = worker.GetElementResult();
            if (cell != null) {
                IDictionary<String, String> cssProps = stylesContainer.GetStyles();
                VerticalAlignmentApplierUtil.ApplyVerticalAlignmentForCells(cssProps, context, cell);
                float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
                float rem = context.GetCssContext().GetRootFontSize();
                Border[] bordersArray = BorderStyleApplierUtil.GetBordersArray(cssProps, em, rem);
                if (bordersArray[0] == null) {
                    cell.SetProperty(Property.BORDER_TOP, Border.NO_BORDER);
                }
                if (bordersArray[1] == null) {
                    cell.SetProperty(Property.BORDER_RIGHT, Border.NO_BORDER);
                }
                if (bordersArray[2] == null) {
                    cell.SetProperty(Property.BORDER_BOTTOM, Border.NO_BORDER);
                }
                if (bordersArray[3] == null) {
                    cell.SetProperty(Property.BORDER_LEFT, Border.NO_BORDER);
                }
            }
        }
    }
}
