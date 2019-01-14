/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using System.IO;
using System.Reflection;
using Common.Logging;
using Versions.Attributes;
using iText.IO.Util;
using iText.Layout.Font;
using iText.StyledXmlParser.Resolver.Font;

namespace iText.Html2pdf.Resolver.Font {
    /// <summary>
    /// The default
    /// <see cref="iText.StyledXmlParser.Resolver.Font.BasicFontProvider"/>
    /// for pdfHTML, that, as opposed to
    /// the font provider in iText 7's styled-xml-parser, also includes a
    /// series of fonts that are shipped with the add-on.
    /// </summary>
    public class DefaultFontProvider : BasicFontProvider {
        /// <summary>The logger.</summary>
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(iText.Html2pdf.Resolver.Font.DefaultFontProvider
            ));

        /// <summary>The path to the shipped fonts.</summary>
        private const String SHIPPED_FONT_RESOURCE_PATH = "iText.Html2Pdf.font.";

        /// <summary>The file names of the shipped fonts.</summary>
        private static readonly String[] SHIPPED_FONT_NAMES = new String[] { "FreeMono.ttf", "FreeMonoBold.ttf", "FreeMonoBoldOblique.ttf"
            , "FreeMonoOblique.ttf", "FreeSans.ttf", "FreeSansBold.ttf", "FreeSansBoldOblique.ttf", "FreeSansOblique.ttf"
            , "FreeSerif.ttf", "FreeSerifBold.ttf", "FreeSerifBoldItalic.ttf", "FreeSerifItalic.ttf" };

        private static readonly Range FREE_FONT_RANGE = new RangeBuilder().AddRange(0, 0x058F).AddRange(0x0E80, int.MaxValue
            ).Create();

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
        /// <param name="registerShippedFreeFonts">use true if you want to register the shipped fonts (can be embedded)
        ///     </param>
        /// <param name="registerSystemFonts">use true if you want to register the system fonts (can require quite some resources)
        ///     </param>
        public DefaultFontProvider(bool registerStandardPdfFonts, bool registerShippedFreeFonts, bool registerSystemFonts
            )
            : base(registerStandardPdfFonts, registerSystemFonts) {
            // This range exclude Hebrew, Arabic, Syriac, Arabic Supplement, Thaana, NKo, Samaritan,
            // Mandaic, Syriac Supplement, Arabic Extended-A, Devanagari, Bengali, Gurmukhi, Gujarati,
            // Oriya, Tamil, Telugu, Kannada, Malayalam, Sinhala, Thai unicode blocks.
            // Those blocks either require pdfCalligraph or do not supported by GNU Free Fonts.
            if (registerShippedFreeFonts) {
                AddShippedFreeFonts(AddCalligraphFonts());
            }
        }

        /// <summary>Adds the shipped free fonts.</summary>
        private void AddShippedFreeFonts(Range rangeToLoad) {
            foreach (String fontName in SHIPPED_FONT_NAMES) {
                try {
                    using (Stream stream = ResourceUtil.GetResourceStream(SHIPPED_FONT_RESOURCE_PATH + fontName)) {
                        byte[] fontProgramBytes = StreamUtil.InputStreamToArray(stream);
                        AddFont(fontProgramBytes, null, rangeToLoad);
                    }
                }
                catch (Exception) {
                    LOGGER.Error(iText.Html2pdf.LogMessageConstant.ERROR_LOADING_FONT);
                }
            }
        }

        private Range AddCalligraphFonts() {
            String methodName = "LoadShippedFonts";
            Type klass = null;
            try {
                klass = GetTypographyUtilsClass();
            }
            catch (TypeLoadException) {
            }
            if (klass != null) {
                try {
                    MethodInfo m = klass.GetMethod(methodName);
                    List<byte[]> fontStreams = (List<byte[]>)m.Invoke(null, null);
                    foreach (byte[] font in fontStreams) {
                        AddFont(font);
                    }
                    // here we return a unicode range that excludes the loaded from the calligraph module fonts
                    // i.e. the unicode range that is to be rendered with standard or shipped free fonts
                    return FREE_FONT_RANGE;
                }
                catch (Exception) {
                    LOGGER.Error(iText.Html2pdf.LogMessageConstant.ERROR_LOADING_FONT);
                }
            }
            return null;
        }

        private static Type GetTypographyUtilsClass() {
            String partialName = "iText.Typography.Util.TypographyShippedFontsUtil, iText.Typography";
            String classFullName = null;

            Assembly html2pdfAssembly = typeof(DefaultFontProvider).GetAssembly();
            try {
                Attribute customAttribute = html2pdfAssembly.GetCustomAttribute(typeof(TypographyVersionAttribute));
                if (customAttribute is TypographyVersionAttribute) {
                    string typographyVersion = ((TypographyVersionAttribute) customAttribute).TypographyVersion;
                    string format = "{0}, Version={1}, Culture=neutral, PublicKeyToken=8354ae6d2174ddca";
                    classFullName = String.Format(format, partialName, typographyVersion);
                }
            } catch (Exception ignored) {
            }

            Type type = null;
            if (classFullName != null) {
                String fileLoadExceptionMessage = null;
                try {
                    type = System.Type.GetType(classFullName);
                } catch (FileLoadException fileLoadException) {
                    fileLoadExceptionMessage = fileLoadException.Message;
                }
                if (type == null) {
                    // try to find typography assembly by it's partial name and check if it refers to current version of itext core
                    try {
                        type = System.Type.GetType(partialName);
                    } catch {
                        // ignore
                    }
                    if (type != null) {
                        bool doesReferToCurrentVersionOfCore = false;
                        foreach (AssemblyName assemblyName in type.GetAssembly().GetReferencedAssemblies()) {
                            if ("itext.io".Equals(assemblyName.Name)) {
                                doesReferToCurrentVersionOfCore = assemblyName.Version.Equals(html2pdfAssembly.GetName().Version);
                                break;
                            }
                        }
                        if (!doesReferToCurrentVersionOfCore) {
                            type = null;
                        }
                    }
                }
            }

            return type;
        }
    }
}
