using System;
using System.Text.RegularExpressions;

namespace iText.Html2pdf.Attach.Impl.Layout.Util {
    public class REMatcher {
        private Regex pattern;

        private Match matcher;

        public REMatcher(String s) {
            pattern = iText.IO.Util.StringUtil.RegexCompile(s, Regex.UNICODE_CHARACTER_CLASS | Regex.DOTALL);
        }

        public virtual void SetStringForMatch(String s) {
            matcher = iText.IO.Util.StringUtil.Match(pattern, s);
        }

        public virtual bool Find() {
            return matcher.Find();
        }

        public virtual String Group(int index) {
            return iText.IO.Util.StringUtil.Group(matcher, index);
        }
    }
}
