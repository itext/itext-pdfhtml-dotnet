using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using iText.IO.Util;

namespace Org.Jsoup
{
    internal static class PortingExtension
    {
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
            return ResourceUtil.GetResourceStream(filename, type);
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
