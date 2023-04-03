/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Kernel.Numbering;

namespace iText.Html2pdf.Html {
    /// <summary>Utilities class with HTML-related functionality.</summary>
    public sealed class HtmlUtils {
        /// <summary>The Constant DISC_SYMBOL.</summary>
        private const String DISC_SYMBOL = "•";

        /// <summary>The Constant CIRCLE_SYMBOL.</summary>
        private const String CIRCLE_SYMBOL = "◦";

        /// <summary>The Constant SQUARE_SYMBOL.</summary>
        private const String SQUARE_SYMBOL = "■";

        /// <summary>Symbols which are used to write numbers in Latin.</summary>
        private const String LATIN_NUMERALS = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>Symbols which are used to write numbers in Greek.</summary>
        private const String GREEK_NUMERALS = "αβγδεζηθικλμνξοπρστυφχψω";

        /// <summary>Symbols which are used to write numbers in Roman.</summary>
        private const String ROMAN_NUMERALS = "ivxlcdm";

        /// <summary>Symbols which are used to write numbers in Georgian.</summary>
        private const String GEORGIAN_NUMERALS = "აბგდევზჱთიკლმნჲოპჟრსტჳფქღყშჩცძწჭხჴჯჰჵ";

        /// <summary>Symbols which are used to write numbers in Armenian.</summary>
        private const String ARMENIAN_NUMERALS = "ԱԲԳԴԵԶԷԸԹԺԻԼԽԾԿՀՁՂՃՄՅՆՇՈՉՊՋՌՍՎՏՐՑՒՓՔ";

        /// <summary>Symbols which are used to write numbers by default.</summary>
        private const String DEFAULT_NUMERALS = "1234567890";

        /// <summary>The Constant MAX_ROMAN_NUMBER.</summary>
        private const int MAX_ROMAN_NUMBER = 3999;

        /// <summary>
        /// Creates a new
        /// <see cref="HtmlUtils"/>
        /// instance.
        /// </summary>
        private HtmlUtils() {
        }

        /// <summary>Converts number according to given glyph style.</summary>
        /// <param name="glyphStyle">style of the glyphs</param>
        /// <param name="number">number to be converted</param>
        /// <returns>converted number</returns>
        public static String ConvertNumberAccordingToGlyphStyle(CounterDigitsGlyphStyle glyphStyle, int number) {
            if (glyphStyle == null) {
                return ConvertNumberDefault(number);
            }
            switch (glyphStyle) {
                case CounterDigitsGlyphStyle.NONE: {
                    return "";
                }

                case CounterDigitsGlyphStyle.DISC: {
                    return DISC_SYMBOL;
                }

                case CounterDigitsGlyphStyle.SQUARE: {
                    return SQUARE_SYMBOL;
                }

                case CounterDigitsGlyphStyle.CIRCLE: {
                    return CIRCLE_SYMBOL;
                }

                case CounterDigitsGlyphStyle.UPPER_ALPHA_AND_LATIN: {
                    return number > 0 ? EnglishAlphabetNumbering.ToLatinAlphabetNumberUpperCase(number) : ConvertNumberDefault
                        (number);
                }

                case CounterDigitsGlyphStyle.LOWER_ALPHA_AND_LATIN: {
                    return number > 0 ? EnglishAlphabetNumbering.ToLatinAlphabetNumberLowerCase(number) : ConvertNumberDefault
                        (number);
                }

                case CounterDigitsGlyphStyle.LOWER_GREEK: {
                    return number > 0 ? GreekAlphabetNumbering.ToGreekAlphabetNumberLowerCase(number) : ConvertNumberDefault(number
                        );
                }

                case CounterDigitsGlyphStyle.LOWER_ROMAN: {
                    return number <= MAX_ROMAN_NUMBER ? RomanNumbering.ToRomanLowerCase(number) : ConvertNumberDefault(number);
                }

                case CounterDigitsGlyphStyle.UPPER_ROMAN: {
                    return number <= MAX_ROMAN_NUMBER ? RomanNumbering.ToRomanUpperCase(number) : ConvertNumberDefault(number);
                }

                case CounterDigitsGlyphStyle.DECIMAL_LEADING_ZERO: {
                    return (number < 10 ? "0" : "") + ConvertNumberDefault(number);
                }

                case CounterDigitsGlyphStyle.GEORGIAN: {
                    return GeorgianNumbering.ToGeorgian(number);
                }

                case CounterDigitsGlyphStyle.ARMENIAN: {
                    return ArmenianNumbering.ToArmenian(number);
                }

                case CounterDigitsGlyphStyle.DEFAULT:
                default: {
                    return ConvertNumberDefault(number);
                }
            }
        }

        private static String ConvertNumberDefault(int number) {
            return number.ToString();
        }

        /// <summary>Gets a string which contains all glyphs which can be used in number according to given glyph style.
        ///     </summary>
        /// <param name="glyphStyle">style of the glyphs</param>
        /// <returns>string of all number glyphs</returns>
        public static String GetAllNumberGlyphsForStyle(CounterDigitsGlyphStyle glyphStyle) {
            if (glyphStyle == null) {
                return DEFAULT_NUMERALS;
            }
            switch (glyphStyle) {
                case CounterDigitsGlyphStyle.NONE: {
                    return "";
                }

                case CounterDigitsGlyphStyle.DISC: {
                    return DISC_SYMBOL;
                }

                case CounterDigitsGlyphStyle.SQUARE: {
                    return SQUARE_SYMBOL;
                }

                case CounterDigitsGlyphStyle.CIRCLE: {
                    return CIRCLE_SYMBOL;
                }

                case CounterDigitsGlyphStyle.UPPER_ALPHA_AND_LATIN: {
                    return LATIN_NUMERALS.ToUpperInvariant();
                }

                case CounterDigitsGlyphStyle.LOWER_ALPHA_AND_LATIN: {
                    return LATIN_NUMERALS;
                }

                case CounterDigitsGlyphStyle.LOWER_GREEK: {
                    return GREEK_NUMERALS;
                }

                case CounterDigitsGlyphStyle.LOWER_ROMAN: {
                    return ROMAN_NUMERALS;
                }

                case CounterDigitsGlyphStyle.UPPER_ROMAN: {
                    return ROMAN_NUMERALS.ToUpperInvariant();
                }

                case CounterDigitsGlyphStyle.GEORGIAN: {
                    return GEORGIAN_NUMERALS;
                }

                case CounterDigitsGlyphStyle.ARMENIAN: {
                    return ARMENIAN_NUMERALS;
                }

                default: {
                    return DEFAULT_NUMERALS;
                }
            }
        }

        /// <summary>Gets enum representation of given digits glyph style.</summary>
        /// <param name="glyphStyle">style of the glyphs</param>
        /// <returns>
        /// 
        /// <see cref="iText.Html2pdf.Css.Resolve.Func.Counter.CounterDigitsGlyphStyle"/>
        /// equivalent of given glyph style
        /// </returns>
        public static CounterDigitsGlyphStyle ConvertStringCounterGlyphStyleToEnum(String glyphStyle) {
            if (glyphStyle == null) {
                return CounterDigitsGlyphStyle.DEFAULT;
            }
            switch (glyphStyle) {
                case CssConstants.NONE: {
                    return CounterDigitsGlyphStyle.NONE;
                }

                case CssConstants.DISC: {
                    return CounterDigitsGlyphStyle.DISC;
                }

                case CssConstants.SQUARE: {
                    return CounterDigitsGlyphStyle.SQUARE;
                }

                case CssConstants.CIRCLE: {
                    return CounterDigitsGlyphStyle.CIRCLE;
                }

                case CssConstants.UPPER_ALPHA:
                case CssConstants.UPPER_LATIN: {
                    return CounterDigitsGlyphStyle.UPPER_ALPHA_AND_LATIN;
                }

                case CssConstants.LOWER_ALPHA:
                case CssConstants.LOWER_LATIN: {
                    return CounterDigitsGlyphStyle.LOWER_ALPHA_AND_LATIN;
                }

                case CssConstants.LOWER_GREEK: {
                    return CounterDigitsGlyphStyle.LOWER_GREEK;
                }

                case CssConstants.LOWER_ROMAN: {
                    return CounterDigitsGlyphStyle.LOWER_ROMAN;
                }

                case CssConstants.UPPER_ROMAN: {
                    return CounterDigitsGlyphStyle.UPPER_ROMAN;
                }

                case CssConstants.GEORGIAN: {
                    return CounterDigitsGlyphStyle.GEORGIAN;
                }

                case CssConstants.ARMENIAN: {
                    return CounterDigitsGlyphStyle.ARMENIAN;
                }

                case CssConstants.DECIMAL_LEADING_ZERO: {
                    return CounterDigitsGlyphStyle.DECIMAL_LEADING_ZERO;
                }

                default: {
                    return CounterDigitsGlyphStyle.DEFAULT;
                }
            }
        }
    }
}
