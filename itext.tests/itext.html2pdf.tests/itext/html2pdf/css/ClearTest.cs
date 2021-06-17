/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
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
