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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for elements with display flex.
    /// </summary>
    public class DisplayFlexTagCssApplier : BlockCssApplier {
        public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            base.Apply(context, stylesContainer, tagWorker);
            IPropertyContainer container = tagWorker.GetElementResult();
            if (container != null) {
                FlexApplierUtil.ApplyFlexContainerProperties(stylesContainer.GetStyles(), container);
                //TODO DEVSIX-5087 remove these lines when working on a ticket
                container.DeleteOwnProperty(Property.FLOAT);
                container.DeleteOwnProperty(Property.CLEAR);
                container.DeleteOwnProperty(Property.OVERFLOW_X);
                container.DeleteOwnProperty(Property.OVERFLOW_Y);
            }
        }
    }
}
