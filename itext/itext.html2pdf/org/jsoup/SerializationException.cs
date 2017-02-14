using System;

namespace Org.Jsoup {
    /// <summary>A SerializationException is raised whenever serialization of a DOM element fails.</summary>
    /// <remarks>
    /// A SerializationException is raised whenever serialization of a DOM element fails. This exception usually wraps an
    /// <see cref="System.IO.IOException"/>
    /// that may be thrown due to an inaccessible output stream.
    /// </remarks>
    public sealed class SerializationException : Exception {
        /// <summary>Creates and initializes a new serialization exception with no error message and cause.</summary>
        public SerializationException()
            : base() {
        }

        /// <summary>Creates and initializes a new serialization exception with the given error message and no cause.</summary>
        /// <param name="message">the error message of the new serialization exception (may be <code>null</code>).</param>
        public SerializationException(String message)
            : base(message) {
        }

        /// <summary>
        /// Creates and initializes a new serialization exception with the specified cause and an error message of
        /// <code>(cause==null ? null : cause.toString())</code> (which typically contains the class and error message of
        /// <code>cause</code>).
        /// </summary>
        /// <param name="cause">the cause of the new serialization exception (may be <code>null</code>).</param>
        public SerializationException(Exception cause)
            : base(cause == null ? "Exception with null cause" : cause.Message, cause) {
        }

        /// <summary>Creates and initializes a new serialization exception with the given error message and cause.</summary>
        /// <param name="message">the error message of the new serialization exception.</param>
        /// <param name="cause">the cause of the new serialization exception.</param>
        public SerializationException(String message, Exception cause)
            : base(message, cause) {
        }
    }
}
