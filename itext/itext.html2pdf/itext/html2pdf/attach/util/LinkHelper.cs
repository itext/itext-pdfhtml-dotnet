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
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Datastructures;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Tagging;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Helper class for links.</summary>
    public class LinkHelper {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Util.LinkHelper
            ));

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
        [System.ObsoleteAttribute(@"in favour ofapplyLinkAnnotation(IPropertyContainer container, String url, ProcessorContext context)"
            )]
        public static void ApplyLinkAnnotation(IPropertyContainer container, String url) {
            // Fake context here
            ApplyLinkAnnotation(container, url, new ProcessorContext(new ConverterProperties()));
        }

        /// <summary>Applies a link annotation.</summary>
        /// <param name="container">the containing object.</param>
        /// <param name="url">the destination.</param>
        /// <param name="context">the processor context.</param>
        public static void ApplyLinkAnnotation(IPropertyContainer container, String url, ProcessorContext context) {
            if (container != null) {
                PdfLinkAnnotation linkAnnotation;
                if (url.StartsWith("#")) {
                    String id = url.Substring(1);
                    linkAnnotation = context.GetLinkContext().GetLinkAnnotation(id);
                    if (linkAnnotation == null) {
                        linkAnnotation = (PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0, 0, 0)).SetAction(PdfAction.CreateGoTo
                            (id)).SetFlags(PdfAnnotation.PRINT);
                        context.GetLinkContext().AddLinkAnnotation(id, linkAnnotation);
                    }
                }
                else {
                    linkAnnotation = (PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0, 0, 0)).SetAction(PdfAction.CreateURI
                        (url)).SetFlags(PdfAnnotation.PRINT);
                }
                linkAnnotation.SetBorder(new PdfArray(new float[] { 0, 0, 0 }));
                container.SetProperty(Property.LINK_ANNOTATION, linkAnnotation);
                if (container is ILeafElement && container is IAccessibleElement) {
                    ((IAccessibleElement)container).GetAccessibilityProperties().SetRole(StandardRoles.LINK);
                }
            }
        }

        /// <summary>Creates a destination</summary>
        /// <param name="tagWorker">the tagworker that is building the (iText) element</param>
        /// <param name="element">the (HTML) element being converted</param>
        /// <param name="context">the Processor context</param>
        public static void CreateDestination(ITagWorker tagWorker, IElementNode element, ProcessorContext context) {
            String id = element.GetAttribute(AttributeConstants.ID);
            if (id == null) {
                return;
            }
            IPropertyContainer propertyContainer = GetPropertyContainer(tagWorker);
            if (context.GetLinkContext().IsUsedLinkDestination(id)) {
                if (propertyContainer == null) {
                    String tagWorkerClassName = tagWorker != null ? tagWorker.GetType().FullName : "null";
                    LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.ANCHOR_LINK_NOT_HANDLED, element.Name
                        (), id, tagWorkerClassName));
                    return;
                }
                PdfLinkAnnotation linkAnnotation = context.GetLinkContext().GetLinkAnnotation(id);
                if (linkAnnotation == null) {
                    linkAnnotation = (PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0, 0, 0)).SetAction(PdfAction.CreateGoTo
                        (id)).SetFlags(PdfAnnotation.PRINT);
                    context.GetLinkContext().AddLinkAnnotation(id, linkAnnotation);
                }
                propertyContainer.SetProperty(Property.DESTINATION, new Tuple2<String, PdfDictionary>(id, linkAnnotation.GetAction
                    ()));
            }
            if (propertyContainer != null) {
                propertyContainer.SetProperty(Property.ID, id);
            }
        }

        private static IPropertyContainer GetPropertyContainer(ITagWorker tagWorker) {
            if (tagWorker != null) {
                if (tagWorker is SpanTagWorker) {
                    IList<IPropertyContainer> spanElements = ((SpanTagWorker)tagWorker).GetAllElements();
                    if (!spanElements.IsEmpty()) {
                        return spanElements[0];
                    }
                }
                else {
                    return tagWorker.GetElementResult();
                }
            }
            return null;
        }
    }
}
