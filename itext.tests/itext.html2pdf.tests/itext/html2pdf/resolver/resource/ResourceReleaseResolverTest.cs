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
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;

namespace iText.Html2pdf.Resolver.Resource {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ResourceReleaseResolverTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/resolver/resource/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/resolver/resource/release/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TestThatSvgIsReleasedAfterConversion() {
            String dirName = "LocalImageResolverReleaseTest/";
            String htmlFileName = "testWithSvg.html";
            String svgFileName = "imageWithMultipleShapes.svg";
            String imageFileName = "image.png";
            String sourceHtmlFile = sourceFolder + dirName + htmlFileName;
            String sourceSvgFile = sourceFolder + dirName + svgFileName;
            String sourceImageFile = sourceFolder + dirName + imageFileName;
            String workDir = destinationFolder + dirName;
            CreateDestinationFolder(workDir);
            String targetPdfFile = workDir + "target.pdf";
            String workDirHtmlFile = workDir + htmlFileName;
            String workDirSvgFile = workDir + svgFileName;
            String workDirImageFile = workDir + imageFileName;
            File.Copy(System.IO.Path.Combine(sourceHtmlFile), System.IO.Path.Combine(workDirHtmlFile));
            File.Copy(System.IO.Path.Combine(sourceSvgFile), System.IO.Path.Combine(workDirSvgFile));
            File.Copy(System.IO.Path.Combine(sourceImageFile), System.IO.Path.Combine(workDirImageFile));
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

        [NUnit.Framework.Test]
        public virtual void TestThatLocalFontIsReleasedAfterConversion() {
            String dirName = "LocalFontIsReleased/";
            String htmlFileName = "localFontIsReleased.html";
            String fontFileName = "NotoSans-Regular.ttf";
            String sourceHtmlFile = sourceFolder + dirName + htmlFileName;
            String sourceFontFile = sourceFolder + dirName + fontFileName;
            String workDir = destinationFolder + dirName;
            CreateDestinationFolder(workDir);
            String targetPdfFile = workDir + "target.pdf";
            String workDirHtmlFile = workDir + htmlFileName;
            String workDirFontFile = workDir + fontFileName;
            File.Copy(System.IO.Path.Combine(sourceHtmlFile), System.IO.Path.Combine(workDirHtmlFile));
            File.Copy(System.IO.Path.Combine(sourceFontFile), System.IO.Path.Combine(workDirFontFile));
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

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("Ignored because currently the font file is not removed and test is failed. Remove this ignore after DEVSIX-3199 is fixed"
            )]
        public virtual void TestThatAddedFontIsReleasedAfterConversion() {
            // TODO unignore after DEVSIX-3199 is fixed
            String dirName = "AddedFontIsReleased/";
            String htmlFileName = "addedFontIsReleased.html";
            String fontFileName = "NotoSans-Regular.ttf";
            String sourceHtmlFile = sourceFolder + dirName + htmlFileName;
            String sourceFontFile = sourceFolder + dirName + fontFileName;
            String workDir = destinationFolder + dirName;
            CreateDestinationFolder(workDir);
            String targetPdfFile = workDir + "target.pdf";
            String workDirFontFile = workDir + fontFileName;
            File.Copy(System.IO.Path.Combine(sourceFontFile), System.IO.Path.Combine(workDirFontFile));
            BasicFontProvider fontProvider = new BasicFontProvider(true, false, false);
            fontProvider.AddDirectory(workDir);
            ConverterProperties properties = new ConverterProperties().SetBaseUri(sourceFolder).SetFontProvider(fontProvider
                );
            HtmlConverter.ConvertToPdf(new FileInfo(sourceHtmlFile), new FileInfo(targetPdfFile), properties);
            FileInfo resourceToBeRemoved = new FileInfo(workDirFontFile);
            FileUtil.DeleteFile(resourceToBeRemoved);
            NUnit.Framework.Assert.IsFalse(resourceToBeRemoved.Exists);
        }

        [NUnit.Framework.Test]
        public virtual void TestThatCssIsReleasedAfterConversion() {
            String dirName = "CssIsReleased/";
            String htmlFileName = "cssIsReleased.html";
            String cssFileName = "cssIsReleased.css";
            String sourceHtmlFile = sourceFolder + dirName + htmlFileName;
            String sourceCssFile = sourceFolder + dirName + cssFileName;
            String workDir = destinationFolder + dirName;
            CreateDestinationFolder(workDir);
            String targetPdfFile = workDir + "target.pdf";
            String workDirHtmlFile = workDir + htmlFileName;
            String workDirCssFile = workDir + cssFileName;
            File.Copy(System.IO.Path.Combine(sourceHtmlFile), System.IO.Path.Combine(workDirHtmlFile));
            File.Copy(System.IO.Path.Combine(sourceCssFile), System.IO.Path.Combine(workDirCssFile));
            ConverterProperties properties = new ConverterProperties().SetBaseUri(workDir);
            HtmlConverter.ConvertToPdf(new FileInfo(workDirHtmlFile), new FileInfo(targetPdfFile), properties);
            FileInfo resourceToBeRemoved = new FileInfo(workDirCssFile);
            FileUtil.DeleteFile(resourceToBeRemoved);
            NUnit.Framework.Assert.IsFalse(resourceToBeRemoved.Exists);
        }
    }
}
