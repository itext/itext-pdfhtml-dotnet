/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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

namespace iText.Html2pdf.Util {
    /// <summary>
    /// Class that allows to map keys (html tags, css attributes) to the
    /// corresponding tag processors (a tag worker or a CSS applier).
    /// </summary>
    public class TagProcessorMapping<T> {
        /// <summary>The default display key.</summary>
        private static String DEFAULT_DISPLAY_KEY = "defaultKey";

        /// <summary>The actual mapping.</summary>
        private IDictionary<String, IDictionary<String, T>> mapping;

        /// <summary>
        /// Creates a new
        /// <see cref="TagProcessorMapping{T}"/>
        /// instance.
        /// </summary>
        public TagProcessorMapping() {
            mapping = new Dictionary<String, IDictionary<String, T>>();
        }

        /// <summary>Add a new tag to the map.</summary>
        /// <param name="tag">the key</param>
        /// <param name="mapping">the class instance that maps to the tag</param>
        public virtual void PutMapping(String tag, T mapping) {
            EnsureMappingExists(tag).Put(DEFAULT_DISPLAY_KEY, mapping);
        }

        /// <summary>Add a new tag to the map.</summary>
        /// <param name="tag">the key</param>
        /// <param name="display">the display value</param>
        /// <param name="mapping">the class instance that maps to the tag</param>
        public virtual void PutMapping(String tag, String display, T mapping) {
            EnsureMappingExists(tag).Put(display, mapping);
        }

        /// <summary>Gets the class that maps to a specific tag.</summary>
        /// <param name="tag">the key</param>
        /// <returns>the class that maps to the tag</returns>
        public virtual Object GetMapping(String tag) {
            return GetMapping(tag, DEFAULT_DISPLAY_KEY);
        }

        /// <summary>Gets the class that maps to a specific tag.</summary>
        /// <param name="tag">the key</param>
        /// <param name="display">the display value</param>
        /// <returns>the class that maps to the tag</returns>
        public virtual Object GetMapping(String tag, String display) {
            IDictionary<String, T> tagMapping = mapping.Get(tag);
            if (tagMapping == null) {
                return null;
            }
            else {
                return tagMapping.Get(display);
            }
        }

        /// <summary>Ensure that a mapping for a specific key exists.</summary>
        /// <param name="tag">the key</param>
        /// <returns>the map</returns>
        private IDictionary<String, T> EnsureMappingExists(String tag) {
            if (mapping.ContainsKey(tag)) {
                return mapping.Get(tag);
            }
            else {
                IDictionary<String, T> tagMapping = new Dictionary<String, T>();
                mapping.Put(tag, tagMapping);
                return tagMapping;
            }
        }
    }
}
