using System;
using System.Collections.Generic;
using Org.Jsoup.Nodes;
using iText.IO.Util;

namespace Org.Jsoup.Select {
    /// <summary>Base combining (and, or) evaluator.</summary>
    internal abstract class CombiningEvaluator : Evaluator {
        internal readonly List<Evaluator> evaluators;

        internal int num = 0;

        internal CombiningEvaluator()
            : base() {
            evaluators = new List<Evaluator>();
        }

        internal CombiningEvaluator(ICollection<Evaluator> evaluators)
            : this() {
            this.evaluators.AddAll(evaluators);
            UpdateNumEvaluators();
        }

        internal virtual Evaluator RightMostEvaluator() {
            return num > 0 ? evaluators[num - 1] : null;
        }

        internal virtual void ReplaceRightMostEvaluator(Evaluator replacement) {
            evaluators[num - 1] = replacement;
        }

        internal virtual void UpdateNumEvaluators() {
            // used so we don't need to bash on size() for every match test
            num = evaluators.Count;
        }

        internal sealed class And : CombiningEvaluator {
            internal And(ICollection<Evaluator> evaluators)
                : base(evaluators) {
            }

            internal And(params Evaluator[] evaluators)
                : this(iText.IO.Util.JavaUtil.ArraysAsList(evaluators)) {
            }

            public override bool Matches(Element root, Element node) {
                for (int i = 0; i < num; i++) {
                    Evaluator s = evaluators[i];
                    if (!s.Matches(root, node)) {
                        return false;
                    }
                }
                return true;
            }

            public override String ToString() {
                return Org.Jsoup.Helper.StringUtil.Join(evaluators, " ");
            }
        }

        internal sealed class OR : CombiningEvaluator {
            /// <summary>Create a new Or evaluator.</summary>
            /// <remarks>Create a new Or evaluator. The initial evaluators are ANDed together and used as the first clause of the OR.
            ///     </remarks>
            /// <param name="evaluators">initial OR clause (these are wrapped into an AND evaluator).</param>
            internal OR(ICollection<Evaluator> evaluators)
                : base() {
                if (num > 1) {
                    this.evaluators.Add(new CombiningEvaluator.And(evaluators));
                }
                else {
                    // 0 or 1
                    this.evaluators.AddAll(evaluators);
                }
                UpdateNumEvaluators();
            }

            internal OR()
                : base() {
            }

            public void Add(Evaluator e) {
                evaluators.Add(e);
                UpdateNumEvaluators();
            }

            public override bool Matches(Element root, Element node) {
                for (int i = 0; i < num; i++) {
                    Evaluator s = evaluators[i];
                    if (s.Matches(root, node)) {
                        return true;
                    }
                }
                return false;
            }

            public override String ToString() {
                return MessageFormatUtil.Format(":or{0}", evaluators);
            }
        }
    }
}
