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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;
using iText.Kernel.Numbering;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public sealed class ListStyleApplierUtil {
        private const int GREEK_ALPHABET_LENGTH = 24;

        private static readonly char[] GREEK_LOWERCASE = new char[GREEK_ALPHABET_LENGTH];

        private const String DISC_SYMBOL = "\u2022";

        private const String CIRCLE_SYMBOL = "\u25cb";

        private const String SQUARE_SYMBOL = "\u25a0";

        static ListStyleApplierUtil() {
            //private static final String HTML_SYMBOL_FONT = "Sans-serif";
            for (int i = 0; i < GREEK_ALPHABET_LENGTH; i++) {
                GREEK_LOWERCASE[i] = (char)(945 + i + (i > 16 ? 1 : 0));
            }
        }

        private ListStyleApplierUtil() {
        }

        public static void ApplyListStyleImageProperty(IDictionary<String, String> cssProps, ProcessorContext context
            , IPropertyContainer element) {
            String listStyleImage = cssProps.Get(CssConstants.LIST_STYLE_IMAGE);
            if (listStyleImage != null && !CssConstants.NONE.Equals(listStyleImage)) {
                String url = CssUtils.ExtractUrl(listStyleImage);
                PdfImageXObject imageXObject = context.GetResourceResolver().RetrieveImage(url);
                if (imageXObject != null) {
                    element.SetProperty(Property.LIST_SYMBOL, new Image(imageXObject));
                    element.SetProperty(Property.LIST_SYMBOL_INDENT, 5);
                }
            }
        }

        public static void ApplyListStyleTypeProperty(IStylesContainer stylesContainer, IDictionary<String, String
            > cssProps, ProcessorContext context, IPropertyContainer element) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
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
                                                            ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.ListStyleApplierUtil));
                                                            logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.NOT_SUPPORTED_LIST_STYLE_TYPE, style));
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

        public static void SetDiscStyle(IPropertyContainer element, float em) {
            Text symbol = new Text(DISC_SYMBOL);
            element.SetProperty(Property.LIST_SYMBOL, symbol);
            SetListSymbolIndent(element, em);
        }

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

        private static void SetSquareStyle(IPropertyContainer element, float em) {
            Text symbol = new Text(SQUARE_SYMBOL);
            symbol.SetTextRise(1.5f * em / 12);
            symbol.SetFontSize(4.5f * em / 12);
            element.SetProperty(Property.LIST_SYMBOL, symbol);
            SetListSymbolIndent(element, em);
        }

        private static void SetCircleStyle(IPropertyContainer element, float em) {
            Text symbol = new Text(CIRCLE_SYMBOL);
            symbol.SetTextRise(1.5f * em / 12);
            symbol.SetFontSize(4.5f * em / 12);
            element.SetProperty(Property.LIST_SYMBOL, symbol);
            SetListSymbolIndent(element, em);
        }

        private static void SetListSymbolIndent(IPropertyContainer element, float em) {
            if (ListSymbolPosition.INSIDE == element.GetProperty<ListSymbolPosition?>(Property.LIST_SYMBOL_POSITION)) {
                element.SetProperty(Property.LIST_SYMBOL_INDENT, 1.5f * em);
            }
            else {
                element.SetProperty(Property.LIST_SYMBOL_INDENT, 7.75f);
            }
        }

        private class HtmlAlphabetSymbolFactory : IListSymbolFactory {
            private readonly char[] alphabet;

            public HtmlAlphabetSymbolFactory(char[] alphabet) {
                this.alphabet = alphabet;
            }

            public virtual IElement CreateSymbol(int index, IPropertyContainer list, IPropertyContainer listItem) {
                Object preValue = GetListItemOrListProperty(listItem, list, Property.LIST_SYMBOL_PRE_TEXT);
                Object postValue = GetListItemOrListProperty(listItem, list, Property.LIST_SYMBOL_POST_TEXT);
                Text result = new Text(preValue + AlphabetNumbering.ToAlphabetNumber(index, alphabet) + postValue);
                return result;
            }

            private static Object GetListItemOrListProperty(IPropertyContainer listItem, IPropertyContainer list, int 
                propertyId) {
                return listItem.HasProperty(propertyId) ? listItem.GetProperty<Object>(propertyId) : list.GetProperty<Object
                    >(propertyId);
            }
        }
    }
}
