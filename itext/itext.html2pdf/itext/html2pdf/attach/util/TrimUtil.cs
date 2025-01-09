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
using System.Collections.Generic;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Util;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Utility class to trim content.</summary>
    public sealed class TrimUtil {
        /// <summary>
        /// Creates a new
        /// <see cref="TrimUtil"/>
        /// instance.
        /// </summary>
        private TrimUtil() {
        }

//\cond DO_NOT_DOCUMENT
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
//\endcond

        /// <summary>Trims a sub list of leaf elements.</summary>
        /// <param name="list">the list of leaf elements</param>
        /// <param name="begin">the index where to begin</param>
        /// <param name="end">the index where to end</param>
        /// <param name="last">indicates where to start, if true, we start at the end</param>
        private static void TrimSubList(List<IElement> list, int begin, int end, bool last) {
            while (end > begin) {
                int pos = last ? end - 1 : begin;
                IElement leaf = list[pos];
                if (IsElementFloating(leaf) || leaf is RunningElement) {
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
            while (pos < text.GetText().Length && WhiteSpaceUtil.IsNonLineBreakSpace(text.GetText()[pos])) {
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
            while (pos > 0 && WhiteSpaceUtil.IsNonLineBreakSpace(text.GetText()[pos - 1])) {
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
                ) || 0 == ((Border)leafElement.GetProperty<Border>(Property.BORDER_LEFT)).GetWidth()) && 
                        // Note that iText parses padding and float values to points so the next unit values are always point values
                        (null == leafElement.GetProperty<UnitValue>(Property.PADDING_RIGHT) || 0 == leafElement.GetProperty<UnitValue
                >(Property.PADDING_RIGHT).GetValue()) && (null == leafElement.GetProperty<UnitValue>(Property.PADDING_LEFT
                ) || 0 == leafElement.GetProperty<UnitValue>(Property.PADDING_LEFT).GetValue()) && (null == leafElement
                .GetProperty<UnitValue>(Property.MARGIN_RIGHT) || 0 == leafElement.GetProperty<UnitValue>(Property.MARGIN_RIGHT
                ).GetValue()) && (null == leafElement.GetProperty<UnitValue>(Property.MARGIN_LEFT) || 0 == leafElement
                .GetProperty<UnitValue>(Property.MARGIN_LEFT).GetValue());
        }
    }
}
