/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Versions.Attributes;
using iText.Kernel;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    public class Html2ElementsTest : ExtendedITextTest {
        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/Html2ElementsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
                );
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest01() {
            String html = "<p>Hello world!</p>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 1);
            NUnit.Framework.Assert.IsTrue(lst[0] is Paragraph);
            Paragraph p = (Paragraph)lst[0];
            NUnit.Framework.Assert.AreEqual("Hello world!", ((Text)p.GetChildren()[0]).GetText());
            NUnit.Framework.Assert.AreEqual(12f, (float)(Object)p.GetProperty<float?>(Property.FONT_SIZE), 1e-10);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest02() {
            String html = "<table style=\"font-size: 2em\"><tr><td>123</td><td><456></td></tr><tr><td>Long cell</td></tr></table>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 1);
            NUnit.Framework.Assert.IsTrue(lst[0] is Table);
            Table t = (Table)lst[0];
            NUnit.Framework.Assert.IsTrue(t.GetNumberOfRows() == 2);
            NUnit.Framework.Assert.AreEqual("123", ((Text)(((Paragraph)t.GetCell(0, 0).GetChildren()[0]).GetChildren()
                [0])).GetText());
            NUnit.Framework.Assert.AreEqual(24f, (float)(Object)t.GetProperty<float?>(Property.FONT_SIZE), 1e-10);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest03() {
            String html = "<p>Hello world!</p><table><tr><td>123</td><td><456></td></tr><tr><td>Long cell</td></tr></table><p>Hello world!</p>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 3);
            NUnit.Framework.Assert.IsTrue(lst[0] is Paragraph);
            NUnit.Framework.Assert.IsTrue(lst[1] is Table);
            NUnit.Framework.Assert.IsTrue(lst[2] is Paragraph);
            NUnit.Framework.Assert.AreEqual("Hello world!", ((Text)((Paragraph)lst[0]).GetChildren()[0]).GetText());
            NUnit.Framework.Assert.AreEqual("123", ((Text)(((Paragraph)((Table)lst[1]).GetCell(0, 0).GetChildren()[0])
                .GetChildren()[0])).GetText());
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest04() {
            // Handles malformed html
            String html = "<p>Hello world!<table><td>123";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 2);
            NUnit.Framework.Assert.IsTrue(lst[0] is Paragraph);
            NUnit.Framework.Assert.IsTrue(lst[1] is Table);
            NUnit.Framework.Assert.AreEqual("Hello world!", ((Text)((Paragraph)lst[0]).GetChildren()[0]).GetText());
            NUnit.Framework.Assert.AreEqual("123", ((Text)(((Paragraph)((Table)lst[1]).GetCell(0, 0).GetChildren()[0])
                .GetChildren()[0])).GetText());
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.TEXT_WAS_NOT_PROCESSED)]
        public virtual void HtmlToElementsTest05() {
            String html = "123";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 0);
        }
    }
}