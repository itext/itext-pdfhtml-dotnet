/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
Authors: iText Software.

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

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

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

        [NUnit.Framework.Test]
        public virtual void FloatsPositioningInsideAndOutsideFlexTest() {
            // TODO DEVSIX-5135 floating elements inside flex container are incorrectly positioned
            ConvertToPdfAndCompare("floatsPositioningInsideAndOutsideFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
