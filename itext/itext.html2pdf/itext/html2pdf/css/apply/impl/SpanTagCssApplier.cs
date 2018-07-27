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
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for Span elements.
    /// </summary>
    public class SpanTagCssApplier : ICssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.ICssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public virtual void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            SpanTagWorker spanTagWorker = (SpanTagWorker)tagWorker;
            IDictionary<String, String> cssStyles = stylesContainer.GetStyles();
            foreach (IPropertyContainer child in spanTagWorker.GetOwnLeafElements()) {
                // Workaround for form fields so that SpanTagCssApplier does not apply its font-size to the child.
                // Form fields have their own CSS applier // TODO remove when form fields are not leaf elements anymore
                if (!(child is IFormField)) {
                    ApplyChildElementStyles(child, cssStyles, context, stylesContainer);
                }
            }
            VerticalAlignmentApplierUtil.ApplyVerticalAlignmentForInlines(cssStyles, context, stylesContainer, spanTagWorker
                .GetAllElements());
            if (cssStyles.ContainsKey(CssConstants.OPACITY)) {
                foreach (IPropertyContainer elem in spanTagWorker.GetAllElements()) {
                    if (elem is Text && !elem.HasProperty(Property.OPACITY)) {
                        OpacityApplierUtil.ApplyOpacity(cssStyles, context, elem);
                    }
                }
            }
            // TODO as for now spans are flattened, let's at least make kids of floating spans floating too
            String floatVal = cssStyles.Get(CssConstants.FLOAT);
            if (floatVal != null && !CssConstants.NONE.Equals(floatVal)) {
                foreach (IPropertyContainer elem in spanTagWorker.GetAllElements()) {
                    FloatPropertyValue? kidFloatVal = elem.GetProperty<FloatPropertyValue?>(Property.FLOAT);
                    if (kidFloatVal == null || FloatPropertyValue.NONE.Equals(kidFloatVal)) {
                        FloatApplierUtil.ApplyFloating(cssStyles, context, elem);
                    }
                }
            }
        }

        /// <summary>Applies styles to child elements.</summary>
        /// <param name="element">the element</param>
        /// <param name="css">the CSS mapping</param>
        /// <param name="context">the processor context</param>
        /// <param name="stylesContainer">the styles container</param>
        protected internal virtual void ApplyChildElementStyles(IPropertyContainer element, IDictionary<String, String
            > css, ProcessorContext context, IStylesContainer stylesContainer) {
            FontStyleApplierUtil.ApplyFontStyles(css, context, stylesContainer, element);
            //TODO: Background-applying currently doesn't work in html way for spans inside other spans.
            BackgroundApplierUtil.ApplyBackground(css, context, element);
            //TODO: Border-applying currently doesn't work in html way for spans inside other spans.
            BorderStyleApplierUtil.ApplyBorders(css, context, element);
            OutlineApplierUtil.ApplyOutlines(css, context, element);
            HyphenationApplierUtil.ApplyHyphenation(css, context, stylesContainer, element);
            //TODO: Margins-applying currently doesn't work in html way for spans inside other spans. (see SpanTest#spanTest07)
            MarginApplierUtil.ApplyMargins(css, context, element);
            PositionApplierUtil.ApplyPosition(css, context, element);
            FloatApplierUtil.ApplyFloating(css, context, element);
            PaddingApplierUtil.ApplyPaddings(css, context, element);
        }
    }
}
