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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for table elements.
    /// </summary>
    public class TableTagCssApplier : BlockCssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.impl.BlockCssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker worker) {
            base.Apply(context, stylesContainer, worker);
            Table table = (Table)worker.GetElementResult();
            if (table != null) {
                String tableLayout = stylesContainer.GetStyles().Get(CssConstants.TABLE_LAYOUT);
                if (tableLayout != null) {
                    table.SetProperty(Property.TABLE_LAYOUT, tableLayout);
                }
                String borderCollapse = stylesContainer.GetStyles().Get(CssConstants.BORDER_COLLAPSE);
                // BorderCollapsePropertyValue.COLLAPSE is default in iText layout
                if (null == borderCollapse || CssConstants.SEPARATE.Equals(borderCollapse)) {
                    table.SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
                }
                String borderSpacing = stylesContainer.GetStyles().Get(CssConstants.BORDER_SPACING);
                if (null != borderSpacing) {
                    String[] props = iText.Commons.Utils.StringUtil.Split(borderSpacing, "\\s+");
                    if (1 == props.Length) {
                        table.SetHorizontalBorderSpacing(CssDimensionParsingUtils.ParseAbsoluteLength(props[0]));
                        table.SetVerticalBorderSpacing(CssDimensionParsingUtils.ParseAbsoluteLength(props[0]));
                    }
                    else {
                        if (2 == props.Length) {
                            table.SetHorizontalBorderSpacing(CssDimensionParsingUtils.ParseAbsoluteLength(props[0]));
                            table.SetVerticalBorderSpacing(CssDimensionParsingUtils.ParseAbsoluteLength(props[1]));
                        }
                    }
                }
            }
        }
    }
}
