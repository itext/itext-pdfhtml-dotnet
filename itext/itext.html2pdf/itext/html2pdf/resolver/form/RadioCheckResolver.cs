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
using iText.Forms.Form;
using iText.Forms.Form.Element;

namespace iText.Html2pdf.Resolver.Form {
    /// <summary>Utilities class that resolves radio's checked property value.</summary>
    public class RadioCheckResolver {
        /// <summary>A map containing all the radio group names, mapped to the corresponded checked radio field.</summary>
        private readonly IDictionary<String, Radio> @checked = new Dictionary<String, Radio>();

        /// <summary>
        /// Creates a new
        /// <see cref="RadioCheckResolver"/>
        /// instance.
        /// </summary>
        public RadioCheckResolver() {
        }

        /// <summary>Checks the radio field.</summary>
        /// <param name="radioGroupName">the name of the radio group the radio field belongs to</param>
        /// <param name="checkedField">the radio field to be checked</param>
        public virtual void CheckField(String radioGroupName, Radio checkedField) {
            Radio previouslyChecked = @checked.Get(radioGroupName);
            if (null != previouslyChecked) {
                previouslyChecked.DeleteOwnProperty(FormProperty.FORM_FIELD_CHECKED);
            }
            @checked.Put(radioGroupName, checkedField);
        }
    }
}
