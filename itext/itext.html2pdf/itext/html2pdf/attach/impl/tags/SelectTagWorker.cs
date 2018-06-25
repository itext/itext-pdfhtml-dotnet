using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>select</c>
    /// element.
    /// </summary>
    public class SelectTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The form element.</summary>
        private AbstractSelectField selectElement;

        /// <summary>The display.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="SelectTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public SelectTagWorker(IElementNode element, ProcessorContext context) {
            String name = context.GetFormFieldNameResolver().ResolveFormName(element.GetAttribute(AttributeConstants.NAME
                ));
            bool multipleAttr = element.GetAttribute(AttributeConstants.MULTIPLE) != null;
            int? sizeAttr = CssUtils.ParseInteger(element.GetAttribute(AttributeConstants.SIZE));
            int size = GetSelectSize(sizeAttr, multipleAttr);
            if (size > 1 || multipleAttr) {
                selectElement = new ListBoxField(name, size, multipleAttr);
            }
            else {
                selectElement = new ComboBoxField(name);
            }
            selectElement.SetProperty(Html2PdfProperty.FORM_FIELD_FLATTEN, !context.IsCreateAcroForm());
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return content == null || String.IsNullOrEmpty(content.Trim());
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            if (childTagWorker is OptionTagWorker || childTagWorker is OptGroupTagWorker) {
                if (childTagWorker.GetElementResult() is IBlockElement) {
                    selectElement.AddOption((IBlockElement)childTagWorker.GetElementResult());
                    return true;
                }
            }
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return selectElement;
        }

        public virtual String GetDisplay() {
            return display;
        }

        private int GetSelectSize(int? size, bool multiple) {
            if (size != null && size > 0) {
                return (int)size;
            }
            return multiple ? 4 : 1;
        }
    }
}
