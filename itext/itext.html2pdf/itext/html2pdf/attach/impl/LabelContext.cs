/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Text;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>This class keeps track of labels attached to applicable elements that present in the document.</summary>
    /// <remarks>
    /// This class keeps track of labels attached to applicable elements that present in the document.
    /// After elements are proccessed, the DOM tree is scanned and labels are attached to corresponding objects.
    /// This object in the
    /// <see cref="iText.Html2pdf.Attach.ProcessorContext"/>.
    /// <para />
    /// This class is not reusable, and a new instance shall be created for every new conversion process.
    /// </remarks>
    public class LabelContext : IDocumentTreeJob {
        private readonly IDictionary<String, String> idToDesc = new Dictionary<String, String>();

        private readonly IDictionary<String, IElementNode> idToElement = new Dictionary<String, IElementNode>();

        private readonly LabelContext.LabelIdGenerator idGenerator = new LabelContext.LabelIdGenerator();

        /// <summary>Creates new label context.</summary>
        public LabelContext() {
        }

        // Empty constructor, nothing to initialize
        /// <summary>Gets the alternate description for the element by id.</summary>
        /// <param name="id">id of the element</param>
        /// <returns>alternate description</returns>
        public virtual String GetAltDescription(String id) {
            String desc = idToDesc.Get(id);
            return desc == null ? GetTextRepresentation(idToElement.Get(id)) : desc;
        }

        /// <summary>Processes a given node to analyze and store potential labels.</summary>
        /// <param name="node">the node to process</param>
        /// <param name="level">the level of the node within the document tree</param>
        public virtual void Process(INode node, int level) {
            if (node is IElementNode) {
                IElementNode elem = (IElementNode)node;
                if (elem.GetAttribute(AttributeConstants.ID) != null) {
                    idToElement.Put(((IElementNode)node).GetAttribute(AttributeConstants.ID), (IElementNode)node);
                }
                if (TagConstants.LABEL.Equals(elem.Name())) {
                    ParseLabelDesc(elem);
                }
            }
        }

        private void ParseLabelDesc(IElementNode elem) {
            if (elem.GetAttribute(AttributeConstants.FOR) != null) {
                idToDesc.Put(elem.GetAttribute(AttributeConstants.FOR), GetTextRepresentation(elem));
            }
            else {
                LabelContext.ImplicitLabelTextNodeCollector collector = new LabelContext.ImplicitLabelTextNodeCollector();
                DocumentTreeUtil.Traverse(elem, JavaCollectionsUtil.SingletonList((IDocumentTreeJob)collector));
                if (collector.GetLabeledElement() != null) {
                    String id = GenerateIdIfNotExists(collector.GetLabeledElement());
                    idToDesc.Put(id, collector.GetText());
                }
            }
        }

        private String GenerateIdIfNotExists(IElementNode labeledElement) {
            String id = labeledElement.GetAttribute(AttributeConstants.ID);
            if (id == null) {
                String generatedId = idGenerator.GenerateId();
                labeledElement.GetAttributes().SetAttribute(AttributeConstants.ID, generatedId);
                return generatedId;
            }
            return id;
        }

        private static String GetTextRepresentation(IElementNode elem) {
            if (elem == null) {
                return null;
            }
            LabelContext.SimpleTextNodeCollector collector = new LabelContext.SimpleTextNodeCollector();
            DocumentTreeUtil.Traverse(elem, JavaCollectionsUtil.SingletonList((IDocumentTreeJob)collector));
            return collector.GetText();
        }

        /// <summary>Class that collects all children text nodes of given node to a string.</summary>
        private sealed class SimpleTextNodeCollector : IDocumentTreeJob {
            private readonly StringBuilder textBuilder = new StringBuilder();

            /// <summary>Creates a new simple text node collector</summary>
            public SimpleTextNodeCollector() {
            }

            public void Process(INode node, int level) {
                if (node is ITextNode) {
                    String text = ((ITextNode)node).WholeText();
                    if (OnlySpacesOrNewlines(text)) {
                        return;
                    }
                    textBuilder.Append(text).Append(' ');
                }
            }

            public String GetText() {
                return textBuilder.ToString();
            }

            private static bool OnlySpacesOrNewlines(String s) {
                if (s == null) {
                    return true;
                }
                for (int i = 0; i < s.Length; ++i) {
                    char c = s[i];
                    if (c != ' ' && c != '\n' && c != '\r') {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Class that collects all children text nodes of the given node to a string except for the first encountered
        /// labelable element.
        /// </summary>
        private sealed class ImplicitLabelTextNodeCollector : IDocumentTreeJob {
            private IElementNode labeledElement = null;

            //3 states: -1 - labeled element not initialized, 0 - labeled element initialized,
            // > 0 - stack size snapshot before labeledElement processing
            private int stackSize = -1;

            private readonly LabelContext.SimpleTextNodeCollector collector = new LabelContext.SimpleTextNodeCollector
                ();

            /// <summary>Creates new text node collector for implicit labels.</summary>
            public ImplicitLabelTextNodeCollector() {
            }

            public void Process(INode iNode, int level) {
                if (stackSize == level) {
                    stackSize = -1;
                }
                if (stackSize == 0) {
                    stackSize = level - labeledElement.ChildNodes().Count;
                }
                if (stackSize >= 0) {
                    return;
                }
                if (labeledElement == null && iNode is IElementNode && LabelUtil.IsLabelable((IElementNode)iNode)) {
                    labeledElement = (IElementNode)iNode;
                    if (labeledElement.ChildNodes().IsEmpty()) {
                        return;
                    }
                    stackSize = 0;
                    return;
                }
                collector.Process(iNode, level);
            }

            public String GetText() {
                return collector.GetText();
            }

            public IElementNode GetLabeledElement() {
                return labeledElement;
            }
        }

        /// <summary>Class that generate ids for labelable elements which don't have assigned id</summary>
        private sealed class LabelIdGenerator {
            private int counter = 0;

            /// <summary>Creates new label id generator.</summary>
            public LabelIdGenerator() {
            }

            public String GenerateId() {
                return "idForLabel" + JavaUtil.IntegerToString(counter++);
            }
        }
    }
}
