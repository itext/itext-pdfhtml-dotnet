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
using System.Text.RegularExpressions;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Selector;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;

namespace iText.Html2pdf.Css {
    public class CssRuleSet : CssStatement {
        private static readonly Regex importantMatcher = iText.IO.Util.StringUtil.RegexCompile(".*!\\s*important$"
            );

        private CssSelector selector;

        private IList<CssDeclaration> normalDeclarations;

        private IList<CssDeclaration> importantDeclarations;

        public CssRuleSet(CssSelector selector, IList<CssDeclaration> declarations) {
            this.selector = selector;
            this.normalDeclarations = new List<CssDeclaration>();
            this.importantDeclarations = new List<CssDeclaration>();
            SplitDeclarationsIntoNormalAndImportant(declarations);
        }

        public override IList<iText.Html2pdf.Css.CssRuleSet> GetCssRuleSets(IElementNode element, MediaDeviceDescription
             deviceDescription) {
            if (selector.Matches(element)) {
                return JavaCollectionsUtil.SingletonList(this);
            }
            else {
                return base.GetCssRuleSets(element, deviceDescription);
            }
        }

        public override String ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(selector.ToString());
            sb.Append(" {\n");
            for (int i = 0; i < normalDeclarations.Count; i++) {
                if (i > 0) {
                    sb.Append(";").Append("\n");
                }
                CssDeclaration declaration = normalDeclarations[i];
                sb.Append("    ").Append(declaration.ToString());
            }
            for (int i_1 = 0; i_1 < importantDeclarations.Count; i_1++) {
                if (i_1 > 0 || normalDeclarations.Count > 0) {
                    sb.Append(";").Append("\n");
                }
                CssDeclaration declaration = importantDeclarations[i_1];
                sb.Append("    ").Append(declaration.ToString()).Append(" !important");
            }
            sb.Append("\n}");
            return sb.ToString();
        }

        public virtual CssSelector GetSelector() {
            return selector;
        }

        public virtual IList<CssDeclaration> GetNormalDeclarations() {
            return normalDeclarations;
        }

        public virtual IList<CssDeclaration> GetImportantDeclarations() {
            return importantDeclarations;
        }

        private void SplitDeclarationsIntoNormalAndImportant(IList<CssDeclaration> declarations) {
            foreach (CssDeclaration declaration in declarations) {
                int exclIndex = declaration.GetExpression().IndexOf('!');
                if (exclIndex > 0 && iText.IO.Util.StringUtil.Match(importantMatcher, declaration.GetExpression()).Success
                    ) {
                    importantDeclarations.Add(new CssDeclaration(declaration.GetProperty(), declaration.GetExpression().JSubstring
                        (0, exclIndex).Trim()));
                }
                else {
                    normalDeclarations.Add(declaration);
                }
            }
        }
    }
}
