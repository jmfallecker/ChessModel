using System;

namespace StandardChess.Model.Exceptions
{
    public class PawnPromotionException : Exception
    {
        public PawnPromotionException() { }

        public PawnPromotionException(string message) : base(message) { }

        public PawnPromotionException(string message, Exception innerException) : base(message, innerException) { }
    }
}