using System;
using System.IO;

namespace iText.Html2pdf.Jsoup {
    internal class PortTestUtil {
        public static readonly String sourceFolder =
            iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext.CurrentContext.TestDirectory) +
            "/";

        public static FileInfo GetFile(String filename) {
            return new FileInfo(sourceFolder + "resources/org/jsoup" + filename);
        }
    }
}