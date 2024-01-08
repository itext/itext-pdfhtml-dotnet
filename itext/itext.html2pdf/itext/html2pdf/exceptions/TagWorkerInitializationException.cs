/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Commons.Exceptions;
using iText.Commons.Utils;

namespace iText.Html2pdf.Exceptions {
    /// <summary>Runtime exception that gets thrown if a tag worker can't be initialized.</summary>
    public class TagWorkerInitializationException : ITextException {
        /// <summary>Template for the error message in case a tag worker couldn't be instantiated.</summary>
        public const String REFLECTION_IN_TAG_WORKER_FACTORY_IMPLEMENTATION_FAILED = "Could not " + "instantiate TagWorker-class {0} for tag {1}.";

        /// <summary>
        /// Creates a
        /// <see cref="TagWorkerInitializationException"/>
        /// instance.
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="classNames">the class names</param>
        /// <param name="tag">the tag</param>
        public TagWorkerInitializationException(String message, String classNames, String tag)
            : base(MessageFormatUtil.Format(message, classNames, tag)) {
        }

        /// <summary>
        /// Creates a
        /// <see cref="TagWorkerInitializationException"/>
        /// instance.
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="classNames">the class names</param>
        /// <param name="tag">the tag</param>
        /// <param name="cause">the cause</param>
        public TagWorkerInitializationException(String message, String classNames, String tag, Exception cause)
            : base(MessageFormatUtil.Format(message, classNames, tag), cause) {
        }
    }
}
