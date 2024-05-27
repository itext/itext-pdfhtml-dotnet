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
using iText.Commons.Utils;
using iText.StyledXmlParser.Css;

namespace iText.Html2pdf.Css {
    public class CssConstants : CommonCssConstants {
        /// <summary>The Constant BLEED.</summary>
        public const String BLEED = "bleed";

        /// <summary>The Constant BOTH.</summary>
        public const String BOTH = "both";

        /// <summary>The Constant BOX_SIZING.</summary>
        public const String BOX_SIZING = "box-sizing";

        /// <summary>The Constant CLEAR.</summary>
        public const String CLEAR = "clear";

        /// <summary>The Constant CONTENT.</summary>
        public const String CONTENT = "content";

        /// <summary>The Constant COUNTER_INCREMENT.</summary>
        public const String COUNTER_INCREMENT = "counter-increment";

        /// <summary>The Constant COUNTER_RESET.</summary>
        public const String COUNTER_RESET = "counter-reset";

        /// <summary>The Constant DISPLAY.</summary>
        public const String DISPLAY = "display";

        /// <summary>The Constant LIST_ITEM.</summary>
        public const String LIST_ITEM = "list-item";

        /// <summary>The Constant MARKS.</summary>
        public const String MARKS = "marks";

        /// <summary>The Constant MAX_HEIGHT.</summary>
        public const String MAX_HEIGHT = "max-height";

        /// <summary>The Constant MAX_WIDTH.</summary>
        public const String MAX_WIDTH = "max-width";

        /// <summary>The Constant MIN_WIDTH.</summary>
        public const String MIN_WIDTH = "min-width";

        /// <summary>The Constant OBJECT_FIT.</summary>
        public const String OBJECT_FIT = "object-fit";

        /// <summary>The Constant OUTLINE_OFFSET.</summary>
        public const String OUTLINE_OFFSET = "outline-offset";

        /// <summary>The Constant OVERFLOW_X.</summary>
        public const String OVERFLOW_X = "overflow-x";

        /// <summary>The Constant OVERFLOW_Y.</summary>
        public const String OVERFLOW_Y = "overflow-y";

        /// <summary>The Constant PADDING_INLINE_START.</summary>
        public const String PADDING_INLINE_START = "padding-inline-start";

        /// <summary>The Constant PLACEHOLDER.</summary>
        public const String PLACEHOLDER = "placeholder";

        /// <summary>The Constant SIZE.</summary>
        public const String SIZE = "size";

        /// <summary>The Constant STYLE.</summary>
        public const String STYLE = "style";

        /// <summary>The Constant TABLE_LAYOUT.</summary>
        public const String TABLE_LAYOUT = "table-layout";

        /// <summary>The Constant VERTICAL_ALIGN.</summary>
        public const String VERTICAL_ALIGN = "vertical-align";

        // property values
        /// <summary>The Constant ABSOLUTE.</summary>
        public const String ABSOLUTE = "absolute";

        /// <summary>The Constant BLINK.</summary>
        public const String BLINK = "blink";

        /// <summary>The Constant BLOCK.</summary>
        public const String BLOCK = "block";

        /// <summary>The Constant CAPITALIZE.</summary>
        public const String CAPITALIZE = "capitalize";

        /// <summary>The Constant CONTENTS.</summary>
        public const String CONTENTS = "contents";

        /// <summary>The Constant COLLAPSE.</summary>
        public const String COLLAPSE = "collapse";

        /// <summary>The Constant CROP.</summary>
        public const String CROP = "crop";

        /// <summary>The Constant CROSS.</summary>
        public const String CROSS = "cross";

        /// <summary>The Constant FILL.</summary>
        public const String FILL = "fill";

        /// <summary>The Constant FIRST.</summary>
        public const String FIRST = "first";

        /// <summary>The Constant FIRST_EXCEPT.</summary>
        public const String FIRST_EXCEPT = "first-except";

        /// <summary>The Constant GRID.</summary>
        public const String GRID = "grid";

        /// <summary>The Constant GRID_COLUMN_END.</summary>
        public const String GRID_COLUMN_END = "grid-column-end";

        /// <summary>The Constant GRID_COLUMN_START.</summary>
        public const String GRID_COLUMN_START = "grid-column-start";

        /// <summary>The Constant GRID_ROW_END.</summary>
        public const String GRID_ROW_END = "grid-row-end";

        /// <summary>The Constant GRID_ROW_START.</summary>
        public const String GRID_ROW_START = "grid-row-start";

        /// <summary>The Constant GRID_TEMPLATE_AREAS.</summary>
        public const String GRID_TEMPLATE_AREAS = "grid-template-areas";

        /// <summary>The Constant GRID_TEMPLATE_COLUMNS.</summary>
        public const String GRID_TEMPLATE_COLUMNS = "grid-template-columns";

        /// <summary>The Constant GRID_TEMPLATE_ROWS.</summary>
        public const String GRID_TEMPLATE_ROWS = "grid-template-rows";

        /// <summary>The Constant GRID_AUTO_ROWS.</summary>
        public const String GRID_AUTO_ROWS = "grid-auto-rows";

        /// <summary>The Constant GRID_AUTO_COLUMNS.</summary>
        public const String GRID_AUTO_COLUMNS = "grid-auto-columns";

        /// <summary>The Constant GRID_AUTO_FLOW.</summary>
        public const String GRID_AUTO_FLOW = "grid-auto-flow";

        /// <summary>The Constant GRID_AREA.</summary>
        public const String GRID_AREA = "grid-area";

        /// <summary>The Constant DENSE.</summary>
        public const String DENSE = "dense";

        /// <summary>The Constant INLINE.</summary>
        public const String INLINE = "inline";

        /// <summary>The Constant INLINE_BLOCK.</summary>
        public const String INLINE_BLOCK = "inline-block";

        /// <summary>The Constant INLINE_FLEX.</summary>
        public const String INLINE_FLEX = "INLINE_FLEX";

        /// <summary>The Constant INLINE_GRID.</summary>
        public const String INLINE_GRID = "INLINE_GRID";

        /// <summary>The Constant INLINE_TABLE.</summary>
        public const String INLINE_TABLE = "inline-table";

        /// <summary>The Constant INVERT.</summary>
        public const String INVERT = "invert";

        /// <summary>The Constant JUSTIFY.</summary>
        public const String JUSTIFY = "justify";

        /// <summary>The Constant LANDSCAPE.</summary>
        public const String LANDSCAPE = "landscape";

        /// <summary>The Constant LAST.</summary>
        public const String LAST = "last";

        /// <summary>The Constant LINE_THROUGH.</summary>
        public const String LINE_THROUGH = "line-through";

        /// <summary>The Constant LOWERCASE.</summary>
        public const String LOWERCASE = "lowercase";

        /// <summary>The Constant LTR.</summary>
        public const String LTR = "ltr";

        /// <summary>The Constant MIDDLE.</summary>
        public const String MIDDLE = "middle";

        /// <summary>The Constant NOWRAP.</summary>
        public const String NOWRAP = "nowrap";

        /// <summary>The Constant OVERLINE.</summary>
        public const String OVERLINE = "overline";

        /// <summary>The Constant PAGE.</summary>
        public const String PAGE = "page";

        /// <summary>The Constant PAGES.</summary>
        public const String PAGES = "pages";

        /// <summary>The Constant PORTRAIT.</summary>
        public const String PORTRAIT = "portrait";

        /// <summary>The Constant PRE.</summary>
        public const String PRE = "pre";

        /// <summary>The Constant PRE_LINE.</summary>
        public const String PRE_LINE = "pre-line";

        /// <summary>The Constant PRE_WRAP.</summary>
        public const String PRE_WRAP = "pre-wrap";

        /// <summary>The Constant RELATIVE.</summary>
        public const String RELATIVE = "relative";

        /// <summary>The Constant RUN_IN.</summary>
        public const String RUN_IN = "run-in";

        /// <summary>The Constant RTL.</summary>
        public const String RTL = "rtl";

        /// <summary>The Constant SCALE_DOWN.</summary>
        public const String SCALE_DOWN = "scale-down";

        /// <summary>The Constant SEPARATE.</summary>
        public const String SEPARATE = "separate";

        /// <summary>The Constant SUB.</summary>
        public const String SUB = "sub";

        /// <summary>The Constant SUPER.</summary>
        public const String SUPER = "super";

        /// <summary>The Constant TABLE.</summary>
        public const String TABLE = "table";

        /// <summary>The Constant TABLE_CAPTION.</summary>
        public const String TABLE_CAPTION = "table-caption";

        /// <summary>The Constant TABLE_CELL.</summary>
        public const String TABLE_CELL = "table-cell";

        /// <summary>The Constant TABLE_COLUMN.</summary>
        public const String TABLE_COLUMN = "table-column";

        /// <summary>The Constant TABLE_COLUMN_GROUP.</summary>
        public const String TABLE_COLUMN_GROUP = "table-column-group";

        /// <summary>The Constant TABLE_FOOTER_GROUP.</summary>
        public const String TABLE_FOOTER_GROUP = "table-footer-group";

        /// <summary>The Constant TABLE_HEADER_GROUP.</summary>
        public const String TABLE_HEADER_GROUP = "table-header-group";

        /// <summary>The Constant TABLE_ROW.</summary>
        public const String TABLE_ROW = "table-row";

        /// <summary>The Constant TABLE_ROW_GROUP.</summary>
        public const String TABLE_ROW_GROUP = "table-row-group";

        /// <summary>The Constant TEXT_BOTTOM.</summary>
        public const String TEXT_BOTTOM = "text-bottom";

        /// <summary>The Constant TEXT_TOP.</summary>
        public const String TEXT_TOP = "text-top";

        /// <summary>The Constant UNDERLINE.</summary>
        public const String UNDERLINE = "underline";

        /// <summary>The Constant UPPERCASE.</summary>
        public const String UPPERCASE = "uppercase";

        // properties possible values
        /// <summary>The Constant OVERFLOW_VALUES.</summary>
        public static readonly ICollection<String> OVERFLOW_VALUES = JavaCollectionsUtil.UnmodifiableSet(new HashSet
            <String>(JavaUtil.ArraysAsList(CommonCssConstants.VISIBLE, HIDDEN, SCROLL, AUTO)));

        // pseudo-elements
        /// <summary>The Constant AFTER.</summary>
        public const String AFTER = "after";

        /// <summary>The Constant BEFORE.</summary>
        public const String BEFORE = "before";

        /// <summary>The Constant FIRST_LETTER.</summary>
        public const String FIRST_LETTER = "first-letter";

        /// <summary>The Constant FIRST_LINE.</summary>
        public const String FIRST_LINE = "first-line";

        /// <summary>The Constant SELECTION.</summary>
        public const String SELECTION = "selection";

        // Functions
        /// <summary>The Constant COUNTER.</summary>
        public const String COUNTER = "counter";

        /// <summary>The Constant COUNTERS.</summary>
        public const String COUNTERS = "counters";

        /// <summary>The Constant RUNNING.</summary>
        public const String ELEMENT = "element";

        /// <summary>The Constant RUNNING.</summary>
        public const String RUNNING = "running";

        /// <summary>The Constant TARGET_COUNTER.</summary>
        public const String TARGET_COUNTER = "target-counter";

        /// <summary>The Constant TARGET_COUNTERS.</summary>
        public const String TARGET_COUNTERS = "target-counters";

        // units of resolution
        /// <summary>The Constant DPI.</summary>
        public const String DPI = "dpi";
    }
}
