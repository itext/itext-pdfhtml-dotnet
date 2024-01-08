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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
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
        private readonly TagProcessorMapping<DefaultTagCssApplierMapping.ICssApplierCreator> defaultMapping;

        /// <summary>
        /// Creates a new
        /// <see cref="DefaultCssApplierFactory"/>
        /// instance.
        /// </summary>
        public DefaultCssApplierFactory() {
            defaultMapping = new DefaultTagCssApplierMapping().GetDefaultCssApplierMapping();
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
                DefaultTagCssApplierMapping.ICssApplierCreator cssApplierCreator = GetCssApplierCreator(defaultMapping, tag
                    );
                if (cssApplierCreator == null) {
                    return null;
                }
                return cssApplierCreator();
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

        internal virtual TagProcessorMapping<DefaultTagCssApplierMapping.ICssApplierCreator> GetDefaultMapping() {
            return defaultMapping;
        }

        /// <summary>Gets the css applier class.</summary>
        /// <param name="mapping">the mapping</param>
        /// <param name="tag">the tag</param>
        /// <returns>the css applier class creator</returns>
        private static DefaultTagCssApplierMapping.ICssApplierCreator GetCssApplierCreator(TagProcessorMapping<DefaultTagCssApplierMapping.ICssApplierCreator
            > mapping, IElementNode tag) {
            DefaultTagCssApplierMapping.ICssApplierCreator cssApplierCreator = null;
            String display = tag.GetStyles() != null ? tag.GetStyles().Get(CssConstants.DISPLAY) : null;
            if (display != null) {
                cssApplierCreator = (DefaultTagCssApplierMapping.ICssApplierCreator)mapping.GetMapping(tag.Name(), display
                    );
            }
            if (cssApplierCreator == null) {
                cssApplierCreator = (DefaultTagCssApplierMapping.ICssApplierCreator)mapping.GetMapping(tag.Name());
            }
            return cssApplierCreator;
        }
    }
}
