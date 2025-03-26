using System;
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Resolve {
    [NUnit.Framework.Category("IntegrationTest")]
    public class CssVariableResolverTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css" + "/CssVariableResolverTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css" + "/CssVariableResolverTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VariableScopeTest() {
            ConvertToPdfAndCompare("variableScope", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ReversedDeclarationVariableScopeTest() {
            ConvertToPdfAndCompare("reversedDeclarationVariableScope", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DefaultValueTest() {
            ConvertToPdfAndCompare("defaultValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IncorrectDefaultValueTest() {
            ConvertToPdfAndCompare("incorrectDefaultValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleRootSelectorTest() {
            ConvertToPdfAndCompare("simpleRootSelector", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VarInSameContextTest() {
            ConvertToPdfAndCompare("varInSameContext", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        [NUnit.Framework.Test]
        public virtual void VarOverridingTest() {
            ConvertToPdfAndCompare("varOverriding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        [NUnit.Framework.Test]
        public virtual void VarInheritanceTest() {
            ConvertToPdfAndCompare("varInheritance", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        [NUnit.Framework.Test]
        public virtual void RootSelectorTest() {
            ConvertToPdfAndCompare("rootSelector", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundTest() {
            ConvertToPdfAndCompare("background", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VarAsShorthandTest() {
            ConvertToPdfAndCompare("varAsShorthand", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VarInShorthandTest() {
            ConvertToPdfAndCompare("varInShorthand", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VarsInShorthandTest() {
            ConvertToPdfAndCompare("varsInShorthand", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        [NUnit.Framework.Test]
        public virtual void DefaultValuesWithValidationTest() {
            ConvertToPdfAndCompare("defaultValuesWithValidation", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        [NUnit.Framework.Test]
        public virtual void ExtraSpacesInVarTest() {
            ConvertToPdfAndCompare("extraSpacesInVar", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VarInStyleAttributeTest() {
            ConvertToPdfAndCompare("varInStyleAttribute", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ComplexTest() {
            ConvertToPdfAndCompare("complex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
