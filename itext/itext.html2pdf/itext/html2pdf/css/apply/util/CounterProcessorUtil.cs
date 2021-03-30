/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
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
        /// <param name="scope">the scope</param>
        [System.ObsoleteAttribute(@"This method need to be removed in 7.2")]
        public static void ProcessCounters(IDictionary<String, String> cssProps, CssContext context, INode scope) {
            ProcessCounters(cssProps, context);
        }

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
                String[] @params = iText.IO.Util.StringUtil.Split(counterReset, " ");
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
                String[] @params = iText.IO.Util.StringUtil.Split(counterIncrement, " ");
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
