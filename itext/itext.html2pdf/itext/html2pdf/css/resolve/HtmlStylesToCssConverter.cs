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
using iText.Html2pdf.Css.Resolve.Shorthand.Impl;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve {
    internal class HtmlStylesToCssConverter {
        private static readonly IDictionary<String, HtmlStylesToCssConverter.IAttributeConverter> htmlAttributeConverters;

        static HtmlStylesToCssConverter() {
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
            htmlAttributeConverters[AttributeConstants.VALIGN] = new HtmlStylesToCssConverter.VAlignAttributeConverter
                ();
        }

        public static IList<CssDeclaration> Convert(IElementNode element) {
            List<CssDeclaration> convertedHtmlStyles = new List<CssDeclaration>();
            if (element.GetAdditionalHtmlStyles() != null) {
                Dictionary<String, String> additionalStyles = new Dictionary<String, String>();
                foreach (IDictionary<String, String> styles in element.GetAdditionalHtmlStyles()) {
                    additionalStyles.AddAll(styles);
                }
                convertedHtmlStyles.EnsureCapacity(convertedHtmlStyles.Count + additionalStyles.Count);
                foreach (KeyValuePair<String, String> entry in additionalStyles) {
                    convertedHtmlStyles.Add(new CssDeclaration(entry.Key, entry.Value));
                }
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
            private static void ApplyBordersToTableCells(INode node, IDictionary<String, String> borderStyles) {
                IList<INode> nodes = node.ChildNodes();
                foreach (INode childNode in nodes) {
                    if (childNode is IElementNode) {
                        IElementNode elementNode = (IElementNode)childNode;
                        if (TagConstants.TD.Equals(elementNode.Name()) || TagConstants.TH.Equals(elementNode.Name())) {
                            elementNode.AddAdditionalHtmlStyles(borderStyles);
                        }
                        else {
                            ApplyBordersToTableCells(childNode, borderStyles);
                        }
                    }
                }
            }

            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.IMG.Equals(elementName) || TagConstants.TABLE.Equals(elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                float? width = CssUtils.ParseFloat(value);
                if (width != null) {
                    if (TagConstants.TABLE.Equals(element.Name()) && width != 0) {
                        IList<CssDeclaration> declarations = new BorderShorthandResolver().ResolveShorthand("1px solid");
                        IDictionary<String, String> styles = new Dictionary<String, String>(declarations.Count);
                        foreach (CssDeclaration declaration in declarations) {
                            styles[declaration.GetProperty()] = declaration.GetExpression();
                        }
                        ApplyBordersToTableCells(element, styles);
                    }
                    if (width >= 0) {
                        return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.BORDER, value + "px solid"));
                    }
                }
                return JavaCollectionsUtil.EmptyList<CssDeclaration>();
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
                return TagConstants.HR.Equals(elementName) || TagConstants.IMG.Equals(elementName) || TagConstants.TABLE.Equals
                    (elementName) || TagConstants.TD.Equals(elementName) || TagConstants.TH.Equals(elementName) || TagConstants
                    .COLGROUP.Equals(elementName) || TagConstants.COL.Equals(elementName);
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
                if (TagConstants.HR.Equals(element.Name()) || (TagConstants.TABLE.Equals(element.Name()) && AttributeConstants
                    .CENTER.Equals(value))) {
                    // html align-center attribute doesn't apply text wrapping
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
                        }
                    }
                    if (leftMargin != null) {
                        result.Add(new CssDeclaration(CssConstants.MARGIN_LEFT, leftMargin));
                        result.Add(new CssDeclaration(CssConstants.MARGIN_RIGHT, rightMargin));
                    }
                }
                else {
                    if (TagConstants.TABLE.Equals(element.Name()) || TagConstants.IMG.Equals(element.Name())) {
                        if (TagConstants.IMG.Equals(element.Name()) && AttributeConstants.TOP.Equals(value) && AttributeConstants.
                            MIDDLE.Equals(value) && AttributeConstants.BOTTOM.Equals(value)) {
                            result.Add(new CssDeclaration(CssConstants.VERTICAL_ALIGN, value));
                        }
                        else {
                            if (AttributeConstants.LEFT.Equals(value) || AttributeConstants.RIGHT.Equals(value)) {
                                result.Add(new CssDeclaration(CssConstants.FLOAT, value));
                            }
                        }
                    }
                    else {
                        // TODO in fact, align attribute also affects horizontal alignment of all child blocks (not only direct children),
                        // however this effect conflicts in queer manner with 'text-align' property if it set on the same blocks explicitly via CSS
                        // (see HorizontalAlignmentTest#alignAttribute01)
                        result.Add(new CssDeclaration(CssConstants.TEXT_ALIGN, value));
                    }
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

        private class VAlignAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.TD.Equals(elementName) || TagConstants.TH.Equals(elementName) || TagConstants.TR.Equals
                    (elementName);
            }

            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.VERTICAL_ALIGN, value));
            }
        }
    }
}
