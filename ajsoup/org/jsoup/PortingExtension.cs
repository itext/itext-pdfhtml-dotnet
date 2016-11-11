using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using iText.IO.Util;
using Org.Jsoup.Helper;

namespace Org.Jsoup
{
    internal static class PortingExtension
    {
        public static String Name(this Encoding e) {
            return e.WebName.ToUpperInvariant();
        }

        public static String DisplayName(this Encoding e) {
            return e.WebName.ToUpperInvariant();
        }

        public static bool RegionMatches(this string s, bool ignoreCase, int toffset, String other, int ooffset, int len) {
            return 0 == String.Compare(s, toffset, other, ooffset, len, ignoreCase, CultureInfo.InvariantCulture);
        }

        public static bool StartsWith(this string s, string prefix, int pos) {
            int to = pos;
            int po = 0;
            int pc = prefix.Length;
            if ((pos < 0) || (pos > s.Length - pc)) {
                return false;
            }
            while (--pc >= 0) {
                if (s[to++] != prefix[po++]) {
                    return false;
                }
            }
            return true;
        }

        public static void ReadFully(this FileStream stream, byte[] bytes) {
            stream.Read(bytes, 0, bytes.Length);
        }

        public static String Decode(this Encoding encoding, ByteBuffer byteBuffer) {
            byte[] bom;
            int offset = 0;
            Encoding temp = null;
            if (encoding.CodePage == Encoding.Unicode.CodePage && byteBuffer.Remaining() >= 2) {
                bom = new byte[2];
                byteBuffer.Peek(bom);
                if (bom[0] == (byte)0xFE && bom[1] == (byte)0xFF) {
                    temp = Encoding.BigEndianUnicode;
                    offset = 2;
                }
                if (bom[0] == (byte)0xFF && bom[1] == (byte)0xFE) {
                    offset = 2;
                }
            }
            if (encoding.CodePage == Encoding.UTF32.CodePage && byteBuffer.Remaining() >= 4) {
                bom = new byte[4];
                byteBuffer.Peek(bom);
                if (bom[0] == 0x00 && bom[1] == 0x00 && bom[2] == (byte) 0xFE && bom[3] == (byte) 0xFF) {
                    temp = Encoding.GetEncoding("utf-32be");
                    offset = 4;
                }
                if (bom[0] == (byte)0xFF && bom[1] == (byte)0xFE && bom[2] == 0x00 && bom[3] == 0x00) {
                    offset = 4;
                }
            }
            if (temp == null) {
                temp = Encoding.GetEncoding(encoding.CodePage, EncoderFallback.ReplacementFallback, DecoderFallback.ReplacementFallback);
            }
            var result = temp.GetString(byteBuffer.buffer, byteBuffer.position + offset, byteBuffer.Remaining() - offset);
            byteBuffer.Position(byteBuffer.buffer.Length - 1);
            return result;
        }

        public static String ToExternalForm(this Uri u) {
            /*
            // pre-compute length of StringBuffer
            int len = u.Scheme.Length + 1;
            if (!String.IsNullOrEmpty(u.Authority))
                len += 2 + u.Authority.Length;
            if (!String.IsNullOrEmpty(u.AbsolutePath))
                len += u.AbsolutePath.Length - (u.AbsolutePath.EndsWith("/") ? 1 : 0);
            if (!String.IsNullOrEmpty(u.Query))
                len += u.Query.Length;
            if (!String.IsNullOrEmpty(u.Fragment))
                len += 1 + u.Fragment.Length;

            StringBuilder result = new StringBuilder(len);
            result.Append(u.Scheme);
            result.Append(":");
            if (!String.IsNullOrEmpty(u.Authority)) {
                result.Append("//");
                result.Append(u.Authority);
            }
            if (!String.IsNullOrEmpty(u.AbsolutePath)) {
                var path = u.AbsolutePath;
                if (path.EndsWith("/"))
                    path = path.Substring(0, path.Length - 1);
                result.Append(path);
            }
            if (!String.IsNullOrEmpty(u.Query))
                result.Append(u.Query);
            if (!String.IsNullOrEmpty(u.Fragment))
                result.Append(u.Fragment);
            return result.ToString();
            */
            return Uri.UnescapeDataString(u.AbsoluteUri);
        }

        public static bool CanEncode(this Encoding encoding, char c) {
            return encoding.CanEncode(c.ToString());
        }

        public static bool CanEncode(this Encoding encoding, String chars) {
            byte[] src = Encoding.Unicode.GetBytes(chars);
            return encoding.CanEncode(src);
        }

        public static bool CanEncode(this Encoding encoding, byte[] src) {
            try {
                byte[] dest = Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(encoding.CodePage, new EncoderExceptionFallback(), new DecoderExceptionFallback()), src);
            }
            catch (EncoderFallbackException) {
                return false;
            }
            return true;
        }

        public static Stream GetResourceAsStream(this Type type, string filename) {
            Stream s = ResourceUtil.GetResourceStream(type.Namespace.ToLower(CultureInfo.InvariantCulture) + "." + filename, type);
            if (s == null) {
                throw new IOException();
            }
            return s;
        }

        public static int CodePointAt(this String str, int index) {
            return char.ConvertToUtf32(str, index);
        }

        public static StringBuilder AppendCodePoint(this StringBuilder sb, int codePoint) {
            return sb.Append(char.ConvertFromUtf32(codePoint));
        }

        public static String JSubstring(this String str, int beginIndex, int endIndex)
        {
            return str.Substring(beginIndex, endIndex - beginIndex);
        }

        public static void JReset(this MemoryStream stream)
        {
            stream.Position = 0;
        }

        public static StringBuilder Delete(this StringBuilder sb, int beginIndex, int endIndex) {
            return sb.Remove(beginIndex, endIndex - beginIndex);
        }

        public static void Write(this Stream stream, int value)
        {
            stream.WriteByte((byte)value);
        }

        public static int Read(this Stream stream)
        {
            return stream.ReadByte();
        }

        public static int Read(this Stream stream, byte[] buffer)
        {
            int size = stream.Read(buffer, 0, buffer.Length);
            return size == 0 ? -1 : size;
        }

        public static int JRead(this Stream stream, byte[] buffer, int offset, int count)
        {
            int result = stream.Read(buffer, offset, count);
            return result == 0 ? -1 : result;
        }

        public static void Write(this Stream stream, byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }

        public static byte[] GetBytes(this String str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        public static byte[] GetBytes(this String str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static byte[] GetBytes(this String str, String encodingName)
        {
            return Encoding.GetEncoding(encodingName).GetBytes(str);
        }

        public static ByteBuffer Encode(this Encoding encoding, String str) {
            return ByteBuffer.Wrap(encoding.GetBytes(str));
        }

        public static long Seek(this FileStream fs, long offset)
        {
            return fs.Seek(offset, SeekOrigin.Begin);
        }

        public static long Skip(this Stream s, long n)
        {
            s.Seek(n, SeekOrigin.Current);
            return n;
        }

        public static List<T> SubList<T>(this IList<T> list, int fromIndex, int toIndex)
        {
            return ((List<T>)list).GetRange(fromIndex, toIndex - fromIndex);
        }

        public static void GetChars(this StringBuilder sb, int srcBegin, int srcEnd, char[] dst, int dstBegin)
        {
            sb.CopyTo(srcBegin, dst, dstBegin, srcEnd - srcBegin);
        }

        public static String[] Split(this String str, String regex)
        {
            return str.Split(regex.ToCharArray());
        }

        public static void AddAll<T>(this ICollection<T> c, IEnumerable<T> collectionToAdd) {
            foreach (T o in collectionToAdd)
            {
                c.Add(o);
            }
        }

        public static T[] ToArray<T>(this ICollection<T> col) {
            T[] result = new T[col.Count];
            return col.ToArray<T>(result);
        }

        public static T[] ToArray<T>(this ICollection<T> col, T[] toArray)
        {
            T[] r;
            int colSize = col.Count;
            if (colSize <= toArray.Length)
            {
                col.CopyTo(toArray, 0);
                if (colSize != toArray.Length)
                {
                    toArray[colSize] = default(T);
                }
                r = toArray;
            }
            else
            {
                r = new T[colSize];
                col.CopyTo(r, 0);
            }

            return r;
        }

        public static bool RemoveAll<T>(this ICollection<T> set, ICollection<T> c) {
            bool modified = false;
            foreach (var item in c) {
                if (set.Remove(item)) modified = true;
            }
            return modified;
        }

        public static TValue JRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            TValue value;
            dictionary.TryGetValue(key, out value);
            dictionary.Remove(key);

            return value;
        }

        public static bool Matches(this String str, String regex) {
            return Regex.IsMatch(str, regex);
        }

        public static String ReplaceFirst(this String input, String pattern, String replacement) {
            var regex = new Regex(pattern);
            return  regex.Replace(input, replacement, 1);
        }

        public static bool EqualsIgnoreCase(this String str, String anotherString) {
            return String.Equals(str, anotherString, StringComparison.OrdinalIgnoreCase);
        }

        public static void AddAll<TKey, TValue>(this IDictionary<TKey, TValue> c, IDictionary<TKey, TValue> collectionToAdd)
        {
            foreach (KeyValuePair<TKey, TValue> pair in collectionToAdd)
            {
                c[pair.Key] = pair.Value;
            }
        }

        public static void AddAll<T>(this IList<T> list, int index, IList<T> c)
        {
            for (int i = c.Count - 1; i >= 0; i--)
            {
                list.Insert(index, c[i]);
            }
        }

        public static bool IsEmpty<T>(this ICollection<T> c) {
            return c.Count == 0;
        }

        public static bool IsEmpty<T>(this IList<T> l)
        {
            return l.Count == 0;
        }

        public static void Add<T>(this IList<T> list, int index, T elem)
        {
            list.Insert(index, elem);
        }

        public static bool Add<T>(this LinkedList<T> list, T elem) {
            list.AddLast(elem);
            return true;
        }

        public static T JRemove<T>(this LinkedList<T> list) {
            T head = list.First.Value;
            list.RemoveFirst();
            return head;
        }

        public static T JRemoveAt<T>(this IList<T> list, int index)
        {
            T value = list[index];
            list.RemoveAt(index);

            return value;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> col, TKey key)
        {
            TValue value = default(TValue);
            if (key != null)
            {
                col.TryGetValue(key, out value);
            }

            return value;
        }

        public static TValue Put<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary[key] = value;
            return value;
        }

        public static bool Contains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.ContainsKey(key);
        }
    }
}
