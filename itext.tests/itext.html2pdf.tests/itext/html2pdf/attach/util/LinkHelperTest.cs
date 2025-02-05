/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
Authors: Apryse Software.

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
using iText.Commons.Datastructures;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class LinkHelperTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CreateDestinationDestinationTest() {
            ITagWorker worker = new DivTagWorker(new PageTargetCountElementNode(null, ""), null);
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.ID, "some_id");
            attributes.Put(AttributeConstants.HREF, "#some_id");
            JsoupElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("a"), "", attributes));
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            context.GetLinkContext().ScanForIds(elementNode);
            LinkHelper.CreateDestination(worker, elementNode, context);
            Object destination = worker.GetElementResult().GetProperty<Object>(Property.DESTINATION);
            Tuple2<String, PdfDictionary> destTuple = (Tuple2<String, PdfDictionary>)destination;
            NUnit.Framework.Assert.AreEqual("some_id", destTuple.GetFirst());
            NUnit.Framework.Assert.AreEqual(new PdfString("some_id"), destTuple.GetSecond().Get(PdfName.D));
        }

        [NUnit.Framework.Test]
        public virtual void CreateDestinationIDTest() {
            ITagWorker worker = new DivTagWorker(new PageTargetCountElementNode(null, ""), null);
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.ID, "some_id");
            JsoupElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("a"), "", attributes));
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            LinkHelper.CreateDestination(worker, elementNode, context);
            NUnit.Framework.Assert.AreEqual("some_id", worker.GetElementResult().GetProperty<String>(Property.ID));
        }
    }
}
