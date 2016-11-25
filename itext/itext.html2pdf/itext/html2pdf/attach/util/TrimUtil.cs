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
using System.Collections.Generic;
using iText.Layout.Element;

namespace iText.Html2pdf.Attach.Util {
    public sealed class TrimUtil {
        private TrimUtil() {
        }

        // Note that the end is not trimmed. Maybe we should trim the content here, but the end trim is performed during layout anyway
        public static IList<ILeafElement> TrimLeafElementsFirstAndSanitize(IList<ILeafElement> leafElements) {
            IList<ILeafElement> waitingLeafs = new List<ILeafElement>(leafElements);
            while (waitingLeafs.Count > 0 && waitingLeafs[0] is Text) {
                Text text = (Text)waitingLeafs[0];
                TrimLeafElementFirst(text);
                if (text.GetText().Length == 0) {
                    waitingLeafs.JRemoveAt(0);
                }
                else {
                    break;
                }
            }
            int pos = 0;
            while (pos < waitingLeafs.Count - 1) {
                if (waitingLeafs[pos] is Text) {
                    Text first = (Text)waitingLeafs[pos];
                    if (first.GetText().Length > 0 && IsNonLineBreakSpace(first.GetText()[first.GetText().Length - 1])) {
                        while (pos + 1 < waitingLeafs.Count && waitingLeafs[pos + 1] is Text) {
                            Text second = (Text)waitingLeafs[pos + 1];
                            if (second.GetText().Length > 0 && IsNonLineBreakSpace(second.GetText()[0])) {
                                int secondPos = 0;
                                while (secondPos < second.GetText().Length && IsNonLineBreakSpace(second.GetText()[secondPos])) {
                                    secondPos++;
                                }
                                second.SetText(second.GetText().Substring(secondPos));
                            }
                            if (second.GetText().Length == 0) {
                                waitingLeafs.JRemoveAt(pos + 1);
                            }
                            else {
                                break;
                            }
                        }
                    }
                }
                pos++;
            }
            return waitingLeafs;
        }

        public static ILeafElement TrimLeafElementFirst(ILeafElement leafElement) {
            if (leafElement is Text) {
                Text text = (Text)leafElement;
                int pos = 0;
                while (pos < text.GetText().Length && IsNonLineBreakSpace(text.GetText()[pos])) {
                    pos++;
                }
                text.SetText(text.GetText().Substring(pos));
            }
            return leafElement;
        }

        internal static bool IsNonLineBreakSpace(char ch) {
            return iText.IO.Util.TextUtil.IsWhiteSpace(ch) && ch != '\n';
        }
    }
}
