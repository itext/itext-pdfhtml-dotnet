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
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Pdf.Tagutils;
using iText.Layout;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class ThTagWorker : TdTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="ThTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ThTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }

        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessEnd(element, context);
            IPropertyContainer elementResult = base.GetElementResult();
            if (elementResult is IAccessibleElement) {
                ((IAccessibleElement)elementResult).GetAccessibilityProperties().SetRole(StandardRoles.TH);
                if (context.GetPdfDocument() == null || context.GetPdfDocument().IsTagged()) {
                    String scope = element.GetAttribute(AttributeConstants.SCOPE);
                    AccessibilityProperties properties = ((IAccessibleElement)elementResult).GetAccessibilityProperties();
                    PdfDictionary attributes = new PdfDictionary();
                    attributes.Put(PdfName.O, PdfName.Table);
                    if (scope != null && (AttributeConstants.ROW.EqualsIgnoreCase(scope) || AttributeConstants.ROWGROUP.EqualsIgnoreCase
                        (scope))) {
                        attributes.Put(PdfName.Scope, PdfName.Row);
                        properties.AddAttributes(new PdfStructureAttributes(attributes));
                    }
                    else {
                        if (scope != null && (AttributeConstants.COL.EqualsIgnoreCase(scope) || AttributeConstants.COLGROUP.EqualsIgnoreCase
                            (scope))) {
                            attributes.Put(PdfName.Scope, PdfName.Column);
                            properties.AddAttributes(new PdfStructureAttributes(attributes));
                        }
                        else {
                            ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.ThTagWorker));
                            logger.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.NOT_SUPPORTED_TH_SCOPE_TYPE, scope));
                        }
                    }
                }
            }
        }
    }
}
