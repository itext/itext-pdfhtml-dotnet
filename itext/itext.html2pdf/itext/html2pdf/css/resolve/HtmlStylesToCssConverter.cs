/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
using iText.Commons.Utils;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Resolve.Shorthand.Impl;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Resolve {
    /// <summary>Utilities class that converts HTML styles to CSS.</summary>
    internal class HtmlStylesToCssConverter {
        /// <summary>Maps HTML styles to a specific converter.</summary>
        private static readonly IDictionary<String, HtmlStylesToCssConverter.IAttributeConverter> htmlAttributeConverters;

        static HtmlStylesToCssConverter() {
            htmlAttributeConverters = new Dictionary<String, HtmlStylesToCssConverter.IAttributeConverter>();
            htmlAttributeConverters.Put(AttributeConstants.ALIGN, new HtmlStylesToCssConverter.AlignAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.BORDER, new HtmlStylesToCssConverter.BorderAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.BGCOLOR, new HtmlStylesToCssConverter.BgColorAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.CELLPADDING, new HtmlStylesToCssConverter.CellPaddingAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.CELLSPACING, new HtmlStylesToCssConverter.CellSpacingAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.COLOR, new HtmlStylesToCssConverter.FontColorAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.DIR, new HtmlStylesToCssConverter.DirAttributeConverter());
            htmlAttributeConverters.Put(AttributeConstants.SIZE, new HtmlStylesToCssConverter.SizeAttributeConverter()
                );
            htmlAttributeConverters.Put(AttributeConstants.FACE, new HtmlStylesToCssConverter.FontFaceAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.NOSHADE, new HtmlStylesToCssConverter.NoShadeAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.NOWRAP, new HtmlStylesToCssConverter.NoWrapAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.TYPE, new HtmlStylesToCssConverter.TypeAttributeConverter()
                );
            htmlAttributeConverters.Put(AttributeConstants.WIDTH, new HtmlStylesToCssConverter.WidthAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.HEIGHT, new HtmlStylesToCssConverter.HeightAttributeConverter
                ());
            htmlAttributeConverters.Put(AttributeConstants.VALIGN, new HtmlStylesToCssConverter.VAlignAttributeConverter
                ());
        }

        /// <summary>
        /// Converts a the HTML styles of an element to a list of
        /// <see cref="iText.StyledXmlParser.Css.CssDeclaration"/>
        /// instances.
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>
        /// the resulting list of
        /// <see cref="iText.StyledXmlParser.Css.CssDeclaration"/>
        /// instances.
        /// </returns>
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

        /// <summary>Interface for all the attribute converter classes.</summary>
        private interface IAttributeConverter {
            /// <summary>Checks if the converter is supported for a specific element.</summary>
            /// <param name="elementName">the element name</param>
            /// <returns>true, if the converter is supported</returns>
            bool IsSupportedForElement(String elementName);

            /// <summary>
            /// Converts a specific HTML style to a
            /// <see cref="iText.StyledXmlParser.Css.CssDeclaration"/>.
            /// </summary>
            /// <param name="element">the element</param>
            /// <param name="value">the value of the HTML style</param>
            /// <returns>
            /// a list of
            /// <see cref="iText.StyledXmlParser.Css.CssDeclaration"/>
            /// instances
            /// </returns>
            IList<CssDeclaration> Convert(IElementNode element, String value);
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML border styles.
        /// </summary>
        private class BorderAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /// <summary>Applies borders to the tables and cells.</summary>
            /// <param name="node">the node</param>
            /// <param name="borderStyles">the border styles</param>
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

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.IMG.Equals(elementName) || TagConstants.TABLE.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                float? width = CssDimensionParsingUtils.ParseFloat(value);
                if (width != null) {
                    if (TagConstants.TABLE.Equals(element.Name()) && width != 0) {
                        IList<CssDeclaration> declarations = new BorderShorthandResolver().ResolveShorthand("1px solid");
                        IDictionary<String, String> styles = new Dictionary<String, String>(declarations.Count);
                        foreach (CssDeclaration declaration in declarations) {
                            styles.Put(declaration.GetProperty(), declaration.GetExpression());
                        }
                        ApplyBordersToTableCells(element, styles);
                    }
                    if (width >= 0) {
                        return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.BORDER, value + "px solid"));
                    }
                }
                return JavaCollectionsUtil.EmptyList<CssDeclaration>();
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for table's cellpadding.
        /// </summary>
        private class CellPaddingAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /// <summary>Applies paddings to the table's cells.</summary>
            /// <param name="node">the node</param>
            /// <param name="paddingStyle">cellpadding</param>
            private static void ApplyPaddingsToTableCells(INode node, IDictionary<String, String> paddingStyle) {
                IList<INode> nodes = node.ChildNodes();
                foreach (INode childNode in nodes) {
                    if (childNode is IElementNode) {
                        IElementNode elementNode = (IElementNode)childNode;
                        if (TagConstants.TD.Equals(elementNode.Name()) || TagConstants.TH.Equals(elementNode.Name())) {
                            elementNode.AddAdditionalHtmlStyles(paddingStyle);
                        }
                        else {
                            ApplyPaddingsToTableCells(childNode, paddingStyle);
                        }
                    }
                }
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.TABLE.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                float? cellPadding = CssDimensionParsingUtils.ParseFloat(value);
                if (cellPadding != null) {
                    if (TagConstants.TABLE.Equals(element.Name())) {
                        IDictionary<String, String> styles = new Dictionary<String, String>();
                        styles.Put(CssConstants.PADDING, value + "px");
                        ApplyPaddingsToTableCells(element, styles);
                    }
                }
                return JavaCollectionsUtil.EmptyList<CssDeclaration>();
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for table's cellspacing.
        /// </summary>
        private class CellSpacingAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.TABLE.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.BORDER_SPACING, value));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML background color styles.
        /// </summary>
        private class BgColorAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /// <summary>The supported tags.</summary>
            private static ICollection<String> supportedTags = new HashSet<String>(JavaUtil.ArraysAsList(TagConstants.
                BODY, TagConstants.COL, TagConstants.COLGROUP, TagConstants.MARQUEE, TagConstants.TABLE, TagConstants.
                TBODY, TagConstants.TFOOT, TagConstants.TD, TagConstants.TH, TagConstants.TR));

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return supportedTags.Contains(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.BACKGROUND_COLOR, value));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for font color styles.
        /// </summary>
        private class FontColorAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.COLOR, value));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for size properties.
        /// </summary>
        private class SizeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName) || TagConstants.HR.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                String cssValueEquivalent = null;
                String cssPropertyEquivalent = null;
                String elementName = element.Name();
                if (TagConstants.FONT.Equals(elementName)) {
                    cssPropertyEquivalent = CssConstants.FONT_SIZE;
                    try {
                        bool signedValue = value.Contains("-") || value.Contains("+");
                        int htmlFontSize = System.Convert.ToInt32(value, System.Globalization.CultureInfo.InvariantCulture);
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
                return JavaUtil.ArraysAsList(new CssDeclaration(cssPropertyEquivalent, cssValueEquivalent));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML font face styles.
        /// </summary>
        private class FontFaceAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.FONT.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.FONT_FAMILY, value));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML ordered list types.
        /// </summary>
        private class TypeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.OL.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
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
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.LIST_STYLE_TYPE, cssEquivalent));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML direction styles (e.g. right to left direction).
        /// </summary>
        private class DirAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return true;
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.DIRECTION, value));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML width values.
        /// </summary>
        private class WidthAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName) || TagConstants.IMG.Equals(elementName) || TagConstants.TABLE.Equals
                    (elementName) || TagConstants.TD.Equals(elementName) || TagConstants.TH.Equals(elementName) || TagConstants
                    .COLGROUP.Equals(elementName) || TagConstants.COL.Equals(elementName) || TagConstants.OBJECT.Equals(elementName
                    );
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                // Trim semicolons at the end because they seem to not affect the value in browsers
                String cssEquivalent = iText.Commons.Utils.StringUtil.ReplaceAll(value, ";+$", "");
                if (!CssTypesValidationUtils.IsMetricValue(cssEquivalent) && !cssEquivalent.EndsWith(CssConstants.PERCENTAGE
                    )) {
                    cssEquivalent += CssConstants.PX;
                }
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.WIDTH, cssEquivalent));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML height values.
        /// </summary>
        private class HeightAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.IMG.Equals(elementName) || TagConstants.TD.Equals(elementName) || TagConstants.OBJECT.
                    Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                // Trim semicolons at the end because they seem to not affect the value in browsers
                String cssEquivalent = iText.Commons.Utils.StringUtil.ReplaceAll(value, ";+$", "");
                if (!CssTypesValidationUtils.IsMetricValue(cssEquivalent) && !cssEquivalent.EndsWith(CssConstants.PERCENTAGE
                    )) {
                    cssEquivalent += CssConstants.PX;
                }
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.HEIGHT, cssEquivalent));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML horizontal alignment styles.
        /// </summary>
        private class AlignAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName) || TagConstants.TABLE.Equals(elementName) || TagConstants.IMG.Equals
                    (elementName) || TagConstants.TD.Equals(elementName) || TagConstants.DIV.Equals(elementName) || TagConstants
                    .P.Equals(elementName) || TagConstants.CAPTION.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                IList<CssDeclaration> result = new List<CssDeclaration>(2);
                if (TagConstants.HR.Equals(element.Name()) || 
                                // html align-center attribute doesn't apply text wrapping
                                (TagConstants.TABLE.Equals(element.Name()) && AttributeConstants.CENTER.Equals(value))) {
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
                        if (TagConstants.IMG.Equals(element.Name()) && (AttributeConstants.TOP.Equals(value) || AttributeConstants
                            .MIDDLE.Equals(value))) {
                            // No BOTTOM here because VERTICAL_ALIGN is deprecated and BOTTOM is translated to nothing
                            result.Add(new CssDeclaration(CssConstants.VERTICAL_ALIGN, value));
                        }
                        else {
                            if (AttributeConstants.LEFT.Equals(value) || AttributeConstants.RIGHT.Equals(value)) {
                                result.Add(new CssDeclaration(CssConstants.FLOAT, value));
                            }
                        }
                    }
                    else {
                        if (TagConstants.CAPTION.Equals(element.Name())) {
                            result.Add(new CssDeclaration(CssConstants.CAPTION_SIDE, value));
                        }
                        else {
                            // TODO DEVSIX-5518 fix conflicts of 'align' and 'text-align'
                            result.Add(new CssDeclaration(CssConstants.TEXT_ALIGN, value));
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML shade styles.
        /// </summary>
        private class NoShadeAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.HR.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.HEIGHT, "2px"), new CssDeclaration(CssConstants
                    .BORDER_WIDTH, "0"), new CssDeclaration(CssConstants.BACKGROUND_COLOR, "gray"));
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML shade styles.
        /// </summary>
        private class NoWrapAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.TD.Equals(elementName) || TagConstants.TH.Equals(elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaCollectionsUtil.SingletonList(new CssDeclaration(CssConstants.WHITE_SPACE, CssConstants.NOWRAP)
                    );
            }
        }

        /// <summary>
        /// <see cref="IAttributeConverter"/>
        /// implementation for HTML vertical alignment styles.
        /// </summary>
        private class VAlignAttributeConverter : HtmlStylesToCssConverter.IAttributeConverter {
            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#isSupportedForElement(java.lang.String)
            */
            public virtual bool IsSupportedForElement(String elementName) {
                return TagConstants.TD.Equals(elementName) || TagConstants.TH.Equals(elementName) || TagConstants.TR.Equals
                    (elementName);
            }

            /* (non-Javadoc)
            * @see com.itextpdf.html2pdf.css.resolve.HtmlStylesToCssConverter.IAttributeConverter#convert(com.itextpdf.styledxmlparser.html.node.IElementNode, java.lang.String)
            */
            public virtual IList<CssDeclaration> Convert(IElementNode element, String value) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.VERTICAL_ALIGN, value));
            }
        }
    }
}
