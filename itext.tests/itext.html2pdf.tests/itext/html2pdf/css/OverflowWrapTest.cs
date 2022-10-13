/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("Integration test")]
    public class OverflowWrapTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/OverflowWrapTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/OverflowWrapTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapColoredBackgroundTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapColoredBackground.html"), new FileInfo
                (destinationFolder + "overflowWrapColoredBackground.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapColoredBackground.pdf"
                , sourceFolder + "cmp_overflowWrapColoredBackground.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowXOverflowWrapTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowXOverflowWrap.html"), new FileInfo(destinationFolder
                 + "overflowXOverflowWrap.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowXOverflowWrap.pdf"
                , sourceFolder + "cmp_overflowXOverflowWrap.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceAndOverflowWrapTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "whiteSpaceAndOverflowWrap.html"), new FileInfo(destinationFolder
                 + "whiteSpaceAndOverflowWrap.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "whiteSpaceAndOverflowWrap.pdf"
                , sourceFolder + "cmp_whiteSpaceAndOverflowWrap.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceOnParentAndOverflowWrapOnChildTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "whiteSpaceOnParentAndOverflowWrapOnChildTest.html"
                ), new FileInfo(destinationFolder + "whiteSpaceOnParentAndOverflowWrapOnChildTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "whiteSpaceOnParentAndOverflowWrapOnChildTest.pdf"
                , sourceFolder + "cmp_whiteSpaceOnParentAndOverflowWrapOnChildTest.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapAndFloatTest() {
            // TODO DEVSIX-2482
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapAndFloat.html"), new FileInfo(destinationFolder
                 + "overflowWrapAndFloat.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapAndFloat.pdf"
                , sourceFolder + "cmp_overflowWrapAndFloat.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            2)]
        public virtual void OverflowWrapTableScenarioTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapTableScenario.html"), new FileInfo(destinationFolder
                 + "overflowWrapTableScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapTableScenario.pdf"
                , sourceFolder + "cmp_overflowWrapTableScenario.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapWordWrapInheritance() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapWordWrapInheritance.html"), new FileInfo
                (destinationFolder + "overflowWrapWordWrapInheritance.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapWordWrapInheritance.pdf"
                , sourceFolder + "cmp_overflowWrapWordWrapInheritance.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void ChosenOverflowWrapValue01() {
            IList<IElement> elements = ConvertToElements("chosenOverflowWrapValue01");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            OverflowWrapPropertyValue? overflowWrapPropertyValue = paragraph.GetProperty<OverflowWrapPropertyValue?>(Property
                .OVERFLOW_WRAP);
            NUnit.Framework.Assert.IsNotNull(overflowWrapPropertyValue);
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.BREAK_WORD, overflowWrapPropertyValue);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void ChosenOverflowWrapValue02() {
            IList<IElement> elements = ConvertToElements("chosenOverflowWrapValue02");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            OverflowWrapPropertyValue? overflowWrapPropertyValue = paragraph.GetProperty<OverflowWrapPropertyValue?>(Property
                .OVERFLOW_WRAP);
            NUnit.Framework.Assert.IsNotNull(overflowWrapPropertyValue);
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.NORMAL, overflowWrapPropertyValue);
        }

        [NUnit.Framework.Test]
        public virtual void ChosenOverflowWrapValue03() {
            IList<IElement> elements = ConvertToElements("chosenOverflowWrapValue03");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            OverflowWrapPropertyValue? overflowWrapPropertyValue = paragraph.GetProperty<OverflowWrapPropertyValue?>(Property
                .OVERFLOW_WRAP);
            NUnit.Framework.Assert.IsNotNull(overflowWrapPropertyValue);
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.NORMAL, overflowWrapPropertyValue);
        }

        [NUnit.Framework.Test]
        public virtual void ChosenOverflowWrapValueUnset01() {
            IList<IElement> elements = ConvertToElements("chosenOverflowWrapValueUnset01");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.BREAK_WORD, paragraph.GetProperty<OverflowWrapPropertyValue?
                >(Property.OVERFLOW_WRAP));
            Div divAndNestedParagraph = (Div)elements[1];
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.ANYWHERE, divAndNestedParagraph.GetProperty<OverflowWrapPropertyValue?
                >(Property.OVERFLOW_WRAP));
            paragraph = (Paragraph)divAndNestedParagraph.GetChildren()[0];
            NUnit.Framework.Assert.IsNull(paragraph.GetProperty<OverflowWrapPropertyValue?>(Property.OVERFLOW_WRAP));
        }

        // todo DEVSIX-4723 replace assertNull above with the commented lines below
        //        Assert.assertEquals(OverflowWrapPropertyValue.ANYWHERE,
        //                paragraph.<OverflowWrapPropertyValue>getProperty(Property.OVERFLOW_WRAP));
        [NUnit.Framework.Test]
        public virtual void ChosenOverflowWrapValueUnset02() {
            IList<IElement> elements = ConvertToElements("chosenOverflowWrapValueUnset02");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.NORMAL, paragraph.GetProperty<OverflowWrapPropertyValue?
                >(Property.OVERFLOW_WRAP));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void OverflowWrapWordWrapInheritanceAndInvalidValues() {
            IList<IElement> elements = ConvertToElements("overflowWrapWordWrapInheritanceAndInvalidValues");
            Div div = (Div)elements[0];
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.ANYWHERE, div.GetProperty<OverflowWrapPropertyValue?
                >(Property.OVERFLOW_WRAP));
            IList<IElement> children = div.GetChildren();
            NUnit.Framework.Assert.AreEqual(2, children.Count);
            Paragraph firstChild = (Paragraph)children[0];
            NUnit.Framework.Assert.IsNull(firstChild.GetProperty<OverflowWrapPropertyValue?>(Property.OVERFLOW_WRAP));
            Paragraph secondChild = (Paragraph)children[1];
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.BREAK_WORD, secondChild.GetProperty<OverflowWrapPropertyValue?
                >(Property.OVERFLOW_WRAP));
            IList<IElement> innerChildren = secondChild.GetChildren();
            NUnit.Framework.Assert.AreEqual(2, children.Count);
            Text innerChild = (Text)innerChildren[1];
            NUnit.Framework.Assert.AreEqual(OverflowWrapPropertyValue.BREAK_WORD, innerChild.GetProperty<OverflowWrapPropertyValue?
                >(Property.OVERFLOW_WRAP));
        }

        private IList<IElement> ConvertToElements(String name) {
            String sourceHtml = sourceFolder + name + ".html";
            return HtmlConverter.ConvertToElements(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read));
        }
    }
}
