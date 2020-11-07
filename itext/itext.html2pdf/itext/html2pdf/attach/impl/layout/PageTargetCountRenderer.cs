using System;
using System.Collections.Generic;
using Common.Logging;
using iText.IO.Util;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// <see cref="iText.Layout.Renderer.TextRenderer"/>
    /// implementation for the page target-counter.
    /// </summary>
    public class PageTargetCountRenderer : TextRenderer {
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer
            ));

        private const String UNDEFINED_VALUE = "0";

        private readonly String target;

        /// <summary>
        /// Instantiates a new
        /// <see cref="PageTargetCountRenderer"/>.
        /// </summary>
        /// <param name="textElement">the text element</param>
        internal PageTargetCountRenderer(PageTargetCountElement textElement)
            : base(textElement) {
            target = textElement.GetTarget();
        }

        /// <summary><inheritDoc/></summary>
        public override LayoutResult Layout(LayoutContext layoutContext) {
            String previousText = GetText().ToString();
            int? page = TargetCounterHandler.GetPageByID(this, target);
            if (page == null) {
                SetText(UNDEFINED_VALUE);
            }
            else {
                SetText(page.ToString());
            }
            LayoutResult result = base.Layout(layoutContext);
            SetText(previousText);
            return result;
        }

        /// <summary><inheritDoc/></summary>
        public override void Draw(DrawContext drawContext) {
            if (!TargetCounterHandler.IsValueDefinedForThisID(this, target)) {
                LOGGER.Warn(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.CANNOT_RESOLVE_TARGET_COUNTER_VALUE
                    , target));
            }
            base.Draw(drawContext);
        }

        /// <summary><inheritDoc/></summary>
        public override IRenderer GetNextRenderer() {
            return this;
        }

        /// <summary><inheritDoc/></summary>
        protected override bool ResolveFonts(IList<IRenderer> addTo) {
            IList<IRenderer> dummyList = new List<IRenderer>();
            base.ResolveFonts(dummyList);
            SetProperty(Property.FONT, dummyList[0].GetProperty<Object>(Property.FONT));
            addTo.Add(this);
            return true;
        }
    }
}
