/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html.Impl.Jsoup.Node;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;

namespace iText.Html2pdf.Attach.Impl {
    public class OutlinesHandler {
        private const String DESTINATION_PREFIX = "pdfHTML-itext-outline-";

        private PdfOutline currentOutline;

        private LinkedList<String> destinationsInProcess = new LinkedList<String>();

        private LinkedList<int> levelsInProcess = new LinkedList<int>();

        private IDictionary<String, int?> tagPrioritiesMapping = new Dictionary<String, int?>();

        private IDictionary<String, int?> uniqueIDs = new Dictionary<String, int?>();

        public static OutlinesHandler CreateDefault() {
            OutlinesHandler handler = new OutlinesHandler();
            handler.PutTagPriorityMapping("h1", 1);
            handler.PutTagPriorityMapping("h2", 2);
            handler.PutTagPriorityMapping("h3", 3);
            handler.PutTagPriorityMapping("h4", 4);
            handler.PutTagPriorityMapping("h5", 5);
            handler.PutTagPriorityMapping("h6", 6);
            return handler;
        }

        public virtual OutlinesHandler PutTagPriorityMapping(String tagName, int? priority) {
            tagPrioritiesMapping.Put(tagName, priority);
            return this;
        }

        public virtual OutlinesHandler PutAllTagPriorityMappings(IDictionary<String, int?> mappings) {
            tagPrioritiesMapping.AddAll(mappings);
            return this;
        }

        public virtual int? GetTagPriorityMapping(String tagName) {
            return tagPrioritiesMapping.Get(tagName);
        }

        public virtual bool HasTagPriorityMapping(String tagName) {
            return tagPrioritiesMapping.ContainsKey(tagName);
        }

        public virtual OutlinesHandler AddOutline(ITagWorker tagWorker, IElementNode element, ProcessorContext context
            ) {
            String tagName = element.Name();
            if (null != tagWorker && HasTagPriorityMapping(tagName)) {
                int level = GetTagPriorityMapping(tagName);
                if (null == currentOutline) {
                    currentOutline = context.GetPdfDocument().GetOutlines(false);
                }
                PdfOutline parent = currentOutline;
                while (!levelsInProcess.IsEmpty() && level <= levelsInProcess.Peek()) {
                    parent = parent.GetParent();
                    levelsInProcess.JRemoveFirst();
                }
                String content = ((JsoupElementNode)element).Text();
                if (String.IsNullOrEmpty(content)) {
                    content = GetUniqueID(tagName);
                }
                PdfOutline outline = parent.AddOutline(content);
                String destination = DESTINATION_PREFIX + GetUniqueID(DESTINATION_PREFIX);
                outline.AddDestination(PdfDestination.MakeDestination(new PdfString(destination)));
                destinationsInProcess.AddFirst(destination);
                levelsInProcess.AddFirst(level);
                currentOutline = outline;
            }
            return this;
        }

        public virtual OutlinesHandler AddDestination(ITagWorker tagWorker, IElementNode element) {
            String tagName = element.Name();
            if (null != tagWorker && HasTagPriorityMapping(tagName)) {
                String content = destinationsInProcess.JRemoveFirst();
                if (null != tagWorker.GetElementResult()) {
                    ((ElementPropertyContainer)tagWorker.GetElementResult()).SetDestination(content);
                }
                else {
                    ILogger logger = LoggerFactory.GetLogger(typeof(OutlinesHandler));
                    logger.Warn(String.Format(iText.Html2pdf.LogMessageConstant.NO_IPROPERTYCONTAINER_RESULT_FOR_THE_TAG, tagName
                        ));
                }
            }
            return this;
        }

        private String GetUniqueID(String key) {
            if (!uniqueIDs.ContainsKey(key)) {
                uniqueIDs.Put(key, 1);
            }
            int id = uniqueIDs.Get(key);
            uniqueIDs.Put(key, id + 1);
            return key + id;
        }
    }
}
