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
using System.Text;
using iText.IO.Log;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Util {
    /// <summary>Utilities class with functionality to normalize CSS properties.</summary>
    internal class CssPropertyNormalizer {
        /// <summary>Normalize a property.</summary>
        /// <param name="str">the property</param>
        /// <returns>the normalized property</returns>
        public static String Normalize(String str) {
            StringBuilder buffer = new StringBuilder();
            int segmentStart = 0;
            for (int i = 0; i < str.Length; ++i) {
                if (str[i] == '\\') {
                    ++i;
                }
                else {
                    if (str[i] == '\'' || str[i] == '"') {
                        AppendAndFormatSegment(buffer, str, segmentStart, i + 1);
                        segmentStart = i = AppendQuoteContent(buffer, str, i + 1, str[i]);
                    }
                }
            }
            if (segmentStart < str.Length) {
                AppendAndFormatSegment(buffer, str, segmentStart, str.Length);
            }
            return buffer.ToString();
        }

        /// <summary>Appends and formats a segment.</summary>
        /// <param name="buffer">the current buffer</param>
        /// <param name="source">a source</param>
        /// <param name="start">where to start in the source</param>
        /// <param name="end">where to end in the source</param>
        private static void AppendAndFormatSegment(StringBuilder buffer, String source, int start, int end) {
            String[] parts = iText.IO.Util.StringUtil.Split(source.JSubstring(start, end), "\\s");
            StringBuilder sb = new StringBuilder();
            foreach (String part in parts) {
                if (part.Length > 0) {
                    if (sb.Length > 0 && !TrimSpaceAfter(sb[sb.Length - 1]) && !TrimSpaceBefore(part[0])) {
                        sb.Append(" ");
                    }
                    // Do not make base64 data lowercase, function name only
                    if (part.Matches("^[uU][rR][lL]\\(.+\\)") && CssUtils.IsBase64Data(part.JSubstring(4, part.Length - 1))) {
                        sb.Append(part.JSubstring(0, 3).ToLowerInvariant()).Append(part.Substring(3));
                    }
                    else {
                        sb.Append(part.ToLowerInvariant());
                    }
                }
            }
            buffer.Append(sb);
        }

        /// <summary>Appends quoted content.</summary>
        /// <param name="buffer">the current buffer</param>
        /// <param name="source">a source</param>
        /// <param name="start">where to start in the source</param>
        /// <param name="endQuoteSymbol">the end quote symbol</param>
        /// <returns>the new position in the source</returns>
        private static int AppendQuoteContent(StringBuilder buffer, String source, int start, char endQuoteSymbol) {
            int end = FindNextUnescapedChar(source, endQuoteSymbol, start);
            if (end == -1) {
                end = source.Length;
                LoggerFactory.GetLogger(typeof(CssPropertyNormalizer)).Warn(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant
                    .QUOTE_IS_NOT_CLOSED_IN_CSS_EXPRESSION, source));
            }
            buffer.JAppend(source, start, end);
            return end;
        }

        /// <summary>Find the next unescaped character.</summary>
        /// <param name="source">a source</param>
        /// <param name="ch">the character to look for</param>
        /// <param name="startIndex">where to start looking</param>
        /// <returns>the position of the next unescaped character</returns>
        private static int FindNextUnescapedChar(String source, char ch, int startIndex) {
            int symbolPos = source.IndexOf(ch, startIndex);
            if (symbolPos == -1) {
                return -1;
            }
            int afterNoneEscapePos = symbolPos;
            while (afterNoneEscapePos > 0 && source[afterNoneEscapePos - 1] == '\\') {
                --afterNoneEscapePos;
            }
            return (symbolPos - afterNoneEscapePos) % 2 == 0 ? symbolPos : FindNextUnescapedChar(source, ch, symbolPos
                 + 1);
        }

        /// <summary>Checks if spaces can be trimmed after a specific character.</summary>
        /// <param name="ch">the character</param>
        /// <returns>true, if spaces can be trimmed after the character</returns>
        private static bool TrimSpaceAfter(char ch) {
            return ch == ',' || ch == '(';
        }

        /// <summary>Checks if spaces can be trimmed before a specific character.</summary>
        /// <param name="ch">the character</param>
        /// <returns>true, if spaces can be trimmed before the character</returns>
        private static bool TrimSpaceBefore(char ch) {
            return ch == ',' || ch == ')';
        }
    }
}
