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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Parse;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve {
    internal class HtmlStylesToCssConverter {
        private const String DEFAULT_CSS_PATH = "iText.Html2Pdf.default.css";

        private static readonly CssStyleSheet defaultCss;

        private static readonly IDictionary<String, HtmlStylesToCssConverter.IAttributeConverter> htmlAttributeConverters;

        static HtmlStylesToCssConverter() {
            CssStyleSheet parsedStylesheet = new CssStyleSheet();
            try {
                parsedStylesheet = CssStyleSheetParser.Parse(ResourceUtil.GetResourceStream(DEFAULT_CSS_PATH, typeof(HtmlStylesToCssConverter)));
            }
            catch (System.IO.IOException exc) {
                ILogger logger = LoggerFactory.GetLogger(typeof(HtmlStylesToCssConverter));
                logger.Error("Error parsing default.css", exc);
            }
            finally {
                defaultCss = parsedStylesheet;
            }
            htmlAttributeConverters = new Dictionary<String, HtmlStylesToCssConverter.IAttributeConverter>();
            htmlAttributeConverters[AttributeConstants.ALIGN] = new HtmlStylesToCssConverter.AlignAttributeConverter();
            htmlAttributeConverters[AttributeConstants.BORDER] = new HtmlStylesToCssConverter.BorderAttributeConverter
                ();
            htmlAttributeConverters[AttributeConstants.BGCOLOR] = new HtmlStylesToCssConverter.BgColorAttributeConverter
                ();
            htmlAttributeConverters[AttributeConstants.COLOR] = new HtmlStylesToCssConverter.FontColorAttributeConverter
                ();
            htmlAttributeConverters[AttributeConstants.DIR] = new HtmlStylesToCssConverter.DirAttributeConverter();
            htmlAttributeConverters[AttributeConstants.SIZE] = new HtmlStylesToCssConverter.SizeAttributeConverter();
            htmlAttributeConverters[AttributeConstants.FACE] = new HtmlStylesToCssConverter.FontFaceAttributeConverter
                ();
            htmlAttributeConverters[AttributeConstants.NOSHADE] = new HtmlStylesToCssConverter.NoShadeAttributeConverter
                ();
            htmlAttributeConverters[AttributeConstants.TYPE] = new HtmlStylesToCssConverter.TypeAttributeConverter();
            htmlAttributeConverters[AttributeConstants.WIDTH] = new HtmlStylesToCssConverter.WidthAttributeConverter();
        }

        // TODO
        //        htmlAttributeConverters.put("height", );
        public static IList<CssDeclaration> Convert(IElementNode element) {
            IList<CssDeclaration> convertedHtmlStyles = new List<CssDeclaration>();
            IList<CssDeclaration> tagCssStyles = defaultCss.GetCssDeclarations(element, MediaDeviceDescription.CreateDefault
                ());
            if (tagCssStyles != null) {
                convertedHtmlStyles.AddAll(tagCssStyles);
            }
            foreach (IAttribute a in element.GetAttributes()) {
                HtmlStylesToCssConverter.IAttributeConverter aConverter = htmlAttributeConverters.Get(a.GetKey());
                if (aConverter != null && aConverter.IsSupportedForElement(element.Name())) {
                    convertedHtmlStyles.AddAll(aConverter.Convert(element.Name(), a.GetValue()));
                }
            }
            return convertedHtmlStyles;
        }

        private interface IAttributeConverter {
            bool IsSupportedForElement(String elementName);

            IList<CssDeclaration> Convert(String elementName, String value);
        }

        private class BorderAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            // TODO for table border, border attribute affects cell borders as well
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.IMG.Equals(elementName) || TagConstants.TABLE.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.BORDER, value + "px solid black"
                    ));
            }
        }

        private class BgColorAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            private static ICollection<String> supportedTags = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList
                (TagConstants.BODY, TagConstants.COL, TagConstants.COLGROUP, TagConstants.MARQUEE, TagConstants.TABLE, 
                TagConstants.TBODY, TagConstants.TFOOT, TagConstants.TD, TagConstants.TH, TagConstants.TR));

            public virtual bool IsSupportedForElement(String elementName) {
                return supportedTags.Contains(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.BACKGROUND_COLOR, value));
            }
        }

        private class FontColorAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.COLOR, value));
            }
        }

        private class SizeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName) || TagConstants.HR.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                String cssValueEquivalent = null;
                String cssPropertyEquivalent = null;
                if (TagConstants.FONT.Equals(elementName)) {
                    cssPropertyEquivalent = CssConstants.FONT_SIZE;
                    if ("1".Equals(value)) {
                        cssValueEquivalent = CssConstants.XX_SMALL;
                    }
                    else {
                        if ("2".Equals(value)) {
                            cssValueEquivalent = CssConstants.X_SMALL;
                        }
                        else {
                            if ("3".Equals(value)) {
                                cssValueEquivalent = CssConstants.SMALL;
                            }
                            else {
                                if ("4".Equals(value)) {
                                    cssValueEquivalent = CssConstants.MEDIUM;
                                }
                                else {
                                    if ("5".Equals(value)) {
                                        cssValueEquivalent = CssConstants.LARGE;
                                    }
                                    else {
                                        if ("6".Equals(value)) {
                                            cssValueEquivalent = CssConstants.X_LARGE;
                                        }
                                        else {
                                            if ("7".Equals(value)) {
                                                cssValueEquivalent = CssConstants.XX_LARGE;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    if (TagConstants.HR.Equals(elementName)) {
                        cssPropertyEquivalent = CssConstants.HEIGHT;
                        cssValueEquivalent = value + CssConstants.PX;
                    }
                }
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(cssPropertyEquivalent, cssValueEquivalent));
            }
        }

        private class FontFaceAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.FONT_FAMILY, value));
            }
        }

        private class TypeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.OL.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                String cssEquivalent = null;
                switch (value) {
                    case "1": {
                        cssEquivalent = CssConstants.DECIMAL;
                        break;
                    }

                    case "A": {
                        cssEquivalent = CssConstants.UPPER_ALPHA;
                        break;
                    }

                    case "a": {
                        cssEquivalent = CssConstants.LOWER_ALPHA;
                        break;
                    }

                    case "I": {
                        cssEquivalent = CssConstants.UPPER_ROMAN;
                        break;
                    }

                    case "i": {
                        cssEquivalent = CssConstants.LOWER_ROMAN;
                        break;
                    }
                }
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.LIST_STYLE_TYPE, cssEquivalent)
                    );
            }
        }

        private class DirAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return true;
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.DIRECTION, value));
            }
        }

        private class WidthAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                String cssEquivalent = value;
                if (!value.EndsWith("%")) {
                    cssEquivalent += CssConstants.PX;
                }
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.WIDTH, cssEquivalent));
            }
        }

        private class AlignAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                IList<CssDeclaration> result = new List<CssDeclaration>(2);
                if ("right".Equals(value)) {
                    result.Add(new CssDeclaration(CssConstants.MARGIN_RIGHT, "0"));
                }
                else {
                    if ("left".Equals(value)) {
                        result.Add(new CssDeclaration(CssConstants.MARGIN_LEFT, "0"));
                    }
                    else {
                        if ("center".Equals(value)) {
                            result.Add(new CssDeclaration(CssConstants.MARGIN_RIGHT, CssConstants.AUTO));
                            result.Add(new CssDeclaration(CssConstants.MARGIN_LEFT, CssConstants.AUTO));
                        }
                    }
                }
                return result;
            }
        }

        private class NoShadeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(String elementName, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.HEIGHT, "2px"), new CssDeclaration
                    (CssConstants.BORDER_WIDTH, "0"), new CssDeclaration(CssConstants.BACKGROUND_COLOR, "gray"));
            }
        }
    }
}
