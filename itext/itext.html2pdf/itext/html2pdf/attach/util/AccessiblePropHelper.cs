using System;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf.Tagutils;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Util {
    public class AccessiblePropHelper {
        public static void TrySetLangAttribute(IAccessibleElement accessibleElement, IElementNode element) {
            String lang = element.GetAttribute(AttributeConstants.LANG);
            TrySetLangAttribute(accessibleElement, lang);
        }

        public static void TrySetLangAttribute(IAccessibleElement accessibleElement, String lang) {
            if (lang != null) {
                AccessibilityProperties properties = accessibleElement.GetAccessibilityProperties();
                if (properties.GetLanguage() == null) {
                    properties.SetLanguage(lang);
                }
            }
        }
    }
}
