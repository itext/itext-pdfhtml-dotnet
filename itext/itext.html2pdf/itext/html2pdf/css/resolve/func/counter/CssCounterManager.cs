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
using System.Text;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    /// <summary>Class that manages counters (e.g. for list symbols).</summary>
    public class CssCounterManager {
        /// <summary>The Constant DEFAULT_COUNTER_VALUE.</summary>
        private const int DEFAULT_COUNTER_VALUE = 0;

        /// <summary>The Constant DEFAULT_INCREMENT_VALUE.</summary>
        private const int DEFAULT_INCREMENT_VALUE = 1;

        /// <summary>Map to store target-counter values.</summary>
        /// <remarks>Map to store target-counter values. First key is target-counter ID. Second key is counter name.</remarks>
        private readonly IDictionary<String, IDictionary<String, int?>> targetCounterMap = new Dictionary<String, 
            IDictionary<String, int?>>();

        /// <summary>Map to store target-counters values.</summary>
        /// <remarks>Map to store target-counters values. First key is target-counters ID. Second key is counter name.
        ///     </remarks>
        private readonly IDictionary<String, IDictionary<String, String>> targetCountersMap = new Dictionary<String
            , IDictionary<String, String>>();

        /// <summary>Map to store counters values.</summary>
        /// <remarks>
        /// Map to store counters values. The key is the counter name, the value is the
        /// <see cref="System.Collections.Stack{E}"/>
        /// with counters values.
        /// </remarks>
        private readonly IDictionary<String, LinkedList<int>> counters = new Dictionary<String, LinkedList<int>>();

        /// <summary>Map to store counter values.</summary>
        /// <remarks>Map to store counter values. The key is the counter name, the value is the current counter value.
        ///     </remarks>
        private readonly IDictionary<String, int?> counterValues = new Dictionary<String, int?>();

        /// <summary>List of counters that were pushed before processing children of the corresponding element.</summary>
        private readonly IDictionary<IElementNode, IList<String>> pushedCountersMap = new Dictionary<IElementNode, 
            IList<String>>();

        /// <summary>
        /// Creates a new
        /// <see cref="CssCounterManager"/>
        /// instance.
        /// </summary>
        public CssCounterManager() {
        }

        /// <summary>Clears information about counters.</summary>
        /// <remarks>Clears information about counters. Target-counter(s) information remains.</remarks>
        public virtual void ClearManager() {
            counters.Clear();
        }

        /// <summary>Pushes every current non-null counter to stack of counters.</summary>
        /// <remarks>
        /// Pushes every current non-null counter to stack of counters.
        /// This method should be called when we are about to process child nodes.
        /// </remarks>
        /// <param name="element">the element which counters shall be pushed</param>
        public virtual void PushEveryCounterToCounters(IElementNode element) {
            IList<String> pushedCounters = new List<String>();
            foreach (KeyValuePair<String, int?> counter in new HashSet<KeyValuePair<String, int?>>(counterValues)) {
                if (counter.Value != null) {
                    PushCounter(counter.Key, counter.Value);
                    pushedCounters.Add(counter.Key);
                    counterValues.Put(counter.Key, null);
                }
            }
            pushedCountersMap.Put(element, pushedCounters);
        }

        /// <summary>Pops every given counter from stack of counters.</summary>
        /// <remarks>
        /// Pops every given counter from stack of counters.
        /// This method should be called when we have finished processing child nodes.
        /// </remarks>
        /// <param name="element">the element which counters shall be popped</param>
        public virtual void PopEveryCounterFromCounters(IElementNode element) {
            counterValues.Clear();
            if (pushedCountersMap.Get(element) != null) {
                foreach (String pushedCounter in pushedCountersMap.Get(element)) {
                    counterValues.Put(pushedCounter, PopCounter(pushedCounter));
                }
                pushedCountersMap.JRemove(element);
            }
        }

        /// <summary>Gets target-counter value for specified ID and counterName.</summary>
        /// <remarks>Gets target-counter value for specified ID and counterName. Value is converted according to listSymbolType.
        ///     </remarks>
        /// <param name="id">
        /// ID of the element. The first call adds ID to the Map, which means we require its value.
        /// The second call returns corresponding value if we already encountered corresponding element
        /// </param>
        /// <param name="counterName">
        /// name of the counter. The first call adds counterName to the Map,
        /// which means we require its value.
        /// The second call returns corresponding value if we already encountered corresponding element
        /// </param>
        /// <param name="listSymbolType">the list symbol type to convert counter's value. null if conversion is not required.
        ///     </param>
        /// <returns>target-counter value.</returns>
        public virtual String ResolveTargetCounter(String id, String counterName, CounterDigitsGlyphStyle listSymbolType
            ) {
            int? counterValue = null;
            if (targetCounterMap.ContainsKey(id)) {
                IDictionary<String, int?> countersForThisId = targetCounterMap.Get(id);
                if (countersForThisId.ContainsKey(counterName)) {
                    counterValue = countersForThisId.Get(counterName);
                }
                else {
                    countersForThisId.Put(counterName, null);
                }
            }
            else {
                targetCounterMap.Put(id, new Dictionary<String, int?>());
                targetCounterMap.Get(id).Put(counterName, null);
            }
            return counterValue == null ? null : HtmlUtils.ConvertNumberAccordingToGlyphStyle(listSymbolType, (int)counterValue
                );
        }

        /// <summary>Gets target-counter value for specified ID and counterName.</summary>
        /// <remarks>Gets target-counter value for specified ID and counterName. Value is converted according to listSymbolType.
        ///     </remarks>
        /// <param name="id">
        /// ID of the element. The first call adds ID at the Map,
        /// which means we require its value. The second call returns corresponding value
        /// if we already encountered this element
        /// </param>
        /// <param name="counterName">
        /// name of the counter. The first call adds name at the Map,
        /// which means we require its value. The second call returns corresponding value
        /// if we already encountered this element
        /// </param>
        /// <param name="counterSeparatorStr">separator to separate counters values.</param>
        /// <param name="listSymbolType">the list symbol type to convert counter's value. null if conversion is not required.
        ///     </param>
        /// <returns>target-counter value.</returns>
        public virtual String ResolveTargetCounters(String id, String counterName, String counterSeparatorStr, CounterDigitsGlyphStyle
             listSymbolType) {
            String countersStr = null;
            if (targetCountersMap.ContainsKey(id)) {
                IDictionary<String, String> countersForThisId = targetCountersMap.Get(id);
                if (countersForThisId.ContainsKey(counterName)) {
                    countersStr = countersForThisId.Get(counterName);
                }
                else {
                    countersForThisId.Put(counterName, null);
                }
            }
            else {
                targetCountersMap.Put(id, new Dictionary<String, String>());
                targetCountersMap.Get(id).Put(counterName, null);
            }
            if (countersStr == null) {
                return null;
            }
            else {
                String[] resolvedCounters = iText.Commons.Utils.StringUtil.Split(countersStr, "\\.");
                IList<String> convertedCounters = new List<String>();
                foreach (String counter in resolvedCounters) {
                    convertedCounters.Add(HtmlUtils.ConvertNumberAccordingToGlyphStyle(listSymbolType, Convert.ToInt32(counter
                        , System.Globalization.CultureInfo.InvariantCulture)));
                }
                return BuildCountersStringFromList(convertedCounters, counterSeparatorStr);
            }
        }

        /// <summary>Adds counter value to every counter in the Map corresponding to a node ID.</summary>
        /// <param name="node">node to take ID and scope from</param>
        public virtual void AddTargetCounterIfRequired(IElementNode node) {
            String id = node.GetAttribute(AttributeConstants.ID);
            if (id != null && targetCounterMap.ContainsKey(id)) {
                foreach (KeyValuePair<String, int?> targetCounter in new HashSet<KeyValuePair<String, int?>>(targetCounterMap
                    .Get(id))) {
                    String counterName = targetCounter.Key;
                    String counterStr = ResolveCounter(counterName, CounterDigitsGlyphStyle.DEFAULT);
                    if (counterStr != null) {
                        targetCounterMap.Get(id).Put(counterName, Convert.ToInt32(counterStr, System.Globalization.CultureInfo.InvariantCulture
                            ));
                    }
                }
            }
        }

        /// <summary>Adds counters value to every counter in the Map corresponding to a node ID.</summary>
        /// <param name="node">node to take ID and scope from</param>
        public virtual void AddTargetCountersIfRequired(IElementNode node) {
            String id = node.GetAttribute(AttributeConstants.ID);
            if (id != null && targetCountersMap.ContainsKey(id)) {
                foreach (KeyValuePair<String, String> targetCounter in new HashSet<KeyValuePair<String, String>>(targetCountersMap
                    .Get(id))) {
                    String counterName = targetCounter.Key;
                    String resolvedCounters = ResolveCounters(counterName, ".", CounterDigitsGlyphStyle.DEFAULT);
                    if (resolvedCounters != null) {
                        targetCountersMap.Get(id).Put(counterName, resolvedCounters);
                    }
                }
            }
        }

        /// <summary>Resolves a counter.</summary>
        /// <param name="counterName">the counter name</param>
        /// <param name="listSymbolType">the list symbol type</param>
        /// <returns>
        /// the counter value as a
        /// <see cref="System.String"/>
        /// </returns>
        public virtual String ResolveCounter(String counterName, CounterDigitsGlyphStyle listSymbolType) {
            int? result = counterValues.Get(counterName);
            if (result == null) {
                if (!counters.ContainsKey(counterName) || counters.Get(counterName).IsEmpty()) {
                    result = 0;
                }
                else {
                    result = counters.Get(counterName).JGetLast();
                }
            }
            return HtmlUtils.ConvertNumberAccordingToGlyphStyle(listSymbolType, (int)result);
        }

        /// <summary>Resolves counters.</summary>
        /// <param name="counterName">the counter name</param>
        /// <param name="counterSeparatorStr">the counter separator</param>
        /// <param name="listSymbolType">the list symbol type</param>
        /// <returns>
        /// the counters as a
        /// <see cref="System.String"/>
        /// </returns>
        public virtual String ResolveCounters(String counterName, String counterSeparatorStr, CounterDigitsGlyphStyle
             listSymbolType) {
            IList<String> resolvedCounters = new List<String>();
            if (counters.ContainsKey(counterName)) {
                foreach (int? value in counters.Get(counterName)) {
                    resolvedCounters.Add(HtmlUtils.ConvertNumberAccordingToGlyphStyle(listSymbolType, (int)value));
                }
            }
            int? currentValue = counterValues.Get(counterName);
            if (currentValue != null) {
                resolvedCounters.Add(HtmlUtils.ConvertNumberAccordingToGlyphStyle(listSymbolType, (int)currentValue));
            }
            if (resolvedCounters.IsEmpty()) {
                return HtmlUtils.ConvertNumberAccordingToGlyphStyle(listSymbolType, 0);
            }
            else {
                return BuildCountersStringFromList(resolvedCounters, counterSeparatorStr);
            }
        }

        /// <summary>Resets the counter.</summary>
        /// <param name="counterName">the counter name</param>
        public virtual void ResetCounter(String counterName) {
            ResetCounter(counterName, DEFAULT_COUNTER_VALUE);
        }

        /// <summary>Resets the counter.</summary>
        /// <param name="counterName">the counter name</param>
        /// <param name="value">the new value</param>
        public virtual void ResetCounter(String counterName, int value) {
            counterValues.Put(counterName, value);
        }

        /// <summary>Increments the counter.</summary>
        /// <param name="counterName">the counter name</param>
        public virtual void IncrementCounter(String counterName) {
            IncrementCounter(counterName, DEFAULT_INCREMENT_VALUE);
        }

        /// <summary>Increments the counter.</summary>
        /// <param name="counterName">the counter name</param>
        /// <param name="incrementValue">the increment value</param>
        public virtual void IncrementCounter(String counterName, int incrementValue) {
            int? currentValue = counterValues.Get(counterName);
            if (currentValue == null) {
                LinkedList<int> counterStack = counters.Get(counterName);
                if (counterStack == null || counterStack.IsEmpty()) {
                    // If 'counter-increment' or 'content' on an element or pseudo-element refers to a counter that is not in the scope of any 'counter-reset',
                    // implementations should behave as though a 'counter-reset' had reset the counter to 0 on that element or pseudo-element.
                    currentValue = DEFAULT_COUNTER_VALUE;
                    ResetCounter(counterName, (int)currentValue);
                    counterValues.Put(counterName, currentValue + incrementValue);
                }
                else {
                    currentValue = counterStack.JGetLast();
                    counterStack.RemoveLast();
                    counterStack.AddLast((int)(currentValue + incrementValue));
                }
            }
            else {
                counterValues.Put(counterName, currentValue + incrementValue);
            }
        }

        private int? PopCounter(String counterName) {
            if (counters.ContainsKey(counterName) && !counters.Get(counterName).IsEmpty()) {
                int? last = counters.Get(counterName).JGetLast();
                counters.Get(counterName).RemoveLast();
                return last;
            }
            return null;
        }

        private void PushCounter(String counterName, int? value) {
            if (!counters.ContainsKey(counterName)) {
                counters.Put(counterName, new LinkedList<int>());
            }
            counters.Get(counterName).AddLast((int)value);
        }

        private static String BuildCountersStringFromList(IList<String> resolvedCounters, String counterSeparatorStr
            ) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < resolvedCounters.Count; i++) {
                sb.Append(resolvedCounters[i]);
                if (i != resolvedCounters.Count - 1) {
                    sb.Append(counterSeparatorStr);
                }
            }
            return sb.ToString();
        }
    }
}
