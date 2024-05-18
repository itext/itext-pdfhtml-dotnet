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
using iText.Html2pdf.Css;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class GridApplierUtilTest {
        [NUnit.Framework.Test]
        public virtual void ApplyColumnStartTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "2");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_COLUMN_START);
            NUnit.Framework.Assert.AreEqual(2, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyColumnEndTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_END, "4");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_COLUMN_END);
            NUnit.Framework.Assert.AreEqual(4, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyRowStartTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_ROW_START, "3");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_ROW_START);
            NUnit.Framework.Assert.AreEqual(3, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyRowEndTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_ROW_END, "11");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_ROW_END);
            NUnit.Framework.Assert.AreEqual(11, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyInvalidColumnStartTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, CssConstants.AUTO);
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_COLUMN_START);
            NUnit.Framework.Assert.IsNull(columnStart);
        }
    }
}
