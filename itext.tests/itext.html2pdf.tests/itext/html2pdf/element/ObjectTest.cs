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
        public virtual void RelativeSizeSvg3_2_2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_2_2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_4", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_5Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_5", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_6Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_6", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_7Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_7", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_8Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_8", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_9Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_9", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_10Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_11Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_12Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_13Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_14Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_15Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_2Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_3Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_4Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_4", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_5Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_5", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_6Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_6", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_7Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_7", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_8Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_8", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_9Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_9", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_10Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_11Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_12Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_13Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_14Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_15Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_2Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_3Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_4Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_4", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_5Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_5", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_6Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_6", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_7Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_7", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_8Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_8", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_9Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_9", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_10Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_11Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_12Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_13Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_14Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_15Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_15", sourceFolder, destinationFolder);
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

        //TODO DEVSIX-1316 fix incorrect min max width
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock2() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock2", sourceFolder, destinationFolder);
        }

        //TODO DEVSIX-1316 fix incorrect min max width
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock2_2() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock2_2", sourceFolder, destinationFolder);
        }

        //TODO DEVSIX-1316 fix incorrect min max width
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock3() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock4() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock4", sourceFolder, destinationFolder);
        }

        //TODO DEVSIX-1316 fix incorrect min max width
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInlineBlock5() {
            ConvertToPdfAndCompare("relativeSizeSvgInlineBlock5", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable1() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable1", sourceFolder, destinationFolder);
        }

        //TODO DEVSIX-7003, DEVSIX-1316 fix image with relative size in the table
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable2() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable2", sourceFolder, destinationFolder);
        }

        //TODO DEVSIX-7003, DEVSIX-1316 fix image with relative size in the table
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable3() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable3", sourceFolder, destinationFolder);
        }

        //TODO DEVSIX-7003, DEVSIX-1316 fix image with relative size in the table
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInTable3_2() {
            ConvertToPdfAndCompare("relativeSizeSvgInTable3_2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInFixedObject() {
            ConvertToPdfAndCompare("relativeSizeSvgInFixedObject", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInFixedObject2() {
            ConvertToPdfAndCompare("relativeSizeSvgInFixedObject2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInRelativeObject() {
            ConvertToPdfAndCompare("relativeSizeSvgInRelativeObject", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void GiantSvgInRelativeObject() {
            ConvertToPdfAndCompare("giantSvgInRelativeObject", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void InvalidSizeSvgInRelativeObject() {
            ConvertToPdfAndCompare("invalidSizeSvgInRelativeObject", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FixedSizeSvgInRelativeObject() {
            ConvertToPdfAndCompare("fixedSizeSvgInRelativeObject", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgFixedInlineBlock() {
            ConvertToPdfAndCompare("relativeSizeSvgFixedInlineBlock", sourceFolder, destinationFolder);
        }
    }
}
