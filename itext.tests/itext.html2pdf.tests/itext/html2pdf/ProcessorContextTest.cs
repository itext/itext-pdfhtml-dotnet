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
using System.IO;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.StyledXmlParser;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
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
            NUnit.Framework.Assert.Catch(typeof(PdfException), () => {
                FileStream fileInputStream = new FileStream(sourceFolder + "justHelloWorld.html", FileMode.Open, FileAccess.Read
                    );
                IXmlParser parser = new JsoupHtmlParser();
                IDocumentNode documentNode = parser.Parse(fileInputStream, null);
                ConverterProperties converterProperties = new ConverterProperties();
                converterProperties.SetFontProvider(new _BasicFontProvider_66(false, true, false));
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
            );
        }

        private sealed class _BasicFontProvider_66 : BasicFontProvider {
            public _BasicFontProvider_66(bool baseArg1, bool baseArg2, bool baseArg3)
                : base(baseArg1, baseArg2, baseArg3) {
            }

            public override void Reset() {
            }
        }
    }
}
