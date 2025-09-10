using iText.Html2pdf.Html;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>Utility class for handling operations related to labels</summary>
    public sealed class LabelUtil {
        private LabelUtil() {
        }

        // Utility class, no instances allowed
        /// <summary>Determines whether the provided element can be labeled.</summary>
        /// <param name="element">element to be checked</param>
        /// <returns>true if the element can be labeled; false otherwise</returns>
        public static bool IsLabelable(INameContainer element) {
            return TagConstants.INPUT.Equals(element.Name()) || TagConstants.TEXTAREA.Equals(element.Name()) || TagConstants
                .SELECT.Equals(element.Name()) || TagConstants.BUTTON.Equals(element.Name());
        }
    }
}
