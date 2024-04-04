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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Forms.Form;
using iText.Forms.Form.Element;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>select</c>
    /// element.
    /// </summary>
    public class SelectTagWorker : ITagWorker, IDisplayAware {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.SelectTagWorker
            ));

        /// <summary>The form element.</summary>
        private AbstractSelectField selectElement;

        /// <summary>The display.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="SelectTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public SelectTagWorker(IElementNode element, ProcessorContext context) {
            String name = context.GetFormFieldNameResolver().ResolveFormName(element.GetAttribute(AttributeConstants.NAME
                ));
            bool multipleAttr = element.GetAttribute(AttributeConstants.MULTIPLE) != null;
            int? sizeAttr = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.SIZE));
            int size = GetSelectSize(sizeAttr, multipleAttr);
            if (size > 1 || multipleAttr) {
                selectElement = new ListBoxField(name, size, multipleAttr);
                // Remove some properties which are set in ListBoxField constructor
                selectElement.DeleteOwnProperty(Property.PADDING_LEFT);
                selectElement.DeleteOwnProperty(Property.PADDING_RIGHT);
                selectElement.DeleteOwnProperty(Property.PADDING_TOP);
                selectElement.DeleteOwnProperty(Property.PADDING_BOTTOM);
            }
            else {
                selectElement = new ComboBoxField(name);
            }
            String lang = element.GetAttribute(AttributeConstants.LANG);
            selectElement.GetAccessibilityProperties().SetLanguage(lang);
            selectElement.SetProperty(FormProperty.FORM_FIELD_FLATTEN, !context.IsCreateAcroForm());
            if (context.GetConformanceLevel() != null) {
                selectElement.SetProperty(FormProperty.FORM_CONFORMANCE_LEVEL, context.GetConformanceLevel());
            }
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return content == null || String.IsNullOrEmpty(content.Trim());
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            if (childTagWorker is OptionTagWorker || childTagWorker is OptGroupTagWorker) {
                if (childTagWorker.GetElementResult() is IBlockElement) {
                    IBlockElement blockElement = (IBlockElement)childTagWorker.GetElementResult();
                    String label = blockElement.GetProperty<String>(FormProperty.FORM_FIELD_LABEL);
                    SelectFieldItem item = new SelectFieldItem(label, blockElement);
                    selectElement.AddOption(item);
                    bool? isFlattenFromProperty = selectElement.GetProperty<bool?>(FormProperty.FORM_FIELD_FLATTEN);
                    if (childTagWorker is OptGroupTagWorker && !true.Equals(isFlattenFromProperty)) {
                        LOGGER.LogWarning(Html2PdfLogMessageConstant.OPTGROUP_NOT_SUPPORTED_IN_INTERACTIVE_SELECT);
                    }
                    return true;
                }
            }
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return selectElement;
        }

        public virtual String GetDisplay() {
            return display;
        }

        private int GetSelectSize(int? size, bool multiple) {
            if (size != null && size > 0) {
                return (int)size;
            }
            return multiple ? 4 : 1;
        }
    }
}
