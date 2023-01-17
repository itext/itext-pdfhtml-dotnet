using System;
using System.Collections.Generic;
using System.Text;
using iText.Commons.Utils;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    public sealed class TextDecorationApplierUtil {
        private const String IGNORED = "__ignored__";

        private static readonly TextDecorationApplierUtil.SupportedTextDecoration[] SUPPORTED_TEXT_DECORATION_PROPERTIES
             = new TextDecorationApplierUtil.SupportedTextDecoration[] { new TextDecorationApplierUtil.SupportedTextDecoration
            (CommonCssConstants.TEXT_DECORATION_LINE, IGNORED), new TextDecorationApplierUtil.SupportedTextDecoration
            (CommonCssConstants.TEXT_DECORATION_STYLE, CommonCssConstants.SOLID), new TextDecorationApplierUtil.SupportedTextDecoration
            (CommonCssConstants.TEXT_DECORATION_COLOR, CommonCssConstants.CURRENTCOLOR) };

        private static readonly int AMOUNT = SUPPORTED_TEXT_DECORATION_PROPERTIES.Length;

        private TextDecorationApplierUtil() {
        }

        public static void PropagateTextDecorationProperties(IElementNode currentNode) {
            ExpandTextDecorationProperties(currentNode);
            if (!ShouldPropagateTextDecorationProperties(currentNode)) {
                return;
            }
            MergeProperties(currentNode);
        }

        private static bool ShouldOnlyKeepParentProperties(IElementNode currentNode) {
            return currentNode.GetStyles().Get(CommonCssConstants.TEXT_DECORATION_LINE) != null && currentNode.GetStyles
                ().Get(CommonCssConstants.TEXT_DECORATION_LINE).Contains(CommonCssConstants.NONE);
        }

        private static bool ShouldPropagateTextDecorationProperties(IElementNode currentNode) {
            IElementNode parent = (IElementNode)currentNode.ParentNode();
            if (parent == null || parent.GetStyles() == null) {
                return false;
            }
            if (DoesNotHaveTextDecorationKey(currentNode) && DoesNotHaveTextDecorationKey(parent)) {
                return false;
            }
            if (CommonCssConstants.INLINE_BLOCK.Equals(currentNode.GetStyles().Get(CommonCssConstants.DISPLAY))) {
                return false;
            }
            return true;
        }

        private static IList<String> ParseTokenIntoList(IElementNode node, String propertyName) {
            String currentValue = node.GetStyles().Get(propertyName);
            return currentValue == null ? new List<String>() : JavaUtil.ArraysAsList(iText.Commons.Utils.StringUtil.Split
                (currentValue, "\\s+"));
        }

        private static void MergeProperties(IElementNode node) {
            // this method expands the text decoration properties
            // example we have a  style like this:
            //      text-decoration-line: overline underline
            //      text-decoration-color: pink
            //      text-decoration-style: solid
            // will become underlying to avoid loss of data
            //      text-decoration-line: overline underline
            //      text-decoration-color: pink pink
            //      text-decoration-style: solid solid
            bool onlyKeepParentProperties = ShouldOnlyKeepParentProperties(node);
            IList<IList<String>> data = new List<IList<String>>();
            foreach (TextDecorationApplierUtil.SupportedTextDecoration supportedTextDecoration in SUPPORTED_TEXT_DECORATION_PROPERTIES
                ) {
                IList<String> currentValues = ParseTokenIntoList(node, supportedTextDecoration.GetPropertyName());
                IList<String> parentValues = ParseTokenIntoList((IElementNode)node.ParentNode(), supportedTextDecoration.GetPropertyName
                    ());
                IList<String> properties = new List<String>(parentValues);
                data.Add(properties);
                if (currentValues.IsEmpty() || onlyKeepParentProperties) {
                    continue;
                }
                properties.AddAll(currentValues);
            }
            int amount = data[0].Count;
            ICollection<String> uniqueMaker = new LinkedHashSet<String>();
            //create a list for the keys so the location of each property is the same
            IList<int> indexListToKeep = new List<int>();
            for (int i = 0; i < amount; i++) {
                StringBuilder hashBuilder = new StringBuilder();
                for (int j = 0; j < AMOUNT; j++) {
                    hashBuilder.Append(data[j][i]);
                }
                String hash = hashBuilder.ToString();
                // if it doesn't contain the hash this value is unique so the indexes should be stored
                if (!hash.Contains(IGNORED) && !uniqueMaker.Contains(hash)) {
                    indexListToKeep.Add((int)i);
                    uniqueMaker.Add(hashBuilder.ToString());
                }
            }
            for (int i = 0; i < AMOUNT; i++) {
                StringBuilder result = new StringBuilder();
                foreach (int? integer in indexListToKeep) {
                    int index = (int)integer;
                    result.Append(data[i][index]).Append(' ');
                }
                node.GetStyles().Put(SUPPORTED_TEXT_DECORATION_PROPERTIES[i].GetPropertyName(), result.ToString().Trim());
            }
        }

        private static bool DoesNotHaveTextDecorationKey(IElementNode node) {
            for (int i = 0; i < AMOUNT; i++) {
                if (node.GetStyles().ContainsKey(SUPPORTED_TEXT_DECORATION_PROPERTIES[i].GetPropertyName())) {
                    return false;
                }
            }
            return true;
        }

        private static void ExpandTextDecorationProperties(IElementNode node) {
            if (DoesNotHaveTextDecorationKey(node)) {
                return;
            }
            IList<String[]> currentValuesParsed = new List<String[]>(AMOUNT);
            foreach (TextDecorationApplierUtil.SupportedTextDecoration supportedTextDecorationProperty in SUPPORTED_TEXT_DECORATION_PROPERTIES
                ) {
                String unParsedValue = node.GetStyles().Get(supportedTextDecorationProperty.GetPropertyName());
                String[] value = supportedTextDecorationProperty.GetDefaultValue();
                if (unParsedValue != null) {
                    value = iText.Commons.Utils.StringUtil.Split(unParsedValue, "\\s+");
                }
                System.Diagnostics.Debug.Assert(value != null);
                System.Diagnostics.Debug.Assert(value.Length > 0);
                currentValuesParsed.Add(value);
            }
            int maxValue = currentValuesParsed[0].Length;
            foreach (String[] strings in currentValuesParsed) {
                if (strings.Length > maxValue) {
                    maxValue = strings.Length;
                }
            }
            IList<StringBuilder> correctValues = new List<StringBuilder>(AMOUNT);
            for (int i = 0; i < AMOUNT; i++) {
                correctValues.Add(new StringBuilder());
            }
            for (int i = 0; i < maxValue; i++) {
                for (int indexProperty = 0; indexProperty < AMOUNT; indexProperty++) {
                    correctValues[indexProperty].Append(GetValueAtIndexOrLast(currentValuesParsed, i, indexProperty)).Append(' '
                        );
                }
            }
            for (int i = 0; i < AMOUNT; i++) {
                node.GetStyles().Put(SUPPORTED_TEXT_DECORATION_PROPERTIES[i].GetPropertyName(), correctValues[i].ToString(
                    ).Trim());
            }
        }

        private static String GetValueAtIndexOrLast(IList<String[]> currentValuesParsed, int i, int indexProperty) {
            return currentValuesParsed[indexProperty].Length - 1 > i ? currentValuesParsed[indexProperty][i] : currentValuesParsed
                [indexProperty][currentValuesParsed[indexProperty].Length - 1];
        }

        private sealed class SupportedTextDecoration {
            private readonly String propertyName;

            private readonly String[] defaultValue;

            public SupportedTextDecoration(String propertyName, String defaultValue) {
                this.propertyName = propertyName;
                this.defaultValue = new String[] { defaultValue };
            }

            public String GetPropertyName() {
                return propertyName;
            }

            public String[] GetDefaultValue() {
                return (String[])defaultValue.Clone();
            }
        }
    }
}
