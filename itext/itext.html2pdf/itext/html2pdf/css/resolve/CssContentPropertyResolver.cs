/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Parse;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Css.Resolve;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Resolve {
//\cond DO_NOT_DOCUMENT
    /// <summary>The Class CssContentPropertyResolver.</summary>
    internal class CssContentPropertyResolver {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(CssContentPropertyResolver));

        private static readonly EscapeGroup[] ALLOWED_ESCAPE_CHARACTERS = new EscapeGroup[] { new EscapeGroup('\''
            ), new EscapeGroup('\"') };

        private const int COUNTERS_MIN_PARAMS_SIZE = 2;

        private const int COUNTER_MIN_PARAMS_SIZE = 1;

        private const int TARGET_COUNTERS_MIN_PARAMS_SIZE = 3;

        private const int TARGET_COUNTER_MIN_PARAMS_SIZE = 2;

//\cond DO_NOT_DOCUMENT
        /// <summary>Resolves content.</summary>
        /// <param name="styles">the styles map</param>
        /// <param name="contentContainer">the content container</param>
        /// <param name="context">the CSS context</param>
        /// <returns>
        /// a list of
        /// <see cref="iText.StyledXmlParser.Node.INode"/>
        /// instances
        /// </returns>
        internal static IList<INode> ResolveContent(IDictionary<String, String> styles, INode contentContainer, CssContext
             context) {
            String contentStr = styles.Get(CssConstants.CONTENT);
            IList<INode> result = new List<INode>();
            if (contentStr == null || CssConstants.NONE.Equals(contentStr) || CssConstants.NORMAL.Equals(contentStr)) {
                return null;
            }
            CssDeclarationValueTokenizer tokenizer = new CssDeclarationValueTokenizer(contentStr);
            CssDeclarationValueTokenizer.Token token;
            CssQuotes quotes = null;
            while ((token = tokenizer.GetNextValidToken()) != null) {
                if (token.IsString()) {
                    result.Add(new CssContentPropertyResolver.ContentTextNode(contentContainer, token.GetValue()));
                    continue;
                }
                if (token.GetValue().StartsWith(CssConstants.COUNTERS + "(")) {
                    String paramsStr = token.GetValue().JSubstring(CssConstants.COUNTERS.Length + 1, token.GetValue().Length -
                         1);
                    IList<String> @params = CssUtils.SplitString(paramsStr, ',', ALLOWED_ESCAPE_CHARACTERS);
                    if (@params.Count < COUNTERS_MIN_PARAMS_SIZE) {
                        return ErrorFallback(contentStr);
                    }
                    // Counters are denoted by case-sensitive identifiers
                    String counterName = @params[0].Trim();
                    String counterSeparationStr = @params[1].Trim();
                    counterSeparationStr = counterSeparationStr.JSubstring(1, counterSeparationStr.Length - 1);
                    CounterDigitsGlyphStyle listStyleType = HtmlUtils.ConvertStringCounterGlyphStyleToEnum(@params.Count > COUNTERS_MIN_PARAMS_SIZE
                         ? @params[COUNTERS_MIN_PARAMS_SIZE].Trim() : null);
                    CssCounterManager counterManager = context.GetCounterManager();
                    INode scope = contentContainer;
                    if (CssConstants.PAGE.Equals(counterName)) {
                        result.Add(new PageCountElementNode(false, contentContainer).SetDigitsGlyphStyle(listStyleType));
                    }
                    else {
                        if (CssConstants.PAGES.Equals(counterName)) {
                            result.Add(new PageCountElementNode(true, contentContainer).SetDigitsGlyphStyle(listStyleType));
                        }
                        else {
                            String resolvedCounter = counterManager.ResolveCounters(counterName, counterSeparationStr, listStyleType);
                            result.Add(new CssContentPropertyResolver.ContentTextNode(scope, resolvedCounter));
                        }
                    }
                }
                else {
                    if (token.GetValue().StartsWith(CssConstants.COUNTER + "(")) {
                        String paramsStr = token.GetValue().JSubstring(CssConstants.COUNTER.Length + 1, token.GetValue().Length - 
                            1);
                        IList<String> @params = CssUtils.SplitString(paramsStr, ',', ALLOWED_ESCAPE_CHARACTERS);
                        if (@params.Count < COUNTER_MIN_PARAMS_SIZE) {
                            return ErrorFallback(contentStr);
                        }
                        // Counters are denoted by case-sensitive identifiers
                        String counterName = @params[0].Trim();
                        CounterDigitsGlyphStyle listStyleType = HtmlUtils.ConvertStringCounterGlyphStyleToEnum(@params.Count > COUNTER_MIN_PARAMS_SIZE
                             ? @params[COUNTER_MIN_PARAMS_SIZE].Trim() : null);
                        CssCounterManager counterManager = context.GetCounterManager();
                        INode scope = contentContainer;
                        if (CssConstants.PAGE.Equals(counterName)) {
                            result.Add(new PageCountElementNode(false, contentContainer).SetDigitsGlyphStyle(listStyleType));
                        }
                        else {
                            if (CssConstants.PAGES.Equals(counterName)) {
                                result.Add(new PageCountElementNode(true, contentContainer).SetDigitsGlyphStyle(listStyleType));
                            }
                            else {
                                String resolvedCounter = counterManager.ResolveCounter(counterName, listStyleType);
                                result.Add(new CssContentPropertyResolver.ContentTextNode(scope, resolvedCounter));
                            }
                        }
                    }
                    else {
                        if (token.GetValue().StartsWith(CssConstants.TARGET_COUNTER + "(")) {
                            String paramsStr = token.GetValue().JSubstring(CssConstants.TARGET_COUNTER.Length + 1, token.GetValue().Length
                                 - 1);
                            IList<String> @params = CssUtils.SplitString(paramsStr, ',', ALLOWED_ESCAPE_CHARACTERS);
                            if (@params.Count < TARGET_COUNTER_MIN_PARAMS_SIZE) {
                                return ErrorFallback(contentStr);
                            }
                            String target = @params[0].StartsWith(CommonCssConstants.ATTRIBUTE + "(") ? CssUtils.ExtractAttributeValue
                                (@params[0], (IElementNode)contentContainer.ParentNode()) : CssUtils.ExtractUrl(@params[0]);
                            if (target == null) {
                                return ErrorFallback(contentStr);
                            }
                            String counterName = @params[1].Trim();
                            CounterDigitsGlyphStyle listStyleType = HtmlUtils.ConvertStringCounterGlyphStyleToEnum(@params.Count > TARGET_COUNTER_MIN_PARAMS_SIZE
                                 ? @params[TARGET_COUNTER_MIN_PARAMS_SIZE].Trim() : null);
                            if (CssConstants.PAGE.Equals(counterName)) {
                                result.Add(new PageTargetCountElementNode(contentContainer, target).SetDigitsGlyphStyle(listStyleType));
                            }
                            else {
                                if (CssConstants.PAGES.Equals(counterName)) {
                                    result.Add(new PageCountElementNode(true, contentContainer).SetDigitsGlyphStyle(listStyleType));
                                }
                                else {
                                    String counter = context.GetCounterManager().ResolveTargetCounter(target.Replace("'", "").Replace("#", "")
                                        , counterName, listStyleType);
                                    CssContentPropertyResolver.ContentTextNode node = new CssContentPropertyResolver.ContentTextNode(contentContainer
                                        , counter == null ? "0" : counter);
                                    result.Add(node);
                                }
                            }
                        }
                        else {
                            if (token.GetValue().StartsWith(CssConstants.TARGET_COUNTERS + "(")) {
                                String paramsStr = token.GetValue().JSubstring(CssConstants.TARGET_COUNTERS.Length + 1, token.GetValue().Length
                                     - 1);
                                IList<String> @params = CssUtils.SplitString(paramsStr, ',', ALLOWED_ESCAPE_CHARACTERS);
                                if (@params.Count < TARGET_COUNTERS_MIN_PARAMS_SIZE) {
                                    return ErrorFallback(contentStr);
                                }
                                String target = @params[0].StartsWith(CommonCssConstants.ATTRIBUTE + "(") ? CssUtils.ExtractAttributeValue
                                    (@params[0], (IElementNode)contentContainer.ParentNode()) : CssUtils.ExtractUrl(@params[0]);
                                if (target == null) {
                                    return ErrorFallback(contentStr);
                                }
                                String counterName = @params[1].Trim();
                                String counterSeparator = @params[2].Trim();
                                counterSeparator = counterSeparator.JSubstring(1, counterSeparator.Length - 1);
                                CounterDigitsGlyphStyle listStyleType = HtmlUtils.ConvertStringCounterGlyphStyleToEnum(@params.Count > TARGET_COUNTERS_MIN_PARAMS_SIZE
                                     ? @params[TARGET_COUNTERS_MIN_PARAMS_SIZE].Trim() : null);
                                if (CssConstants.PAGE.Equals(counterName)) {
                                    result.Add(new PageTargetCountElementNode(contentContainer, target).SetDigitsGlyphStyle(listStyleType));
                                }
                                else {
                                    if (CssConstants.PAGES.Equals(counterName)) {
                                        result.Add(new PageCountElementNode(true, contentContainer).SetDigitsGlyphStyle(listStyleType));
                                    }
                                    else {
                                        String counters = context.GetCounterManager().ResolveTargetCounters(target.Replace(",", "").Replace("#", ""
                                            ), counterName, counterSeparator, listStyleType);
                                        CssContentPropertyResolver.ContentTextNode node = new CssContentPropertyResolver.ContentTextNode(contentContainer
                                            , counters == null ? "0" : counters);
                                        result.Add(node);
                                    }
                                }
                            }
                            else {
                                if (token.GetValue().StartsWith("url(")) {
                                    IDictionary<String, String> attributes = new Dictionary<String, String>();
                                    attributes.Put(AttributeConstants.SRC, CssUtils.ExtractUrl(token.GetValue()));
                                    attributes.Put(AttributeConstants.STYLE, CssConstants.DISPLAY + ":" + CssConstants.INLINE_BLOCK);
                                    result.Add(new CssContentElementNode(contentContainer, TagConstants.IMG, attributes));
                                }
                                else {
                                    if (CssGradientUtil.IsCssLinearGradientValue(token.GetValue())) {
                                        IDictionary<String, String> attributes = new Dictionary<String, String>();
                                        attributes.Put(AttributeConstants.STYLE, CssConstants.BACKGROUND_IMAGE + ":" + token.GetValue() + ";" + CssConstants
                                            .HEIGHT + ":" + CssConstants.INHERIT + ";" + CssConstants.WIDTH + ":" + CssConstants.INHERIT + ";");
                                        result.Add(new CssContentElementNode(contentContainer, TagConstants.DIV, attributes));
                                    }
                                    else {
                                        if (token.GetValue().StartsWith(CommonCssConstants.ATTRIBUTE + "(") && contentContainer is CssPseudoElementNode
                                            ) {
                                            String value = CssUtils.ExtractAttributeValue(token.GetValue(), (IElementNode)contentContainer.ParentNode(
                                                ));
                                            if (value == null) {
                                                return ErrorFallback(contentStr);
                                            }
                                            result.Add(new CssContentPropertyResolver.ContentTextNode(contentContainer, value));
                                        }
                                        else {
                                            if (token.GetValue().EndsWith("quote") && contentContainer is IStylesContainer) {
                                                if (quotes == null) {
                                                    quotes = CssQuotes.CreateQuotes(styles.Get(CssConstants.QUOTES), true);
                                                }
                                                String value = quotes.ResolveQuote(token.GetValue(), context);
                                                if (value == null) {
                                                    return ErrorFallback(contentStr);
                                                }
                                                result.Add(new CssContentPropertyResolver.ContentTextNode(contentContainer, value));
                                            }
                                            else {
                                                if (token.GetValue().StartsWith(CssConstants.ELEMENT + "(") && contentContainer is PageMarginBoxContextNode
                                                    ) {
                                                    String paramsStr = token.GetValue().JSubstring(CssConstants.ELEMENT.Length + 1, token.GetValue().Length - 
                                                        1);
                                                    String[] @params = iText.Commons.Utils.StringUtil.Split(paramsStr, ",");
                                                    if (@params.Length == 0) {
                                                        return ErrorFallback(contentStr);
                                                    }
                                                    String name = @params[0].Trim();
                                                    String runningElementOccurrence = null;
                                                    if (@params.Length > 1) {
                                                        runningElementOccurrence = @params[1].Trim();
                                                    }
                                                    result.Add(new PageMarginRunningElementNode(name, runningElementOccurrence));
                                                }
                                                else {
                                                    return ErrorFallback(contentStr);
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
            return result;
        }
//\endcond

        /// <summary>Resolves content in case of errors.</summary>
        /// <param name="contentStr">the content</param>
        /// <returns>
        /// the resulting list of
        /// <see cref="iText.StyledXmlParser.Node.INode"/>
        /// instances
        /// </returns>
        private static IList<INode> ErrorFallback(String contentStr) {
            int logMessageParameterMaxLength = 100;
            if (contentStr.Length > logMessageParameterMaxLength) {
                contentStr = contentStr.JSubstring(0, logMessageParameterMaxLength) + ".....";
            }
            LOGGER.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, contentStr));
            return null;
        }

        /// <summary>
        /// <see cref="iText.StyledXmlParser.Node.ITextNode"/>
        /// implementation for content text.
        /// </summary>
        private class ContentTextNode : ITextNode {
            /// <summary>The parent.</summary>
            private readonly INode parent;

            /// <summary>The content.</summary>
            private String content;

//\cond DO_NOT_DOCUMENT
            /// <summary>
            /// Creates a new
            /// <see cref="ContentTextNode"/>
            /// instance.
            /// </summary>
            /// <param name="parent">the parent</param>
            /// <param name="content">the content</param>
            internal ContentTextNode(INode parent, String content) {
                this.parent = parent;
                this.content = content;
            }
//\endcond

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.html.node.INode#childNodes()
            */
            public virtual IList<INode> ChildNodes() {
                return JavaCollectionsUtil.EmptyList<INode>();
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.html.node.INode#addChild(com.itextpdf.html2pdf.html.node.INode)
            */
            public virtual void AddChild(INode node) {
                throw new NotSupportedException();
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.html.node.INode#parentNode()
            */
            public virtual INode ParentNode() {
                return parent;
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.html.node.ITextNode#wholeText()
            */
            public virtual String WholeText() {
                return content;
            }
        }
    }
//\endcond
}
