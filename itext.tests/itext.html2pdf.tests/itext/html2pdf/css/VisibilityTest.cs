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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VisibilityTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VisibilityTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VisibilityTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyLastPageTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyLastPageTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyTableTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyTableTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertySvgTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertySvgTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyLinkTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyLinkTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyImagesTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyImagesTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormsTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormsTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormFieldTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormFieldTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormRadioButtonTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormRadioButtonTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormDropdownListTest() {
            //TODO update cmp-file after DEVSIX-2090
            String htmlFile = sourceFolder + "visiblePropertyInFormDropdownListTest.html";
            String outAcroPdf = destinationFolder + "visiblePropertyInFormDropdownListTest.pdf";
            ConverterProperties properties = new ConverterProperties();
            properties.SetCreateAcroForm(true);
            HtmlConverter.ConvertToPdf(new FileInfo(htmlFile), new FileInfo(outAcroPdf), properties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outAcroPdf, sourceFolder + "cmp_visiblePropertyInFormDropdownListTest.pdf"
                , destinationFolder, "diff_dropdown"));
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormCheckBoxTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormCheckBoxTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void VisiblePropertyDivTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyDivTest", sourceFolder, destinationFolder);
        }
    }
}
