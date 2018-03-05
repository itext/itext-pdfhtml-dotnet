/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using Common.Logging;
using iText.Html2pdf.Css;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve {
    /// <summary>Helper class that allows you to get the default values of CSS properties.</summary>
    public class CssDefaults {
        /// <summary>A map with properties and their default values.</summary>
        private static readonly IDictionary<String, String> defaultValues = new Dictionary<String, String>();

        static CssDefaults() {
            defaultValues.Put(CssConstants.COLOR, "black");
            // not specified, varies from browser to browser
            defaultValues.Put(CssConstants.OPACITY, "1");
            defaultValues.Put(CssConstants.BACKGROUND_ATTACHMENT, CssConstants.SCROLL);
            defaultValues.Put(CssConstants.BACKGROUND_BLEND_MODE, CssConstants.NORMAL);
            defaultValues.Put(CssConstants.BACKGROUND_COLOR, CssConstants.TRANSPARENT);
            defaultValues.Put(CssConstants.BACKGROUND_IMAGE, CssConstants.NONE);
            defaultValues.Put(CssConstants.BACKGROUND_POSITION, "0% 0%");
            defaultValues.Put(CssConstants.BACKGROUND_REPEAT, CssConstants.REPEAT);
            defaultValues.Put(CssConstants.BACKGROUND_CLIP, CssConstants.BORDER_BOX);
            defaultValues.Put(CssConstants.BACKGROUND_ORIGIN, CssConstants.PADDING_BOX);
            defaultValues.Put(CssConstants.BACKGROUND_SIZE, CssConstants.AUTO);
            defaultValues.Put(CssConstants.BORDER_BOTTOM_COLOR, CssConstants.CURRENTCOLOR);
            defaultValues.Put(CssConstants.BORDER_LEFT_COLOR, CssConstants.CURRENTCOLOR);
            defaultValues.Put(CssConstants.BORDER_RIGHT_COLOR, CssConstants.CURRENTCOLOR);
            defaultValues.Put(CssConstants.BORDER_TOP_COLOR, CssConstants.CURRENTCOLOR);
            defaultValues.Put(CssConstants.BORDER_BOTTOM_STYLE, CssConstants.NONE);
            defaultValues.Put(CssConstants.BORDER_LEFT_STYLE, CssConstants.NONE);
            defaultValues.Put(CssConstants.BORDER_RIGHT_STYLE, CssConstants.NONE);
            defaultValues.Put(CssConstants.BORDER_TOP_STYLE, CssConstants.NONE);
            defaultValues.Put(CssConstants.BORDER_BOTTOM_WIDTH, CssConstants.MEDIUM);
            defaultValues.Put(CssConstants.BORDER_LEFT_WIDTH, CssConstants.MEDIUM);
            defaultValues.Put(CssConstants.BORDER_RIGHT_WIDTH, CssConstants.MEDIUM);
            defaultValues.Put(CssConstants.BORDER_TOP_WIDTH, CssConstants.MEDIUM);
            defaultValues.Put(CssConstants.BORDER_WIDTH, CssConstants.MEDIUM);
            defaultValues.Put(CssConstants.BORDER_IMAGE, CssConstants.NONE);
            defaultValues.Put(CssConstants.BORDER_RADIUS, "0");
            defaultValues.Put(CssConstants.BORDER_BOTTOM_LEFT_RADIUS, "0");
            defaultValues.Put(CssConstants.BORDER_BOTTOM_RIGHT_RADIUS, "0");
            defaultValues.Put(CssConstants.BORDER_TOP_LEFT_RADIUS, "0");
            defaultValues.Put(CssConstants.BORDER_TOP_RIGHT_RADIUS, "0");
            defaultValues.Put(CssConstants.BOX_SHADOW, CssConstants.NONE);
            defaultValues.Put(CssConstants.FLOAT, CssConstants.NONE);
            defaultValues.Put(CssConstants.FONT_WEIGHT, CssConstants.NORMAL);
            defaultValues.Put(CssConstants.FONT_SIZE, CssConstants.MEDIUM);
            defaultValues.Put(CssConstants.FONT_STYLE, CssConstants.NORMAL);
            defaultValues.Put(CssConstants.FONT_VARIANT, CssConstants.NORMAL);
            defaultValues.Put(CssConstants.HYPHENS, CssConstants.MANUAL);
            defaultValues.Put(CssConstants.LINE_HEIGHT, CssConstants.NORMAL);
            defaultValues.Put(CssConstants.LIST_STYLE_TYPE, CssConstants.DISC);
            defaultValues.Put(CssConstants.LIST_STYLE_IMAGE, CssConstants.NONE);
            defaultValues.Put(CssConstants.LIST_STYLE_POSITION, CssConstants.OUTSIDE);
            defaultValues.Put(CssConstants.MARGIN_BOTTOM, "0");
            defaultValues.Put(CssConstants.MARGIN_LEFT, "0");
            defaultValues.Put(CssConstants.MARGIN_RIGHT, "0");
            defaultValues.Put(CssConstants.MARGIN_TOP, "0");
            defaultValues.Put(CssConstants.MIN_HEIGHT, "0");
            defaultValues.Put(CssConstants.OUTLINE_COLOR, CssConstants.CURRENTCOLOR);
            defaultValues.Put(CssConstants.OUTLINE_STYLE, CssConstants.NONE);
            defaultValues.Put(CssConstants.OUTLINE_WIDTH, CssConstants.MEDIUM);
            defaultValues.Put(CssConstants.PADDING_BOTTOM, "0");
            defaultValues.Put(CssConstants.PADDING_LEFT, "0");
            defaultValues.Put(CssConstants.PADDING_RIGHT, "0");
            defaultValues.Put(CssConstants.PADDING_TOP, "0");
            defaultValues.Put(CssConstants.PAGE_BREAK_AFTER, CssConstants.AUTO);
            defaultValues.Put(CssConstants.PAGE_BREAK_BEFORE, CssConstants.AUTO);
            defaultValues.Put(CssConstants.PAGE_BREAK_INSIDE, CssConstants.AUTO);
            defaultValues.Put(CssConstants.POSITION, CssConstants.STATIC);
            defaultValues.Put(CssConstants.QUOTES, "\"\\00ab\" \"\\00bb\"");
            defaultValues.Put(CssConstants.TEXT_ALIGN, CssConstants.START);
            defaultValues.Put(CssConstants.TEXT_DECORATION, CssConstants.NONE);
            defaultValues.Put(CssConstants.TEXT_TRANSFORM, CssConstants.NONE);
            defaultValues.Put(CssConstants.TEXT_DECORATION, CssConstants.NONE);
            defaultValues.Put(CssConstants.WHITE_SPACE, CssConstants.NORMAL);
            defaultValues.Put(CssConstants.WIDTH, CssConstants.AUTO);
        }

        // TODO not complete
        /// <summary>Gets the default value of a property.</summary>
        /// <param name="property">the property</param>
        /// <returns>the default value</returns>
        public static String GetDefaultValue(String property) {
            String defaultVal = defaultValues.Get(property);
            if (defaultVal == null) {
                ILog logger = LogManager.GetLogger(typeof(CssDefaults));
                logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.DEFAULT_VALUE_OF_CSS_PROPERTY_UNKNOWN
                    , property));
            }
            return defaultVal;
        }
    }
}
