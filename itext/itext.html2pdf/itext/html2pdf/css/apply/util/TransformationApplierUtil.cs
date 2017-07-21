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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Util;
using iText.Layout;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public class TransformationApplierUtil {
        /// <summary>Creates a new <code>TransformationApplierUtil</code> instance.</summary>
        private TransformationApplierUtil() {
        }

        /// <summary>Applies a transformation to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the properties context</param>
        /// <param name="element">the element</param>
        public static void ApplyTransformation(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String transformationFunction;
            String function;
            String args;
            if (cssProps.Get(CssConstants.TRANSFORM) != null) {
                transformationFunction = cssProps.Get(CssConstants.TRANSFORM).ToLowerInvariant();
            }
            else {
                transformationFunction = "none";
            }
            if (!CssConstants.NONE.Equals(transformationFunction)) {
                function = transformationFunction.JSubstring(0, transformationFunction.IndexOf('('));
                args = transformationFunction.JSubstring(transformationFunction.IndexOf('(') + 1, transformationFunction.Length
                     - 1);
            }
            else {
                function = transformationFunction;
                args = "0";
            }
            if (CssConstants.MATRIX.Equals(function)) {
                String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                if (arg.Length == 6) {
                    float[] matrix = new float[6];
                    int i = 0;
                    for (; i < 6; i++) {
                        if (i == 4 || i == 5) {
                            matrix[i] = CssUtils.ParseAbsoluteLength(arg[i].Trim());
                        }
                        else {
                            matrix[i] = float.Parse(arg[i].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        if (i == 1 || i == 2 || i == 5) {
                            matrix[i] *= -1;
                        }
                    }
                    element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(matrix));
                }
            }
            else {
                if (CssConstants.TRANSLATE.Equals(function)) {
                    String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                    String xStr = null;
                    String yStr = null;
                    float x = 0;
                    float y = 0;
                    if (arg[0].IndexOf('%') > 0) {
                        xStr = arg[0];
                    }
                    else {
                        x = CssUtils.ParseAbsoluteLength(arg[0].Trim());
                    }
                    if (arg.Length == 2) {
                        if (arg[1].IndexOf('%') > 0) {
                            yStr = System.Convert.ToString(-1 * float.Parse(arg[1].JSubstring(0, arg[1].IndexOf('%')), System.Globalization.CultureInfo.InvariantCulture
                                ), System.Globalization.CultureInfo.InvariantCulture) + '%';
                        }
                        else {
                            y = -1 * CssUtils.ParseAbsoluteLength(arg[1].Trim());
                        }
                    }
                    String[] transform = FloatArrayToStringArray(new float[] { 1, 0, 0, 1, x, y });
                    if (xStr != null) {
                        transform[4] = xStr;
                    }
                    if (yStr != null) {
                        transform[5] = yStr;
                    }
                    element.SetProperty(Property.TRANSFORM, transform);
                }
                else {
                    if (CssConstants.TRANSLATE_X.Equals(function)) {
                        String xStr = null;
                        float x = 0;
                        if (args.IndexOf('%') > 0) {
                            xStr = args;
                        }
                        else {
                            x = CssUtils.ParseAbsoluteLength(args.Trim());
                        }
                        String[] transform = FloatArrayToStringArray(new float[] { 1, 0, 0, 1, x, 0 });
                        if (xStr != null) {
                            transform[4] = xStr;
                        }
                        element.SetProperty(Property.TRANSFORM, transform);
                    }
                    else {
                        if (CssConstants.TRANSLATE_Y.Equals(function)) {
                            String yStr = null;
                            float y = 0;
                            if (args.IndexOf('%') > 0) {
                                yStr = System.Convert.ToString(-1 * float.Parse(args.JSubstring(0, args.IndexOf('%')), System.Globalization.CultureInfo.InvariantCulture
                                    ), System.Globalization.CultureInfo.InvariantCulture) + '%';
                            }
                            else {
                                y = -1 * CssUtils.ParseAbsoluteLength(args.Trim());
                            }
                            String[] transform = FloatArrayToStringArray(new float[] { 1, 0, 0, 1, 0, y });
                            if (yStr != null) {
                                transform[4] = yStr;
                            }
                            element.SetProperty(Property.TRANSFORM, transform);
                        }
                        else {
                            if (CssConstants.ROTATE.Equals(function)) {
                                double angleInRad = ParseAngleToRadians(args);
                                float cos = (float)Math.Cos(angleInRad);
                                float sin = (float)Math.Sin(angleInRad);
                                element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(new float[] { cos, sin, -1 * sin, cos, 0, 
                                    0 }));
                            }
                            else {
                                if (CssConstants.SKEW.Equals(function)) {
                                    String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                                    double xAngleInRad = ParseAngleToRadians(arg[0]);
                                    double yAngleInRad = arg.Length == 2 ? ParseAngleToRadians(arg[1]) : 0.0;
                                    float tanX = (float)Math.Tan(xAngleInRad);
                                    float tanY = (float)Math.Tan(yAngleInRad);
                                    element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(new float[] { 1, tanY, tanX, 1, 0, 0 }));
                                }
                                else {
                                    if (CssConstants.SKEW_X.Equals(function)) {
                                        float tanX = (float)Math.Tan(ParseAngleToRadians(args));
                                        element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(new float[] { 1, 0, tanX, 1, 0, 0 }));
                                    }
                                    else {
                                        if (CssConstants.SKEW_Y.Equals(function)) {
                                            float tanY = (float)Math.Tan(ParseAngleToRadians(args));
                                            element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(new float[] { 1, tanY, 0, 1, 0, 0 }));
                                        }
                                        else {
                                            if (CssConstants.SCALE.Equals(function)) {
                                                String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                                                float x;
                                                float y;
                                                if (arg.Length == 2) {
                                                    x = float.Parse(arg[0].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                                                    y = float.Parse(arg[1].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                                                }
                                                else {
                                                    x = float.Parse(arg[0].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                                                    y = x;
                                                }
                                                element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(new float[] { x, 0, 0, y, 0, 0 }));
                                            }
                                            else {
                                                if (CssConstants.SCALE_X.Equals(function)) {
                                                    float x = float.Parse(args.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                                                    element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(new float[] { x, 0, 0, 1, 0, 0 }));
                                                }
                                                else {
                                                    if (CssConstants.SCALE_Y.Equals(function)) {
                                                        float y = float.Parse(args.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                                                        element.SetProperty(Property.TRANSFORM, FloatArrayToStringArray(new float[] { 1, 0, 0, y, 0, 0 }));
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

        private static double ParseAngleToRadians(String value) {
            if (value.IndexOf('d') < 0) {
                return 0.0;
            }
            if (value.IndexOf('r') > 0) {
                return -1 * System.Double.Parse(value.Trim().JSubstring(0, value.IndexOf('r')), System.Globalization.CultureInfo.InvariantCulture
                    );
            }
            return iText.IO.Util.MathUtil.ToRadians(-1 * System.Double.Parse(value.Trim().JSubstring(0, value.IndexOf(
                'd')), System.Globalization.CultureInfo.InvariantCulture));
        }

        private static String[] FloatArrayToStringArray(float[] floats) {
            String[] strings = new String[floats.Length];
            for (int i = 0; i < floats.Length; i++) {
                strings[i] = System.Convert.ToString(floats[i], System.Globalization.CultureInfo.InvariantCulture);
            }
            return strings;
        }
    }
}
