using System;
using System.Runtime.Serialization;

namespace KTypes.Exceptions
{
    [Serializable]
    public class UnhandledPromiseRejectionException : Exception
    {
        public UnhandledPromiseRejectionException() { }
        public UnhandledPromiseRejectionException(string message) : base(message) { }
        public UnhandledPromiseRejectionException(string message, Exception inner) : base(message, inner) { }
        protected UnhandledPromiseRejectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
