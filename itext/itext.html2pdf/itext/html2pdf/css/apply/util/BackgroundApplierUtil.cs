/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply backgrounds.</summary>
    public sealed class BackgroundApplierUtil {
        /// <summary>
        /// Creates a new
        /// <see cref="BackgroundApplierUtil"/>
        /// instance.
        /// </summary>
        private BackgroundApplierUtil() {
        }

        /// <summary>Applies background to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyBackground(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String backgroundColorStr = cssProps.Get(CssConstants.BACKGROUND_COLOR);
            if (backgroundColorStr != null && !CssConstants.TRANSPARENT.Equals(backgroundColorStr)) {
                float[] rgbaColor = CssUtils.ParseRgbaColor(backgroundColorStr);
                Color color = new DeviceRgb(rgbaColor[0], rgbaColor[1], rgbaColor[2]);
                float opacity = rgbaColor[3];
                Background backgroundColor = new Background(color, opacity);
                element.SetProperty(Property.BACKGROUND, backgroundColor);
            }
            String backgroundImageStr = cssProps.Get(CssConstants.BACKGROUND_IMAGE);
            if (backgroundImageStr != null && !backgroundImageStr.Equals(CssConstants.NONE)) {
                String backgroundRepeatStr = cssProps.Get(CssConstants.BACKGROUND_REPEAT);
                PdfImageXObject image = context.GetResourceResolver().RetrieveImage(CssUtils.ExtractUrl(backgroundImageStr
                    ));
                bool repeatX = true;
                bool repeatY = true;
                if (backgroundRepeatStr != null) {
                    repeatX = backgroundRepeatStr.Equals(CssConstants.REPEAT) || backgroundRepeatStr.Equals(CssConstants.REPEAT_X
                        );
                    repeatY = backgroundRepeatStr.Equals(CssConstants.REPEAT) || backgroundRepeatStr.Equals(CssConstants.REPEAT_Y
                        );
                }
                if (image != null) {
                    BackgroundImage backgroundImage = new BackgroundImage(image, repeatX, repeatY);
                    element.SetProperty(Property.BACKGROUND_IMAGE, backgroundImage);
                }
            }
        }
    }
}
