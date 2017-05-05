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
using System.Text;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Util {
    public class WaitingInlineElementsHelper {
        private String textTransform;

        private bool keepLineBreaks;

        private bool collapseSpaces;

        private IList<ILeafElement> waitingLeaves = new List<ILeafElement>();

        public WaitingInlineElementsHelper(String whiteSpace, String textTransform) {
            keepLineBreaks = CssConstants.PRE.Equals(whiteSpace) || CssConstants.PRE_WRAP.Equals(whiteSpace) || CssConstants
                .PRE_LINE.Equals(whiteSpace);
            collapseSpaces = !(CssConstants.PRE.Equals(whiteSpace) || CssConstants.PRE_WRAP.Equals(whiteSpace));
            this.textTransform = textTransform;
        }

        public virtual void Add(String text) {
            if (!keepLineBreaks && collapseSpaces) {
                text = CollapseConsecutiveSpaces(text);
            }
            else {
                if (keepLineBreaks && collapseSpaces) {
                    StringBuilder sb = new StringBuilder(text.Length);
                    for (int i = 0; i < text.Length; i++) {
                        if (TrimUtil.IsNonLineBreakSpace(text[i])) {
                            if (sb.Length == 0 || sb[sb.Length - 1] != ' ') {
                                sb.Append(" ");
                            }
                        }
                        else {
                            sb.Append(text[i]);
                        }
                    }
                    text = sb.ToString();
                }
                else {
                    // false == collapseSpaces
                    // prohibit trimming first and last spaces
                    StringBuilder sb = new StringBuilder(text.Length);
                    sb.Append('\u200d');
                    for (int i = 0; i < text.Length; i++) {
                        sb.Append(text[i]);
                        if ('\n' == text[i] || ('\r' == text[i] && i + 1 < text.Length && '\n' != text[i + 1])) {
                            sb.Append('\u200d');
                        }
                    }
                    if ('\u200d' == sb[sb.Length - 1]) {
                        sb.Delete(sb.Length - 1, sb.Length);
                    }
                    text = sb.ToString();
                }
            }
            if (CssConstants.UPPERCASE.Equals(textTransform)) {
                text = text.ToUpperInvariant();
            }
            else {
                if (CssConstants.LOWERCASE.Equals(textTransform)) {
                    text = text.ToLowerInvariant();
                }
            }
            waitingLeaves.Add(new iText.Layout.Element.Text(text));
        }

        public virtual void Add(ILeafElement element) {
            waitingLeaves.Add(element);
        }

        public virtual void AddAll(ICollection<ILeafElement> collection) {
            waitingLeaves.AddAll(collection);
        }

        public virtual void FlushHangingLeaves(IPropertyContainer container) {
            Paragraph p = CreateLeavesContainer();
            if (p != null) {
                if (container is Document) {
                    ((Document)container).Add(p);
                }
                else {
                    if (container is Paragraph) {
                        foreach (ILeafElement leafElement in waitingLeaves) {
                            ((Paragraph)container).Add(leafElement);
                        }
                    }
                    else {
                        if (container is Div) {
                            ((Div)container).Add(p);
                        }
                        else {
                            if (container is Cell) {
                                ((Cell)container).Add(p);
                            }
                            else {
                                if (container is List) {
                                    ListItem li = new ListItem();
                                    li.Add(p);
                                    ((List)container).Add(li);
                                }
                                else {
                                    throw new InvalidOperationException("Unable to process hanging inline content");
                                }
                            }
                        }
                    }
                }
                waitingLeaves.Clear();
            }
        }

        public virtual Paragraph CreateLeavesContainer() {
            if (collapseSpaces) {
                waitingLeaves = TrimUtil.TrimLeafElementsAndSanitize(waitingLeaves);
            }
            if (CssConstants.CAPITALIZE.Equals(textTransform)) {
                Capitalize(waitingLeaves);
            }
            if (waitingLeaves.Count > 0) {
                Paragraph p = CreateParagraphContainer();
                foreach (ILeafElement leaf in waitingLeaves) {
                    p.Add(leaf);
                    FloatPropertyValue? floatPropertyValue = leaf.GetProperty(Property.FLOAT);
                    if (floatPropertyValue != null && !(leaf is Image)) {
                        p.SetProperty(Property.FLOAT, floatPropertyValue);
                    }
                    ClearPropertyValue? clearPropertyValue = leaf.GetProperty(Property.CLEAR);
                    if (clearPropertyValue != null && !(leaf is Image)) {
                        p.SetProperty(Property.CLEAR, clearPropertyValue);
                    }
                }
                // Default leading in html is 1.2 and it is an inherited value. However, if a paragraph only contains an image,
                // the default leading should be 1. This is the case when we create a dummy paragraph, therefore we should emulate this behavior.
                if (p.GetChildren().Count == 1 && p.GetChildren()[0] is Image) {
                    p.SetMultipliedLeading(1);
                }
                return p;
            }
            else {
                return null;
            }
        }

        public virtual ICollection<ILeafElement> GetWaitingLeaves() {
            return waitingLeaves;
        }

        public virtual IList<ILeafElement> GetSanitizedWaitingLeaves() {
            if (collapseSpaces) {
                return TrimUtil.TrimLeafElementsAndSanitize(waitingLeaves);
            }
            else {
                return waitingLeaves;
            }
        }

        public virtual void ClearWaitingLeaves() {
            waitingLeaves.Clear();
        }

        public virtual Paragraph CreateParagraphContainer() {
            return new Paragraph().SetMargin(0);
        }

        private static void Capitalize(IList<ILeafElement> leaves) {
            bool previousLetter = false;
            foreach (ILeafElement element in leaves) {
                if (element is iText.Layout.Element.Text) {
                    String text = ((iText.Layout.Element.Text)element).GetText();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < text.Length; i++) {
                        if (char.IsLower(text[i]) && !previousLetter) {
                            sb.Append(char.ToUpper(text[i]));
                            previousLetter = true;
                        }
                        else {
                            if (char.IsLetter(text[i])) {
                                sb.Append(text[i]);
                                previousLetter = true;
                            }
                            else {
                                sb.Append(text[i]);
                                previousLetter = false;
                            }
                        }
                    }
                    ((iText.Layout.Element.Text)element).SetText(sb.ToString());
                }
                else {
                    previousLetter = false;
                }
            }
        }

        private static String CollapseConsecutiveSpaces(String s) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++) {
                if (iText.IO.Util.TextUtil.IsWhiteSpace(s[i])) {
                    if (sb.Length == 0 || !iText.IO.Util.TextUtil.IsWhiteSpace(sb[sb.Length - 1])) {
                        sb.Append(" ");
                    }
                }
                else {
                    sb.Append(s[i]);
                }
            }
            return sb.ToString();
        }
    }
}
