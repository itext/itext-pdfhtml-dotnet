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
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for elements with display grid.
    /// </summary>
    public class DisplayGridTagCssApplier : BlockCssApplier {
        /// <summary><inheritDoc/></summary>
        public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            base.Apply(context, stylesContainer, tagWorker);
            IPropertyContainer container = tagWorker.GetElementResult();
            if (container != null) {
                IDictionary<String, String> cssProps = stylesContainer.GetStyles();
                float emValue = CssDimensionParsingUtils.ParseAbsoluteFontSize(cssProps.Get(CssConstants.FONT_SIZE));
                float remValue = context.GetCssContext().GetRootFontSize();
                String templateColumnsStr = cssProps.Get(CssConstants.GRID_TEMPLATE_COLUMNS);
                ParseAndSetTemplate(templateColumnsStr, container, Property.GRID_TEMPLATE_COLUMNS, emValue, remValue);
                String templateRowsStr = cssProps.Get(CssConstants.GRID_TEMPLATE_ROWS);
                ParseAndSetTemplate(templateRowsStr, container, Property.GRID_TEMPLATE_ROWS, emValue, remValue);
                String autoRows = cssProps.Get(CssConstants.GRID_AUTO_ROWS);
                UnitValue autoRowsUnit = CssDimensionParsingUtils.ParseLengthValueToPt(autoRows, emValue, remValue);
                if (autoRowsUnit != null) {
                    container.SetProperty(Property.GRID_AUTO_ROWS, autoRowsUnit);
                }
                String autoColumns = cssProps.Get(CssConstants.GRID_AUTO_COLUMNS);
                UnitValue autoColumnsUnit = CssDimensionParsingUtils.ParseLengthValueToPt(autoColumns, emValue, remValue);
                if (autoColumnsUnit != null) {
                    container.SetProperty(Property.GRID_AUTO_COLUMNS, autoColumnsUnit);
                }
            }
            MultiColumnCssApplierUtil.ApplyMultiCol(stylesContainer.GetStyles(), context, container);
        }

        private static void ParseAndSetTemplate(String templateStr, IPropertyContainer container, int property, float
             emValue, float remValue) {
            if (templateStr != null) {
                IList<String> templateStrArray = CssUtils.ExtractShorthandProperties(templateStr)[0];
                IList<UnitValue> templateResult = new List<UnitValue>();
                foreach (String s in templateStrArray) {
                    UnitValue trackUnit = CssDimensionParsingUtils.ParseLengthValueToPt(s, emValue, remValue);
                    if (trackUnit != null) {
                        templateResult.Add(trackUnit);
                    }
                }
                if (!templateResult.IsEmpty()) {
                    container.SetProperty(property, templateResult);
                }
            }
        }
    }
}
