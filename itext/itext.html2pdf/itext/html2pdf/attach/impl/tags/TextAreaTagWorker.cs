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
            textArea.SetProperty(FormProperty.FORM_ACCESSIBILITY_LANGUAGE, element.GetAttribute(AttributeConstants.LANG
                ));
            // Default html2pdf text area appearance differs from the default one for form fields.
            // That's why we need to get rid of all properties we set by default during TextArea instance creation.
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
