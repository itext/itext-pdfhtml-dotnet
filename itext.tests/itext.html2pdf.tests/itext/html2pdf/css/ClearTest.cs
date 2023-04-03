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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ClearTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/ClearTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/ClearTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear02Test() {
            ConvertToPdfAndCompare("clear02Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear03Test() {
            ConvertToPdfAndCompare("clear03Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear04Test() {
            ConvertToPdfAndCompare("clear04Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear06Test() {
            ConvertToPdfAndCompare("clear06Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear07Test() {
            ConvertToPdfAndCompare("clear07Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear08Test() {
            // TODO: DEVSIX-1269, DEVSIX-5474 update cmp file after fixing issues
            ConvertToPdfAndCompare("clear08Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear09Test() {
            // TODO: DEVSIX-5474 update cmp file after fixing
            ConvertToPdfAndCompare("clear09Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear10Test() {
            // TODO: DEVSIX-5474 update cmp file after fixing
            ConvertToPdfAndCompare("clear10Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Clear11Test() {
            ConvertToPdfAndCompare("clear11Test", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImageFloatParagraphClearTest() {
            ConvertToPdfAndCompare("imageFloatParagraphClear", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ClearInTableWithImageTest() {
            ConvertToPdfAndCompare("clearInTableWithImage", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImgFloatAmongParaWithClearPropTest() {
            ConvertToPdfAndCompare("imgFloatAmongParaWithClearProp", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImgFloatAmongParaWithSpanTest() {
            ConvertToPdfAndCompare("imgFloatAmongParaWithSpan", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ParaFloatLeftImgClearLeftTest() {
            ConvertToPdfAndCompare("paraFloatLeftImgClearLeft", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ParaFloatImgClearAndDisplayBlockTest() {
            ConvertToPdfAndCompare("paraFloatImgClearAndDisplayBlock", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ParaFloatImgWideBorderClearAndDisplayBlockTest() {
            ConvertToPdfAndCompare("paraFloatImgWideBorderClearAndDisplayBlock", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImgFloatWideBorderAmongParaWithClearTest() {
            ConvertToPdfAndCompare("imgFloatWideBorderAmongParaWithClear", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImgWideBorderFloatAmongParaWithSpanTest() {
            ConvertToPdfAndCompare("imgWideBorderFloatAmongParaWithSpan", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImgWideBorderClearAndDisplayBlockParaFloatTest() {
            ConvertToPdfAndCompare("imgWideBorderClearAndDisplayBlockParaFloat", sourceFolder, destinationFolder);
        }
    }
}
