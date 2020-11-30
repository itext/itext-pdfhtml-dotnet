/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class TargetCounterTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/TargetCounterTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/TargetCounterTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageUrlNameTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageUrlName");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageUrlIdTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageUrlId");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.EXCEEDED_THE_MAXIMUM_NUMBER_OF_RELAYOUTS)]
        public virtual void TargetCounterManyRelayoutsTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterManyRelayouts");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageBigElementTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageBigElement");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageAllTagsTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageAllTags");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CANNOT_RESOLVE_TARGET_COUNTER_VALUE, Count = 2)]
        public virtual void TargetCounterNotExistingTargetTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterNotExistingTarget");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void PageTargetCounterTestWithLogMessageTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("pageTargetCounterTestWithLogMessage");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void NonPageTargetCounterTestWithLogMessageTest() {
            // There should be only one log message here, but we have two because we resolve css styles twice.
            ConvertToPdfWithTargetCounterEnabledAndCompare("nonPageTargetCounterTestWithLogMessage");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterSeveralCountersTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterSeveralCounters");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterIDNotExistTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterIDNotExist");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterNotCounterElementTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterNotCounterElement");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterNestedCountersTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterNestedCounters");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterUnusualStylesTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterUnusualStyles");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCountersNestedCountersTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCountersNestedCounters");
        }

        private void ConvertToPdfWithTargetCounterEnabledAndCompare(String name) {
            String sourceHtml = sourceFolder + name + ".html";
            String cmpPdf = sourceFolder + "cmp_" + name + ".pdf";
            String destinationPdf = destinationFolder + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(sourceFolder).SetTargetCounterEnabled
                (true);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, converterProperties);
            }
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceHtml) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, destinationFolder
                , "diff_" + name + "_"));
        }
    }
}
