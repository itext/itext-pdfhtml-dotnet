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

namespace iText.Html2pdf.Css.Media {
    public class MediaDeviceDescription {
        private String type;

        private int bitsPerComponent = 0;

        private int colorIndex = 0;

        private float width;

        private float height;

        private bool isGrid;

        private String scan;

        private String orientation;

        private int monochrome;

        private float resolution;

        public MediaDeviceDescription(String type) {
            // in points
            // in points
            // in dpi // TODO change default units? If so, change CssUtils#parseResolution as well
            this.type = type;
        }

        public MediaDeviceDescription(String type, float width, float height)
            : this(type) {
            this.width = width;
            this.height = height;
        }

        public static iText.Html2pdf.Css.Media.MediaDeviceDescription CreateDefault() {
            return new iText.Html2pdf.Css.Media.MediaDeviceDescription(MediaType.ALL);
        }

        public virtual String GetType() {
            return type;
        }

        public virtual int GetBitsPerComponent() {
            return bitsPerComponent;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetBitsPerComponent(int bitsPerComponent) {
            this.bitsPerComponent = bitsPerComponent;
            return this;
        }

        public virtual int GetColorIndex() {
            return colorIndex;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetColorIndex(int colorIndex) {
            this.colorIndex = colorIndex;
            return this;
        }

        public virtual float GetWidth() {
            return width;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetWidth(float width) {
            this.width = width;
            return this;
        }

        public virtual float GetHeight() {
            return height;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetHeight(float height) {
            this.height = height;
            return this;
        }

        public virtual bool IsGrid() {
            return isGrid;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetGrid(bool grid) {
            isGrid = grid;
            return this;
        }

        public virtual String GetScan() {
            return scan;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetScan(String scan) {
            this.scan = scan;
            return this;
        }

        public virtual String GetOrientation() {
            return orientation;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetOrientation(String orientation) {
            this.orientation = orientation;
            return this;
        }

        public virtual int GetMonochrome() {
            return monochrome;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetMonochrome(int monochrome) {
            this.monochrome = monochrome;
            return this;
        }

        public virtual float GetResolution() {
            return resolution;
        }

        public virtual iText.Html2pdf.Css.Media.MediaDeviceDescription SetResolution(float resolution) {
            this.resolution = resolution;
            return this;
        }
    }
}
