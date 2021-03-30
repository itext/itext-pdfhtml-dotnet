/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

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
using System.IO;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.StyledXmlParser;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup;
using iText.Test;

namespace iText.Html2pdf {
    public class ProcessorContextTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/ProcessorContextTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/ProcessorContextTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DoNotResetFontProviderTest() {
            NUnit.Framework.Assert.That(() =>  {
                FileStream fileInputStream = new FileStream(sourceFolder + "justHelloWorld.html", FileMode.Open, FileAccess.Read
                    );
                IXmlParser parser = new JsoupHtmlParser();
                IDocumentNode documentNode = parser.Parse(fileInputStream, null);
                ConverterProperties converterProperties = new ConverterProperties();
                converterProperties.SetFontProvider(new _DefaultFontProvider_91(false, true, false));
                // Do nothing here. That should result in an exception.
                IHtmlProcessor processor = new DefaultHtmlProcessor(converterProperties);
                Document doc1 = processor.ProcessDocument(documentNode, new PdfDocument(new PdfWriter(new MemoryStream()))
                    );
                doc1.Close();
                Document doc2 = processor.ProcessDocument(documentNode, new PdfDocument(new PdfWriter(new MemoryStream()))
                    );
                doc2.Close();
                NUnit.Framework.Assert.IsTrue(false, "The test should have failed before that assert, since it's strictly forbidden not to reset the FontProvider instance after each html to pdf conversion."
                    );
            }
            , NUnit.Framework.Throws.InstanceOf<PdfException>())
;
        }

        private sealed class _DefaultFontProvider_91 : DefaultFontProvider {
            public _DefaultFontProvider_91(bool baseArg1, bool baseArg2, bool baseArg3)
                : base(baseArg1, baseArg2, baseArg3) {
            }

            public override void Reset() {
            }
        }
    }
}
