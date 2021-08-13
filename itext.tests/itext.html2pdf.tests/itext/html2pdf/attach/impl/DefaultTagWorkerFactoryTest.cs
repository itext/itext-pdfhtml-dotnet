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
using System;
using iText.Events.Util;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Exceptions;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl {
    public class DefaultTagWorkerFactoryTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CannotGetTagWorkerForCustomTagViaReflection() {
            String tag = "custom-tag";
            String className = "iText.Html2pdf.Attach.Impl.TestClass";
            NUnit.Framework.Assert.That(() =>  {
                new TestTagWorkerFactory().GetTagWorker(new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element
                    (iText.StyledXmlParser.Jsoup.Parser.Tag.ValueOf("custom-tag"), "")), new ProcessorContext(new ConverterProperties
                    ()));
            }
            , NUnit.Framework.Throws.InstanceOf<TagWorkerInitializationException>().With.Message.EqualTo(MessageFormatUtil.Format(TagWorkerInitializationException.REFLECTION_IN_TAG_WORKER_FACTORY_IMPLEMENTATION_FAILED, className, tag)))
;
        }
    }

    internal class TestTagWorkerFactory : DefaultTagWorkerFactory {
        public TestTagWorkerFactory() {
            defaultMapping.PutMapping("custom-tag", typeof(TestClass));
        }
    }

    internal class TestClass {
    }
}
