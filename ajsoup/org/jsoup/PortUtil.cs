using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Org.Jsoup {
    internal static class PortUtil {
        public static bool HasMatch(Regex pattern, String input) {

            return pattern.IsMatch(input);
        }

        public static void Reset(StringBuilder sb) {
            var dic = new IdentityDictionary<int, int>(12);
            sb.Clear();
        }

        public static char[] ToChars(int codePoint) {
            return char.ConvertFromUtf32(codePoint).ToCharArray();
        }

        public const int CHARACTER_MIN_SUPPLEMENTARY_CODE_POINT = 0x010000;

        public static int CharCount(int codePoint) {
            return codePoint >= CHARACTER_MIN_SUPPLEMENTARY_CODE_POINT ? 2 : 1;
        }

        public static Encoding NewEncoder(Encoding charset) {
            return charset;
        }
    }

    public class IdentityComparaor<T> : EqualityComparer<T>
    {
        public override bool Equals(T x, T y)
        {
            return GetHashCode(x) == GetHashCode(y) && object.ReferenceEquals(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return Default.GetHashCode(obj);
        }
    }

    public class IdentityDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {

        public IdentityDictionary() :
            base(new IdentityComparaor<TKey>())
        {
        }

        public IdentityDictionary(Int32 capasity) :
            base(capasity, new IdentityComparaor<TKey>())
        {
        }

        public IdentityDictionary(IDictionary<TKey, TValue> dictionary) :
            base(dictionary, new IdentityComparaor<TKey>())
        {
        }
    }
}
