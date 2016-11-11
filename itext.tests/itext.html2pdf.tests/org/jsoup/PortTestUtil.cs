using System;
using System.IO;
using iText.IO.Util;

namespace Org.Jsoup {
    internal class PortTestUtil {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/../../";

        public static FileInfo GetFile(String filename) {
            return new FileInfo(sourceFolder + "resources/org/jsoup" + filename);
        }
    }
}