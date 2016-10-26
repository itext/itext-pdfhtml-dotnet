using System;
using System.IO;
using System.Runtime.Serialization;

namespace Org.Jsoup
{
    [Serializable]
    internal class MalformedURLException : IOException
    {
        public MalformedURLException()
        {
        }

        public MalformedURLException(string message) : base(message)
        {
        }

        public MalformedURLException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MalformedURLException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}