/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css.Apply;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Css.Apply.Impl {
    [NUnit.Framework.Category("UnitTest")]
    public class DefaultCssApplierFactoryTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CannotGetCssApplierForCustomTagViaReflection() {
            ICssApplier cssApplier = new TestCssApplierFactory().GetCssApplier(new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element
                (iText.StyledXmlParser.Jsoup.Parser.Tag.ValueOf("custom-tag"), "")));
            NUnit.Framework.Assert.AreEqual(typeof(TestClass), cssApplier.GetType());
        }
    }

    internal class TestCssApplierFactory : DefaultCssApplierFactory {
        public TestCssApplierFactory() {
            GetDefaultMapping().PutMapping("custom-tag", () => new TestClass());
        }
    }

    internal class TestClass : ICssApplier {
        public virtual void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
        }
    }
}
