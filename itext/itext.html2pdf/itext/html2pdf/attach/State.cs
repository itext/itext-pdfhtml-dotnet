/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using System.Collections.Generic;

namespace iText.Html2pdf.Attach {
    /// <summary>State machine to push, pop, peek,...</summary>
    /// <remarks>
    /// State machine to push, pop, peek,...
    /// <see cref="ITagWorker"/>
    /// instances to and from the
    /// <see cref="System.Collections.Stack{E}"/>.
    /// </remarks>
    public class State {
        /// <summary>The stack.</summary>
        private Stack<ITagWorker> stack;

        /// <summary>
        /// Instantiates a new
        /// <see cref="State"/>
        /// instance.
        /// </summary>
        public State() {
            stack = new Stack<ITagWorker>();
        }

        /// <summary>Gets the stack.</summary>
        /// <returns>the stack</returns>
        public virtual Stack<ITagWorker> GetStack() {
            return stack;
        }

        /// <summary>
        /// Pushes a
        /// <see cref="ITagWorker"/>
        /// instance to the stack.
        /// </summary>
        /// <param name="tagWorker">the tag worker</param>
        public virtual void Push(ITagWorker tagWorker) {
            stack.Push(tagWorker);
        }

        /// <summary>
        /// Pops a
        /// <see cref="ITagWorker"/>
        /// from the stack.
        /// </summary>
        /// <returns>the tag worker</returns>
        public virtual ITagWorker Pop() {
            return stack.Pop();
        }

        /// <summary>
        /// Peeks at the
        /// <see cref="ITagWorker"/>
        /// at the top of the stack.
        /// </summary>
        /// <returns>the tag worker at the top</returns>
        public virtual ITagWorker Top() {
            return stack.Peek();
        }

        /// <summary>Checks if the stack is empty.</summary>
        /// <returns>true, if the stack is empty</returns>
        public virtual bool Empty() {
            return stack.Count == 0;
        }
    }
}
