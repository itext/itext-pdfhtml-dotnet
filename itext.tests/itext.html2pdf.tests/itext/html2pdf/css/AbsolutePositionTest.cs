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
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class AbsolutePositionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/AbsolutePositionTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/AbsolutePositionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition01Test() {
            ConvertToPdfAndCompare("absolutePositionTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1616: Absolute position for elements that break across pages is not supported"
            )]
        public virtual void AbsolutePosition02Test() {
            ConvertToPdfAndCompare("absolutePositionTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition03Test() {
            ConvertToPdfAndCompare("absolutePositionTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition04Test() {
            ConvertToPdfAndCompare("absolutePositionTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition05Test() {
            ConvertToPdfAndCompare("absolutePositionTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition06Test() {
            ConvertToPdfAndCompare("absolutePositionTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition07Test() {
            ConvertToPdfAndCompare("absolutePositionTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition08Test() {
            ConvertToPdfAndCompare("absolutePositionTest08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED, Count = 1)]
        public virtual void AbsolutePosition09Test() {
            ConvertToPdfAndCompare("absolutePositionTest09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition10Test() {
            ConvertToPdfAndCompare("absolutePositionTest10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition11Test() {
            ConvertToPdfAndCompare("absolutePositionTest11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition12Test() {
            ConvertToPdfAndCompare("absolutePositionTest12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition13Test() {
            ConvertToPdfAndCompare("absolutePositionTest13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition14Test() {
            ConvertToPdfAndCompare("absolutePositionTest14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition15Test() {
            ConvertToPdfAndCompare("absolutePositionTest15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest16() {
            ConvertToPdfAndCompare("absolutePositionTest16", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest17() {
            ConvertToPdfAndCompare("absolutePositionTest17", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Ignore("DEVSIX-1818")]
        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest18() {
            ConvertToPdfAndCompare("absolutePositionTest18", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosNoTopBottomTest01() {
            // TODO DEVSIX-1950
            ConvertToPdfAndCompare("absPosNoTopBottomTest01", sourceFolder, destinationFolder);
        }
    }
}
