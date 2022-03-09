using System;
using System.IO;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    public class ButtonColorTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attach/impl/layout/form/renderer/ButtonColorTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attach/impl/layout/form/renderer/ButtonColorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonsWithColorTest() {
            String outPdf = DESTINATION_FOLDER + "buttonsWithColor.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_buttonsWithColor.pdf";
            DrawButtons(outPdf, cmpPdf, ColorConstants.RED);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonsWithoutColorTest() {
            String outPdf = DESTINATION_FOLDER + "buttonsWithoutColor.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_buttonsWithoutColor.pdf";
            DrawButtons(outPdf, cmpPdf, null);
        }

        private static void DrawButtons(String outPdf, String cmpPdf, Color color) {
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream(outPdf, FileMode.Create)))) {
                using (Document document = new Document(pdfDocument)) {
                    Button button = new Button("button");
                    button.Add(new Paragraph("button child"));
                    InputButton inputButton = new InputButton("input button");
                    button.SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, false);
                    inputButton.SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, false);
                    button.SetProperty(Html2PdfProperty.FORM_FIELD_VALUE, "button value");
                    inputButton.SetProperty(Html2PdfProperty.FORM_FIELD_VALUE, "input button value");
                    button.SetProperty(Property.FONT_COLOR, color == null ? null : new TransparentColor(color));
                    inputButton.SetProperty(Property.BACKGROUND, color == null ? null : new Background(color));
                    document.Add(button);
                    document.Add(inputButton);
                }
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, DESTINATION_FOLDER));
        }
    }
}
