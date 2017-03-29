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
using iText.Html2pdf.Css;

namespace iText.Html2pdf.Attach.Impl {
    internal class FontFace {
        private readonly String alias;

        private readonly IList<FontFace.FontFaceSrc> sources;

        public static iText.Html2pdf.Attach.Impl.FontFace Create(IList<CssDeclaration> properties) {
            String fontFamily = null;
            String srcs = null;
            foreach (CssDeclaration descriptor in properties) {
                if ("font-family".Equals(descriptor.GetProperty())) {
                    fontFamily = descriptor.GetExpression();
                }
                else {
                    if ("src".Equals(descriptor.GetProperty())) {
                        srcs = descriptor.GetExpression();
                    }
                }
            }
            if (fontFamily == null || srcs == null) {
                // 'font-family' and 'src' is required according to spec:
                // https://www.w3.org/TR/2013/CR-css-fonts-3-20131003/#descdef-font-family\
                // https://www.w3.org/TR/2013/CR-css-fonts-3-20131003/#descdef-src
                return null;
            }
            IList<FontFace.FontFaceSrc> sources = new List<FontFace.FontFaceSrc>();
            // ttc collection are supported via url(Arial.ttc#1), url(Arial.ttc#2), etc.
            foreach (String src in iText.IO.Util.StringUtil.Split(srcs, ",")) {
                //local|url("ideal-sans-serif.woff")( format("woff"))?
                FontFace.FontFaceSrc source = FontFace.FontFaceSrc.Create(src);
                if (source != null) {
                    sources.Add(source);
                }
            }
            if (sources.Count > 0) {
                return new iText.Html2pdf.Attach.Impl.FontFace(fontFamily, sources);
            }
            else {
                return null;
            }
        }

        /// <summary>Actually font-family is an alias.</summary>
        public virtual String GetFontFamily() {
            return alias;
        }

        public virtual IList<FontFace.FontFaceSrc> GetSources() {
            return sources;
        }

        private FontFace(String alias, IList<FontFace.FontFaceSrc> sources) {
            this.alias = alias;
            this.sources = sources;
        }

        internal class FontFaceSrc {
            internal static Regex UrlPattern = iText.IO.Util.StringUtil.RegexCompile("^((local)|(url))\\(((\'[^\']*\')|(\"[^\"]*\")|([^\'\"\\)]*))\\)( format\\(((\'[^\']*\')|(\"[^\"]*\")|([^\'\"\\)]*))\\))?$"
                );

            internal static int TypeGroup = 1;

            internal static int UrlGroup = 4;

            internal static int FormatGroup = 9;

            internal readonly FontFace.FontFormat format;

            internal readonly String src;

            internal readonly bool isLocal;

            //region Nested types
            public override String ToString() {
                return String.Format("%s(%s)%s", isLocal ? "local" : "url", src, format != FontFace.FontFormat.None ? String
                    .Format(" format(%s)", format) : "");
            }

            internal static FontFace.FontFaceSrc Create(String src) {
                Match m = iText.IO.Util.StringUtil.Match(UrlPattern, src);
                if (!m.Success) {
                    return null;
                }
                return new FontFace.FontFaceSrc(Unquote(iText.IO.Util.StringUtil.Group(m, UrlGroup)), "local".Equals(iText.IO.Util.StringUtil.Group
                    (m, TypeGroup)), ParseFormat(iText.IO.Util.StringUtil.Group(m, FormatGroup)));
            }

            internal static FontFace.FontFormat ParseFormat(String formatStr) {
                if (formatStr != null && formatStr.Length > 0) {
                    switch (Unquote(formatStr).ToLowerInvariant()) {
                        case "truetype": {
                            return FontFace.FontFormat.TrueType;
                        }

                        case "opentype": {
                            return FontFace.FontFormat.OpenType;
                        }

                        case "woff": {
                            return FontFace.FontFormat.WOFF;
                        }

                        case "woff2": {
                            return FontFace.FontFormat.WOFF2;
                        }

                        case "embedded-opentype": {
                            return FontFace.FontFormat.EOT;
                        }

                        case "svg": {
                            return FontFace.FontFormat.SVG;
                        }
                    }
                }
                return FontFace.FontFormat.None;
            }

            internal static String Unquote(String quotedString) {
                if (quotedString[0] == '\'' || quotedString[0] == '\"') {
                    return quotedString.JSubstring(1, quotedString.Length - 1);
                }
                return quotedString;
            }

            private FontFaceSrc(String src, bool isLocal, FontFace.FontFormat format) {
                this.format = format;
                this.src = src;
                this.isLocal = isLocal;
            }
        }

        internal enum FontFormat {
            None,
            TrueType,
            OpenType,
            WOFF,
            WOFF2,
            EOT,
            SVG
        }
        // "truetype"
        // "opentype"
        // "woff"
        // "woff2"
        // "embedded-opentype"
        // "svg"
        //endregion
    }
}
