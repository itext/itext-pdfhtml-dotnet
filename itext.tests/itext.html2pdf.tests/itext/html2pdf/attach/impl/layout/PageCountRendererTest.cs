/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
Authors: iText Software.

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
using System.Collections.Generic;
using iText.IO.Font.Otf;
using iText.Kernel.Font;
using iText.Layout.Renderer;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Attach.Impl.Layout {
    [NUnit.Framework.Category("UnitTest")]
    public class PageCountRendererTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.GET_NEXT_RENDERER_SHOULD_BE_OVERRIDDEN)]
        public virtual void GetNextRendererShouldBeOverriddenTest() {
            PageCountRenderer pageCountRenderer = new _PageCountRenderer_50(new PageCountElement());
            // Nothing is overridden
            NUnit.Framework.Assert.AreEqual(typeof(PageCountRenderer), pageCountRenderer.GetNextRenderer().GetType());
        }

        private sealed class _PageCountRenderer_50 : PageCountRenderer {
            public _PageCountRenderer_50(PageCountElement baseArg1)
                : base(baseArg1) {
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CREATE_COPY_SHOULD_BE_OVERRIDDEN)]
        public virtual void CreateCopyShouldBeOverriddenTest() {
            PageCountRenderer pageCountRenderer = new PageCountRendererTest.CustomPageCountRenderer(new PageCountElement
                ());
            NUnit.Framework.Assert.AreEqual(typeof(PageCountRendererTest.CustomPageCountRenderer), pageCountRenderer.GetNextRenderer
                ().GetType());
            // This test checks for the log message being sent, so we should get there
            NUnit.Framework.Assert.IsTrue(true);
        }

        internal class CustomPageCountRenderer : PageCountRenderer {
            public CustomPageCountRenderer(PageCountElement textElement)
                : base(textElement) {
            }

            public override IRenderer GetNextRenderer() {
                try {
                    // In Java protected methods could be accessed within the same package as default ones,
                    // but .NET works differently. Hence to test a log about #copyText being not overridden
                    // we need to call it from inside the class.
                    TextRenderer copy = CreateCopy(new GlyphLine(new List<Glyph>()), PdfFontFactory.CreateFont());
                    NUnit.Framework.Assert.IsNotNull(copy);
                }
                catch (System.IO.IOException) {
                    NUnit.Framework.Assert.Fail("We do not expect PdfFontFactory#createFont() to throw an exception here.");
                }
                return new PageCountRendererTest.CustomPageCountRenderer((PageCountElement)this.modelElement);
            }
        }
    }
}
