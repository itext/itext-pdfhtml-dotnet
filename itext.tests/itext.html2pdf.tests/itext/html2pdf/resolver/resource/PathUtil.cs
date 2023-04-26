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
using System;
using iText.Test;

namespace iText.Html2pdf.Resolver.Resource
{
    internal sealed class PathUtil
    {
        private PathUtil() {
        }

        internal static String GetAbsolutePathToResourcesForHtmlResourceResolverTest()
        {
            return TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext.CurrentContext.TestDirectory) +
                   "/resources/itext/html2pdf/resolver/resource/HtmlResourceResolverTest/res";
        }

        internal static String GetUriToResourcesForHtmlResourceResolverTest()
        {
            // It is important to put a trailing slash in the end: if you specify base URI via absolute URI string,
            // you need to follow URI standards, in which a path without trailing slash is referring to a file.
            return new Uri(TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext.CurrentContext.TestDirectory))
                       .ToExternalForm() +
                   "/resources/itext/html2pdf/resolver/resource/HtmlResourceResolverTest/res/";
        }
    }
}
