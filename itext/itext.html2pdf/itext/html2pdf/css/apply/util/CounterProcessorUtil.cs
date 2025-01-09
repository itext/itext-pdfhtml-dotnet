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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to process counters.</summary>
    public class CounterProcessorUtil {
        /// <summary>Processes counters.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        public static void ProcessCounters(IDictionary<String, String> cssProps, CssContext context) {
            String counterReset = cssProps.Get(CssConstants.COUNTER_RESET);
            ProcessReset(counterReset, context);
            String counterIncrement = cssProps.Get(CssConstants.COUNTER_INCREMENT);
            ProcessIncrement(counterIncrement, context);
        }

        /// <summary>Starts processing counters.</summary>
        /// <remarks>
        /// Starts processing counters. Pushes current counter values to counters if necessary.
        /// Usually it is expected that this method should be called before processing children of the element.
        /// </remarks>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element which counters shall be processed</param>
        public static void StartProcessingCounters(CssContext context, IElementNode element) {
            CssCounterManager counterManager = context.GetCounterManager();
            counterManager.PushEveryCounterToCounters(element);
        }

        /// <summary>Ends processing counters.</summary>
        /// <remarks>
        /// Ends processing counters. Pops values of given counter list from counters if necessary.
        /// Usually it is expected that this method should be called after processing cheldren of the element.
        /// </remarks>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element which counters shall be processed</param>
        public static void EndProcessingCounters(CssContext context, IElementNode element) {
            CssCounterManager counterManager = context.GetCounterManager();
            counterManager.PopEveryCounterFromCounters(element);
        }

        private static void ProcessReset(String counterReset, CssContext context) {
            if (counterReset != null) {
                CssCounterManager counterManager = context.GetCounterManager();
                String[] @params = iText.Commons.Utils.StringUtil.Split(counterReset, " ");
                for (int i = 0; i < @params.Length; i++) {
                    String counterName = @params[i];
                    int? possibleCounterValue;
                    if (i + 1 < @params.Length && (possibleCounterValue = CssDimensionParsingUtils.ParseInteger(@params[i + 1]
                        )) != null) {
                        counterManager.ResetCounter(counterName, (int)possibleCounterValue);
                        i++;
                    }
                    else {
                        counterManager.ResetCounter(counterName);
                    }
                }
            }
        }

        private static void ProcessIncrement(String counterIncrement, CssContext context) {
            if (counterIncrement != null) {
                CssCounterManager counterManager = context.GetCounterManager();
                String[] @params = iText.Commons.Utils.StringUtil.Split(counterIncrement, " ");
                for (int i = 0; i < @params.Length; i++) {
                    String counterName = @params[i];
                    int? possibleIncrementValue;
                    if (i + 1 < @params.Length && (possibleIncrementValue = CssDimensionParsingUtils.ParseInteger(@params[i + 
                        1])) != null) {
                        counterManager.IncrementCounter(counterName, (int)possibleIncrementValue);
                        i++;
                    }
                    else {
                        counterManager.IncrementCounter(counterName);
                    }
                }
            }
        }
    }
}
