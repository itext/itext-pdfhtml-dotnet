/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

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
