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
using iText.IO.Util;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Utility class to trim content.</summary>
    public sealed class TrimUtil {
        private static readonly ICollection<char> EM_SPACES = new HashSet<char>();

        static TrimUtil() {
            EM_SPACES.Add((char)0x2002);
            EM_SPACES.Add((char)0x2003);
            EM_SPACES.Add((char)0x2009);
        }

        /// <summary>
        /// Creates a new
        /// <see cref="TrimUtil"/>
        /// instance.
        /// </summary>
        private TrimUtil() {
        }

        /// <summary>Trim leaf elements, and sanitize.</summary>
        /// <param name="leafElements">the leaf elements</param>
        /// <returns>the trimmed and sanitized list</returns>
        internal static IList<IElement> TrimLeafElementsAndSanitize(IList<IElement> leafElements) {
            List<IElement> waitingLeaves = new List<IElement>(leafElements);
            TrimSubList(waitingLeaves, 0, waitingLeaves.Count, false);
            TrimSubList(waitingLeaves, 0, waitingLeaves.Count, true);
            int pos = 0;
            while (pos < waitingLeaves.Count - 1) {
                if (waitingLeaves[pos] is Text) {
                    Text first = (Text)waitingLeaves[pos];
                    if (IsElementFloating(first)) {
                        TrimTextElement(first, false);
                        TrimTextElement(first, true);
                    }
                    else {
                        int firstEnd = GetIndexAfterLastNonSpace(first);
                        if (firstEnd < first.GetText().Length) {
                            TrimSubList(waitingLeaves, pos + 1, waitingLeaves.Count, false);
                            first.SetText(first.GetText().JSubstring(0, firstEnd + 1));
                        }
                    }
                }
                pos++;
            }
            return waitingLeaves;
        }

        /// <summary>Checks if a character is white space value that doesn't cause a newline.</summary>
        /// <param name="ch">the character</param>
        /// <returns>true, if the character is a white space character, but no newline</returns>
        internal static bool IsNonLineBreakSpace(char ch) {
            return IsNonEmSpace(ch) && ch != '\n';
        }

        /// <summary>Checks if a character is white space value that is not em, en or similar special whitespace character.
        ///     </summary>
        /// <param name="ch">the character</param>
        /// <returns>true, if the character is a white space character, but no em, en or similar</returns>
        internal static bool IsNonEmSpace(char ch) {
            return TextUtil.IsWhiteSpace(ch) && !EM_SPACES.Contains(ch);
        }

        /// <summary>Trims a sub list of leaf elements.</summary>
        /// <param name="list">the list of leaf elements</param>
        /// <param name="begin">the index where to begin</param>
        /// <param name="end">the index where to end</param>
        /// <param name="last">indicates where to start, if true, we start at the end</param>
        private static void TrimSubList(List<IElement> list, int begin, int end, bool last) {
            while (end > begin) {
                int pos = last ? end - 1 : begin;
                IElement leaf = list[pos];
                if (IsElementFloating(leaf)) {
                    if (last) {
                        --end;
                    }
                    else {
                        ++begin;
                    }
                    continue;
                }
                if (leaf is Text) {
                    Text text = (Text)leaf;
                    TrimTextElement(text, last);
                    if (text.GetText().Length == 0) {
                        if (HasZeroWidth(text)) {
                            list.JRemoveAt(pos);
                        }
                        end--;
                        continue;
                    }
                }
                break;
            }
        }

        /// <summary>Trims a text element.</summary>
        /// <param name="text">the text element</param>
        /// <param name="last">indicates where to start, if true, we start at the end</param>
        private static void TrimTextElement(Text text, bool last) {
            int begin = last ? 0 : GetIndexOfFirstNonSpace(text);
            int end = last ? GetIndexAfterLastNonSpace(text) : text.GetText().Length;
            text.SetText(text.GetText().JSubstring(begin, end));
        }

        /// <summary>Gets the index of first character that isn't white space in some text.</summary>
        /// <remarks>
        /// Gets the index of first character that isn't white space in some text.
        /// Note: newline characters aren't counted as white space characters.
        /// </remarks>
        /// <param name="text">the text</param>
        /// <returns>the index of first character that isn't white space</returns>
        private static int GetIndexOfFirstNonSpace(Text text) {
            int pos = 0;
            while (pos < text.GetText().Length && IsNonLineBreakSpace(text.GetText()[pos])) {
                pos++;
            }
            return pos;
        }

        /// <summary>Gets the index of last character following a character that isn't white space in some text.</summary>
        /// <remarks>
        /// Gets the index of last character following a character that isn't white space in some text.
        /// Note: newline characters aren't counted as white space characters.
        /// </remarks>
        /// <param name="text">the text</param>
        /// <returns>the index following the last character that isn't white space</returns>
        private static int GetIndexAfterLastNonSpace(Text text) {
            int pos = text.GetText().Length;
            while (pos > 0 && IsNonLineBreakSpace(text.GetText()[pos - 1])) {
                pos--;
            }
            return pos;
        }

        private static bool IsElementFloating(IElement leafElement) {
            FloatPropertyValue? floatPropertyValue = leafElement.GetProperty<FloatPropertyValue?>(Property.FLOAT);
            int? position = leafElement.GetProperty<int?>(Property.POSITION);
            return (position == null || position != LayoutPosition.ABSOLUTE) && floatPropertyValue != null && !floatPropertyValue
                .Equals(FloatPropertyValue.NONE);
        }

        private static bool HasZeroWidth(IElement leafElement) {
            return (null == leafElement.GetProperty<Border>(Property.BORDER_RIGHT) || 0 == ((Border)leafElement.GetProperty
                <Border>(Property.BORDER_RIGHT)).GetWidth()) && (null == leafElement.GetProperty<Border>(Property.BORDER_LEFT
                ) || 0 == ((Border)leafElement.GetProperty<Border>(Property.BORDER_LEFT)).GetWidth()) && (null == leafElement
                .GetProperty<Object>(Property.PADDING_RIGHT) || 0 == (float)NumberUtil.AsFloat(leafElement.GetProperty
                <Object>(Property.PADDING_RIGHT))) && (null == leafElement.GetProperty<Object>(Property.PADDING_LEFT) 
                || 0 == (float)NumberUtil.AsFloat(leafElement.GetProperty<Object>(Property.PADDING_LEFT))) && (null ==
                 leafElement.GetProperty<Object>(Property.MARGIN_RIGHT) || 0 == (float)NumberUtil.AsFloat(leafElement.
                GetProperty<Object>(Property.MARGIN_RIGHT))) && (null == leafElement.GetProperty<Object>(Property.MARGIN_LEFT
                ) || 0 == (float)NumberUtil.AsFloat(leafElement.GetProperty<Object>(Property.MARGIN_LEFT)));
        }
    }
}
