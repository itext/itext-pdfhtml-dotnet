using System;
using System.Collections.Generic;
using iText.Commons.Utils;
using iText.Html2pdf.Html;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Svg;
using iText.Svg.Element;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>
    /// Helper class for resolving the alternate description of an
    /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>.
    /// </summary>
    /// <remarks>
    /// Helper class for resolving the alternate description of an
    /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>.
    /// The alternate description is resolved in the following order:
    /// 1) alt attribute
    /// 2) title attribute
    /// 3) aria-label attribute
    /// 4) aria-labelledby attribute
    /// <para />
    /// If none of the above attributes are present, the alternate description is not set.
    /// </remarks>
    public class AlternateDescriptionResolver {
        private static readonly IList<String> ALTERNATIVE_DESCRIPTION_RESOLUTION_ORDER = JavaUtil.ArraysAsList(AttributeConstants
            .ALT, AttributeConstants.ARIA_LABEL, AttributeConstants.TITLE);

        /// <summary>
        /// Creates a new
        /// <see cref="AlternateDescriptionResolver"/>
        /// instance.
        /// </summary>
        public AlternateDescriptionResolver() {
        }

        // Empty constructor
        /// <summary>
        /// Resolves the alternate description of the
        /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>
        /// based on the attributes of the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>.
        /// </summary>
        /// <param name="accessibleElement">
        /// the
        /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>
        /// to which the alternate description should be applied.
        /// </param>
        /// <param name="element">
        /// the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// from which the alternate description should be resolved.
        /// </param>
        public virtual void Resolve(IAccessibleElement accessibleElement, IElementNode element) {
            bool hasManagedToResolveSpecificImplementation = false;
            if (TagConstants.SVG.EqualsIgnoreCase(element.Name())) {
                hasManagedToResolveSpecificImplementation = ResolveSvg((SvgImage)accessibleElement, element);
            }
            else {
                if (TagConstants.A.EqualsIgnoreCase(element.Name())) {
                    hasManagedToResolveSpecificImplementation = ResolveLink(accessibleElement, element);
                }
            }
            if (hasManagedToResolveSpecificImplementation) {
                return;
            }
            ResolveFallback(accessibleElement, element);
        }

        /// <summary>
        /// Resolves the alternate description of the
        /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>
        /// based on the attributes of the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>.
        /// </summary>
        /// <remarks>
        /// Resolves the alternate description of the
        /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>
        /// based on the attributes of the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// . If the link has an img tag as a child, the alt attribute of the img tag is used as the
        /// alternate description.
        /// </remarks>
        /// <param name="accessibleElement">
        /// the
        /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>
        /// to which the alternate description should be applied.
        /// </param>
        /// <param name="element">
        /// the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// from which the alternate description should be resolved.
        /// </param>
        /// <returns>
        /// 
        /// <see langword="true"/>
        /// if the alternate description was resolved,
        /// <see langword="false"/>
        /// otherwise.
        /// </returns>
        protected internal virtual bool ResolveLink(IAccessibleElement accessibleElement, IElementNode element) {
            IList<INode> children = element.ChildNodes();
            // If there is an img tag under the link then prefer the alt attribute as a link description.
            if (children.Count == 1 && children[0].ChildNodes().IsEmpty() && children[0] is JsoupElementNode && ((JsoupElementNode
                )children[0]).GetAttribute(AttributeConstants.ALT) != null) {
                String result = ((JsoupElementNode)children[0]).GetAttribute(AttributeConstants.ALT);
                accessibleElement.GetAccessibilityProperties().SetAlternateDescription(result);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Resolves the alternate description of the
        /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>
        /// based on the attributes of the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// in a fallback manner.
        /// </summary>
        /// <param name="accessibleElement">
        /// the
        /// <see cref="iText.Layout.Tagging.IAccessibleElement"/>
        /// to which the alternate description should be applied.
        /// </param>
        /// <param name="element">
        /// the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// from which the alternate description should be resolved.
        /// </param>
        protected internal virtual void ResolveFallback(IAccessibleElement accessibleElement, IElementNode element
            ) {
            foreach (String s in ALTERNATIVE_DESCRIPTION_RESOLUTION_ORDER) {
                String alt = element.GetAttribute(s);
                if (alt != null && !String.IsNullOrEmpty(alt)) {
                    accessibleElement.GetAccessibilityProperties().SetAlternateDescription(alt);
                    break;
                }
            }
        }

        /// <summary>
        /// Resolves the alternate description of the
        /// <see cref="iText.Svg.Element.SvgImage"/>
        /// based on the attributes of the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>.
        /// </summary>
        /// <remarks>
        /// Resolves the alternate description of the
        /// <see cref="iText.Svg.Element.SvgImage"/>
        /// based on the attributes of the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>.
        /// <para />
        /// If the alternate description is not found in the attributes, it is searched for the
        /// <c>&lt;descr&gt;</c>
        /// tag the
        /// child nodes.
        /// </remarks>
        /// <param name="accessibleElement">
        /// the
        /// <see cref="iText.Svg.Element.SvgImage"/>
        /// to which the alternate description should be applied.
        /// </param>
        /// <param name="element">
        /// the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// from which the alternate description should be resolved.
        /// </param>
        /// <returns>
        /// 
        /// <see langword="true"/>
        /// if the alternate description was resolved,
        /// <see langword="false"/>
        /// otherwise.
        /// </returns>
        protected internal virtual bool ResolveSvg(SvgImage accessibleElement, IElementNode element) {
            foreach (INode childNode in element.ChildNodes()) {
                if (!(childNode is IElementNode)) {
                    continue;
                }
                IElementNode childElement = (IElementNode)childNode;
                if (SvgConstants.Tags.DESC.EqualsIgnoreCase(childElement.Name())) {
                    if (childElement.ChildNodes().IsEmpty()) {
                        break;
                    }
                    INode firstChild = childElement.ChildNodes()[0];
                    if (firstChild is JsoupTextNode) {
                        JsoupTextNode textNode = (JsoupTextNode)firstChild;
                        accessibleElement.GetAccessibilityProperties().SetAlternateDescription(textNode.WholeText());
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
