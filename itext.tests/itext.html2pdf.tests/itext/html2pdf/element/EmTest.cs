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
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class EmTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/EmTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/EmTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Em01Test() {
            RunTest("emTest01");
        }

        [NUnit.Framework.Test]
        public virtual void EmWithNotLocallyLoadedFontTest() {
            RunTest("emWithNotLocallyLoadedFont");
        }

        private void RunTest(String testName) {
            String htmlName = SOURCE_FOLDER + testName + ".html";
            String outFileName = DESTINATION_FOLDER + testName + ".pdf";
            String cmpFileName = SOURCE_FOLDER + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlName) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, DESTINATION_FOLDER
                ));
        }
    }
}
