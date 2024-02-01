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
using iText.IO.Image;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Utils;
using iText.StyledXmlParser.Resolver.Resource;
using iText.Test;

namespace iText.Html2pdf.Resolver.Resource {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ExternalImageTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/resolver/resource/ExternalImageTest/";

        [NUnit.Framework.Test]
        public virtual void Test() {
            // Android-Conversion-Ignore-Test (TODO DEVSIX-6459 fix the SecurityException(Permission denied) from UrlUtil method)
            ResourceResolver resourceResolver = new ResourceResolver("");
            PdfXObject externalImage = resourceResolver.RetrieveImage("https://raw.githubusercontent.com/itext/itext7/develop/layout/src/test/resources/com/itextpdf/layout/ImageTest/itis.jpg"
                );
            NUnit.Framework.Assert.IsNotNull(externalImage);
            ImageData imageData = ImageDataFactory.Create(sourceFolder + "itis.jpg");
            iText.Layout.Element.Image localImage = new iText.Layout.Element.Image(imageData);
            NUnit.Framework.Assert.IsTrue(new CompareTool().CompareStreams(externalImage.GetPdfObject(), localImage.GetXObject
                ().GetPdfObject()));
        }
    }
}
