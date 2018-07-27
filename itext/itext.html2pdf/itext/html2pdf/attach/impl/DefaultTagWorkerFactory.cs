/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using System.Reflection;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Html.Node;
using iText.Html2pdf.Util;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>
    /// The default implementation of a tag worker factory, mapping tags to
    /// tag worker implementations.
    /// </summary>
    public class DefaultTagWorkerFactory : ITagWorkerFactory {
        private static readonly ITagWorkerFactory INSTANCE = new iText.Html2pdf.Attach.Impl.DefaultTagWorkerFactory
            ();

        /// <summary>The default mapping.</summary>
        private TagProcessorMapping defaultMapping;

        /// <summary>Instantiates a new default tag worker factory.</summary>
        public DefaultTagWorkerFactory() {
            this.defaultMapping = DefaultTagWorkerMapping.GetDefaultTagWorkerMapping();
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
                Type tagWorkerClass = GetTagWorkerClass(this.defaultMapping, tag);
                if (tagWorkerClass == null) {
                    return null;
                }
                // Use reflection to create an instance
                try {
                    ConstructorInfo ctor = tagWorkerClass.GetConstructor(new Type[] { typeof(IElementNode), typeof(ProcessorContext
                        ) });
                    ITagWorker res = (ITagWorker)ctor.Invoke(new Object[] { tag, context });
                    return res;
                }
                catch (Exception e) {
                    throw new TagWorkerInitializationException(TagWorkerInitializationException.REFLECTION_IN_TAG_WORKER_FACTORY_IMPLEMENTATION_FAILED
                        , tagWorkerClass.FullName, tag.Name(), e);
                }
            }
            return tagWorker;
        }

        /// <summary>Gets the tag worker class for a specific element node.</summary>
        /// <param name="mapping">the mapping</param>
        /// <param name="tag">the element node</param>
        /// <returns>the tag worker class</returns>
        private Type GetTagWorkerClass(TagProcessorMapping mapping, IElementNode tag) {
            Type tagWorkerClass = null;
            String display = tag.GetStyles() != null ? tag.GetStyles().Get(CssConstants.DISPLAY) : null;
            if (display != null) {
                tagWorkerClass = mapping.GetMapping(tag.Name(), display);
            }
            if (tagWorkerClass == null) {
                tagWorkerClass = mapping.GetMapping(tag.Name());
            }
            return tagWorkerClass;
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
