/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using System.Text.RegularExpressions;

namespace iText.Html2pdf.Css.Parse {
    /// <summary>Internal class not for public use.</summary>
    /// <remarks>Internal class not for public use. Its API may change.</remarks>
    internal class CssSelectorParserMatch {
        private Regex pattern;

        private Match matcher;

        private String source;

        private int offset;

        public CssSelectorParserMatch(String source, Regex pattern) {
            this.source = source;
            this.matcher = iText.IO.Util.StringUtil.Match(pattern, source);
            this.pattern = pattern;
            this.offset = 0;
        }

        public virtual int GetIndex() {
            return offset + matcher.Index;
        }

        public virtual String GetValue() {
            return iText.IO.Util.StringUtil.Group(matcher, 0);
        }

        public virtual String GetSource() {
            return source;
        }

        public virtual bool Success() {
            return matcher.Success;
        }

        public virtual void Next() {
            this.matcher = matcher.NextMatch();
        }

        public virtual void Next(int startIndex) {
            offset = startIndex;
            this.matcher = iText.IO.Util.StringUtil.Match(pattern, source.Substring(startIndex));
        }
    }
}
