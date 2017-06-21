/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf.Attach;
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
    /// <summary>The HtmlConverter is the class you will use most when converting HTML to PDF.</summary>
    /// <remarks>
    /// The HtmlConverter is the class you will use most when converting HTML to PDF.
    /// It contains a series of static methods that accept HTML as a <code>String</code>,
    /// <code>File</code>, or <code>InputStream</code>, and convert it to PDF in the
    /// form of an <code>OutputStream</code>, <code>File</code>, or a series of
    /// iText elements. It's also possible to write to a <code>PdfWriter</code> or
    /// <code>PdfDocument</code> instance.
    /// </remarks>
    public class HtmlConverter {
        /// <summary>Instantiates a new HtmlConverter instance.</summary>
        private HtmlConverter() {
        }

        // TODO: Auto-generated Javadoc
        // TODO add overloads with Charset provided
        // TODO add overloads without automatic elements flushing
        /// <summary>
        /// Converts a <code>String</code> containing HTML to an <code>OutputStream</code>
        /// containing PDF.
        /// </summary>
        /// <param name="html">the html in the form of a <code>String</code></param>
        /// <param name="pdfStream">the PDF as an <code>OutputStream</code></param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(String html, Stream pdfStream) {
            ConvertToPdf(html, pdfStream, null);
        }

        /// <summary>
        /// Converts a <code>String</code> containing HTML to an <code>OutputStream</code>
        /// containing PDF, using specific <code>ConverterProperties</code>.
        /// </summary>
        /// <param name="html">the html in the form of a <code>String</code></param>
        /// <param name="pdfStream">the PDF as an <code>OutputStream</code></param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(String html, Stream pdfStream, ConverterProperties converterProperties) {
            ConvertToPdf(html, new PdfWriter(pdfStream), converterProperties);
        }

        /// <summary>
        /// Converts a <code>String</code> containing HTML to PDF by writing PDF content
        /// to a <code>PdfWriter</code> instance.
        /// </summary>
        /// <param name="html">the html in the form of a <code>String</code></param>
        /// <param name="pdfWriter">the <code>PdfWriter</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(String html, PdfWriter pdfWriter) {
            ConvertToPdf(html, pdfWriter, null);
        }

        /// <summary>
        /// Converts a <code>String</code> containing HTML to PDF by writing PDF content
        /// to a <code>PdfWriter</code> instance, using specific <code>ConverterProperties</code>.
        /// </summary>
        /// <param name="html">the html in the form of a <code>String</code></param>
        /// <param name="pdfWriter">the <code>PdfWriter</code> instance</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(String html, PdfWriter pdfWriter, ConverterProperties converterProperties) {
            Stream stream = new MemoryStream(html.GetBytes());
            ConvertToPdf(stream, pdfWriter, converterProperties);
        }

        /// <summary>Converts HTML stored in a <code>File</code> to a PDF <code>File</code>.</summary>
        /// <param name="htmlFile">the <code>File</code> containing the source HTML</param>
        /// <param name="pdfFile">the <code>File</code> containing the resulting PDF</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(FileInfo htmlFile, FileInfo pdfFile) {
            ConvertToPdf(htmlFile, pdfFile, null);
        }

        /// <summary>
        /// Converts HTML stored in a <code>File</code> to a PDF <code>File</code>,
        /// using specific <code>ConverterProperties</code>.
        /// </summary>
        /// <param name="htmlFile">the <code>File</code> containing the source HTML</param>
        /// <param name="pdfFile">the <code>File</code> containing the resulting PDF</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(FileInfo htmlFile, FileInfo pdfFile, ConverterProperties converterProperties
            ) {
            if (converterProperties == null) {
                converterProperties = new ConverterProperties().SetBaseUri(FileUtil.GetParentDirectory(htmlFile.FullName) 
                    + System.IO.Path.DirectorySeparatorChar);
            }
            else {
                if (converterProperties.GetBaseUri() == null) {
                    converterProperties = new ConverterProperties(converterProperties).SetBaseUri(FileUtil.GetParentDirectory(
                        htmlFile.FullName) + System.IO.Path.DirectorySeparatorChar);
                }
            }
            ConvertToPdf(new FileStream(htmlFile.FullName, FileMode.Open, FileAccess.Read), new FileStream(pdfFile.FullName
                , FileMode.Create), converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to a PDF written to
        /// an <code>OutputStream</code>.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfStream">the <code>OutputStream</code> for the resulting PDF</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(Stream htmlStream, Stream pdfStream) {
            ConvertToPdf(htmlStream, pdfStream, null);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to a PDF written to
        /// an <code>OutputStream</code>.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfStream">the <code>OutputStream</code> for the resulting PDF</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(Stream htmlStream, Stream pdfStream, ConverterProperties converterProperties
            ) {
            ConvertToPdf(htmlStream, new PdfWriter(pdfStream), converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to objects that
        /// will be added to a <code>PdfDocument</code>.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfDocument">the <code>PdfDocument</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(Stream htmlStream, PdfDocument pdfDocument) {
            ConvertToPdf(htmlStream, pdfDocument, null);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to content that
        /// will be written to a <code>PdfWriter</code>.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfFile">the <code>File</code> containing the resulting PDF</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(Stream htmlStream, PdfWriter pdfWriter) {
            ConvertToPdf(htmlStream, new PdfDocument(pdfWriter));
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to content that
        /// will be written to a <code>PdfWriter</code>, using specific
        /// <code>ConverterProperties</code>.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfFile">the <code>File</code> containing the resulting PDF</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(Stream htmlStream, PdfWriter pdfWriter, ConverterProperties converterProperties
            ) {
            ConvertToPdf(htmlStream, new PdfDocument(pdfWriter), converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to objects that
        /// will be added to a <code>PdfDocument</code>, using specific <code>ConverterProperties</code>.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfDocument">the <code>PdfDocument</code> instance</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static void ConvertToPdf(Stream htmlStream, PdfDocument pdfDocument, ConverterProperties converterProperties
            ) {
            Document document = ConvertToDocument(htmlStream, pdfDocument, converterProperties);
            document.Close();
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to content that
        /// will be written to a <code>PdfWriter</code>, returning a <code>Document</code> instance.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfFile">the <code>File</code> containing the resulting PDF</param>
        /// <returns>a <code>Document</code> instance</returns>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static Document ConvertToDocument(Stream htmlStream, PdfWriter pdfWriter) {
            return ConvertToDocument(htmlStream, pdfWriter, null);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to content that
        /// will be written to a <code>PdfWriter</code>, using specific
        /// <code>ConverterProperties</code>, returning a <code>Document</code> instance.
        /// </summary>
        /// <param name="htmlStream">the html stream</param>
        /// <param name="pdfWriter">the pdf writer</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <returns>a <code>Document</code> instance</returns>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static Document ConvertToDocument(Stream htmlStream, PdfWriter pdfWriter, ConverterProperties converterProperties
            ) {
            return ConvertToDocument(htmlStream, new PdfDocument(pdfWriter), converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to objects that
        /// will be added to a <code>PdfDocument</code>, using specific <code>ConverterProperties</code>,
        /// returning a <code>Document</code> instance.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="pdfDocument">the <code>PdfDocument</code> instance</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <returns>a <code>Document</code> instance</returns>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static Document ConvertToDocument(Stream htmlStream, PdfDocument pdfDocument, ConverterProperties converterProperties
            ) {

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
            return Attacher.Attach(doc, pdfDocument, converterProperties);
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

        /// <summary>
        /// Converts a <code>String</code> containing HTML to a <code>List</code> of
        /// iText objects (<code>IElement</code> instances).
        /// </summary>
        /// <param name="html">the html in the form of a <code>String</code></param>
        /// <returns>a list of iText building blocks</returns>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static IList<IElement> ConvertToElements(String html) {
            Stream stream = new MemoryStream(html.GetBytes());
            return ConvertToElements(stream, null);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to a <code>List</code> of
        /// iText objects (<code>IElement</code> instances).
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <returns>a list of iText building blocks</returns>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static IList<IElement> ConvertToElements(Stream htmlStream) {
            return ConvertToElements(htmlStream, null);
        }

        /// <summary>
        /// Converts a <code>String</code> containing HTML to a <code>List</code> of
        /// iText objects (<code>IElement</code> instances), using specific
        /// <code>ConverterProperties</code>.
        /// </summary>
        /// <param name="html">the html in the form of a <code>String</code></param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <returns>a list of iText building blocks</returns>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static IList<IElement> ConvertToElements(String html, ConverterProperties converterProperties) {
            MemoryStream stream = new MemoryStream(html.GetBytes());
            return ConvertToElements(stream, converterProperties);
        }

        /// <summary>
        /// Converts HTML obtained from an <code>InputStream</code> to a <code>List</code> of
        /// iText objects (<code>IElement</code> instances), using specific
        /// <code>ConverterProperties</code>.
        /// </summary>
        /// <param name="htmlStream">the <code>InputStream</code> with the source HTML</param>
        /// <param name="converterProperties">a <code>ConverterProperties</code> instance</param>
        /// <returns>a list of iText building blocks</returns>
        /// <exception cref="System.IO.IOException">Signals that an I/O exception has occurred.</exception>
        public static IList<IElement> ConvertToElements(Stream htmlStream, ConverterProperties converterProperties
            ) {

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
            IDocumentNode doc = parser.Parse(htmlStream, DetectEncoding(htmlStream));
            return Attacher.Attach(doc, converterProperties);
        }

        /// <summary>Detects encoding of a specific <code>InputStream</code>.</summary>
        /// <param name="in">the <code>InputStream</code></param>
        /// <returns>the encoding; currently always returns "UTF-8".</returns>
        private static String DetectEncoding(Stream @in) {
            // TODO
            return "UTF-8";
        }
    }
}
