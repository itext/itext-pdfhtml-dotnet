/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using iText.Layout.Font;
using iText.StyledXmlParser.Resolver.Font;

namespace iText.Html2pdf.Resolver.Font {
    /// <summary>
    /// The default
    /// <see cref="iText.StyledXmlParser.Resolver.Font.BasicFontProvider"/>
    /// for pdfHTML, that, as opposed to
    /// the font provider in iText's styled-xml-parser, also includes a
    /// series of fonts that are shipped with the add-on.
    /// </summary>
    /// <remarks>
    /// The default
    /// <see cref="iText.StyledXmlParser.Resolver.Font.BasicFontProvider"/>
    /// for pdfHTML, that, as opposed to
    /// the font provider in iText's styled-xml-parser, also includes a
    /// series of fonts that are shipped with the add-on.
    /// <para />
    /// Deprecated in favour of
    /// <see cref="iText.StyledXmlParser.Resolver.Font.BasicFontProvider"/>
    /// since it has the same functionality
    /// This class will be removed and
    /// <see cref="iText.StyledXmlParser.Resolver.Font.BasicFontProvider"/>
    /// will be renamed to
    /// <c>DefaultFontProvider</c>
    /// </remarks>
    [Obsolete]
    public class DefaultFontProvider : BasicFontProvider {
        /// <summary>
        /// Creates a new
        /// <see cref="DefaultFontProvider"/>
        /// instance.
        /// </summary>
        public DefaultFontProvider()
            : this(true, true, false) {
        }

        /// <summary>
        /// Creates a new
        /// <see cref="DefaultFontProvider"/>
        /// instance.
        /// </summary>
        /// <param name="registerStandardPdfFonts">use true if you want to register the standard Type 1 fonts (can't be embedded)
        ///     </param>
        /// <param name="registerShippedFonts">use true if you want to register the shipped fonts (can be embedded)</param>
        /// <param name="registerSystemFonts">use true if you want to register the system fonts (can require quite some resources)
        ///     </param>
        public DefaultFontProvider(bool registerStandardPdfFonts, bool registerShippedFonts, bool registerSystemFonts
            )
            : base(registerStandardPdfFonts, registerShippedFonts, registerSystemFonts) {
        }

        /// <summary>
        /// Creates a new
        /// <see cref="DefaultFontProvider"/>
        /// instance.
        /// </summary>
        /// <param name="registerStandardPdfFonts">use true if you want to register the standard Type 1 fonts (can't be embedded)
        ///     </param>
        /// <param name="registerShippedFonts">use true if you want to register the shipped fonts (can be embedded)</param>
        /// <param name="registerSystemFonts">use true if you want to register the system fonts (can require quite some resources)
        ///     </param>
        /// <param name="defaultFontFamily">default font family</param>
        public DefaultFontProvider(bool registerStandardPdfFonts, bool registerShippedFonts, bool registerSystemFonts
            , String defaultFontFamily)
            : base(registerStandardPdfFonts, registerShippedFonts, registerSystemFonts, defaultFontFamily) {
        }

        /// <summary>Adds the shipped fonts.</summary>
        /// <remarks>
        /// Adds the shipped fonts.
        /// <para />
        /// Deprecated since similar method was added to parent class.
        /// </remarks>
        /// <param name="rangeToLoad">
        /// a unicode
        /// <see cref="iText.Layout.Font.Range"/>
        /// to load characters
        /// </param>
        [Obsolete]
        protected override void AddShippedFonts(Range rangeToLoad) {
            base.AddShippedFonts(rangeToLoad);
        }

        /// <summary>This method loads a list of noto fonts from pdfCalligraph (if present in the classpath!) into FontProvider.
        ///     </summary>
        /// <remarks>
        /// This method loads a list of noto fonts from pdfCalligraph (if present in the classpath!) into FontProvider.
        /// The list is the following (each font is represented in regular and bold types): NotoSansArabic, NotoSansGurmukhi,
        /// NotoSansOriya, NotoSerifBengali, NotoSerifDevanagari, NotoSerifGujarati, NotoSerifHebrew, NotoSerifKannada,
        /// NotoSerifKhmer, NotoSerifMalayalam, NotoSerifTamil, NotoSerifTelugu, NotoSerifThai.
        /// If it's needed to have a DefaultFontProvider without typography fonts loaded,
        /// create an extension of DefaultFontProvider and override this method so it does nothing and only returns null.
        /// <para />
        /// Deprecated since similar method was added to parent class.
        /// </remarks>
        /// <returns>
        /// a unicode
        /// <see cref="iText.Layout.Font.Range"/>
        /// that excludes the loaded from pdfCalligraph fonts,
        /// i.e. the unicode range that is to be rendered with any other font contained in this FontProvider
        /// </returns>
        [Obsolete]
        protected override Range AddCalligraphFonts() {
            return base.AddCalligraphFonts();
        }
    }
}
