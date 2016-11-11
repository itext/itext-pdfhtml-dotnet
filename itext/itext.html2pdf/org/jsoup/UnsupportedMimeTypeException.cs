using System;

namespace Org.Jsoup {
    /// <summary>Signals that a HTTP response returned a mime type that is not supported.</summary>
    public class UnsupportedMimeTypeException : System.IO.IOException {
        private String mimeType;

        private String url;

        public UnsupportedMimeTypeException(String message, String mimeType, String url)
            : base(message) {
            this.mimeType = mimeType;
            this.url = url;
        }

        public virtual String GetMimeType() {
            return mimeType;
        }

        public virtual String GetUrl() {
            return url;
        }

        public override String ToString() {
            return base.ToString() + ". Mimetype=" + mimeType + ", URL=" + url;
        }
    }
}
