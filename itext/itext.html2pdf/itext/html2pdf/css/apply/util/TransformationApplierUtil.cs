/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Util;
using iText.IO.Util;
using iText.Layout;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public class TransformationApplierUtil {
        /// <summary>
        /// Creates a new
        /// <see cref="TransformationApplierUtil"/>
        /// instance.
        /// </summary>
        private TransformationApplierUtil() {
        }

        /// <summary>Applies a transformation to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the properties context</param>
        /// <param name="element">the element</param>
        public static void ApplyTransformation(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String transformationFunction;
            if (cssProps.Get(CssConstants.TRANSFORM) != null) {
                transformationFunction = cssProps.Get(CssConstants.TRANSFORM).ToLowerInvariant();
            }
            else {
                return;
            }
            String[] components = iText.IO.Util.StringUtil.Split(transformationFunction, "\\)");
            Transform multipleFunction = new Transform(components.Length);
            foreach (String component in components) {
                multipleFunction.AddSingleTransform(ParseSingleFunction(component));
            }
            element.SetProperty(Property.TRANSFORM, multipleFunction);
        }

        private static Transform.SingleTransform ParseSingleFunction(String transformationFunction) {
            String function;
            String args;
            if (!CssConstants.NONE.Equals(transformationFunction)) {
                function = transformationFunction.JSubstring(0, transformationFunction.IndexOf('(')).Trim();
                args = transformationFunction.Substring(transformationFunction.IndexOf('(') + 1);
            }
            else {
                return GetSingleTransform(1, 0, 0, 1, 0, 0);
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
                    return GetSingleTransform(matrix);
                }
            }
            if (CssConstants.TRANSLATE.Equals(function)) {
                String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                bool xPoint;
                bool yPoint = true;
                float x;
                float y = 0;
                xPoint = arg[0].IndexOf('%') < 0;
                x = xPoint ? CssUtils.ParseAbsoluteLength(arg[0].Trim()) : float.Parse(arg[0].Trim().JSubstring(0, arg[0].
                    IndexOf('%')), System.Globalization.CultureInfo.InvariantCulture);
                if (arg.Length == 2) {
                    yPoint = arg[1].IndexOf('%') < 0;
                    y = -1 * (yPoint ? CssUtils.ParseAbsoluteLength(arg[1].Trim()) : float.Parse(arg[1].Trim().JSubstring(0, arg
                        [1].IndexOf('%')), System.Globalization.CultureInfo.InvariantCulture));
                }
                return GetSingleTransformTranslate(1, 0, 0, 1, x, y, xPoint, yPoint);
            }
            if (CssConstants.TRANSLATE_X.Equals(function)) {
                bool xPoint = args.IndexOf('%') < 0;
                float x = xPoint ? CssUtils.ParseAbsoluteLength(args.Trim()) : float.Parse(args.Trim().JSubstring(0, args.
                    IndexOf('%')), System.Globalization.CultureInfo.InvariantCulture);
                return GetSingleTransformTranslate(1, 0, 0, 1, x, 0, xPoint, true);
            }
            if (CssConstants.TRANSLATE_Y.Equals(function)) {
                bool yPoint = args.IndexOf('%') < 0;
                float y = -1 * (yPoint ? CssUtils.ParseAbsoluteLength(args.Trim()) : float.Parse(args.Trim().JSubstring(0, 
                    args.IndexOf('%')), System.Globalization.CultureInfo.InvariantCulture));
                return GetSingleTransformTranslate(1, 0, 0, 1, 0, y, true, yPoint);
            }
            if (CssConstants.ROTATE.Equals(function)) {
                double angleInRad = ParseAngleToRadians(args);
                float cos = (float)Math.Cos(angleInRad);
                float sin = (float)Math.Sin(angleInRad);
                return GetSingleTransform(cos, sin, -1 * sin, cos, 0, 0);
            }
            if (CssConstants.SKEW.Equals(function)) {
                String[] arg = iText.IO.Util.StringUtil.Split(args, ",");
                double xAngleInRad = ParseAngleToRadians(arg[0]);
                double yAngleInRad = arg.Length == 2 ? ParseAngleToRadians(arg[1]) : 0.0;
                float tanX = (float)Math.Tan(xAngleInRad);
                float tanY = (float)Math.Tan(yAngleInRad);
                return GetSingleTransform(1, tanY, tanX, 1, 0, 0);
            }
            if (CssConstants.SKEW_X.Equals(function)) {
                float tanX = (float)Math.Tan(ParseAngleToRadians(args));
                return GetSingleTransform(1, 0, tanX, 1, 0, 0);
            }
            if (CssConstants.SKEW_Y.Equals(function)) {
                float tanY = (float)Math.Tan(ParseAngleToRadians(args));
                return GetSingleTransform(1, tanY, 0, 1, 0, 0);
            }
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
                return GetSingleTransform(x, 0, 0, y, 0, 0);
            }
            if (CssConstants.SCALE_X.Equals(function)) {
                float x = float.Parse(args.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                return GetSingleTransform(x, 0, 0, 1, 0, 0);
            }
            if (CssConstants.SCALE_Y.Equals(function)) {
                float y = float.Parse(args.Trim(), System.Globalization.CultureInfo.InvariantCulture);
                return GetSingleTransform(1, 0, 0, y, 0, 0);
            }
            return new Transform.SingleTransform();
        }

        /// <summary>Convert an angle (presented as radians or degrees) to radians</summary>
        /// <param name="value">the angle (as a CSS string)</param>
        /// <returns/>
        private static double ParseAngleToRadians(String value) {
            if (value.IndexOf('d') < 0) {
                return 0.0;
            }
            if (value.IndexOf('r') > 0) {
                return -1 * Double.Parse(value.Trim().JSubstring(0, value.IndexOf('r')), System.Globalization.CultureInfo.InvariantCulture
                    );
            }
            return MathUtil.ToRadians(-1 * Double.Parse(value.Trim().JSubstring(0, value.IndexOf('d')), System.Globalization.CultureInfo.InvariantCulture
                ));
        }

        /// <summary>Apply a linear transformation, using a transformation matrix</summary>
        /// <param name="a">element [0,0] of the transformation matrix</param>
        /// <param name="b">element [0,1] of the transformation matrix</param>
        /// <param name="c">element [1,0] of the transformation matrix</param>
        /// <param name="d">element [1,1] of the transformation matrix</param>
        /// <param name="tx">translation on x-axis</param>
        /// <param name="ty">translation on y-axis</param>
        /// <param name="xPoint"/>
        /// <param name="yPoint"/>
        /// <returns/>
        private static Transform.SingleTransform GetSingleTransformTranslate(float a, float b, float c, float d, float
             tx, float ty, bool xPoint, bool yPoint) {
            return new Transform.SingleTransform(a, b, c, d, new UnitValue(xPoint ? UnitValue.POINT : UnitValue.PERCENT
                , tx), new UnitValue(yPoint ? UnitValue.POINT : UnitValue.PERCENT, ty));
        }

        /// <summary>Apply a linear transformation using a transformation matrix</summary>
        /// <param name="a">element [0,0] of the transformation matrix</param>
        /// <param name="b">element [0,1] of the transformation matrix</param>
        /// <param name="c">element [1,0] of the transformation matrix</param>
        /// <param name="d">element [1,1] of the transformation matrix</param>
        /// <param name="tx">translation on x-axis</param>
        /// <param name="ty">translation on y-axis</param>
        /// <returns/>
        private static Transform.SingleTransform GetSingleTransform(float a, float b, float c, float d, float tx, 
            float ty) {
            return new Transform.SingleTransform(a, b, c, d, new UnitValue(UnitValue.POINT, tx), new UnitValue(UnitValue
                .POINT, ty));
        }

        /// <summary>Apply a linear transformation using a transformation matrix</summary>
        /// <param name="floats">the transformation matrix (flattened) as array</param>
        /// <returns/>
        private static Transform.SingleTransform GetSingleTransform(float[] floats) {
            return new Transform.SingleTransform(floats[0], floats[1], floats[2], floats[3], new UnitValue(UnitValue.POINT
                , floats[4]), new UnitValue(UnitValue.POINT, floats[5]));
        }
    }
}
