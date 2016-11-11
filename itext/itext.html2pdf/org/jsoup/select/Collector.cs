using Org.Jsoup.Nodes;

namespace Org.Jsoup.Select {
    /// <summary>Collects a list of elements that match the supplied criteria.</summary>
    /// <author>Jonathan Hedley</author>
    public class Collector {
        private Collector() {
        }

        /// <summary>Build a list of elements, by visiting root and every descendant of root, and testing it against the evaluator.
        ///     </summary>
        /// <param name="eval">Evaluator to test elements against</param>
        /// <param name="root">root of tree to descend</param>
        /// <returns>list of matches; empty if none</returns>
        public static Elements Collect(Evaluator eval, Element root) {
            Elements elements = new Elements();
            new NodeTraversor(new Collector.Accumulator(root, elements, eval)).Traverse(root);
            return elements;
        }

        private class Accumulator : NodeVisitor {
            private readonly Element root;

            private readonly Elements elements;

            private readonly Evaluator eval;

            internal Accumulator(Element root, Elements elements, Evaluator eval) {
                this.root = root;
                this.elements = elements;
                this.eval = eval;
            }

            public virtual void Head(Node node, int depth) {
                if (node is Element) {
                    Element el = (Element)node;
                    if (eval.Matches(root, el)) {
                        elements.Add(el);
                    }
                }
            }

            public virtual void Tail(Node node, int depth) {
            }
            // void
        }
    }
}
