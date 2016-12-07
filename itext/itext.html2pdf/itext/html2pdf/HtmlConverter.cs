/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Impl.Jsoup;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Versions.Attributes;
using iText.Kernel;

namespace iText.Html2pdf {
    public class HtmlConverter {
        private HtmlConverter() {
        }

        // TODO add overloads with Charset provided
        // TODO add overloads without automatic elements flushing
        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(String html, Stream pdfStream) {
            ConvertToPdf(html, pdfStream, "");
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(String html, Stream pdfStream, String baseUri) {
            ConvertToPdf(html, new PdfWriter(pdfStream), baseUri);
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(String html, PdfWriter pdfWriter) {
            ConvertToPdf(html, pdfWriter, "");
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(String html, PdfWriter pdfWriter, String baseUri) {
            Stream stream = new MemoryStream(html.GetBytes());
            ConvertToPdf(stream, pdfWriter, baseUri);
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(FileInfo htmlFile, FileInfo pdfFile) {
            ConvertToPdf(htmlFile, pdfFile, MediaDeviceDescription.CreateDefault());
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(FileInfo htmlFile, FileInfo pdfFile, MediaDeviceDescription deviceDescription
            ) {
            ConvertToPdf(new FileStream(htmlFile.FullName, FileMode.Open, FileAccess.Read), new FileStream(pdfFile.FullName
                , FileMode.Create), FileUtil.GetParentDirectory(htmlFile.FullName) + Path.DirectorySeparatorChar, deviceDescription
                );
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, Stream pdfStream) {
            ConvertToPdf(htmlStream, pdfStream, "");
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, Stream pdfStream, String baseUri) {
            ConvertToPdf(htmlStream, pdfStream, baseUri, MediaDeviceDescription.CreateDefault());
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, Stream pdfStream, String baseUri, MediaDeviceDescription
             deviceDescription) {
            ConvertToPdf(htmlStream, new PdfWriter(pdfStream), baseUri, deviceDescription);
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, PdfWriter pdfWriter) {
            ConvertToPdf(htmlStream, new PdfDocument(pdfWriter));
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, PdfDocument pdfDocument) {
            ConvertToPdf(htmlStream, pdfDocument, "");
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, PdfWriter pdfWriter, String baseUri) {
            ConvertToPdf(htmlStream, new PdfDocument(pdfWriter), baseUri);
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, PdfDocument pdfDocument, String baseUri) {
            ConvertToPdf(htmlStream, pdfDocument, baseUri, MediaDeviceDescription.CreateDefault());
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, PdfWriter pdfWriter, String baseUri, MediaDeviceDescription
             deviceDescription) {
            ConvertToPdf(htmlStream, new PdfDocument(pdfWriter), baseUri, deviceDescription);
        }

        /// <exception cref="System.IO.IOException"/>
        public static void ConvertToPdf(Stream htmlStream, PdfDocument pdfDocument, String baseUri, MediaDeviceDescription
             deviceDescription) {
            Document document = ConvertToDocument(htmlStream, pdfDocument, baseUri, deviceDescription);
            document.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        public static Document ConvertToDocument(Stream htmlStream, PdfWriter pdfWriter) {
            return ConvertToDocument(htmlStream, pdfWriter, "");
        }

        /// <exception cref="System.IO.IOException"/>
        public static Document ConvertToDocument(Stream htmlStream, PdfWriter pdfWriter, String baseUri) {
            return ConvertToDocument(htmlStream, pdfWriter, baseUri, MediaDeviceDescription.CreateDefault());
        }

        /// <exception cref="System.IO.IOException"/>
        public static Document ConvertToDocument(Stream htmlStream, PdfWriter pdfWriter, String baseUri, MediaDeviceDescription
             deviceDescription) {
            return ConvertToDocument(htmlStream, new PdfDocument(pdfWriter), baseUri, deviceDescription);
        }

        /// <exception cref="System.IO.IOException"/>
        public static Document ConvertToDocument(Stream htmlStream, PdfDocument pdfDocument, String baseUri, MediaDeviceDescription
             deviceDescription) {

            try 
            {
                String licenseKeyClassName = "iText.License.LicenseKey, itext.licensekey";
                String licenseKeyProductClassName = "iText.License.LicenseKeyProduct, itext.licensekey";
                String licenseKeyFeatureClassName = "iText.License.LicenseKeyProductFeature, itext.licensekey";
                String checkLicenseKeyMethodName = "ScheduledCheck";
                Type licenseKeyClass = GetClass(licenseKeyClassName);
                if ( licenseKeyClass != null ) 
                {                
                    Type licenseKeyProductClass = GetClass(licenseKeyProductClassName);
                    Type licenseKeyProductFeatureClass = GetClass(licenseKeyFeatureClassName);
                    Array array = Array.CreateInstance(licenseKeyProductFeatureClass, 0);
                    object[] objects = new object[] { "pdfHtml", 1, 0, array };
                    Object productObject = System.Activator.CreateInstance(licenseKeyProductClass, objects);
                    MethodInfo m = licenseKeyClass.GetMethod(checkLicenseKeyMethodName);
                    m.Invoke(System.Activator.CreateInstance(licenseKeyClass), new object[] {productObject});
                }   
            } 
            catch ( Exception e ) 
            {
                if ( !Kernel.Version.IsAGPLVersion() )
                {
                    throw;
                }
            }
            if (pdfDocument.GetReader() != null) {
                throw new Html2PdfException(Html2PdfException.PdfDocumentShouldBeInWritingMode);
            }
            IHtmlParser parser = new JsoupHtmlParser();
            String detectedCharset = DetectEncoding(htmlStream);
            IDocumentNode doc = parser.Parse(htmlStream, detectedCharset);
            ResourceResolver resourceResolver = new ResourceResolver(baseUri);
            ICssResolver cssResolver = new DefaultCssResolver(doc, deviceDescription, resourceResolver);
            Document document = Attacher.Attach(doc, cssResolver, pdfDocument, resourceResolver);
            return document;
        }

        private static Type GetClass(string className)
        {
            String licenseKeyClassFullName = null;
            Assembly assembly = typeof(HtmlConverter).Assembly;
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
                if (fileLoadExceptionMessage != null)
                {
                    try
                    {
                        type = System.Type.GetType(className);
                    }
                    catch
                    {
                        // empty
                    }
                }
            }
            return type;
        }

        public static IList<IElement> ConvertToElements(String html) {
            return ConvertToElements(html, "");
        }

        public static IList<IElement> ConvertToElements(String html, String baseUri) {
            return ConvertToElements(html, MediaDeviceDescription.CreateDefault(), baseUri);
        }

        public static IList<IElement> ConvertToElements(String html, MediaDeviceDescription deviceDescription) {
            return ConvertToElements(html, deviceDescription, "");
        }

        public static IList<IElement> ConvertToElements(String html, MediaDeviceDescription deviceDescription, String
             baseUri) {

            try 
            {
                String licenseKeyClassName = "iText.License.LicenseKey, itext.licensekey";
                String licenseKeyProductClassName = "iText.License.LicenseKeyProduct, itext.licensekey";
                String licenseKeyFeatureClassName = "iText.License.LicenseKeyProductFeature, itext.licensekey";
                String checkLicenseKeyMethodName = "ScheduledCheck";
                Type licenseKeyClass = GetClass(licenseKeyClassName);
                if ( licenseKeyClass != null ) 
                {                
                    Type licenseKeyProductClass = GetClass(licenseKeyProductClassName);
                    Type licenseKeyProductFeatureClass = GetClass(licenseKeyFeatureClassName);
                    Array array = Array.CreateInstance(licenseKeyProductFeatureClass, 0);
                    object[] objects = new object[] { "pdfHtml", 1, 0, array };
                    Object productObject = System.Activator.CreateInstance(licenseKeyProductClass, objects);
                    MethodInfo m = licenseKeyClass.GetMethod(checkLicenseKeyMethodName);
                    m.Invoke(System.Activator.CreateInstance(licenseKeyClass), new object[] {productObject});
                }   
            } 
            catch ( Exception e ) 
            {
                if ( !Kernel.Version.IsAGPLVersion() )
                {
                    throw;
                }
            }
            IHtmlParser parser = new JsoupHtmlParser();
            IDocumentNode doc = parser.Parse(html);
            ResourceResolver resourceResolver = new ResourceResolver(baseUri);
            ICssResolver cssResolver = new DefaultCssResolver(doc, deviceDescription, resourceResolver);
            return Attacher.Attach(doc, cssResolver, resourceResolver);
        }

        // TODO
        private static String DetectEncoding(Stream @in) {
            return "UTF-8";
        }
    }
}
