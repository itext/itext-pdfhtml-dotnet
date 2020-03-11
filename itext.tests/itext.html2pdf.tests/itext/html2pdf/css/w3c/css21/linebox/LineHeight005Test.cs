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
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Linebox {
    public class LineHeight005Test : W3CCssAhemFontTest {
        protected internal override String GetHtmlFileName() {
            return "line-height-005.xht";
        }

        protected internal override ConverterProperties GetConverterProperties() {
            ConverterProperties converterProperties = base.GetConverterProperties();
            converterProperties.SetTagWorkerFactory(new HtmlModeTagWorkerFactory());
            return converterProperties;
        }

        [NUnit.Framework.Test]
        [LogMessage("The background rectangle has negative or zero sizes. It will not be displayed.")]
        [LogMessage("Unable to process external css file")]
        public override void Test() {
            base.Test();
        }
    }
}
