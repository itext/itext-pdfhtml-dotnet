/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using iText.IO;
using Microsoft.Extensions.Logging;
using iText.IO.Util;
using iText.Kernel.Counter;
using Versions.Attributes;

namespace iText.Html2pdf.Util {

    /// <summary>Utility class for handling operation related to reflections.</summary>
    public sealed class ReflectionUtils {

        private static readonly String LICENSEKEY = "iText.License.LicenseKey, itext.licensekey";
        private static readonly String LICENSEKEY_PRODUCT = "iText.License.LicenseKeyProduct, itext.licensekey";
        private static readonly String CHECK_LICENSEKEY_METHOD = "ScheduledCheck";

        private ReflectionUtils() {
        }

        /// <summary>Performs a scheduled license check.</summary>
        public static void ScheduledLicenseCheck()
        {
            try
            {
                Type licenseKeyClass = GetClass(LICENSEKEY);
                if ( licenseKeyClass != null )
                {
                    Type licenseKeyProductClass = GetClass(LICENSEKEY_PRODUCT);
                    object[] objects = new object[]
                    {
                        Html2PdfProductInfo.PRODUCT_NAME,
                        Html2PdfProductInfo.MAJOR_VERSION.ToString(),
                        Html2PdfProductInfo.MINOR_VERSION.ToString()
                    };
                    Object productObject = System.Activator.CreateInstance(licenseKeyProductClass, objects);
                    licenseKeyClass.GetMethod(CHECK_LICENSEKEY_METHOD)
                        .Invoke(System.Activator.CreateInstance(licenseKeyClass), new object[] { productObject });
                }
            }
            catch (Exception e)
            {
                if (!iText.Kernel.Version.IsAGPLVersion())
                {
                    throw;
                }
            }
        }

        private static Type GetClass(string className)
        {
            String licenseKeyClassFullName = null;
            Assembly assembly = typeof(ReflectionUtils).GetAssembly();
            Attribute keyVersionAttr = assembly.GetCustomAttribute(typeof(KeyVersionAttribute));
            if (keyVersionAttr is KeyVersionAttribute)
            {
                String keyVersion = ((KeyVersionAttribute)keyVersionAttr).KeyVersion;
                String format = "{0}, Version={1}, Culture=neutral, PublicKeyToken=8354ae6d2174ddca";
                licenseKeyClassFullName = String.Format(format, className, keyVersion);
            }
            Type type = null;
            if (licenseKeyClassFullName != null)
            {
                String fileLoadExceptionMessage = null;
                try
                {
                    type = System.Type.GetType(licenseKeyClassFullName);
                }
                catch (FileLoadException fileLoadException)
                {
                    fileLoadExceptionMessage = fileLoadException.Message;
                }
                if (type == null)
                {
                    try
                    {
                        type = System.Type.GetType(className);
                    }
                    catch
                    {
                        // empty
                    }
                    if (type == null && fileLoadExceptionMessage != null)
                    {
                        ITextLogManager.GetLogger(typeof(ReflectionUtils)).LogError(fileLoadExceptionMessage);
                    }
                }
            }
            return type;
        }
    }
}
