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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Validate;

namespace iText.Html2pdf.Css.Validate.Impl.Datatype {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Validate.ICssDataTypeValidator"/>
    /// implementation for .
    /// </summary>
    public class CssTransformValidator : ICssDataTypeValidator {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.validate.ICssDataTypeValidator#isValid(java.lang.String)
        */
        public virtual bool IsValid(String objectString) {
            String function;
            String args;
            if (!CssConstants.NONE.Equals(objectString)) {
                function = objectString.JSubstring(0, objectString.IndexOf('('));
                args = objectString.JSubstring(objectString.IndexOf('(') + 1, objectString.Length - 1);
            }
            else {
                return true;
            }
            if (CssConstants.MATRIX.Equals(function) || CssConstants.SCALE.Equals(function) || CssConstants.SCALE_X.Equals
                (function) || CssConstants.SCALE_Y.Equals(function)) {
                String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                if (arg.Length == 6 && CssConstants.MATRIX.Equals(function) || (arg.Length == 1 || arg.Length == 2) && CssConstants
                    .SCALE.Equals(function) || arg.Length == 1 && (CssConstants.SCALE_X.Equals(function) || CssConstants.SCALE_Y
                    .Equals(function))) {
                    int i = 0;
                    for (; i < arg.Length; i++) {
                        try {
                            float.Parse(arg[i].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        catch (FormatException) {
                            return false;
                        }
                    }
                    if (i == arg.Length) {
                        return true;
                    }
                }
                return false;
            }
            else {
                if (CssConstants.TRANSLATE.Equals(function) || CssConstants.TRANSLATE_X.Equals(function) || CssConstants.TRANSLATE_Y
                    .Equals(function)) {
                    String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                    if ((arg.Length == 1 || arg.Length == 2 && CssConstants.TRANSLATE.Equals(function)) || (arg.Length == 1 &&
                         (CssConstants.TRANSLATE_X.Equals(function) || CssConstants.TRANSLATE_Y.Equals(function)))) {
                        foreach (String a in arg) {
                            if (!IsValidForTranslate(a)) {
                                return false;
                            }
                        }
                        return true;
                    }
                    return false;
                }
                else {
                    if (CssConstants.ROTATE.Equals(function)) {
                        int i = args.IndexOf('d');
                        if (i > 0 && args.Substring(i).Equals("deg")) {
                            try {
                                System.Double.Parse(args.JSubstring(0, i), System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (FormatException) {
                                return false;
                            }
                            return true;
                        }
                        return false;
                    }
                    else {
                        if (CssConstants.SKEW.Equals(function) || CssConstants.SKEW_X.Equals(function) || CssConstants.SKEW_Y.Equals
                            (function)) {
                            String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                            if ((arg.Length == 1 || arg.Length == 2 && CssConstants.SKEW.Equals(function)) || (arg.Length == 1 && (CssConstants
                                .SKEW_X.Equals(function) || CssConstants.SKEW_Y.Equals(function)))) {
                                for (int k = 0; k < arg.Length; k++) {
                                    int i = arg[k].IndexOf('d');
                                    if (i < 0 || !arg[k].Substring(i).Equals("deg")) {
                                        return false;
                                    }
                                    try {
                                        float.Parse(arg[k].Trim().JSubstring(0, i), System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                    catch (FormatException) {
                                        return false;
                                    }
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private static bool IsValidForTranslate(String @string) {
            if (@string == null) {
                return false;
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
            if (pos > 0) {
                try {
                    float.Parse(@string.JSubstring(0, pos), System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (FormatException) {
                    return false;
                }
                return (@string.Substring(pos).Equals(CssConstants.PT) || @string.Substring(pos).Equals(CssConstants.IN) ||
                     @string.Substring(pos).Equals(CssConstants.CM) || @string.Substring(pos).Equals(CssConstants.Q) || @string
                    .Substring(pos).Equals(CssConstants.MM) || @string.Substring(pos).Equals(CssConstants.PC) || @string.Substring
                    (pos).Equals(CssConstants.PX));
            }
            return false;
        }
    }
}
