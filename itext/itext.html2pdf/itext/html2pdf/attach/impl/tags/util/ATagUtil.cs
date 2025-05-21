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
