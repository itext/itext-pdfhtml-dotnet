/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Resolver.Form {
    /// <summary>Utilities class that resolves form field names.</summary>
    public class FormFieldNameResolver {
        /// <summary>The default base name of a field: "Field".</summary>
        private const String DEFAULT_NAME = "Field";

        /// <summary>The separator between a field name and an index.</summary>
        private const String NAME_COUNT_SEPARATOR = "_";

        /// <summary>A map containing all the base field names, mapped to the current index.</summary>
        private readonly IDictionary<String, int?> names = new Dictionary<String, int?>();

        /// <summary>
        /// Creates a new
        /// <see cref="FormFieldNameResolver"/>
        /// instance.
        /// </summary>
        public FormFieldNameResolver() {
        }

        /// <summary>Resolves a proposed field name to a valid field name.</summary>
        /// <param name="name">the proposed name</param>
        /// <returns>the valid name</returns>
        public virtual String ResolveFormName(String name) {
            name = NormalizeString(name);
            if (String.IsNullOrEmpty(name)) {
                return ResolveNormalizedFormName(DEFAULT_NAME);
            }
            else {
                return ResolveNormalizedFormName(name);
            }
        }

        /// <summary>Resets the map containing all the field names.</summary>
        public virtual void Reset() {
            names.Clear();
        }

        /// <summary>Normalizes a field name.</summary>
        /// <param name="s">the proposed field name</param>
        /// <returns>the normalized name</returns>
        private String NormalizeString(String s) {
            return s != null ? s.Trim().Replace(".", "") : "";
        }

        /// <summary>Resolves a normalized form name.</summary>
        /// <param name="name">the proposed name</param>
        /// <returns>the resolved name</returns>
        private String ResolveNormalizedFormName(String name) {
            int separatorIndex = name.LastIndexOf(NAME_COUNT_SEPARATOR);
            int? nameIndex = null;
            if (separatorIndex != -1 && separatorIndex < name.Length) {
                String numberString = name.Substring(separatorIndex + 1);
                nameIndex = CssDimensionParsingUtils.ParseInteger(numberString);
                //Treat number as index only in case it is positive
                if (nameIndex != null && nameIndex > 0) {
                    name = name.JSubstring(0, separatorIndex);
                }
            }
            int? savedIndex = names.Get(name);
            int indexToSave = savedIndex != null ? savedIndex.Value + 1 : 0;
            if (nameIndex != null && indexToSave < nameIndex.Value) {
                indexToSave = nameIndex.Value;
            }
            names.Put(name, indexToSave);
            return indexToSave == 0 ? name : name + NAME_COUNT_SEPARATOR + indexToSave.ToString();
        }
    }
}
