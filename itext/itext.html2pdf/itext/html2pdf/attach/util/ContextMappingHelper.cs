using iText.Html2pdf.Attach;
using iText.Svg.Processors.Impl;

namespace iText.Html2pdf.Attach.Util {
    public class ContextMappingHelper {
        public static SvgConverterProperties MapToSvgConverterProperties(ProcessorContext context) {
            SvgConverterProperties svgConverterProperties = new SvgConverterProperties();
            svgConverterProperties.SetFontProvider(context.GetFontProvider()).SetBaseUri(context.GetBaseUri()).SetMediaDeviceDescription
                (context.GetDeviceDescription());
            return svgConverterProperties;
        }
    }
}
