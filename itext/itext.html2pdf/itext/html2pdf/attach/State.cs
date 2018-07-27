/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using System.Collections.Generic;

namespace iText.Html2pdf.Attach {
    /// <summary>State machine to push, pop, peek,...</summary>
    /// <remarks>
    /// State machine to push, pop, peek,...
    /// <see cref="ITagWorker"/>
    /// instances to and from the
    /// <see cref="System.Collections.Stack{E}"/>
    /// .
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
