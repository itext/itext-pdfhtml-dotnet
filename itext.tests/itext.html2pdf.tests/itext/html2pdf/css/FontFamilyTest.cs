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
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontFamilyTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontFamilyTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontFamilyTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HugeFontFamilyForDosAttackTest() {
            String htmlPath = SOURCE_FOLDER + "hugeFontFamilyForDosAttackTest.html";
            String pdfPath = DESTINATION_FOLDER + "hugeFontFamilyForDosAttackTest.pdf";
            String cmpPdfPath = SOURCE_FOLDER + "cmp_hugeFontFamilyForDosAttackTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, DESTINATION_FOLDER, 
                "diff_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_FONT, Count = 1)]
        public virtual void SelectFontFromTTCTest() {
            String htmlPath = SOURCE_FOLDER + "selectFontInGroup.html";
            String pdfPath = DESTINATION_FOLDER + "selectFontInGroup.pdf";
            String cmpPdfPath = SOURCE_FOLDER + "cmp_selectFontInGroup.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath));
            //TODO DEVSIX-1104: Change cmp file after supporting ttc#id when selecting font from ttc
            //Currently it will look for a font file where #{id} is part of the font path.
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, DESTINATION_FOLDER, 
                "diff_"));
        }
    }
}
