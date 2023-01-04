/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
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
                    button.SetProperty(Html2PdfProperty.FORM_ACCESSIBILITY_LANGUAGE, lang);
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
                    InputButton inputButton = new InputButton(name);
                    inputButton.SetProperty(Html2PdfProperty.FORM_ACCESSIBILITY_LANGUAGE, lang);
                    inputButton.SetProperty(Html2PdfProperty.FORM_FIELD_VALUE, fallbackContent.ToString().Trim());
                    formField = inputButton;
                }
            }
            formField.SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, flatten);
            return formField;
        }
    }
}
