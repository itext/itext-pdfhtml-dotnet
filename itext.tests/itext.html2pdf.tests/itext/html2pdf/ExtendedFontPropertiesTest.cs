/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Test;

namespace iText.Html2pdf {
    public class ExtendedFontPropertiesTest : ExtendedITextTest {
        private const String HTML_TOP = "<html><head>";

        private const String HTML_BEGIN_BODY = "</head><body>";

        private const String HTML_BOTTOM = "</body></html>";

        private const String TR_OPEN = "<tr>";

        private const String TD_OPEN = "<td>";

        private const String TD_CLOSE = "</td>";

        private const String TR_CLOSE = "</tr>";

        private const String TABLE_OPEN = "<table>";

        private const String TABLE_CLOSE = "</table>";

        private const String DOCUMENT_PREFIX = "FOR_VISUAL_CMP_";

        private const String COLUMN_DECLARATIONS = "<col width='50'><col width='50'><col width='50'><col width='300'>";

        private const String TH_FONT_FAMILY = "<th scope='col'>Font-family</th>";

        private const String TH_FONT_WEIGHT = "<th scope='col'>Font-weight</th>";

        private const String TH_FONT_STYLE = "<th scope='col'>Font-style</th>";

        private const String TH_RESULT = "<th scope='col' >Result</th>";

        private const String TITLE_TAG = "<title>Font Family Test</title>";

        private const String HTML_TITLE = "<h1>Font Family Test</h1>";

        private const String TD_STYLE = "<td style = \"";

        private const String FONT_FAMILY = " font-family: '";

        private const String FONT_STYLE = "; font-style: ";

        private const String FONT_WEIGHT = "'; font-weight: ";

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void RunTest(String htmlString, String sourceFolder, String destinationFolder, String fileName
            , String testName) {
            String outPdf = destinationFolder + fileName + ".pdf";
            String cmpPdf = sourceFolder + "cmp_" + fileName + ".pdf";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outPdf));
            Document doc = new Document(pdfDoc);
            byte[] bytes = htmlString.GetBytes(Encoding.UTF8);
            // save to html
            GenerateTestHtml(destinationFolder, fileName, bytes);
            // Convert to elements
            WriteToDocument(doc, bytes);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff_"
                 + testName + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        private void WriteToDocument(Document doc, byte[] bytes) {
            Stream @in = new MemoryStream(bytes);
            IList<IElement> arrayList = HtmlConverter.ConvertToElements(@in);
            foreach (IElement element in arrayList) {
                if (element is IBlockElement) {
                    doc.Add((IBlockElement)element);
                }
            }
            doc.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        private void GenerateTestHtml(String destinationFolder, String fileName, byte[] bytes) {
            String htmlPath = destinationFolder + DOCUMENT_PREFIX + fileName + ".html";
            FileStream @out = new FileStream(htmlPath, FileMode.Create);
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(htmlPath).AbsolutePath + "\n");
            @out.Dispose();
        }

        public virtual String BuildDocumentTree(String[] fontFamilies, String[] fontWeights, String[] fontStyles, 
            String[] cssFiles, String text) {
            StringBuilder builder = new StringBuilder();
            builder.Append(HTML_TOP);
            if (cssFiles != null) {
                foreach (String css in cssFiles) {
                    builder.Append(" <link href='").Append(css).Append("' rel='stylesheet' type='text/css'>\n ");
                }
            }
            //Build Html top
            String styleTag = "<style>\n" + " th, td {text-align: center;\n" + " height: 45px; border: 1px solid black; }\n"
                 + " table {font-family: Courier; }" + " </style>\n";
            builder.Append(TITLE_TAG);
            builder.Append(styleTag);
            builder.Append(HTML_BEGIN_BODY);
            builder.Append(HTML_TITLE);
            builder.Append(TABLE_OPEN);
            builder.Append(COLUMN_DECLARATIONS);
            builder.Append(TR_OPEN);
            builder.Append(TH_FONT_FAMILY);
            builder.Append(TH_FONT_WEIGHT);
            builder.Append(TH_FONT_STYLE);
            builder.Append(TH_RESULT);
            builder.Append(TR_CLOSE);
            //Build body content
            foreach (String name in fontFamilies) {
                foreach (String weight in fontWeights) {
                    foreach (String style in fontStyles) {
                        // the tr
                        builder.Append(TR_OPEN);
                        // the first td
                        builder.Append(TD_OPEN).Append(name).Append(TD_CLOSE);
                        // the second td
                        builder.Append(TD_OPEN).Append(weight).Append(TD_CLOSE);
                        // the third td
                        builder.Append(TD_OPEN).Append(style).Append(TD_CLOSE);
                        // the fourth td
                        builder.Append(TD_STYLE).Append(FONT_FAMILY).Append(name).Append(FONT_WEIGHT).Append(weight).Append(FONT_STYLE
                            ).Append(style).Append("\";>").Append(text).Append(TD_CLOSE);
                        // the tr
                        builder.Append(TR_CLOSE);
                    }
                }
            }
            builder.Append(TABLE_CLOSE);
            builder.Append(HTML_BOTTOM);
            return builder.ToString();
        }
    }
}
