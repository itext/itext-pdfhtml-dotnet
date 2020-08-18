using System;
using iText.Html2pdf;
using iText.Html2pdf.Css.W3c;
using iText.StyledXmlParser.Css.Media;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2027. There are NO multiple pages and there is NO a blue box on all pages.
    public class BackgroundAttachment010Test : W3CCssTest {
        private static readonly String SRC_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/w3c/css21/backgrounds/";

        protected internal override String GetHtmlFileName() {
            return "background-attachment-010.xht";
        }

        // iText sets "enable printing background images" by default, but "paged media view" enables below in the method
        protected internal override ConverterProperties GetConverterProperties() {
            return new ConverterProperties().SetBaseUri(SRC_FOLDER + "background-attachment-010.xht").SetMediaDeviceDescription
                (new MediaDeviceDescription(MediaType.PRINT));
        }
    }
}
