using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class SvgTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/SvgTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/SvgTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Ignore("RND-982")]
        [NUnit.Framework.Test]
        public virtual void InlineSvgTest() {
            String name = "inline";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_PARSING_COULD_NOT_MAP_NODE)]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 4)]
        public virtual void ExternalImageSuccessTest() {
            String name = "external_img";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalImageNonExistentRefTest() {
            String name = "external_img_nonExistentRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_PARSING_COULD_NOT_MAP_NODE)]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 4)]
        public virtual void ExternalObjectSuccessTest() {
            String name = "external_object";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ExternalObjectNonExistentRefTest() {
            String name = "external_objectNonExistentRef";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff_" + name + "_"));
        }
    }
}
