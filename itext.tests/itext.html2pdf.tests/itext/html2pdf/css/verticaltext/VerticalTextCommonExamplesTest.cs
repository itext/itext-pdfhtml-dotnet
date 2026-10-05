/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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

namespace iText.Html2pdf.Css.Verticaltext {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextCommonExamplesTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/verticaltext/VerticalTextCommonExamplesTest/";

        public static readonly String DEST_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/test/itext/html2pdf/css/verticaltext/VerticalTextCommonExamplesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DEST_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 32)]
        public virtual void VerticalJapaneseNovelExcerptWithRubyTest() {
            ConvertToPdfAndCompare("verticalJapaneseNovelExcerptWithRuby", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 26)]
        public virtual void VerticalJapaneseNovelWagahaiTest() {
            ConvertToPdfAndCompare("verticalJapaneseNovelWagahai", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 24)]
        public virtual void VerticalJapaneseNovelInlineFeaturesTest() {
            ConvertToPdfAndCompare("verticalJapaneseNovelInlineFeatures", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseHaikuTanzakuTest() {
            ConvertToPdfAndCompare("verticalJapaneseHaikuTanzaku", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalChineseTangPoemTraditionalTest() {
            ConvertToPdfAndCompare("verticalChineseTangPoemTraditional", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalChineseSpringFestivalCoupletsTest() {
            ConvertToPdfAndCompare("verticalChineseSpringFestivalCouplets", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseCertificateOfAppreciationTest() {
            ConvertToPdfAndCompare("verticalJapaneseCertificateOfAppreciation", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 4)]
        public virtual void VerticalJapaneseBusinessCardTest() {
            ConvertToPdfAndCompare("verticalJapaneseBusinessCard", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalKoreanSijoPoemsTest() {
            ConvertToPdfAndCompare("verticalKoreanSijoPoems", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 3)]
        public virtual void VerticalJapaneseRestaurantMenuTest() {
            ConvertToPdfAndCompare("verticalJapaneseRestaurantMenu", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 2)]
        public virtual void VerticalJapaneseNewspaperMultiColumnTest() {
            ConvertToPdfAndCompare("verticalJapaneseNewspaperMultiColumn", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 108)]
        public virtual void VerticalJapaneseAozoraKumoNoItoTest() {
            ConvertToPdfAndCompare("verticalJapaneseAozoraKumoNoIto", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 4)]
        public virtual void VerticalJapaneseRecipeOrderedListTest() {
            ConvertToPdfAndCompare("verticalJapaneseRecipeOrderedList", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 4)]
        public virtual void VerticalJapaneseSchoolNewsletterTableTest() {
            ConvertToPdfAndCompare("verticalJapaneseSchoolNewsletterTable", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 3)]
        public virtual void VerticalJapaneseBlogPostWithFigureTest() {
            ConvertToPdfAndCompare("verticalJapaneseBlogPostWithFigure", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        //Mixed baseline / upright
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 2)]
        public virtual void VerticalChineseNewsWithLatinInitialismsTest() {
            ConvertToPdfAndCompare("verticalChineseNewsWithLatinInitialisms", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, Count = 2)]
        public virtual void VerticalJapaneseMultiPageDocumentTest() {
            ConvertToPdfAndCompare("verticalJapaneseMultiPageDocument", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseTcyLatinAbbreviationAndDigitsTest() {
            ConvertToPdfAndCompare("verticalJapaneseTcyLatinAbbreviationAndDigits", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void VerticalJapaneseTcyDigitsCountVariantsTest() {
            ConvertToPdfAndCompare("verticalJapaneseTcyDigitsCountVariants", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseTcyLongRunCompressionTest() {
            ConvertToPdfAndCompare("verticalJapaneseTcyLongRunCompression", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseTcyGeneratedContentFootnotesTest() {
            ConvertToPdfAndCompare("verticalJapaneseTcyGeneratedContentFootnotes", SOURCE_FOLDER, DEST_FOLDER, false);
        }
    }
}
