using System;
namespace TwentyOne
{
    public class FraudException : Exception
    {
        public FraudException()
            : base() { }
        public FraudException(string message)
            :base(message) { }
    }
}
