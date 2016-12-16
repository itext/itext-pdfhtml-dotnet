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
                parsedStylesheet = CssStyleSheetParser.Parse(ResourceUtil.GetResourceStream(DEFAULT_CSS_PATH));
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
            htmlAttributeConverters[AttributeConstants.HEIGHT] = new HtmlStylesToCssConverter.HeightAttributeConverter
                ();
        }

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
                    convertedHtmlStyles.AddAll(aConverter.Convert(element, a.GetValue()));
                }
            }
            return convertedHtmlStyles;
        }

        private interface IAttributeConverter {
            bool IsSupportedForElement(String elementName);

            IList<CssDeclaration> Convert(IElementNode element, String value);
        }

        private class BorderAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            // TODO for table border, border attribute affects cell borders as well
            private static void ApplyBordersToTableCells(IElementNode element, String value) {
                IList<INode> nodes = element.ChildNodes();
                foreach (INode node in nodes) {
                    if (node is IElementNode) {
                        String elementName = ((IElementNode)node).Name();
                        if (elementName.Equals(TagConstants.TD) || elementName.Equals(TagConstants.TH)) {
                            String styleAttribute = ((IElementNode)node).GetAttribute(AttributeConstants.STYLE);
                            if (styleAttribute == null) {
                                styleAttribute = "";
                            }
                            if (!styleAttribute.Contains(CssConstants.BORDER)) {
                                if (!String.IsNullOrEmpty(styleAttribute)) {
                                    styleAttribute = styleAttribute + "; ";
                                }
                                ((IElementNode)node).GetAttributes().SetAttribute(AttributeConstants.STYLE, styleAttribute + new CssDeclaration
                                    (CssConstants.TABLE_CUSTOM_BORDER, value + "px solid black").ToString());
                            }
                        }
                        else {
                            ApplyBordersToTableCells((IElementNode)node, value);
                        }
                    }
                }
            }

            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.IMG.Equals(elementName) || TagConstants.TABLE.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                ApplyBordersToTableCells(element, value);
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

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.BACKGROUND_COLOR, value));
            }
        }

        private class FontColorAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.COLOR, value));
            }
        }

        private class SizeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName) || TagConstants.HR.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                String cssValueEquivalent = null;
                String cssPropertyEquivalent = null;
                String elementName = element.Name();
                if (TagConstants.FONT.Equals(elementName)) {
                    cssPropertyEquivalent = CssConstants.FONT_SIZE;
                    try {
                        bool signedValue = value.Contains("-") || value.Contains("+");
                        int htmlFontSize = System.Convert.ToInt32(value);
                        if (signedValue) {
                            htmlFontSize = 3 + htmlFontSize;
                        }
                        if (htmlFontSize < 2) {
                            cssValueEquivalent = CssConstants.X_SMALL;
                        }
                        else {
                            if (htmlFontSize > 6) {
                                cssValueEquivalent = "48px";
                            }
                            else {
                                if (htmlFontSize == 2) {
                                    cssValueEquivalent = CssConstants.SMALL;
                                }
                                else {
                                    if (htmlFontSize == 3) {
                                        cssValueEquivalent = CssConstants.MEDIUM;
                                    }
                                    else {
                                        if (htmlFontSize == 4) {
                                            cssValueEquivalent = CssConstants.LARGE;
                                        }
                                        else {
                                            if (htmlFontSize == 5) {
                                                cssValueEquivalent = CssConstants.X_LARGE;
                                            }
                                            else {
                                                if (htmlFontSize == 6) {
                                                    cssValueEquivalent = CssConstants.XX_LARGE;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (FormatException) {
                        cssValueEquivalent = CssConstants.MEDIUM;
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

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.FONT_FAMILY, value));
            }
        }

        private class TypeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.OL.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                String cssEquivalent = null;
                switch (value) {
                    case AttributeConstants._1: {
                        cssEquivalent = CssConstants.DECIMAL;
                        break;
                    }

                    case AttributeConstants.A: {
                        cssEquivalent = CssConstants.UPPER_ALPHA;
                        break;
                    }

                    case AttributeConstants.a: {
                        cssEquivalent = CssConstants.LOWER_ALPHA;
                        break;
                    }

                    case AttributeConstants.I: {
                        cssEquivalent = CssConstants.UPPER_ROMAN;
                        break;
                    }

                    case AttributeConstants.i: {
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

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.DIRECTION, value));
            }
        }

        private class WidthAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName) || TagConstants.IMG.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                String cssEquivalent = value;
                if (!value.EndsWith(CssConstants.PERCENTAGE)) {
                    cssEquivalent += CssConstants.PX;
                }
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.WIDTH, cssEquivalent));
            }
        }

        private class HeightAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.IMG.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                String cssEquivalent = value;
                if (!value.EndsWith(CssConstants.PERCENTAGE)) {
                    cssEquivalent += CssConstants.PX;
                }
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.HEIGHT, cssEquivalent));
            }
        }

        private class AlignAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName) || TagConstants.TABLE.Equals(elementName) || TagConstants.IMG.Equals
                    (elementName) || TagConstants.TD.Equals(elementName) || TagConstants.DIV.Equals(elementName) || TagConstants
                    .P.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                IList<CssDeclaration> result = new List<CssDeclaration>(2);
                if (TagConstants.HR.Equals(element.Name()) || TagConstants.TABLE.Equals(element.Name()) || TagConstants.IMG
                    .Equals(element.Name())) {
                    // TODO In fact, 'align' attribute on 'table' and 'img' tags should be translated to the 'float' css property, however it's not supported yet.
                    // TODO Another difference here would be that alignment via margins is reset if element itself has explicit corresponding margin property,
                    // however elements with 'float: left' for example are shown at the right side regardless of the margins. This means that a table with
                    // 'align: right' in html are shown at the right side regardless of margins properties, however in our current implementation margins matter.
                    // (see HorizontalAlignmentTest#alignAttribute04)
                    String leftMargin = null;
                    String rightMargin = null;
                    if (AttributeConstants.RIGHT.Equals(value)) {
                        leftMargin = CssConstants.AUTO;
                        rightMargin = "0";
                    }
                    else {
                        if (AttributeConstants.LEFT.Equals(value)) {
                            leftMargin = "0";
                            rightMargin = CssConstants.AUTO;
                        }
                        else {
                            if (AttributeConstants.CENTER.Equals(value)) {
                                leftMargin = CssConstants.AUTO;
                                rightMargin = CssConstants.AUTO;
                            }
                            else {
                                if (TagConstants.IMG.Equals(element.Name()) && AttributeConstants.TOP.Equals(value) && AttributeConstants.
                                    MIDDLE.Equals(value) && AttributeConstants.BOTTOM.Equals(value)) {
                                    result.Add(new CssDeclaration(CssConstants.VERTICAL_ALIGN, value));
                                }
                            }
                        }
                    }
                    if (leftMargin != null) {
                        result.Add(new CssDeclaration(CssConstants.MARGIN_LEFT, leftMargin));
                        result.Add(new CssDeclaration(CssConstants.MARGIN_RIGHT, rightMargin));
                    }
                }
                else {
                    // TODO in fact, align attribute also affects horizontal alignment of all child blocks (not only direct children),
                    // however this effect conflicts in queer manner with 'text-align' property if it set on the same blocks explicitly via CSS
                    // (see HorizontalAlignmentTest#alignAttribute01)
                    result.Add(new CssDeclaration(CssConstants.TEXT_ALIGN, value));
                }
                return result;
            }
        }

        private class NoShadeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.HEIGHT, "2px"), new CssDeclaration
                    (CssConstants.BORDER_WIDTH, "0"), new CssDeclaration(CssConstants.BACKGROUND_COLOR, "gray"));
            }
        }
    }
}
