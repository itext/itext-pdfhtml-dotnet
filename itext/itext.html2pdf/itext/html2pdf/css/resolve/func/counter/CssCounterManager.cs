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
using iText.Html2pdf.Html.Node;
using iText.Kernel.Numbering;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    public class CssCounterManager {
        private const String DISC_SYMBOL = "\u2022";

        private const String CIRCLE_SYMBOL = "\u25e6";

        private const String SQUARE_SYMBOL = "\u25a0";

        private const int DEFAULT_COUNTER_VALUE = 0;

        private const int DEFAULT_INCREMENT_VALUE = 1;

        private const int MAX_ROMAN_NUMBER = 3999;

        private IDictionary<INode, IDictionary<String, int?>> counters = new Dictionary<INode, IDictionary<String, 
            int?>>();

        public CssCounterManager() {
        }

        public virtual String ResolveCounter(String counterName, String listSymbolType, INode scope) {
            IDictionary<String, int?> scopeCounters = FindSuitableScopeMap(scope, counterName);
            int? counterValue = scopeCounters != null ? scopeCounters.Get(counterName) : null;
            if (counterValue == null) {
                return null;
            }
            else {
                // TODO we do that to print a logger message. We might want to reconsider and silently reset to 0 in the future
                if (listSymbolType == null) {
                    return counterValue.ToString();
                }
                else {
                    if (CssConstants.NONE.Equals(listSymbolType)) {
                        return "";
                    }
                    else {
                        if (CssConstants.DISC.Equals(listSymbolType)) {
                            return DISC_SYMBOL;
                        }
                        else {
                            if (CssConstants.SQUARE.Equals(listSymbolType)) {
                                return SQUARE_SYMBOL;
                            }
                            else {
                                if (CssConstants.CIRCLE.Equals(listSymbolType)) {
                                    return CIRCLE_SYMBOL;
                                }
                                else {
                                    if (CssConstants.UPPER_ALPHA.Equals(listSymbolType) || CssConstants.UPPER_LATIN.Equals(listSymbolType)) {
                                        return EnglishAlphabetNumbering.ToLatinAlphabetNumberUpperCase((int)counterValue);
                                    }
                                    else {
                                        if (CssConstants.LOWER_ALPHA.Equals(listSymbolType) || CssConstants.LOWER_LATIN.Equals(listSymbolType)) {
                                            return EnglishAlphabetNumbering.ToLatinAlphabetNumberLowerCase((int)counterValue);
                                        }
                                        else {
                                            if (CssConstants.LOWER_ROMAN.Equals(listSymbolType)) {
                                                return counterValue <= MAX_ROMAN_NUMBER ? RomanNumbering.ToRomanLowerCase((int)counterValue) : counterValue
                                                    .ToString();
                                            }
                                            else {
                                                if (CssConstants.UPPER_ROMAN.Equals(listSymbolType)) {
                                                    return counterValue <= MAX_ROMAN_NUMBER ? RomanNumbering.ToRomanUpperCase((int)counterValue) : counterValue
                                                        .ToString();
                                                }
                                                else {
                                                    if (CssConstants.DECIMAL_LEADING_ZERO.Equals(listSymbolType)) {
                                                        return (counterValue < 10 ? "0" : "") + counterValue.ToString();
                                                    }
                                                    else {
                                                        if (CssConstants.LOWER_GREEK.Equals(listSymbolType)) {
                                                            return GreekAlphabetNumbering.ToGreekAlphabetNumberLowerCase((int)counterValue);
                                                        }
                                                        else {
                                                            if (CssConstants.GEORGIAN.Equals(listSymbolType)) {
                                                                return GeorgianNumbering.ToGeorgian((int)counterValue);
                                                            }
                                                            else {
                                                                if (CssConstants.ARMENIAN.Equals(listSymbolType)) {
                                                                    return ArmenianNumbering.ToArmenian((int)counterValue);
                                                                }
                                                                else {
                                                                    return counterValue.ToString();
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //TODO
        public virtual String ResolveCounters(String counterName, String counterSeparatorStr, String listSymbolType
            , INode scope) {
            INode currentScope = scope;
            IList<String> resolvedCounters = null;
            while (currentScope != null) {
                INode curCounterOwnerScope = FindCounterOwner(currentScope, counterName);
                if (curCounterOwnerScope != null) {
                    if (resolvedCounters == null) {
                        resolvedCounters = new List<String>();
                    }
                    resolvedCounters.Add(ResolveCounter(counterName, listSymbolType, curCounterOwnerScope));
                }
                currentScope = curCounterOwnerScope == null ? null : curCounterOwnerScope.ParentNode();
            }
            if (resolvedCounters == null) {
                return null;
            }
            else {
                StringBuilder sb = new StringBuilder();
                for (int i = resolvedCounters.Count - 1; i >= 0; i--) {
                    sb.Append(resolvedCounters[i]);
                    if (i != 0) {
                        sb.Append(counterSeparatorStr);
                    }
                }
                return sb.ToString();
            }
        }

        public virtual void ResetCounter(String counterName, INode scope) {
            ResetCounter(counterName, DEFAULT_COUNTER_VALUE, scope);
        }

        public virtual void ResetCounter(String counterName, int value, INode scope) {
            GetOrCreateScopeCounterMap(scope).Put(counterName, value);
        }

        public virtual void IncrementCounter(String counterName, int incrementValue, INode scope) {
            IDictionary<String, int?> scopeCounters = FindSuitableScopeMap(scope, counterName);
            int? curValue = scopeCounters != null ? scopeCounters.Get(counterName) : null;
            if (curValue == null) {
                // If 'counter-increment' or 'content' on an element or pseudo-element refers to a counter that is not in the scope of any 'counter-reset',
                // implementations should behave as though a 'counter-reset' had reset the counter to 0 on that element or pseudo-element.
                curValue = DEFAULT_COUNTER_VALUE;
                ResetCounter(counterName, (int)curValue, scope);
                scopeCounters = GetOrCreateScopeCounterMap(scope);
            }
            scopeCounters.Put(counterName, curValue + incrementValue);
        }

        public virtual void IncrementCounter(String counterName, INode scope) {
            IncrementCounter(counterName, DEFAULT_INCREMENT_VALUE, scope);
        }

        private IDictionary<String, int?> GetOrCreateScopeCounterMap(INode scope) {
            IDictionary<String, int?> scopeCounters = counters.Get(scope);
            if (scopeCounters == null) {
                scopeCounters = new Dictionary<String, int?>();
                counters.Put(scope, scopeCounters);
            }
            return scopeCounters;
        }

        private IDictionary<String, int?> FindSuitableScopeMap(INode scope, String counterName) {
            INode ownerScope = FindCounterOwner(scope, counterName);
            return ownerScope == null ? null : counters.Get(ownerScope);
        }

        private INode FindCounterOwner(INode scope, String counterName) {
            while (scope != null && (!counters.ContainsKey(scope) || !counters.Get(scope).ContainsKey(counterName))) {
                // First, search through previous siblings
                bool foundSuitableSibling = false;
                if (scope.ParentNode() != null) {
                    IList<INode> allSiblings = scope.ParentNode().ChildNodes();
                    int indexOfCurScope = allSiblings.IndexOf(scope);
                    for (int i = indexOfCurScope - 1; i >= 0; i--) {
                        INode siblingScope = allSiblings[i];
                        if (counters.ContainsKey(siblingScope) && counters.Get(siblingScope).ContainsKey(counterName)) {
                            scope = siblingScope;
                            foundSuitableSibling = true;
                            break;
                        }
                    }
                }
                // If a previous sibling with matching counter was not found, move to parent scope
                if (!foundSuitableSibling) {
                    scope = scope.ParentNode();
                }
            }
            return scope;
        }
    }
}
