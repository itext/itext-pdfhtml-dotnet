/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf.Css;
using iText.Html2pdf.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>
    /// The default implementation of a tag worker factory, mapping tags to
    /// tag worker implementations.
    /// </summary>
    public class DefaultTagWorkerFactory : ITagWorkerFactory {
        private static readonly ITagWorkerFactory INSTANCE = new iText.Html2pdf.Attach.Impl.DefaultTagWorkerFactory
            ();

        /// <summary>The default mapping.</summary>
        private readonly TagProcessorMapping<DefaultTagWorkerMapping.ITagWorkerCreator> defaultMapping;

        /// <summary>Instantiates a new default tag worker factory.</summary>
        public DefaultTagWorkerFactory() {
            this.defaultMapping = new DefaultTagWorkerMapping().GetDefaultTagWorkerMapping();
        }

        /// <summary>
        /// Gets
        /// <see cref="iText.Html2pdf.Attach.ITagWorkerFactory"/>
        /// instance.
        /// </summary>
        /// <returns>default instance that is used if custom tag workers are not configured</returns>
        public static ITagWorkerFactory GetInstance() {
            return INSTANCE;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorkerFactory#getTagWorker(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public ITagWorker GetTagWorker(IElementNode tag, ProcessorContext context) {
            ITagWorker tagWorker = GetCustomTagWorker(tag, context);
            if (tagWorker == null) {
                DefaultTagWorkerMapping.ITagWorkerCreator tagWorkerCreator = GetTagWorkerCreator(this.defaultMapping, tag);
                if (tagWorkerCreator == null) {
                    return null;
                }
                return tagWorkerCreator(tag, context);
            }
            return tagWorker;
        }

        internal virtual TagProcessorMapping<DefaultTagWorkerMapping.ITagWorkerCreator> GetDefaultMapping() {
            return defaultMapping;
        }

        /// <summary>Gets the tag worker creator for a specific element node.</summary>
        /// <param name="mapping">the mapping</param>
        /// <param name="tag">the element node</param>
        /// <returns>the tag worker class creator</returns>
        private static DefaultTagWorkerMapping.ITagWorkerCreator GetTagWorkerCreator(TagProcessorMapping<DefaultTagWorkerMapping.ITagWorkerCreator
            > mapping, IElementNode tag) {
            DefaultTagWorkerMapping.ITagWorkerCreator tagWorkerCreator = null;
            String display = tag.GetStyles() != null ? tag.GetStyles().Get(CssConstants.DISPLAY) : null;
            if (display != null) {
                tagWorkerCreator = (DefaultTagWorkerMapping.ITagWorkerCreator)mapping.GetMapping(tag.Name(), display);
            }
            if (tagWorkerCreator == null) {
                tagWorkerCreator = (DefaultTagWorkerMapping.ITagWorkerCreator)mapping.GetMapping(tag.Name());
            }
            return tagWorkerCreator;
        }

        /// <summary>This is a hook method.</summary>
        /// <remarks>
        /// This is a hook method. Users wanting to provide a custom mapping
        /// or introduce their own ITagWorkers should implement this method.
        /// </remarks>
        /// <param name="tag">the tag</param>
        /// <param name="context">the context</param>
        /// <returns>the custom tag worker</returns>
        public virtual ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
            return null;
        }
    }
}
