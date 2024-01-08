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

        public virtual void RunTest(String htmlString, String sourceFolder, String destinationFolder, String fileName
            , String testName) {
            String outPdf = destinationFolder + fileName + ".pdf";
            String cmpPdf = sourceFolder + "cmp_" + fileName + ".pdf";
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outPdf));
            Document doc = new Document(pdfDoc);
            byte[] bytes = htmlString.GetBytes(System.Text.Encoding.UTF8);
            // save to html
            GenerateTestHtml(destinationFolder, fileName, bytes);
            // Convert to elements
            WriteToDocument(doc, bytes);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff_"
                 + testName + "_"));
        }

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

        private void GenerateTestHtml(String destinationFolder, String fileName, byte[] bytes) {
            String htmlPath = destinationFolder + DOCUMENT_PREFIX + fileName + ".html";
            FileStream @out = new FileStream(htmlPath, FileMode.Create);
            @out.Write(bytes);
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
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
