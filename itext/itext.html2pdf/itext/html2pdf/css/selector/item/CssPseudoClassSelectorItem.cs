/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

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
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Selector.Item {
    /// <summary>
    /// <see cref="ICssSelectorItem"/>
    /// implementation for pseudo class selectors.
    /// </summary>
    public class CssPseudoClassSelectorItem : ICssSelectorItem {
        /// <summary>The pseudo class.</summary>
        private String pseudoClass;

        /// <summary>The arguments.</summary>
        private String arguments;

        /// <summary>The nth child A.</summary>
        private int nthChildA;

        /// <summary>The nth child B.</summary>
        private int nthChildB;

        /// <summary>Creates a new <code>CssPseudoClassSelectorItem<code> instance.</summary>
        /// <param name="pseudoClass">the pseudo class name</param>
        public CssPseudoClassSelectorItem(String pseudoClass) {
            int indexOfParentheses = pseudoClass.IndexOf('(');
            if (indexOfParentheses == -1) {
                this.pseudoClass = pseudoClass;
                this.arguments = "";
            }
            else {
                this.pseudoClass = pseudoClass.JSubstring(0, indexOfParentheses);
                this.arguments = pseudoClass.JSubstring(indexOfParentheses + 1, pseudoClass.Length - 1).Trim();
                GetNthChildArguments();
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.selector.item.ICssSelectorItem#getSpecificity()
        */
        public virtual int GetSpecificity() {
            return CssSpecificityConstants.CLASS_SPECIFICITY;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.selector.item.ICssSelectorItem#matches(com.itextpdf.html2pdf.html.node.INode)
        */
        public virtual bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode) {
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

        /* (non-Javadoc)
        * @see java.lang.Object#toString()
        */
        public override String ToString() {
            return ":" + pseudoClass + (!String.IsNullOrEmpty(arguments) ? "(" + arguments + ")" : "");
        }

        /// <summary>Gets the all the siblings of a child node.</summary>
        /// <param name="child">the child node</param>
        /// <returns>the sibling nodes</returns>
        private IList<INode> GetAllChildren(INode child) {
            INode parentElement = child.ParentNode();
            if (parentElement != null) {
                IList<INode> childrenUnmodifiable = parentElement.ChildNodes();
                IList<INode> children = new List<INode>(childrenUnmodifiable.Count);
                foreach (INode iNode in childrenUnmodifiable) {
                    if (iNode is IElementNode) {
                        children.Add(iNode);
                    }
                }
                return children;
            }
            return JavaCollectionsUtil.EmptyList<INode>();
        }

        /// <summary>Gets the nth child arguments.</summary>
        /// <returns>the nth child arguments</returns>
        private void GetNthChildArguments() {
            if (arguments.Matches("((-|\\+)?[0-9]*n(\\s*(-|\\+)\\s*[0-9]+)?|(-|\\+)?[0-9]+|odd|even)")) {
                if (arguments.Equals("odd")) {
                    this.nthChildA = 2;
                    this.nthChildB = 1;
                }
                else {
                    if (arguments.Equals("even")) {
                        this.nthChildA = 2;
                        this.nthChildB = 0;
                    }
                    else {
                        int indexOfN = arguments.IndexOf('n');
                        if (indexOfN == -1) {
                            this.nthChildA = 0;
                            this.nthChildB = System.Convert.ToInt32(arguments);
                        }
                        else {
                            String aParticle = arguments.JSubstring(0, indexOfN).Trim();
                            if (String.IsNullOrEmpty(aParticle)) {
                                this.nthChildA = 0;
                            }
                            else {
                                if (aParticle.Length == 1 && !char.IsDigit(aParticle[0])) {
                                    this.nthChildA = aParticle.Equals("+") ? 1 : -1;
                                }
                                else {
                                    this.nthChildA = System.Convert.ToInt32(aParticle);
                                }
                            }
                            String bParticle = arguments.Substring(indexOfN + 1).Trim();
                            if (!String.IsNullOrEmpty(bParticle)) {
                                this.nthChildB = System.Convert.ToInt32(bParticle[0] + bParticle.Substring(1).Trim());
                            }
                            else {
                                this.nthChildB = 0;
                            }
                        }
                    }
                }
            }
            else {
                this.nthChildA = 0;
                this.nthChildB = 0;
            }
        }

        /// <summary>Resolves the nth child.</summary>
        /// <param name="node">a node</param>
        /// <param name="children">the children</param>
        /// <returns>true, if successful</returns>
        private bool ResolveNthChild(INode node, IList<INode> children) {
            if (!children.Contains(node)) {
                return false;
            }
            if (this.nthChildA > 0) {
                int temp = children.IndexOf(node) + 1 - this.nthChildB;
                return temp >= 0 ? temp % this.nthChildA == 0 : false;
            }
            else {
                if (this.nthChildA < 0) {
                    int temp = children.IndexOf(node) + 1 - this.nthChildB;
                    return temp <= 0 ? temp % this.nthChildA == 0 : false;
                }
                else {
                    return (children.IndexOf(node) + 1) - this.nthChildB == 0;
                }
            }
        }
    }
}
