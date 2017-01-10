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
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Html.Node;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Impl {
    public class SpanTagCssApplier : ICssApplier {
        public virtual void Apply(ProcessorContext context, IElementNode element, ITagWorker tagWorker) {
            SpanTagWorker spanTagWorker = (SpanTagWorker)tagWorker;
            IDictionary<String, String> cssStyles = element.GetStyles();
            foreach (IPropertyContainer child in spanTagWorker.GetOwnLeafElements()) {
                ApplyChildElementStyles(child, cssStyles, context, element);
            }
            VerticalAlignmentApplierUtil.ApplyVerticalAlignmentForInlines(cssStyles, context, element, spanTagWorker.GetAllElements
                ());
            if (cssStyles.ContainsKey(CssConstants.OPACITY)) {
                foreach (IPropertyContainer elem in spanTagWorker.GetAllElements()) {
                    if (elem is Text && !elem.HasProperty(Property.OPACITY)) {
                        OpacityApplierUtil.ApplyOpacity(cssStyles, context, elem);
                    }
                }
            }
        }

        private void ApplyChildElementStyles(IPropertyContainer element, IDictionary<String, String> css, ProcessorContext
             context, IElementNode elementNode) {
            FontStyleApplierUtil.ApplyFontStyles(css, context, element);
            //TODO: Background-applying currently doesn't work in html way for spans inside other spans.
            BackgroundApplierUtil.ApplyBackground(css, context, element);
            //TODO: Border-applying currently doesn't work in html way for spans inside other spans.
            BorderStyleApplierUtil.ApplyBorders(css, context, element);
            HyphenationApplierUtil.ApplyHyphenation(css, context, elementNode, element);
            //TODO: Margins-applying currently doesn't work in html way for spans inside other spans. (see SpanTest#spanTest07)
            MarginApplierUtil.ApplyMargins(css, context, element);
            PositionApplierUtil.ApplyPosition(css, context, element);
        }
    }
}
