/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
using System;
using System.Collections;
using System.IO;
using System.Text;
using iText.Html2pdf.Exceptions;

namespace iText.Html2pdf.Resolver.Resource {
    internal class EncodeUtil {
        internal static BitArray dontNeedEncoding;

        internal const int caseDiff = ('a' - 'A');

        internal static String dfltEncName = "UTF-8";

        static EncodeUtil() {
            dontNeedEncoding = new BitArray(256);
            int i;
            for (i = 'a'; i <= 'z'; i++) {
                dontNeedEncoding.Set(i, true);
            }
            for (i = 'A'; i <= 'Z'; i++) {
                dontNeedEncoding.Set(i, true);
            }
            for (i = '0'; i <= '9'; i++) {
                dontNeedEncoding.Set(i, true);
            }
            dontNeedEncoding.Set('-', true);
            dontNeedEncoding.Set('_', true);
            dontNeedEncoding.Set('.', true);
            dontNeedEncoding.Set(':', true);
            dontNeedEncoding.Set('*', true);
            dontNeedEncoding.Set('/', true);
            dontNeedEncoding.Set('?', true);
            dontNeedEncoding.Set('"', true);
            dontNeedEncoding.Set('<', true);
            dontNeedEncoding.Set('>', true);
            dontNeedEncoding.Set('|', true);
            dontNeedEncoding.Set('\\', true);
        }

        public static String Encode(String s) {
            return Encode(s, dfltEncName);
        }

        public static String Encode(String s, String enc) {
            bool needToChange = false;
            StringBuilder @out = new StringBuilder(s.Length);
            Encoding charset;
            BinaryWriter charArrayWriter = new BinaryWriter(new MemoryStream());
            if (enc == null) {
                throw new Html2PdfException(Html2PdfException.UnsupportedEncodingException);
            }
            charset = iText.IO.Util.EncodingUtil.GetEncoding(enc);

            for (int i = 0; i < s.Length; ) {
                int c = (int)s[i];
                if (dontNeedEncoding.Get(c)) {
                    @out.Append((char)c);
                    i++;
                }
                else {
                    int numOfChars = 0;
                    do {
                        // convert to external encoding before hex conversion
                        charArrayWriter.Write((char)c);
                        numOfChars++;
                        /*
                        * If this character represents the start of a Unicode
                        * surrogate pair, then pass in two characters. It's not
                        * clear what should be done if a bytes reserved in the
                        * surrogate pairs range occurs outside of a legal
                        * surrogate pair. For now, just treat it as if it were
                        * any other character.
                        */
                        if (c >= 0xD800 && c <= 0xDBFF) {
                            /*
                            System.out.println(Integer.toHexString(c)
                            + " is high surrogate");
                            */
                            if ((i + 1) < s.Length) {
                                int d = (int)s[i + 1];
                                /*
                                System.out.println("\tExamining "
                                + Integer.toHexString(d));
                                */
                                if (d >= 0xDC00 && d <= 0xDFFF) {
                                    /*
                                    System.out.println("\t"
                                    + Integer.toHexString(d)
                                    + " is low surrogate");
                                    */
                                    charArrayWriter.Write(d);
                                    i++;
                                    numOfChars++;
                                }
                            }
                        }
                        i++;
                    }
                    while (i < s.Length && !dontNeedEncoding.Get((c = (int)s[i])));
                    BinaryReader binReader = new BinaryReader(charArrayWriter.BaseStream);
                    char[] chars = binReader.ReadChars(numOfChars);
                    String str = new String(chars);
                    byte[] ba = str.GetBytes(charset);
                    for (int j = 0; j < ba.Length; j++) {
                        @out.Append('%');
                        int ch = Convert.ToInt32(((ba[j] >> 4) & 0xF).ToString(), 16);
                        // converting to use uppercase letter as part of
                        // the hex value if ch is a letter.
                        if (char.IsLetter((char)ch)) {
                            ch -= (char)caseDiff;
                        }
                        @out.Append(ch);
                        ch = Convert.ToInt32(((ba[j] >> 4) & 0xF).ToString(), 16);
                        if (char.IsLetter((char)ch)) {
                            ch -= (char)caseDiff;
                        }
                        @out.Append(ch);
                    }
                    charArrayWriter.Flush();
                    needToChange = true;
                }
            }
            return (needToChange ? @out.ToString() : s);
        }
    }
}
