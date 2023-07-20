/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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

namespace iText.Html2pdf.Css.Multicol {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BreakTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/multicol/BreakTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/multicol/BreakTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAutoTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAutoTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakBeforeAutoTest() {
            RunTest("pageBreakBeforeAutoTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAutoInsideColTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAutoInsideColTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAlwaysTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAlwaysTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakBeforeAlwaysTest() {
            RunTest("pageBreakBeforeAlwaysTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAvoidTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAvoidTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakBeforeAvoidTest() {
            RunTest("pageBreakBeforeAvoidTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAvoidInsideColTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAvoidInsideColTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAllTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAllTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAvoidPageTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAvoidPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakBeforeAvoidPageTest() {
            RunTest("pageBreakBeforeAvoidPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAvoidPageInsideColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAvoidPageInsideColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakBeforeAvoidInsideColumnTest() {
            RunTest("pageBreakBeforeAvoidInsideColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforePageTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforePageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforePageInsideColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforePageInsideColumnTest");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("TODO DEVSIX-7552 Column-count: support break-inside, break-after and break-before properties"
            )]
        public virtual void ConvertPageBreakBeforePageInsideColumnTest() {
            RunTest("pageBreakBeforePageInsideColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeLeftTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeLeftTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakBeforeLeftTest() {
            RunTest("pageBreakBeforeLeftTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeRightTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeRightTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakBeforeRightTest() {
            RunTest("pageBreakBeforeRightTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeAvoidColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeAvoidColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakBeforeColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakBeforeColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAutoTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAutoTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakAfterAutoTest() {
            RunTest("pageBreakAfterAutoTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAutoInsideColTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAutoInsideColTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAlwaysTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAlwaysTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakAfterAlwaysTest() {
            RunTest("pageBreakAfterAlwaysTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAvoidTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAvoidTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakAfterAvoidTest() {
            // TODO DEVSIX-7552 Column-count: support break-inside, break-after and break-before properties
            RunTest("pageBreakAfterAvoidTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAvoidInsideColTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAvoidInsideColTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAllTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAllTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAvoidPageTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAvoidPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakAfterAvoidPageTest() {
            RunTest("pageBreakAfterAvoidPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAvoidPageInsideColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAvoidPageInsideColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakAfterAvoidInsideColumnTest() {
            RunTest("pageBreakAfterAvoidInsideColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterPageTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterPageInsideColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterPageInsideColumnTest");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("TODO DEVSIX-7552 Column-count: support break-inside, break-after and break-before properties"
            )]
        public virtual void ConvertPageBreakAfterPageInsideColumnTest() {
            RunTest("pageBreakAfterPageInsideColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterLeftTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterLeftTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakAfterLeftTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("pageBreakAfterLeftTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterRightTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterRightTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakAfterRightTest() {
            RunTest("pageBreakAfterRightTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterAvoidColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterAvoidColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakAfterColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakAfterColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakInsideAutoTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakInsideAutoTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakInsideAutoTest() {
            RunTest("pageBreakInsideAutoTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakInsideAvoidTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakInsideAvoidTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPageBreakInsideAvoidTest() {
            // TODO DEVSIX-7552 Column-count: support break-inside, break-after and break-before properties
            RunTest("pageBreakInsideAvoidTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakInsideAvoidInsideColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakInsideAvoidInsideColumnTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakInsideAvoidPageTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakInsideAvoidPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBreakInsideAvoidColumnTest() {
            // TODO DEVSIX-3819 support break-after, break-before, break-inside CSS properties
            RunTest("breakInsideAvoidColumnTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
