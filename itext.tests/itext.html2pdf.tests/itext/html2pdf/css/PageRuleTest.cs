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
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Html.Node;
using iText.Kernel.Utils;
using iText.Layout.Element;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class PageRuleTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/PageRuleTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/PageRuleTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarksCropCrossPageRuleTest() {
            RunTest("marksCropCrossPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarksCropPageRuleTest() {
            RunTest("marksCropPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarksCrossPageRuleTest() {
            RunTest("marksCrossPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarksInvalidPageRuleTest() {
            RunTest("marksInvalidPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarksNonePageRuleTest() {
            RunTest("marksNonePageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void PaddingPageRuleTest() {
            RunTest("paddingPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CompoundSizePageRuleTest() {
            RunTest("compoundSizePageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BleedPageRuleTest() {
            RunTest("bleedPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.PAGE_SIZE_VALUE_IS_INVALID, Count = 3)]
        public virtual void InvalidCompoundSizePageRuleTest() {
            RunTest("invalidCompoundSizePageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NotAllMarginsPageRuleTest() {
            RunTest("notAllMarginsPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FirstLeftRightPageRuleTest() {
            RunTest("firstLeftRightPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarksBleedPageRuleTest() {
            RunTest("marksBleedPageRuleTest");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 3)]
        public virtual void MarginBoxTest01() {
            RunTest("marginBoxTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarginBoxTest02() {
            RunTest("marginBoxTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarginBoxTest03() {
            RunTest("marginBoxTest03");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarginBoxTest04() {
            RunTest("marginBoxTest04");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigImageOnPageMarginTest01() {
            RunTest("bigImageOnPageMarginTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigImageOnPageMarginTest02() {
            RunTest("bigImageOnPageMarginTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigImageOnPageMarginTest03() {
            RunTest("bigImageOnPageMarginTest03", new PageRuleTest.PageMarginBoxImagesTagWorkerFactory(), null);
        }

        private class PageMarginBoxImagesTagWorkerFactory : DefaultTagWorkerFactory {
            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                if (tag.Name().Equals(PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG)) {
                    return new PageRuleTest.PageMarginBoxImagesWorker(tag, context);
                }
                return base.GetCustomTagWorker(tag, context);
            }
        }

        private class PageMarginBoxImagesWorker : PageMarginBoxWorker {
            public PageMarginBoxImagesWorker(IElementNode element, ProcessorContext context)
                : base(element, context) {
            }

            public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
                if (childTagWorker.GetElementResult() is Image) {
                    // TODO Since iText 7.2 release it is ("it will be" for now, see PageMarginBoxDummyElement class) possible
                    // to get current page margin box name and dimensions from the "element" IElementNode passed to the constructor of this tag worker.
                    ((Image)childTagWorker.GetElementResult()).SetAutoScale(true);
                }
                return base.ProcessTagChild(childTagWorker, context);
            }
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigTextOnPageMarginTest01() {
            RunTest("bigTextOnPageMarginTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BigTextOnPageMarginTest02() {
            RunTest("bigTextOnPageMarginTest02");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarginBoxOverflowPropertyTest01() {
            RunTest("marginBoxOverflowPropertyTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarginBoxOverflowPropertyTest02() {
            RunTest("marginBoxOverflowPropertyTest02", null, new PageRuleTest.PageMarginsOverflowCssApplierFactory());
        }

        private class PageMarginsOverflowCssApplierFactory : DefaultCssApplierFactory {
            public override ICssApplier GetCustomCssApplier(IElementNode tag) {
                if (PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG.Equals(tag.Name())) {
                    return new PageRuleTest.CustomOverflowPageMarginBoxCssApplier();
                }
                return base.GetCustomCssApplier(tag);
            }
        }

        private class CustomOverflowPageMarginBoxCssApplier : PageMarginBoxCssApplier {
            public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
                ) {
                IDictionary<String, String> styles = stylesContainer.GetStyles();
                if (styles.Get(CssConstants.OVERFLOW) == null) {
                    styles.Put(CssConstants.OVERFLOW, CssConstants.VISIBLE);
                }
                base.Apply(context, stylesContainer, tagWorker);
            }
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MarginBoxOutlinePropertyTest01() {
            // TODO Outlines are currently not supported for page margin boxes, because of the outlines handling specificity (they are handled on renderer's parent level).
            //      See com.itextpdf.html2pdf.attach.impl.layout.PageContextProcessor.
            RunTest("marginBoxOutlinePropertyTest01");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String name) {
            RunTest(name, null, null);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String name, ITagWorkerFactory customTagWorkerFactory, ICssApplierFactory customCssApplierFactory
            ) {
            String htmlPath = sourceFolder + name + ".html";
            String pdfPath = destinationFolder + name + ".pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String diffPrefix = "diff_" + name + "_";
            ConverterProperties converterProperties = new ConverterProperties();
            if (customTagWorkerFactory != null) {
                converterProperties.SetTagWorkerFactory(customTagWorkerFactory);
            }
            if (customCssApplierFactory != null) {
                converterProperties.SetCssApplierFactory(customCssApplierFactory);
            }
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfPath, cmpPdfPath, destinationFolder, diffPrefix
                ));
        }
    }
}
