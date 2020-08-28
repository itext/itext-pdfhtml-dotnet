/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Util;
using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Utils;
using iText.Svg;
using iText.Svg.Converter;
using iText.Svg.Exceptions;
using iText.Svg.Processors;
using iText.Svg.Renderers;
using iText.Svg.Renderers.Impl;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Resolver.Resource {
    public class HtmlResourceResolverTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/resolver/resource/HtmlResourceResolverTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/resolver/resource/HtmlResourceResolverTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest01() {
            String outPdf = destinationFolder + "resourceResolverHtmlWithSvgTest01.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest01.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceResolverHtmlWithSvgTest01.html"), new FileInfo
                (outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI, Count = 2)]
        public virtual void ResourceResolverHtmlWithSvgTest02() {
            String baseUri = sourceFolder + "%23r%e%2525s@o%25urces/";
            String outPdf = destinationFolder + "resourceResolverHtmlWithSvgTest02.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest02.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverHtmlWithSvgTest02.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest07() {
            String outPdf = destinationFolder + "resourceResolverTest07.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceResolverTest07.html"), new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff07_"
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public virtual void ResourceResolverTest07A() {
            String baseUri = sourceFolder + "%23r%e%2525s@o%25urces/";
            String outPdf = destinationFolder + "resourceResolverTest07A.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07A.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest07A.html", FileMode.Open
                , FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff07A_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest07B() {
            String outPdf = destinationFolder + "resourceResolverTest07B.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07B.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "#r%e%25s@o%urces/resourceResolverTest07B.html"), new 
                FileInfo(outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff07B_"
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 1)]
        public virtual void ResourceResolverTest07C() {
            String outPdf = destinationFolder + "resourceResolverTest07C.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest07C.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "#r%e%25s@o%urces/resourceResolverTest07C.html"), new 
                FileInfo(outPdf), new ConverterProperties().SetBaseUri(sourceFolder + "#r%e%25s@o%urces/.."));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff07C_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest03() {
            String baseUri = sourceFolder + "%23r%e%2525s@o%25urces/";
            String outPdf = destinationFolder + "resourceResolverHtmlWithSvgTest03.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest03.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverHtmlWithSvgTest03.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest04() {
            String outPdf = destinationFolder + "resourceResolverHtmlWithSvgTest04.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgTest04.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverHtmlWithSvgTest04.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(sourceFolder
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverCssWithSvg() {
            //TODO: update after DEVSIX-2239 fix
            String outPdf = destinationFolder + "resourceResolverCssWithSvg.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverCssWithSvg.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "resourceResolverCssWithSvg.html"), new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void ResourceResolverHtmlWithSvgDifferentLevels() {
            String outPdf = destinationFolder + "resourceResolverHtmlWithSvgDifferentLevels.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverHtmlWithSvgDifferentLevels.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverHtmlWithSvgDifferentLevels.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(sourceFolder
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        public virtual void AttemptToProcessBySvgProcessingUtilSvgWithImageTest() {
            // TODO review this test in the scope of DEVSIX-4107
            String fileName = "svgWithImage.svg";
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            HtmlResourceResolver resourceResolver = new HtmlResourceResolver(sourceFolder, context);
            ISvgConverterProperties svgConverterProperties = ContextMappingHelper.MapToSvgConverterProperties(context);
            ISvgProcessorResult res = SvgConverter.ParseAndProcess(resourceResolver.RetrieveResourceAsInputStream(fileName
                ), svgConverterProperties);
            ISvgNodeRenderer imageRenderer = ((SvgTagSvgNodeRenderer)res.GetRootRenderer()).GetChildren()[0];
            // Remove the previous result of the resource resolving in order to demonstrate that the resource will not be
            // resolved due to not setting of baseUri in the SvgProcessingUtil#createXObjectFromProcessingResult method.
            imageRenderer.SetAttribute(SvgConstants.Attributes.XLINK_HREF, "doggo.jpg");
            SvgProcessingUtil processingUtil = new SvgProcessingUtil(resourceResolver);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            PdfFormXObject pdfFormXObject = processingUtil.CreateXObjectFromProcessingResult(res, pdfDocument);
            PdfDictionary resources = (PdfDictionary)pdfFormXObject.GetResources().GetPdfObject().Get(PdfName.XObject);
            PdfDictionary fm1Dict = (PdfDictionary)resources.Get(new PdfName("Fm1"));
            NUnit.Framework.Assert.IsFalse(((PdfDictionary)fm1Dict.Get(PdfName.Resources)).ContainsKey(PdfName.XObject
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        public virtual void AttemptToProcessBySvgProcessingUtilSvgWithSvgTest() {
            // TODO review this test in the scope of DEVSIX-4107
            String fileName = "svgWithSvg.svg";
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            HtmlResourceResolver resourceResolver = new HtmlResourceResolver(sourceFolder, context);
            ISvgConverterProperties svgConverterProperties = ContextMappingHelper.MapToSvgConverterProperties(context);
            ISvgProcessorResult res = SvgConverter.ParseAndProcess(resourceResolver.RetrieveResourceAsInputStream(fileName
                ), svgConverterProperties);
            ISvgNodeRenderer imageRenderer = ((SvgTagSvgNodeRenderer)res.GetRootRenderer()).GetChildren()[1];
            // Remove the previous result of the resource resolving in order to demonstrate that the resource will not be
            // resolved due to not setting of baseUri in the SvgProcessingUtil#createXObjectFromProcessingResult method.
            // But even if set baseUri in the SvgProcessingUtil#createXObjectFromProcessingResult method, the SVG will not
            // be processed, because in the createXObjectFromProcessingResult method we create ResourceResolver, not HtmlResourceResolver.
            imageRenderer.SetAttribute(SvgConstants.Attributes.XLINK_HREF, "res\\itextpdf.com\\lines.svg");
            SvgProcessingUtil processingUtil = new SvgProcessingUtil(resourceResolver);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            PdfFormXObject pdfFormXObject = processingUtil.CreateXObjectFromProcessingResult(res, pdfDocument);
            PdfDictionary resources = (PdfDictionary)pdfFormXObject.GetResources().GetPdfObject().Get(PdfName.XObject);
            PdfDictionary fm1Dict = (PdfDictionary)resources.Get(new PdfName("Fm1"));
            NUnit.Framework.Assert.IsFalse(((PdfDictionary)fm1Dict.Get(PdfName.Resources)).ContainsKey(PdfName.XObject
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        public virtual void ResourceResolverSvgEmbeddedSvg() {
            // TODO review this test in the scope of DEVSIX-4107
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverSvgEmbeddedSvg.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverSvgEmbeddedSvg.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverSvgEmbeddedSvg.html", FileMode.Open
                , FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffEmbeddedSvg_"
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        public virtual void ResourceResolverObjectWithSvgEmbeddedSvg() {
            // TODO review this test in the scope of DEVSIX-4107
            String baseUri = sourceFolder;
            String outPdf = destinationFolder + "resourceResolverObjectWithSvgEmbeddedSvg.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverObjectWithSvgEmbeddedSvg.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverObjectWithSvgEmbeddedSvg.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diffObjectWithSvg_"
                ));
        }

        [NUnit.Framework.Test]
        // TODO DEVSIX-1595
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public virtual void ResourceResolverTest11() {
            String outPdf = destinationFolder + "resourceResolverTest11.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverTest11.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverTest11.html", FileMode.Open
                , FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri("https://en.wikipedia.org/wiki/Welsh_Corgi"
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverSvgWithImageInlineTest() {
            String outPdf = destinationFolder + "resourceResolverSvgWithImageInline.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverSvgWithImageInline.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverSvgWithImageInline.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(sourceFolder
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverSvgWithImageBackgroundTest() {
            //Browsers do not render this
            String outPdf = destinationFolder + "resourceResolverSvgWithImageBackground.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverSvgWithImageBackground.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverSvgWithImageBackground.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(sourceFolder
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverSvgWithImageObjectTest() {
            String outPdf = destinationFolder + "resourceResolverSvgWithImageObject.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverSvgWithImageObject.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverSvgWithImageObject.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(sourceFolder
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(SvgLogMessageConstant.NOROOT)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ResourceResolverIncorrectSyntaxTest() {
            String outPdf = destinationFolder + "resourceResolverIncorrectSyntaxObject.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverIncorrectSyntaxObject.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverIncorrectSyntaxObject.html"
                , FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(sourceFolder
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverLinkBaseRefTest() {
            String outPdf = destinationFolder + "resourceResolverLinkBaseRef.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverLinkBaseRef.pdf";
            String baseUri = sourceFolder + "img/";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverLinkBaseRef.html", FileMode.Open
                , FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverLinkDirectRefTest() {
            String outPdf = destinationFolder + "resourceResolverLinkDirectRef.pdf";
            String cmpPdf = sourceFolder + "cmp_resourceResolverLinkDirectRef.pdf";
            using (FileStream fileInputStream = new FileStream(sourceFolder + "resourceResolverLinkDirectRef.html", FileMode.Open
                , FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(sourceFolder
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder));
        }
        // TODO test with absolute http links for resources?
        // TODO test with http base URI?
    }
}
