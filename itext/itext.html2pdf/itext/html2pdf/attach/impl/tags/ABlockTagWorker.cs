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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags.Util;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Html;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for a link block.</summary>
    public class ABlockTagWorker : DivTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="ABlockTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ABlockTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.DivTagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessEnd(element, context);
            String url = element.GetAttribute(AttributeConstants.HREF);
            if (url != null) {
                String anchorLink = element.GetAttribute(AttributeConstants.HREF);
                String baseUri = context.GetBaseUri();
                String modifiedUrl = ATagUtil.ResolveAnchorLink(anchorLink, baseUri);
                LinkHelper.ApplyLinkAnnotation(GetElementResult(), modifiedUrl, context, element);
            }
            if (GetElementResult() != null) {
                String name = element.GetAttribute(AttributeConstants.NAME);
                GetElementResult().SetProperty(Property.DESTINATION, name);
            }
        }
    }
}
