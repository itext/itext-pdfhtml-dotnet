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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Parse;
using iText.Html2pdf.Css.Pseudo;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve {
    /// <summary>The Class CssContentPropertyResolver.</summary>
    internal class CssContentPropertyResolver {
        /// <summary>The logger.</summary>
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(CssContentPropertyResolver));

        /// <summary>Resolves content.</summary>
        /// <param name="styles">the styles map</param>
        /// <param name="contentContainer">the content container</param>
        /// <param name="context">the CSS context</param>
        /// <returns>
        /// a list of
        /// <see cref="iNode"/>
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
                }
                else {
                    if (token.GetValue().StartsWith(CssConstants.COUNTERS + "(")) {
                        String paramsStr = token.GetValue().JSubstring(CssConstants.COUNTERS.Length + 1, token.GetValue().Length -
                             1);
                        String[] @params = iText.IO.Util.StringUtil.Split(paramsStr, ",");
                        if (@params.Length == 0) {
                            return null;
                        }
                        // Counters are denoted by case-sensitive identifiers
                        String counterName = @params[0].Trim();
                        String counterSeparationStr = @params[1].Trim();
                        counterSeparationStr = counterSeparationStr.JSubstring(1, counterSeparationStr.Length - 1);
                        String listStyleType = @params.Length > 2 ? @params[2].Trim() : null;
                        CssCounterManager counterManager = context.GetCounterManager();
                        INode scope = contentContainer;
                        if ("page".Equals(counterName)) {
                            result.Add(new PageCountElementNode(false));
                        }
                        else {
                            if ("pages".Equals(counterName)) {
                                result.Add(new PageCountElementNode(true));
                            }
                            else {
                                String resolvedCounter = counterManager.ResolveCounters(counterName, counterSeparationStr, listStyleType, 
                                    scope);
                                if (resolvedCounter == null) {
                                    logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RESOLVE_COUNTER, counterName));
                                }
                                else {
                                    result.Add(new CssContentPropertyResolver.ContentTextNode(scope, resolvedCounter));
                                }
                            }
                        }
                    }
                    else {
                        if (token.GetValue().StartsWith(CssConstants.COUNTER + "(")) {
                            String paramsStr = token.GetValue().JSubstring(CssConstants.COUNTER.Length + 1, token.GetValue().Length - 
                                1);
                            String[] @params = iText.IO.Util.StringUtil.Split(paramsStr, ",");
                            if (@params.Length == 0) {
                                return null;
                            }
                            // Counters are denoted by case-sensitive identifiers
                            String counterName = @params[0].Trim();
                            String listStyleType = @params.Length > 1 ? @params[1].Trim() : null;
                            CssCounterManager counterManager = context.GetCounterManager();
                            INode scope = contentContainer;
                            if ("page".Equals(counterName)) {
                                result.Add(new PageCountElementNode(false));
                            }
                            else {
                                if ("pages".Equals(counterName)) {
                                    result.Add(new PageCountElementNode(true));
                                }
                                else {
                                    String resolvedCounter = counterManager.ResolveCounter(counterName, listStyleType, scope);
                                    if (resolvedCounter == null) {
                                        logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RESOLVE_COUNTER, counterName));
                                    }
                                    else {
                                        result.Add(new CssContentPropertyResolver.ContentTextNode(scope, resolvedCounter));
                                    }
                                }
                            }
                        }
                        else {
                            if (token.GetValue().StartsWith("url(")) {
                                IDictionary<String, String> attributes = new Dictionary<String, String>();
                                attributes.Put(AttributeConstants.SRC, CssUtils.ExtractUrl(token.GetValue()));
                                //TODO: probably should add user agent styles on CssContentElementNode creation, not here.
                                attributes.Put(AttributeConstants.STYLE, CssConstants.DISPLAY + ":" + CssConstants.INLINE_BLOCK);
                                result.Add(new CssContentElementNode(contentContainer, TagConstants.IMG, attributes));
                            }
                            else {
                                if (token.GetValue().StartsWith("attr(") && contentContainer is CssPseudoElementNode) {
                                    int endBracket = token.GetValue().IndexOf(')');
                                    if (endBracket > 5) {
                                        String attrName = token.GetValue().JSubstring(5, endBracket);
                                        if (attrName.Contains("(") || attrName.Contains(" ") || attrName.Contains("'") || attrName.Contains("\"")) {
                                            return ErrorFallback(contentStr);
                                        }
                                        IElementNode element = (IElementNode)contentContainer.ParentNode();
                                        String value = element.GetAttribute(attrName);
                                        result.Add(new CssContentPropertyResolver.ContentTextNode(contentContainer, value == null ? "" : value));
                                    }
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
                                        return ErrorFallback(contentStr);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>Resolves content in case of errors.</summary>
        /// <param name="contentStr">the content</param>
        /// <returns>
        /// the resulting list of
        /// <see cref="iNode"/>
        /// instances
        /// </returns>
        private static IList<INode> ErrorFallback(String contentStr) {
            ILogger logger = LoggerFactory.GetLogger(typeof(CssContentPropertyResolver));
            int logMessageParameterMaxLength = 100;
            if (contentStr.Length > logMessageParameterMaxLength) {
                contentStr = contentStr.JSubstring(0, logMessageParameterMaxLength) + ".....";
            }
            logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.CONTENT_PROPERTY_INVALID, contentStr));
            return null;
        }

        /// <summary>
        /// <see cref="iTextNode"/>
        /// implementation for content text.
        /// </summary>
        private class ContentTextNode : ITextNode {
            /// <summary>The parent.</summary>
            private readonly INode parent;

            /// <summary>The content.</summary>
            private String content;

            /// <summary>Creates a new <code>ContentTextNode</code> instance.</summary>
            /// <param name="parent">the parent</param>
            /// <param name="content">the content</param>
            public ContentTextNode(INode parent, String content) {
                this.parent = parent;
                this.content = content;
            }

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
}
