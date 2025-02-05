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
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Node;
using iText.Test;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    [NUnit.Framework.Category("UnitTest")]
    public class CssCounterManagerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void PushPopEveryCounterToCountersTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter1", 1);
            manager.ResetCounter("counter2", 2);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter1", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.AreEqual("2", manager.ResolveCounters("counter2", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            IElementNode node1 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node1);
            manager.ResetCounter("counter1", 3);
            manager.ResetCounter("counter2", 4);
            NUnit.Framework.Assert.AreEqual("1,3", manager.ResolveCounters("counter1", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            IElementNode node2 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node2);
            manager.ResetCounter("counter1", 5);
            NUnit.Framework.Assert.AreEqual("1,3,5", manager.ResolveCounters("counter1", ",", CounterDigitsGlyphStyle.
                DEFAULT));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            IElementNode node3 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node3);
            NUnit.Framework.Assert.AreEqual("1,3,5", manager.ResolveCounters("counter1", ",", CounterDigitsGlyphStyle.
                DEFAULT));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            manager.PopEveryCounterFromCounters(node3);
            NUnit.Framework.Assert.AreEqual("1,3,5", manager.ResolveCounters("counter1", ",", CounterDigitsGlyphStyle.
                DEFAULT));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            manager.PopEveryCounterFromCounters(node2);
            NUnit.Framework.Assert.AreEqual("1,3", manager.ResolveCounters("counter1", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.AreEqual("2,4", manager.ResolveCounters("counter2", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            manager.PopEveryCounterFromCounters(node1);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter1", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.AreEqual("2", manager.ResolveCounters("counter2", ",", CounterDigitsGlyphStyle.DEFAULT
                ));
            manager.PopEveryCounterFromCounters(node1);
        }

        [NUnit.Framework.Test]
        public virtual void ResolveTargetCounterTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter");
            manager.IncrementCounter("counter", 5);
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id", "counter", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id", "counter", CounterDigitsGlyphStyle.DEFAULT
                ));
            IElementNode node = new _CssPseudoElementNode_95(null, "name");
            manager.AddTargetCounterIfRequired(node);
            NUnit.Framework.Assert.AreEqual("5", manager.ResolveTargetCounter("id", "counter", CounterDigitsGlyphStyle
                .DEFAULT));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id", "counter2", CounterDigitsGlyphStyle.DEFAULT
                ));
            manager.IncrementCounter("counter2", 10);
            manager.AddTargetCounterIfRequired(node);
            NUnit.Framework.Assert.AreEqual("10", manager.ResolveTargetCounter("id", "counter2", CounterDigitsGlyphStyle
                .DEFAULT));
        }

        private sealed class _CssPseudoElementNode_95 : CssPseudoElementNode {
            public _CssPseudoElementNode_95(INode baseArg1, String baseArg2)
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
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounters("id", "counter", ".", CounterDigitsGlyphStyle.
                DEFAULT));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounters("id", "counter", ".", CounterDigitsGlyphStyle.
                DEFAULT));
            IElementNode node = new _CssPseudoElementNode_126(null, "name");
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("5", manager.ResolveTargetCounters("id", "counter", ".", CounterDigitsGlyphStyle
                .DEFAULT));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounters("id", "counter2", ".", CounterDigitsGlyphStyle
                .DEFAULT));
            manager.IncrementCounter("counter2", 10);
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("10", manager.ResolveTargetCounters("id", "counter2", ".", CounterDigitsGlyphStyle
                .DEFAULT));
            manager.PushEveryCounterToCounters(node);
            manager.ResetCounter("counter2", 7);
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("5", manager.ResolveTargetCounters("id", "counter", ".", CounterDigitsGlyphStyle
                .DEFAULT));
            NUnit.Framework.Assert.AreEqual("10.7", manager.ResolveTargetCounters("id", "counter2", ".", CounterDigitsGlyphStyle
                .DEFAULT));
        }

        private sealed class _CssPseudoElementNode_126 : CssPseudoElementNode {
            public _CssPseudoElementNode_126(INode baseArg1, String baseArg2)
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
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id1", "counter", CounterDigitsGlyphStyle.DEFAULT
                ));
            IElementNode node1 = new _CssPseudoElementNode_160(null, "name");
            IElementNode node2 = new _CssPseudoElementNode_169(null, "name");
            IElementNode node3 = new CssPseudoElementNode(null, "name");
            manager.AddTargetCounterIfRequired(node1);
            manager.AddTargetCounterIfRequired(node2);
            manager.AddTargetCounterIfRequired(node3);
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveTargetCounter("id1", "counter", CounterDigitsGlyphStyle
                .DEFAULT));
            NUnit.Framework.Assert.IsNull(manager.ResolveTargetCounter("id2", "counter", CounterDigitsGlyphStyle.DEFAULT
                ));
            manager.ResetCounter("counter");
            manager.IncrementCounter("counter");
            manager.AddTargetCounterIfRequired(node1);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveTargetCounter("id1", "counter", CounterDigitsGlyphStyle
                .DEFAULT));
        }

        private sealed class _CssPseudoElementNode_160 : CssPseudoElementNode {
            public _CssPseudoElementNode_160(INode baseArg1, String baseArg2)
                : base(baseArg1, baseArg2) {
            }

            public override String GetAttribute(String key) {
                if (AttributeConstants.ID.Equals(key)) {
                    return "id1";
                }
                return null;
            }
        }

        private sealed class _CssPseudoElementNode_169 : CssPseudoElementNode {
            public _CssPseudoElementNode_169(INode baseArg1, String baseArg2)
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
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounter("counter1", CounterDigitsGlyphStyle.DEFAULT));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter2", CounterDigitsGlyphStyle.DEFAULT));
            IElementNode node = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node);
            manager.ResetCounter("counter2", 1);
            manager.IncrementCounter("counter1", 2);
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounter("counter1", CounterDigitsGlyphStyle.DEFAULT));
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounter("counter2", CounterDigitsGlyphStyle.DEFAULT));
        }

        [NUnit.Framework.Test]
        public virtual void ResolveCountersTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter1", 1);
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter1", ";", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounters("counter2", "::", CounterDigitsGlyphStyle.DEFAULT
                ));
            IElementNode node1 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node1);
            manager.ResetCounter("counter2", 1);
            manager.IncrementCounter("counter1", 2);
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounters("counter1", ";", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounters("counter2", "::", CounterDigitsGlyphStyle.DEFAULT
                ));
            IElementNode node2 = new CssPseudoElementNode(null, "name");
            manager.PushEveryCounterToCounters(node2);
            manager.ResetCounter("counter1", 2);
            manager.ResetCounter("counter1", 30);
            manager.ResetCounter("counter2", 10);
            NUnit.Framework.Assert.AreEqual("3;30", manager.ResolveCounters("counter1", ";", CounterDigitsGlyphStyle.DEFAULT
                ));
            NUnit.Framework.Assert.AreEqual("1::10", manager.ResolveCounters("counter2", "::", CounterDigitsGlyphStyle
                .DEFAULT));
        }

        [NUnit.Framework.Test]
        public virtual void ResetCounterTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter");
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", CounterDigitsGlyphStyle.DEFAULT));
            manager.ResetCounter("counter", 101);
            NUnit.Framework.Assert.AreEqual("101", manager.ResolveCounter("counter", CounterDigitsGlyphStyle.DEFAULT));
            manager.ResetCounter("counter", -5);
            NUnit.Framework.Assert.AreEqual("-5", manager.ResolveCounter("counter", CounterDigitsGlyphStyle.DEFAULT));
        }

        [NUnit.Framework.Test]
        public virtual void IncrementCounterTest() {
            CssCounterManager manager = new CssCounterManager();
            manager.ResetCounter("counter");
            manager.IncrementCounter("counter");
            NUnit.Framework.Assert.AreEqual("1", manager.ResolveCounter("counter", CounterDigitsGlyphStyle.DEFAULT));
            manager.IncrementCounter("counter", 101);
            NUnit.Framework.Assert.AreEqual("102", manager.ResolveCounter("counter", CounterDigitsGlyphStyle.DEFAULT));
            manager.IncrementCounter("counter", -3);
            NUnit.Framework.Assert.AreEqual("99", manager.ResolveCounter("counter", CounterDigitsGlyphStyle.DEFAULT));
            manager.IncrementCounter("counter", -101);
            NUnit.Framework.Assert.AreEqual("-2", manager.ResolveCounter("counter", CounterDigitsGlyphStyle.DEFAULT));
        }

        [NUnit.Framework.Test]
        public virtual void ResolveEveryCounterWithNotDefaultSymbolsTest() {
            CssCounterManager manager = new CssCounterManager();
            IElementNode node = new _CssPseudoElementNode_271(null, "name");
            manager.ResetCounter("counter", 3);
            manager.PushEveryCounterToCounters(node);
            NUnit.Framework.Assert.AreEqual("III", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
            manager.ResetCounter("counter", 2);
            NUnit.Framework.Assert.AreEqual("III.II", manager.ResolveCounters("counter", ".", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
            manager.ResolveTargetCounter("id", "counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum(CssConstants.
                UPPER_ROMAN));
            manager.ResolveTargetCounters("id", "counter", ".", HtmlUtils.ConvertStringCounterGlyphStyleToEnum(CssConstants
                .UPPER_ROMAN));
            manager.AddTargetCounterIfRequired(node);
            manager.AddTargetCountersIfRequired(node);
            NUnit.Framework.Assert.AreEqual("II", manager.ResolveTargetCounter("id", "counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("III.II", manager.ResolveTargetCounters("id", "counter", ".", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
        }

        private sealed class _CssPseudoElementNode_271 : CssPseudoElementNode {
            public _CssPseudoElementNode_271(INode baseArg1, String baseArg2)
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
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (null)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.NONE)));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DISC)));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.SQUARE)));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.CIRCLE)));
            NUnit.Framework.Assert.AreEqual("C", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("C", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_LATIN)));
            NUnit.Framework.Assert.AreEqual("c", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("c", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_LATIN)));
            NUnit.Framework.Assert.AreEqual("\u03b3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_GREEK)));
            NUnit.Framework.Assert.AreEqual("iii", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("III", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("03", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DECIMAL_LEADING_ZERO)));
            NUnit.Framework.Assert.AreEqual("\u10D2", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.GEORGIAN)));
            NUnit.Framework.Assert.AreEqual("\u0533", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.ARMENIAN)));
            NUnit.Framework.Assert.AreEqual("3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                ("some_script")));
            manager.ResetCounter("counter", 0);
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (null)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.NONE)));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DISC)));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.SQUARE)));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.CIRCLE)));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_LATIN)));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_LATIN)));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_GREEK)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("00", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DECIMAL_LEADING_ZERO)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.GEORGIAN)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.ARMENIAN)));
            NUnit.Framework.Assert.AreEqual("0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                ("some_script")));
            manager.ResetCounter("counter", -3);
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (null)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.NONE)));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DISC)));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.SQUARE)));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.CIRCLE)));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_LATIN)));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_LATIN)));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_GREEK)));
            NUnit.Framework.Assert.AreEqual("-iii", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("-III", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("0-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DECIMAL_LEADING_ZERO)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.GEORGIAN)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.ARMENIAN)));
            NUnit.Framework.Assert.AreEqual("-3", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                ("some_script")));
            manager.ResetCounter("counter", 5000);
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (null)));
            NUnit.Framework.Assert.AreEqual("", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.NONE)));
            NUnit.Framework.Assert.AreEqual("\u2022", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DISC)));
            NUnit.Framework.Assert.AreEqual("\u25a0", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.SQUARE)));
            NUnit.Framework.Assert.AreEqual("\u25e6", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.CIRCLE)));
            NUnit.Framework.Assert.AreEqual("GJH", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("GJH", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_LATIN)));
            NUnit.Framework.Assert.AreEqual("gjh", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ALPHA)));
            NUnit.Framework.Assert.AreEqual("gjh", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_LATIN)));
            NUnit.Framework.Assert.AreEqual("\u03b8\u03c0\u03b8", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_GREEK)));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.LOWER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.UPPER_ROMAN)));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.DECIMAL_LEADING_ZERO)));
            NUnit.Framework.Assert.AreEqual("\u10ed", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.GEORGIAN)));
            NUnit.Framework.Assert.AreEqual("\u0550", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                (CssConstants.ARMENIAN)));
            NUnit.Framework.Assert.AreEqual("5000", manager.ResolveCounter("counter", HtmlUtils.ConvertStringCounterGlyphStyleToEnum
                ("some_script")));
        }
    }
}
