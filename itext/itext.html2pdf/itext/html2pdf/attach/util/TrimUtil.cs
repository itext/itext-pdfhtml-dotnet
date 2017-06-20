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
using System.Collections.Generic;
using iText.Layout.Element;

namespace iText.Html2pdf.Attach.Util {
    public sealed class TrimUtil {
        private TrimUtil() {
        }

        public static IList<ILeafElement> TrimLeafElementsAndSanitize(IList<ILeafElement> leafElements) {
            List<ILeafElement> waitingLeaves = new List<ILeafElement>(leafElements);
            TrimSubList(waitingLeaves, 0, waitingLeaves.Count, false);
            TrimSubList(waitingLeaves, 0, waitingLeaves.Count, true);
            int pos = 0;
            while (pos < waitingLeaves.Count - 1) {
                if (waitingLeaves[pos] is Text) {
                    Text first = (Text)waitingLeaves[pos];
                    int firstEnd = GetIndexAfterLastNonSpace(first);
                    if (firstEnd < first.GetText().Length) {
                        TrimSubList(waitingLeaves, pos + 1, waitingLeaves.Count, false);
                        first.SetText(first.GetText().JSubstring(0, firstEnd + 1));
                    }
                }
                pos++;
            }
            return waitingLeaves;
        }

        internal static bool IsNonLineBreakSpace(char ch) {
            return iText.IO.Util.TextUtil.IsWhiteSpace(ch) && ch != '\n';
        }

        private static void TrimSubList(List<ILeafElement> list, int begin, int end, bool last) {
            while (end > begin) {
                int pos = last ? end - 1 : begin;
                ILeafElement leaf = list[pos];
                if (leaf is Text) {
                    Text text = (Text)leaf;
                    TrimLeafElement(text, last);
                    if (text.GetText().Length == 0) {
                        list.JRemoveAt(pos);
                        end--;
                        continue;
                    }
                }
                break;
            }
        }

        private static ILeafElement TrimLeafElement(ILeafElement leafElement, bool last) {
            if (leafElement is Text) {
                Text text = (Text)leafElement;
                int begin = last ? 0 : GetIndexOfFirstNonSpace(text);
                int end = last ? GetIndexAfterLastNonSpace(text) : text.GetText().Length;
                text.SetText(text.GetText().JSubstring(begin, end));
            }
            return leafElement;
        }

        private static int GetIndexOfFirstNonSpace(Text text) {
            int pos = 0;
            while (pos < text.GetText().Length && IsNonLineBreakSpace(text.GetText()[pos])) {
                pos++;
            }
            return pos;
        }

        private static int GetIndexAfterLastNonSpace(Text text) {
            int pos = text.GetText().Length;
            while (pos > 0 && IsNonLineBreakSpace(text.GetText()[pos - 1])) {
                pos--;
            }
            return pos;
        }
    }
}
