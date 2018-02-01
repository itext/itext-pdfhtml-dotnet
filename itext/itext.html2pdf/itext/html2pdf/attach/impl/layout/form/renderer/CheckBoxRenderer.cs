/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using Common.Logging;
using iText.Forms;
using iText.Forms.Fields;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// The
    /// <see cref="AbstractOneLineTextFieldRenderer"/>
    /// implementation for checkboxes.
    /// </summary>
    public class CheckBoxRenderer : AbstractOneLineTextFieldRenderer {
        public static readonly Color CHECKBOX_BORDER_COLOR = ColorConstants.DARK_GRAY;

        private const float CHECKBOX_BORDER_WIDTH = 0.75f;

        private const float CHECKBOX_FONT_SIZE = 6f;

        private const float CHECKBOX_HEIGHT = 8.25f;

        private const float CHECKBOX_WIDTH = 8.25f;

        /// <summary>
        /// Creates a new
        /// <see cref="CheckBoxRenderer"/>
        /// instance.
        /// </summary>
        /// <param name="modelElement">the model element</param>
        public CheckBoxRenderer(CheckBox modelElement)
            : base(modelElement) {
        }

        // 1px
        // enough to fit the area
        // 11px
        // 11px
        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.IRenderer#getNextRenderer()
        */
        public override IRenderer GetNextRenderer() {
            return new iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.CheckBoxRenderer((CheckBox)modelElement);
        }

        protected internal override IRenderer CreateFlatRenderer() {
            return CreateParagraphRenderer(IsBoxChecked() ? "\u2713" : "\u00A0");
        }

        protected internal override void AdjustFieldLayout() {
            throw new Exception("adjustFieldLayout() is deprecated and shouldn't be used. Override adjustFieldLayout(LayoutContext) instead"
                );
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.renderer.AbstractFormFieldRenderer#adjustFieldLayout()
        */
        protected internal override void AdjustFieldLayout(LayoutContext layoutContext) {
            UpdatePdfFont((ParagraphRenderer)flatRenderer);
            if (null == font) {
                LogManager.GetLogger(GetType()).Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.ERROR_WHILE_LAYOUT_OF_FORM_FIELD_WITH_TYPE
                    , "checkbox input"));
                SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, true);
            }
        }

        /// <summary>Defines whether the box is checked or not.</summary>
        /// <returns>the default value of the checkbox field</returns>
        public virtual bool IsBoxChecked() {
            return null != GetProperty(Html2PdfProperty.FORM_FIELD_CHECKED);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.renderer.AbstractFormFieldRenderer#applyAcroField(com.itextpdf.layout.renderer.DrawContext)
        */
        protected internal override void ApplyAcroField(DrawContext drawContext) {
            String name = GetModelId();
            PdfDocument doc = drawContext.GetDocument();
            Rectangle area = flatRenderer.GetOccupiedArea().GetBBox().Clone();
            PdfPage page = doc.GetPage(occupiedArea.GetPageNumber());
            PdfButtonFormField checkBox = PdfFormField.CreateCheckBox(doc, area, name, IsBoxChecked() ? "Yes" : "Off");
            PdfAcroForm.GetAcroForm(doc, true).AddField(checkBox, page);
        }

        /// <summary>Creates a paragraph renderer.</summary>
        /// <param name="defaultValue">the default value</param>
        /// <returns>the renderer</returns>
        internal override IRenderer CreateParagraphRenderer(String defaultValue) {
            Paragraph paragraph = new Paragraph(defaultValue).SetWidth(CHECKBOX_WIDTH).SetHeight(CHECKBOX_HEIGHT).SetBorder
                (new SolidBorder(CHECKBOX_BORDER_COLOR, CHECKBOX_BORDER_WIDTH)).SetFontSize(CHECKBOX_FONT_SIZE).SetTextAlignment
                (TextAlignment.CENTER);
            Leading leading = this.GetProperty<Leading>(Property.LEADING);
            if (leading != null) {
                paragraph.SetProperty(Property.LEADING, leading);
            }
            return paragraph.CreateRendererSubTree();
        }
    }
}
