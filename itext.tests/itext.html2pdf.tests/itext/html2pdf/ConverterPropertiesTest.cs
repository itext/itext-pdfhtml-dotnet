/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Commons.Actions;
using iText.Commons.Actions.Contexts;
using iText.Html2pdf.Attach.Util;
using iText.Test;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("UnitTest")]
    public class ConverterPropertiesTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void GetDefaultMetaInfoTest() {
            ConverterProperties properties = new ConverterProperties();
            IMetaInfo metaInfo = properties.GetEventMetaInfo();
            NUnit.Framework.Assert.IsTrue(metaInfo.GetType().FullName.StartsWith(NamespaceConstant.PDF_HTML + "."));
        }

        [NUnit.Framework.Test]
        public virtual void SetEventMetaInfoAndGetTest() {
            ConverterProperties properties = new ConverterProperties();
            ConverterPropertiesTest.TestMetaInfo testMetaInfo = new ConverterPropertiesTest.TestMetaInfo();
            properties.SetEventMetaInfo(testMetaInfo);
            IMetaInfo metaInfo = properties.GetEventMetaInfo();
            NUnit.Framework.Assert.AreSame(testMetaInfo, metaInfo);
        }

        [NUnit.Framework.Test]
        public virtual void CheckDefaultsTest() {
            ConverterProperties properties = new ConverterProperties();
            NUnit.Framework.Assert.IsTrue(properties.IsImmediateFlush());
            NUnit.Framework.Assert.IsFalse(properties.IsCreateAcroForm());
            NUnit.Framework.Assert.AreEqual(10, properties.GetLimitOfLayouts());
            properties.SetImmediateFlush(false);
            properties.SetCreateAcroForm(true);
            properties.SetLimitOfLayouts(20);
            ConverterProperties propertiesCopied = new ConverterProperties(properties);
            NUnit.Framework.Assert.IsFalse(propertiesCopied.IsImmediateFlush());
            NUnit.Framework.Assert.IsTrue(propertiesCopied.IsCreateAcroForm());
            NUnit.Framework.Assert.AreEqual(20, propertiesCopied.GetLimitOfLayouts());
        }

        [NUnit.Framework.Test]
        public virtual void ConverterPropsSetDependencyWithNullInstance() {
            ConverterProperties converterProperties = new ConverterProperties();
            NUnit.Framework.Assert.Catch(typeof(ArgumentException), () => {
                converterProperties.RegisterDependency(typeof(AlternateDescriptionResolver), null);
            }
            );
        }

        [NUnit.Framework.Test]
        public virtual void ConverterPropsSetDependencyWithNullType() {
            ConverterProperties converterProperties = new ConverterProperties();
            Func<Object> dummySupplier = () => new Object();
            NUnit.Framework.Assert.Catch(typeof(ArgumentException), () => {
                converterProperties.RegisterDependency(null, dummySupplier);
            }
            );
        }

        [NUnit.Framework.Test]
        public virtual void GetDependenciesTest() {
            ConverterProperties converterProperties = new ConverterProperties();
            Object dummyObject = new Object();
            Func<Object> dummySupplier = () => dummyObject;
            converterProperties.RegisterDependency(typeof(AlternateDescriptionResolver), dummySupplier);
            IDictionary<Type, Object> dependencies = converterProperties.GetDependencies();
            NUnit.Framework.Assert.AreEqual(1, dependencies.Count);
            NUnit.Framework.Assert.AreSame(dummyObject, dependencies.Get(typeof(AlternateDescriptionResolver)));
        }

        private class TestMetaInfo : IMetaInfo {
        }
    }
}
