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
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexAlignSelfTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/FlexAlignSelfTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/FlexAlignSelfTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("startFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartFlexDirRowReverseFlexWrapTest() {
            ConvertToPdfAndCompare("startFlexDirRowReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartFlexDirColumnFlexWrapTest() {
            ConvertToPdfAndCompare("startFlexDirColumnFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartFlexDirColumnReverseFlexWrapTest() {
            ConvertToPdfAndCompare("startFlexDirColumnReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 84, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void StartFlexDirColumnFlexWrapDirTest() {
            // TODO DEVSIX-9436 Flex: alignment doesn't work correctly with direction: rtl
            ConvertToPdfAndCompare("startFlexDirColumnFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EndFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("endFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EndFlexDirRowReverseFlexWrapTest() {
            ConvertToPdfAndCompare("endFlexDirRowReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EndFlexDirColumnFlexWrapTest() {
            ConvertToPdfAndCompare("endFlexDirColumnFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 84, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void EndFlexDirColumnFlexWrapDirTest() {
            ConvertToPdfAndCompare("endFlexDirColumnFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EndFlexDirColumnReverseFlexWrapTest() {
            ConvertToPdfAndCompare("endFlexDirColumnReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CenterFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("centerFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CenterFlexDirRowReverseFlexWrapTest() {
            ConvertToPdfAndCompare("centerFlexDirRowReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CenterFlexDirColumnFlexWrapTest() {
            ConvertToPdfAndCompare("centerFlexDirColumnFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CenterFlexDirColumnReverseFlexWrapTest() {
            ConvertToPdfAndCompare("centerFlexDirColumnReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexStartFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("flexStartFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexStartFlexDirRowReverseFlexWrapTest() {
            ConvertToPdfAndCompare("flexStartFlexDirRowReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexStartFlexDirColumnFlexWrapTest() {
            ConvertToPdfAndCompare("flexStartFlexDirColumnFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexStartFlexDirColumnReverseFlexWrapTest() {
            ConvertToPdfAndCompare("flexStartFlexDirColumnReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 84, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void FlexStartFlexDirColumnFlexWrapDirTest() {
            // TODO DEVSIX-9436 Flex: alignment doesn't work correctly with direction: rtl
            ConvertToPdfAndCompare("flexStartFlexDirColumnFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("flexEndFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndFlexDirRowReverseFlexWrapTest() {
            ConvertToPdfAndCompare("flexEndFlexDirRowReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndFlexDirColumnFlexWrapTest() {
            ConvertToPdfAndCompare("flexEndFlexDirColumnFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndFlexDirColumnReverseFlexWrapTest() {
            ConvertToPdfAndCompare("flexEndFlexDirColumnReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 84, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void FlexEndFlexDirColumnReverseFlexWrapDirTest() {
            ConvertToPdfAndCompare("flexEndFlexDirColumnReverseFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // TODO DEVSIX-5167 Support baseline value for align-items and align-self
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void BaselineFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("baselineFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // TODO DEVSIX-5167 Support baseline value for align-items and align-self
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void BaselineFlexDirRowReverseFlexWrapTest() {
            ConvertToPdfAndCompare("baselineFlexDirRowReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // TODO DEVSIX-5167 Support baseline value for align-items and align-self
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void BaselineFlexDirColumnFlexWrapTest() {
            ConvertToPdfAndCompare("baselineFlexDirColumnFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // TODO DEVSIX-5167 Support baseline value for align-items and align-self
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void BaselineFlexDirColumnReverseFlexWrapTest() {
            ConvertToPdfAndCompare("baselineFlexDirColumnReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AutoFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("autoFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NormalFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("normalFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 84, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void SelfStartFlexDirColumnFlexWrapDirTest() {
            // TODO DEVSIX-9436 Flex: alignment doesn't work correctly with direction: rtl
            ConvertToPdfAndCompare("selfStartFlexDirColumnFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 84, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void SelfStartFlexDirColumnReverseFlexWrapDirTest() {
            // TODO DEVSIX-9436 Flex: alignment doesn't work correctly with direction: rtl
            ConvertToPdfAndCompare("selfStartFlexDirColumnReverseFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 108, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void SelfStartFlexDirRowFlexWrapDirTest() {
            // TODO DEVSIX-9436 Flex: alignment doesn't work correctly with direction: rtl
            ConvertToPdfAndCompare("selfStartFlexDirRowFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 84, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void SelfEndFlexDirColumnFlexWrapDirTest() {
            // TODO DEVSIX-9436 Flex: alignment doesn't work correctly with direction: rtl
            ConvertToPdfAndCompare("selfEndFlexDirColumnFlexWrapDirTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchFlexDirRowFlexWrapTest() {
            ConvertToPdfAndCompare("stretchFlexDirRowFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchFlexDirRowReverseFlexWrapTest() {
            ConvertToPdfAndCompare("stretchFlexDirRowReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchFlexDirColumnFlexWrapTest() {
            ConvertToPdfAndCompare("stretchFlexDirColumnFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchFlexDirColumnReverseFlexWrapTest() {
            ConvertToPdfAndCompare("stretchFlexDirColumnReverseFlexWrapTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
