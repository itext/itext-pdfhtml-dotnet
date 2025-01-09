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
using iText.Forms.Form;
using iText.Forms.Form.Element;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>textarea</c>
    /// element.
    /// </summary>
    public class TextAreaTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The Constant DEFAULT_TEXTAREA_NAME.</summary>
        private const String DEFAULT_TEXTAREA_NAME = "TextArea";

        /// <summary>The text area.</summary>
        private TextArea textArea;

        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="TextAreaTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public TextAreaTagWorker(IElementNode element, ProcessorContext context) {
            String name = element.GetAttribute(AttributeConstants.ID);
            if (name == null) {
                name = DEFAULT_TEXTAREA_NAME;
            }
            name = context.GetFormFieldNameResolver().ResolveFormName(name);
            textArea = new TextArea(name);
            int? rows = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.ROWS));
            int? cols = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.COLS));
            textArea.SetProperty(FormProperty.FORM_FIELD_ROWS, rows);
            textArea.SetProperty(FormProperty.FORM_FIELD_COLS, cols);
            textArea.SetProperty(FormProperty.FORM_FIELD_FLATTEN, !context.IsCreateAcroForm());
            textArea.GetAccessibilityProperties().SetLanguage(element.GetAttribute(AttributeConstants.LANG));
            // Default html2pdf text area appearance differs from the default one for form fields.
            // That's why we need to get rid of several properties we set by default during TextArea instance creation.
            textArea.DeleteOwnProperty(Property.BOX_SIZING);
            String placeholder = element.GetAttribute(AttributeConstants.PLACEHOLDER);
            if (null != placeholder) {
                Paragraph paragraph;
                if (String.IsNullOrEmpty(placeholder)) {
                    paragraph = new Paragraph();
                }
                else {
                    if (String.IsNullOrEmpty(placeholder.Trim())) {
                        paragraph = new Paragraph("\u00A0");
                    }
                    else {
                        paragraph = new Paragraph(placeholder);
                    }
                }
                textArea.SetPlaceholder(paragraph.SetMargin(0));
            }
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            if (content.StartsWith("\r\n")) {
                content = content.Substring(2);
            }
            else {
                if (content.StartsWith("\r") || content.StartsWith("\n")) {
                    content = content.Substring(1);
                }
            }
            textArea.SetProperty(FormProperty.FORM_FIELD_VALUE, content);
            return true;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return childTagWorker is PlaceholderTagWorker && null != textArea.GetPlaceholder();
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return textArea;
        }

        public virtual String GetDisplay() {
            return display;
        }
    }
}
