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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.IO.Util;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Css.Parse;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Resolve {
    /// <summary>Utilities class to get the styles of a node.</summary>
    internal class UserAgentCss {
        /// <summary>The path to the default CSS file.</summary>
        private const String DEFAULT_CSS_PATH = "iText.Html2Pdf.default.css";

        /// <summary>
        /// The default
        /// <see cref="iText.StyledXmlParser.Css.CssStyleSheet"/>
        /// instance.
        /// </summary>
        private static readonly CssStyleSheet defaultCss;

        static UserAgentCss() {
            CssStyleSheet parsedStylesheet = new CssStyleSheet();
            try {
                parsedStylesheet = CssStyleSheetParser.Parse(ResourceUtil.GetResourceStream(DEFAULT_CSS_PATH));
            }
            catch (Exception exc) {
                ILogger logger = ITextLogManager.GetLogger(typeof(UserAgentCss));
                logger.LogError(exc, "Error parsing default.css");
            }
            finally {
                defaultCss = parsedStylesheet;
            }
        }

        /// <summary>Gets the styles of a node.</summary>
        /// <param name="node">the node</param>
        /// <returns>
        /// a list of
        /// <see cref="iText.StyledXmlParser.Css.CssDeclaration"/>
        /// values
        /// </returns>
        public static IList<CssDeclaration> GetStyles(INode node) {
            return defaultCss.GetCssDeclarations(node, MediaDeviceDescription.CreateDefault());
        }
    }
}
