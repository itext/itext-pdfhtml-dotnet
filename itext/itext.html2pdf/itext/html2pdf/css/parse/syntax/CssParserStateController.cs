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
using iText.Html2pdf.Css.Parse;
using iText.IO.Log;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Parse.Syntax {
    public sealed class CssParserStateController {
        private IParserState currentState;

        private bool isCurrentRuleSupported = true;

        private IParserState previousActiveState;

        private StringBuilder buffer = new StringBuilder();

        private String currentSelector;

        private CssStyleSheet styleSheet;

        private Stack<CssNestedAtRule> nestedAtRules;

        private static readonly ICollection<String> SUPPORTED_RULES = JavaCollectionsUtil.UnmodifiableSet(new HashSet
            <String>(iText.IO.Util.JavaUtil.ArraysAsList(CssConstants.MEDIA)));

        private readonly IParserState commentStartState;

        private readonly IParserState commendEndState;

        private readonly IParserState commendInnerState;

        private readonly IParserState unknownState;

        private readonly IParserState ruleState;

        private readonly IParserState propertiesState;

        private readonly IParserState atRuleBlockState;

        public CssParserStateController() {
            //Hashed value
            // Non-comment
            styleSheet = new CssStyleSheet();
            nestedAtRules = new Stack<CssNestedAtRule>();
            commentStartState = new CommentStartState(this);
            commendEndState = new CommentEndState(this);
            commendInnerState = new CommentInnerState(this);
            unknownState = new UnknownState(this);
            ruleState = new RuleState(this);
            propertiesState = new PropertiesState(this);
            atRuleBlockState = new AtRuleBlockState(this);
            currentState = unknownState;
        }

        public void Process(char ch) {
            currentState.Process(ch);
        }

        public CssStyleSheet GetParsingResult() {
            return styleSheet;
        }

        internal void AppendToBuffer(char ch) {
            buffer.Append(ch);
        }

        internal void EnterPreviousActiveState() {
            SetState(previousActiveState);
        }

        internal void EnterCommentStartState() {
            SaveActiveState();
            SetState(commentStartState);
        }

        internal void EnterCommentEndState() {
            SetState(commendEndState);
        }

        internal void EnterCommentInnerState() {
            SetState(commendInnerState);
        }

        internal void EnterRuleState() {
            SetState(ruleState);
        }

        internal void EnterUnknownStateIfNestedBlocksFinished() {
            if (nestedAtRules.Count == 0) {
                SetState(unknownState);
            }
            else {
                SetState(atRuleBlockState);
            }
        }

        internal void EnterUnknownState() {
            SetState(unknownState);
        }

        internal void EnterAtRuleBlockState() {
            SetState(atRuleBlockState);
        }

        internal void EnterPropertiesState() {
            SetState(propertiesState);
        }

        internal void StoreCurrentSelector() {
            currentSelector = buffer.ToString();
            buffer.Length = 0;
        }

        internal void StoreCurrentProperties() {
            if (isCurrentRuleSupported) {
                ProcessProperties(currentSelector, buffer.ToString());
            }
            currentSelector = null;
            buffer.Length = 0;
        }

        internal void StoreSemicolonAtRule() {
            if (isCurrentRuleSupported) {
                ProcessSemicolonAtRule(buffer.ToString());
            }
            buffer.Length = 0;
        }

        internal void FinishAtRuleBlock() {
            CssNestedAtRule atRule = nestedAtRules.Pop();
            if (isCurrentRuleSupported) {
                ProcessFinishedAtRuleBlock(atRule);
            }
            isCurrentRuleSupported = IsCurrentRuleSupported();
            buffer.Length = 0;
        }

        internal void PushBlockPrecedingAtRule() {
            nestedAtRules.Push(CssNestedAtRuleFactory.CreateNestedRule(buffer.ToString()));
            isCurrentRuleSupported = IsCurrentRuleSupported();
            buffer.Length = 0;
        }

        private void SaveActiveState() {
            previousActiveState = currentState;
        }

        private void SetState(IParserState state) {
            currentState = state;
        }

        private void ProcessProperties(String selector, String properties) {
            IList<CssRuleSet> ruleSets = CssRuleSetParser.ParseRuleSet(selector, properties);
            foreach (CssRuleSet ruleSet in ruleSets) {
                if (nestedAtRules.Count == 0) {
                    styleSheet.AddStatement(ruleSet);
                }
                else {
                    nestedAtRules.Peek().AddStatementToBody(ruleSet);
                }
            }
        }

        private void ProcessSemicolonAtRule(String ruleStr) {
            CssSemicolonAtRule atRule = new CssSemicolonAtRule(ruleStr);
            styleSheet.AddStatement(atRule);
        }

        private void ProcessFinishedAtRuleBlock(CssNestedAtRule atRule) {
            if (nestedAtRules.Count != 0) {
                nestedAtRules.Peek().AddStatementToBody(atRule);
            }
            else {
                styleSheet.AddStatement(atRule);
            }
        }

        private bool IsCurrentRuleSupported() {
            bool isSupported = nestedAtRules.IsEmpty() || SUPPORTED_RULES.Contains(nestedAtRules.Peek().GetRuleName());
            if (!isSupported) {
                LoggerFactory.GetLogger(GetType()).Error(String.Format(iText.Html2pdf.LogMessageConstant.RULE_IS_NOT_SUPPORTED
                    , nestedAtRules.Peek().GetRuleName()));
            }
            return isSupported;
        }
    }
}
