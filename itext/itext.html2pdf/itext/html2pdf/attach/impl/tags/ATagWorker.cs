/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf.Tagging;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Resolver.Resource;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>a</c>
    /// element.
    /// </summary>
    public class ATagWorker : SpanTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="ATagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ATagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.SpanTagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessEnd(element, context);
            String url = element.GetAttribute(AttributeConstants.HREF);
            if (url != null) {
                String @base = context.GetBaseUri();
                if (@base != null) {
                    UriResolver uriResolver = new UriResolver(@base);
                    if (!(url.StartsWith("#") && uriResolver.IsLocalBaseUri())) {
                        try {
                            String resolvedUri = uriResolver.ResolveAgainstBaseUri(url).ToExternalForm();
                            if (!url.EndsWith("/") && resolvedUri.EndsWith("/")) {
                                resolvedUri = resolvedUri.JSubstring(0, resolvedUri.Length - 1);
                            }
                            if (!resolvedUri.StartsWith("file:")) {
                                url = resolvedUri;
                            }
                        }
                        catch (UriFormatException) {
                        }
                    }
                }
                for (int i = 0; i < GetAllElements().Count; i++) {
                    if (GetAllElements()[i] is RunningElement) {
                        continue;
                    }
                    if (GetAllElements()[i] is IBlockElement) {
                        Div simulatedDiv = new Div();
                        simulatedDiv.GetAccessibilityProperties().SetRole(StandardRoles.LINK);
                        Transform cssTransform = GetAllElements()[i].GetProperty<Transform>(Property.TRANSFORM);
                        if (cssTransform != null) {
                            GetAllElements()[i].DeleteOwnProperty(Property.TRANSFORM);
                            simulatedDiv.SetProperty(Property.TRANSFORM, cssTransform);
                        }
                        FloatPropertyValue? floatPropVal = GetAllElements()[i].GetProperty<FloatPropertyValue?>(Property.FLOAT);
                        if (floatPropVal != null) {
                            GetAllElements()[i].DeleteOwnProperty(Property.FLOAT);
                            simulatedDiv.SetProperty(Property.FLOAT, floatPropVal);
                        }
                        simulatedDiv.Add((IBlockElement)GetAllElements()[i]);
                        String display = childrenDisplayMap.JRemove(GetAllElements()[i]);
                        if (display != null) {
                            childrenDisplayMap.Put(simulatedDiv, display);
                        }
                        GetAllElements()[i] = simulatedDiv;
                    }
                    LinkHelper.ApplyLinkAnnotation(GetAllElements()[i], url);
                }
            }
            if (!GetAllElements().IsEmpty()) {
                String name = element.GetAttribute(AttributeConstants.NAME);
                IPropertyContainer firstElement = GetAllElements()[0];
                firstElement.SetProperty(Property.DESTINATION, name);
            }
        }
    }
}
