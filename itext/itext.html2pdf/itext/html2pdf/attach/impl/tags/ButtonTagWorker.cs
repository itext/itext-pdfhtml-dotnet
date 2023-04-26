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
using System.Text;
using iText.Forms.Form;
using iText.Forms.Form.Element;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for a button element.</summary>
    public class ButtonTagWorker : DivTagWorker {
        /// <summary>The Constant DEFAULT_BUTTON_NAME.</summary>
        private const String DEFAULT_BUTTON_NAME = "Button";

        /// <summary>The button.</summary>
        private IFormField formField;

        /// <summary>The lang attribute value.</summary>
        private String lang;

        private StringBuilder fallbackContent = new StringBuilder();

        private String name;

        private bool flatten;

        private bool hasChildren = false;

        /// <summary>
        /// Creates a new
        /// <see cref="ButtonTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ButtonTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
            String name = element.GetAttribute(AttributeConstants.ID);
            if (name == null) {
                name = DEFAULT_BUTTON_NAME;
            }
            this.name = context.GetFormFieldNameResolver().ResolveFormName(name);
            flatten = !context.IsCreateAcroForm();
            lang = element.GetAttribute(AttributeConstants.LANG);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override bool ProcessContent(String content, ProcessorContext context) {
            fallbackContent.Append(content);
            return base.ProcessContent(content, context);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            hasChildren = true;
            return base.ProcessTagChild(childTagWorker, context);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public override IPropertyContainer GetElementResult() {
            if (formField == null) {
                if (hasChildren) {
                    Button button = new Button(name);
                    button.SetProperty(FormProperty.FORM_ACCESSIBILITY_LANGUAGE, lang);
                    Div div = (Div)base.GetElementResult();
                    foreach (IElement element in div.GetChildren()) {
                        if (element is IAccessibleElement) {
                            AccessiblePropHelper.TrySetLangAttribute((IAccessibleElement)element, lang);
                        }
                        if (element is IBlockElement) {
                            button.Add((IBlockElement)element);
                        }
                        else {
                            if (element is Image) {
                                button.Add((Image)element);
                            }
                        }
                    }
                    div.GetChildren().Clear();
                    formField = button;
                }
                else {
                    Button inputButton = new Button(name);
                    inputButton.SetProperty(FormProperty.FORM_ACCESSIBILITY_LANGUAGE, lang);
                    inputButton.SetValue(fallbackContent.ToString().Trim());
                    formField = inputButton;
                }
            }
            formField.SetProperty(FormProperty.FORM_FIELD_FLATTEN, flatten);
            return formField;
        }
    }
}
