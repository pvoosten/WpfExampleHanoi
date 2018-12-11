using System;
using System.Collections.Generic;
using System.Text;

namespace TowerOfHanoi.Model
{

    [Serializable]
    public class InvalidDiscCountException : Exception
    {
        public InvalidDiscCountException() { }
        public InvalidDiscCountException(string message) : base(message) { }
        public InvalidDiscCountException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDiscCountException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
