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
using System.IO;
using Org.Jsoup.Nodes;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Impl.Jsoup.Node;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;

namespace iText.Html2pdf.Html.Impl.Jsoup {
    /// <summary>Class that uses JSoup to parse HTML.</summary>
    public class JsoupHtmlParser : IHtmlParser {
        /// <summary>The logger.</summary>
        private static ILogger logger = LoggerFactory.GetLogger(typeof(JsoupHtmlParser));

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.html.IHtmlParser#parse(java.io.InputStream, java.lang.String)
        */
        /// <exception cref="System.IO.IOException"/>
        public virtual IDocumentNode Parse(Stream htmlStream, String charset) {
            // Based on some brief investigations, it seems that Jsoup uses baseUri for resolving relative uri's into absolute
            // on user demand. We perform such resolving in ResourceResolver class, therefore it is not needed here.
            String baseUri = "";
            Document doc = Org.Jsoup.Jsoup.Parse(htmlStream, charset, baseUri);
            INode result = WrapJsoupHierarchy(doc);
            if (result is IDocumentNode) {
                return (IDocumentNode)result;
            }
            else {
                throw new InvalidOperationException();
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.html.IHtmlParser#parse(java.lang.String)
        */
        public virtual IDocumentNode Parse(String html) {
            Document doc = Org.Jsoup.Jsoup.Parse(html);
            INode result = WrapJsoupHierarchy(doc);
            if (result is IDocumentNode) {
                return (IDocumentNode)result;
            }
            else {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Wraps JSoup nodes into pdfHTML
        /// <see cref="iText.Html2pdf.Html.Node.INode"/>
        /// classes.
        /// </summary>
        /// <param name="jsoupNode">the JSoup node instance</param>
        /// <returns>
        /// the
        /// <see cref="iText.Html2pdf.Html.Node.INode"/>
        /// instance
        /// </returns>
        private INode WrapJsoupHierarchy(Org.Jsoup.Nodes.Node jsoupNode) {
            INode resultNode = null;
            if (jsoupNode is Document) {
                resultNode = new JsoupDocumentNode((Document)jsoupNode);
            }
            else {
                if (jsoupNode is TextNode) {
                    resultNode = new JsoupTextNode((TextNode)jsoupNode);
                }
                else {
                    if (jsoupNode is Element) {
                        resultNode = new JsoupElementNode((Element)jsoupNode);
                    }
                    else {
                        if (jsoupNode is DataNode) {
                            resultNode = new JsoupDataNode((DataNode)jsoupNode);
                        }
                        else {
                            if (jsoupNode is DocumentType) {
                                resultNode = new JsoupDocumentTypeNode((DocumentType)jsoupNode);
                            }
                            else {
                                if (jsoupNode is Comment) {
                                }
                                else {
                                    logger.Error(String.Format("Could not map node type: {0}", jsoupNode.GetType()));
                                }
                            }
                        }
                    }
                }
            }
            foreach (Org.Jsoup.Nodes.Node node in jsoupNode.ChildNodes()) {
                INode childNode = WrapJsoupHierarchy(node);
                if (childNode != null) {
                    resultNode.AddChild(childNode);
                }
            }
            return resultNode;
        }
    }
}
