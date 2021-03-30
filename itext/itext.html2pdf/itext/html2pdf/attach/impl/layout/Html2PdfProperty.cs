/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>Set of constants that will be used as keys to get and set properties.</summary>
    public class Html2PdfProperty {
        /// <summary>The Constant PROPERTY_START.</summary>
        private const int PROPERTY_START = (1 << 20);

        /// <summary>The Constant KEEP_WITH_PREVIOUS works only for top-level elements, i.e. ones that are added to the document directly.
        ///     </summary>
        public const int KEEP_WITH_PREVIOUS = PROPERTY_START + 1;

        /// <summary>The Constant PAGE_COUNT_TYPE.</summary>
        public const int PAGE_COUNT_TYPE = PROPERTY_START + 2;

        /// <summary>The Constant FORM_FIELD_FLATTEN for form related properties.</summary>
        public const int FORM_FIELD_FLATTEN = PROPERTY_START + 3;

        /// <summary>The Constant FORM_FIELD_SIZE.</summary>
        public const int FORM_FIELD_SIZE = PROPERTY_START + 4;

        /// <summary>The Constant FORM_FIELD_VALUE.</summary>
        public const int FORM_FIELD_VALUE = PROPERTY_START + 5;

        /// <summary>The Constant FORM_FIELD_PASSWORD_FLAG.</summary>
        public const int FORM_FIELD_PASSWORD_FLAG = PROPERTY_START + 6;

        /// <summary>The Constant FORM_FIELD_COLS.</summary>
        public const int FORM_FIELD_COLS = PROPERTY_START + 7;

        /// <summary>The Constant FORM_FIELD_ROWS.</summary>
        public const int FORM_FIELD_ROWS = PROPERTY_START + 8;

        /// <summary>The Constant FORM_FIELD_CHECKED.</summary>
        public const int FORM_FIELD_CHECKED = PROPERTY_START + 9;

        /// <summary>The Constant BODY_STYLING.</summary>
        public const int BODY_STYLING = PROPERTY_START + 10;

        /// <summary>The Constant HTML_STYLING.</summary>
        public const int HTML_STYLING = PROPERTY_START + 11;

        /// <summary>The Constant FORM_FIELD_MULTIPLE.</summary>
        public const int FORM_FIELD_MULTIPLE = PROPERTY_START + 12;

        /// <summary>The Constant FORM_FIELD_SELECTED.</summary>
        public const int FORM_FIELD_SELECTED = PROPERTY_START + 13;

        /// <summary>The Constant FORM_FIELD_SELECTED.</summary>
        public const int FORM_FIELD_LABEL = PROPERTY_START + 14;

        /// <summary>The Constant FORM_ACCESSIBILITY_LANGUAGE.</summary>
        public const int FORM_ACCESSIBILITY_LANGUAGE = PROPERTY_START + 15;

        /// <summary>The Constant CAPITALIZE_ELEMENT indicates if an inline element needs to be capitalized.</summary>
        public const int CAPITALIZE_ELEMENT = PROPERTY_START + 16;
    }
}
