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

namespace iText.Html2pdf.Css.Selectors {
    [NUnit.Framework.Category("IntegrationTest")]
    public class IsSelectorTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/selectors/IsSelectorTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/selectors/IsSelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsClassOverridesListTest() {
            ConvertToPdfAndCompare("isClassOverridesList", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsDeepNestedTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("isDeepNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsDeepNested2Test() {
            ConvertToPdfAndCompare("isDeepNested2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsFirstVsClassTest() {
            ConvertToPdfAndCompare("isFirstVsClass", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsFlexDirTest() {
            ConvertToPdfAndCompare("isFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsFlexDirReadDirTest() {
            ConvertToPdfAndCompare("isFlexDirReadDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsFlexWrapTest() {
            ConvertToPdfAndCompare("isFlexWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsFlexWrapAlignTest() {
            ConvertToPdfAndCompare("isFlexWrapAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsGapJustifyContentTest() {
            ConvertToPdfAndCompare("isGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsGroupedSelectorsTest() {
            ConvertToPdfAndCompare("isGroupedSelectors", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsNestedTest() {
            ConvertToPdfAndCompare("isNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsNestedListsTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("isNestedLists", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsNestedListsFlexTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("isNestedListsFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsNthChildTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("isNthChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsReadDirFlexTest() {
            ConvertToPdfAndCompare("isReadDirFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsSelectoreBasicTest() {
            ConvertToPdfAndCompare("isSelectoreBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsSpecificOverrideTest() {
            ConvertToPdfAndCompare("isSpecificOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsWinsFromClassTest() {
            ConvertToPdfAndCompare("isWinsFromClass", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsWithIdTest() {
            ConvertToPdfAndCompare("isWithId", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsWithJustifyAndAlignContentTest() {
            ConvertToPdfAndCompare("isWithJustifyAndAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
