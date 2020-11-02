/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using System.Collections.Generic;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Logs;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>
    /// A
    /// <see cref="OutlineHandler"/>
    /// handles creating outlines for tags.
    /// </summary>
    /// <remarks>
    /// A
    /// <see cref="OutlineHandler"/>
    /// handles creating outlines for tags.
    /// <para />
    /// This class is not reusable and a new instance shall be created for every new conversion process.
    /// </remarks>
    public class OutlineHandler {
        /// <summary>The Constant DEFAULT_DESTINATION_PREFIX.</summary>
        private const String DEFAULT_DESTINATION_NAME_PREFIX = "pdfHTML-iText-outline-";

        /// <summary>The destination counter.</summary>
        /// <remarks>
        /// The destination counter.
        /// Counts the number of created the destinations with the same prefix in name,
        /// to achieve the uniqueness of the destination names.
        /// </remarks>
        private IDictionary<String, int?> destCounter = new Dictionary<String, int?>();

        /// <summary>The current outline.</summary>
        private PdfOutline currentOutline;

        /// <summary>The destinations in process.</summary>
        private LinkedList<String> destinationsInProcess = new LinkedList<String>();

        /// <summary>The levels in process.</summary>
        private LinkedList<int> levelsInProcess = new LinkedList<int>();

        /// <summary>The tag priorities mapping.</summary>
        private IDictionary<String, int?> tagPrioritiesMapping = new Dictionary<String, int?>();

        /// <summary>The destination prefix.</summary>
        private String destinationNamePrefix = DEFAULT_DESTINATION_NAME_PREFIX;

        /// <summary>Creates an OutlineHandler with standard predefined mappings.</summary>
        /// <returns>the outline handler</returns>
        public static OutlineHandler CreateStandardHandler() {
            OutlineHandler handler = new OutlineHandler();
            handler.PutTagPriorityMapping("h1", 1);
            handler.PutTagPriorityMapping("h2", 2);
            handler.PutTagPriorityMapping("h3", 3);
            handler.PutTagPriorityMapping("h4", 4);
            handler.PutTagPriorityMapping("h5", 5);
            handler.PutTagPriorityMapping("h6", 6);
            return handler;
        }

        /// <summary>Put tag priority mapping.</summary>
        /// <param name="tagName">the tag name</param>
        /// <param name="priority">the priority</param>
        /// <returns>the outline handler</returns>
        public virtual OutlineHandler PutTagPriorityMapping(String tagName, int? priority) {
            tagPrioritiesMapping.Put(tagName, priority);
            return this;
        }

        /// <summary>Put all tag priority mappings.</summary>
        /// <param name="mappings">the mappings</param>
        /// <returns>the outline handler</returns>
        public virtual OutlineHandler PutAllTagPriorityMappings(IDictionary<String, int?> mappings) {
            tagPrioritiesMapping.AddAll(mappings);
            return this;
        }

        /// <summary>Gets the tag priority mapping.</summary>
        /// <param name="tagName">the tag name</param>
        /// <returns>the tag priority mapping</returns>
        public virtual int? GetTagPriorityMapping(String tagName) {
            return tagPrioritiesMapping.Get(tagName);
        }

        /// <summary>Checks for tag priority mapping.</summary>
        /// <param name="tagName">the tag name</param>
        /// <returns>true, if the tag name is listed in the tag priorities mapping</returns>
        public virtual bool HasTagPriorityMapping(String tagName) {
            return tagPrioritiesMapping.ContainsKey(tagName);
        }

        /// <summary>
        /// Resets the current state so that this
        /// <see cref="OutlineHandler"/>
        /// is ready to process new document
        /// </summary>
        public virtual void Reset() {
            currentOutline = null;
            destinationsInProcess.Clear();
            levelsInProcess.Clear();
            destCounter.Clear();
        }

        /// <summary>Sets the destination name prefix.</summary>
        /// <remarks>
        /// Sets the destination name prefix.
        /// The destination name prefix serves as the prefix for the destination names created in the
        /// <see cref="GenerateUniqueDestinationName(iText.StyledXmlParser.Node.IElementNode)"/>
        /// method.
        /// </remarks>
        /// <param name="destinationNamePrefix">the destination name prefix</param>
        public virtual void SetDestinationNamePrefix(String destinationNamePrefix) {
            this.destinationNamePrefix = destinationNamePrefix;
        }

        /// <summary>Gets the destination name prefix.</summary>
        /// <remarks>
        /// Gets the destination name prefix.
        /// The destination name prefix serves as the prefix for the destination names created in the
        /// <see cref="GenerateUniqueDestinationName(iText.StyledXmlParser.Node.IElementNode)"/>
        /// method.
        /// </remarks>
        /// <returns>the destination name prefix</returns>
        public virtual String GetDestinationNamePrefix() {
            return destinationNamePrefix;
        }

        /// <summary>Generate the unique destination name.</summary>
        /// <remarks>
        /// Generate the unique destination name.
        /// The destination name is a unique identifier for the outline so it is generated for the outline
        /// in the
        /// <see cref="AddOutlineAndDestToDocument(iText.Html2pdf.Attach.ITagWorker, iText.StyledXmlParser.Node.IElementNode, iText.Html2pdf.Attach.ProcessorContext)
        ///     "/>
        /// method. You can override this method to set
        /// your own way to generate the destination names, to avoid the destination name conflicts when
        /// merging several PDF files created by html2pdf.
        /// </remarks>
        /// <param name="element">the element</param>
        /// <returns>the unique destination name</returns>
        protected internal virtual String GenerateUniqueDestinationName(IElementNode element) {
            return GetUniqueID(destinationNamePrefix);
        }

        /// <summary>Generate the unique outline name.</summary>
        /// <remarks>
        /// Generate the unique outline name.
        /// This method is used in the
        /// <see cref="AddOutlineAndDestToDocument(iText.Html2pdf.Attach.ITagWorker, iText.StyledXmlParser.Node.IElementNode, iText.Html2pdf.Attach.ProcessorContext)
        ///     "/>
        /// method.
        /// You can override this method to set your own way to generate the outline names.
        /// </remarks>
        /// <param name="element">the element</param>
        /// <returns>the unique destination name</returns>
        protected internal virtual String GenerateUniqueOutlineName(IElementNode element) {
            String tagName = element.Name();
            String content = ((JsoupElementNode)element).Text();
            if (String.IsNullOrEmpty(content)) {
                content = GetUniqueID(tagName);
            }
            return content;
        }

        /// <summary>Adds the outline and the destination.</summary>
        /// <remarks>
        /// Adds the outline and the destination.
        /// Adds the outline and its corresponding the destination to the PDF document
        /// if the priority mapping is set for the element.
        /// </remarks>
        /// <param name="tagWorker">the tag worker</param>
        /// <param name="element">the element</param>
        /// <param name="context">the processor context</param>
        /// <returns>the outline handler</returns>
        internal virtual OutlineHandler AddOutlineAndDestToDocument(ITagWorker tagWorker, IElementNode element, ProcessorContext
             context) {
            String tagName = element.Name();
            if (null != tagWorker && HasTagPriorityMapping(tagName) && context.GetPdfDocument() != null) {
                int level = (int)GetTagPriorityMapping(tagName);
                if (null == currentOutline) {
                    currentOutline = context.GetPdfDocument().GetOutlines(false);
                }
                PdfOutline parent = currentOutline;
                while (!levelsInProcess.IsEmpty() && level <= levelsInProcess.JGetFirst()) {
                    parent = parent.GetParent();
                    levelsInProcess.JRemoveFirst();
                }
                PdfOutline outline = parent.AddOutline(GenerateUniqueOutlineName(element));
                String destination = GenerateUniqueDestinationName(element);
                outline.AddDestination(PdfDestination.MakeDestination(new PdfString(destination)));
                destinationsInProcess.AddFirst(destination);
                levelsInProcess.AddFirst(level);
                currentOutline = outline;
            }
            return this;
        }

        /// <summary>Sets the destination to element.</summary>
        /// <remarks>
        /// Sets the destination to element.
        /// Sets the destination previously created in the
        /// <see cref="AddOutlineAndDestToDocument(iText.Html2pdf.Attach.ITagWorker, iText.StyledXmlParser.Node.IElementNode, iText.Html2pdf.Attach.ProcessorContext)
        ///     "/>
        /// method
        /// to the tag worker element.
        /// </remarks>
        /// <param name="tagWorker">the tag worker</param>
        /// <param name="element">the element</param>
        /// <returns>the outline handler</returns>
        internal virtual OutlineHandler SetDestinationToElement(ITagWorker tagWorker, IElementNode element) {
            String tagName = element.Name();
            if (null != tagWorker && HasTagPriorityMapping(tagName) && destinationsInProcess.Count > 0) {
                String content = destinationsInProcess.JRemoveFirst();
                if (tagWorker.GetElementResult() is IElement) {
                    tagWorker.GetElementResult().SetProperty(Property.DESTINATION, content);
                }
                else {
                    ILog logger = LogManager.GetLogger(typeof(OutlineHandler));
                    logger.Warn(MessageFormatUtil.Format(Html2PdfLogMessageConstant.NO_IPROPERTYCONTAINER_RESULT_FOR_THE_TAG, 
                        tagName));
                }
            }
            return this;
        }

        /// <summary>Gets the unique ID.</summary>
        /// <remarks>
        /// Gets the unique ID.
        /// This method is used in the
        /// <see cref="GenerateUniqueDestinationName(iText.StyledXmlParser.Node.IElementNode)"/>
        /// method to generate the unique
        /// destination names and in the
        /// <see cref="GenerateUniqueOutlineName(iText.StyledXmlParser.Node.IElementNode)"/>
        /// method to generate the unique
        /// outline names. The
        /// <see cref="destCounter"/>
        /// map serves to achieve the uniqueness of an ID.
        /// </remarks>
        /// <param name="key">the key</param>
        /// <returns>the unique ID</returns>
        private String GetUniqueID(String key) {
            if (!destCounter.ContainsKey(key)) {
                destCounter.Put(key, 1);
            }
            int id = (int)destCounter.Get(key);
            destCounter.Put(key, id + 1);
            return key + id;
        }
    }
}
