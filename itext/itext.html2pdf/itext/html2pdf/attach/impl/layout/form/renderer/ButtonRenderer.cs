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
using System.Collections.Generic;
using Common.Logging;
using iText.Forms;
using iText.Forms.Fields;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.IO.Util;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// The
    /// <see cref="AbstractOneLineTextFieldRenderer"/>
    /// implementation for buttons.
    /// </summary>
    public class ButtonRenderer : AbstractOneLineTextFieldRenderer {
        /// <summary>Indicates of the content was split.</summary>
        private bool isSplit = false;

        /// <summary>
        /// Creates a new
        /// <see cref="ButtonRenderer"/>
        /// instance.
        /// </summary>
        /// <param name="modelElement">the model element</param>
        public ButtonRenderer(Button modelElement)
            : base(modelElement) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.IRenderer#getNextRenderer()
        */
        public override IRenderer GetNextRenderer() {
            return new iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.ButtonRenderer((Button)modelElement);
        }

        protected internal override void AdjustFieldLayout() {
            throw new Exception("adjustFieldLayout() is deprecated and shouldn't be used. Override adjustFieldLayout(LayoutContext) instead"
                );
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.renderer.AbstractFormFieldRenderer#adjustFieldLayout()
        */
        protected internal override void AdjustFieldLayout(LayoutContext layoutContext) {
            IList<LineRenderer> flatLines = ((ParagraphRenderer)flatRenderer).GetLines();
            Rectangle flatBBox = flatRenderer.GetOccupiedArea().GetBBox();
            UpdatePdfFont((ParagraphRenderer)flatRenderer);
            if (!flatLines.IsEmpty() && font != null) {
                if (flatLines.Count != 1) {
                    isSplit = true;
                }
                CropContentLines(flatLines, flatBBox);
                float? width = RetrieveWidth(layoutContext.GetArea().GetBBox().GetWidth());
                if (width == null) {
                    LineRenderer drawnLine = flatLines[0];
                    drawnLine.Move(flatBBox.GetX() - drawnLine.GetOccupiedArea().GetBBox().GetX(), 0);
                    flatBBox.SetWidth(drawnLine.GetOccupiedArea().GetBBox().GetWidth());
                }
            }
            else {
                LogManager.GetLogger(GetType()).Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.ERROR_WHILE_LAYOUT_OF_FORM_FIELD_WITH_TYPE
                    , "button"));
                SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, true);
                baseline = flatBBox.GetTop();
                flatBBox.SetY(flatBBox.GetTop()).SetHeight(0);
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.renderer.AbstractFormFieldRenderer#createFlatRenderer()
        */
        protected internal override IRenderer CreateFlatRenderer() {
            return CreateParagraphRenderer(GetDefaultValue());
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.renderer.AbstractFormFieldRenderer#applyAcroField(com.itextpdf.layout.renderer.DrawContext)
        */
        protected internal override void ApplyAcroField(DrawContext drawContext) {
            String value = GetDefaultValue();
            String name = GetModelId();
            UnitValue fontSize = (UnitValue)this.GetPropertyAsUnitValue(Property.FONT_SIZE);
            if (!fontSize.IsPointValue()) {
                ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.ButtonRenderer));
                logger.Error(MessageFormatUtil.Format(iText.IO.LogMessageConstant.PROPERTY_IN_PERCENTS_NOT_SUPPORTED, Property
                    .FONT_SIZE));
            }
            PdfDocument doc = drawContext.GetDocument();
            Rectangle area = flatRenderer.GetOccupiedArea().GetBBox().Clone();
            ApplyPaddings(area, true);
            PdfPage page = doc.GetPage(occupiedArea.GetPageNumber());
            PdfButtonFormField button = PdfFormField.CreatePushButton(doc, area, name, value, font, fontSize.GetValue(
                ));
            Background background = this.GetProperty<Background>(Property.BACKGROUND);
            if (background != null && background.GetColor() != null) {
                button.SetBackgroundColor(background.GetColor());
            }
            ApplyDefaultFieldProperties(button);
            PdfAcroForm.GetAcroForm(doc, true).AddField(button, page);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.renderer.AbstractFormFieldRenderer#isRendererFit(float, float)
        */
        protected internal override bool IsRendererFit(float availableWidth, float availableHeight) {
            return !isSplit && base.IsRendererFit(availableWidth, availableHeight);
        }
    }
}
