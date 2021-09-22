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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// A factory for creating
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// objects.
    /// </summary>
    public class DefaultCssApplierFactory : ICssApplierFactory {
        private static readonly ICssApplierFactory INSTANCE = new iText.Html2pdf.Css.Apply.Impl.DefaultCssApplierFactory
            ();

        /// <summary>The default mapping of CSS keywords and CSS appliers.</summary>
        private readonly TagProcessorMapping defaultMapping;

        /// <summary>
        /// Creates a new
        /// <see cref="DefaultCssApplierFactory"/>
        /// instance.
        /// </summary>
        public DefaultCssApplierFactory() {
            defaultMapping = DefaultTagCssApplierMapping.GetDefaultCssApplierMapping();
        }

        /// <summary>
        /// Gets
        /// <see cref="DefaultCssApplierFactory"/>
        /// instance.
        /// </summary>
        /// <returns>default instance that is used if custom css appliers are not configured</returns>
        public static ICssApplierFactory GetInstance() {
            return INSTANCE;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.ICssApplierFactory#getCssApplier(com.itextpdf.html2pdf.html.node.IElementNode)
        */
        public ICssApplier GetCssApplier(IElementNode tag) {
            ICssApplier cssApplier = GetCustomCssApplier(tag);
            if (cssApplier == null) {
                Type cssApplierClass = GetCssApplierClass(defaultMapping, tag);
                if (cssApplierClass != null) {
                    try {
                        return (ICssApplier)System.Activator.CreateInstance(cssApplierClass);
                    }
                    catch (Exception) {
                        throw new CssApplierInitializationException(CssApplierInitializationException.REFLECTION_FAILED, cssApplierClass
                            .FullName, tag.Name());
                    }
                }
            }
            return cssApplier;
        }

        /// <summary>Gets a custom CSS applier.</summary>
        /// <remarks>
        /// Gets a custom CSS applier.
        /// This method needs to be overridden because the default CSS applier
        /// factory will always return
        /// <see langword="null"/>.
        /// </remarks>
        /// <param name="tag">the key</param>
        /// <returns>the custom CSS applier</returns>
        public virtual ICssApplier GetCustomCssApplier(IElementNode tag) {
            return null;
        }

        internal virtual TagProcessorMapping GetDefaultMapping() {
            return defaultMapping;
        }

        /// <summary>Gets the css applier class.</summary>
        /// <param name="mapping">the mapping</param>
        /// <param name="tag">the tag</param>
        /// <returns>the css applier class</returns>
        private static Type GetCssApplierClass(TagProcessorMapping mapping, IElementNode tag) {
            Type cssApplierClass = null;
            String display = tag.GetStyles() != null ? tag.GetStyles().Get(CssConstants.DISPLAY) : null;
            if (display != null) {
                cssApplierClass = mapping.GetMapping(tag.Name(), display);
            }
            if (cssApplierClass == null) {
                cssApplierClass = mapping.GetMapping(tag.Name());
            }
            return cssApplierClass;
        }
    }
}
