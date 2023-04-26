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
    public class CssTransformTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssTransformTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssTransformTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformMatrixTest() {
            ConvertToPdfAndCompare("cssTransformMatrix", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformRotateTest() {
            ConvertToPdfAndCompare("cssTransformRotate", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateTest() {
            ConvertToPdfAndCompare("cssTransformTranslate", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateXTest() {
            ConvertToPdfAndCompare("cssTransformTranslateX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateXHugeValueTest() {
            ConvertToPdfAndCompare("cssTransformTranslateXHugeValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateYTest() {
            ConvertToPdfAndCompare("cssTransformTranslateY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateYHugeValueTest() {
            ConvertToPdfAndCompare("cssTransformTranslateYHugeValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformSkewTest() {
            ConvertToPdfAndCompare("cssTransformSkew", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformSkewXTest() {
            ConvertToPdfAndCompare("cssTransformSkewX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformSkewYTest() {
            ConvertToPdfAndCompare("cssTransformSkewY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformScaleTest() {
            ConvertToPdfAndCompare("cssTransformScale", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformScaleXTest() {
            ConvertToPdfAndCompare("cssTransformScaleX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformScaleYTest() {
            ConvertToPdfAndCompare("cssTransformScaleY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformCombinationTest() {
            ConvertToPdfAndCompare("cssTransformCombination", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformCellTest() {
            // TODO DEVSIX-2862 layout: improve block's TRANSFORM processing
            ConvertToPdfAndCompare("cssTransformCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
