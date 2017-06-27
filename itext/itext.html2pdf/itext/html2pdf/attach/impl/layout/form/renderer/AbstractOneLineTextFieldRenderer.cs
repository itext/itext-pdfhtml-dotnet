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
using System.Collections.Generic;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Kernel.Geom;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// Abstract
    /// <see cref="iText.Layout.Renderer.BlockRenderer"/>
    /// for a single line of text content in a form field.
    /// </summary>
    public abstract class AbstractOneLineTextFieldRenderer : AbstractTextFieldRenderer {
        /// <summary>The position of the base line of the text.</summary>
        protected internal float baseline;

        /// <summary>Creates a new <code>AbstractOneLineTextFieldRenderer</code> instance.</summary>
        /// <param name="modelElement">the model element</param>
        protected internal AbstractOneLineTextFieldRenderer(IFormField modelElement)
            : base(modelElement) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.ILeafElementRenderer#getAscent()
        */
        public override float GetAscent() {
            return occupiedArea.GetBBox().GetTop() - baseline;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.ILeafElementRenderer#getDescent()
        */
        public override float GetDescent() {
            return occupiedArea.GetBBox().GetBottom() - baseline;
        }

        /// <summary>Crops the content lines.</summary>
        /// <param name="lines">a list of lines</param>
        /// <param name="bBox">the bounding box</param>
        protected internal virtual void CropContentLines(IList<LineRenderer> lines, Rectangle bBox) {
            AdjustNumberOfContentLines(lines, bBox, 1);
            UpdateParagraphHeight();
            baseline = lines[0].GetYLine();
        }

        /// <summary>Updates the paragraph height.</summary>
        protected internal virtual void UpdateParagraphHeight() {
            OverrideHeightProperties();
            float? height = RetrieveHeight();
            float? minHeight = RetrieveMinHeight();
            float? maxHeight = RetrieveMaxHeight();
            Rectangle flatBBox = flatRenderer.GetOccupiedArea().GetBBox();
            if (height != null && height.Value > 0) {
                SetContentHeight(flatBBox, height.Value);
            }
            else {
                if (minHeight != null && minHeight.Value > flatBBox.GetHeight()) {
                    SetContentHeight(flatBBox, minHeight.Value);
                }
                else {
                    if (maxHeight != null && maxHeight.Value > 0 && maxHeight.Value < flatBBox.GetHeight()) {
                        SetContentHeight(flatBBox, maxHeight.Value);
                    }
                }
            }
        }

        /// <summary>Sets the content height.</summary>
        /// <param name="bBox">the bounding box</param>
        /// <param name="height">the height</param>
        private void SetContentHeight(Rectangle bBox, float height) {
            float dy = (height - bBox.GetHeight()) / 2;
            bBox.MoveDown(dy);
            bBox.SetHeight(height);
        }
    }
}
