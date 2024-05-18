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
