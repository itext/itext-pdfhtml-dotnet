<p align="center">
    <img src="./assets/iText_Logo_Small.png" alt="Logo iText">
</p>


![Nuget](https://img.shields.io/nuget/v/itext7.pdfhtml)
[![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](https://github.com/itext/itext7/blob/master/LICENSE.md)
![Nuget](https://img.shields.io/nuget/dt/itext7.pdfhtml)
![GitHub commit activity (branch)](https://img.shields.io/github/commit-activity/m/itext/itext-pdfhtml-dotnet)

html2PDF is an iText Core add-on available for Java and C# (.NET) that allows you to easily convert HTML and CSS into
standards
compliant PDFs that are accessible, searchable and usable for indexing.

### The key features of html2PDF are:

* Converting XML/HTML & CSS to PDF or PDF/A
* Convert XML/HTML & CSS to iText objects
* Good default support for HTML5 and CSS3
* Easily configurable and extensible when needed
* Not based on any browser engine implementation

For the full list of supported\unsupported features please refer to [Supported\Unsupported featues](https://kb.itextpdf.com/itext/what-features-are-supported-or-unsupported-in-pdfh)

Want to discover what's possible? Head over to
our [Demo Lab](https://itextpdf.com/demos/convert-html-css-to-pdf-free-online)! It contains a collection of
demo applications ready to use online!

### Getting started

The easiest way to get started is to use NuGet, just execute the following install command in the folder of your
project:

```shell
dotnet add package itext.pdfhtml --version <REPLACE_WITH_DESIRED_ITEXT_VERSION>
```

For more advanced use cases, please refer to
the [Installation guidelines](https://kb.itextpdf.com/home/it7kb/installation-guidelines).
You can also [build iText Community from source][building].
### Hello html2PDF!

The following code snippet shows how to convert HTML to PDF using the `HtmlConverter` class:

### Hello PDF!

The following example shows how easy it is to create a simple PDF document:

```csharp
using iText.Html2pdf.ConverterProperties;
using iText.Html2pdf.HtmlConverter;

namespace HelloPdf {
    class Program {
        static void Main(string[] args) {
            // Base URI is required to resolve the path to source files
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(resourceLoc);
            
            HtmlConverter.ConvertToPdf(
                new FileStream(htmlSource, FileMode.Open, FileAccess.Read, FileShare.Read), 
                new FileStream(pdfDest, FileMode.Create, FileAccess.Write), 
                converterProperties);
        }
    }
}
```

### Examples

For more advanced examples, refer to our [Knowledge Base](https://kb.itextpdf.com/home/it7kb/examples) or the
main [Examples repo](https://github.com/itext/i7ns-samples). You can find C# equivalents to the
Java [Signing examples](https://github.com/itext/i7js-signing-examples) [here](https://github.com/itext/itext-publications-samples-dotnet/tree/develop/itext/itext.publications),
though the Java code is very similar since they have the same API.

Some of the output PDF files will be incorrectly displayed by the GitHub previewer, so be sure to download them to see
the correct
results.

| Description                                           | Link                                                                                                                                                                                                                                                                                                              |
|-------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Convert HTML to PDF/A-3B compliant document           | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/pdfhtml/HtmlToPdfA3Convert.cs), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/pdfhtml/cmp_HtmlToPdfA3Convert.pdf)       |
| Convert HTML with custom QR code tags to PDF          | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/pdfhtml/ParseHtmlQRcode.cs), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/pdfhtml/cmp_qrcode.pdf)                      |
| Create accessible tagged PDF from HTML content        | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/pdfhtml/CreateAccessiblePDF.cs), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/pdfhtml/cmp_Accessibility.pdf)           |
| Convert HTML to PDF simulating color blindness vision | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/pdfhtml/ParseHtmlColorBlind.cs), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/pdfhtml/cmp_rainbow_colourBlind.pdf)     |
| Register custom fonts for HTML to PDF                 | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/pdfhtml/UsingCustomFonts.cs)                                                                                                                                                                |
| Convert HTML forms to tagged PDF with custom roles    | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/pdfhtml/PdfHtmlFormTagging.cs), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/pdfhtml/cmp_changeFormRole.pdf)           |
| Convert HTML file with assets                         | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/layout/ParagraphTextWithStyle.cs), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/layout/cmp_paragraphTextWithStyle.pdf) |
| Enable formfield interactivity                        | [C#](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/itext/samples/sandbox/layout/ParagraphTextWithStyle.cs), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/layout/cmp_paragraphTextWithStyle.pdf) |
| Convert HTML containing arabic and hebrew             | [C#](https://kb.itextpdf.com/itext/how-to-convert-html-containing-arabic-hebrew-chara), [PDF](https://github.com/itext/itext-publications-samples-dotnet/blob/master/itext/itext.samples/cmpfiles/sandbox/layout/cmp_paragraphTextWithStyle.pdf)                                                                  |

### FAQs, tutorials, etc. ###
Check out the [iText Knowledge Base](https://kb.itextpdf.com) for tutorials, [FAQs](https://kb.itextpdf.com/itext/faq) and more. 
Many common questions have already been answered
on [Stack Overflow](https://stackoverflow.com/questions/tagged/itext+html2pdf), so make sure to also check there.

### Contributing

Many people have contributed to **iText Community** over the years. If you've found a bug, a mistake in documentation, or have a hot new feature you want to implement, we welcome your contributions.

Small changes or fixes can be submitted as a [Pull Request](https://github.com/itext/itext-pdfhtml-dotnet/pulls), while for major changes we request you contact us at community@apryse.com so we can better coordinate our efforts and prevent duplication of work.

Please read our [Contribution Guidelines][contributing] for details on code submissions, coding rules, and more.

### Licensing

**html2Pdf** is dual licensed as [AGPL][agpl]/[Commercial software][sales].

AGPL is a free/open-source software license, however, this doesn't mean the software is [gratis][gratis]!

The AGPL is a copyleft license, which means that any derivative work must also be licensed under the same terms. If you’re using iText in software or a service which cannot comply with the AGPL terms, we have a commercial license available that exempts you from such obligations.

Contact [Sales] for more info.

[agpl]: LICENSE.md

[building]: BUILDING.md

[contributing]: CONTRIBUTING.md

[sales]: https://itextpdf.com/sales

[gratis]: https://en.wikipedia.org/wiki/Gratis_versus_libre
