using System;
using System.Collections.Generic;
using System.Text;

namespace TowerOfHanoi.Model
{

    [Serializable]
    public class TowerEmptyException : Exception
    {
        public TowerEmptyException() { }
        public TowerEmptyException(string message) : base(message) { }
        public TowerEmptyException(string message, Exception inner) : base(message, inner) { }
        protected TowerEmptyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
