using System;
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Html;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css {
    public class OrphansWidowsUnitTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/OrphansWidowsUnitTest/";

        [NUnit.Framework.Test]
        public virtual void OrphansDefaultValue() {
            IList<IElement> elements = ConvertToElements("orphansDefaultValue");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphOrphansControl orphansControl = paragraph.GetProperty<ParagraphOrphansControl>(Property.ORPHANS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(orphansControl);
            NUnit.Framework.Assert.AreEqual(2, orphansControl.GetMinOrphans());
        }

        [NUnit.Framework.Test]
        public virtual void OrphansPropertyPresent() {
            IList<IElement> elements = ConvertToElements("orphansPropertyPresent");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphOrphansControl orphansControl = paragraph.GetProperty<ParagraphOrphansControl>(Property.ORPHANS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(orphansControl);
            NUnit.Framework.Assert.AreEqual(3, orphansControl.GetMinOrphans());
        }

        [NUnit.Framework.Test]
        public virtual void OrphansPropertyInherited() {
            IList<IElement> elements = ConvertToElements("orphansPropertyInherited");
            Div div = (Div)elements[0];
            NUnit.Framework.Assert.IsNotNull(div);
            ParagraphOrphansControl divOrphansControl = div.GetProperty<ParagraphOrphansControl>(Property.ORPHANS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(divOrphansControl);
            NUnit.Framework.Assert.AreEqual(3, divOrphansControl.GetMinOrphans());
            Paragraph paragraph = (Paragraph)div.GetChildren()[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphOrphansControl paragraphOrphansControl = paragraph.GetProperty<ParagraphOrphansControl>(Property.
                ORPHANS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(paragraphOrphansControl);
            NUnit.Framework.Assert.AreEqual(3, paragraphOrphansControl.GetMinOrphans());
        }

        [NUnit.Framework.Test]
        public virtual void OrphansPropertyOneInheritedOneRedefined() {
            IList<IElement> elements = ConvertToElements("orphansPropertyOneInheritedOneRedefined");
            Div div = (Div)elements[0];
            NUnit.Framework.Assert.IsNotNull(div);
            ParagraphOrphansControl divOrphansControl = div.GetProperty<ParagraphOrphansControl>(Property.ORPHANS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(divOrphansControl);
            NUnit.Framework.Assert.AreEqual(3, divOrphansControl.GetMinOrphans());
            Paragraph paragraph = (Paragraph)div.GetChildren()[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphOrphansControl paragraphOrphansControl = paragraph.GetProperty<ParagraphOrphansControl>(Property.
                ORPHANS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(paragraphOrphansControl);
            NUnit.Framework.Assert.AreEqual(3, paragraphOrphansControl.GetMinOrphans());
            Paragraph anotherParagraph = (Paragraph)div.GetChildren()[1];
            NUnit.Framework.Assert.IsNotNull(anotherParagraph);
            ParagraphOrphansControl anotherParagraphOrphansControl = anotherParagraph.GetProperty<ParagraphOrphansControl
                >(Property.ORPHANS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(anotherParagraphOrphansControl);
            NUnit.Framework.Assert.AreEqual(4, anotherParagraphOrphansControl.GetMinOrphans());
        }

        [NUnit.Framework.Test]
        public virtual void WidowsDefaultValue() {
            IList<IElement> elements = ConvertToElements("widowsDefaultValue");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphWidowsControl widowsControl = paragraph.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(widowsControl);
            NUnit.Framework.Assert.AreEqual(2, widowsControl.GetMinWidows());
        }

        [NUnit.Framework.Test]
        public virtual void WidowsPropertyPresent() {
            IList<IElement> elements = ConvertToElements("widowsPropertyPresent");
            Paragraph paragraph = (Paragraph)elements[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphWidowsControl widowsControl = paragraph.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(widowsControl);
            NUnit.Framework.Assert.AreEqual(3, widowsControl.GetMinWidows());
        }

        [NUnit.Framework.Test]
        public virtual void WidowsPropertyInherited() {
            IList<IElement> elements = ConvertToElements("widowsPropertyInherited");
            Div div = (Div)elements[0];
            NUnit.Framework.Assert.IsNotNull(div);
            ParagraphWidowsControl divWidowsControl = div.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(divWidowsControl);
            NUnit.Framework.Assert.AreEqual(3, divWidowsControl.GetMinWidows());
            Paragraph paragraph = (Paragraph)div.GetChildren()[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphWidowsControl paragraphWidowsControl = paragraph.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(paragraphWidowsControl);
            NUnit.Framework.Assert.AreEqual(3, paragraphWidowsControl.GetMinWidows());
        }

        [NUnit.Framework.Test]
        public virtual void WidowsPropertyOneInheritedOneRedefined() {
            IList<IElement> elements = ConvertToElements("widowsPropertyOneInheritedOneRedefined");
            Div div = (Div)elements[0];
            NUnit.Framework.Assert.IsNotNull(div);
            ParagraphWidowsControl divWidowsControl = div.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(divWidowsControl);
            NUnit.Framework.Assert.AreEqual(3, divWidowsControl.GetMinWidows());
            Paragraph paragraph = (Paragraph)div.GetChildren()[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphWidowsControl paragraphWidowsControl = paragraph.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(paragraphWidowsControl);
            NUnit.Framework.Assert.AreEqual(3, paragraphWidowsControl.GetMinWidows());
            Paragraph anotherParagraph = (Paragraph)div.GetChildren()[1];
            NUnit.Framework.Assert.IsNotNull(anotherParagraph);
            ParagraphWidowsControl anotherParagraphWidowsControl = anotherParagraph.GetProperty<ParagraphWidowsControl
                >(Property.WIDOWS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(anotherParagraphWidowsControl);
            NUnit.Framework.Assert.AreEqual(4, anotherParagraphWidowsControl.GetMinWidows());
        }

        [NUnit.Framework.Test]
        public virtual void OrphansWidowsParallelInheritance() {
            IList<IElement> elements = ConvertToElements("orphansWidowsParallelInheritance");
            Div level1Div = (Div)elements[0];
            NUnit.Framework.Assert.IsNotNull(level1Div);
            ParagraphOrphansControl level1DivOrphansControl = level1Div.GetProperty<ParagraphOrphansControl>(Property.
                ORPHANS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(level1DivOrphansControl);
            NUnit.Framework.Assert.AreEqual(3, level1DivOrphansControl.GetMinOrphans());
            ParagraphWidowsControl level1DivWidowsControl = level1Div.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNull(level1DivWidowsControl);
            Div level2Div = (Div)level1Div.GetChildren()[0];
            NUnit.Framework.Assert.IsNotNull(level2Div);
            ParagraphOrphansControl level2DivOrphansControl = level2Div.GetProperty<ParagraphOrphansControl>(Property.
                ORPHANS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(level2DivOrphansControl);
            NUnit.Framework.Assert.AreEqual(3, level2DivOrphansControl.GetMinOrphans());
            ParagraphWidowsControl level2DivWidowsControl = level2Div.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(level2DivWidowsControl);
            NUnit.Framework.Assert.AreEqual(5, level2DivWidowsControl.GetMinWidows());
            Paragraph paragraph1 = (Paragraph)level2Div.GetChildren()[0];
            NUnit.Framework.Assert.IsNotNull(paragraph1);
            ParagraphOrphansControl paragraph1OrphansControl = paragraph1.GetProperty<ParagraphOrphansControl>(Property
                .ORPHANS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(paragraph1OrphansControl);
            NUnit.Framework.Assert.AreEqual(3, paragraph1OrphansControl.GetMinOrphans());
            ParagraphWidowsControl paragraph1WidowsControl = paragraph1.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(paragraph1WidowsControl);
            NUnit.Framework.Assert.AreEqual(5, paragraph1WidowsControl.GetMinWidows());
            Paragraph paragraph2 = (Paragraph)level2Div.GetChildren()[1];
            NUnit.Framework.Assert.IsNotNull(paragraph2);
            ParagraphOrphansControl paragraph2OrphansControl = paragraph2.GetProperty<ParagraphOrphansControl>(Property
                .ORPHANS_CONTROL);
            NUnit.Framework.Assert.IsNotNull(paragraph2OrphansControl);
            NUnit.Framework.Assert.AreEqual(4, paragraph2OrphansControl.GetMinOrphans());
            ParagraphWidowsControl paragraph2WidowsControl = paragraph2.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(paragraph2WidowsControl);
            NUnit.Framework.Assert.AreEqual(5, paragraph2WidowsControl.GetMinWidows());
        }

        [NUnit.Framework.Test]
        public virtual void AlterOrphansWidowsTest() {
            ConverterProperties converterProperties = new ConverterProperties();
            DefaultCssApplierFactory cssApplierFactory = new OrphansWidowsUnitTest.CustomBlockCssApplierFactory();
            converterProperties.SetCssApplierFactory(cssApplierFactory);
            IList<IElement> elements = HtmlConverter.ConvertToElements(new FileStream(sourceFolder + "orphansWidows.html"
                , FileMode.Open, FileAccess.Read), converterProperties);
            Div div = (Div)elements[0];
            NUnit.Framework.Assert.IsNotNull(div);
            Paragraph paragraph = (Paragraph)div.GetChildren()[0];
            NUnit.Framework.Assert.IsNotNull(paragraph);
            ParagraphWidowsControl paragraphWidowsControl = paragraph.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                );
            NUnit.Framework.Assert.IsNotNull(paragraphWidowsControl);
            NUnit.Framework.Assert.AreEqual(3, paragraphWidowsControl.GetMaxLinesToMove());
            NUnit.Framework.Assert.IsTrue(paragraphWidowsControl.IsOverflowOnWidowsViolation());
        }

        private IList<IElement> ConvertToElements(String name) {
            String sourceHtml = sourceFolder + name + ".html";
            return HtmlConverter.ConvertToElements(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read));
        }

        internal class CustomBlockCssApplierFactory : DefaultCssApplierFactory {
            public override ICssApplier GetCustomCssApplier(IElementNode tag) {
                if (TagConstants.P.Equals(tag.Name())) {
                    return new OrphansWidowsUnitTest.CustomBlockCssApplier();
                }
                return null;
            }
        }

        internal class CustomBlockCssApplier : BlockCssApplier {
            public override void Apply(ProcessorContext context, IStylesContainer stylesContainer, ITagWorker tagWorker
                ) {
                base.Apply(context, stylesContainer, tagWorker);
                Paragraph paragraph = (Paragraph)tagWorker.GetElementResult();
                ParagraphWidowsControl widowsControl = paragraph.GetProperty<ParagraphWidowsControl>(Property.WIDOWS_CONTROL
                    );
                if (widowsControl != null) {
                    widowsControl.SetMinAllowedWidows(widowsControl.GetMinWidows(), 3, true);
                    paragraph.SetProperty(Property.WIDOWS_CONTROL, widowsControl);
                }
            }
        }
    }
}
