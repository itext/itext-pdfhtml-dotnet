/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    [NUnit.Framework.TestFixtureSource("RotationRelatedPropertiesTestFixtureData")]
    public class FontStyleParameterizedTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontStyleParameterizedTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontStyleParameterizedTest/";

        private readonly String htmlName;

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        public FontStyleParameterizedTest(String htmlName) {
            this.htmlName = htmlName;
        }

        public FontStyleParameterizedTest(String[] array)
            : this(array[0]) {
        }

        public static IEnumerable<Object[]> RotationRelatedProperties() {
            return JavaUtil.ArraysAsList(new Object[][] { new Object[] { "fontWithSerifTest" }, new Object[] { "fontWithSansSerifTest"
                 }, new Object[] { "monospaceFontTest" }, new Object[] { "cursiveFontTest" }, new Object[] { "fantasyFontTest"
                 } });
        }

        public static ICollection<NUnit.Framework.TestFixtureData> RotationRelatedPropertiesTestFixtureData() {
            return RotationRelatedProperties().Select(array => new NUnit.Framework.TestFixtureData(array)).ToList();
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4Test() {
            String htmlPath = SOURCE_FOLDER + htmlName + ".html";
            String pdfPath = DESTINATION_FOLDER + htmlName + ".pdf";
            String cmpPdfPath = SOURCE_FOLDER + "cmp_" + htmlName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, DESTINATION_FOLDER, 
                "diff_"));
        }
    }
}