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
    public class PageTargetCountRendererTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.GET_NEXT_RENDERER_SHOULD_BE_OVERRIDDEN)]
        public virtual void GetNextRendererShouldBeOverriddenTest() {
            PageTargetCountRenderer pageTargetCountRenderer = new _PageTargetCountRenderer_51(new PageTargetCountElement
                ("test"));
            // Nothing is overridden
            NUnit.Framework.Assert.AreEqual(typeof(PageTargetCountRenderer), pageTargetCountRenderer.GetNextRenderer()
                .GetType());
        }

        private sealed class _PageTargetCountRenderer_51 : PageTargetCountRenderer {
            public _PageTargetCountRenderer_51(PageTargetCountElement baseArg1)
                : base(baseArg1) {
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CREATE_COPY_SHOULD_BE_OVERRIDDEN)]
        public virtual void CreateCopyShouldBeOverriddenTest() {
            PageTargetCountRenderer pageTargetCountRenderer = new PageTargetCountRendererTest.CustomPageTargetCountRenderer
                (new PageTargetCountElement("test"));
            NUnit.Framework.Assert.AreEqual(typeof(PageTargetCountRendererTest.CustomPageTargetCountRenderer), pageTargetCountRenderer
                .GetNextRenderer().GetType());
            // This test checks for the log message being sent, so we should get there
            NUnit.Framework.Assert.IsTrue(true);
        }

        internal class CustomPageTargetCountRenderer : PageTargetCountRenderer {
            public CustomPageTargetCountRenderer(PageTargetCountElement textElement)
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
                return new PageTargetCountRendererTest.CustomPageTargetCountRenderer((PageTargetCountElement)this.modelElement
                    );
            }
        }
    }
}
