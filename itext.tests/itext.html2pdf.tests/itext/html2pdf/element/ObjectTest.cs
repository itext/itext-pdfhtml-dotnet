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
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ObjectTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ObjectTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ObjectTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Base64svgTest() {
            ConvertToPdfAndCompare("objectTag_base64svg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
            , Count = 1)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void HtmlObjectIncorrectBase64Test() {
            ConvertToPdfAndCompare("objectTag_incorrectBase64svg", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        //TODO: update after DEVSIX-1346
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_IT_S_TEXT_CONTENT, Count = 1)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void HtmlObjectAltTextTest() {
            ConvertToPdfAndCompare("objectTag_altText", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void HtmlObjectNestedObjectTest() {
            ConvertToPdfAndCompare("objectTag_nestedTag", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_4", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_5Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_5", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg4", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg4_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg4_3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg5_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg5_3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock1() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock1", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock2() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock2_2() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock2_2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock3() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock4() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock4", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock5() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock5", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable1() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable1", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable2() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable3() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable3_2() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable3_2", sourceFolder, destinationFolder);
        }
    }
}
