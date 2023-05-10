/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Exceptions;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class TaggedPdfFormTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/TaggedPdfFormTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/TaggedPdfFormTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleTextFieldTagged() {
            ConvertToPdfAcroformFlattenAndCompare("simpleTextField", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleTextareaTagged() {
            ConvertToPdfAcroformFlattenAndCompare("simpleTextarea", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleButtonTagged() {
            ConvertToPdfAcroformFlattenAndCompare("simpleButton", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleLabelTagged() {
            ConvertToPdfAcroformFlattenAndCompare("simpleLabel", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleCheckboxTagged() {
            ConvertToPdfAcroformFlattenAndCompare("simpleCheckbox", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleSelectTagged() {
            ConvertToPdfAcroformFlattenAndCompare("simpleSelect", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void ListBoxSelectTagged() {
            ConvertToPdfAcroformFlattenAndCompare("listBoxSelect", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.OPTGROUP_NOT_SUPPORTED_IN_INTERACTIVE_SELECT, Count = 2)]
        public virtual void ListBoxOptGroupSelectTagged() {
            ConvertToPdfAcroformFlattenAndCompare("listBoxOptGroupSelect", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleRadioFormTagged() {
            ConvertToPdfAcroformFlattenAndCompare("simpleRadioForm", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-980. DefaultHtmlProcessor ERROR No worker found for tag datalist")]
        public virtual void DataListFormTagged() {
            ConvertToPdfAcroformFlattenAndCompare("dataListForm", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void FieldSetFormTagged() {
            ConvertToPdfAcroformFlattenAndCompare("fieldSetForm", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void InputFormPrematureFlush() {
            NUnit.Framework.Assert.That(() =>  {
                // TODO DEVSIX-4601
                // exception is thrown on "convert tagged PDF with acroform" stage
                ConvertToPdfAcroformFlattenAndCompare("inputFormPrematureFlush", sourceFolder, destinationFolder, true);
            }
            , NUnit.Framework.Throws.InstanceOf<PdfException>().With.Message.EqualTo(KernelExceptionMessageConstant.TAG_STRUCTURE_FLUSHING_FAILED_IT_MIGHT_BE_CORRUPTED))
;
        }
    }
}
