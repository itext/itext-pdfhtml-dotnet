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
using System.Collections;
using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.IO.Log;

namespace iText.Html2pdf.Css.Resolve {
    public class CssQuotes {
        private const String EMPTY_QUOTE = "";

        private List<String> openQuotes = new List<String>();

        private List<String> closeQuotes = new List<String>();

        public static iText.Html2pdf.Css.Resolve.CssQuotes CreateQuotes(String quotesString, bool fallbackToDefault
            ) {
            bool error = false;
            List<List<String>> quotes = new List<List<String>>(2);
            quotes.Add(new List<String>());
            quotes.Add(new List<String>());
            if (quotesString != null) {
                if (quotesString.Equals(CssConstants.NONE)) {
                    quotes[0].Add(EMPTY_QUOTE);
                    quotes[1].Add(EMPTY_QUOTE);
                    return new iText.Html2pdf.Css.Resolve.CssQuotes(quotes[0], quotes[1]);
                }
                CssContentTokenizer tokenizer = new CssContentTokenizer(quotesString);
                CssContentTokenizer.ContentToken token;
                for (int i = 0; ((token = tokenizer.GetNextValidToken()) != null); ++i) {
                    if (token.IsString()) {
                        quotes[i % 2].Add(token.GetValue());
                    }
                    else {
                        error = true;
                        break;
                    }
                }
                if (quotes[0].Count == quotes[1].Count && !quotes[0].IsEmpty() && !error) {
                    return new iText.Html2pdf.Css.Resolve.CssQuotes(quotes[0], quotes[1]);
                }
                else {
                    LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Resolve.CssQuotes)).Error(String.Format(iText.Html2pdf.LogMessageConstant
                        .QUOTES_PROPERTY_INVALID, quotesString));
                }
            }
            return fallbackToDefault ? CreateDefaultQuotes() : null;
        }

        public static iText.Html2pdf.Css.Resolve.CssQuotes CreateDefaultQuotes() {
            List<String> openQuotes = new List<String>();
            List<String> closeQuotes = new List<String>();
            openQuotes.Add("\u00ab");
            closeQuotes.Add("\u00bb");
            return new iText.Html2pdf.Css.Resolve.CssQuotes(openQuotes, closeQuotes);
        }

        public CssQuotes(List<String> openQuotes, List<String> closeQuotes) {
            this.openQuotes = openQuotes;
            this.closeQuotes = closeQuotes;
        }

        public virtual String ResolveQuote(String value, CssContext context) {
            int depth = context.GetQuotesDepth();
            if (CssConstants.OPEN_QUOTE.Equals(value)) {
                IncreaseDepth(context);
                return GetQuote(depth, openQuotes);
            }
            else {
                if (CssConstants.CLOSE_QUOTE.Equals(value)) {
                    DecreaseDepth(context);
                    return GetQuote(depth - 1, closeQuotes);
                }
                else {
                    if (CssConstants.NO_OPEN_QUOTE.Equals(value)) {
                        IncreaseDepth(context);
                        return EMPTY_QUOTE;
                    }
                    else {
                        if (CssConstants.NO_CLOSE_QUOTE.Equals(value)) {
                            DecreaseDepth(context);
                            return EMPTY_QUOTE;
                        }
                    }
                }
            }
            return null;
        }

        private void IncreaseDepth(CssContext context) {
            context.SetQuotesDepth(context.GetQuotesDepth() + 1);
        }

        private void DecreaseDepth(CssContext context) {
            if (context.GetQuotesDepth() > 0) {
                context.SetQuotesDepth(context.GetQuotesDepth() - 1);
            }
        }

        private String GetQuote(int depth, List<String> quotes) {
            if (depth >= quotes.Count) {
                return quotes[quotes.Count - 1];
            }
            if (depth < 0) {
                return EMPTY_QUOTE;
            }
            return quotes[depth];
        }
    }
}
