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
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Util;
using iText.Test;

namespace iText.Html2pdf.Resolver.Resource {
    public class ResourceReleaseResolverTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/resolver/resource/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/resolver/resource/";

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
            DefaultFontProvider fontProvider = new DefaultFontProvider(true, false, false);
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
