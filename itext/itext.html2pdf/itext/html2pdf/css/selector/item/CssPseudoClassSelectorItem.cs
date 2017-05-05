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
using iText.Html2pdf.Css.Pseudo;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    public class CssPseudoClassSelectorItem : ICssSelectorItem {
        private String pseudoClass;

        private String arguments;

        public CssPseudoClassSelectorItem(String pseudoClass) {
            int indexOfParentheses = pseudoClass.IndexOf('(');
            if (indexOfParentheses == -1) {
                this.pseudoClass = pseudoClass;
                this.arguments = null;
            }
            else {
                this.pseudoClass = pseudoClass.JSubstring(0, indexOfParentheses);
                this.arguments = pseudoClass.JSubstring(indexOfParentheses + 1, pseudoClass.Length - 1).ToLowerInvariant();
            }
        }

        public virtual int GetSpecificity() {
            return CssSpecificityConstants.CLASS_SPECIFICITY;
        }

        public virtual bool Matches(INode node) {
            if (!(node is IElementNode) || node is CssPseudoElementNode) {
                return false;
            }
            IList<INode> children = GetAllChildren(node);
            switch (pseudoClass) {
                case "first-child": {
                    return children.IsEmpty() ? false : node.Equals(children[0]);
                }

                case "last-child": {
                    return children.IsEmpty() ? false : node.Equals(children[children.Count - 1]);
                }

                case "nth-child": {
                    return children.IsEmpty() ? false : ResolveNthChild(node, children);
                }

                default: {
                    return false;
                }
            }
        }

        public override String ToString() {
            return ":" + pseudoClass + (arguments != null ? new String("(" + arguments + ")") : "");
        }

        private IList<INode> GetAllChildren(INode child) {
            INode parentElement = child.ParentNode();
            IList<INode> childrenUnmodifiable = parentElement.ChildNodes();
            IList<INode> children = new List<INode>(childrenUnmodifiable.Count);
            foreach (INode iNode in childrenUnmodifiable) {
                if (iNode is IElementNode) {
                    children.Add(iNode);
                }
            }
            return children;
        }

        private bool ResolveNthChild(INode node, IList<INode> children) {
            if (arguments.Matches("\\s*((-|\\+)?[0-9]*n(\\s*(-|\\+)\\s*[0-9]+)?|(-|\\+)?[0-9]+|odd|even)\\s*")) {
                int a;
                int b;
                bool bIsPositive = true;
                if (arguments.Matches("\\s*(odd|even)\\s*")) {
                    a = 2;
                    b = arguments.Matches("\\s*odd\\s*") ? 1 : 0;
                }
                else {
                    int indexOfN = arguments.IndexOf('n');
                    if (indexOfN == -1) {
                        a = children.Count;
                        b = System.Convert.ToInt32(arguments);
                    }
                    else {
                        a = System.Convert.ToInt32(arguments.JSubstring(0, indexOfN).Trim());
                        String[] bParticle = iText.IO.Util.StringUtil.Split(arguments.Substring(indexOfN + 1).Trim(), "\\s+");
                        bIsPositive = bParticle[0].Equals("+") ? true : false;
                        b = System.Convert.ToInt32(bParticle[1]);
                    }
                }
                if (bIsPositive) {
                    return (children.IndexOf(node) + 1) % a == b;
                }
                else {
                    return (children.IndexOf(node) + 1) % a == a - b;
                }
            }
            else {
                return false;
            }
        }
    }
}
