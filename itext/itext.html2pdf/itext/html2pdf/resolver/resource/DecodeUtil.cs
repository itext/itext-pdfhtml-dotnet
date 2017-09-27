/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using System.Text;
using iText.Html2pdf.Exceptions;

namespace iText.Html2pdf.Resolver.Resource
{
    /// <summary>
    /// Utilities class to decode HTML strings to a strings in a specific encoding.
    /// </summary>
    [System.ObsoleteAttribute(@"Will be removed in iText 7.1")]
    public class DecodeUtil
    {

        /// <summary>
        /// The default encoding ("UTF-8").
        /// </summary>
        internal static String dfltEncName = "UTF-8";

        /// <summary>
        /// The default uri scheme ("file").
        /// </summary>
        internal static String dftUriScheme = "file";

        /// <summary>
        /// Decode a <see cref="String"/> to a <see cref="String"/> using the default uri scheme
        /// and default encoding.
        /// </summary>
        /// <param name="s">the string to decode</param>
        /// <returns>the decoded string</returns>
        public static String Decode(String s)
        {
            return Decode(s, dftUriScheme);
        }

        /// <summary>
        /// Decode a <see cref="String"/> to a <see cref="String"/> using the provided scheme
        /// and the default encoding.
        /// </summary>
        /// <param name="s">the string to decode</param>
        /// <param name="scheme">the uri scheme</param>
        /// <returns>the decoded string</returns>
        public static String Decode(String s, String scheme)
        {
            return Decode(s, scheme, dfltEncName);
        }

        /// <summary>
        /// Decodes a <see cref="String"/> to a <see cref="String"/> using a specific encoding.
        /// </summary>
        /// <param name="s">the string to decode</param>
        /// <param name="scheme">the uri scheme</param>
        /// <param name="enc">the encoding</param>
        /// <returns>the decoded string</returns>
        public static String Decode(String s, String scheme, String enc)
        {
            bool needToChange = false;
            int numChars = s.Length;
            StringBuilder sb = new StringBuilder(numChars > 500 ? numChars / 2 : numChars);
            int i = 0;
            if (null == enc || enc.Length == 0)
            {
                throw new Html2PdfException(Html2PdfException.UnsupportedEncodingException);
            }
            char c;
            byte[] bytes = null;
            while (i < numChars)
            {
                c = s[i];
                if ('%' == c)
                {
                    // (numChars-i)/3 is an upper bound for the number
                    // of remaining bytes
                    if (bytes == null)
                    {
                        bytes = new byte[(numChars - i) / 3];
                    }
                    int pos = 0;
                    while (((i + 2) < numChars) && (c == '%'))
                    {
                        int v;
                        try
                        {
                            v = System.Convert.ToInt32(s.JSubstring(i + 1, i + 3), 16);
                        }
                        catch (FormatException)
                        {
                            v = -1;
                        }
                        if (v < 0)
                        {
                            i++;
                            break;
                        }
                        else
                        {
                            bytes[pos++] = (byte)v;
                            i += 3;
                            if (i < numChars)
                            {
                                c = s[i];
                            }
                        }
                    }
                    if ((i < numChars) && (c == '%'))
                    {
                        bytes[pos++] = (byte)c;
                    }
                    try
                    {
                        sb.Append(iText.IO.Util.JavaUtil.GetStringForBytes(bytes, 0, pos, enc));
                    }
                    catch (ArgumentException)
                    {
                        throw new Html2PdfException(Html2PdfException.UnsupportedEncodingException);
                    }
                    needToChange = true;
                }
                else
                {
                    sb.Append(c);
                    i++;
                    if ("http".Equals(scheme) || "https".Equals(scheme))
                    {
                        if ('?' == c)
                        {
                            break;
                        }
                        else if ('#' == c)
                        {
                            sb.Append(c);
                            needToChange = true;
                        }

                    }
                }

            }

            if (needToChange && i + 1 < numChars)
            {
                sb.Append(s.Substring(i + 1));
            }

            return (needToChange ? sb.ToString() : s);
        }
    }
}
