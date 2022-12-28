/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
Authors: iText Software.

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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>select</c>
    /// element.
    /// </summary>
    public class SelectTagWorker : ITagWorker, IDisplayAware {
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
            }
            else {
                selectElement = new ComboBoxField(name);
            }
            String lang = element.GetAttribute(AttributeConstants.LANG);
            selectElement.SetProperty(Html2PdfProperty.FORM_ACCESSIBILITY_LANGUAGE, lang);
            selectElement.SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, !context.IsCreateAcroForm());
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
                    selectElement.AddOption((IBlockElement)childTagWorker.GetElementResult());
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
