using System;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Node;
using iText.Test;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    public class CssCounterManagerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void PushPopEveryCounterToCountersTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter1", 1);
            manager.ResetCounter("counter2", 2);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter1", ",", null));
            NUnit.Framework.Assert.AreEqual("2", manager.ResolveCounters("counter2", ",", null));
            IElementNode node1 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node1);
            manager.ResetCounter("counter1", 3);
            manager.ResetCounter("counter2", 4);
            NUnit.Framework.Assert.AreEqual("1,3", manager.ResolveCounters("counter1", ",", null));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", null));
            IElementNode node2 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node2);
            manager.ResetCounter("counter1", 5);
            NUnit.Framework.Assert.AreEqual("1,3,5", manager.ResolveCounters("counter1", ",", null));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", null));
            IElementNode node3 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node3);
            NUnit.Framework.Assert.AreEqual("1,3,5", manager.ResolveCounters("counter1", ",", null));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", null));
            manager.PopEveryCounterFromCounters(node3);
            NUnit.Framework.Assert.AreEqual("1,3,5", manager.ResolveCounters("counter1", ",", null));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", null));
            manager.PopEveryCounterFromCounters(node2);
            NUnit.Framework.Assert.AreEqual("1,3", manager.ResolveCounters("counter1", ",", null));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", null));
            manager.PopEveryCounterFromCounters(node1);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter1", ",", null));
            NUnit.Framework.Assert.AreEqual("2", manager.ResolveCounters("counter2", ",", null));
            manager.PopEveryCounterFromCounters(node1);
        }

        [NUnit.Framework.Test]
        public virtual void ResolveTargetCounterTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter");
            manager.IncrementCounter("counter", 5);
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id", "counter", null));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id", "counter", null));
            IElementNode node = new _CssPseudoElementNode_73(null, "name");
            manager.AddTargetCounterIfRequired(node);
            NUnit.Framework.Assert.AreEqual("5", manager.ResolveTargetCounter("id", "counter", null));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id", "counter2", null));
            manager.IncrementCounter("counter2", 10);
            manager.AddTargetCounterIfRequired(node);
            NUnit.Framework.Assert.AreEqual("10", manager.ResolveTargetCounter("id", "counter2", null));
        }

        private sealed class _CssPseudoElementNode_73 : CssPseudoElementNode {
            public _CssPseudoElementNode_73(INode baseArg1, String baseArg2)
                : base(baseArg1, baseArg2) {
            }

            public override String GetAttribute(String key) {
                if (AttributeConstants.ID.Equals(key)) {
                    return "id";
                }
                return null;
            }
        }

        [NUnit.Framework.Test]
        public virtual void ResolveTargetCountersTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter");
            manager.IncrementCounter("counter", 5);
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounters("id", "counter", ".", null));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounters("id", "counter", ".", null));
            IElementNode node = new _CssPseudoElementNode_104(null, "name");
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("5", manager.ResolveTargetCounters("id", "counter", ".", null));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounters("id", "counter2", ".", null));
            manager.IncrementCounter("counter2", 10);
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("10", manager.ResolveTargetCounters("id", "counter2", ".", null));
            manager.PushEveryCounterToCounters(node);
            manager.ResetCounter("counter2", 7);
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("5", manager.ResolveTargetCounters("id", "counter", ".", null));
            NUnit.Framework.Assert.AreEqual("10.7", manager.ResolveTargetCounters("id", "counter2", ".", null));
        }

        private sealed class _CssPseudoElementNode_104 : CssPseudoElementNode {
            public _CssPseudoElementNode_104(INode baseArg1, String baseArg2)
                : base(baseArg1, baseArg2) {
            }

            public override String GetAttribute(String key) {
                if (AttributeConstants.ID.Equals(key)) {
                    return "id";
                }
                return null;
            }
        }

        [NUnit.Framework.Test]
        public virtual void AddTargetCounterIfRequiredTest() {
            CssCounterManager manager = new CssCounterManager();
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id1", "counter", null));
            IElementNode node1 = new _CssPseudoElementNode_138(null, "name");
            IElementNode node2 = new _CssPseudoElementNode_147(null, "name");
            IElementNode node3 = new CssPseudoElementNode(null, "name");
            manager.AddTargetCounterIfRequired(node1);
            manager.AddTargetCounterIfRequired(node2);
            manager.AddTargetCounterIfRequired(node3);
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveTargetCounter("id1", "counter", null));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id2", "counter", null));
            manager.ResetCounter("counter");
            manager.IncrementCounter("counter");
            manager.AddTargetCounterIfRequired(node1);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveTargetCounter("id1", "counter", null));
        }

        private sealed class _CssPseudoElementNode_138 : CssPseudoElementNode {
            public _CssPseudoElementNode_138(INode baseArg1, String baseArg2)
                : base(baseArg1, baseArg2) {
            }

            public override String GetAttribute(String key) {
                if (AttributeConstants.ID.Equals(key)) {
                    return "id1";
                }
                return null;
            }
        }

        private sealed class _CssPseudoElementNode_147 : CssPseudoElementNode {
            public _CssPseudoElementNode_147(INode baseArg1, String baseArg2)
                : base(baseArg1, baseArg2) {
            }

            public override String GetAttribute(String key) {
                if (AttributeConstants.ID.Equals(key)) {
                    return "id2";
                }
                return null;
            }
        }

        [NUnit.Framework.Test]
        public virtual void ResolveCounterTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter1", 1);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounter("counter1", null));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter2", null));
            IElementNode node = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node);
            manager.ResetCounter("counter2", 1);
            manager.IncrementCounter("counter1", 2);
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounter("counter1", null));
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounter("counter2", null));
        }

        [NUnit.Framework.Test]
        public virtual void ResolveCountersTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter1", 1);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter1", ";", null));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounters("counter2", "::", null));
            IElementNode node1 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node1);
            manager.ResetCounter("counter2", 1);
            manager.IncrementCounter("counter1", 2);
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounters("counter1", ";", null));
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter2", "::", null));
            IElementNode node2 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node2);
            manager.ResetCounter("counter1", 2);
            manager.ResetCounter("counter1", 30);
            manager.ResetCounter("counter2", 10);
            NUnit.Framework.Assert.AreEqual("3;30", manager.ResolveCounters("counter1", ";", null));
            NUnit.Framework.Assert.AreEqual("1::10", manager.ResolveCounters("counter2", "::", null));
        }

        [NUnit.Framework.Test]
        public virtual void ResetCounterTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter");
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", null));
            manager.ResetCounter("counter", 101);
            NUnit.Framework.Assert.AreEqual("101", manager.ResolveCounter("counter", null));
            manager.ResetCounter("counter", -5);
            NUnit.Framework.Assert.AreEqual("-5", manager.ResolveCounter("counter", null));
        }

        [NUnit.Framework.Test]
        public virtual void IncrementCounterTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter");
            manager.IncrementCounter("counter");
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounter("counter", null));
            manager.IncrementCounter("counter", 101);
            NUnit.Framework.Assert.AreEqual("102", manager.ResolveCounter("counter", null));
            manager.IncrementCounter("counter", -3);
            NUnit.Framework.Assert.AreEqual("99", manager.ResolveCounter("counter", null));
            manager.IncrementCounter("counter", -101);
            NUnit.Framework.Assert.AreEqual("-2", manager.ResolveCounter("counter", null));
        }

        [NUnit.Framework.Test]
        public virtual void ResolveEveryCounterWithNotDefaultSymbolsTest() {
            CssCounterManager manager = new CssCounterManager();
            IElementNode node = new _CssPseudoElementNode_249(null, "name");
            manager.ResetCounter("counter", 3);
            manager.PushEveryCounterToCounters(node);
            NUnit.Framework.Assert.AreEqual("III", manager.ResolveCounter("counter", CssConstants.UPPER_ROMAN));
            manager.ResetCounter("counter", 2);
            NUnit.Framework.Assert.AreEqual("III.II", manager.ResolveCounters("counter", ".", CssConstants.UPPER_ROMAN
                ));
            manager.ResolveTargetCounter("id", "counter", CssConstants.UPPER_ROMAN);
            manager.ResolveTargetCounters("id", "counter", ".", CssConstants.UPPER_ROMAN);
            manager.AddTargetCounterIfRequired(node);
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("II", manager.ResolveTargetCounter("id", "counter", CssConstants.UPPER_ROMAN
                ));
            NUnit.Framework.Assert.AreEqual("III.II", manager.ResolveTargetCounters("id", "counter", ".", CssConstants
                .UPPER_ROMAN));
        }

        private sealed class _CssPseudoElementNode_249 : CssPseudoElementNode {
            public _CssPseudoElementNode_249(INode baseArg1, String baseArg2)
                : base(baseArg1, baseArg2) {
            }

            public override String GetAttribute(String key) {
                if (AttributeConstants.ID.Equals(key)) {
                    return "id";
                }
                return null;
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertCounterToSymbolTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter", 3);
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounter("counter", null));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.NONE));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", CssConstants.DISC));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", CssConstants.SQUARE));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", CssConstants.CIRCLE));
            NUnit.Framework.Assert.AreEqual("C", manager.ResolveCounter("counter", CssConstants.UPPER_ALPHA));
            NUnit.Framework.Assert.AreEqual("C", manager.ResolveCounter("counter", CssConstants.UPPER_LATIN));
            NUnit.Framework.Assert.AreEqual("c", manager.ResolveCounter("counter", CssConstants.LOWER_ALPHA));
            NUnit.Framework.Assert.AreEqual("c", manager.ResolveCounter("counter", CssConstants.LOWER_LATIN));
            NUnit.Framework.Assert.AreEqual("\u03b3", manager.ResolveCounter("counter", CssConstants.LOWER_GREEK));
            NUnit.Framework.Assert.AreEqual("iii", manager.ResolveCounter("counter", CssConstants.LOWER_ROMAN));
            NUnit.Framework.Assert.AreEqual("III", manager.ResolveCounter("counter", CssConstants.UPPER_ROMAN));
            NUnit.Framework.Assert.AreEqual("03", manager.ResolveCounter("counter", CssConstants.DECIMAL_LEADING_ZERO)
                );
            NUnit.Framework.Assert.AreEqual("\u10D2", manager.ResolveCounter("counter", CssConstants.GEORGIAN));
            NUnit.Framework.Assert.AreEqual("\u0533", manager.ResolveCounter("counter", CssConstants.ARMENIAN));
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounter("counter", "some_script"));
            manager.ResetCounter("counter", 0);
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", null));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.NONE));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", CssConstants.DISC));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", CssConstants.SQUARE));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", CssConstants.CIRCLE));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", CssConstants.UPPER_ALPHA));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", CssConstants.UPPER_LATIN));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", CssConstants.LOWER_ALPHA));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", CssConstants.LOWER_LATIN));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", CssConstants.LOWER_GREEK));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.LOWER_ROMAN));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.UPPER_ROMAN));
            NUnit.Framework.Assert.AreEqual("00", manager.ResolveCounter("counter", CssConstants.DECIMAL_LEADING_ZERO)
                );
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.GEORGIAN));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.ARMENIAN));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", "some_script"));
            manager.ResetCounter("counter", -3);
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", null));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.NONE));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", CssConstants.DISC));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", CssConstants.SQUARE));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", CssConstants.CIRCLE));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", CssConstants.UPPER_ALPHA));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", CssConstants.UPPER_LATIN));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", CssConstants.LOWER_ALPHA));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", CssConstants.LOWER_LATIN));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", CssConstants.LOWER_GREEK));
            NUnit.Framework.Assert.AreEqual("-iii", manager.ResolveCounter("counter", CssConstants.LOWER_ROMAN));
            NUnit.Framework.Assert.AreEqual("-III", manager.ResolveCounter("counter", CssConstants.UPPER_ROMAN));
            NUnit.Framework.Assert.AreEqual("0-3", manager.ResolveCounter("counter", CssConstants.DECIMAL_LEADING_ZERO
                ));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.GEORGIAN));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.ARMENIAN));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", "some_script"));
            manager.ResetCounter("counter", 5000);
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", null));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", CssConstants.NONE));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", CssConstants.DISC));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", CssConstants.SQUARE));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", CssConstants.CIRCLE));
            NUnit.Framework.Assert.AreEqual("GJH", manager.ResolveCounter("counter", CssConstants.UPPER_ALPHA));
            NUnit.Framework.Assert.AreEqual("GJH", manager.ResolveCounter("counter", CssConstants.UPPER_LATIN));
            NUnit.Framework.Assert.AreEqual("gjh", manager.ResolveCounter("counter", CssConstants.LOWER_ALPHA));
            NUnit.Framework.Assert.AreEqual("gjh", manager.ResolveCounter("counter", CssConstants.LOWER_LATIN));
            NUnit.Framework.Assert.AreEqual("\u03b8\u03c0\u03b8", manager.ResolveCounter("counter", CssConstants.LOWER_GREEK
                ));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", CssConstants.LOWER_ROMAN));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", CssConstants.UPPER_ROMAN));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", CssConstants.DECIMAL_LEADING_ZERO
                ));
            NUnit.Framework.Assert.AreEqual("\u10ed", manager.ResolveCounter("counter", CssConstants.GEORGIAN));
            NUnit.Framework.Assert.AreEqual("\u0550", manager.ResolveCounter("counter", CssConstants.ARMENIAN));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", "some_script"));
        }
    }
}
