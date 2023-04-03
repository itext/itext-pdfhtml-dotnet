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
using iText.Commons.Utils;
using iText.StyledXmlParser;

namespace iText.Html2pdf.Html {
    /// <summary>Class that bundles a series of attribute constants.</summary>
    public sealed class AttributeConstants : CommonAttributeConstants {
        /// <summary>
        /// Creates a new
        /// <see cref="AttributeConstants"/>
        /// instance.
        /// </summary>
        private AttributeConstants() {
        }

        /// <summary>The Constant ALIGN.</summary>
        public const String ALIGN = "align";

        /// <summary>The Constant ALT.</summary>
        public const String ALT = "alt";

        /// <summary>The Constant APPLICATION_NAME.</summary>
        public const String APPLICATION_NAME = "application-name";

        /// <summary>The Constant AUTHOR.</summary>
        public const String AUTHOR = "author";

        /// <summary>The Constant BGCOLOR.</summary>
        public const String BGCOLOR = "bgcolor";

        /// <summary>The Constant BORDER.</summary>
        public const String BORDER = "border";

        /// <summary>The Constant CLASS.</summary>
        public const String CELLPADDING = "cellpadding";

        /// <summary>The Constant CLASS.</summary>
        public const String CELLSPACING = "cellspacing";

        /// <summary>The Constant COLOR.</summary>
        public const String COLOR = "color";

        /// <summary>The Constant COLS.</summary>
        public const String COL = "col";

        /// <summary>The Constant COLS.</summary>
        public const String COLGROUP = "colgroup";

        /// <summary>The Constant COLS.</summary>
        public const String COLS = "cols";

        /// <summary>The Constant COLSPAN.</summary>
        public const String COLSPAN = "colspan";

        /// <summary>The Constant CONTENT.</summary>
        public const String CONTENT = "content";

        /// <summary>The Constant DATA</summary>
        public const String DATA = "data";

        /// <summary>The Constant DESCRIPTION.</summary>
        public const String DESCRIPTION = "description";

        /// <summary>The Constant DIR.</summary>
        public const String DIR = "dir";

        /// <summary>The Constant FACE.</summary>
        public const String FACE = "face";

        /// <summary>The Constant HEIGHT.</summary>
        public const String HEIGHT = "height";

        /// <summary>The Constant HREF.</summary>
        public const String HREF = "href";

        /// <summary>The Constant HSPACE.</summary>
        public const String HSPACE = "hspace";

        /// <summary>The Constant ID.</summary>
        public const String ID = "id";

        /// <summary>The Constant KEYWORDS.</summary>
        public const String KEYWORDS = "keywords";

        /// <summary>The Constant LABEL.</summary>
        public const String LABEL = "label";

        /// <summary>The Constant LANG.</summary>
        public const String LANG = "lang";

        /// <summary>The Constant MEDIA.</summary>
        public const String MEDIA = "media";

        /// <summary>The Constant MULTIPLE.</summary>
        public const String MULTIPLE = "multiple";

        /// <summary>The Constant NAME.</summary>
        public const String NAME = "name";

        /// <summary>The Constant NOSHADE.</summary>
        public const String NOSHADE = "noshade";

        /// <summary>The Constant NOWRAP.</summary>
        public const String NOWRAP = "nowrap";

        /// <summary>The Constant NUMBER.</summary>
        public const String NUMBER = "number";

        /// <summary>The Constant ROWS.</summary>
        public const String ROW = "row";

        /// <summary>The Constant ROWS.</summary>
        public const String ROWGROUP = "rowgroup";

        /// <summary>The Constant ROWS.</summary>
        public const String ROWS = "rows";

        /// <summary>The Constant ROWSPAN.</summary>
        public const String ROWSPAN = "rowspan";

        /// <summary>The Constant SCOPE.</summary>
        public const String SCOPE = "scope";

        /// <summary>The Constant SELECTED.</summary>
        public const String SELECTED = "selected";

        /// <summary>The Constant SIZE.</summary>
        public const String SIZE = "size";

        /// <summary>The Constant SPAN.</summary>
        public const String SPAN = "span";

        /// <summary>The Constant SRC.</summary>
        public const String SRC = "src";

        /// <summary>The Constant STYLE.</summary>
        public const String STYLE = "style";

        /// <summary>The Constant TYPE.</summary>
        public const String TYPE = "type";

        /// <summary>The Constant VALIGN.</summary>
        public const String VALIGN = "valign";

        /// <summary>The Constant VALUE.</summary>
        public const String VALUE = "value";

        /// <summary>The Constant VSPACE.</summary>
        public const String VSPACE = "vspace";

        /// <summary>The Constant WIDTH.</summary>
        public const String WIDTH = "width";

        /// <summary>The Constant TITLE.</summary>
        public const String TITLE = "title";

        // attribute values
        /// <summary>The Constant _1.</summary>
        public const String _1 = "1";

        /// <summary>The Constant A.</summary>
        public const String A = "A";

        /// <summary>The Constant a.</summary>
        public const String a = "a";

        /// <summary>The Constant BOTTOM.</summary>
        public const String BOTTOM = "bottom";

        /// <summary>The Constant BUTTON.</summary>
        public const String BUTTON = "button";

        /// <summary>The Constant CENTER.</summary>
        public const String CENTER = "center";

        /// <summary>The Constant CHECKBOX.</summary>
        public const String CHECKBOX = "checkbox";

        /// <summary>The Constant CHECKED.</summary>
        public const String CHECKED = "checked";

        /// <summary>The Constant DATE.</summary>
        public const String DATE = "date";

        /// <summary>The Constant DATETIME.</summary>
        public const String DATETIME = "datetime";

        /// <summary>The Constant DATETIME_LOCAL.</summary>
        public const String DATETIME_LOCAL = "datetime_local";

        /// <summary>The Constant EMAIL.</summary>
        public const String EMAIL = "email";

        /// <summary>The Constant FILE.</summary>
        public const String FILE = "file";

        /// <summary>The Constant HIDDEN.</summary>
        public const String HIDDEN = "hidden";

        /// <summary>The Constant I.</summary>
        public const String I = "I";

        /// <summary>The Constant i.</summary>
        public const String i = "i";

        /// <summary>The Constant IMAGE.</summary>
        public const String IMAGE = "image";

        /// <summary>The Constant LEFT.</summary>
        public const String LEFT = "left";

        /// <summary>The Constant LTR.</summary>
        public const String LTR = "ltr";

        /// <summary>The Constant MIDDLE.</summary>
        public const String MIDDLE = "middle";

        /// <summary>The Constant MONTH.</summary>
        public const String MONTH = "month";

        /// <summary>The Constant PASSWORD.</summary>
        public const String PASSWORD = "password";

        /// <summary>The Constant PLACEHOLDER.</summary>
        public const String PLACEHOLDER = "placeholder";

        /// <summary>The Constant RADIO.</summary>
        public const String RADIO = "radio";

        /// <summary>The Constant RANGE.</summary>
        public const String RANGE = "range";

        /// <summary>The Constant RESET.</summary>
        public const String RESET = "reset";

        /// <summary>The Constant RIGHT.</summary>
        public const String RIGHT = "right";

        /// <summary>The Constant RTL.</summary>
        public const String RTL = "rtl";

        /// <summary>The Constant SEARCH.</summary>
        public const String SEARCH = "search";

        /// <summary>The Constant START</summary>
        public const String START = "start";

        /// <summary>The Constant SUBMIT.</summary>
        public const String SUBMIT = "submit";

        /// <summary>The Constant TEL.</summary>
        public const String TEL = "tel";

        /// <summary>The Constant TEXT.</summary>
        public const String TEXT = "text";

        /// <summary>The Constant TIME.</summary>
        public const String TIME = "time";

        /// <summary>The Constant TOP.</summary>
        public const String TOP = "top";

        /// <summary>The Constant URL</summary>
        public const String URL = "url";

        /// <summary>The Constant WEEK</summary>
        public const String WEEK = "week";

        /// <summary>The Constant INPUT_TYPE_VALUES.</summary>
        public static readonly ICollection<String> INPUT_TYPE_VALUES = JavaCollectionsUtil.UnmodifiableSet(new HashSet
            <String>(JavaUtil.ArraysAsList(new String[] { BUTTON, CHECKBOX, COLOR, DATE, DATETIME, DATETIME_LOCAL, 
            EMAIL, FILE, HIDDEN, IMAGE, MONTH, NUMBER, PASSWORD, RADIO, RANGE, RESET, SEARCH, SUBMIT, TEL, TEXT, TIME
            , URL, WEEK })));

        // iText custom attributes
        public sealed class ObjectTypes {
            public const String SVGIMAGE = "image/svg+xml";
        }
    }
}
