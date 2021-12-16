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
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class PseudoElementsTest : ExtendedHtmlConversionITextTest {
        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/PseudoElementsTest/";

        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/PseudoElementsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentWithWidthAndHeightTest() {
            ConvertToPdfAndCompare("pseudoContentWithWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentWithPercentWidthAndHeightTest() {
            ConvertToPdfAndCompare("pseudoContentWithPercentWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentDisplayNoneTest() {
            ConvertToPdfAndCompare("pseudoContentDisplayNoneTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentWithAutoWidthAndHeightTest() {
            ConvertToPdfAndCompare("pseudoContentWithAutoWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest01() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest02() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest03() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest04() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest05() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest06() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest07() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest08() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void BeforeAfterPseudoTest09() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest10() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest11() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest12() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest13() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest14() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo01() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo02() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo03() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo04() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo05() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo06() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest01() {
            ConvertToPdfAndCompare("escapedStringTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest02() {
            ConvertToPdfAndCompare("escapedStringTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest03() {
            ConvertToPdfAndCompare("escapedStringTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest04() {
            ConvertToPdfAndCompare("escapedStringTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest05() {
            ConvertToPdfAndCompare("escapedStringTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 5)]
        public virtual void AttrTest01() {
            ConvertToPdfAndCompare("attrTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 3)]
        public virtual void AttrTest02() {
            ConvertToPdfAndCompare("attrTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest01() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest02() {
            // TODO DEVSIX-1393 position property is not supported for inline elements.
            ConvertToPdfAndCompare("emptyStillShownPseudoTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest03() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest04() {
            // TODO DEVSIX-1393 position property is not supported for inline elements.
            ConvertToPdfAndCompare("emptyStillShownPseudoTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest05() {
            // TODO DEVSIX-1393 position property is not supported for inline elements.
            ConvertToPdfAndCompare("emptyStillShownPseudoTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest06() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest07() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest08() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest09() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoDisplayTable01Test() {
            ConvertToPdfAndCompare("pseudoDisplayTable01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoDisplayTable02Test() {
            ConvertToPdfAndCompare("pseudoDisplayTable02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeDivTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeDiv", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeDivDisplayBlockTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeDivDisplayBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ImgPseudoBeforeImgTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeImg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeWithTextTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeWithText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeInSeveralDivsTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeInSeveralDivs", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoWithPageRuleTest() {
            ConvertToPdfAndCompare("imgPseudoWithPageRule", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NonNormalizedAfterBeforeTest() {
            ConvertToPdfAndCompare("nonNormalizedAfterBefore", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoElementsWithMarginTest() {
            // TODO: update cmp file after DEVSIX-6192 will be fixed
            ConvertToPdfAndCompare("pseudoElementsWithMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
