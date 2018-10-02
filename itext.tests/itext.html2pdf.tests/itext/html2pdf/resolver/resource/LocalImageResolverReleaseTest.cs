using System;
using System.IO;
using iText.Html2pdf;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Resolver.Resource {
    public class LocalImageResolverReleaseTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/resolver/resource/LocalImageResolverReleaseTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/resolver/resource/LocalImageResolverReleaseTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.ERROR_RESOLVING_PARENT_STYLES, Count = 60)]
        public virtual void TestThatSvgIsReleasedAfterConversion() {
            String htmlFileName = "testWithSvg.html";
            String svgFileName = "imageWithMultipleShapes.svg";
            String imageFileName = "image.png";
            String sourceHtmlFile = sourceFolder + htmlFileName;
            String sourceSvgFile = sourceFolder + svgFileName;
            String sourceImageFile = sourceFolder + imageFileName;
            String workDir = destinationFolder + "work/";
            CreateDestinationFolder(workDir);
            String targetPdfFile = workDir + "target.pdf";
            String workDirHtmlFile = workDir + htmlFileName;
            String workDirSvgFile = workDir + svgFileName;
            String workDirImageFile = workDir + imageFileName;
            File.Copy(Path.Combine(sourceHtmlFile), Path.Combine(workDirHtmlFile));
            File.Copy(Path.Combine(sourceSvgFile), Path.Combine(workDirSvgFile));
            File.Copy(Path.Combine(sourceImageFile), Path.Combine(workDirImageFile));
            for (int i = 0; i < 10; i++) {
                HtmlConverter.ConvertToPdf(new FileInfo(workDirHtmlFile), new FileInfo(targetPdfFile));
            }
            // The resource must be freed after the conversion
            FileInfo resourceToBeRemoved = new FileInfo(workDirSvgFile);
            resourceToBeRemoved.Delete();
            NUnit.Framework.Assert.IsFalse(resourceToBeRemoved.Exists);
            resourceToBeRemoved = new FileInfo(workDirImageFile);
            resourceToBeRemoved.Delete();
            NUnit.Framework.Assert.IsFalse(resourceToBeRemoved.Exists);
        }
    }
}
