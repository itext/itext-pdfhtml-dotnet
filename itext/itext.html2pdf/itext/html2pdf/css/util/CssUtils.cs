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
using System.Text;
using iText.Html2pdf.Css;
using iText.IO.Log;
using iText.Kernel.Colors;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Util {
    public class CssUtils {
        private CssUtils() {
        }

        public static String RemoveDoubleSpacesAndTrim(String str) {
            String[] parts = iText.IO.Util.StringUtil.Split(str, "\\s");
            StringBuilder sb = new StringBuilder();
            foreach (String part in parts) {
                if (part.Length > 0) {
                    if (sb.Length != 0) {
                        sb.Append(" ");
                    }
                    sb.Append(part);
                }
            }
            return sb.ToString();
        }

        public static int? ParseInteger(String str) {
            if (str == null) {
                return null;
            }
            try {
                return System.Convert.ToInt32(str);
            }
            catch (FormatException) {
                return null;
            }
        }

        public static float? ParseFloat(String str) {
            if (str == null) {
                return null;
            }
            try {
                return float.Parse(str);
            }
            catch (FormatException) {
                return null;
            }
        }

        public static int[] ParseAspectRatio(String str) {
            int indexOfSlash = str.IndexOf('/');
            try {
                int first = System.Convert.ToInt32(str.JSubstring(0, indexOfSlash));
                int second = System.Convert.ToInt32(str.Substring(indexOfSlash + 1));
                return new int[] { first, second };
            }
            catch (Exception) {
                return null;
            }
        }

        /// <summary>Parses a length with an allowed metric unit (px, pt, in, cm, mm, pc, q) or numeric value (e.g.</summary>
        /// <remarks>
        /// Parses a length with an allowed metric unit (px, pt, in, cm, mm, pc, q) or numeric value (e.g. 123, 1.23,
        /// .123) to pt.<br />
        /// A numeric value (without px, pt, etc in the given length string) is considered to be in the default metric that
        /// was given.
        /// </remarks>
        /// <param name="length">the string containing the length.</param>
        /// <param name="defaultMetric">
        /// the string containing the metric if it is possible that the length string does not contain
        /// one. If null the length is considered to be in px as is default in HTML/CSS.
        /// </param>
        /// <returns>parsed value</returns>
        public static float ParseAbsoluteLength(String length, String defaultMetric) {
            int pos = DeterminePositionBetweenValueAndUnit(length);
            if (pos == 0) {
                return 0f;
            }
            float f = float.Parse(length.JSubstring(0, pos), System.Globalization.CultureInfo.InvariantCulture);
            String unit = length.Substring(pos);
            //points
            if (unit.StartsWith(CssConstants.PT) || unit.Equals("") && defaultMetric.Equals(CssConstants.PT)) {
                return f;
            }
            // inches
            if (unit.StartsWith(CssConstants.IN) || (unit.Equals("") && defaultMetric.Equals(CssConstants.IN))) {
                return f * 72f;
            }
            else {
                // centimeters
                if (unit.StartsWith(CssConstants.CM) || (unit.Equals("") && defaultMetric.Equals(CssConstants.CM))) {
                    return (f / 2.54f) * 72f;
                }
                else {
                    // quarter of a millimeter (1/40th of a centimeter).
                    if (unit.StartsWith(CssConstants.Q) || (unit.Equals("") && defaultMetric.Equals(CssConstants.Q))) {
                        return (f / 2.54f) * 72f / 40;
                    }
                    else {
                        // millimeters
                        if (unit.StartsWith(CssConstants.MM) || (unit.Equals("") && defaultMetric.Equals(CssConstants.MM))) {
                            return (f / 25.4f) * 72f;
                        }
                        else {
                            // picas
                            if (unit.StartsWith(CssConstants.PC) || (unit.Equals("") && defaultMetric.Equals(CssConstants.PC))) {
                                return f * 12f;
                            }
                            else {
                                // pixels (1px = 0.75pt).
                                if (unit.StartsWith(CssConstants.PX) || (unit.Equals("") && defaultMetric.Equals(CssConstants.PX))) {
                                    return f * 0.75f;
                                }
                            }
                        }
                    }
                }
            }
            ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Util.CssUtils));
            logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.UNKNOWN_ABSOLUTE_METRIC_LENGTH_PARSED, unit.Equals
                ("") ? defaultMetric : unit));
            return f;
        }

        public static float ParseAbsoluteLength(String length) {
            return ParseAbsoluteLength(length, CssConstants.PX);
        }

        /// <summary>Parses an relative value based on the base value that was given, in the metric unit of the base value.
        ///     </summary>
        /// <remarks>
        /// Parses an relative value based on the base value that was given, in the metric unit of the base value. <br />
        /// (e.g. margin=10% should be based on the page width, so if an A4 is used, the margin = 0.10*595.0 = 59.5f)
        /// </remarks>
        /// <param name="relativeValue">in %, em or ex.</param>
        /// <param name="baseValue">the value the returned float is based on.</param>
        /// <returns>the parsed float in the metric unit of the base value.</returns>
        public static float ParseRelativeValue(String relativeValue, float baseValue) {
            int pos = DeterminePositionBetweenValueAndUnit(relativeValue);
            if (pos == 0) {
                return 0f;
            }
            double f = System.Double.Parse(relativeValue.JSubstring(0, pos), System.Globalization.CultureInfo.InvariantCulture
                );
            String unit = relativeValue.Substring(pos);
            if (unit.StartsWith(CssConstants.PERCENTAGE)) {
                f = baseValue * f / 100;
            }
            else {
                if (unit.StartsWith(CssConstants.EM)) {
                    f = baseValue * f;
                }
                else {
                    if (unit.Contains(CssConstants.EX)) {
                        f = baseValue * f / 2;
                    }
                }
            }
            return (float)f;
        }

        public static UnitValue ParseLengthValueToPt(String value, float emValue) {
            if (IsMetricValue(value) || IsNumericValue(value)) {
                return new UnitValue(UnitValue.POINT, ParseAbsoluteLength(value));
            }
            else {
                if (value != null && value.EndsWith(CssConstants.PERCENTAGE)) {
                    return new UnitValue(UnitValue.PERCENT, float.Parse(value.JSubstring(0, value.Length - 1), System.Globalization.CultureInfo.InvariantCulture
                        ));
                }
                else {
                    if (value != null && (value.EndsWith(CssConstants.EM) || value.EndsWith(CssConstants.EX))) {
                        return new UnitValue(UnitValue.POINT, ParseRelativeValue(value, emValue));
                    }
                }
            }
            return null;
        }

        /// <summary>Returns value in dpi (currently)</summary>
        /// <param name="resolutionStr"/>
        /// <returns/>
        public static float ParseResolution(String resolutionStr) {
            // TODO change default units? If so, change MediaDeviceDescription#resolutoin as well
            int pos = DeterminePositionBetweenValueAndUnit(resolutionStr);
            if (pos == 0) {
                return 0f;
            }
            float f = float.Parse(resolutionStr.JSubstring(0, pos), System.Globalization.CultureInfo.InvariantCulture);
            String unit = resolutionStr.Substring(pos);
            if (unit.StartsWith(CssConstants.DPCM)) {
                f *= 2.54f;
            }
            else {
                if (unit.StartsWith(CssConstants.DPPX)) {
                    f *= 96;
                }
            }
            return f;
        }

        /// <summary>Method used in preparation of splitting a string containing a numeric value with a metric unit (e.g.
        ///     </summary>
        /// <remarks>
        /// Method used in preparation of splitting a string containing a numeric value with a metric unit (e.g. 18px, 9pt, 6cm, etc).<br /><br />
        /// Determines the position between digits and affiliated characters ('+','-','0-9' and '.') and all other characters.<br />
        /// e.g. string "16px" will return 2, string "0.5em" will return 3 and string '-8.5mm' will return 4.
        /// </remarks>
        /// <param name="string">containing a numeric value with a metric unit</param>
        /// <returns>int position between the numeric value and unit or 0 if string is null or string started with a non-numeric value.
        ///     </returns>
        private static int DeterminePositionBetweenValueAndUnit(String @string) {
            if (@string == null) {
                return 0;
            }
            int pos = 0;
            while (pos < @string.Length) {
                if (@string[pos] == '+' || @string[pos] == '-' || @string[pos] == '.' || @string[pos] >= '0' && @string[pos
                    ] <= '9') {
                    pos++;
                }
                else {
                    break;
                }
            }
            return pos;
        }

        public static bool IsColorProperty(String value) {
            return value.Contains("rgb(") || value.Contains("rgba(") || value.Contains("#") || WebColors.NAMES.Contains
                (value.ToLower(System.Globalization.CultureInfo.InvariantCulture)) || CssConstants.TRANSPARENT.Equals(
                value);
        }

        /// <summary>Checks whether a string contains an allowed metric unit in HTML/CSS; px, in, cm, mm, pc or pt.</summary>
        /// <param name="value">the string that needs to be checked.</param>
        /// <returns>boolean true if value contains an allowed metric value.</returns>
        public static bool IsMetricValue(String value) {
            // TODO make it check if it is a number + metric ending
            return value != null && (value.EndsWith(CssConstants.PX) || value.EndsWith(CssConstants.IN) || value.EndsWith
                (CssConstants.CM) || value.EndsWith(CssConstants.MM) || value.EndsWith(CssConstants.PC) || value.EndsWith
                (CssConstants.PT));
        }

        /// <summary>Checks whether a string contains an allowed value relative to previously set value.</summary>
        /// <param name="value">the string that needs to be checked.</param>
        /// <returns>boolean true if value contains an allowed metric value.</returns>
        public static bool IsRelativeValue(String value) {
            return value != null && (value.EndsWith(CssConstants.PERCENTAGE) || value.EndsWith(CssConstants.EM) || value
                .EndsWith(CssConstants.EX));
        }

        /// <summary>Checks whether a string matches a numeric value (e.g.</summary>
        /// <remarks>Checks whether a string matches a numeric value (e.g. 123, 1.23, .123). All these metric values are allowed in HTML/CSS.
        ///     </remarks>
        /// <param name="value">the string that needs to be checked.</param>
        /// <returns>boolean true if value contains an allowed metric value.</returns>
        public static bool IsNumericValue(String value) {
            return value != null && (value.Matches("^-?\\d\\d*\\.\\d*$") || value.Matches("^-?\\d\\d*$") || value.Matches
                ("^-?\\.\\d\\d*$"));
        }

        /// <summary>Parses <code>url("file.jpg")</code> to <code>file.jpg</code>.</summary>
        /// <param name="url">the url attribute to parse</param>
        /// <returns>the parsed url. Or original url if not wrappend in url()</returns>
        public static String ExtractUrl(String url) {
            String str = null;
            if (url.StartsWith("url")) {
                String urlString = url.Substring(3).Trim().Replace("(", "").Replace(")", "").Trim();
                if (urlString.StartsWith("'") && urlString.EndsWith("'")) {
                    str = urlString.JSubstring(urlString.IndexOf("'", StringComparison.Ordinal) + 1, urlString.LastIndexOf("'"
                        ));
                }
                else {
                    if (urlString.StartsWith("\"") && urlString.EndsWith("\"")) {
                        str = urlString.JSubstring(urlString.IndexOf('"') + 1, urlString.LastIndexOf('"'));
                    }
                    else {
                        str = urlString;
                    }
                }
            }
            else {
                // assume it's an url without wrapping in "url()"
                str = url;
            }
            return str;
        }
    }
}
