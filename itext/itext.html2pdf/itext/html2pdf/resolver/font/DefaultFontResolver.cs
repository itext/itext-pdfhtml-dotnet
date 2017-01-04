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
using iText.IO.Font;
using iText.Kernel.Font;

namespace iText.Html2pdf.Resolver.Font {
    public class DefaultFontResolver : IFontResolver {
        internal static readonly Dictionary<String, String> DEFAULT_FONTS = new Dictionary<String, String>();

        static DefaultFontResolver() {
            DEFAULT_FONTS[FontConstants.COURIER.ToLowerInvariant()] = FontConstants.COURIER;
            DEFAULT_FONTS[FontConstants.COURIER_BOLD.ToLowerInvariant()] = FontConstants.COURIER_BOLD;
            DEFAULT_FONTS[FontConstants.COURIER_BOLDOBLIQUE.ToLowerInvariant()] = FontConstants.COURIER_BOLDOBLIQUE;
            DEFAULT_FONTS[FontConstants.COURIER_OBLIQUE.ToLowerInvariant()] = FontConstants.COURIER_OBLIQUE;
            DEFAULT_FONTS[FontConstants.HELVETICA.ToLowerInvariant()] = FontConstants.HELVETICA;
            DEFAULT_FONTS[FontConstants.HELVETICA_BOLD.ToLowerInvariant()] = FontConstants.HELVETICA_BOLD;
            DEFAULT_FONTS[FontConstants.HELVETICA_BOLDOBLIQUE.ToLowerInvariant()] = FontConstants.HELVETICA_BOLDOBLIQUE;
            DEFAULT_FONTS[FontConstants.HELVETICA_OBLIQUE.ToLowerInvariant()] = FontConstants.HELVETICA_OBLIQUE;
            DEFAULT_FONTS[FontConstants.SYMBOL.ToLowerInvariant()] = FontConstants.SYMBOL;
            DEFAULT_FONTS[FontConstants.TIMES_ROMAN.ToLowerInvariant()] = FontConstants.TIMES_ROMAN;
            DEFAULT_FONTS[FontConstants.TIMES_BOLD.ToLowerInvariant()] = FontConstants.TIMES_BOLD;
            DEFAULT_FONTS[FontConstants.TIMES_BOLDITALIC.ToLowerInvariant()] = FontConstants.TIMES_BOLDITALIC;
            DEFAULT_FONTS[FontConstants.TIMES_ITALIC.ToLowerInvariant()] = FontConstants.TIMES_ITALIC;
            DEFAULT_FONTS[FontConstants.ZAPFDINGBATS.ToLowerInvariant()] = FontConstants.ZAPFDINGBATS;
        }

        public DefaultFontResolver() {
        }

        //PdfFontFactory.registerSystemDirectories();
        /// <exception cref="System.IO.IOException"/>
        public virtual PdfFont GetFont(String name) {
            //return PdfFontFactory.createRegisteredFont(name);
            PdfFont result;
            try {
                String defaultName = DEFAULT_FONTS.Get(name.ToLowerInvariant());
                if (defaultName != null) {
                    result = PdfFontFactory.CreateFont(defaultName);
                }
                else {
                    result = PdfFontFactory.CreateFont(name);
                }
            }
            catch (Exception) {
                //LoggerFactory.getLogger(getClass()).error(MessageFormat.format(LogMessageConstant.UNABLE_TO_RESOLVE_FONT, name), any);
                result = PdfFontFactory.CreateFont();
            }
            if (result == null) {
                //LoggerFactory.getLogger(getClass()).error(MessageFormat.format(LogMessageConstant.UNABLE_TO_RESOLVE_FONT, name));
                result = PdfFontFactory.CreateFont();
            }
            return result;
        }
    }
}
