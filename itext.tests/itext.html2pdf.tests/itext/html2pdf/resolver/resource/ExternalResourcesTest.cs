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
using iText.StyledXmlParser.Resolver.Resource;
using iText.Test;

namespace iText.Html2pdf.Resolver.Resource {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ExternalResourcesTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ExternalStylesheetTest() {
            // Android-Conversion-Ignore-Test (TODO DEVSIX-6459 fix the SecurityException(Permission denied) from UrlUtil method)
            ResourceResolver resourceResolver = new ResourceResolver("");
            byte[] exByteArray = resourceResolver.RetrieveBytesFromResource("https://raw.githubusercontent.com/itext/i7j-pdfhtml/develop/src/test/resources/com/itextpdf/html2pdf/styles.css"
                );
            NUnit.Framework.Assert.IsNotNull(exByteArray);
        }
    }
}
