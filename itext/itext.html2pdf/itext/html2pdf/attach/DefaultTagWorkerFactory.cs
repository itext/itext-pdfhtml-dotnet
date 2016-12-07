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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>Created by SamuelHuylebroeck on 11/30/2016.</summary>
    public class DefaultTagWorkerFactory : ITagWorkerFactory {
        /// <summary>Internal map to keep track of tags and associated tagworkers</summary>
        private IDictionary<String, String> map;

        public DefaultTagWorkerFactory() {
            this.map = new ConcurrentDictionary<String, String>();
            RegisterDefaultHtmlTagWorkers();
        }

        /// <exception cref="iText.Html2pdf.Exceptions.NoTagWorkerFoundException"/>
        public virtual ITagWorker GetTagWorkerInstance(IElementNode tag, ProcessorContext context) {
            //Get Tag Worker class name
            String tagWorkerClassName = map.Get(tag.Name());
            if (tagWorkerClassName == null) {
                //TODO:Log the fact that no instance could be found
                //throw new NoTagWorkerFoundException(NoTagWorkerFoundException.NoTagWorkerRegistered);
                return null;
            }
            //Use reflection to create an instance
            try {
                Type c = System.Type.GetType(tagWorkerClassName);
                ConstructorInfo ctor = c.GetConstructor(new Type[] { typeof(IElementNode), typeof(ProcessorContext)});
                ITagWorker res = (ITagWorker)ctor.Invoke(new object[] { tag, context });
                return res;
            }
            catch (Exception e) {
                throw new NoTagWorkerFoundException(NoTagWorkerFoundException.REFLECTION_IN_TAG_WORKER_FACTORY_IMPLEMENTATION_FAILED
                    );
            }
        }

        public virtual void RegisterTagWorker(String tag, String nameSpace) {
            map[tag] = nameSpace;
        }

        public virtual void RemovetagWorker(String tag) {
            map.JRemove(tag);
        }

        private void RegisterDefaultHtmlTagWorkers() {
            IDictionary<String, String> defaultMapping = DefaultTagMapping.GetDefaultTagWorkerMapping();
            foreach (KeyValuePair<String, String> ent in defaultMapping) {
                map[ent.Key] = ent.Value;
            }
        }
    }
}
