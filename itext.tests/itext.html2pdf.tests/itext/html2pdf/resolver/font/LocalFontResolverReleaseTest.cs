using System;
using System.IO;
using iText.Html2pdf;
using iText.Test;

namespace iText.Html2pdf.Resolver.Font {
    public class LocalFontResolverReleaseTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/resolver/font/LocalFontResolverReleaseTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/resolver/font/LocalFontResolverReleaseTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void TestThatLocalFontIsReleasedAfterConversion() {
            String htmlFileName = "test.html";
            String fontFileName = "NotoSans-Regular.ttf";
            String sourceHtmlFile = sourceFolder + htmlFileName;
            String sourceFontFile = sourceFolder + fontFileName;
            String workDir = destinationFolder + "work/";
            CreateDestinationFolder(workDir);
            String targetPdfFile = workDir + "target.pdf";
            String workDirHtmlFile = workDir + htmlFileName;
            String workDirFontFile = workDir + fontFileName;
            File.Copy(Path.Combine(sourceHtmlFile), Path.Combine(workDirHtmlFile));
            File.Copy(Path.Combine(sourceFontFile), Path.Combine(workDirFontFile));
            // The issue cannot be reproduced consistently and automatically with just single conversion for some reason
            // Probably related to some optimizations done by JVM
            // It already reproduces with 2 conversions, but here we do more to get rid of possible JVM even trickier optimizations
            for (int i = 0; i < 5; i++) {
                HtmlConverter.ConvertToPdf(new FileInfo(workDirHtmlFile), new FileInfo(targetPdfFile));
            }
            // The resource must be freed after the conversion
            FileInfo resourceToBeRemoved = new FileInfo(workDirFontFile);
            resourceToBeRemoved.Delete();
            NUnit.Framework.Assert.IsFalse(resourceToBeRemoved.Exists);
        }
    }
}
