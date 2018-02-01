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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Parse;
using iText.Html2pdf.Css.Selector;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Selector.Item {
    /// <summary>
    /// <see cref="ICssSelectorItem"/>
    /// implementation for pseudo class selectors.
    /// </summary>
    public abstract class CssPseudoClassSelectorItem : ICssSelectorItem {
        /// <summary>The arguments.</summary>
        protected internal String arguments;

        /// <summary>The pseudo class.</summary>
        private String pseudoClass;

        /// <summary>
        /// Creates a new
        /// <see cref="CssPseudoClassSelectorItem"/>
        /// instance.
        /// </summary>
        /// <param name="pseudoClass">the pseudo class name</param>
        protected internal CssPseudoClassSelectorItem(String pseudoClass)
            : this(pseudoClass, "") {
        }

        protected internal CssPseudoClassSelectorItem(String pseudoClass, String arguments) {
            this.pseudoClass = pseudoClass;
            this.arguments = arguments;
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassSelectorItem Create(String fullSelectorString
            ) {
            int indexOfParentheses = fullSelectorString.IndexOf('(');
            String pseudoClass;
            String arguments;
            if (indexOfParentheses == -1) {
                pseudoClass = fullSelectorString;
                arguments = "";
            }
            else {
                pseudoClass = fullSelectorString.JSubstring(0, indexOfParentheses);
                arguments = fullSelectorString.JSubstring(indexOfParentheses + 1, fullSelectorString.Length - 1).Trim();
            }
            return Create(pseudoClass, arguments);
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassSelectorItem Create(String pseudoClass, String
             arguments) {
            switch (pseudoClass) {
                case CssConstants.FIRST_CHILD: {
                    return CssPseudoClassSelectorItem.FirstChildSelectorItem.GetInstance();
                }

                case CssConstants.FIRST_OF_TYPE: {
                    return CssPseudoClassSelectorItem.FirstOfTypeSelectorItem.GetInstance();
                }

                case CssConstants.LAST_CHILD: {
                    return CssPseudoClassSelectorItem.LastChildSelectorItem.GetInstance();
                }

                case CssConstants.LAST_OF_TYPE: {
                    return CssPseudoClassSelectorItem.LastOfTypeSelectorItem.GetInstance();
                }

                case CssConstants.NTH_CHILD: {
                    return new CssPseudoClassSelectorItem.NthChildSelectorItem(arguments);
                }

                case CssConstants.NTH_OF_TYPE: {
                    return new CssPseudoClassSelectorItem.NthOfTypeSelectorItem(arguments);
                }

                case CssConstants.NOT: {
                    CssSelector selector = new CssSelector(arguments);
                    foreach (ICssSelectorItem item in selector.GetSelectorItems()) {
                        if (item is CssPseudoClassSelectorItem.NotSelectorItem || item is CssPseudoElementSelectorItem) {
                            return null;
                        }
                    }
                    return new CssPseudoClassSelectorItem.NotSelectorItem(selector);
                }

                case CssConstants.LINK: {
                    return new CssPseudoClassSelectorItem.AlwaysApplySelectorItem(pseudoClass, arguments);
                }

                case CssConstants.ACTIVE:
                case CssConstants.FOCUS:
                case CssConstants.HOVER:
                case CssConstants.TARGET:
                case CssConstants.VISITED: {
                    return new CssPseudoClassSelectorItem.AlwaysNotApplySelectorItem(pseudoClass, arguments);
                }

                default: {
                    //Still unsupported, should be addressed in DEVSIX-1440
                    //case CssConstants.CHECKED:
                    //case CssConstants.DISABLED:
                    //case CssConstants.EMPTY:
                    //case CssConstants.ENABLED:
                    //case CssConstants.IN_RANGE:
                    //case CssConstants.INVALID:
                    //case CssConstants.LANG:
                    //case CssConstants.NTH_LAST_CHILD:
                    //case CssConstants.NTH_LAST_OF_TYPE:
                    //case CssConstants.ONLY_OF_TYPE:
                    //case CssConstants.ONLY_CHILD:
                    //case CssConstants.OPTIONAL:
                    //case CssConstants.OUT_OF_RANGE:
                    //case CssConstants.READ_ONLY:
                    //case CssConstants.READ_WRITE:
                    //case CssConstants.REQUIRED:
                    //case CssConstants.ROOT:
                    //case CssConstants.VALID:
                    return null;
                }
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
            return false;
        }

        /* (non-Javadoc)
        * @see java.lang.Object#toString()
        */
        public override String ToString() {
            return ":" + pseudoClass + (!String.IsNullOrEmpty(arguments) ? "(" + arguments + ")" : "");
        }

        public virtual String GetPseudoClass() {
            return pseudoClass;
        }

        private class ChildSelectorItem : CssPseudoClassSelectorItem {
            /// <summary>
            /// Creates a new
            /// <see cref="CssPseudoClassSelectorItem"/>
            /// instance.
            /// </summary>
            /// <param name="pseudoClass">the pseudo class name</param>
            internal ChildSelectorItem(String pseudoClass)
                : base(pseudoClass) {
            }

            internal ChildSelectorItem(String pseudoClass, String arguments)
                : base(pseudoClass, arguments) {
            }

            /// <summary>Gets the all the siblings of a child node.</summary>
            /// <param name="node">the child node</param>
            /// <returns>the sibling nodes</returns>
            internal virtual IList<INode> GetAllSiblings(INode node) {
                INode parentElement = node.ParentNode();
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

            /// <summary>Gets all siblings of a child node with the type of a child node.</summary>
            /// <param name="node">the child node</param>
            /// <returns>the sibling nodes with the type of a child node</returns>
            internal virtual IList<INode> GetAllSiblingsOfNodeType(INode node) {
                INode parentElement = node.ParentNode();
                if (parentElement != null) {
                    IList<INode> childrenUnmodifiable = parentElement.ChildNodes();
                    IList<INode> children = new List<INode>(childrenUnmodifiable.Count);
                    foreach (INode iNode in childrenUnmodifiable) {
                        if (iNode is IElementNode && ((IElementNode)iNode).Name().Equals(((IElementNode)node).Name())) {
                            children.Add(iNode);
                        }
                    }
                    return children;
                }
                return JavaCollectionsUtil.EmptyList<INode>();
            }
        }

        private class FirstChildSelectorItem : CssPseudoClassSelectorItem.ChildSelectorItem {
            private static readonly CssPseudoClassSelectorItem.FirstChildSelectorItem instance = new CssPseudoClassSelectorItem.FirstChildSelectorItem
                ();

            private FirstChildSelectorItem()
                : base(CssConstants.FIRST_CHILD) {
            }

            public static CssPseudoClassSelectorItem.FirstChildSelectorItem GetInstance() {
                return instance;
            }

            public override bool Matches(INode node) {
                if (!(node is IElementNode) || node is ICustomElementNode) {
                    return false;
                }
                IList<INode> children = GetAllSiblings(node);
                return !children.IsEmpty() && node.Equals(children[0]);
            }
        }

        private class FirstOfTypeSelectorItem : CssPseudoClassSelectorItem.ChildSelectorItem {
            private static readonly CssPseudoClassSelectorItem.FirstOfTypeSelectorItem instance = new CssPseudoClassSelectorItem.FirstOfTypeSelectorItem
                ();

            private FirstOfTypeSelectorItem()
                : base(CssConstants.FIRST_OF_TYPE) {
            }

            public static CssPseudoClassSelectorItem.FirstOfTypeSelectorItem GetInstance() {
                return instance;
            }

            public override bool Matches(INode node) {
                if (!(node is IElementNode) || node is ICustomElementNode) {
                    return false;
                }
                IList<INode> children = GetAllSiblingsOfNodeType(node);
                return !children.IsEmpty() && node.Equals(children[0]);
            }
        }

        private class LastChildSelectorItem : CssPseudoClassSelectorItem.ChildSelectorItem {
            private static readonly CssPseudoClassSelectorItem.LastChildSelectorItem instance = new CssPseudoClassSelectorItem.LastChildSelectorItem
                ();

            private LastChildSelectorItem()
                : base(CssConstants.LAST_CHILD) {
            }

            public static CssPseudoClassSelectorItem.LastChildSelectorItem GetInstance() {
                return instance;
            }

            public override bool Matches(INode node) {
                if (!(node is IElementNode) || node is ICustomElementNode) {
                    return false;
                }
                IList<INode> children = GetAllSiblings(node);
                return !children.IsEmpty() && node.Equals(children[children.Count - 1]);
            }
        }

        private class LastOfTypeSelectorItem : CssPseudoClassSelectorItem.ChildSelectorItem {
            private static readonly CssPseudoClassSelectorItem.LastOfTypeSelectorItem instance = new CssPseudoClassSelectorItem.LastOfTypeSelectorItem
                ();

            private LastOfTypeSelectorItem()
                : base(CssConstants.LAST_OF_TYPE) {
            }

            public static CssPseudoClassSelectorItem.LastOfTypeSelectorItem GetInstance() {
                return instance;
            }

            public override bool Matches(INode node) {
                if (!(node is IElementNode) || node is ICustomElementNode) {
                    return false;
                }
                IList<INode> children = GetAllSiblingsOfNodeType(node);
                return !children.IsEmpty() && node.Equals(children[children.Count - 1]);
            }
        }

        private class NthSelectorItem : CssPseudoClassSelectorItem.ChildSelectorItem {
            /// <summary>The nth A.</summary>
            private int nthA;

            /// <summary>The nth B.</summary>
            private int nthB;

            internal NthSelectorItem(String pseudoClass, String arguments)
                : base(pseudoClass, arguments) {
                GetNthArguments();
            }

            public override bool Matches(INode node) {
                if (!(node is IElementNode) || node is ICustomElementNode) {
                    return false;
                }
                IList<INode> children = GetAllSiblings(node);
                return !children.IsEmpty() && ResolveNth(node, children);
            }

            /// <summary>Gets the nth arguments.</summary>
            protected internal virtual void GetNthArguments() {
                if (arguments.Matches("((-|\\+)?[0-9]*n(\\s*(-|\\+)\\s*[0-9]+)?|(-|\\+)?[0-9]+|odd|even)")) {
                    if (arguments.Equals("odd")) {
                        this.nthA = 2;
                        this.nthB = 1;
                    }
                    else {
                        if (arguments.Equals("even")) {
                            this.nthA = 2;
                            this.nthB = 0;
                        }
                        else {
                            int indexOfN = arguments.IndexOf('n');
                            if (indexOfN == -1) {
                                this.nthA = 0;
                                this.nthB = System.Convert.ToInt32(arguments);
                            }
                            else {
                                String aParticle = arguments.JSubstring(0, indexOfN).Trim();
                                if (String.IsNullOrEmpty(aParticle)) {
                                    this.nthA = 0;
                                }
                                else {
                                    if (aParticle.Length == 1 && !char.IsDigit(aParticle[0])) {
                                        this.nthA = aParticle.Equals("+") ? 1 : -1;
                                    }
                                    else {
                                        this.nthA = System.Convert.ToInt32(aParticle);
                                    }
                                }
                                String bParticle = arguments.Substring(indexOfN + 1).Trim();
                                if (!String.IsNullOrEmpty(bParticle)) {
                                    this.nthB = System.Convert.ToInt32(bParticle[0] + bParticle.Substring(1).Trim());
                                }
                                else {
                                    this.nthB = 0;
                                }
                            }
                        }
                    }
                }
                else {
                    this.nthA = 0;
                    this.nthB = 0;
                }
            }

            /// <summary>Resolves the nth.</summary>
            /// <param name="node">a node</param>
            /// <param name="children">the children</param>
            /// <returns>true, if successful</returns>
            protected internal virtual bool ResolveNth(INode node, IList<INode> children) {
                if (!children.Contains(node)) {
                    return false;
                }
                if (this.nthA > 0) {
                    int temp = children.IndexOf(node) + 1 - this.nthB;
                    return temp >= 0 && temp % this.nthA == 0;
                }
                else {
                    if (this.nthA < 0) {
                        int temp = children.IndexOf(node) + 1 - this.nthB;
                        return temp <= 0 && temp % this.nthA == 0;
                    }
                    else {
                        return (children.IndexOf(node) + 1) - this.nthB == 0;
                    }
                }
            }
        }

        private class NthChildSelectorItem : CssPseudoClassSelectorItem.NthSelectorItem {
            internal NthChildSelectorItem(String arguments)
                : base(CssConstants.NTH_CHILD, arguments) {
            }
        }

        private class NthOfTypeSelectorItem : CssPseudoClassSelectorItem.NthSelectorItem {
            public NthOfTypeSelectorItem(String arguments)
                : base(CssConstants.NTH_OF_TYPE, arguments) {
            }

            public override bool Matches(INode node) {
                if (!(node is IElementNode) || node is ICustomElementNode) {
                    return false;
                }
                IList<INode> children = GetAllSiblingsOfNodeType(node);
                return !children.IsEmpty() && ResolveNth(node, children);
            }
        }

        private class NotSelectorItem : CssPseudoClassSelectorItem {
            private ICssSelector argumentsSelector;

            internal NotSelectorItem(ICssSelector argumentsSelector)
                : base(CssConstants.NOT, argumentsSelector.ToString()) {
                this.argumentsSelector = argumentsSelector;
            }

            public virtual IList<ICssSelectorItem> GetArgumentsSelector() {
                return CssSelectorParser.ParseSelectorItems(arguments);
            }

            public override bool Matches(INode node) {
                return !argumentsSelector.Matches(node);
            }
        }

        private class AlwaysApplySelectorItem : CssPseudoClassSelectorItem {
            internal AlwaysApplySelectorItem(String pseudoClass, String arguments)
                : base(pseudoClass, arguments) {
            }

            public override bool Matches(INode node) {
                return true;
            }
        }

        private class AlwaysNotApplySelectorItem : CssPseudoClassSelectorItem {
            internal AlwaysNotApplySelectorItem(String pseudoClass, String arguments)
                : base(pseudoClass, arguments) {
            }

            public override bool Matches(INode node) {
                return false;
            }
        }
    }
}
