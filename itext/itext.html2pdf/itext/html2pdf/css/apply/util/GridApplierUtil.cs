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
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Datastructures;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Properties.Grid;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply css grid properties and styles.</summary>
    public sealed class GridApplierUtil {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.GridApplierUtil
            ));

        private static readonly Regex SPAN_PLACEMENT = iText.Commons.Utils.StringUtil.RegexCompile("^span\\s+(.+)$"
            );

        /// <summary>Property map which maps property order in grid-area css prop to layout property</summary>
        private static readonly IDictionary<int, int?> propsMap = new Dictionary<int, int?>();

        /// <summary>Property map which maps property order in grid-area css prop to grid span property</summary>
        private static readonly IDictionary<int, int?> spansMap = new Dictionary<int, int?>();

        static GridApplierUtil() {
            propsMap.Put(0, Property.GRID_ROW_START);
            propsMap.Put(1, Property.GRID_COLUMN_START);
            propsMap.Put(2, Property.GRID_ROW_END);
            propsMap.Put(3, Property.GRID_COLUMN_END);
            spansMap.Put(0, Property.GRID_ROW_SPAN);
            spansMap.Put(1, Property.GRID_COLUMN_SPAN);
            spansMap.Put(2, Property.GRID_ROW_SPAN);
            spansMap.Put(3, Property.GRID_COLUMN_SPAN);
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
            ApplyGridArea(cssProps, element);
            ApplyGridItemPlacement(cssProps.Get(CssConstants.GRID_COLUMN_END), element, Property.GRID_COLUMN_END, Property
                .GRID_COLUMN_SPAN);
            ApplyGridItemPlacement(cssProps.Get(CssConstants.GRID_COLUMN_START), element, Property.GRID_COLUMN_START, 
                Property.GRID_COLUMN_SPAN);
            ApplyGridItemPlacement(cssProps.Get(CssConstants.GRID_ROW_END), element, Property.GRID_ROW_END, Property.GRID_ROW_SPAN
                );
            ApplyGridItemPlacement(cssProps.Get(CssConstants.GRID_ROW_START), element, Property.GRID_ROW_START, Property
                .GRID_ROW_SPAN);
        }

        /// <summary>Applies grid properties to a grid container.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="container">the grid container</param>
        /// <param name="context">the context</param>
        public static void ApplyGridContainerProperties(IDictionary<String, String> cssProps, IPropertyContainer container
            , ProcessorContext context) {
            float emValue = CssDimensionParsingUtils.ParseAbsoluteFontSize(cssProps.Get(CssConstants.FONT_SIZE));
            float remValue = context.GetCssContext().GetRootFontSize();
            GridApplierUtil.NamedAreas namedAreas = ApplyNamedAreas(cssProps.Get(CssConstants.GRID_TEMPLATE_AREAS), container
                );
            ApplyTemplate(cssProps.Get(CssConstants.GRID_TEMPLATE_COLUMNS), container, Property.GRID_TEMPLATE_COLUMNS, 
                emValue, remValue, namedAreas);
            ApplyTemplate(cssProps.Get(CssConstants.GRID_TEMPLATE_ROWS), container, Property.GRID_TEMPLATE_ROWS, emValue
                , remValue, namedAreas);
            ApplyAuto(cssProps.Get(CssConstants.GRID_AUTO_ROWS), container, Property.GRID_AUTO_ROWS, emValue, remValue
                );
            ApplyAuto(cssProps.Get(CssConstants.GRID_AUTO_COLUMNS), container, Property.GRID_AUTO_COLUMNS, emValue, remValue
                );
            ApplyFlow(cssProps.Get(CssConstants.GRID_AUTO_FLOW), container);
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
                TemplateValue value = ParseTemplateValue(autoStr, emValue, remValue);
                if (value != null) {
                    container.SetProperty(property, value);
                }
            }
        }

        private static void ApplyFlow(String flow, IPropertyContainer container) {
            GridFlow value = GridFlow.ROW;
            if (flow != null) {
                if (flow.Contains(CommonCssConstants.COLUMN)) {
                    if (flow.Contains(CssConstants.DENSE)) {
                        value = GridFlow.COLUMN_DENSE;
                    }
                    else {
                        value = GridFlow.COLUMN;
                    }
                }
                else {
                    if (flow.Contains(CssConstants.DENSE)) {
                        value = GridFlow.ROW_DENSE;
                    }
                }
            }
            container.SetProperty(Property.GRID_FLOW, value);
        }

        private static GridApplierUtil.NamedAreas ApplyNamedAreas(String gridTemplateAreas, IPropertyContainer container
            ) {
            if (gridTemplateAreas == null || CommonCssConstants.NONE.Equals(gridTemplateAreas)) {
                return null;
            }
            GridApplierUtil.NamedAreas namedAreas = ParseGridTemplateAreas(gridTemplateAreas);
            IList<IElement> children = ((IAbstractElement)container).GetChildren();
            foreach (IElement child in children) {
                // Area name can be only in GRID_ROW_START
                Object propValue = child.GetProperty<Object>(Property.GRID_ROW_START);
                if (propValue is String) {
                    // It will override all props by integers if area name is found
                    namedAreas.SetPlaceToElement((String)propValue, child);
                }
            }
            return namedAreas;
        }

        private static void ApplyTemplate(String templateStr, IPropertyContainer container, int property, float emValue
            , float remValue, GridApplierUtil.NamedAreas namedAreas) {
            IDictionary<String, IList<int>> lineNumbersPerName = new Dictionary<String, IList<int>>();
            int namedAreaLength = 0;
            if (namedAreas != null) {
                if (property == Property.GRID_TEMPLATE_COLUMNS) {
                    lineNumbersPerName = namedAreas.GetNamedColumnNumbers();
                    namedAreaLength = namedAreas.GetColumnsCount();
                }
                else {
                    lineNumbersPerName = namedAreas.GetNamedRowNumbers();
                    namedAreaLength = namedAreas.GetRowsCount();
                }
            }
            IList<TemplateValue> templateResult = new List<TemplateValue>();
            int currentLine = 1;
            if (templateStr != null) {
                IList<String> templateStrArray = CssUtils.ExtractShorthandProperties(templateStr)[0];
                foreach (String str in templateStrArray) {
                    TemplateValue value = ParseTemplateValue(str, emValue, remValue, lineNumbersPerName, currentLine);
                    if (value != null) {
                        templateResult.Add(value);
                        if (value is FixedRepeatValue) {
                            currentLine += ((FixedRepeatValue)value).GetRepeatCount() * ((FixedRepeatValue)value).GetValues().Count;
                        }
                        else {
                            ++currentLine;
                        }
                    }
                }
                if (!templateResult.IsEmpty()) {
                    container.SetProperty(property, templateResult);
                }
            }
            // Now process all children to apply line names
            int startProperty;
            int endProperty;
            int spanProperty;
            if (property == Property.GRID_TEMPLATE_COLUMNS) {
                startProperty = Property.GRID_COLUMN_START;
                endProperty = Property.GRID_COLUMN_END;
                spanProperty = Property.GRID_COLUMN_SPAN;
            }
            else {
                startProperty = Property.GRID_ROW_START;
                endProperty = Property.GRID_ROW_END;
                spanProperty = Property.GRID_ROW_SPAN;
            }
            IList<IElement> children = ((IAbstractElement)container).GetChildren();
            foreach (IElement child in children) {
                SubstituteLinename(lineNumbersPerName, startProperty, child, Math.Max(namedAreaLength + 1, currentLine), "-start"
                    );
                SubstituteLinename(lineNumbersPerName, endProperty, child, Math.Max(namedAreaLength + 1, currentLine), "-end"
                    );
                SubstituteLinenameInSpan(lineNumbersPerName, startProperty, endProperty, spanProperty, child, Math.Max(namedAreaLength
                     + 1, currentLine));
            }
        }

        private static void SubstituteLinenameInSpan(IDictionary<String, IList<int>> lineNumbersPerName, int startProperty
            , int endProperty, int spanProperty, IElement child, int lastLineNumber) {
            Object propValue = child.GetProperty<Object>(spanProperty);
            if (!(propValue is String)) {
                // It means it's null or we processed it earlier
                return;
            }
            child.DeleteOwnProperty(spanProperty);
            // Here we need one of grid-row/column-start or grid-row/column-end
            // as otherwise the property doesn't have sense
            // And we know that there can't be both start and end at this point
            int? startPoint = child.GetProperty<int?>(startProperty);
            int? endPoint = child.GetProperty<int?>(endProperty);
            if (startPoint == null && endPoint == null) {
                return;
            }
            Tuple2<int, String> parsedValue = ParseStringValue((String)propValue);
            int distance = parsedValue.GetFirst();
            String strValue = parsedValue.GetSecond();
            IList<int> lineNumbers = lineNumbersPerName.Get(strValue);
            if (lineNumbers == null || distance <= 0 || strValue == null) {
                return;
            }
            // We should span by X linenames back or forth starting from current position
            int direction = startPoint != null ? 1 : -1;
            int startPosition = startPoint != null ? startPoint.Value : endPoint.Value;
            // linenumbers are sorted, let's find current position in the array
            int start = -1;
            int correction = -direction;
            foreach (int? lineNumber in lineNumbers) {
                ++start;
                if (startPosition <= lineNumber) {
                    if (startPosition == lineNumber) {
                        correction = 0;
                    }
                    break;
                }
            }
            int spanIdx = start + distance * direction + correction;
            if (spanIdx < 0) {
                // Going negative is not supported
                return;
            }
            int endPosition;
            if (spanIdx > lineNumbers.Count - 1) {
                // Increase grid
                endPosition = lastLineNumber + spanIdx - (lineNumbers.Count - 1);
            }
            else {
                endPosition = lineNumbers[spanIdx];
            }
            if (direction == 1) {
                child.SetProperty(endProperty, endPosition);
            }
            else {
                child.SetProperty(startProperty, endPosition);
            }
        }

        private static void SubstituteLinename(IDictionary<String, IList<int>> lineNumbersPerName, int property, IElement
             child, int lastLineNumber, String alternateLineNameSuffix) {
            Object propValue = child.GetProperty<Object>(property);
            if (!(propValue is String)) {
                // It means it's null or we processed it earlier
                return;
            }
            child.DeleteOwnProperty(property);
            Tuple2<int, String> parsedValue = ParseStringValue((String)propValue);
            int idx = parsedValue.GetFirst();
            String strValue = parsedValue.GetSecond();
            if (idx == 0 || strValue == null) {
                return;
            }
            IList<int> lineNumbers = lineNumbersPerName.Get(strValue);
            if (lineNumbers == null) {
                lineNumbers = lineNumbersPerName.Get(strValue + alternateLineNameSuffix);
            }
            if (lineNumbers == null) {
                return;
            }
            if (idx > lineNumbers.Count) {
                // Increase grid
                // We should also go to negative in a similar manner
                // but currently we don't support negative columns/rows
                child.SetProperty(property, lastLineNumber + idx - lineNumbers.Count);
                return;
            }
            if (Math.Abs(idx) > lineNumbers.Count) {
                // The case when it's too negative
                LOGGER.LogError(Html2PdfLogMessageConstant.ADDING_GRID_LINES_TO_THE_LEFT_OR_TOP_IS_NOT_SUPPORTED);
                return;
            }
            if (idx < 0) {
                idx = lineNumbers.Count + idx + 1;
            }
            child.SetProperty(property, lineNumbers[idx - 1]);
        }

        private static Tuple2<int, String> ParseStringValue(String strPropValue) {
            String[] propValues = iText.Commons.Utils.StringUtil.Split(strPropValue, "\\s+");
            int idx = 1;
            String strValue = null;
            if (propValues.Length == 1) {
                strValue = propValues[0];
            }
            else {
                if (propValues.Length == 2) {
                    // Here we have two options
                    // grid-row-start: 1 a and grid-row-start a 1
                    int? i0 = CssDimensionParsingUtils.ParseInteger(propValues[0]);
                    int? i1 = CssDimensionParsingUtils.ParseInteger(propValues[1]);
                    int? i = i0 != null ? i0 : i1;
                    if (i != null) {
                        idx = i.Value;
                    }
                    strValue = i0 != null ? propValues[1] : propValues[0];
                }
            }
            return new Tuple2<int, String>(idx, strValue);
        }

        private static TemplateValue ParseTemplateValue(String str, float emValue, float remValue) {
            UnitValue unit = CssDimensionParsingUtils.ParseLengthValueToPt(str, emValue, remValue);
            if (unit != null) {
                if (unit.IsPointValue()) {
                    return new PointValue(unit.GetValue());
                }
                else {
                    return new PercentValue(unit.GetValue());
                }
            }
            if (CommonCssConstants.MIN_CONTENT.Equals(str)) {
                return MinContentValue.VALUE;
            }
            if (CommonCssConstants.MAX_CONTENT.Equals(str)) {
                return MaxContentValue.VALUE;
            }
            if (CommonCssConstants.AUTO.Equals(str)) {
                return AutoValue.VALUE;
            }
            float? fr = CssDimensionParsingUtils.ParseFlex(str);
            if (fr != null) {
                return new FlexValue((float)fr);
            }
            if (DetermineFunction(str, CommonCssConstants.FIT_CONTENT)) {
                return ParseFitContent(str, emValue, remValue);
            }
            if (DetermineFunction(str, CssConstants.MINMAX)) {
                return ParseMinMax(str, emValue, remValue);
            }
            return null;
        }

        private static TemplateValue ParseTemplateValue(String str, float emValue, float remValue, IDictionary<String
            , IList<int>> lineNumbersPerName, int currentLine) {
            if (str == null) {
                return null;
            }
            if (str.StartsWith("[") && str.EndsWith("]")) {
                // It's a linename
                String strStripped = str.JSubstring(1, str.Length - 1);
                String[] linenames = iText.Commons.Utils.StringUtil.Split(strStripped.Trim(), "\\s+");
                foreach (String linename in linenames) {
                    if (!lineNumbersPerName.ContainsKey(linename)) {
                        lineNumbersPerName.Put(linename, new List<int>(1));
                    }
                    lineNumbersPerName.Get(linename).Add(currentLine);
                }
                return null;
            }
            if (DetermineFunction(str, CommonCssConstants.REPEAT)) {
                return ParseRepeat(str, emValue, remValue, lineNumbersPerName, currentLine);
            }
            return ParseTemplateValue(str, emValue, remValue);
        }

        private static FitContentValue ParseFitContent(String str, float emValue, float remValue) {
            UnitValue length = CssDimensionParsingUtils.ParseLengthValueToPt(str.JSubstring(CommonCssConstants.FIT_CONTENT
                .Length + 1, str.Length - 1), emValue, remValue);
            if (length == null) {
                return null;
            }
            return new FitContentValue(length);
        }

        private static bool DetermineFunction(String str, String function) {
            return str.StartsWith(function) && str.Length > function.Length + 2;
        }

        private static TemplateValue ParseMinMax(String str, float emValue, float remValue) {
            int parameterSeparator = str.IndexOf(',');
            if (parameterSeparator < 0) {
                return null;
            }
            TemplateValue min = ParseTemplateValue(str.JSubstring(CssConstants.MINMAX.Length + 1, parameterSeparator).
                Trim(), emValue, remValue);
            TemplateValue max = ParseTemplateValue(str.JSubstring(parameterSeparator + 1, str.Length - 1).Trim(), emValue
                , remValue);
            if (!(min is BreadthValue) || !(max is BreadthValue)) {
                return null;
            }
            return new MinMaxValue((BreadthValue)min, (BreadthValue)max);
        }

        private static TemplateValue ParseRepeat(String str, float emValue, float remValue, IDictionary<String, IList
            <int>> lineNumbersPerName, int currentLine) {
            IList<GridValue> repeatList = new List<GridValue>();
            int repeatTypeEndIndex = str.IndexOf(',');
            if (repeatTypeEndIndex < 0) {
                return null;
            }
            String repeatType = str.JSubstring(CommonCssConstants.REPEAT.Length + 1, repeatTypeEndIndex).Trim();
            int? repeatCount = CssDimensionParsingUtils.ParseInteger(repeatType);
            IList<String> repeatStr = CssUtils.ExtractShorthandProperties(str.JSubstring(repeatTypeEndIndex + 1, str.Length
                 - 1))[0];
            IDictionary<String, IList<int>> repeatLineNumbersPerName = new Dictionary<String, IList<int>>();
            foreach (String strValue in repeatStr) {
                TemplateValue value = ParseTemplateValue(strValue, emValue, remValue, repeatLineNumbersPerName, currentLine
                    );
                if (value is GridValue) {
                    repeatList.Add((GridValue)value);
                    ++currentLine;
                }
            }
            // Now multiply line numbers for repeats
            if (repeatCount != null && repeatCount.Value > 1) {
                foreach (IList<int> repeatLineNumbers in repeatLineNumbersPerName.Values) {
                    IList<int> extraLineNumbers = new List<int>();
                    for (int i = 1; i < repeatCount.Value; ++i) {
                        foreach (int? lineNumber in repeatLineNumbers) {
                            int extraLineNumber = lineNumber.Value + repeatList.Count * i;
                            if (!extraLineNumbers.Contains(extraLineNumber)) {
                                extraLineNumbers.Add(extraLineNumber);
                            }
                        }
                    }
                    repeatLineNumbers.RemoveAll(extraLineNumbers);
                    repeatLineNumbers.AddAll(extraLineNumbers);
                }
            }
            // Now merge with common lineNumbersPerName
            MapUtil.Merge(lineNumbersPerName, repeatLineNumbersPerName, (dest, source) => {
                dest.RemoveAll(source);
                dest.AddAll(source);
                return dest;
            }
            );
            if (repeatCount != null) {
                return new FixedRepeatValue(repeatCount.Value, repeatList);
            }
            else {
                if (CssConstants.AUTO_FILL.Equals(repeatType)) {
                    if (!lineNumbersPerName.IsEmpty()) {
                        LOGGER.LogWarning(Html2PdfLogMessageConstant.LINENAMES_ARE_NOT_SUPPORTED_WITHIN_AUTO_REPEAT);
                    }
                    return new AutoRepeatValue(false, repeatList);
                }
                else {
                    if (CssConstants.AUTO_FIT.Equals(repeatType)) {
                        if (!lineNumbersPerName.IsEmpty()) {
                            LOGGER.LogWarning(Html2PdfLogMessageConstant.LINENAMES_ARE_NOT_SUPPORTED_WITHIN_AUTO_REPEAT);
                        }
                        return new AutoRepeatValue(true, repeatList);
                    }
                }
            }
            return null;
        }

        private static void ApplyGridArea(IDictionary<String, String> cssProps, IPropertyContainer element) {
            if (cssProps.Get(CssConstants.GRID_AREA) == null) {
                return;
            }
            String gridArea = cssProps.Get(CssConstants.GRID_AREA);
            String[] gridAreaParts = iText.Commons.Utils.StringUtil.Split(gridArea, "/");
            for (int i = 0; i < gridAreaParts.Length; ++i) {
                String part = gridAreaParts[i].Trim();
                if (CommonCssConstants.AUTO.Equals(part)) {
                    // We override already set value if any
                    element.DeleteOwnProperty(propsMap.Get(i).Value);
                    continue;
                }
                // If it's an area name from grid-template-areas, it will go into GRID_ROW_START
                ApplyGridItemPlacement(part, element, propsMap.Get(i).Value, spansMap.Get(i).Value);
            }
        }

        private static void ApplyGridItemPlacement(String value, IPropertyContainer element, int property, int spanProperty
            ) {
            if (value == null) {
                return;
            }
            int? intValue = CssDimensionParsingUtils.ParseInteger(value);
            if (intValue != null) {
                // grid-row-start: 2
                element.SetProperty(property, intValue);
                return;
            }
            Matcher matcher = iText.Commons.Utils.Matcher.Match(SPAN_PLACEMENT, value.Trim());
            if (matcher.Matches()) {
                int? spanValue = CssDimensionParsingUtils.ParseInteger(matcher.Group(1));
                if (spanValue != null) {
                    // grid-row-start: span 2
                    element.SetProperty(spanProperty, spanValue);
                }
                else {
                    // grid-row-start: span linename or grid-row-start: span linename 2
                    // Later on we will convert linename to number or remove
                    element.SetProperty(spanProperty, matcher.Group(1).Trim());
                }
                return;
            }
            // grid-row-start: linename
            // Later on we will convert linename to number or remove
            element.SetProperty(property, value.Trim());
        }

        private static GridApplierUtil.NamedAreas ParseGridTemplateAreas(String templateAreas) {
            GridApplierUtil.NamedAreas res = new GridApplierUtil.NamedAreas();
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
            return res;
        }

        private sealed class NamedAreas {
            private const String DOT_PLACEHOLDER = ".";

            private const String AREA_START_SUFFIX = "-start";

            private const String AREA_END_SUFFIX = "-end";

            private readonly IDictionary<String, GridApplierUtil.Placement> areas = new Dictionary<String, GridApplierUtil.Placement
                >();

            private int rowsCount = 0;

            private int columnsCount = 0;

//\cond DO_NOT_DOCUMENT
            internal NamedAreas() {
            }
//\endcond

            // Empty constructor
            public void AddName(String name, int row, int column) {
                // Dot has a special meaning saying this area is not named and grid-template-areas doesn't work for it
                // Numbers are also not allowed
                if (DOT_PLACEHOLDER.Equals(name) || CssDimensionParsingUtils.ParseInteger(name) != null) {
                    return;
                }
                GridApplierUtil.Placement placement = areas.Get(name);
                if (placement == null) {
                    areas.Put(name, new GridApplierUtil.Placement(row, row, column, column));
                }
                else {
                    placement.IncreaseSpansTill(row, column);
                }
                rowsCount = Math.Max(rowsCount, row);
                columnsCount = Math.Max(columnsCount, column);
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

            public IDictionary<String, IList<int>> GetNamedRowNumbers() {
                IDictionary<String, IList<int>> namedNumbers = new Dictionary<String, IList<int>>(areas.Count * 2);
                foreach (KeyValuePair<String, GridApplierUtil.Placement> area in areas) {
                    namedNumbers.Put(area.Key + AREA_START_SUFFIX, new List<int>(JavaUtil.ArraysAsList(area.Value.GetRowStart(
                        ))));
                    namedNumbers.Put(area.Key + AREA_END_SUFFIX, new List<int>(JavaUtil.ArraysAsList(area.Value.GetRowEnd() + 
                        1)));
                }
                return namedNumbers;
            }

            public IDictionary<String, IList<int>> GetNamedColumnNumbers() {
                IDictionary<String, IList<int>> namedNumbers = new Dictionary<String, IList<int>>();
                foreach (KeyValuePair<String, GridApplierUtil.Placement> area in areas) {
                    namedNumbers.Put(area.Key + AREA_START_SUFFIX, new List<int>(JavaUtil.ArraysAsList(area.Value.GetColumnStart
                        ())));
                    namedNumbers.Put(area.Key + AREA_END_SUFFIX, new List<int>(JavaUtil.ArraysAsList(area.Value.GetColumnEnd()
                         + 1)));
                }
                return namedNumbers;
            }

            public int GetRowsCount() {
                return rowsCount;
            }

            public int GetColumnsCount() {
                return columnsCount;
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
                    valid = column == columnStart;
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
