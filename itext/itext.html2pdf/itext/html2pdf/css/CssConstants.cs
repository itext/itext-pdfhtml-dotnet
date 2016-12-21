/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;

namespace iText.Html2pdf.Css {
    public class CssConstants {
        public const String ALIGN = "align";

        public const String BACKGROUND = "background";

        public const String BACKGROUND_ATTACHMENT = "background-attachment";

        public const String BACKGROUND_BLEND_MODE = "background-blend-mode";

        public const String BACKGROUND_CLIP = "background-clip";

        public const String BACKGROUND_COLOR = "background-color";

        public const String BACKGROUND_IMAGE = "background-image";

        public const String BACKGROUND_ORIGIN = "background-origin";

        public const String BACKGROUND_POSITION = "background-position";

        public const String BACKGROUND_REPEAT = "background-repeat";

        public const String BACKGROUND_SIZE = "background-size";

        public const String BORDER = "border";

        public const String BORDER_BOTTOM = "border-bottom";

        public const String BORDER_BOTTOM_COLOR = "border-bottom-color";

        public const String BORDER_BOTTOM_STYLE = "border-bottom-style";

        public const String BORDER_BOTTOM_WIDTH = "border-bottom-width";

        public const String BORDER_COLLAPSE = "border-collapse";

        public const String BORDER_COLOR = "border-color";

        public const String BORDER_LEFT = "border-left";

        public const String BORDER_LEFT_COLOR = "border-left-color";

        public const String BORDER_LEFT_STYLE = "border-left-style";

        public const String BORDER_LEFT_WIDTH = "border-left-width";

        public const String BORDER_RIGHT = "border-right";

        public const String BORDER_RIGHT_COLOR = "border-right-color";

        public const String BORDER_RIGHT_STYLE = "border-right-style";

        public const String BORDER_RIGHT_WIDTH = "border-right-width";

        public const String BORDER_SPACING = "border-spacing";

        public const String BORDER_STYLE = "border-style";

        public const String BORDER_TOP = "border-top";

        public const String BORDER_TOP_COLOR = "border-top-color";

        public const String BORDER_TOP_STYLE = "border-top-style";

        public const String BORDER_TOP_WIDTH = "border-top-width";

        public const String BORDER_WIDTH = "border-width";

        public const String CAPTION_SIDE = "caption-side";

        public const String COLOR = "color";

        public const String COLSPAN = "colspan";

        public const String DIRECTION = "direction";

        public const String DISPLAY = "display";

        public const String EMPTY_CELLS = "empty-cells";

        public const String FLOAT = "float";

        public const String FONT = "font";

        public const String FONT_FAMILY = "font-family";

        public const String FONT_FEATURE_SETTINGS = "font-feature-settings";

        public const String FONT_KERNING = "font-kerning";

        public const String FONT_LANGUAGE_OVERRIDE = "font-language-override";

        public const String FONT_SIZE = "font-size";

        public const String FONT_SIZE_ADJUST = "font-size-adjust";

        public const String FONT_STRETCH = "font-stretch";

        public const String FONT_STYLE = "font-style";

        public const String FONT_SYNTHESIS = "font-synthesis";

        public const String FONT_VARIANT = "font-variant";

        public const String FONT_VARIANT_ALTERNATES = "font-variant-alternates";

        public const String FONT_VARIANT_CAPS = "font-variant-caps";

        public const String FONT_VARIANT_EAST_ASIAN = "font-variant-east-asian";

        public const String FONT_VARIANT_LIGATURES = "font-variant-ligatures";

        public const String FONT_VARIANT_NUMERIC = "font-variant-numeric";

        public const String FONT_VARIANT_POSITION = "font-variant-position";

        public const String FONT_WEIGHT = "font-weight";

        public const String HANGING_PUNCTUATION = "hanging-punctuation";

        public const String HEIGHT = "height";

        public const String HSPACE = "hspace";

        public const String HYPHENS = "hyphens";

        public const String LETTER_SPACING = "letter-spacing";

        public const String LINE_HEIGHT = "line-height";

        public const String LIST_STYLE = "list-style";

        public const String LIST_STYLE_IMAGE = "list-style-image";

        public const String LIST_STYLE_POSITION = "list-style-position";

        public const String LIST_STYLE_TYPE = "list-style-type";

        public const String MARGIN = "margin";

        public const String MARGIN_BOTTOM = "margin-bottom";

        public const String MARGIN_LEFT = "margin-left";

        public const String MARGIN_RIGHT = "margin-right";

        public const String MARGIN_TOP = "margin-top";

        public const String MAX_HEIGHT = "max-height";

        public const String MIN_HEIGHT = "min-height";

        public const String OPACITY = "opacity";

        public const String OUTLINE = "outline";

        public const String OUTLINE_COLOR = "outline-color";

        public const String OUTLINE_STYLE = "outline-style";

        public const String OUTLINE_WIDTH = "outline-width";

        public const String OVERFLOW_WRAP = "overflow-wrap";

        public const String PADDING = "padding";

        public const String PADDING_BOTTOM = "padding-bottom";

        public const String PADDING_LEFT = "padding-left";

        public const String PADDING_RIGHT = "padding-right";

        public const String PADDING_TOP = "padding-top";

        public const String QUOTES = "quotes";

        public const String ROWSPAN = "rowspan";

        public const String STYLE = "style";

        public const String TAB_SIZE = "tab-size";

        public const String TEXT_ALIGN = "text-align";

        public const String TEXT_ALIGN_LAST = "text-align-last";

        public const String TEXT_COMBINE_UPRIGHT = "text-combine-upright";

        public const String TEXT_DECORATION = "text-decoration";

        public const String TEXT_INDENT = "text-indent";

        public const String TEXT_JUSTIFY = "text-justify";

        public const String TEXT_ORIENTATION = "text-orientation";

        public const String TEXT_SHADOW = "text-shadow";

        public const String TEXT_TRANSFORM = "text-transform";

        public const String TEXT_UNDERLINE_POSITION = "text-underline-position";

        public const String UNICODE_BIDI = "unicode-bidi";

        public const String VERTICAL_ALIGN = "vertical-align";

        public const String VISIBILITY = "visibility";

        public const String VSPACE = "vspace";

        public const String WHITE_SPACE = "white-space";

        public const String WIDTH = "width";

        public const String WORDWRAP = "word-wrap";

        public const String WORD_BREAK = "word-break";

        public const String WORD_SPACING = "word-spacing";

        public const String WRITING_MODE = "writing-mode";

        public const String ARMENIAN = "armenian";

        public const String AUTO = "auto";

        public const String BLINK = "blink";

        public const String BOLD = "bold";

        public const String BOLDER = "bolder";

        public const String BORDER_BOX = "border-box";

        public const String BOTTOM = "bottom";

        public const String CAPITALIZE = "capitalize";

        public const String CAPTION = "caption";

        public const String CENTER = "center";

        public const String CIRCLE = "circle";

        public const String CJK_IDEOGRAPHIC = "cjk-ideographic";

        public const String CONTAIN = "contain";

        public const String CONTENT_BOX = "content-box";

        public const String COVER = "cover";

        public const String DASHED = "dashed";

        public const String DECIMAL = "decimal";

        public const String DECIMAL_LEADING_ZERO = "decimal-leading-zero";

        public const String DISC = "disc";

        public const String DOTTED = "dotted";

        public const String DOUBLE = "double";

        public const String FIXED = "fixed";

        public const String GEORGIAN = "georgian";

        public const String GROOVE = "groove";

        public const String HEBREW = "hebrew";

        public const String HIDDEN = "hidden";

        public const String HIRAGANA = "hiragana";

        public const String HIRAGANA_IROHA = "hiragana-iroha";

        public const String ICON = "icon";

        public const String INHERIT = "inherit";

        public const String INITIAL = "initial";

        public const String INSET = "inset";

        public const String INSIDE = "inside";

        public const String INVERT = "invert";

        public const String ITALIC = "italic";

        public const String LARGE = "large";

        public const String LARGER = "larger";

        public const String LEFT = "left";

        public const String LIGHTER = "lighter";

        public const String LINE_THROUGH = "line-through";

        public const String LOCAL = "local";

        public const String LOWER_ALPHA = "lower-alpha";

        public const String LOWER_GREEK = "lower-greek";

        public const String LOWER_LATIN = "lower-latin";

        public const String LOWER_ROMAN = "lower-roman";

        public const String LOWERCASE = "lowercase";

        public const String LTR = "ltr";

        public const String MANUAL = "manual";

        public const String MEDIUM = "medium";

        public const String MENU = "menu";

        public const String MESSAGE_BOX = "message-box";

        public const String MIDDLE = "middle";

        public const String NO_REPEAT = "no-repeat";

        public const String NONE = "none";

        public const String NORMAL = "normal";

        public const String OBLIQUE = "oblique";

        public const String OUTSIDE = "outside";

        public const String OUTSET = "outset";

        public const String OVERLINE = "overline";

        public const String PADDING_BOX = "padding-box";

        public const String PRE = "pre";

        public const String PRE_LINE = "pre-line";

        public const String PRE_WRAP = "pre-wrap";

        public const String REPEAT = "repeat";

        public const String REPEAT_X = "repeat-x";

        public const String REPEAT_Y = "repeat-y";

        public const String RIDGE = "ridge";

        public const String RIGHT = "right";

        public const String RTL = "rtl";

        public const String SCROLL = "scroll";

        public const String SMALL = "small";

        public const String SMALL_CAPS = "small-caps";

        public const String SMALL_CAPTION = "small-caption";

        public const String SMALLER = "smaller";

        public const String SOLID = "solid";

        public const String SQUARE = "square";

        public const String START = "start";

        public const String STATUS_BAR = "status-bar";

        public const String SUB = "sub";

        public const String SUPER = "super";

        public const String TEXT_BOTTOM = "text-bottom";

        public const String TEXT_TOP = "text-top";

        public const String THICK = "thick";

        public const String THIN = "thin";

        public const String TOP = "top";

        public const String TRANSPARENT = "transparent";

        public const String UNDERLINE = "underline";

        public const String UPPER_ALPHA = "upper-alpha";

        public const String UPPER_LATIN = "upper-latin";

        public const String UPPER_ROMAN = "upper-roman";

        public const String UPPERCASE = "uppercase";

        public const String X_LARGE = "x-large";

        public const String X_SMALL = "x-small";

        public const String XX_LARGE = "xx-large";

        public const String XX_SMALL = "xx-small";

        public static readonly ICollection<String> BACKGROUND_SIZE_VALUES = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (AUTO, COVER, CONTAIN));

        public static readonly ICollection<String> BACKGROUND_ORIGIN_OR_CLIP_VALUES = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (PADDING_BOX, BORDER_BOX, CONTENT_BOX));

        public static readonly ICollection<String> BACKGROUND_REPEAT_VALUES = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (REPEAT, NO_REPEAT, REPEAT_X, REPEAT_Y));

        public static readonly ICollection<String> BACKGROUND_ATTACHMENT_VALUES = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (FIXED, SCROLL, LOCAL));

        public static readonly ICollection<String> BACKGROUND_POSITION_VALUES = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (LEFT, CENTER, BOTTOM, TOP, RIGHT));

        public static readonly ICollection<String> BORDER_WIDTH_VALUES = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (new String[] { THIN, MEDIUM, THICK }));

        public static readonly ICollection<String> BORDER_STYLE_VALUES = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (new String[] { NONE, HIDDEN, DOTTED, DASHED, SOLID, DOUBLE, GROOVE, RIDGE, INSET, OUTSET }));

        public static readonly ICollection<String> FONT_ABSOLUTE_SIZE_KEYWORDS = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
            (CssConstants.MEDIUM, CssConstants.XX_SMALL, CssConstants.X_SMALL, CssConstants.SMALL, CssConstants.LARGE
            , CssConstants.X_LARGE, CssConstants.XX_LARGE));

        public const String CM = "cm";

        public const String EM = "em";

        public const String EX = "ex";

        public const String IN = "in";

        public const String MM = "mm";

        public const String PC = "pc";

        public const String PERCENTAGE = "%";

        public const String PT = "pt";

        public const String PX = "px";

        public const String Q = "q";

        public const String DPI = "dpi";

        public const String DPCM = "dpcm";

        public const String DPPX = "dppx";
        // properties
        // property values
        // properties possible values
        // units of measurement
        // units of resolution
    }
}
