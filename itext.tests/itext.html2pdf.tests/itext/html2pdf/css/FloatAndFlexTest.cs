using System;
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Css {
    public class FloatAndFlexTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FloatAndFlexTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FloatAndFlexTest/";

        [NUnit.Framework.Test]
        public virtual void FloatAtFlexContainerTest() {
            //TODO DEVSIX-5087 remove this test when working on the ticket
            String name = "floatAtFlexContainer";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer.GetRenderer() is FlexContainerRenderer);
            NUnit.Framework.Assert.IsFalse(flexContainer.HasProperty(Property.FLOAT));
            NUnit.Framework.Assert.IsFalse(flexContainer.HasProperty(Property.CLEAR));
        }

        [NUnit.Framework.Test]
        public virtual void ClearAtFlexItemTest() {
            ConvertToPdfAndCompare("clearAtFlexItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexContainerHeightTest() {
            ConvertToPdfAndCompare("flexContainerHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FloatAtFlexItemTest() {
            ConvertToPdfAndCompare("floatAtFlexItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FloatAtFlexItemNestedTest() {
            ConvertToPdfAndCompare("floatAtFlexItemNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
