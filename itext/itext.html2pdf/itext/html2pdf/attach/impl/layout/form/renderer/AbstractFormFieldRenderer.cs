/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.IO.Log;
using iText.Kernel.Geom;
using iText.Layout.Layout;
using iText.Layout.Minmaxwidth;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    ///  Abstract 
    /// <see cref="iText.Layout.Renderer.BlockRenderer"/>
    /// for form fields.
    /// </summary>
    public abstract class AbstractFormFieldRenderer : BlockRenderer, ILeafElementRenderer {
        /// <summary>
        /// The flat renderer.
        /// </summary>
        protected internal IRenderer flatRenderer;

        /// <summary>
        ///  Creates a new <see cref="AbstractFormFieldRenderer"/> instance.
        /// </summary>
        /// <param name="modelElement">the model element</param>
        protected internal AbstractFormFieldRenderer(IFormField modelElement)
            : base(modelElement) {
        }

        /// <summary>
        /// Checks if form fields need to be flattened.
        /// </summary>
        /// <returns>true, if fields need to be flattened</returns>
        public virtual bool IsFlatten() {
            bool? flatten = GetPropertyAsBoolean(Html2PdfProperty.FORM_FIELD_FLATTEN);
            return flatten != null ? (bool)flatten : (bool)modelElement.GetDefaultProperty<bool>(Html2PdfProperty.FORM_FIELD_FLATTEN
                );
        }

        /// <summary>
        ///  Gets the default value of the form field.
        /// </summary>
        /// <returns>the default value of the form field</returns>
        public virtual String GetDefaultValue() {
            String defaultValue = this.GetProperty<String>(Html2PdfProperty.FORM_FIELD_VALUE);
            return defaultValue != null ? defaultValue : modelElement.GetDefaultProperty<String>(Html2PdfProperty.FORM_FIELD_VALUE
                );
        }

        public override LayoutResult Layout(LayoutContext layoutContext) {
            return Layout(layoutContext, false);
        }

        public override void Draw(DrawContext drawContext) {
            if (flatRenderer != null) {
                base.Draw(drawContext);
            }
        }

        public override void DrawChildren(DrawContext drawContext) {
            drawContext.GetCanvas().SaveState();
            bool flatten = IsFlatten();
            if (flatten) {
                drawContext.GetCanvas().Rectangle(occupiedArea.GetBBox()).Clip().NewPath();
                flatRenderer.Draw(drawContext);
            }
            else {
                ApplyAcroField(drawContext);
            }
            drawContext.GetCanvas().RestoreState();
        }

        protected override MinMaxWidth GetMinMaxWidth(float availableWidth) {
            MinMaxWidthLayoutResult result = (MinMaxWidthLayoutResult)Layout(new LayoutContext(new LayoutArea(1, new Rectangle(availableWidth, AbstractRenderer.INF))), true);
            return result.GetNotNullMinMaxWidth(availableWidth);
        }

        protected internal abstract void AdjustFieldLayout();

        protected internal abstract IRenderer CreateFlatRenderer();

        protected internal abstract void ApplyAcroField(DrawContext drawContext);

        protected internal virtual String GetModelId() {
            return ((IFormField)GetModelElement()).GetId();
        }

        /// <summary>
        ///  Checks if the renderer fits a certain width and height.
        /// </summary>
        /// <param name="availableWidth">the available width</param>
        /// <param name="availableHeight">the available height</param>
        /// <returns>true, if the renderer fits</returns>
        protected internal virtual bool IsRendererFit(float availableWidth, float availableHeight) {
            if (occupiedArea == null) {
                return false;
            }
            return availableHeight >= occupiedArea.GetBBox().GetHeight() && availableWidth >= occupiedArea.GetBBox().GetWidth
                ();
        }

        /// <summary>
        /// Gets the content width.
        /// </summary>
        /// <returns>the content width</returns>
        protected internal virtual float? GetContentWidth() {
            UnitValue width = this.GetProperty<UnitValue>(Property.WIDTH);
            if (width != null) {
                if (width.IsPointValue()) {
                    return RetrieveWidth(0);
                } else {
                    LoggerFactory.GetLogger(GetType()).Warn(iText.Html2pdf.LogMessageConstant.INPUT_SUPPORTS_ONLY_POINT_WIDTH);
                }
            }
            return null;
        }

        public abstract float GetAscent();

        public abstract float GetDescent();

        private LayoutResult Layout(LayoutContext layoutContext, bool minMaxWidth) {
            childRenderers.Clear();
            flatRenderer = null;
            float parentWidth = layoutContext.GetArea().GetBBox().GetWidth();
            float parentHeight = layoutContext.GetArea().GetBBox().GetHeight();
            UnitValue maxHeight = this.GetProperty<UnitValue>(Property.MAX_HEIGHT);
            UnitValue height = this.GetProperty<UnitValue>(Property.HEIGHT);
            bool restoreMaxHeight = HasOwnProperty(Property.MAX_HEIGHT);
            bool restoreHeight = HasOwnProperty(Property.HEIGHT);
            SetProperty(Property.MAX_HEIGHT, null);
            SetProperty(Property.HEIGHT, null);
            IRenderer renderer = CreateFlatRenderer();
            float? width = GetContentWidth();
            if (width != null) {
                renderer.SetProperty(Property.WIDTH, new UnitValue(UnitValue.POINT, (float)width));
            }
            AddChild(renderer);
            Rectangle bBox = layoutContext.GetArea().GetBBox().Clone().MoveDown(INF - parentHeight).SetHeight(INF);
            layoutContext.GetArea().SetBBox(bBox);
            LayoutResult result = base.Layout(layoutContext);
            if (restoreMaxHeight) {
                SetProperty(Property.MAX_HEIGHT, maxHeight);
            } else {
                DeleteOwnProperty(Property.MAX_HEIGHT);
            }
            if (restoreHeight) {
                SetProperty(Property.HEIGHT, height);
            } else {
                DeleteOwnProperty(Property.HEIGHT);
            }
            if (!true.Equals(GetPropertyAsBoolean(Property.FORCED_PLACEMENT)) && (result.GetStatus() != LayoutResult.FULL
                )) {
                //@TODO investigate this tricky code a little more.
                FloatPropertyValue? floatPropertyValue = this.GetProperty<FloatPropertyValue?>(Property.FLOAT);
                if (floatPropertyValue == null || floatPropertyValue == FloatPropertyValue.NONE) {
                    SetProperty(Property.FORCED_PLACEMENT, true);
                } else {
                    flatRenderer = childRenderers[0];
                    childRenderers.Clear();
                    LayoutArea flatRendererOccupiedArea = flatRenderer.GetOccupiedArea();
                    ApplyPaddings(flatRendererOccupiedArea.GetBBox(), true);
                    ApplyBorderBox(flatRendererOccupiedArea.GetBBox(), true);
                    ApplyMargins(flatRendererOccupiedArea.GetBBox(), true);
                    childRenderers.Add(flatRenderer);
                    AdjustFieldLayout();
                    occupiedArea.SetBBox(flatRenderer.GetOccupiedArea().GetBBox().Clone());
                }
                return new MinMaxWidthLayoutResult(LayoutResult.NOTHING, occupiedArea, null, this, this).SetMinMaxWidth(new 
                    MinMaxWidth(0, parentWidth));
            }
            if (!childRenderers.IsEmpty()) {
                flatRenderer = childRenderers[0];
                childRenderers.Clear();
                childRenderers.Add(flatRenderer);
                AdjustFieldLayout();
                Rectangle fBox = flatRenderer.GetOccupiedArea().GetBBox();
                occupiedArea.GetBBox().SetX(fBox.GetX()).SetY(fBox.GetY()).SetWidth(fBox.GetWidth()).SetHeight(fBox.GetHeight());
                ApplyPaddings(occupiedArea.GetBBox(), true);
                ApplyBorderBox(occupiedArea.GetBBox(), true);
                ApplyMargins(occupiedArea.GetBBox(), true);
            } else {
                LoggerFactory.GetLogger(GetType()).Error(iText.Html2pdf.LogMessageConstant.ERROR_WHILE_LAYOUT_OF_FORM_FIELD
                );
                occupiedArea.GetBBox().SetWidth(0).SetHeight(0);
            }
            if (!true.Equals(GetPropertyAsBoolean(Property.FORCED_PLACEMENT)) && !IsRendererFit(parentWidth, parentHeight
                )) {
                if (!minMaxWidth) {
                    SetProperty(Property.FORCED_PLACEMENT, true);
                    occupiedArea.GetBBox().SetWidth(0).SetHeight(0);
                    return new MinMaxWidthLayoutResult(LayoutResult.NOTHING, occupiedArea, null, this, this).SetMinMaxWidth(new MinMaxWidth(0, parentWidth));
                } else {
                    return new MinMaxWidthLayoutResult(LayoutResult.NOTHING, occupiedArea, null, this, this).SetMinMaxWidth(new MinMaxWidth(occupiedArea.GetBBox().GetWidth(), occupiedArea.GetBBox().GetWidth()));
                }
            }
            if (result.GetStatus() != LayoutResult.FULL || !IsRendererFit(parentWidth, parentHeight)) {
                LoggerFactory.GetLogger(GetType()).Warn(iText.Html2pdf.LogMessageConstant.INPUT_FIELD_DOES_NOT_FIT);
            }
            return new MinMaxWidthLayoutResult(LayoutResult.FULL, occupiedArea, this, null).SetMinMaxWidth(new MinMaxWidth
                (0, parentWidth, occupiedArea.GetBBox().GetWidth(), occupiedArea.GetBBox().GetWidth()));
        }

    }
}
