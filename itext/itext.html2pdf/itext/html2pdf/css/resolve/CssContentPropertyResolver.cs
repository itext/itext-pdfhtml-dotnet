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
using iText.Html2pdf.Css.Pseudo;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve {
    internal class CssContentPropertyResolver {
        internal static IList<INode> ResolveContent(String contentStr, INode contentContainer, CssContext context) {
            List<INode> result = new List<INode>();
            if (contentStr == null || CssConstants.NONE.Equals(contentStr) || CssConstants.NORMAL.Equals(contentStr)) {
                return null;
            }
            CssContentPropertyResolver.ContentListTokenizer tokenizer = new CssContentPropertyResolver.ContentListTokenizer
                (contentStr);
            CssContentPropertyResolver.ContentToken token;
            while ((token = tokenizer.GetNextValidToken()) != null) {
                if (!token.IsString()) {
                    if (token.GetValue().StartsWith("url(")) {
                        Dictionary<String, String> attributes = new Dictionary<String, String>();
                        attributes.Put(AttributeConstants.SRC, CssUtils.ExtractUrl(token.GetValue()));
                        //TODO: probably should add user agent styles on CssContentElementNode creation, not here.
                        attributes.Put(AttributeConstants.STYLE, "display:inline-block;");
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
                                result.Add(new CssContentPropertyResolver.ContentTextNode(contentContainer, element.GetAttribute(attrName)
                                    ));
                            }
                        }
                        else {
                            return ErrorFallback(contentStr);
                        }
                    }
                }
                else {
                    result.Add(new CssContentPropertyResolver.ContentTextNode(contentContainer, token.GetValue()));
                }
            }
            return result;
        }

        private static IList<INode> ErrorFallback(String contentStr) {
            ILogger logger = LoggerFactory.GetLogger(typeof(CssContentPropertyResolver));
            int logMessageParameterMaxLength = 100;
            if (contentStr.Length > logMessageParameterMaxLength) {
                contentStr = contentStr.JSubstring(0, logMessageParameterMaxLength) + ".....";
            }
            logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.CONTENT_PROPERTY_INVALID, contentStr));
            return null;
        }

        private class ContentTextNode : ITextNode {
            private readonly INode parent;

            private String content;

            public ContentTextNode(INode parent, String content) {
                this.parent = parent;
                this.content = content;
            }

            public virtual IList<INode> ChildNodes() {
                return JavaCollectionsUtil.EmptyList<INode>();
            }

            public virtual void AddChild(INode node) {
                throw new NotSupportedException();
            }

            public virtual INode ParentNode() {
                return parent;
            }

            public virtual String WholeText() {
                return content;
            }
        }

        private class ContentListTokenizer {
            private String src;

            private int index;

            private char stringQuote;

            private bool inString;

            public ContentListTokenizer(String src) {
                this.src = src;
                index = -1;
            }

            public virtual CssContentPropertyResolver.ContentToken GetNextValidToken() {
                CssContentPropertyResolver.ContentToken token = GetNextToken();
                while (token != null && !token.IsString() && String.IsNullOrEmpty(token.GetValue().Trim())) {
                    token = GetNextToken();
                }
                return token;
            }

            private CssContentPropertyResolver.ContentToken GetNextToken() {
                StringBuilder buff = new StringBuilder();
                char curChar;
                if (index >= src.Length - 1) {
                    return null;
                }
                if (!inString) {
                    while (++index < src.Length) {
                        curChar = src[index];
                        if (curChar == '(') {
                            int closeBracketIndex = src.IndexOf(')', index);
                            if (closeBracketIndex == -1) {
                                closeBracketIndex = src.Length - 1;
                            }
                            buff.Append(src.JSubstring(index, closeBracketIndex + 1));
                            index = closeBracketIndex;
                        }
                        else {
                            if (curChar == '"' || curChar == '\'') {
                                stringQuote = curChar;
                                inString = true;
                                return new CssContentPropertyResolver.ContentToken(buff.ToString(), false);
                            }
                            else {
                                if (iText.IO.Util.TextUtil.IsWhiteSpace(curChar)) {
                                    return new CssContentPropertyResolver.ContentToken(buff.ToString(), false);
                                }
                                else {
                                    buff.Append(curChar);
                                }
                            }
                        }
                    }
                }
                else {
                    bool isEscaped = false;
                    StringBuilder pendingUnicodeSequence = new StringBuilder();
                    while (++index < src.Length) {
                        curChar = src[index];
                        if (isEscaped) {
                            if (IsHexDigit(curChar) && pendingUnicodeSequence.Length < 6) {
                                pendingUnicodeSequence.Append(curChar);
                            }
                            else {
                                if (pendingUnicodeSequence.Length != 0) {
                                    buff.AppendCodePoint(System.Convert.ToInt32(pendingUnicodeSequence.ToString(), 16));
                                    pendingUnicodeSequence.Length = 0;
                                    if (curChar == stringQuote) {
                                        inString = false;
                                        return new CssContentPropertyResolver.ContentToken(buff.ToString(), true);
                                    }
                                    else {
                                        if (!iText.IO.Util.TextUtil.IsWhiteSpace(curChar)) {
                                            buff.Append(curChar);
                                        }
                                    }
                                    isEscaped = false;
                                }
                                else {
                                    buff.Append(curChar);
                                    isEscaped = false;
                                }
                            }
                        }
                        else {
                            if (curChar == stringQuote) {
                                inString = false;
                                return new CssContentPropertyResolver.ContentToken(buff.ToString(), true);
                            }
                            else {
                                if (curChar == '\\') {
                                    isEscaped = true;
                                }
                                else {
                                    buff.Append(curChar);
                                }
                            }
                        }
                    }
                }
                return new CssContentPropertyResolver.ContentToken(buff.ToString(), false);
            }

            private bool IsHexDigit(char c) {
                return (47 < c && c < 58) || (64 < c && c < 71) || (96 < c && c < 103);
            }
        }

        private class ContentToken {
            private String value;

            private bool isString;

            public ContentToken(String value, bool isString) {
                this.value = value;
                this.isString = isString;
            }

            public virtual String GetValue() {
                return value;
            }

            public virtual bool IsString() {
                return isString;
            }

            public override String ToString() {
                return value;
            }
        }
    }
}
