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
using System.Text;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Resolve.Shorthand;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;

namespace iText.Html2pdf.Css {
    public class CssStyleSheet {
        private IList<CssStatement> statements;

        public CssStyleSheet() {
            statements = new List<CssStatement>();
        }

        public virtual void AddStatement(CssStatement statement) {
            statements.Add(statement);
        }

        // TODO move this functionality to the parser (parse into)
        public virtual void AppendCssStyleSheet(iText.Html2pdf.Css.CssStyleSheet anotherCssStyleSheet) {
            statements.AddAll(anotherCssStyleSheet.statements);
        }

        public override String ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (CssStatement statement in statements) {
                if (sb.Length > 0) {
                    sb.Append("\n");
                }
                sb.Append(statement.ToString());
            }
            return sb.ToString();
        }

        public virtual IList<CssStatement> GetStatements() {
            return JavaCollectionsUtil.UnmodifiableList(statements);
        }

        public virtual IList<CssDeclaration> GetCssDeclarations(IElementNode element, MediaDeviceDescription deviceDescription
            ) {
            IList<CssRuleSet> ruleSets = GetCssRuleSets(element, deviceDescription);
            IDictionary<String, CssDeclaration> declarations = new LinkedDictionary<String, CssDeclaration>();
            JavaCollectionsUtil.Sort(ruleSets, new CssRuleSetComparator());
            foreach (CssRuleSet ruleSet in ruleSets) {
                PopulateDeclarationsMap(ruleSet.GetNormalDeclarations(), declarations);
            }
            foreach (CssRuleSet ruleSet_1 in ruleSets) {
                PopulateDeclarationsMap(ruleSet_1.GetImportantDeclarations(), declarations);
            }
            return new List<CssDeclaration>(declarations.Values);
        }

        private static void PopulateDeclarationsMap(IList<CssDeclaration> declarations, IDictionary<String, CssDeclaration
            > map) {
            foreach (CssDeclaration declaration in declarations) {
                IShorthandResolver shorthandResolver = ShorthandResolverFactory.GetShorthandResolver(declaration.GetProperty
                    ());
                if (shorthandResolver == null) {
                    map[declaration.GetProperty()] = declaration;
                }
                else {
                    IList<CssDeclaration> resolvedShorthandProps = shorthandResolver.ResolveShorthand(declaration.GetExpression
                        ());
                    foreach (CssDeclaration resolvedProp in resolvedShorthandProps) {
                        map[resolvedProp.GetProperty()] = resolvedProp;
                    }
                }
            }
        }

        private IList<CssRuleSet> GetCssRuleSets(IElementNode element, MediaDeviceDescription deviceDescription) {
            IList<CssRuleSet> ruleSets = new List<CssRuleSet>();
            foreach (CssStatement statement in statements) {
                ruleSets.AddAll(statement.GetCssRuleSets(element, deviceDescription));
            }
            return ruleSets;
        }
    }
}
