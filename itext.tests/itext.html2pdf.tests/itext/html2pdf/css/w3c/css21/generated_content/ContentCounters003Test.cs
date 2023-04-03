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
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Generated_content {
    /// <summary>Difference in width of two lines is due the our approach of fonts resolving.</summary>
    /// <remarks>
    /// Difference in width of two lines is due the our approach of fonts resolving.
    /// In first line, every span (and spaces in-between them) is a separate text chunk, for which
    /// fonts are resolved separately. Thus spaces end up in TimesRoman font, while discs font is
    /// resolved to FreeMono. The second line is a single text chunk, which is wholly resolved to FreeMono.
    /// This creates difference in lines width.
    /// The issue in the first place is that by default we should not resolve fonts to mono generic family,
    /// which will be supported under DEVSIX-1034.
    /// </remarks>
    public class ContentCounters003Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "content-counters-003.xht";
        }
    }
}
