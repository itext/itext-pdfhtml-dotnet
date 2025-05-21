/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using iText.StyledXmlParser.Resolver.Resource;

namespace iText.Html2pdf.Attach.Impl.Tags.Util {
    /// <summary>Utility class to work with the data in A tag.</summary>
    public sealed class ATagUtil {
        private ATagUtil() {
        }

        //Empty constructor
        /// <summary>Resolves a link in A tag.</summary>
        /// <param name="anchorLink">the link in A tag</param>
        /// <param name="baseUrl">the base URL</param>
        /// <returns>the resolved link</returns>
        public static String ResolveAnchorLink(String anchorLink, String baseUrl) {
            if (baseUrl != null) {
                UriResolver uriResolver = new UriResolver(baseUrl);
                if (!anchorLink.StartsWith("#")) {
                    try {
                        String resolvedUri = uriResolver.ResolveAgainstBaseUri(anchorLink).ToExternalForm();
                        if (!anchorLink.EndsWith("/") && resolvedUri.EndsWith("/")) {
                            resolvedUri = resolvedUri.JSubstring(0, resolvedUri.Length - 1);
                        }
                        if (!resolvedUri.StartsWith("file:")) {
                            return resolvedUri;
                        }
                    }
                    catch (UriFormatException) {
                    }
                }
            }
            return anchorLink;
        }
    }
}
