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
using Java.Lang;
using iText.Html2pdf.Exceptions;

namespace iText.Html2pdf.Css.Apply {
    /// <summary>Created by SamuelHuylebroeck on 11/30/2016.</summary>
    public class DefaultCssApplierFactory : ICssApplierFactory {
        private IDictionary<String, Type> map;

        public DefaultCssApplierFactory() {
            this.map = new ConcurrentDictionary<String, Type>();
            this.RegisterDefaultCssAppliers();
        }

        public virtual ICssApplier GetCssApplier(String tag) {
            //Get css applier classname
            Type cssApplierClass = map.Get(tag);
            if (cssApplierClass == null) {
                //TODO add logging
                return null;
            }
            //Use reflection to create an instance
            try {
                ICssApplier res = (ICssApplier)System.Activator.CreateInstance(cssApplierClass);
                return res;
            }
            catch (MemberAccessException) {
                throw new NoCssApplierFoundException(NoCssApplierFoundException.ReflectionFailed, cssApplierClass.FullName
                    , tag);
            }
            catch (InstantiationException) {
                throw new NoCssApplierFoundException(NoCssApplierFoundException.ReflectionFailed, cssApplierClass.FullName
                    , tag);
            }
        }

        public virtual void RegisterCssApplier(String tag, Type classToRegister) {
            this.map[tag] = classToRegister;
        }

        public virtual void RemoveCssApplier(String tag) {
            this.map.JRemove(tag);
        }

        private void RegisterDefaultCssAppliers() {
            IDictionary<String, Type> defaultMapping = DefaultTagCssApplierMapping.GetDefaultCssApplierMapping();
            foreach (KeyValuePair<String, Type> ent in defaultMapping) {
                map[ent.Key] = ent.Value;
            }
        }
    }
}
