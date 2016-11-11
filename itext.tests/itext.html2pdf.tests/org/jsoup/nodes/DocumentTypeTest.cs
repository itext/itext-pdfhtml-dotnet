namespace Org.Jsoup.Nodes {
    /// <summary>Tests for the DocumentType node</summary>
    /// <author>Jonathan Hedley, http://jonathanhedley.com/</author>
    public class DocumentTypeTest {
        [NUnit.Framework.Test]
        public virtual void ConstructorValidationOkWithBlankName() {
            DocumentType fail = new DocumentType("", "", "", "");
        }

        [NUnit.Framework.Test]
        public virtual void ConstructorValidationOkWithBlankPublicAndSystemIds() {
            DocumentType fail = new DocumentType("html", "", "", "");
        }

        [NUnit.Framework.Test]
        public virtual void OuterHtmlGeneration() {
            DocumentType html5 = new DocumentType("html", "", "", "");
            NUnit.Framework.Assert.AreEqual("<!doctype html>", html5.OuterHtml());
            DocumentType publicDocType = new DocumentType("html", "-//IETF//DTD HTML//", "", "");
            NUnit.Framework.Assert.AreEqual("<!DOCTYPE html PUBLIC \"-//IETF//DTD HTML//\">", publicDocType.OuterHtml(
                ));
            DocumentType systemDocType = new DocumentType("html", "", "http://www.ibm.com/data/dtd/v11/ibmxhtml1-transitional.dtd"
                , "");
            NUnit.Framework.Assert.AreEqual("<!DOCTYPE html \"http://www.ibm.com/data/dtd/v11/ibmxhtml1-transitional.dtd\">"
                , systemDocType.OuterHtml());
            DocumentType combo = new DocumentType("notHtml", "--public", "--system", "");
            NUnit.Framework.Assert.AreEqual("<!DOCTYPE notHtml PUBLIC \"--public\" \"--system\">", combo.OuterHtml());
        }
    }
}
