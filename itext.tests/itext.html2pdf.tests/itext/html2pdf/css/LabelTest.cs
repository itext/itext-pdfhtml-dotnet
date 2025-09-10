using System;
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Pdfua;
using iText.Pdfua.Exceptions;
using iText.StyledXmlParser.Resolver.Font;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class LabelTest : ExtendedHtmlConversionITextTest {
        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/LabelTest/";

        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/LabelTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImplicitLabelTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "implicitLabel.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "implicit label", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "implicitLabel.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "implicitLabel.pdf", 
                SOURCE_FOLDER + "cmp_implicitLabel.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void ImplicitLabelOnInputTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "implicitLabelOnInput.pdf"), 
                new PdfUAConfig(PdfUAConformance.PDF_UA_1, "implicit label", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "implicitLabelOnInput.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "implicitLabelOnInput.pdf"
                , SOURCE_FOLDER + "cmp_implicitLabelOnInput.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void ImplicitLabelOnSelectTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "implicitLabelOnSelect.pdf")
                , new PdfUAConfig(PdfUAConformance.PDF_UA_1, "implicit label", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "implicitLabelOnSelect.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "implicitLabelOnSelect.pdf"
                , SOURCE_FOLDER + "cmp_implicitLabelOnSelect.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void ImplicitLabelOnAreaTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "implicitLabelOnArea.pdf"), 
                new PdfUAConfig(PdfUAConformance.PDF_UA_1, "implicit label", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "implicitLabelOnArea.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "implicitLabelOnArea.pdf"
                , SOURCE_FOLDER + "cmp_implicitLabelOnArea.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void LabelledByTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "labelledBy.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "labeled by", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "labelledBy.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "labelledBy.pdf", SOURCE_FOLDER
                 + "cmp_labelledBy.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void LabelledByOrderTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "labelledByOrder.pdf"), new 
                PdfUAConfig(PdfUAConformance.PDF_UA_1, "labeled by", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "labelledByOrder.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "labelledByOrder.pdf"
                , SOURCE_FOLDER + "cmp_labelledByOrder.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void DescribedByTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "describedBy.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "described by", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "describedBy.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "describedBy.pdf", SOURCE_FOLDER
                 + "cmp_describedBy.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void ComplexImplicitLabelTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "complexImplicitLabel.pdf"), 
                new PdfUAConfig(PdfUAConformance.PDF_UA_1, "implicit label", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "complexImplicitLabel.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "complexImplicitLabel.pdf"
                , SOURCE_FOLDER + "cmp_complexImplicitLabel.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void AriaLabelTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "ariaLabel.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "aria label", "en-US"))) {
                pdf.SetTagged();
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "ariaLabel.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "ariaLabel.pdf", SOURCE_FOLDER
                 + "cmp_ariaLabel.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void ExplicitLabelSimpleTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "explicitLabel.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "explicit label", "en-US"))) {
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "explicitLabel.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "explicitLabel.pdf", 
                SOURCE_FOLDER + "cmp_explicitLabel.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void ExplicitLabelAfterElementTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "explicitLabelAfterElement.pdf"
                ), new PdfUAConfig(PdfUAConformance.PDF_UA_1, "explicit label", "en-US"))) {
                HtmlConverter.ConvertToPdf(iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                     + "explicitLabelAfterElement.html")), pdf, properties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "explicitLabelAfterElement.pdf"
                , SOURCE_FOLDER + "cmp_explicitLabelAfterElement.pdf", DESTINATION_FOLDER, "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void InvalidIdLabelTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "invalidIdLabel.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "invalid label", "en-US"));
            using (Stream input = iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                 + "invalidIdLabel.html"))) {
                Exception exception = NUnit.Framework.Assert.Catch(typeof(PdfUAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(input, pdf, properties);
                }
                );
                NUnit.Framework.Assert.AreEqual(PdfUAExceptionMessageConstants.MISSING_FORM_FIELD_DESCRIPTION, exception.Message
                    );
            }
        }

        [NUnit.Framework.Test]
        public virtual void InvalidIdLabelledByTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "invalidIdLabelledBy.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "invalid label", "en-US"));
            using (Stream input = iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                 + "invalidIdLabelledBy.html"))) {
                Exception exception = NUnit.Framework.Assert.Catch(typeof(PdfUAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(input, pdf, properties);
                }
                );
                NUnit.Framework.Assert.AreEqual(PdfUAExceptionMessageConstants.MISSING_FORM_FIELD_DESCRIPTION, exception.Message
                    );
            }
        }

        [NUnit.Framework.Test]
        public virtual void InvalidIdDescribedByTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "invalidIdDescribedBy.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "invalid label", "en-US"));
            using (Stream input = iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                 + "invalidIdDescribedBy.html"))) {
                Exception exception = NUnit.Framework.Assert.Catch(typeof(PdfUAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(input, pdf, properties);
                }
                );
                NUnit.Framework.Assert.AreEqual(PdfUAExceptionMessageConstants.MISSING_FORM_FIELD_DESCRIPTION, exception.Message
                    );
            }
        }

        [NUnit.Framework.Test]
        public virtual void EmptyImplicitLabelTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "emptyImplicitLabel.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "invalid label", "en-US"));
            using (Stream input = iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                 + "emptyImplicitLabel.html"))) {
                Exception exception = NUnit.Framework.Assert.Catch(typeof(PdfUAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(input, pdf, properties);
                }
                );
                NUnit.Framework.Assert.AreEqual(PdfUAExceptionMessageConstants.MISSING_FORM_FIELD_DESCRIPTION, exception.Message
                    );
            }
        }

        [NUnit.Framework.Test]
        public virtual void EmptyExplicitLabelTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            PdfDocument pdf = new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "emptyExplicitLabel.pdf"), new PdfUAConfig
                (PdfUAConformance.PDF_UA_1, "invalid label", "en-US"));
            using (Stream input = iText.Commons.Utils.FileUtil.GetInputStreamForFile(System.IO.Path.Combine(SOURCE_FOLDER
                 + "emptyExplicitLabel.html"))) {
                Exception exception = NUnit.Framework.Assert.Catch(typeof(PdfUAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(input, pdf, properties);
                }
                );
                NUnit.Framework.Assert.AreEqual(PdfUAExceptionMessageConstants.MISSING_FORM_FIELD_DESCRIPTION, exception.Message
                    );
            }
        }

        [NUnit.Framework.Test]
        public virtual void LabelConvertToElementsTest() {
            ConverterProperties properties = new ConverterProperties().SetCreateAcroForm(true).SetFontProvider(new BasicFontProvider
                (false, true, false));
            using (Document document = new Document(new PdfUADocument(new PdfWriter(DESTINATION_FOLDER + "labelConvertToElements.pdf"
                ), new PdfUAConfig(PdfUAConformance.PDF_UA_1, "label to elements", "en-US")))) {
                IList<IElement> elements = HtmlConverter.ConvertToElements(iText.Commons.Utils.FileUtil.GetInputStreamForFile
                    (System.IO.Path.Combine(SOURCE_FOLDER + "labelConvertToElements.html")), properties);
                document.SetProperty(Property.COLLAPSING_MARGINS, true);
                foreach (IElement elem in elements) {
                    if (elem is IBlockElement) {
                        document.Add((IBlockElement)elem);
                    }
                    else {
                        if (elem is Image) {
                            document.Add((Image)elem);
                        }
                        else {
                            if (elem is AreaBreak) {
                                document.Add((AreaBreak)elem);
                            }
                            else {
                                NUnit.Framework.Assert.Fail("The #convertToElements method gave element which is unsupported as root element, it's unexpected."
                                    );
                            }
                        }
                    }
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "labelConvertToElements.pdf"
                , SOURCE_FOLDER + "cmp_labelConvertToElements.pdf", DESTINATION_FOLDER, "diff_"));
        }
    }
}
