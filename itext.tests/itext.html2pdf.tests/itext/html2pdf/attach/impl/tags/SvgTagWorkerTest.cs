/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class SvgTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_SVG_ELEMENT, LogLevel = LogLevelConstants.
            ERROR)]
        public virtual void NoSvgRootTest() {
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.FIGURE), TagConstants.FIGURE);
            IElementNode elementNode = new JsoupElementNode(element);
            ConverterProperties properties = new ConverterProperties();
            ProcessorContext context = new ProcessorContext(properties);
            SvgTagWorker svgTagWorker = new SvgTagWorker(elementNode, context);
            NUnit.Framework.Assert.IsNull(svgTagWorker.GetElementResult());
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_SVG_ELEMENT, LogLevel = LogLevelConstants.
            ERROR)]
        public virtual void NullElementTest() {
            ConverterProperties properties = new ConverterProperties();
            ProcessorContext context = new ProcessorContext(properties);
            SvgTagWorker svgTagWorker = new SvgTagWorker(null, context);
            NUnit.Framework.Assert.IsNull(svgTagWorker.GetElementResult());
        }
    }
}
