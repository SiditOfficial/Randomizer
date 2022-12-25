using System;

namespace Sidit.Randomizer
{
    [Serializable]
    public class RareSumHigherThanMaxValueException : Exception
    {
        public RareSumHigherThanMaxValueException() { }
        public RareSumHigherThanMaxValueException(string message) : base(message) { }
        public RareSumHigherThanMaxValueException(string message, Exception inner) : base(message, inner) { }
        protected RareSumHigherThanMaxValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}