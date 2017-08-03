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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Tagutils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Helper class for links.</summary>
    public class LinkHelper {
        /// <summary>
        /// Creates a new
        /// <see cref="LinkHelper"/>
        /// class.
        /// </summary>
        private LinkHelper() {
        }

        /// <summary>Applies a link annotation.</summary>
        /// <param name="container">the containing object</param>
        /// <param name="url">the destination</param>
        public static void ApplyLinkAnnotation(IPropertyContainer container, String url) {
            if (container != null) {
                PdfLinkAnnotation linkAnnotation;
                if (url.StartsWith("#")) {
                    String name = url.Substring(1);
                    linkAnnotation = ((PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0, 0, 0)).SetAction(PdfAction.
                        CreateGoTo(name)));
                }
                else {
                    linkAnnotation = ((PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0, 0, 0)).SetAction(PdfAction.
                        CreateURI(url)));
                }
                linkAnnotation.SetBorder(new PdfArray(new float[] { 0, 0, 0 }));
                container.SetProperty(Property.LINK_ANNOTATION, linkAnnotation);
                if (container is ILeafElement && container is IAccessibleElement) {
                    ((IAccessibleElement)container).SetRole(PdfName.Link);
                }
            }
        }

        /// <summary>Creates a destination</summary>
        /// <param name="tagWorker">the tagworker that is building the (iText) element</param>
        /// <param name="element">the (HTML) element being converted</param>
        public static void CreateDestination(ITagWorker tagWorker, IElementNode element, ProcessorContext context) {
            if (element.GetAttribute(AttributeConstants.ID) == null) {
                return;
            }
            if (tagWorker == null) {
                return;
            }
            IPropertyContainer propertyContainer = tagWorker.GetElementResult();
            if (propertyContainer == null) {
                return;
            }
            // get id
            String id = element.GetAttribute(AttributeConstants.ID);
            // set property
            if (context.GetLinkContext().IsUsedLinkDestination(id)) {
                propertyContainer.SetProperty(Property.DESTINATION, id);
            }
        }
    }
}
