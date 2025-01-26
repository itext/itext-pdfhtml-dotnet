/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HrTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/HrTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/HrTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HrHelloTest() {
            RunHrTest("00");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest01() {
            RunHrTest("01");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest02() {
            RunHrTest("02");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest03() {
            RunHrTest("03");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest04() {
            RunHrTest("04");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest05() {
            RunHrTest("05");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest06() {
            RunHrTest("06");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest07() {
            RunHrTest("07");
        }

        //It is expected that in the resulting PDF and firefox the border on the right is visible,
        //but not in Chrome. This is simply because the border in Chrome has the same color as the BG.
        [NUnit.Framework.Test]
        public virtual void HrTest08() {
            RunHrTest("08");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest09() {
            RunHrTest("09");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest10() {
            RunHrTest("10");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest11() {
            RunHrTest("11");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest12() {
            RunHrTest("12");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest13() {
            //TODO DEVSIX-4384: support box-shadow
            RunHrTest("13");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest14() {
            RunHrTest("14");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest15() {
            //TODO DEVSIX-8856: HR tag should have overflow: hidden by default
            RunHrTest("15");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest15WihtOverflow() {
            //TODO DEVSIX-8856: HR tag should have overflow: hidden by default
            RunHrTest("15WithOverflow");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest16() {
            //TODO DEVSIX-8856: HR tag should have overflow: hidden by default
            RunHrTest("16");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest17() {
            //TODO DEVSIX-8856: HR tag should have overflow: hidden by default
            RunHrTest("17");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest18() {
            //TODO DEVSIX-8856: HR tag should have overflow: hidden by default
            //TODO DEVSIX-4400: overflow: hidden is not working with border-radius
            RunHrTest("18");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest18WithOverflow() {
            //TODO DEVSIX-8856: HR tag should have overflow: hidden by default
            //TODO DEVSIX-4400: overflow: hidden is not working with border-radius
            RunHrTest("18WithOverflow");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest19() {
            RunHrTest("19");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest20() {
            RunHrTest("20");
        }

        [NUnit.Framework.Test]
        public virtual void HrTest21() {
            RunHrTest("21");
        }

        private void RunHrTest(String id) {
            ConvertToPdfAndCompare("hrTest" + id, SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
