/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
