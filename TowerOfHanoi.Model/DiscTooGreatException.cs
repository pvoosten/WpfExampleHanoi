using System;
using System.Collections.Generic;
using System.Text;

namespace TowerOfHanoi.Model
{

    [Serializable]
    public class DiscTooGreatException : Exception
    {
        public DiscTooGreatException() { }
        public DiscTooGreatException(string message) : base(message) { }
        public DiscTooGreatException(string message, Exception inner) : base(message, inner) { }
        protected DiscTooGreatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
