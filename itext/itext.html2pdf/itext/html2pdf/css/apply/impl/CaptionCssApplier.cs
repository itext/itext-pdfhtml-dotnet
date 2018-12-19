using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Apply.ICssApplier"/>
    /// implementation for a <code>caption</code>element.
    /// </summary>
    public class CaptionCssApplier : BlockCssApplier {
        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.apply.impl.BlockCssApplier#apply(com.itextpdf.html2pdf.attach.ProcessorContext, com.itextpdf.html2pdf.html.node.IStylesContainer, com.itextpdf.html2pdf.attach.ITagWorker)
        */
        public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
            ) {
            IDictionary<String, String> cssProps = stylesContainer.GetStyles();
            base.Apply(context, stylesContainer, tagWorker);
            if (CssConstants.BOTTOM.Equals(cssProps.Get(CssConstants.CAPTION_SIDE))) {
                IPropertyContainer container = tagWorker.GetElementResult();
                container.SetProperty(Property.CAPTION_SIDE, CaptionSide.BOTTOM);
            }
        }
    }
}
