using System;
using System.IO;

namespace Org.Jsoup.Helper {
    public class KeyVal {
        private String key;

        private String value;

        private Stream stream;

        public static Org.Jsoup.Helper.KeyVal Create(String key, String value) {
            return (Org.Jsoup.Helper.KeyVal)new Org.Jsoup.Helper.KeyVal().Key(key).Value(value);
        }

        public static Org.Jsoup.Helper.KeyVal Create(String key, String filename, Stream stream) {
            return (Org.Jsoup.Helper.KeyVal)new Org.Jsoup.Helper.KeyVal().Key(key).Value(filename).InputStream(stream);
        }

        private KeyVal() {
        }

        public virtual Org.Jsoup.Helper.KeyVal Key(String key) {
            Validate.NotEmpty(key, "Data key must not be empty");
            this.key = key;
            return this;
        }

        public virtual String Key() {
            return key;
        }

        public virtual Org.Jsoup.Helper.KeyVal Value(String value) {
            Validate.NotNull(value, "Data value must not be null");
            this.value = value;
            return this;
        }

        public virtual String Value() {
            return value;
        }

        public virtual Org.Jsoup.Helper.KeyVal InputStream(Stream inputStream) {
            Validate.NotNull(value, "Data input stream must not be null");
            this.stream = inputStream;
            return this;
        }

        public virtual Stream InputStream() {
            return stream;
        }

        public virtual bool HasInputStream() {
            return stream != null;
        }

        public override String ToString() {
            return key + "=" + value;
        }
    }
}
