using System;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class ATagUtilTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void BaseUrlIsNullTest() {
            String anchorLink = "Anchor link";
            String baseUrl = null;
            String modifiedLink = ATagUtil.ResolveAnchorLink(anchorLink, baseUrl);
            NUnit.Framework.Assert.AreEqual(anchorLink, modifiedLink);
        }

        [NUnit.Framework.Test]
        public virtual void LinkIsAnAnchorTest() {
            String anchorLink = "#Anchor link";
            String baseUrl = "https://not_existing_url.com";
            String modifiedLink = ATagUtil.ResolveAnchorLink(anchorLink, baseUrl);
            NUnit.Framework.Assert.AreEqual(anchorLink, modifiedLink);
        }

        [NUnit.Framework.Test]
        public virtual void BaseLinkTest() {
            String anchorLink = "Anchor link";
            String baseUrl = "Base url";
            String modifiedLink = ATagUtil.ResolveAnchorLink(anchorLink, baseUrl);
            NUnit.Framework.Assert.AreEqual(anchorLink, modifiedLink);
        }

        [NUnit.Framework.Test]
        public virtual void ResolvedURlEndsWIthSlashTest() {
            String anchorLink = "";
            String baseUrl = "/random_path/";
            String modifiedLink = ATagUtil.ResolveAnchorLink(anchorLink, baseUrl);
            NUnit.Framework.Assert.AreEqual(anchorLink, modifiedLink);
        }

        [NUnit.Framework.Test]
        public virtual void ResolveURlForExternalLinkTest() {
            String anchorLink = "Anchor";
            String baseUrl = "https://not_existing_url.com";
            String expectedUrl = "https://not_existing_url.com/Anchor";
            String modifiedLink = ATagUtil.ResolveAnchorLink(anchorLink, baseUrl);
            NUnit.Framework.Assert.AreEqual(expectedUrl, modifiedLink);
        }

        [NUnit.Framework.Test]
        public virtual void ResolveURlThrowsMalformedExceptionTest() {
            String anchorLink = "htt://not_existing_url.com";
            String baseUrl = "https://not_existing_url.com";
            String expectedUrl = "htt://not_existing_url.com";
            String modifiedLink = ATagUtil.ResolveAnchorLink(anchorLink, baseUrl);
            NUnit.Framework.Assert.AreEqual(expectedUrl, modifiedLink);
        }
    }
}
