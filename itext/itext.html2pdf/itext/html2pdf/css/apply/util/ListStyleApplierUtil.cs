/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Colors.Gradients;
using iText.Kernel.Geom;
using iText.Kernel.Numbering;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Exceptions;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply list styles to an element.</summary>
    public sealed class ListStyleApplierUtil {
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.ListStyleApplierUtil
            ));

        /// <summary>The Constant LIST_ITEM_MARKER_SIZE_COEFFICIENT.</summary>
        /// <remarks>
        /// The Constant LIST_ITEM_MARKER_SIZE_COEFFICIENT.
        /// The coefficient value of 2/5 is chosen in such a way that the result
        /// of the converting is as similar as possible to the browsers displaying.
        /// </remarks>
        private const float LIST_ITEM_MARKER_SIZE_COEFFICIENT = 2 / 5f;

        //private static final String HTML_SYMBOL_FONT = "Sans-serif";
        /// <summary>The Constant GREEK_ALPHABET_LENGTH.</summary>
        private const int GREEK_ALPHABET_LENGTH = 24;

        /// <summary>The Constant GREEK_LOWERCASE.</summary>
        private static readonly char[] GREEK_LOWERCASE = new char[GREEK_ALPHABET_LENGTH];

        /// <summary>The Constant DISC_SYMBOL.</summary>
        private const String DISC_SYMBOL = "\u2022";

        /// <summary>The Constant CIRCLE_SYMBOL.</summary>
        private const String CIRCLE_SYMBOL = "\u25cb";

        /// <summary>The Constant SQUARE_SYMBOL.</summary>
        private const String SQUARE_SYMBOL = "\u25a0";

        static ListStyleApplierUtil() {
            for (int i = 0; i < GREEK_ALPHABET_LENGTH; i++) {
                GREEK_LOWERCASE[i] = (char)(945 + i + (i > 16 ? 1 : 0));
            }
        }

        /// <summary>
        /// Creates a new
        /// <see cref="ListStyleApplierUtil"/>
        /// instance.
        /// </summary>
        private ListStyleApplierUtil() {
        }

        /// <summary>Applies an image list style to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyListStyleImageProperty(IDictionary<String, String> cssProps, ProcessorContext context
            , IPropertyContainer element) {
            String listStyleImageStr = cssProps.Get(CssConstants.LIST_STYLE_IMAGE);
            PdfXObject imageXObject = null;
            if (listStyleImageStr != null && !CssConstants.NONE.Equals(listStyleImageStr)) {
                if (CssGradientUtil.IsCssLinearGradientValue(listStyleImageStr)) {
                    float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
                    float rem = context.GetCssContext().GetRootFontSize();
                    try {
                        StrategyBasedLinearGradientBuilder gradientBuilder = CssGradientUtil.ParseCssLinearGradient(listStyleImageStr
                            , em, rem);
                        if (gradientBuilder != null) {
                            Rectangle formBBox = new Rectangle(0, 0, em * LIST_ITEM_MARKER_SIZE_COEFFICIENT, em * LIST_ITEM_MARKER_SIZE_COEFFICIENT
                                );
                            PdfDocument pdfDocument = context.GetPdfDocument();
                            Color gradientColor = gradientBuilder.BuildColor(formBBox, null, pdfDocument);
                            if (gradientColor != null) {
                                imageXObject = new PdfFormXObject(formBBox);
                                new PdfCanvas((PdfFormXObject)imageXObject, context.GetPdfDocument()).SetColor(gradientColor, true).Rectangle
                                    (formBBox).Fill();
                            }
                        }
                    }
                    catch (StyledXMLParserException) {
                        LOGGER.Warn(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, listStyleImageStr
                            ));
                    }
                }
                else {
                    imageXObject = context.GetResourceResolver().RetrieveImageExtended(CssUtils.ExtractUrl(listStyleImageStr));
                }
                if (imageXObject != null) {
                    Image image = null;
                    if (imageXObject is PdfImageXObject) {
                        image = new Image((PdfImageXObject)imageXObject);
                    }
                    else {
                        if (imageXObject is PdfFormXObject) {
                            image = new Image((PdfFormXObject)imageXObject);
                        }
                        else {
                            throw new InvalidOperationException();
                        }
                    }
                    element.SetProperty(Property.LIST_SYMBOL, image);
                    element.SetProperty(Property.LIST_SYMBOL_INDENT, 5);
                }
            }
        }

        /// <summary>Applies a list style to an element.</summary>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyListStyleTypeProperty(IStylesContainer stylesContainer, IDictionary<String, String
            > cssProps, ProcessorContext context, IPropertyContainer element) {
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            String style = cssProps.Get(CssConstants.LIST_STYLE_TYPE);
            if (CssConstants.DISC.Equals(style)) {
                SetDiscStyle(element, em);
            }
            else {
                if (CssConstants.CIRCLE.Equals(style)) {
                    SetCircleStyle(element, em);
                }
                else {
                    if (CssConstants.SQUARE.Equals(style)) {
                        SetSquareStyle(element, em);
                    }
                    else {
                        if (CssConstants.DECIMAL.Equals(style)) {
                            SetListSymbol(element, ListNumberingType.DECIMAL);
                        }
                        else {
                            if (CssConstants.DECIMAL_LEADING_ZERO.Equals(style)) {
                                SetListSymbol(element, ListNumberingType.DECIMAL_LEADING_ZERO);
                            }
                            else {
                                if (CssConstants.UPPER_ALPHA.Equals(style) || CssConstants.UPPER_LATIN.Equals(style)) {
                                    SetListSymbol(element, ListNumberingType.ENGLISH_UPPER);
                                }
                                else {
                                    if (CssConstants.LOWER_ALPHA.Equals(style) || CssConstants.LOWER_LATIN.Equals(style)) {
                                        SetListSymbol(element, ListNumberingType.ENGLISH_LOWER);
                                    }
                                    else {
                                        if (CssConstants.UPPER_ROMAN.Equals(style)) {
                                            SetListSymbol(element, ListNumberingType.ROMAN_UPPER);
                                        }
                                        else {
                                            if (CssConstants.LOWER_ROMAN.Equals(style)) {
                                                SetListSymbol(element, ListNumberingType.ROMAN_LOWER);
                                            }
                                            else {
                                                if (CssConstants.LOWER_GREEK.Equals(style)) {
                                                    element.SetProperty(Property.LIST_SYMBOL, new ListStyleApplierUtil.HtmlAlphabetSymbolFactory(GREEK_LOWERCASE
                                                        ));
                                                }
                                                else {
                                                    if (CssConstants.NONE.Equals(style)) {
                                                        SetListSymbol(element, new Text(""));
                                                    }
                                                    else {
                                                        if (style != null) {
                                                            ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.ListStyleApplierUtil));
                                                            logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.NOT_SUPPORTED_LIST_STYLE_TYPE, style
                                                                ));
                                                        }
                                                        // Fallback style
                                                        if (stylesContainer is IElementNode) {
                                                            String elementName = ((IElementNode)stylesContainer).Name();
                                                            if (TagConstants.UL.Equals(elementName)) {
                                                                SetDiscStyle(element, em);
                                                            }
                                                            else {
                                                                if (TagConstants.OL.Equals(elementName)) {
                                                                    SetListSymbol(element, ListNumberingType.DECIMAL);
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
                        }
                    }
                }
            }
        }

        /// <summary>Applies the "disc" list style to an element.</summary>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        public static void SetDiscStyle(IPropertyContainer element, float em) {
            Text symbol = new Text(DISC_SYMBOL);
            element.SetProperty(Property.LIST_SYMBOL, symbol);
            SetListSymbolIndent(element, em);
        }

        /// <summary>
        /// Sets the list symbol for a
        /// <see cref="iText.Layout.Element.List"/>
        /// or
        /// <see cref="iText.Layout.Element.ListItem"/>
        /// element.
        /// </summary>
        /// <param name="container">
        /// the container element (
        /// <see cref="iText.Layout.Element.List"/>
        /// or
        /// <see cref="iText.Layout.Element.ListItem"/>
        /// )
        /// </param>
        /// <param name="text">the list symbol</param>
        private static void SetListSymbol(IPropertyContainer container, Text text) {
            if (container is List) {
                ((List)container).SetListSymbol(text);
            }
            else {
                if (container is ListItem) {
                    ((ListItem)container).SetListSymbol(text);
                }
            }
        }

        /// <summary>
        /// Sets the list symbol for a
        /// <see cref="iText.Layout.Element.List"/>
        /// or
        /// <see cref="iText.Layout.Element.ListItem"/>
        /// element.
        /// </summary>
        /// <param name="container">
        /// the container element (
        /// <see cref="iText.Layout.Element.List"/>
        /// or
        /// <see cref="iText.Layout.Element.ListItem"/>
        /// )
        /// </param>
        /// <param name="listNumberingType">the list numbering type</param>
        private static void SetListSymbol(IPropertyContainer container, ListNumberingType listNumberingType) {
            if (container is List) {
                ((List)container).SetListSymbol(listNumberingType);
            }
            else {
                if (container is ListItem) {
                    ((ListItem)container).SetListSymbol(listNumberingType);
                }
            }
        }

        /// <summary>Applies the "square" list style to an element.</summary>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        private static void SetSquareStyle(IPropertyContainer element, float em) {
            Text symbol = new Text(SQUARE_SYMBOL);
            symbol.SetTextRise(1.5f * em / 12);
            symbol.SetFontSize(4.5f * em / 12);
            element.SetProperty(Property.LIST_SYMBOL, symbol);
            SetListSymbolIndent(element, em);
        }

        /// <summary>Applies the "circle" list style to an element.</summary>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        private static void SetCircleStyle(IPropertyContainer element, float em) {
            Text symbol = new Text(CIRCLE_SYMBOL);
            symbol.SetTextRise(1.5f * em / 12);
            symbol.SetFontSize(4.5f * em / 12);
            element.SetProperty(Property.LIST_SYMBOL, symbol);
            SetListSymbolIndent(element, em);
        }

        /// <summary>Sets the list symbol indentation.</summary>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        private static void SetListSymbolIndent(IPropertyContainer element, float em) {
            if (ListSymbolPosition.INSIDE == element.GetProperty<ListSymbolPosition?>(Property.LIST_SYMBOL_POSITION)) {
                element.SetProperty(Property.LIST_SYMBOL_INDENT, 1.5f * em);
            }
            else {
                element.SetProperty(Property.LIST_SYMBOL_INDENT, 7.75f);
            }
        }

        /// <summary>
        /// A factory for creating
        /// <c>HtmlAlphabetSymbol</c>
        /// objects.
        /// </summary>
        private class HtmlAlphabetSymbolFactory : IListSymbolFactory {
            /// <summary>The alphabet.</summary>
            private readonly char[] alphabet;

            /// <summary>
            /// Creates a new
            /// <see cref="HtmlAlphabetSymbolFactory"/>
            /// instance.
            /// </summary>
            /// <param name="alphabet">the alphabet</param>
            public HtmlAlphabetSymbolFactory(char[] alphabet) {
                this.alphabet = alphabet;
            }

            /* (non-Javadoc)
            * @see com.itextpdf.layout.property.IListSymbolFactory#createSymbol(int, com.itextpdf.layout.IPropertyContainer, com.itextpdf.layout.IPropertyContainer)
            */
            public virtual IElement CreateSymbol(int index, IPropertyContainer list, IPropertyContainer listItem) {
                Object preValue = GetListItemOrListProperty(listItem, list, Property.LIST_SYMBOL_PRE_TEXT);
                Object postValue = GetListItemOrListProperty(listItem, list, Property.LIST_SYMBOL_POST_TEXT);
                Text result = new Text(preValue + AlphabetNumbering.ToAlphabetNumber(index, alphabet) + postValue);
                return result;
            }

            /// <summary>
            /// Gets the a property from a
            /// <see cref="iText.Layout.Element.ListItem"/>
            /// , or from the
            /// <see cref="iText.Layout.Element.List"/>
            /// (if the property) isn't declared for the list item.
            /// </summary>
            /// <param name="listItem">the list item</param>
            /// <param name="list">the list</param>
            /// <param name="propertyId">the property id</param>
            /// <returns>the property value</returns>
            private static Object GetListItemOrListProperty(IPropertyContainer listItem, IPropertyContainer list, int 
                propertyId) {
                return listItem.HasProperty(propertyId) ? listItem.GetProperty<Object>(propertyId) : list.GetProperty<Object
                    >(propertyId);
            }
        }
    }
}
