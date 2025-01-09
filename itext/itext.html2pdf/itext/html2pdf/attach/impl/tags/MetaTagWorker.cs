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
using iText.Html2pdf.Html;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>meta</c>
    /// element.
    /// </summary>
    public class MetaTagWorker : ITagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="MetaTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="tag">the tag</param>
        /// <param name="context">the context</param>
        public MetaTagWorker(IElementNode tag, ProcessorContext context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            // Note that charset and http-equiv attributes are processed on DataUtil#parseByteData(ByteBuffer, String, String, Parser) level.
            String name = element.GetAttribute(AttributeConstants.NAME);
            if (null != name) {
                name = name.ToLowerInvariant();
                String content = element.GetAttribute(AttributeConstants.CONTENT);
                // although iText do not visit head during processing html to elements
                // meta tag can by accident be presented in body section and that shouldn't cause NPE
                if (null != content && null != context.GetPdfDocument()) {
                    PdfDocumentInfo info = context.GetPdfDocument().GetDocumentInfo();
                    if (AttributeConstants.AUTHOR.Equals(name)) {
                        info.SetAuthor(content);
                    }
                    else {
                        if (AttributeConstants.APPLICATION_NAME.Equals(name)) {
                            info.SetCreator(content);
                        }
                        else {
                            if (AttributeConstants.KEYWORDS.Equals(name)) {
                                info.SetKeywords(content);
                            }
                            else {
                                if (AttributeConstants.DESCRIPTION.Equals(name)) {
                                    info.SetSubject(content);
                                }
                                else {
                                    info.SetMoreInfo(name, content);
                                }
                            }
                        }
                    }
                }
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return null;
        }
    }
}
