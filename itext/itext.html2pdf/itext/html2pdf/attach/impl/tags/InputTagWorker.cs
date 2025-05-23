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
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Forms.Form;
using iText.Forms.Form.Element;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>input</c>
    /// element.
    /// </summary>
    public class InputTagWorker : ITagWorker, IDisplayAware {
        private static readonly Regex NUMBER_INPUT_ALLOWED_VALUES = iText.Commons.Utils.StringUtil.RegexCompile("^(((-?[0-9]+)(\\.[0-9]+)?)|(-?\\.[0-9]+))$"
            );

        private static int radioNameIdx = 0;

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
                    ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.InputTagWorker));
                    logger.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.INPUT_TYPE_IS_INVALID, inputType));
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
                // Default html2pdf input field appearance differs from the default one for form fields.
                // That's why we need to get rid of several properties we set by default during InputField instance creation.
                formElement.DeleteOwnProperty(Property.BOX_SIZING);
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
                formElement.SetProperty(FormProperty.FORM_FIELD_VALUE, value);
                formElement.SetProperty(FormProperty.FORM_FIELD_SIZE, size);
                if (AttributeConstants.PASSWORD.Equals(inputType)) {
                    formElement.SetProperty(FormProperty.FORM_FIELD_PASSWORD_FLAG, true);
                }
            }
            else {
                if (AttributeConstants.SUBMIT.Equals(inputType) || AttributeConstants.BUTTON.Equals(inputType)) {
                    formElement = new Button(name).SetSingleLineValue(value);
                }
                else {
                    if (AttributeConstants.CHECKBOX.Equals(inputType)) {
                        CheckBox cb = new CheckBox(name);
                        String @checked = element.GetAttribute(AttributeConstants.CHECKED);
                        // so in the previous implementation the width was 8.25 and the borders .75,
                        // but the borders got drawn on the outside of the box, so the actual size was 9.75
                        // because 8.25 + 2 * .75 = 9.75
                        float widthWithBordersOnTheInside = 9.75f;
                        float defaultBorderWith = .75f;
                        cb.SetSize(widthWithBordersOnTheInside);
                        cb.SetBorder(new SolidBorder(ColorConstants.DARK_GRAY, defaultBorderWith));
                        cb.SetBackgroundColor(ColorConstants.WHITE);
                        // has attribute == is checked
                        cb.SetChecked(@checked != null);
                        formElement = cb;
                    }
                    else {
                        if (AttributeConstants.RADIO.Equals(inputType)) {
                            String radioGroupName = element.GetAttribute(AttributeConstants.NAME);
                            if (radioGroupName == null || String.IsNullOrEmpty(radioGroupName)) {
                                ++radioNameIdx;
                                radioGroupName = "radio" + radioNameIdx;
                            }
                            Radio radio = new Radio(name, radioGroupName);
                            // Gray circle border
                            Border border = new SolidBorder(1);
                            border.SetColor(ColorConstants.LIGHT_GRAY);
                            radio.SetBorder(border);
                            String @checked = element.GetAttribute(AttributeConstants.CHECKED);
                            if (null != @checked) {
                                context.GetRadioCheckResolver().CheckField(radioGroupName, radio);
                                radio.SetChecked(true);
                            }
                            formElement = radio;
                        }
                        else {
                            ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.InputTagWorker));
                            logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.INPUT_TYPE_IS_NOT_SUPPORTED, inputType
                                ));
                        }
                    }
                }
            }
            if (formElement != null) {
                formElement.SetProperty(FormProperty.FORM_FIELD_FLATTEN, !context.IsCreateAcroForm());
                ((IAccessibleElement)formElement).GetAccessibilityProperties().SetLanguage(lang);
                formElement.SetProperty(FormProperty.FORM_CONFORMANCE_LEVEL, context.GetConformance());
                context.GetDIContainer().GetInstance<AlternateDescriptionResolver>().Resolve((IAccessibleElement)formElement
                    , element);
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

//\cond DO_NOT_DOCUMENT
        internal static String PreprocessInputValue(String value, String inputType) {
            if (AttributeConstants.NUMBER.Equals(inputType) && value != null && !iText.Commons.Utils.Matcher.Match(NUMBER_INPUT_ALLOWED_VALUES
                , value).Matches()) {
                value = "";
            }
            return value;
        }
//\endcond
    }
}
