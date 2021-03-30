/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.IO.Util;
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
                            ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Tags.ThTagWorker));
                            logger.Warn(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.NOT_SUPPORTED_TH_SCOPE_TYPE, scope)
                                );
                        }
                    }
                }
            }
        }
    }
}
