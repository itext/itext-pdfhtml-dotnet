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
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace iText.Html2pdf.Element {
    public class ImageTest : ExternalExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ImageTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ImageTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImagesInBodyTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imagesInBody.html"), new FileInfo(destinationFolder
                 + "imagesInBody.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imagesInBody.pdf", sourceFolder
                 + "cmp_imagesInBody.pdf", destinationFolder, "diff18_"));
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWideBorders() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imagesWithWideBorders.html"), new FileInfo(destinationFolder
                 + "imagesWithWideBorders.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imagesWithWideBorders.pdf"
                , sourceFolder + "cmp_imagesWithWideBorders.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWideMargins() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imagesWithWideMargins.html"), new FileInfo(destinationFolder
                 + "imagesWithWideMargins.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imagesWithWideMargins.pdf"
                , sourceFolder + "cmp_imagesWithWideMargins.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWidePaddings() {
            // TODO DEVSIX-2467
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imagesWithWidePaddings.html"), new FileInfo(destinationFolder
                 + "imagesWithWidePaddings.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imagesWithWidePaddings.pdf"
                , sourceFolder + "cmp_imagesWithWidePaddings.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWidePaddingsBordersMargins() {
            // TODO DEVSIX-2467
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imagesWithWidePaddingsBordersMargins.html"), new FileInfo
                (destinationFolder + "imagesWithWidePaddingsBordersMargins.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imagesWithWidePaddingsBordersMargins.pdf"
                , sourceFolder + "cmp_imagesWithWidePaddingsBordersMargins.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void CheckImageBorderRadius() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "checkImageBorderRadius.html"), new FileInfo(destinationFolder
                 + "checkImageBorderRadius.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "checkImageBorderRadius.pdf"
                , sourceFolder + "cmp_checkImageBorderRadius.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImageFileDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "smallWidthImagePlacement.html"), new FileInfo(destinationFolder
                 + "smallWidthImagePlacement.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "smallWidthImagePlacement.pdf"
                , sourceFolder + "cmp_smallWidthImagePlacement.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImageUrlDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imageUrl.html"), new FileInfo(destinationFolder + 
                "imageUrlDocument.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imageUrlDocument.pdf"
                , sourceFolder + "cmp_imageUrlDocument.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImageWithIncorrectBase64Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imageWithIncorrectBase64.html"), new FileInfo(destinationFolder
                 + "imageWithIncorrectBase64.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imageWithIncorrectBase64.pdf"
                , sourceFolder + "cmp_imageWithIncorrectBase64.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImageBase64DifferentFormatsTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imageBase64DifferentFormats.html"), new FileInfo(destinationFolder
                 + "imageBase64DifferentFormats.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imageBase64DifferentFormats.pdf"
                , sourceFolder + "cmp_imageBase64DifferentFormats.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void SmallImageTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "smallImageTest.html"), new FileInfo(destinationFolder
                 + "smallImageTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "smallImageTest.pdf", 
                sourceFolder + "cmp_smallImageTest.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImageInSpanTest() {
            // TODO: DEVSIX-2485
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "imageInSpan.html"), new FileInfo(destinationFolder
                 + "imageInSpan.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imageInSpan.pdf", sourceFolder
                 + "cmp_imageInSpan.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void CaseSensitiveBase64DataInCssNormalizationTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "caseSensitiveBase64DataInCssNormalization.html"), 
                new FileInfo(destinationFolder + "caseSensitiveBase64DataInCssNormalization.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "caseSensitiveBase64DataInCssNormalization.pdf"
                , sourceFolder + "cmp_caseSensitiveBase64DataInCssNormalization.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void InlineBlockImageTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "inlineBlockImage.html"), new FileInfo(destinationFolder
                 + "inlineBlockImage.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "inlineBlockImage.pdf"
                , sourceFolder + "cmp_inlineBlockImage.pdf", destinationFolder));
        }

        /// <summary>Important: the name of the resource in this test is "base64.svg".</summary>
        /// <remarks>
        /// Important: the name of the resource in this test is "base64.svg".
        /// This is done deliberately and tests for a bug that was present before -
        /// image was only fetched as base64 value and not as resource link
        /// </remarks>
        [NUnit.Framework.Test]
        public virtual void SvgExternalResourceCornerCaseTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "svgExternalResourceCornerCase.html"), new FileInfo
                (destinationFolder + "svgExternalResourceCornerCase.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "svgExternalResourceCornerCase.pdf"
                , sourceFolder + "cmp_svgExternalResourceCornerCase.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImageAltTextTest() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + "imageAltText.pdf"));
            pdfDocument.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceFolder + "imageAltText.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, new ConverterProperties().SetBaseUri(sourceFolder
                    ));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "imageAltText.pdf", sourceFolder
                 + "cmp_imageAltText.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ImageUrlExternalDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "externalUrlImage.html"), new FileInfo(destinationFolder
                 + "externalUrlImage.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "externalUrlImage.pdf"
                , sourceFolder + "cmp_externalUrlImage.pdf", destinationFolder));
        }
    }
}
