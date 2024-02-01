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
using iText.Commons.Actions;
using iText.Commons.Actions.Contexts;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl {
    [NUnit.Framework.Category("UnitTest")]
    public class HtmlMetaInfoContainerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CreateAndGetMetaInfoTest() {
            HtmlMetaInfoContainerTest.TestMetaInfo metaInfo = new HtmlMetaInfoContainerTest.TestMetaInfo();
            HtmlMetaInfoContainer metaInfoContainer = new HtmlMetaInfoContainer(metaInfo);
            NUnit.Framework.Assert.AreSame(metaInfo, metaInfoContainer.GetMetaInfo());
        }

        [NUnit.Framework.Test]
        public virtual void GetNullMetaInfoTest() {
            HtmlMetaInfoContainer metaInfoContainer = new HtmlMetaInfoContainer(null);
            NUnit.Framework.Assert.IsNull(metaInfoContainer.GetMetaInfo());
        }

        [NUnit.Framework.Test]
        public virtual void ProcessorContextCreatorCreatesContextWithHtmlMetaInfoTest() {
            ProcessorContext processorContext = ProcessorContextCreator.CreateProcessorContext(new ConverterProperties
                ());
            HtmlMetaInfoContainer htmlMetaInfoContainer = processorContext.GetMetaInfoContainer();
            NUnit.Framework.Assert.IsNotNull(htmlMetaInfoContainer);
            IMetaInfo metaInfo = htmlMetaInfoContainer.GetMetaInfo();
            NUnit.Framework.Assert.IsTrue(metaInfo.GetType().FullName.StartsWith(NamespaceConstant.PDF_HTML + "."));
        }

        private class TestMetaInfo : IMetaInfo {
        }
    }
}
