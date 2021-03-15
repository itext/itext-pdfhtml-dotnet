/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class FlexAlgoTest : ExtendedHtmlConversionITextTest {
        /* To see unit tests for flex algorithm go to FlexUtilTest in layout module:
        - these test were created as unit tests for flex algo at first
        - the htmls were used to compare the widths, returned by the algo
        - we couldn't maintain such htmls, because they just are present for manual comparison, thus it
        was decided to create html2pdf tests out of them
        - the names are preserved: one can go to FlexUtilTest and see the corresponding tests, but be aware that with
        time they might change and we will not maintain such correspondance */
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexAlgoTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/resources/itext/html2pdf/css/FlexAlgoTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DefaultTest01() {
            ConvertToPdfAndCompare("defaultTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Item1BasisGtWidthGrow0Shrink01Test01() {
            ConvertToPdfAndCompare("item1BasisGtWidthGrow0Shrink01Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisGrow1Shrink0MarginBorderPaddingOnContainerTest01() {
            ConvertToPdfAndCompare("differentBasisGrow1Shrink0MarginBorderPaddingOnContainerTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisGrow1Shrink0MarginBorderPaddingOnContainerNoWidthTest01() {
            ConvertToPdfAndCompare("differentBasisGrow1Shrink0MarginBorderPaddingOnContainerNoWidthTest01", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisGtWidthGrow0Shrink0Test01() {
            ConvertToPdfAndCompare("basisGtWidthGrow0Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisMinGrow0Shrink1Item2Grow05Test01() {
            ConvertToPdfAndCompare("basisMinGrow0Shrink1Item2Grow05Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisMinGrow0Shrink1Item2Grow2Test01() {
            ConvertToPdfAndCompare("basisMinGrow0Shrink1Item2Grow2Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisMinGrow2Shrink1Test01() {
            ConvertToPdfAndCompare("basisMinGrow2Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisMinGrow05SumGt1Shrink1Test01() {
            ConvertToPdfAndCompare("basisMinGrow05SumGt1Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisMinGrow01SumLt1Shrink1Test01() {
            ConvertToPdfAndCompare("basisMinGrow01SumLt1Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisMinGrow0Shrink05SumGt1Test01() {
            ConvertToPdfAndCompare("basisMinGrow0Shrink05SumGt1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasisMinGrow0Shrink01SumLt1Test01() {
            ConvertToPdfAndCompare("basisMinGrow0Shrink01SumLt1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Basis50SumLtWidthGrow0Shrink1Test01() {
            ConvertToPdfAndCompare("basis50SumLtWidthGrow0Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Basis250SumGtWidthGrow0Shrink1Test01() {
            ConvertToPdfAndCompare("basis250SumGtWidthGrow0Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow0Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow0Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink0Item2MarginBorderPadding30Test01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink0Item2MarginBorderPadding30Test01", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MarginBorderPadding30Test01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MarginBorderPadding30Test01", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LtWidthGrow0Shrink1Item2MarginBorderPadding30Test01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("ltWidthGrow0Shrink1Item2MarginBorderPadding30Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void LtWidthGrow0Shrink1Item2MarginBorderPadding30JustifyContentCenterAlignItemsCenterTest01
            () {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("ltWidthGrow0Shrink1Item2MarginBorderPadding30JustifyContentCenterAlignItemsCenterTest01"
                , SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LtWidthGrow0Shrink1Item2MarginBorderPadding30JustifyContentFlexEndAlignItemsFlexEndTest01
            () {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("ltWidthGrow0Shrink1Item2MarginBorderPadding30JustifyContentFlexEndAlignItemsFlexEndTest01"
                , SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LtWidthGrow0Shrink1Item2MBP30JustifyContentFlexStartTest() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("ltWidthGrow0Shrink1Item2MBP30JustifyContentFlexStartTest", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void LtWidthGrow0Shrink1Item2MarginBorderPadding30AlignItemsStretchAndNormalTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("ltWidthGrow0Shrink1Item2MarginBorderPadding30AlignItemsStretchAndNormalTest01", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LtWidthGrow0Shrink0Item2MarginBorderPadding30JustifyContentCenterAlignItemsCenterDontFitTest01
            () {
            ConvertToPdfAndCompare("ltWidthGrow0Shrink0Item2MarginBorderPadding30JustifyContentCenterAlignItemsCenterDontFitTest01"
                , SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MuchContentTest01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MuchContentTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthLtBasisTest01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthLtBasisTest01", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MaxWidthLtBasisTest01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MaxWidthLtBasisTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MaxWidthLtBasisTest02() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MaxWidthLtBasisTest02", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MaxWidthLtBasisTest03() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MaxWidthLtBasisTest03", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink1Item1MinWidthGtBasisTest01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink1Item1MinWidthGtBasisTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ImgGtUsedWidthTest01() {
            ConvertToPdfAndCompare("imgGtUsedWidthTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthGtBasisTest01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthGtBasisTest01", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Basis1Grow0Test01() {
            ConvertToPdfAndCompare("basis1Grow0Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthGtBasisTest02() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthGtBasisTest02", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthGtBasisTest03() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MuchContentSetMinWidthGtBasisTest03", SOURCE_FOLDER
                , DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumEqWidthGrow1Shrink1Item2Basis0Test01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumEqWidthGrow1Shrink1Item2Basis0Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumEqWidthGrow1Shrink1Item2Basis0NoContentTest02() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumEqWidthGrow1Shrink1Item2Basis0NoContentTest02", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow0Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow0Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink05Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink05Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink01Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink01Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink5Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink5Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink1Item3Shrink50Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink1Item3Shrink50Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink1Item3Shrink5Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink1Item3Shrink5Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Basis250SumGtWidthGrow0Shrink1WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("basis250SumGtWidthGrow0Shrink1WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink1WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink1WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink05WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink05WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink01WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink01WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink5WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink5WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink1WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink1WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink1Item3Shrink50WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink1Item3Shrink50WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink1Item3Shrink5WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink1Item3Shrink5WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow0Shrink0WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow0Shrink0WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumGtWidthGrow1Shrink0WrapTest01() {
            //TODO DEVSIX-5086 Support flex-wrap property
            ConvertToPdfAndCompare("differentBasisSumGtWidthGrow1Shrink0WrapTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumLtWidthGrow0Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumLtWidthGrow0Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumLtWidthGrow1Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumLtWidthGrow1Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumLtWidthGrow0Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumLtWidthGrow0Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumLtWidthGrow1Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumLtWidthGrow1Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow0Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow0Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow0Shrink05Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow0Shrink05Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow0Shrink01Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow0Shrink01Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow0Shrink5Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow0Shrink5Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow1Shrink1Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow1Shrink1Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow1Shrink1Item3Shrink50Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow1Shrink1Item3Shrink50Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow1Shrink1Item3Shrink5Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow1Shrink1Item3Shrink5Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow0Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow0Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumGtWidthGrow1Shrink0Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumGtWidthGrow1Shrink0Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisPercentSumLtWidthGrow0Shrink0Item2Grow1Test01() {
            ConvertToPdfAndCompare("differentBasisPercentSumLtWidthGrow0Shrink0Item2Grow1Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow0Shrink0Item2Grow1Test01() {
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow0Shrink0Item2Grow1Test01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void DifferentBasisSumLtWidthGrow1Shrink1Item2MarginsTest01() {
            //TODO
            ConvertToPdfAndCompare("differentBasisSumLtWidthGrow1Shrink1Item2MarginsTest01", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }
    }
}
