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
using System.Text.RegularExpressions;
using iText.Html2pdf.Css.Selector.Item;

namespace iText.Html2pdf.Css.Parse {
    public sealed class CssSelectorParser {
        private static readonly ICollection<String> legacyPseudoElements = new HashSet<String>();

        private const String SELECTOR_PATTERN_STR = "(\\*)|([_a-zA-Z][\\w-]*)|(\\.[_a-zA-Z][\\w-]*)|(#[_a-z][\\w-]*)|(\\[[_a-zA-Z][\\w-]*(([~^$*|])?=((\"[^\"]+\")|([^\"]+)|('[^\"]+')))?\\])|(::?[\\w()-]*)|( )|(\\+)|(>)|(~)";

        private static readonly Regex selectorPattern = iText.IO.Util.StringUtil.RegexCompile(SELECTOR_PATTERN_STR
            );

        static CssSelectorParser() {
            legacyPseudoElements.Add("first-line");
            legacyPseudoElements.Add("first-letter");
            legacyPseudoElements.Add("before");
            legacyPseudoElements.Add("after");
        }

        private CssSelectorParser() {
        }

        public static IList<ICssSelectorItem> ParseSelectorItems(String selector) {
            IList<ICssSelectorItem> selectorItems = new List<ICssSelectorItem>();
            Match itemMatcher = iText.IO.Util.StringUtil.Match(selectorPattern, selector);
            bool tagSelectorDescription = false;
            while (itemMatcher.Success) {
                String selectorItem = iText.IO.Util.StringUtil.Group(itemMatcher, 0);
                itemMatcher = itemMatcher.NextMatch();
                char firstChar = selectorItem[0];
                switch (firstChar) {
                    case '#': {
                        selectorItems.Add(new CssIdSelectorItem(selectorItem.Substring(1)));
                        break;
                    }

                    case '.': {
                        selectorItems.Add(new CssClassSelectorItem(selectorItem.Substring(1)));
                        break;
                    }

                    case '[': {
                        selectorItems.Add(new CssAttributeSelectorItem(selectorItem));
                        break;
                    }

                    case ':': {
                        selectorItems.Add(ResolvePseudoSelector(selectorItem));
                        break;
                    }

                    case ' ':
                    case '+':
                    case '>':
                    case '~': {
                        if (selectorItems.Count == 0) {
                            throw new ArgumentException(String.Format("Invalid token detected in the start of the selector string: {0}"
                                , firstChar));
                        }
                        ICssSelectorItem lastItem = selectorItems[selectorItems.Count - 1];
                        CssSeparatorSelectorItem curItem = new CssSeparatorSelectorItem(firstChar);
                        if (lastItem is CssSeparatorSelectorItem) {
                            if (curItem.GetSeparator() == ' ') {
                                break;
                            }
                            else {
                                if (((CssSeparatorSelectorItem)lastItem).GetSeparator() == ' ') {
                                    selectorItems[selectorItems.Count - 1] = curItem;
                                }
                                else {
                                    throw new ArgumentException(String.Format("Invalid selector description. Two consequent characters occurred: {0}, {1}"
                                        , ((CssSeparatorSelectorItem)lastItem).GetSeparator(), curItem.GetSeparator()));
                                }
                            }
                        }
                        else {
                            selectorItems.Add(curItem);
                            tagSelectorDescription = false;
                        }
                        break;
                    }

                    default: {
                        //and case '*':
                        if (tagSelectorDescription) {
                            throw new InvalidOperationException("Invalid selector string");
                        }
                        tagSelectorDescription = true;
                        selectorItems.Add(new CssTagSelectorItem(selectorItem));
                        break;
                    }
                }
            }
            if (selectorItems.Count == 0) {
                throw new ArgumentException("Selector declaration is invalid");
            }
            return selectorItems;
        }

        private static ICssSelectorItem ResolvePseudoSelector(String pseudoSelector) {
            /*
            This :: notation is introduced by the current document in order to establish a discrimination between
            pseudo-classes and pseudo-elements.
            For compatibility with existing style sheets, user agents must also accept the previous one-colon
            notation for pseudo-elements introduced in CSS levels 1 and 2 (namely, :first-line, :first-letter, :before and :after).
            This compatibility is not allowed for the new pseudo-elements introduced in this specification.
            */
            if (pseudoSelector.StartsWith("::")) {
                return new CssPseudoElementSelectorItem(pseudoSelector.Substring(2));
            }
            else {
                if (pseudoSelector.StartsWith(":") && legacyPseudoElements.Contains(pseudoSelector.Substring(1))) {
                    return new CssPseudoElementSelectorItem(pseudoSelector.Substring(1));
                }
                else {
                    return new CssPseudoClassSelectorItem(pseudoSelector.Substring(1));
                }
            }
        }
    }
}
