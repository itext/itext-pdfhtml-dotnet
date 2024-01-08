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
using iText.Test;

namespace iText.Html2pdf.Resolver.Form {
    [NUnit.Framework.Category("UnitTest")]
    public class NameResolverTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void NormalizationTest() {
            RunTest("Dots.Should.Be.Removed", "DotsShouldBeRemoved");
        }

        [NUnit.Framework.Test]
        public virtual void NoNameTest() {
            RunTest(new String[] { "", null, "  " }, new String[] { "Field", "Field_1", "Field_2" });
        }

        [NUnit.Framework.Test]
        public virtual void EqualNamesTest() {
            RunTest(new String[] { "name", "name", "name_4", "name", "name_3", "name_-2", "name_-2" }, new String[] { 
                "name", "name_1", "name_4", "name_5", "name_6", "name_-2", "name_-2_1" });
        }

        [NUnit.Framework.Test]
        public virtual void SeparatorTest() {
            RunTest(new String[] { "field_name", "field_name_2", "field_name" }, new String[] { "field_name", "field_name_2"
                , "field_name_3" });
        }

        private void RunTest(String input, String expectedOutput) {
            FormFieldNameResolver nameResolver = new FormFieldNameResolver();
            NUnit.Framework.Assert.AreEqual(expectedOutput, nameResolver.ResolveFormName(input));
        }

        private void RunTest(String[] input, String[] expectedOutput) {
            NUnit.Framework.Assert.IsTrue(input.Length == expectedOutput.Length, "Input and output should be the same length"
                );
            FormFieldNameResolver nameResolver = new FormFieldNameResolver();
            String[] output = new String[input.Length];
            for (int i = 0; i < input.Length; ++i) {
                output[i] = nameResolver.ResolveFormName(input[i]);
            }
            NUnit.Framework.Assert.AreEqual(expectedOutput, output);
        }
    }
}
