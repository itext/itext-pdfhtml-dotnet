/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BackgroundSvgImageTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BackgroundSvgImageTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BackgroundSvgImageTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        // ------------------- Tests with [percent percent] not-repeated background with different SVGs
        [NUnit.Framework.Test]
        public virtual void N30prc_50prc_percent_width_percent_heightTest() {
            ConvertToPdfAndCompare("30prc_50prc_percent_width_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // ------------------- Tests with [100px 100px] not-repeated background with different SVGs (different preserveAspectRatio values)
        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_prRatio_1Test() {
            ConvertToPdfAndCompare("100px_100px_viewbox_prRatio_1", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_prRatio_2Test() {
            ConvertToPdfAndCompare("100px_100px_viewbox_prRatio_2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_prRatio_3Test() {
            ConvertToPdfAndCompare("100px_100px_viewbox_prRatio_3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // ------------------- Tests with [600px auto] repeated background with different SVGs (a few different cases)
        [NUnit.Framework.Test]
        public virtual void Repeat_abs_auto_svg_abs_width_abs_height_viewboxTest() {
            ConvertToPdfAndCompare("repeat_abs_auto_svg_abs_width_abs_height_viewbox", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Repeat_abs_auto_svg_abs_width_abs_height_viewbox_2Test() {
            ConvertToPdfAndCompare("repeat_abs_auto_svg_abs_width_abs_height_viewbox_2", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Repeat_abs_auto_svg_abs_width_no_height_no_viewboxTest() {
            ConvertToPdfAndCompare("repeat_abs_auto_svg_abs_width_no_height_no_viewbox", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Repeat_abs_auto_svg_abs_width_no_height_viewboxTest() {
            ConvertToPdfAndCompare("repeat_abs_auto_svg_abs_width_no_height_viewbox", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Repeat_abs_auto_svg_no_width_abs_height_viewboxTest() {
            ConvertToPdfAndCompare("repeat_abs_auto_svg_no_width_abs_height_viewbox", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Repeat_abs_auto_svg_no_width_no_height_no_viewboxTest() {
            ConvertToPdfAndCompare("repeat_abs_auto_svg_no_width_no_height_no_viewbox", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Repeat_abs_auto_svg_no_width_no_height_viewboxTest() {
            ConvertToPdfAndCompare("repeat_abs_auto_svg_no_width_no_height_viewbox", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        // ------------------- Tests with [auto auto] not-repeated background with different SVGs (all 32 combinations)
        [NUnit.Framework.Test]
        public virtual void Auto_fixed_heightTest() {
            ConvertToPdfAndCompare("auto_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("auto_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_fixed_widthTest() {
            ConvertToPdfAndCompare("auto_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_nothing_are_setTest() {
            ConvertToPdfAndCompare("auto_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_heightTest() {
            ConvertToPdfAndCompare("auto_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("auto_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_widthTest() {
            ConvertToPdfAndCompare("auto_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_width_percent_heightTest() {
            ConvertToPdfAndCompare("auto_percent_width_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_width_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_percent_width_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Auto_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_fixed_heightTest() {
            ConvertToPdfAndCompare("auto_viewbox_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("auto_viewbox_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_fixed_widthTest() {
            ConvertToPdfAndCompare("auto_viewbox_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_nothing_are_setTest() {
            ConvertToPdfAndCompare("auto_viewbox_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_heightTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_height_percent_widthTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_widthTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Auto_viewbox_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("auto_viewbox_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // ------------------- Tests with [100px auto] not-repeated background with different SVGs (all 32 combinations)
        [NUnit.Framework.Test]
        public virtual void N100px_auto_fixed_heightTest() {
            ConvertToPdfAndCompare("100px_auto_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("100px_auto_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_auto_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_nothing_are_setTest() {
            ConvertToPdfAndCompare("100px_auto_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_heightTest() {
            ConvertToPdfAndCompare("100px_auto_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_auto_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_widthTest() {
            ConvertToPdfAndCompare("100px_auto_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_width_percent_heightTest() {
            ConvertToPdfAndCompare("100px_auto_percent_width_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_width_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_percent_width_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_fixed_heightTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_nothing_are_setTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_heightTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_height_percent_widthTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_widthTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_auto_viewbox_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_auto_viewbox_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // ------------------- Tests with [100px 100px] not-repeated background with different SVGs (all 32 combinations)
        [NUnit.Framework.Test]
        public virtual void N100px_100px_fixed_heightTest() {
            ConvertToPdfAndCompare("100px_100px_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("100px_100px_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_100px_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_nothing_are_setTest() {
            ConvertToPdfAndCompare("100px_100px_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_heightTest() {
            ConvertToPdfAndCompare("100px_100px_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_100px_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_widthTest() {
            ConvertToPdfAndCompare("100px_100px_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_width_percent_heightTest() {
            ConvertToPdfAndCompare("100px_100px_percent_width_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_width_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_percent_width_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_fixed_heightTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_nothing_are_setTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_heightTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_height_percent_widthTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_widthTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void N100px_100px_viewbox_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("100px_100px_viewbox_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        // ------------------- Tests with [contain] not-repeated background with different SVGs (all 32 combinations)
        [NUnit.Framework.Test]
        public virtual void Contain_fixed_heightTest() {
            ConvertToPdfAndCompare("contain_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("contain_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Contain_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_fixed_widthTest() {
            ConvertToPdfAndCompare("contain_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_nothing_are_setTest() {
            ConvertToPdfAndCompare("contain_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_heightTest() {
            ConvertToPdfAndCompare("contain_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("contain_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_widthTest() {
            ConvertToPdfAndCompare("contain_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_width_percent_heightTest() {
            ConvertToPdfAndCompare("contain_percent_width_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_width_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_percent_width_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Contain_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_fixed_heightTest() {
            ConvertToPdfAndCompare("contain_viewbox_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("contain_viewbox_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_fixed_widthTest() {
            ConvertToPdfAndCompare("contain_viewbox_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_nothing_are_setTest() {
            ConvertToPdfAndCompare("contain_viewbox_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_heightTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_height_percent_widthTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_widthTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Contain_viewbox_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("contain_viewbox_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // ------------------- Tests with [cover] not-repeated background with different SVGs (all 32 combinations)
        [NUnit.Framework.Test]
        public virtual void Cover_fixed_heightTest() {
            ConvertToPdfAndCompare("cover_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("cover_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_fixed_widthTest() {
            ConvertToPdfAndCompare("cover_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_nothing_are_setTest() {
            ConvertToPdfAndCompare("cover_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_heightTest() {
            ConvertToPdfAndCompare("cover_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("cover_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_widthTest() {
            ConvertToPdfAndCompare("cover_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_width_percent_heightTest() {
            ConvertToPdfAndCompare("cover_percent_width_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_width_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_percent_width_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Cover_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_fixed_heightTest() {
            ConvertToPdfAndCompare("cover_viewbox_fixed_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_fixed_height_percent_widthTest() {
            ConvertToPdfAndCompare("cover_viewbox_fixed_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_fixed_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_fixed_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_fixed_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_fixed_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_fixed_widthTest() {
            ConvertToPdfAndCompare("cover_viewbox_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_nothing_are_setTest() {
            ConvertToPdfAndCompare("cover_viewbox_nothing_are_set", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_nothing_are_set_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_nothing_are_set_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_heightTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_height_fixed_widthTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_height_fixed_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_height_fixed_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_height_fixed_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_height_percent_widthTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_height_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_height_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_height_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_height_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_height_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_widthTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_width", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Cover_viewbox_percent_width_prRatio_noneTest() {
            ConvertToPdfAndCompare("cover_viewbox_percent_width_prRatio_none", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
