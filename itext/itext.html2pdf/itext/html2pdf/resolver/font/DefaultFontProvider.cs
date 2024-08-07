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
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Html2pdf.Logs;
using iText.IO.Util;
using iText.Layout.Font;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Resolver.Font;

namespace iText.Html2pdf.Resolver.Font {
    /// <summary>
    /// The default
    /// <see cref="iText.StyledXmlParser.Resolver.Font.BasicFontProvider"/>
    /// for pdfHTML, that, as opposed to
    /// the font provider in iText's styled-xml-parser, also includes a
    /// series of fonts that are shipped with the add-on.
    /// </summary>
    public class DefaultFontProvider : BasicFontProvider {
//\cond DO_NOT_DOCUMENT
        /// <summary>The path to the shipped fonts.</summary>
        internal const String SHIPPED_FONT_RESOURCE_PATH = "iText.Html2Pdf.font.";
//\endcond

//\cond DO_NOT_DOCUMENT
        /// <summary>The file names of the shipped fonts.</summary>
        internal static readonly String[] SHIPPED_FONT_NAMES = new String[] { "NotoSansMono-Regular.ttf", "NotoSansMono-Bold.ttf"
            , "NotoSans-Regular.ttf", "NotoSans-Bold.ttf", "NotoSans-BoldItalic.ttf", "NotoSans-Italic.ttf", "NotoSerif-Regular.ttf"
            , "NotoSerif-Bold.ttf", "NotoSerif-BoldItalic.ttf", "NotoSerif-Italic.ttf" };
//\endcond

        /// <summary>The logger.</summary>
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Resolver.Font.DefaultFontProvider
            ));

        private const String DEFAULT_FONT_FAMILY = "Times";

        // This range exclude Hebrew, Arabic, Syriac, Arabic Supplement, Thaana, NKo, Samaritan,
        // Mandaic, Syriac Supplement, Arabic Extended-A, Devanagari, Bengali, Gurmukhi, Gujarati,
        // Oriya, Tamil, Telugu, Kannada, Malayalam, Sinhala, Thai unicode blocks.
        // Those blocks either require pdfCalligraph or do not supported by GNU Free Fonts.
        private static readonly Range FREE_FONT_RANGE = new RangeBuilder().AddRange(0, 0x058F).AddRange(0x0E80, int.MaxValue
            ).Create();

        //we want to add free fonts to font provider before calligraph fonts. However, the existing public API states
        // that addCalligraphFonts() should be used first to load calligraph fonts and to define the range for loading free fonts.
        // In order to maintain backward compatibility, this temporary field is used to stash calligraph fonts before free fonts are loaded.
        private IList<byte[]> calligraphyFontsTempList = new List<byte[]>();

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
            : this(registerStandardPdfFonts, registerShippedFonts, registerSystemFonts, DEFAULT_FONT_FAMILY) {
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
            : base(registerStandardPdfFonts, registerSystemFonts, defaultFontFamily) {
            if (registerShippedFonts) {
                AddAllAvailableFonts(AddCalligraphFonts());
            }
        }

        private void AddAllAvailableFonts(Range rangeToLoad) {
            AddShippedFonts(rangeToLoad);
            foreach (byte[] fontData in calligraphyFontsTempList) {
                AddFont(fontData, null);
            }
            calligraphyFontsTempList = null;
        }

        /// <summary>Adds the shipped fonts.</summary>
        /// <param name="rangeToLoad">
        /// a unicode
        /// <see cref="iText.Layout.Font.Range"/>
        /// to load characters
        /// </param>
        private void AddShippedFonts(Range rangeToLoad) {
            foreach (String fontName in SHIPPED_FONT_NAMES) {
                try {
                    using (Stream stream = ResourceUtil.GetResourceStream(SHIPPED_FONT_RESOURCE_PATH + fontName)) {
                        byte[] fontProgramBytes = StreamUtil.InputStreamToArray(stream);
                        AddFont(fontProgramBytes, null, rangeToLoad);
                    }
                }
                catch (Exception) {
                    LOGGER.LogError(Html2PdfLogMessageConstant.ERROR_LOADING_FONT);
                }
            }
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
        /// </remarks>
        /// <returns>
        /// a unicode
        /// <see cref="iText.Layout.Font.Range"/>
        /// that excludes the loaded from pdfCalligraph fonts,
        /// i.e. the unicode range that is to be rendered with any other font contained in this FontProvider
        /// </returns>
        protected internal virtual Range AddCalligraphFonts() {
            if (TypographyUtils.IsPdfCalligraphAvailable()) {
                try {
                    IDictionary<String, byte[]> fontStreams = TypographyUtils.LoadShippedFonts();
                    this.calligraphyFontsTempList.AddAll(fontStreams.Values);
                    // here we return a unicode range that excludes the loaded from the calligraph module fonts
                    // i.e. the unicode range that is to be rendered with standard or shipped free fonts
                    return FREE_FONT_RANGE;
                }
                catch (Exception) {
                    LOGGER.LogError(Html2PdfLogMessageConstant.ERROR_LOADING_FONT);
                }
            }
            return null;
        }
    }
}
