using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class VisibilityTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VisibilityTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VisibilityTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyLastPageTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyLastPageTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyTableTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyTableTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertySvgTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertySvgTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyLinkTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyLinkTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyImagesTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyImagesTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormsTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormsTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormFieldTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormFieldTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormRadioButtonTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormRadioButtonTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.ACROFORM_NOT_SUPPORTED_FOR_SELECT)]
        public virtual void VisiblePropertyInFormDropdownListTest() {
            //TODO update cmp-file after DEVSIX-2090 and DEVSIX-1901 done
            String htmlFile = sourceFolder + "visiblePropertyInFormDropdownListTest.html";
            String outAcroPdf = destinationFolder + "visiblePropertyInFormDropdownListTest.pdf";
            ConverterProperties properties = new ConverterProperties();
            properties.SetCreateAcroForm(true);
            HtmlConverter.ConvertToPdf(new FileInfo(htmlFile), new FileInfo(outAcroPdf), properties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outAcroPdf, sourceFolder + "cmp_visiblePropertyInFormDropdownListTest.pdf"
                , destinationFolder, "diff_dropdown"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyInFormCheckBoxTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyInFormCheckBoxTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void VisiblePropertyDivTest() {
            //TODO update cmp-file after DEVSIX-2090 done
            ConvertToPdfAndCompare("visiblePropertyDivTest", sourceFolder, destinationFolder);
        }
    }
}
