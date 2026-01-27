/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HasPseudoClassTest : ExtendedHtmlConversionITextTest {
        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/HasPseudoClassTest/";

        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/HasPseudoClassTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasBasicSelectionTest() {
            ConvertToPdfAndCompare("hasBasicSelectionTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasChildSelectorTest() {
            ConvertToPdfAndCompare("hasChildSelectorTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasAdjacentSiblingSelectorTest() {
            ConvertToPdfAndCompare("hasAdjacentSiblingSelectorTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasGeneralSiblingSelectorTest() {
            ConvertToPdfAndCompare("hasGeneralSiblingSelectorTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasNegationTest() {
            ConvertToPdfAndCompare("hasNegationTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //Nesting 'has' is not allowed in CSS
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count
             = 3)]
        [NUnit.Framework.Test]
        public virtual void HasNestedHasTest() {
            ConvertToPdfAndCompare("hasNestedHasTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasWithPseudoElementsInteractionTest() {
            ConvertToPdfAndCompare("hasWithPseudoElementsInteractionTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasWithDisplayVisibilityTest() {
            ConvertToPdfAndCompare("hasWithDisplayVisibilityTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasWithBoxModelTest() {
            ConvertToPdfAndCompare("hasWithBoxModelTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HasWithPositioningTest() {
            ConvertToPdfAndCompare("hasWithPositioningTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-4258: table body styling is not supported
        [NUnit.Framework.Test]
        public virtual void HasWithTableStructuresTest() {
            ConvertToPdfAndCompare("hasWithTableStructuresTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count
             = 2)]
        //TODO DEVSIX-1440: support :invalid and :checked pseudo-classes
        [NUnit.Framework.Test]
        public virtual void HasWithFormControlsTest() {
            ConvertToPdfAndCompare("hasWithFormControlsTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
