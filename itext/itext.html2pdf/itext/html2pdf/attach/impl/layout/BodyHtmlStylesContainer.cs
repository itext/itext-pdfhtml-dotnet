/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// This class is used to store styles of
    /// <c>&lt;html&gt;</c>
    /// and
    /// <c>&lt;body&gt;</c>
    /// tags,
    /// to simplify their application on the document as an
    /// <see cref="Html2PdfProperty"/>
    /// and to simplify their processing on the layout level.
    /// This class is primarily meant for internal usage.
    /// </summary>
    public class BodyHtmlStylesContainer : IPropertyContainer {
        /// <summary><inheritDoc/></summary>
        protected internal IDictionary<int, Object> properties = new Dictionary<int, Object>();

        /// <summary><inheritDoc/></summary>
        public virtual void SetProperty(int property, Object value) {
            properties.Put(property, value);
        }

        /// <summary><inheritDoc/></summary>
        public virtual bool HasProperty(int property) {
            return HasOwnProperty(property);
        }

        /// <summary><inheritDoc/></summary>
        public virtual bool HasOwnProperty(int property) {
            return properties.ContainsKey(property);
        }

        /// <summary><inheritDoc/></summary>
        public virtual void DeleteOwnProperty(int property) {
            properties.JRemove(property);
        }

        /// <summary><inheritDoc/></summary>
        public virtual T1 GetProperty<T1>(int property) {
            return (T1)this.GetOwnProperty<T1>(property);
        }

        /// <summary><inheritDoc/></summary>
        public virtual T1 GetOwnProperty<T1>(int property) {
            return (T1)properties.Get(property);
        }

        /// <summary><inheritDoc/></summary>
        public virtual T1 GetDefaultProperty<T1>(int property) {
            switch (property) {
                case Property.MARGIN_TOP:
                case Property.MARGIN_RIGHT:
                case Property.MARGIN_BOTTOM:
                case Property.MARGIN_LEFT:
                case Property.PADDING_TOP:
                case Property.PADDING_RIGHT:
                case Property.PADDING_BOTTOM:
                case Property.PADDING_LEFT: {
                    return (T1)(Object)UnitValue.CreatePointValue(0f);
                }

                default: {
                    return (T1)(Object)null;
                }
            }
        }

        /// <summary>
        /// This method is needed to check if we need to draw a simulated
        /// <see cref="iText.Layout.Element.Div"/>
        /// element,
        /// i.e. to perform any drawing at all.
        /// </summary>
        /// <returns>
        /// true if there is at least one
        /// <see cref="iText.Layout.Borders.Border"/>
        /// or a
        /// <see cref="iText.Layout.Properties.Background"/>
        /// or a
        /// <see cref="iText.Layout.Properties.BackgroundImage"/>
        /// present in the container
        /// </returns>
        public virtual bool HasContentToDraw() {
            Border[] borders = GetBodyHtmlBorders();
            for (int i = 0; i < 4; i++) {
                if (borders[i] != null && borders[i].GetWidth() != 0) {
                    return true;
                }
            }
            return this.GetOwnProperty<Background>(Property.BACKGROUND) != null || this.GetOwnProperty<BackgroundImage
                >(Property.BACKGROUND_IMAGE) != null;
        }

        /// <summary>This method is needed to check if there are styles applied on the current element.</summary>
        /// <returns>true if in the container there are present styles which impact the layout</returns>
        public virtual bool HasStylesToApply() {
            float[] totalWidth = GetTotalWidth();
            for (int i = 0; i < 4; i++) {
                if (totalWidth[i] > 0) {
                    return true;
                }
            }
            return this.GetOwnProperty<Background>(Property.BACKGROUND) != null || this.GetOwnProperty<BackgroundImage
                >(Property.BACKGROUND_IMAGE) != null;
        }

        /// <summary>
        /// This method calculates the total widths of TOP-, RIGHT-, BOTTOM- and LEFT- side styles,
        /// each combined of widths of applied margins, borders and paddings.
        /// </summary>
        /// <returns>a float array containing applied TOP-, RIGHT-, BOTTOM- and LEFT- side widths of styles respectively
        ///     </returns>
        public virtual float[] GetTotalWidth() {
            Border[] borders = GetBodyHtmlBorders();
            float[] margins = GetBodyHtmlMarginsOrPaddings(true);
            float[] paddings = GetBodyHtmlMarginsOrPaddings(false);
            float[] width = new float[4];
            for (int i = 0; i < 4; i++) {
                width[i] += margins[i] + paddings[i];
                if (borders[i] != null) {
                    width[i] += borders[i].GetWidth();
                }
            }
            return width;
        }

        private float[] GetBodyHtmlMarginsOrPaddings(bool margin) {
            int[] property = new int[4];
            if (margin) {
                property[0] = Property.MARGIN_TOP;
                property[1] = Property.MARGIN_RIGHT;
                property[2] = Property.MARGIN_BOTTOM;
                property[3] = Property.MARGIN_LEFT;
            }
            else {
                property[0] = Property.PADDING_TOP;
                property[1] = Property.PADDING_RIGHT;
                property[2] = Property.PADDING_BOTTOM;
                property[3] = Property.PADDING_LEFT;
            }
            float[] widths = new float[4];
            UnitValue[] widthsProperties = new UnitValue[4];
            for (int i = 0; i < 4; i++) {
                widthsProperties[i] = this.GetOwnProperty<UnitValue>(property[i]);
                if (widthsProperties[i] != null && widthsProperties[i].IsPointValue()) {
                    widths[i] = ((UnitValue)widthsProperties[i]).GetValue();
                }
            }
            return widths;
        }

        private Border[] GetBodyHtmlBorders() {
            Border[] border = new Border[4];
            border[0] = this.GetOwnProperty<Border>(Property.BORDER_TOP);
            border[1] = this.GetOwnProperty<Border>(Property.BORDER_RIGHT);
            border[2] = this.GetOwnProperty<Border>(Property.BORDER_BOTTOM);
            border[3] = this.GetOwnProperty<Border>(Property.BORDER_LEFT);
            return border;
        }
    }
}
