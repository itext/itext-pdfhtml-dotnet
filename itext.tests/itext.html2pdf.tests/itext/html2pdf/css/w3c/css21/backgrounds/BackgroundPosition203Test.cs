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
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2431 Positioned elements are lost when block is split across pages.
    [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.WAS_NOT_ABLE_TO_DEFINE_BACKGROUND_CSS_SHORTHAND_PROPERTIES
        , Count = 2)]
    [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
        Count = 19)]
    [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.ONLY_THE_LAST_BACKGROUND_CAN_INCLUDE_BACKGROUND_COLOR
        )]
    public class BackgroundPosition203Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-position-203.xht";
        }
    }
}
