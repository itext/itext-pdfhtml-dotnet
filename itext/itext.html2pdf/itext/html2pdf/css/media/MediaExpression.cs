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
using iText.Html2pdf.Css.Util;

namespace iText.Html2pdf.Css.Media {
    public class MediaExpression {
        private bool minPrefix;

        private bool maxPrefix;

        private String feature;

        private String value;

        public MediaExpression(String feature, String value) {
            this.feature = feature.Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture);
            if (value != null) {
                this.value = value.Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture);
            }
            String minPref = MediaRuleConstants.MIN + "-";
            String maxPref = MediaRuleConstants.MAX + "-";
            minPrefix = feature.StartsWith(minPref);
            if (minPrefix) {
                this.feature = feature.Substring(minPref.Length);
            }
            maxPrefix = feature.StartsWith(maxPref);
            if (maxPrefix) {
                this.feature = feature.Substring(maxPref.Length);
            }
        }

        public virtual bool Matches(MediaDeviceDescription deviceDescription) {
            switch (feature) {
                case MediaFeature.COLOR: {
                    int? val = CssUtils.ParseInteger(value);
                    if (minPrefix) {
                        return val != null && deviceDescription.GetBitsPerComponent() >= val;
                    }
                    else {
                        if (maxPrefix) {
                            return val != null && deviceDescription.GetBitsPerComponent() <= val;
                        }
                        else {
                            return val == null ? deviceDescription.GetBitsPerComponent() != 0 : val == deviceDescription.GetBitsPerComponent
                                ();
                        }
                    }
                    goto case MediaFeature.COLOR_INDEX;
                }

                case MediaFeature.COLOR_INDEX: {
                    int? val = CssUtils.ParseInteger(value);
                    if (minPrefix) {
                        return val != null && deviceDescription.GetColorIndex() >= val;
                    }
                    else {
                        if (maxPrefix) {
                            return val != null && deviceDescription.GetColorIndex() <= val;
                        }
                        else {
                            return val == null ? deviceDescription.GetColorIndex() != 0 : val == deviceDescription.GetColorIndex();
                        }
                    }
                    goto case MediaFeature.ASPECT_RATIO;
                }

                case MediaFeature.ASPECT_RATIO: {
                    int[] aspectRatio = CssUtils.ParseAspectRatio(value);
                    if (minPrefix) {
                        return aspectRatio != null && aspectRatio[0] * deviceDescription.GetHeight() >= aspectRatio[1] * deviceDescription
                            .GetWidth();
                    }
                    else {
                        if (maxPrefix) {
                            return aspectRatio != null && aspectRatio[0] * deviceDescription.GetHeight() <= aspectRatio[1] * deviceDescription
                                .GetWidth();
                        }
                        else {
                            return aspectRatio != null && aspectRatio[0] * deviceDescription.GetHeight() == aspectRatio[1] * deviceDescription
                                .GetWidth();
                        }
                    }
                    goto case MediaFeature.GRID;
                }

                case MediaFeature.GRID: {
                    int? val = CssUtils.ParseInteger(value);
                    return val != null && val == 0 && !deviceDescription.IsGrid() || deviceDescription.IsGrid();
                }

                case MediaFeature.SCAN: {
                    return System.Object.Equals(value, deviceDescription.GetScan());
                }

                case MediaFeature.ORIENTATION: {
                    return System.Object.Equals(value, deviceDescription.GetOrientation());
                }

                case MediaFeature.MONOCHROME: {
                    int? val = CssUtils.ParseInteger(value);
                    if (minPrefix) {
                        return val != null && deviceDescription.GetMonochrome() >= val;
                    }
                    else {
                        if (maxPrefix) {
                            return val != null && deviceDescription.GetMonochrome() <= val;
                        }
                        else {
                            return val == null ? deviceDescription.GetMonochrome() > 0 : val == deviceDescription.GetMonochrome();
                        }
                    }
                    goto case MediaFeature.HEIGHT;
                }

                case MediaFeature.HEIGHT: {
                    float val = CssUtils.ParseAbsoluteLength(value);
                    if (minPrefix) {
                        return deviceDescription.GetHeight() >= val;
                    }
                    else {
                        if (maxPrefix) {
                            return deviceDescription.GetHeight() <= val;
                        }
                        else {
                            return deviceDescription.GetHeight() > 0;
                        }
                    }
                    goto case MediaFeature.WIDTH;
                }

                case MediaFeature.WIDTH: {
                    float val = CssUtils.ParseAbsoluteLength(value);
                    if (minPrefix) {
                        return deviceDescription.GetWidth() >= val;
                    }
                    else {
                        if (maxPrefix) {
                            return deviceDescription.GetWidth() <= val;
                        }
                        else {
                            return deviceDescription.GetWidth() > 0;
                        }
                    }
                    goto case MediaFeature.RESOLUTION;
                }

                case MediaFeature.RESOLUTION: {
                    float val = CssUtils.ParseResolution(value);
                    if (minPrefix) {
                        return deviceDescription.GetResolution() >= val;
                    }
                    else {
                        if (maxPrefix) {
                            return deviceDescription.GetResolution() <= val;
                        }
                        else {
                            return deviceDescription.GetResolution() > 0;
                        }
                    }
                    goto default;
                }

                default: {
                    return false;
                }
            }
        }
    }
}
