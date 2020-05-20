

namespace WordChainGame.Common.CustomExceptions
{
    using System;

    public class InvalidWordException : Exception
    {
        public InvalidWordException()
        {
        }

        public InvalidWordException(string message) : base(message)
        {
        }

        public InvalidWordException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
