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
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Parse;
using iText.Html2pdf.Css.Resolve.Shorthand;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;

namespace iText.Html2pdf.Css.Resolve {
    public class DefaultCssResolver : ICssResolver {
        private CssStyleSheet cssStyleSheet;

        private MediaDeviceDescription deviceDescription;

        public DefaultCssResolver(INode treeRoot, MediaDeviceDescription mediaDeviceDescription, ResourceResolver 
            resourceResolver) {
            this.deviceDescription = mediaDeviceDescription;
            CollectCssDeclarations(treeRoot, resourceResolver);
        }

        public virtual IDictionary<String, String> ResolveStyles(IElementNode element) {
            IList<CssDeclaration> nodeCssDeclarations = HtmlStylesToCssConverter.Convert(element);
            nodeCssDeclarations.AddAll(cssStyleSheet.GetCssDeclarations(element, deviceDescription));
            String styleAttribute = element.GetAttribute(AttributeConstants.STYLE);
            if (styleAttribute != null) {
                nodeCssDeclarations.AddAll(CssRuleSetParser.ParsePropertyDeclarations(styleAttribute));
            }
            IDictionary<String, String> elementStyles = CssDeclarationsToMap(nodeCssDeclarations);
            if (element.ParentNode() is IElementNode) {
                IDictionary<String, String> parentStyles = ((IElementNode)element.ParentNode()).GetStyles();
                if (parentStyles == null && !(element.ParentNode() is IDocumentNode)) {
                    ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Resolve.DefaultCssResolver));
                    logger.Error("Element parent styles are not resolved. Styles for current element might be incorrect.");
                }
                if (parentStyles != null) {
                    foreach (KeyValuePair<String, String> entry in parentStyles) {
                        MergeParentCssDeclaration(elementStyles, entry.Key, entry.Value);
                    }
                    String elementFontSize = elementStyles.Get(CssConstants.FONT_SIZE);
                    String parentFontSizeStr = parentStyles.Get(CssConstants.FONT_SIZE);
                    if (CssUtils.IsRelativeValue(elementFontSize) && parentFontSizeStr != null) {
                        float parentFontSize = CssUtils.ParseAbsoluteLength(parentFontSizeStr);
                        float absoluteFontSize = CssUtils.ParseRelativeValue(elementFontSize, parentFontSize);
                        elementStyles[CssConstants.FONT_SIZE] = absoluteFontSize + "pt";
                    }
                }
            }
            ICollection<String> keys = new HashSet<String>();
            foreach (KeyValuePair<String, String> entry_1 in elementStyles) {
                if (CssConstants.INITIAL.Equals(entry_1.Value) || CssConstants.INHERIT.Equals(entry_1.Value)) {
                    // if "inherit" is not resolved till now, parents don't have it
                    keys.Add(entry_1.Key);
                }
            }
            foreach (String key in keys) {
                elementStyles[key] = CssDefaults.GetDefaultValue(key);
            }
            return elementStyles;
        }

        private IDictionary<String, String> CssDeclarationsToMap(IList<CssDeclaration> nodeCssDeclarations) {
            IDictionary<String, String> stylesMap = new Dictionary<String, String>();
            foreach (CssDeclaration cssDeclaration in nodeCssDeclarations) {
                IShorthandResolver shorthandResolver = ShorthandResolverFactory.GetShorthandResolver(cssDeclaration.GetProperty
                    ());
                if (shorthandResolver == null) {
                    stylesMap[cssDeclaration.GetProperty()] = cssDeclaration.GetExpression();
                }
                else {
                    IList<CssDeclaration> resolvedShorthandProps = shorthandResolver.ResolveShorthand(cssDeclaration.GetExpression
                        ());
                    foreach (CssDeclaration resolvedProp in resolvedShorthandProps) {
                        stylesMap[resolvedProp.GetProperty()] = resolvedProp.GetExpression();
                    }
                }
            }
            return stylesMap;
        }

        private INode CollectCssDeclarations(INode rootNode, ResourceResolver resourceResolver) {
            cssStyleSheet = new CssStyleSheet();
            LinkedList<INode> q = new LinkedList<INode>();
            q.Add(rootNode);
            while (!q.IsEmpty()) {
                INode currentNode = q.JGetFirst();
                q.RemoveFirst();
                if (currentNode is IElementNode) {
                    IElementNode headChildElement = (IElementNode)currentNode;
                    if (headChildElement.Name().Equals(TagConstants.STYLE)) {
                        if (currentNode.ChildNodes().Count > 0 && currentNode.ChildNodes()[0] is IDataNode) {
                            String styleData = ((IDataNode)currentNode.ChildNodes()[0]).GetWholeData();
                            cssStyleSheet.AppendCssStyleSheet(CssStyleSheetParser.Parse(styleData));
                        }
                    }
                    else {
                        if (HtmlUtils.IsStyleSheetLink(headChildElement)) {
                            String styleSheetUri = headChildElement.GetAttribute(AttributeConstants.HREF);
                            try {
                                Stream stream = resourceResolver.RetrieveStyleSheet(styleSheetUri);
                                CssStyleSheet styleSheet = CssStyleSheetParser.Parse(stream);
                                String mediaAttribute = headChildElement.GetAttribute(AttributeConstants.MEDIA);
                                if (mediaAttribute != null && mediaAttribute.Length > 0) {
                                    IList<CssStatement> statements = styleSheet.GetStatements();
                                    CssMediaRule mediaRule = new CssMediaRule(CssRuleName.MEDIA, mediaAttribute);
                                    mediaRule.AddStatementsToBody(statements);
                                    styleSheet = new CssStyleSheet();
                                    styleSheet.AddStatement(mediaRule);
                                }
                                cssStyleSheet.AppendCssStyleSheet(styleSheet);
                            }
                            catch (System.IO.IOException exc) {
                                ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Resolve.DefaultCssResolver));
                                logger.Error(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_EXTERNAL_CSS_FILE, exc);
                            }
                        }
                    }
                }
                foreach (INode child in currentNode.ChildNodes()) {
                    if (child is IElementNode) {
                        q.Add(child);
                    }
                }
            }
            return null;
        }

        private void MergeParentCssDeclaration(IDictionary<String, String> styles, String cssProperty, String parentPropValue
            ) {
            String childPropValue = styles.Get(cssProperty);
            if ((childPropValue == null && CssInheritance.IsInheritable(cssProperty)) || CssConstants.INHERIT.Equals(childPropValue
                )) {
                styles[cssProperty] = parentPropValue;
            }
            else {
                if (CssConstants.TEXT_DECORATION.Equals(cssProperty)) {
                    // TODO Note! This property is formally not inherited, but the browsers behave very similar to inheritance here.
                    /* Text decorations on inline boxes are drawn across the entire element,
                    going across any descendant elements without paying any attention to their presence. */
                    // Also, when, for example, parent element has text-decoration:underline, and the child text-decoration:overline,
                    // then the text in the child will be both overline and underline. This is why the declarations are merged
                    // See TextDecorationTest#textDecoration01Test
                    styles[cssProperty] = CssPropertyMerger.MergeTextDecoration(childPropValue, parentPropValue);
                }
            }
        }
    }
}
