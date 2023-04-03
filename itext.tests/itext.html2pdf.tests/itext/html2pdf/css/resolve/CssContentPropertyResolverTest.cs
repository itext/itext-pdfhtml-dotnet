/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Logs;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Node;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Resolve {
    [NUnit.Framework.Category("UnitTest")]
    public class CssContentPropertyResolverTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ResolveContentTargetCounterEnabledTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(url('#some_target'), page)");
            CssContext context = new CssContext();
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.AreEqual(1, result.Count);
            NUnit.Framework.Assert.IsTrue(result[0] is PageTargetCountElementNode);
            NUnit.Framework.Assert.AreEqual("#some_target", ((PageTargetCountElementNode)result[0]).GetTarget());
        }

        [NUnit.Framework.Test]
        public virtual void ResolveContentTargetCounterNotPageTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(url('#some_target'), some_counter)");
            CssContext context = new CssContext();
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.AreEqual(1, result.Count);
            NUnit.Framework.Assert.IsTrue(result[0] is ITextNode);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 4)]
        public virtual void ResolveContentInvalidParamsTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(url('#some_target'))");
            CssContext context = new CssContext();
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNull(result);
            styles.Put(CssConstants.CONTENT, "target-counters(url('#some_target'), some_counter)");
            result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNull(result);
            styles.Put(CssConstants.CONTENT, "counter()");
            result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNull(result);
            styles.Put(CssConstants.CONTENT, "counters(some_counter)");
            result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNull(result);
        }

        [NUnit.Framework.Test]
        public virtual void ResolveContentPagesTargetCountersTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(url('#some_target'), pages)");
            CssContext context = new CssContext();
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.AreEqual(1, result.Count);
            NUnit.Framework.Assert.IsTrue(result[0] is PageCountElementNode);
            NUnit.Framework.Assert.IsTrue(((PageCountElementNode)result[0]).IsTotalPageCount());
            styles.Put(CssConstants.CONTENT, "target-counters(url('#some_target'), pages, '.')");
            result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.AreEqual(1, result.Count);
            NUnit.Framework.Assert.IsTrue(result[0] is PageCountElementNode);
            NUnit.Framework.Assert.IsTrue(((PageCountElementNode)result[0]).IsTotalPageCount());
        }

        [NUnit.Framework.Test]
        public virtual void ResolveContentCounterNotPageTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "counter(some_counter)");
            CssContext context = new CssContext();
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.AreEqual(1, result.Count);
            NUnit.Framework.Assert.IsTrue(result[0] is ITextNode);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 1)]
        public virtual void ResolveContentWrongTargetCounterTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(attr(), pages)");
            CssContext context = new CssContext();
            IElementNode iNode = new CssPseudoElementNode(null, "test");
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, iNode, context);
            NUnit.Framework.Assert.IsNull(result);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 1)]
        public virtual void ResolveContentWrongTargetCountersTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counters(attr(), pages)");
            CssContext context = new CssContext();
            IElementNode iNode = new CssPseudoElementNode(null, "test");
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, iNode, context);
            NUnit.Framework.Assert.IsNull(result);
        }
    }
}
