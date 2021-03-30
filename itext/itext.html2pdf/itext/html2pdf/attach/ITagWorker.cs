/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using iText.Layout;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>Interface for all the tag worker implementations.</summary>
    public interface ITagWorker {
        /// <summary>Placeholder for what needs to be done after the content of a tag has been processed.</summary>
        /// <param name="element">the element node</param>
        /// <param name="context">the processor context</param>
        void ProcessEnd(IElementNode element, ProcessorContext context);

        /// <summary>Placeholder for what needs to be done while the content of a tag is being processed.</summary>
        /// <param name="content">the content of a node</param>
        /// <param name="context">the processor context</param>
        /// <returns>true, if content was successfully processed, otherwise false.</returns>
        bool ProcessContent(String content, ProcessorContext context);

        /// <summary>Placeholder for what needs to be done when a child node is being processed.</summary>
        /// <param name="childTagWorker">the tag worker of the child node</param>
        /// <param name="context">the processor context</param>
        /// <returns>true, if child was successfully processed, otherwise false.</returns>
        bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context);

        /// <summary>
        /// Gets a processed object if it can be expressed as an
        /// <see cref="iText.Layout.IPropertyContainer"/>
        /// instance.
        /// </summary>
        /// <returns>
        /// the same object on every call.
        /// Might return null either if result is not yet produced or if this particular
        /// tag worker doesn't produce result in a form of
        /// <see cref="iText.Layout.IPropertyContainer"/>.
        /// </returns>
        IPropertyContainer GetElementResult();
    }
}
