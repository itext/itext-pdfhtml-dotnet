/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Forms.Form;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>optgroup</c>
    /// element.
    /// </summary>
    public class OptGroupTagWorker : DivTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="OptGroupTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public OptGroupTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
            String label = element.GetAttribute(AttributeConstants.LABEL);
            if (label == null || String.IsNullOrEmpty(label)) {
                label = "\u00A0";
            }
            GetElementResult().SetProperty(FormProperty.FORM_FIELD_LABEL, label);
            Paragraph p = new Paragraph(label).SetMargin(0);
            p.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            p.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);
            ((Div)GetElementResult()).Add(p);
        }

        public override bool ProcessContent(String content, ProcessorContext context) {
            return content == null || String.IsNullOrEmpty(content.Trim());
        }

        public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            if (childTagWorker is OptionTagWorker) {
                IPropertyContainer element = childTagWorker.GetElementResult();
                IPropertyContainer propertyContainer = GetElementResult();
                if (propertyContainer is IAccessibleElement) {
                    String lang = ((IAccessibleElement)propertyContainer).GetAccessibilityProperties().GetLanguage();
                    AccessiblePropHelper.TrySetLangAttribute((Div)childTagWorker.GetElementResult(), lang);
                }
                return AddBlockChild((IElement)element);
            }
            else {
                return base.ProcessTagChild(childTagWorker, context);
            }
        }
    }
}
