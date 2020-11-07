using System;
using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.StyledXmlParser.Node;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Resolve {
    public class CssContentPropertyResolverTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ResolveContentTargetCounterEnabledTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(url('#some_target'), page)");
            CssContext context = new CssContext();
            context.SetTargetCounterEnabled(true);
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.AreEqual(1, result.Count);
            NUnit.Framework.Assert.IsTrue(result[0] is PageTargetCountElementNode);
            NUnit.Framework.Assert.AreEqual("#some_target", ((PageTargetCountElementNode)result[0]).GetTarget());
        }

        [NUnit.Framework.Test]
        public virtual void ResolveContentTargetCounterNotPageTest() {
            // TODO DEVSIX-2995 This code should be correctly parsed.
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(url('#some_target'), some_counter)");
            CssContext context = new CssContext();
            context.SetTargetCounterEnabled(true);
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.AreEqual(0, result.Count);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CONTENT_PROPERTY_INVALID)]
        public virtual void ResolveContentTargetCounterDisabledTest() {
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.CONTENT, "target-counter(url('#some_target'), some_counter)");
            CssContext context = new CssContext();
            context.SetTargetCounterEnabled(false);
            IList<INode> result = CssContentPropertyResolver.ResolveContent(styles, null, context);
            NUnit.Framework.Assert.IsNull(result);
        }
    }
}
