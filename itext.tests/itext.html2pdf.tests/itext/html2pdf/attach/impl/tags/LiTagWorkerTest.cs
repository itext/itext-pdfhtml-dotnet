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
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags {
    [NUnit.Framework.Category("UnitTest")]
    public class LiTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void LangAttrInLiForTaggedPdfTest() {
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.LANG, "en");
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.LI), TagConstants.LI, attributes);
            JsoupElementNode node = new JsoupElementNode(element);
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.NORMAL);
            styles.Put(CssConstants.TEXT_TRANSFORM, CssConstants.LOWERCASE);
            node.SetStyles(styles);
            ProcessorContext processorContext = new ProcessorContext(new ConverterProperties());
            processorContext.GetState().Push(new UlOlTagWorker(node, processorContext));
            LiTagWorker tagWorker = new LiTagWorker(node, processorContext);
            IPropertyContainer propertyContainer = tagWorker.GetElementResult();
            NUnit.Framework.Assert.IsTrue(propertyContainer is IAccessibleElement);
            String lang = ((IAccessibleElement)propertyContainer).GetAccessibilityProperties().GetLanguage();
            NUnit.Framework.Assert.AreEqual("en", lang);
        }
    }
}
