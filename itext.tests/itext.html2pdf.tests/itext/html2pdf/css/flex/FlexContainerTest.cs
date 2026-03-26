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
using iText.Layout.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexContainerTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/FlexContainerTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/FlexContainerTest/";

        //TODO DEVSIX-9519: Update cmp / tests for ul ol li button p tags
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ItemsHigherThanContainerTest() {
            ConvertToPdfAndCompare("itemsHigherThanContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ABasicTest() {
            ConvertToPdfAndCompare("aBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ABasic2ColTest() {
            ConvertToPdfAndCompare("aBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ALongTest() {
            ConvertToPdfAndCompare("aLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ANestedTest() {
            ConvertToPdfAndCompare("aNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AUlNestedTest() {
            ConvertToPdfAndCompare("aUlNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AWideTest() {
            ConvertToPdfAndCompare("aWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleBasicTest() {
            ConvertToPdfAndCompare("articleBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleBasic2ColTest() {
            ConvertToPdfAndCompare("articleBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleLongTest() {
            ConvertToPdfAndCompare("articleLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleNestedTest() {
            ConvertToPdfAndCompare("articleNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleUlNestedTest() {
            ConvertToPdfAndCompare("articleUlNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleWideTest() {
            ConvertToPdfAndCompare("articleWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonBasicTest() {
            ConvertToPdfAndCompare("buttonBasic", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonBasic2ColTest() {
            ConvertToPdfAndCompare("buttonBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void ButtonLongTest() {
            ConvertToPdfAndCompare("buttonLong", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonNestedTest() {
            ConvertToPdfAndCompare("buttonNested", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonWideTest() {
            ConvertToPdfAndCompare("buttonWide", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void FigureBasicTest() {
            ConvertToPdfAndCompare("figureBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FigureBasic2ColTest() {
            ConvertToPdfAndCompare("figureBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FigureLongTest() {
            ConvertToPdfAndCompare("figureLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FigureNestedTest() {
            ConvertToPdfAndCompare("figureNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FigureWideTest() {
            ConvertToPdfAndCompare("figureWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FooterBasicTest() {
            ConvertToPdfAndCompare("footerBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FooterBasic2ColTest() {
            ConvertToPdfAndCompare("footerBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FooterLongTest() {
            ConvertToPdfAndCompare("footerLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FooterNestedTest() {
            ConvertToPdfAndCompare("footerNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FooterWideTest() {
            ConvertToPdfAndCompare("footerWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FormBasicTest() {
            ConvertToPdfAndCompare("formBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FormBasic2ColTest() {
            ConvertToPdfAndCompare("formBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FormLongTest() {
            ConvertToPdfAndCompare("formLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FormNestedTest() {
            ConvertToPdfAndCompare("formNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FormWideTest() {
            ConvertToPdfAndCompare("formWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderBasicTest() {
            ConvertToPdfAndCompare("headerBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderBasic2ColTest() {
            ConvertToPdfAndCompare("headerBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderLongTest() {
            ConvertToPdfAndCompare("headerLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderNestedTest() {
            ConvertToPdfAndCompare("headerNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderWideTest() {
            ConvertToPdfAndCompare("headerWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LabelBasicTest() {
            ConvertToPdfAndCompare("labelBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LabelBasic2ColTest() {
            ConvertToPdfAndCompare("labelBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LabelNestedTest() {
            ConvertToPdfAndCompare("labelNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LabelWideTest() {
            ConvertToPdfAndCompare("labelWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LabelLongTest() {
            ConvertToPdfAndCompare("labelLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LiBasicTest() {
            ConvertToPdfAndCompare("liBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LiBasic2ColTest() {
            ConvertToPdfAndCompare("liBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OlBasicTest() {
            ConvertToPdfAndCompare("olBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OlBasic2ColTest() {
            ConvertToPdfAndCompare("olBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OlLongTest() {
            ConvertToPdfAndCompare("olLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OlNestedTest() {
            ConvertToPdfAndCompare("olNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OlWideTest() {
            ConvertToPdfAndCompare("olWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PBasicTest() {
            ConvertToPdfAndCompare("pBasic", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        public virtual void PLongTest() {
            ConvertToPdfAndCompare("pLong", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void PNestedTest() {
            ConvertToPdfAndCompare("pNested", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void PWideTest() {
            ConvertToPdfAndCompare("pWide", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void PBasic2ColTest() {
            ConvertToPdfAndCompare("pBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void SectionBasicTest() {
            ConvertToPdfAndCompare("sectionBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SectionBasic2ColTest() {
            ConvertToPdfAndCompare("sectionBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SectionLongTest() {
            ConvertToPdfAndCompare("sectionLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SectionNestedTest() {
            ConvertToPdfAndCompare("sectionNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SectionWideTest() {
            ConvertToPdfAndCompare("sectionWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UlBasicTest() {
            ConvertToPdfAndCompare("ulBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UlBasic2ColTest() {
            ConvertToPdfAndCompare("ulBasic2Col", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UlLongTest() {
            ConvertToPdfAndCompare("ulLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UlNestedTest() {
            ConvertToPdfAndCompare("ulNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UlWideTest() {
            ConvertToPdfAndCompare("ulWide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexNestedTest() {
            ConvertToPdfAndCompare("flexNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DeepNestingTest() {
            ConvertToPdfAndCompare("deepNesting", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DeepNesting2Test() {
            ConvertToPdfAndCompare("deepNesting2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SectionButtonNestedTest() {
            ConvertToPdfAndCompare("sectionButtonNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SectionFormNestedTest() {
            ConvertToPdfAndCompare("sectionFormNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LargeMarginTest() {
            ConvertToPdfAndCompare("largeMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LargeMarginsAndPaddingsTest() {
            ConvertToPdfAndCompare("largeMarginsAndPaddings", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LargePaddingTest() {
            ConvertToPdfAndCompare("largePadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AJustifyContentTest() {
            ConvertToPdfAndCompare("aJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleJustifyContentTest() {
            ConvertToPdfAndCompare("articleJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void UlJustifyContentTest() {
            ConvertToPdfAndCompare("ulJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 6)]
        public virtual void PJustifyContentTest() {
            ConvertToPdfAndCompare("pJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void SectionJustifyContentTest() {
            ConvertToPdfAndCompare("sectionJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OlJustifyContentTest() {
            ConvertToPdfAndCompare("olJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LiJustifyContentTest() {
            ConvertToPdfAndCompare("liJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderJustifyContentTest() {
            ConvertToPdfAndCompare("headerJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FormJustifyContentTest() {
            ConvertToPdfAndCompare("formJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FooterJustifyContentTest() {
            ConvertToPdfAndCompare("footerJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FigureJustifyContentTest() {
            ConvertToPdfAndCompare("figureJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonJustifyContentTest() {
            ConvertToPdfAndCompare("buttonJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void ArticleAlignContentTest() {
            ConvertToPdfAndCompare("articleAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AAlignContentTest() {
            ConvertToPdfAndCompare("aAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 6)]
        public virtual void UlAlignContentTest() {
            ConvertToPdfAndCompare("ulAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 6)]
        public virtual void OlAlignContentTest() {
            ConvertToPdfAndCompare("olAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        public virtual void LiAlignContentTest() {
            ConvertToPdfAndCompare("liAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FormAlignContentTest() {
            ConvertToPdfAndCompare("formAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FigureAlignContentTest() {
            ConvertToPdfAndCompare("figureAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonAlignContentTest() {
            ConvertToPdfAndCompare("buttonAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 6)]
        public virtual void PAlignContentTest() {
            ConvertToPdfAndCompare("pAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void FooterAlignContentTest() {
            ConvertToPdfAndCompare("footerAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderAlignContentTest() {
            ConvertToPdfAndCompare("headerAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SectionAlignContentTest() {
            ConvertToPdfAndCompare("sectionAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapOnFlexTagsTest() {
            ConvertToPdfAndCompare("gapOnFlexTags", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 10)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 2)]
        public virtual void FlexTagAlignSelfTest() {
            ConvertToPdfAndCompare("flexTagAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        public virtual void PAlignSelfTest() {
            ConvertToPdfAndCompare("pAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 10)]
        public virtual void FlexTagAlignItemsTest() {
            ConvertToPdfAndCompare("flexTagAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void PAlignItemsTest() {
            ConvertToPdfAndCompare("pAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void GapOnFlexTags2Test() {
            ConvertToPdfAndCompare("gapOnFlexTags2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexWrapTest() {
            ConvertToPdfAndCompare("flexWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexNoWrapTest() {
            ConvertToPdfAndCompare("flexNoWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexWrapReverseTest() {
            ConvertToPdfAndCompare("flexWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderArticleNestedTest() {
            ConvertToPdfAndCompare("headerArticleNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexThirdLevelNestingSplitTest() {
            ConvertToPdfAndCompare("flexThirdLevelNestingSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SeveralFlexNestedSplitTest() {
            ConvertToPdfAndCompare("severalFlexNestedSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SeveralFlexThirdLevelNestingSplitTest() {
            ConvertToPdfAndCompare("severalFlexThirdLevelNestingSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
