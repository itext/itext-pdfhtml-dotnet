/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
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

        /// <summary>The Constant INLINE.</summary>
        public const String INLINE = "inline";

        /// <summary>The Constant INLINE_BLOCK.</summary>
        public const String INLINE_BLOCK = "inline-block";

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

        /// <summary>The Constant TABLE_CELL.</summary>
        public const String TABLE_CELL = "table-cell";

        /// <summary>The Constant TABLE_ROW.</summary>
        public const String TABLE_ROW = "table-row";

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
