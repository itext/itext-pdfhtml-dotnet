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
using iText.Html2pdf.Css;
using iText.IO.Log;

namespace iText.Html2pdf.Css.Resolve {
    public class CssDefaults {
        private static readonly IDictionary<String, String> defaultValues = new Dictionary<String, String>();

        static CssDefaults() {
            // TODO make internal?
            defaultValues[CssConstants.COLOR] = "black";
            // TODO not specified, varies from browser to browser
            defaultValues[CssConstants.OPACITY] = "1";
            defaultValues[CssConstants.BACKGROUND_ATTACHMENT] = CssConstants.SCROLL;
            defaultValues[CssConstants.BACKGROUND_BLEND_MODE] = CssConstants.NORMAL;
            defaultValues[CssConstants.BACKGROUND_COLOR] = CssConstants.TRANSPARENT;
            defaultValues[CssConstants.BACKGROUND_IMAGE] = CssConstants.NONE;
            defaultValues[CssConstants.BACKGROUND_POSITION] = "0% 0%";
            defaultValues[CssConstants.BACKGROUND_REPEAT] = CssConstants.REPEAT;
            defaultValues[CssConstants.BACKGROUND_CLIP] = CssConstants.BORDER_BOX;
            defaultValues[CssConstants.BACKGROUND_ORIGIN] = CssConstants.PADDING_BOX;
            defaultValues[CssConstants.BACKGROUND_SIZE] = CssConstants.AUTO;
            defaultValues[CssConstants.BORDER_BOTTOM_COLOR] = "black";
            // TODO specified as " The current color of the element ", might be better to put null here?
            defaultValues[CssConstants.BORDER_LEFT_COLOR] = "black";
            defaultValues[CssConstants.BORDER_RIGHT_COLOR] = "black";
            defaultValues[CssConstants.BORDER_TOP_COLOR] = "black";
            defaultValues[CssConstants.BORDER_BOTTOM_STYLE] = CssConstants.NONE;
            defaultValues[CssConstants.BORDER_LEFT_STYLE] = CssConstants.NONE;
            defaultValues[CssConstants.BORDER_RIGHT_STYLE] = CssConstants.NONE;
            defaultValues[CssConstants.BORDER_TOP_STYLE] = CssConstants.NONE;
            defaultValues[CssConstants.BORDER_BOTTOM_WIDTH] = CssConstants.MEDIUM;
            defaultValues[CssConstants.BORDER_LEFT_WIDTH] = CssConstants.MEDIUM;
            defaultValues[CssConstants.BORDER_RIGHT_WIDTH] = CssConstants.MEDIUM;
            defaultValues[CssConstants.BORDER_TOP_WIDTH] = CssConstants.MEDIUM;
            defaultValues[CssConstants.BORDER_WIDTH] = CssConstants.MEDIUM;
            defaultValues[CssConstants.FONT_WEIGHT] = CssConstants.NORMAL;
            defaultValues[CssConstants.FONT_SIZE] = CssConstants.MEDIUM;
            defaultValues[CssConstants.FONT_STYLE] = CssConstants.NORMAL;
            defaultValues[CssConstants.FONT_VARIANT] = CssConstants.NORMAL;
            defaultValues[CssConstants.LINE_HEIGHT] = CssConstants.NORMAL;
            defaultValues[CssConstants.MARGIN_BOTTOM] = "0";
            defaultValues[CssConstants.MARGIN_LEFT] = "0";
            defaultValues[CssConstants.MARGIN_RIGHT] = "0";
            defaultValues[CssConstants.MARGIN_TOP] = "0";
            defaultValues[CssConstants.OUTLINE_COLOR] = CssConstants.INVERT;
            defaultValues[CssConstants.OUTLINE_STYLE] = CssConstants.NONE;
            defaultValues[CssConstants.PADDING_BOTTOM] = "0";
            defaultValues[CssConstants.PADDING_LEFT] = "0";
            defaultValues[CssConstants.PADDING_RIGHT] = "0";
            defaultValues[CssConstants.PADDING_TOP] = "0";
            defaultValues[CssConstants.LIST_STYLE_TYPE] = CssConstants.DISC;
            defaultValues[CssConstants.LIST_STYLE_IMAGE] = CssConstants.NONE;
            defaultValues[CssConstants.LIST_STYLE_POSITION] = CssConstants.OUTSIDE;
            defaultValues[CssConstants.TEXT_ALIGN] = CssConstants.START;
            defaultValues[CssConstants.TEXT_DECORATION] = CssConstants.NONE;
            defaultValues[CssConstants.WHITE_SPACE] = CssConstants.NORMAL;
            defaultValues[CssConstants.TEXT_TRANSFORM] = CssConstants.NONE;
            defaultValues[CssConstants.TEXT_DECORATION] = CssConstants.NONE;
            defaultValues[CssConstants.HYPHENS] = CssConstants.MANUAL;
        }

        // TODO not complete
        public static String GetDefaultValue(String property) {
            String defaultVal = defaultValues.Get(property);
            if (defaultVal == null) {
                ILogger logger = LoggerFactory.GetLogger(typeof(CssDefaults));
                logger.Error(String.Format("Default value of the css property \"{0}\" is unknown.", property));
            }
            return defaultVal;
        }
    }
}
