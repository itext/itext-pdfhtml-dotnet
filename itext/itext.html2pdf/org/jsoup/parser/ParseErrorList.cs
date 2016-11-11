using System.Collections.Generic;

namespace Org.Jsoup.Parser {
    /// <summary>A container for ParseErrors.</summary>
    /// <author>Jonathan Hedley</author>
    internal class ParseErrorList : List<ParseError> {
        private const int INITIAL_CAPACITY = 16;

        private readonly int maxSize;

        internal ParseErrorList(int initialCapacity, int maxSize)
            : base(initialCapacity) {
            this.maxSize = maxSize;
        }

        internal virtual bool CanAddError() {
            return Count < maxSize;
        }

        internal virtual int GetMaxSize() {
            return maxSize;
        }

        internal static Org.Jsoup.Parser.ParseErrorList NoTracking() {
            return new Org.Jsoup.Parser.ParseErrorList(0, 0);
        }

        internal static Org.Jsoup.Parser.ParseErrorList Tracking(int maxSize) {
            return new Org.Jsoup.Parser.ParseErrorList(INITIAL_CAPACITY, maxSize);
        }
    }
}
