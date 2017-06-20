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
using System.Text;

namespace iText.Html2pdf.Css.Parse {
    public class CssDeclarationValueTokenizer {
        private String src;

        private int index = -1;

        private char stringQuote;

        private bool inString;

        private int functionDepth = 0;

        public CssDeclarationValueTokenizer(String propertyValue) {
            this.src = propertyValue;
        }

        public virtual CssDeclarationValueTokenizer.Token GetNextValidToken() {
            CssDeclarationValueTokenizer.Token token = GetNextToken();
            while (token != null && !token.IsString() && String.IsNullOrEmpty(token.GetValue().Trim())) {
                token = GetNextToken();
            }
            if (token != null && functionDepth > 0) {
                StringBuilder functionBuffer = new StringBuilder();
                while (token != null && functionDepth > 0) {
                    ProcessFunctionToken(token, functionBuffer);
                    token = GetNextToken();
                }
                functionDepth = 0;
                if (functionBuffer.Length != 0) {
                    if (token != null) {
                        ProcessFunctionToken(token, functionBuffer);
                    }
                    return new CssDeclarationValueTokenizer.Token(functionBuffer.ToString(), CssDeclarationValueTokenizer.TokenType
                        .FUNCTION);
                }
            }
            return token;
        }

        private CssDeclarationValueTokenizer.Token GetNextToken() {
            StringBuilder buff = new StringBuilder();
            char curChar;
            if (index >= src.Length - 1) {
                return null;
            }
            if (inString) {
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
                                int codePoint = System.Convert.ToInt32(pendingUnicodeSequence.ToString(), 16);
                                if (iText.IO.Util.JavaUtil.IsValidCodePoint(codePoint)) {
                                    buff.AppendCodePoint(codePoint);
                                }
                                else {
                                    buff.Append("\uFFFD");
                                }
                                pendingUnicodeSequence.Length = 0;
                                if (curChar == stringQuote) {
                                    inString = false;
                                    return new CssDeclarationValueTokenizer.Token(buff.ToString(), CssDeclarationValueTokenizer.TokenType.STRING
                                        );
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
                            return new CssDeclarationValueTokenizer.Token(buff.ToString(), CssDeclarationValueTokenizer.TokenType.STRING
                                );
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
            else {
                while (++index < src.Length) {
                    curChar = src[index];
                    if (curChar == '(') {
                        ++functionDepth;
                        buff.Append(curChar);
                    }
                    else {
                        if (curChar == ')') {
                            --functionDepth;
                            buff.Append(curChar);
                            if (functionDepth == 0) {
                                return new CssDeclarationValueTokenizer.Token(buff.ToString(), CssDeclarationValueTokenizer.TokenType.FUNCTION
                                    );
                            }
                        }
                        else {
                            if (curChar == '"' || curChar == '\'') {
                                stringQuote = curChar;
                                inString = true;
                                return new CssDeclarationValueTokenizer.Token(buff.ToString(), CssDeclarationValueTokenizer.TokenType.FUNCTION
                                    );
                            }
                            else {
                                if (curChar == ',' && !inString && functionDepth == 0) {
                                    return new CssDeclarationValueTokenizer.Token(",", CssDeclarationValueTokenizer.TokenType.COMMA);
                                }
                                else {
                                    if (iText.IO.Util.TextUtil.IsWhiteSpace(curChar)) {
                                        if (functionDepth > 0) {
                                            buff.Append(curChar);
                                        }
                                        return new CssDeclarationValueTokenizer.Token(buff.ToString(), functionDepth > 0 ? CssDeclarationValueTokenizer.TokenType
                                            .FUNCTION : CssDeclarationValueTokenizer.TokenType.UNKNOWN);
                                    }
                                    else {
                                        buff.Append(curChar);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return new CssDeclarationValueTokenizer.Token(buff.ToString(), CssDeclarationValueTokenizer.TokenType.FUNCTION
                );
        }

        private bool IsHexDigit(char c) {
            return (47 < c && c < 58) || (64 < c && c < 71) || (96 < c && c < 103);
        }

        private void ProcessFunctionToken(CssDeclarationValueTokenizer.Token token, StringBuilder functionBuffer) {
            if (token.IsString()) {
                functionBuffer.Append(stringQuote);
                functionBuffer.Append(token.GetValue());
                functionBuffer.Append(stringQuote);
            }
            else {
                functionBuffer.Append(token.GetValue());
            }
        }

        public class Token {
            private String value;

            private CssDeclarationValueTokenizer.TokenType type;

            public Token(String value, CssDeclarationValueTokenizer.TokenType type) {
                this.value = value;
                this.type = type;
            }

            public virtual String GetValue() {
                return value;
            }

            public virtual CssDeclarationValueTokenizer.TokenType GetType() {
                return type;
            }

            public virtual bool IsString() {
                return type == CssDeclarationValueTokenizer.TokenType.STRING;
            }

            public override String ToString() {
                return value;
            }
        }

        public enum TokenType {
            STRING,
            FUNCTION,
            COMMA,
            UNKNOWN
        }
    }
}
