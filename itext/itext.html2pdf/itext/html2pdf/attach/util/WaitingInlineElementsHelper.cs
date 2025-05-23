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
using System.Collections.Generic;
using System.Text;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Kernel.Pdf.Tagging;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Util;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Helper class for waiting inline elements.</summary>
    public class WaitingInlineElementsHelper {
        /// <summary>A value that defines how to transform text.</summary>
        private String textTransform;

        /// <summary>Indicates whether line breaks need to be preserved.</summary>
        private bool keepLineBreaks;

        /// <summary>Indicates whether white space characters need to be collapsed.</summary>
        private bool collapseSpaces;

        /// <summary>List of waiting leaf elements.</summary>
        private IList<IElement> waitingLeaves = new List<IElement>();

        /// <summary>
        /// Creates a new
        /// <see cref="WaitingInlineElementsHelper"/>
        /// instance.
        /// </summary>
        /// <param name="whiteSpace">we'll check if this value equals "pre" or "pre-wrap"</param>
        /// <param name="textTransform">will define the transformation that needs to be applied to the text</param>
        public WaitingInlineElementsHelper(String whiteSpace, String textTransform) {
            keepLineBreaks = CssConstants.PRE.Equals(whiteSpace) || CssConstants.PRE_WRAP.Equals(whiteSpace) || CssConstants
                .PRE_LINE.Equals(whiteSpace);
            collapseSpaces = !(CssConstants.PRE.Equals(whiteSpace) || CssConstants.PRE_WRAP.Equals(whiteSpace));
            this.textTransform = textTransform;
        }

        /// <summary>Adds text to the waiting leaves.</summary>
        /// <param name="text">the text</param>
        public virtual void Add(String text) {
            text = WhiteSpaceUtil.ProcessWhitespaces(text, keepLineBreaks, collapseSpaces);
            // Here we intentionally use locale dependent toLowerCase/toUpperCase
            if (CssConstants.UPPERCASE.Equals(textTransform)) {
                text = text.ToUpperInvariant();
            }
            else {
                if (CssConstants.LOWERCASE.Equals(textTransform)) {
                    text = text.ToLowerInvariant();
                }
            }
            waitingLeaves.Add(new Text(text));
        }

        /// <summary>Adds a leaf element to the waiting leaves.</summary>
        /// <param name="element">the element</param>
        public virtual void Add(ILeafElement element) {
            waitingLeaves.Add(element);
        }

        /// <summary>Adds a block element to the waiting leaves.</summary>
        /// <param name="element">the element</param>
        public virtual void Add(IBlockElement element) {
            waitingLeaves.Add(element);
        }

        /// <summary>Adds a collecton of leaf elements to the waiting leaves.</summary>
        /// <param name="collection">the collection</param>
        public virtual void AddAll(ICollection<ILeafElement> collection) {
            waitingLeaves.AddAll(collection);
        }

        /// <summary>Flush hanging leaves.</summary>
        /// <param name="container">a container element</param>
        public virtual void FlushHangingLeaves(IPropertyContainer container) {
            AnonymousInlineBox ab = CreateLeavesContainer();
            if (ab != null) {
                IDictionary<String, String> map = new Dictionary<String, String>();
                map.Put(CssConstants.OVERFLOW, CommonCssConstants.VISIBLE);
                OverflowApplierUtil.ApplyOverflow(map, ab);
                if (container is Document) {
                    ((Document)container).Add(ab);
                }
                else {
                    if (container is Paragraph) {
                        foreach (IElement leafElement in waitingLeaves) {
                            if (leafElement is ILeafElement) {
                                ((Paragraph)container).Add((ILeafElement)leafElement);
                            }
                            else {
                                if (leafElement is IBlockElement) {
                                    ((Paragraph)container).Add((IBlockElement)leafElement);
                                }
                            }
                        }
                    }
                    else {
                        if (((IElement)container).GetRenderer() is FlexContainerRenderer) {
                            Div div = new Div();
                            OverflowApplierUtil.ApplyOverflow(map, div);
                            div.Add(ab);
                            ((Div)container).Add(div);
                        }
                        else {
                            if (container is Div) {
                                ((Div)container).Add(ab);
                            }
                            else {
                                if (container is Cell) {
                                    ((Cell)container).Add(ab);
                                }
                                else {
                                    if (container is List) {
                                        ListItem li = new ListItem();
                                        li.Add(ab);
                                        ((List)container).Add(li);
                                    }
                                    else {
                                        throw new InvalidOperationException("Unable to process hanging inline content");
                                    }
                                }
                            }
                        }
                    }
                }
                waitingLeaves.Clear();
            }
        }

        /// <summary>Creates the leaves container.</summary>
        /// <returns>
        /// an
        /// <see cref="iText.Layout.Element.AnonymousInlineBox"/>
        /// </returns>
        private AnonymousInlineBox CreateLeavesContainer() {
            if (collapseSpaces) {
                waitingLeaves = TrimUtil.TrimLeafElementsAndSanitize(waitingLeaves);
            }
            Capitalize(waitingLeaves);
            if (waitingLeaves.IsEmpty()) {
                return null;
            }
            AnonymousInlineBox ab = new AnonymousInlineBox();
            ab.GetAccessibilityProperties().SetRole(StandardRoles.P);
            bool runningElementsOnly = true;
            foreach (IElement leaf in waitingLeaves) {
                if (leaf is ILeafElement) {
                    runningElementsOnly = false;
                    ab.Add((ILeafElement)leaf);
                }
                else {
                    if (leaf is IBlockElement) {
                        runningElementsOnly = runningElementsOnly && leaf is RunningElement;
                        ab.Add((IBlockElement)leaf);
                    }
                }
            }
            if (runningElementsOnly) {
                // TODO DEVSIX-7008 Remove completely empty tags from logical structure of resultant PDF documents
                ab.GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
            }
            return ab;
        }

        /// <summary>Gets the waiting leaves.</summary>
        /// <returns>the waiting leaves</returns>
        public virtual ICollection<IElement> GetWaitingLeaves() {
            return waitingLeaves;
        }

        /// <summary>Gets the sanitized waiting leaves.</summary>
        /// <returns>the sanitized waiting leaves</returns>
        public virtual IList<IElement> GetSanitizedWaitingLeaves() {
            if (collapseSpaces) {
                return TrimUtil.TrimLeafElementsAndSanitize(waitingLeaves);
            }
            else {
                return waitingLeaves;
            }
        }

        /// <summary>Clears the waiting leaves.</summary>
        public virtual void ClearWaitingLeaves() {
            waitingLeaves.Clear();
        }

        /// <summary>Creates a paragraph container.</summary>
        /// <returns>the paragraph container</returns>
        [System.ObsoleteAttribute(@"to remove as it is not used anywhere anymore")]
        public virtual Paragraph CreateParagraphContainer() {
            return new Paragraph().SetMargin(0);
        }

        /// <summary>Capitalizes a series of leaf elements.</summary>
        /// <param name="leaves">a list of leaf elements</param>
        private void Capitalize(IList<IElement> leaves) {
            bool previousLetter = false;
            bool previousProcessed = false;
            for (int i = 0; i < leaves.Count; i++) {
                IElement element = leaves[i];
                bool hasCapitalizeProperty = element.HasOwnProperty(Html2PdfProperty.CAPITALIZE_ELEMENT);
                bool needCapitalize = hasCapitalizeProperty && ((bool)element.GetOwnProperty<bool?>(Html2PdfProperty.CAPITALIZE_ELEMENT
                    ));
                if (hasCapitalizeProperty && !needCapitalize) {
                    previousProcessed = false;
                    continue;
                }
                if (element is Text && (CssConstants.CAPITALIZE.Equals(textTransform) || needCapitalize)) {
                    String text = ((Text)element).GetText();
                    if (!previousProcessed && i > 0) {
                        previousLetter = IsLastCharAlphabetic(leaves[i - 1]);
                    }
                    previousLetter = CapitalizeAndReturnIsLastAlphabetic((Text)element, text, previousLetter);
                    previousProcessed = true;
                }
                else {
                    previousProcessed = false;
                    previousLetter = false;
                }
            }
        }

        private bool IsLastCharAlphabetic(IElement element) {
            if (!(element is Text)) {
                return false;
            }
            String text = ((Text)element).GetText();
            return text.Length > 0 && char.IsLetter(text[text.Length - 1]);
        }

        private bool CapitalizeAndReturnIsLastAlphabetic(Text element, String text, bool previousAlphabetic) {
            StringBuilder sb = new StringBuilder();
            bool previousLetter = previousAlphabetic;
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
            element.SetText(sb.ToString());
            return previousLetter;
        }
    }
}
