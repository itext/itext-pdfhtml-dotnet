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
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for table rows.
    /// </summary>
    public class DisplayTableRowTagCssApplier : BlockCssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.impl.BlockCssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            IDictionary<String, String> cssProps = stylesContainer.GetStyles();
            base.Apply(context, stylesContainer, tagWorker);
            TableRowWrapper rowWrapper = ((DisplayTableRowTagWorker)tagWorker).GetTableRowWrapper();
            foreach (Cell cell in rowWrapper.GetCells()) {
                if (!cell.HasProperty(Property.BACKGROUND)) {
                    BackgroundApplierUtil.ApplyBackground(cssProps, context, cell);
                }
                if (!cell.HasProperty(Property.OPACITY)) {
                    OpacityApplierUtil.ApplyOpacity(cssProps, context, cell);
                }
            }
        }
        // TODO DEVSIX-1554
        //            if (!cell.hasProperty(Property.POSITION)) {
        //                PositionApplierUtil.applyPosition(cssProps, context, cell);
        //            }
        //            if (!cell.hasProperty(Property.TRANSFORM)) {
        //                TransformationApplierUtil.applyTransformation(cssProps, context, cell);
        //            }
    }
}
