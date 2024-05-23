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
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply css grid properties and styles.</summary>
    public sealed class GridApplierUtil {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.GridApplierUtil
            ));

        private static readonly IDictionary<String, GridApplierUtil.NamedAreas> namedAreasCache = new ConcurrentDictionary
            <String, GridApplierUtil.NamedAreas>();

        private const int NAMED_AREAS_CACHE_CAPACITY = 10;

        /// <summary>Property map which maps property order in grid-area css prop to layout property</summary>
        private static readonly IDictionary<int, int?> propsMap = new Dictionary<int, int?>();

        static GridApplierUtil() {
            propsMap.Put(0, Property.GRID_ROW_START);
            propsMap.Put(1, Property.GRID_COLUMN_START);
            propsMap.Put(2, Property.GRID_ROW_END);
            propsMap.Put(3, Property.GRID_COLUMN_END);
        }

        private GridApplierUtil() {
        }

        // empty constructor
        /// <summary>Applies grid properties to a grid item.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="element">the element</param>
        public static void ApplyGridItemProperties(IDictionary<String, String> cssProps, IStylesContainer stylesContainer
            , IPropertyContainer element) {
            if (!(stylesContainer is JsoupElementNode) || !(((JsoupElementNode)stylesContainer).ParentNode() is JsoupElementNode
                )) {
                return;
            }
            IDictionary<String, String> parentStyles = ((JsoupElementNode)((JsoupElementNode)stylesContainer).ParentNode
                ()).GetStyles();
            if (!CssConstants.GRID.Equals(parentStyles.Get(CssConstants.DISPLAY))) {
                // Not a grid - return
                return;
            }
            // Let's parse grid-template-areas here on child level as we need it here
            String gridTemplateAreas = parentStyles.Get(CssConstants.GRID_TEMPLATE_AREAS);
            GridApplierUtil.NamedAreas namedAreas = null;
            if (gridTemplateAreas != null && !CommonCssConstants.NONE.Equals(gridTemplateAreas)) {
                namedAreas = ParseGridTemplateAreas(gridTemplateAreas);
            }
            foreach (KeyValuePair<String, String> entry in cssProps) {
                if (CssConstants.GRID_AREA.Equals(entry.Key)) {
                    String gridArea = entry.Value;
                    String[] gridAreaParts = iText.Commons.Utils.StringUtil.Split(gridArea, "/");
                    for (int i = 0; i < gridAreaParts.Length; ++i) {
                        String part = gridAreaParts[i].Trim();
                        if (CommonCssConstants.AUTO.Equals(part)) {
                            // We override already set value if any
                            element.DeleteOwnProperty(propsMap.Get(i).Value);
                            continue;
                        }
                        int? partInt = CssDimensionParsingUtils.ParseInteger(part);
                        if (partInt != null) {
                            element.SetProperty(propsMap.Get(i).Value, partInt);
                        }
                        else {
                            if (namedAreas != null && i == 0) {
                                // We are interested in the 1st element in grid area for now
                                // so let's even break immediately
                                namedAreas.SetPlaceToElement(part, element);
                                break;
                            }
                        }
                    }
                }
                if (CssConstants.GRID_COLUMN_START.Equals(entry.Key)) {
                    int? columnStart = CssDimensionParsingUtils.ParseInteger(entry.Value);
                    if (columnStart != null) {
                        element.SetProperty(Property.GRID_COLUMN_START, columnStart);
                    }
                }
                if (CssConstants.GRID_COLUMN_END.Equals(entry.Key)) {
                    int? columnStart = CssDimensionParsingUtils.ParseInteger(entry.Value);
                    if (columnStart != null) {
                        element.SetProperty(Property.GRID_COLUMN_END, columnStart);
                    }
                }
                if (CssConstants.GRID_ROW_START.Equals(entry.Key)) {
                    int? columnStart = CssDimensionParsingUtils.ParseInteger(entry.Value);
                    if (columnStart != null) {
                        element.SetProperty(Property.GRID_ROW_START, columnStart);
                    }
                }
                if (CssConstants.GRID_ROW_END.Equals(entry.Key)) {
                    int? columnStart = CssDimensionParsingUtils.ParseInteger(entry.Value);
                    if (columnStart != null) {
                        element.SetProperty(Property.GRID_ROW_END, columnStart);
                    }
                }
            }
        }

        /// <summary>Applies grid properties to a grid container.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="container">the grid container</param>
        /// <param name="context">the context</param>
        public static void ApplyGridContainerProperties(IDictionary<String, String> cssProps, IPropertyContainer container
            , ProcessorContext context) {
            float emValue = CssDimensionParsingUtils.ParseAbsoluteFontSize(cssProps.Get(CssConstants.FONT_SIZE));
            float remValue = context.GetCssContext().GetRootFontSize();
            ApplyTemplate(cssProps.Get(CssConstants.GRID_TEMPLATE_COLUMNS), container, Property.GRID_TEMPLATE_COLUMNS, 
                emValue, remValue);
            ApplyTemplate(cssProps.Get(CssConstants.GRID_TEMPLATE_ROWS), container, Property.GRID_TEMPLATE_ROWS, emValue
                , remValue);
            ApplyAuto(cssProps.Get(CssConstants.GRID_AUTO_ROWS), container, Property.GRID_AUTO_ROWS, emValue, remValue
                );
            ApplyAuto(cssProps.Get(CssConstants.GRID_AUTO_COLUMNS), container, Property.GRID_AUTO_COLUMNS, emValue, remValue
                );
            UnitValue columnGap = CssDimensionParsingUtils.ParseLengthValueToPt(cssProps.Get(CssConstants.COLUMN_GAP), 
                emValue, remValue);
            if (columnGap != null) {
                container.SetProperty(Property.COLUMN_GAP, columnGap.GetValue());
            }
            UnitValue rowGap = CssDimensionParsingUtils.ParseLengthValueToPt(cssProps.Get(CssConstants.ROW_GAP), emValue
                , remValue);
            if (rowGap != null) {
                container.SetProperty(Property.ROW_GAP, rowGap.GetValue());
            }
        }

        private static void ApplyAuto(String autoStr, IPropertyContainer container, int property, float emValue, float
             remValue) {
            if (autoStr != null) {
                GridValue value = GetGridValue(autoStr, emValue, remValue);
                // TODO DEVSIX-8324 - we support only absolute values for now
                // If some relative values are not supported after DEVSIX-8324, add the corresponding warning message
                if (value != null && value.IsAbsoluteValue()) {
                    container.SetProperty(property, value);
                }
            }
        }

        private static void ApplyTemplate(String templateStr, IPropertyContainer container, int property, float emValue
            , float remValue) {
            if (templateStr != null) {
                IList<String> templateStrArray = CssUtils.ExtractShorthandProperties(templateStr)[0];
                IList<GridValue> templateResult = new List<GridValue>();
                foreach (String str in templateStrArray) {
                    GridValue value = GetGridValue(str, emValue, remValue);
                    // TODO DEVSIX-8324 - we support only absolute values for now
                    // If some relative values are not supported after DEVSIX-8324, add the corresponding warning message
                    if (value != null && value.IsAbsoluteValue()) {
                        templateResult.Add(value);
                    }
                }
                if (!templateResult.IsEmpty()) {
                    container.SetProperty(property, templateResult);
                }
            }
        }

        private static GridValue GetGridValue(String str, float emValue, float remValue) {
            UnitValue unit = CssDimensionParsingUtils.ParseLengthValueToPt(str, emValue, remValue);
            if (unit != null) {
                return GridValue.CreateUnitValue(unit);
            }
            else {
                if (CommonCssConstants.MIN_CONTENT.Equals(str)) {
                    return GridValue.CreateSizeValue(SizingValue.CreateMinContentValue());
                }
                else {
                    if (CommonCssConstants.MAX_CONTENT.Equals(str)) {
                        return GridValue.CreateSizeValue(SizingValue.CreateMaxContentValue());
                    }
                    else {
                        if (CommonCssConstants.AUTO.Equals(str)) {
                            return GridValue.CreateSizeValue(SizingValue.CreateAutoValue());
                        }
                    }
                }
            }
            float? fr = CssDimensionParsingUtils.ParseFlex(str);
            if (fr != null) {
                return GridValue.CreateFlexValue((float)fr);
            }
            // TODO DEVSIX-8324 - add a warning for the values we do not support yet
            return null;
        }

        private static GridApplierUtil.NamedAreas ParseGridTemplateAreas(String templateAreas) {
            GridApplierUtil.NamedAreas res = namedAreasCache.Get(templateAreas);
            if (res != null) {
                return res;
            }
            res = new GridApplierUtil.NamedAreas();
            String[] rows = iText.Commons.Utils.StringUtil.Split(templateAreas, "[\\\"|']");
            int rowIdx = 0;
            foreach (String row in rows) {
                String rowTrimmed = row.Trim();
                if (String.IsNullOrEmpty(rowTrimmed)) {
                    continue;
                }
                ++rowIdx;
                int columnIdx = 0;
                String[] names = iText.Commons.Utils.StringUtil.Split(rowTrimmed, "\\s+");
                foreach (String name in names) {
                    if (String.IsNullOrEmpty(name)) {
                        continue;
                    }
                    ++columnIdx;
                    res.AddName(name, rowIdx, columnIdx);
                }
            }
            if (namedAreasCache.Count >= NAMED_AREAS_CACHE_CAPACITY) {
                namedAreasCache.Clear();
            }
            namedAreasCache.Put(templateAreas, res);
            return res;
        }

        private sealed class NamedAreas {
            private const String DOT_PLACEHOLDER = ".";

            private readonly IDictionary<String, GridApplierUtil.Placement> areas = new Dictionary<String, GridApplierUtil.Placement
                >();

            internal NamedAreas() {
            }

            // Empty constructor
            public void AddName(String name, int row, int column) {
                // It has a special meaning saying this area is not named and grid-template-areas doesn't work for it
                if (DOT_PLACEHOLDER.Equals(name)) {
                    return;
                }
                GridApplierUtil.Placement placement = areas.Get(name);
                if (placement == null) {
                    areas.Put(name, new GridApplierUtil.Placement(row, row, column, column));
                }
                else {
                    placement.IncreaseSpansTill(row, column);
                }
            }

            public void SetPlaceToElement(String name, IPropertyContainer element) {
                GridApplierUtil.Placement placement = areas.Get(name);
                if (placement == null) {
                    return;
                }
                element.SetProperty(Property.GRID_ROW_START, placement.GetRowStart());
                element.SetProperty(Property.GRID_ROW_END, placement.GetRowEnd() + 1);
                element.SetProperty(Property.GRID_COLUMN_START, placement.GetColumnStart());
                element.SetProperty(Property.GRID_COLUMN_END, placement.GetColumnEnd() + 1);
            }
        }

        private sealed class Placement {
            // 1-based indexes.
            private int rowStart;

            private int rowEnd;

            private int columnStart;

            private int columnEnd;

            public Placement(int rowStart, int rowEnd, int columnStart, int columnEnd) {
                this.rowStart = rowStart;
                this.rowEnd = rowEnd;
                this.columnStart = columnStart;
                this.columnEnd = columnEnd;
            }

            public void IncreaseSpansTill(int row, int column) {
                bool valid = false;
                if (row == rowEnd + 1) {
                    valid = column == columnEnd;
                }
                else {
                    if (column == columnEnd + 1) {
                        valid = row == rowEnd;
                    }
                }
                // valid stays false
                if (!valid) {
                    LOGGER.LogError(Html2PdfLogMessageConstant.GRID_TEMPLATE_AREAS_IS_INVALID);
                    return;
                }
                rowEnd = row;
                columnEnd = column;
            }

            public int GetRowStart() {
                return rowStart;
            }

            public int GetRowEnd() {
                return rowEnd;
            }

            public int GetColumnStart() {
                return columnStart;
            }

            public int GetColumnEnd() {
                return columnEnd;
            }
        }
    }
}
