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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace iText.Html2pdf.Attach.Impl.Tags {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ATagWorkerTest : ExtendedHtmlConversionITextTest {
        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/ATagWorkerTest/";

        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/ATagWorkerTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ATagHrefRelativeLinkTest() {
            ConverterProperties properties = new ConverterProperties();
            properties.SetBaseUri("https://not_existing_url.com/");
            PdfWriter writer = new PdfWriter(DESTINATION_FOLDER + "aTagHrefRelativeLink.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            HtmlConverter.ConvertToPdf(new FileStream(SOURCE_FOLDER + "aTagHrefRelativeLink.html", FileMode.Open, FileAccess.Read
                ), pdf, properties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "aTagHrefRelativeLink.pdf"
                , SOURCE_FOLDER + "cmp_aTagHrefRelativeLink.pdf", DESTINATION_FOLDER, "diff12_"));
        }

        [NUnit.Framework.Test]
        public virtual void ATagHrefAbsoluteLinkTest() {
            ConverterProperties properties = new ConverterProperties();
            properties.SetBaseUri("https://not_existing_url.com/");
            PdfWriter writer = new PdfWriter(DESTINATION_FOLDER + "aTagHrefAbsoluteLink.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            HtmlConverter.ConvertToPdf(new FileStream(SOURCE_FOLDER + "aTagHrefAbsoluteLink.html", FileMode.Open, FileAccess.Read
                ), pdf, properties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "aTagHrefAbsoluteLink.pdf"
                , SOURCE_FOLDER + "cmp_aTagHrefAbsoluteLink.pdf", DESTINATION_FOLDER, "diff12_"));
        }

        [NUnit.Framework.Test]
        public virtual void ATagHrefLocalFileLinkTest() {
            ConverterProperties properties = new ConverterProperties();
            PdfWriter writer = new PdfWriter(DESTINATION_FOLDER + "aTagHrefLocalFileLink.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            HtmlConverter.ConvertToPdf(new FileStream(SOURCE_FOLDER + "aTagHrefLocalFileLink.html", FileMode.Open, FileAccess.Read
                ), pdf, properties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "aTagHrefLocalFileLink.pdf"
                , SOURCE_FOLDER + "cmp_aTagHrefLocalFileLink.pdf", DESTINATION_FOLDER, "diff12_"));
        }
    }
}
