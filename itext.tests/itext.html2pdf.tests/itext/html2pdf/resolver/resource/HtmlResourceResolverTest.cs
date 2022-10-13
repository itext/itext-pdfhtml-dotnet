/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using iText.Html2pdf.Logs;
using iText.Html2pdf.Util;
using iText.IO.Source;
using iText.IO.Util;
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
    // TODO: DEVSIX-5968 Add new tests in HtmlResourceResolverTest
    [NUnit.Framework.Category("Integration test")]
    public class HtmlResourceResolverTest : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/resolver/resource/HtmlResourceResolverTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/resolver/resource/HtmlResourceResolverTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest01() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverHtmlWithSvgTest01.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverHtmlWithSvgTest01.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "resourceResolverHtmlWithSvgTest01.html"), new FileInfo
                (outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI, Count = 2)]
        public virtual void ResourceResolverHtmlWithSvgTest02() {
            String baseUri = SOURCE_FOLDER + "%23r%e%2525s@o%25urces/";
            String outPdf = DESTINATION_FOLDER + "resourceResolverHtmlWithSvgTest02.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverHtmlWithSvgTest02.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverHtmlWithSvgTest02.html", outPdf, cmpPdf, baseUri);
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest07() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverTest07.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverTest07.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "resourceResolverTest07.html"), new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER, "diff07_"
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public virtual void ResourceResolverTest07A() {
            String baseUri = SOURCE_FOLDER + "%23r%e%2525s@o%25urces/";
            String outPdf = DESTINATION_FOLDER + "resourceResolverTest07A.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverTest07A.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverTest07A.html", outPdf, cmpPdf, baseUri);
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverTest07B() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverTest07B.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverTest07B.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "#r%e%25s@o%urces/resourceResolverTest07B.html"), 
                new FileInfo(outPdf));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER, "diff07B_"
                ));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public virtual void ResourceResolverTest07C() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverTest07C.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverTest07C.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "#r%e%25s@o%urces/resourceResolverTest07C.html"), 
                new FileInfo(outPdf), new ConverterProperties().SetBaseUri(SOURCE_FOLDER + "#r%e%25s@o%urces/.."));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER, "diff07C_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest03() {
            String baseUri = SOURCE_FOLDER + "%23r%e%2525s@o%25urces/";
            String outPdf = DESTINATION_FOLDER + "resourceResolverHtmlWithSvgTest03.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverHtmlWithSvgTest03.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverHtmlWithSvgTest03.html", outPdf, cmpPdf, baseUri);
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverHtmlWithSvgTest04() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverHtmlWithSvgTest04.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverHtmlWithSvgTest04.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverHtmlWithSvgTest04.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverCssWithSvg() {
            //TODO: update after DEVSIX-2239 fix
            String outPdf = DESTINATION_FOLDER + "resourceResolverCssWithSvg.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverCssWithSvg.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "resourceResolverCssWithSvg.html"), new FileInfo(outPdf
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
            )]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void ResourceResolverHtmlWithSvgDifferentLevels() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverHtmlWithSvgDifferentLevels.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverHtmlWithSvgDifferentLevels.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverHtmlWithSvgDifferentLevels.html", outPdf, cmpPdf, 
                SOURCE_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        public virtual void AttemptToProcessBySvgProcessingUtilSvgWithImageTest() {
            // TODO review this test in the scope of DEVSIX-4107
            String fileName = "svgWithImage.svg";
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            HtmlResourceResolver resourceResolver = new HtmlResourceResolver(SOURCE_FOLDER, context);
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
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        public virtual void AttemptToProcessBySvgProcessingUtilSvgWithSvgTest() {
            // TODO review this test in the scope of DEVSIX-4107
            String fileName = "svgWithSvg.svg";
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            HtmlResourceResolver resourceResolver = new HtmlResourceResolver(SOURCE_FOLDER, context);
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
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        public virtual void ResourceResolverSvgEmbeddedSvg() {
            // TODO review this test in the scope of DEVSIX-4107
            String outPdf = DESTINATION_FOLDER + "resourceResolverSvgEmbeddedSvg.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverSvgEmbeddedSvg.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverSvgEmbeddedSvg.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI
            )]
        public virtual void ResourceResolverObjectWithSvgEmbeddedSvg() {
            // TODO review this test in the scope of DEVSIX-4107
            String outPdf = DESTINATION_FOLDER + "resourceResolverObjectWithSvgEmbeddedSvg.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverObjectWithSvgEmbeddedSvg.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverObjectWithSvgEmbeddedSvg.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        // TODO DEVSIX-1595
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public virtual void ResourceResolverTest11() {
            String baseUri = "https://en.wikipedia.org/wiki/Welsh_Corgi";
            String outPdf = DESTINATION_FOLDER + "resourceResolverTest11.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverTest11.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverTest11.html", outPdf, cmpPdf, baseUri);
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverSvgWithImageInlineTest() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverSvgWithImageInline.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverSvgWithImageInline.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverSvgWithImageInline.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverSvgWithImageBackgroundTest() {
            //Browsers do not render this
            String outPdf = DESTINATION_FOLDER + "resourceResolverSvgWithImageBackground.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverSvgWithImageBackground.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverSvgWithImageBackground.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverSvgWithImageObjectTest() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverSvgWithImageObject.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverSvgWithImageObject.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverSvgWithImageObject.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_DATA_URI
            , Count = 3)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 3)]
        public virtual void ResourceResolverSvgDifferentFormatsTest() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverSvgDifferentFormats.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverSvgDifferentFormats.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverSvgDifferentFormats.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_DATA_URI
            )]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ResourceResolverNotValidInlineSvgTest() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverNotValidInlineSvg.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverNotValidInlineSvg.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverNotValidInlineSvg.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(SvgExceptionMessageConstant.NO_ROOT)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ResourceResolverIncorrectSyntaxTest() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverIncorrectSyntaxObject.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverIncorrectSyntaxObject.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverIncorrectSyntaxObject.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverLinkBaseRefTest() {
            String baseUri = SOURCE_FOLDER + "img/";
            String outPdf = DESTINATION_FOLDER + "resourceResolverLinkBaseRef.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverLinkBaseRef.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverLinkBaseRef.html", outPdf, cmpPdf, baseUri);
        }

        [NUnit.Framework.Test]
        public virtual void ResourceResolverLinkDirectRefTest() {
            String outPdf = DESTINATION_FOLDER + "resourceResolverLinkDirectRef.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_resourceResolverLinkDirectRef.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "resourceResolverLinkDirectRef.html", outPdf, cmpPdf, SOURCE_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfWithRelativeBaseUriTest() {
            String baseUri = SOURCE_FOLDER + "textWithStyleAndImage.html";
            String outPdf = DESTINATION_FOLDER + "convertToPdfWithRelativeBaseUriTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_convertToPdfWithRelativeBaseUriTest.pdf";
            ConvertHtmlFileToPdf(SOURCE_FOLDER + "textWithStyleAndImage.html", outPdf, cmpPdf, new ConverterProperties
                ().SetBaseUri(baseUri));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ConvertToPdfWithInvalidBaseUriTest() {
            String invalidBaseUri = "/folderInDiskRoot";
            String outPdf = DESTINATION_FOLDER + "convertToPdfWithInvalidBaseUriTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_convertToPdfWithInvalidBaseUriTest.pdf";
            ConvertHtmlFileToPdf(SOURCE_FOLDER + "textWithStyleAndImage.html", outPdf, cmpPdf, new ConverterProperties
                ().SetBaseUri(invalidBaseUri));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI)]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ConvertInputStreamToPdfWithoutBaseUriTest() {
            String outPdf = DESTINATION_FOLDER + "convertInputStreamToPdfWithoutBaseUriTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_convertInputStreamToPdfWithoutBaseUriTest.pdf";
            ConvertHtmlStreamToPdf(SOURCE_FOLDER + "textWithStyleAndImage.html", outPdf, cmpPdf, null);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertFileToPdfWithoutBaseUriTest() {
            String html = SOURCE_FOLDER + "textWithStyleAndImage.html";
            String outPdf = DESTINATION_FOLDER + "convertFileToPdfWithoutBaseUriTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_convertFileToPdfWithoutBaseUriTest.pdf";
            ConvertHtmlFileToPdf(html, outPdf, cmpPdf, null);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfWithAbsoluteBaseUriTest() {
            String baseUri = PathUtil.GetAbsolutePathToResourcesForHtmlResourceResolverTest();
            String outPdf = DESTINATION_FOLDER + "convertToPdfWithAbsoluteBaseUriTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_convertToPdfWithAbsoluteBaseUriTest.pdf";
            ConvertHtmlFileToPdf(SOURCE_FOLDER + "textWithStyleAndImageWithIncompletePath.html", outPdf, cmpPdf, new ConverterProperties
                ().SetBaseUri(baseUri));
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfWithBaseUriFromUriTest() {
            String baseUri = PathUtil.GetUriToResourcesForHtmlResourceResolverTest();
            String outPdf = DESTINATION_FOLDER + "convertToPdfWithBaseUriFromUriTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_convertToPdfWithBaseUriFromUriTest.pdf";
            ConvertHtmlFileToPdf(SOURCE_FOLDER + "textWithStyleAndImageWithIncompletePath.html", outPdf, cmpPdf, new ConverterProperties
                ().SetBaseUri(baseUri));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void BaseHrefViaHtmlResourceReferenceTest() {
            // TODO DEVSIX-6410 base href on html level is not supported
            String outPdf = DESTINATION_FOLDER + "baseHrefViaHtmlResourceReferenceTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_baseHrefViaHtmlResourceReferenceTest.pdf";
            ConvertHtmlFileToPdf(SOURCE_FOLDER + "baseHrefViaHtmlResourceReferenceTest.html", outPdf, cmpPdf, new ConverterProperties
                ());
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public virtual void BaseHrefViaHtmlStylesheetReferenceTest() {
            // TODO DEVSIX-6410 base href on html level is not supported
            String outPdf = DESTINATION_FOLDER + "baseHrefViaHtmlStylesheetReferenceTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_baseHrefViaHtmlStylesheetReferenceTest.pdf";
            ConvertHtmlFileToPdf(SOURCE_FOLDER + "baseHrefViaHtmlStylesheetReferenceTest.html", outPdf, cmpPdf, new ConverterProperties
                ());
        }

        private void ConvertHtmlStreamToPdf(String htmlPath, String outPdf, String cmpPdf, String baseUri) {
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            using (FileStream fileInputStream = new FileStream(htmlPath, FileMode.Open, FileAccess.Read)) {
                using (FileStream fileOutputStream = new FileStream(outPdf, FileMode.Create)) {
                    HtmlConverter.ConvertToPdf(fileInputStream, fileOutputStream, new ConverterProperties().SetBaseUri(baseUri
                        ));
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER));
        }

        private void ConvertHtmlFileToPdf(String htmlPath, String outPdf, String cmpPdf, ConverterProperties converterProperties
            ) {
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outPdf), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER));
        }
    }
}
