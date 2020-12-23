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
using System.Text.RegularExpressions;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.IO.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>input</c>
    /// element.
    /// </summary>
    public class InputTagWorker : ITagWorker, IDisplayAware {
        private static readonly Regex NUMBER_INPUT_ALLOWED_VALUES = iText.IO.Util.StringUtil.RegexCompile("^(((-?[0-9]+)(\\.[0-9]+)?)|(-?\\.[0-9]+))$"
            );

        /// <summary>The form element.</summary>
        private IElement formElement;

        /// <summary>The display.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="InputTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public InputTagWorker(IElementNode element, ProcessorContext context) {
            String lang = element.GetAttribute(AttributeConstants.LANG);
            String inputType = element.GetAttribute(AttributeConstants.TYPE);
            if (!AttributeConstants.INPUT_TYPE_VALUES.Contains(inputType)) {
                if (null != inputType && 0 != inputType.Length) {
                    ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.InputTagWorker));
                    logger.Warn(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.INPUT_TYPE_IS_INVALID, inputType));
                }
                inputType = AttributeConstants.TEXT;
            }
            String value = element.GetAttribute(AttributeConstants.VALUE);
            String name = context.GetFormFieldNameResolver().ResolveFormName(element.GetAttribute(AttributeConstants.NAME
                ));
            // Default input type is text
            if (inputType == null || AttributeConstants.TEXT.Equals(inputType) || AttributeConstants.EMAIL.Equals(inputType
                ) || AttributeConstants.PASSWORD.Equals(inputType) || AttributeConstants.NUMBER.Equals(inputType)) {
                int? size = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.SIZE));
                formElement = new InputField(name);
                value = PreprocessInputValue(value, inputType);
                // process placeholder instead
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
                    ((InputField)formElement).SetPlaceholder(paragraph.SetMargin(0));
                }
                formElement.SetProperty(Html2PdfProperty.FORM_FIELD_VALUE, value);
                formElement.SetProperty(Html2PdfProperty.FORM_FIELD_SIZE, size);
                if (AttributeConstants.PASSWORD.Equals(inputType)) {
                    formElement.SetProperty(Html2PdfProperty.FORM_FIELD_PASSWORD_FLAG, true);
                }
            }
            else {
                if (AttributeConstants.SUBMIT.Equals(inputType) || AttributeConstants.BUTTON.Equals(inputType)) {
                    formElement = new InputButton(name);
                    formElement.SetProperty(Html2PdfProperty.FORM_FIELD_VALUE, value);
                }
                else {
                    if (AttributeConstants.CHECKBOX.Equals(inputType)) {
                        formElement = new CheckBox(name);
                        String @checked = element.GetAttribute(AttributeConstants.CHECKED);
                        if (null != @checked) {
                            formElement.SetProperty(Html2PdfProperty.FORM_FIELD_CHECKED, @checked);
                        }
                    }
                    else {
                        // has attribute == is checked
                        if (AttributeConstants.RADIO.Equals(inputType)) {
                            formElement = new Radio(name);
                            String radioGroupName = element.GetAttribute(AttributeConstants.NAME);
                            formElement.SetProperty(Html2PdfProperty.FORM_FIELD_VALUE, radioGroupName);
                            String @checked = element.GetAttribute(AttributeConstants.CHECKED);
                            if (null != @checked) {
                                context.GetRadioCheckResolver().CheckField(radioGroupName, (Radio)formElement);
                                formElement.SetProperty(Html2PdfProperty.FORM_FIELD_CHECKED, @checked);
                            }
                        }
                        else {
                            // has attribute == is checked
                            ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.InputTagWorker));
                            logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.INPUT_TYPE_IS_NOT_SUPPORTED, inputType
                                ));
                        }
                    }
                }
            }
            if (formElement != null) {
                formElement.SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, !context.IsCreateAcroForm());
                formElement.SetProperty(Html2PdfProperty.FORM_ACCESSIBILITY_LANGUAGE, lang);
            }
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.IDisplayAware#getDisplay()
        */
        public virtual String GetDisplay() {
            return display;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return childTagWorker is PlaceholderTagWorker && null != ((InputField)formElement).GetPlaceholder();
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return formElement;
        }

        internal static String PreprocessInputValue(String value, String inputType) {
            if (AttributeConstants.NUMBER.Equals(inputType) && value != null && !iText.IO.Util.Matcher.Match(NUMBER_INPUT_ALLOWED_VALUES
                , value).Matches()) {
                value = "";
            }
            return value;
        }
    }
}
