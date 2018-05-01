using System.Collections.Generic;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;

namespace StandardChess.Model.MovementModel
{
    public class Capture : ICapture
    {
        public ChessPosition StartingPosition { get; set; }
        public ChessPosition EndingPosition { get; set; }

        public override bool Equals(object obj)
        {
            var capture = (Capture) obj;
            return StartingPosition == capture.StartingPosition &&
                   EndingPosition == capture.EndingPosition;
        }

        public override int GetHashCode()
        {
            int hashCode = -1795512632;
            hashCode = hashCode * -1521134295 + StartingPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + EndingPosition.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Capture capture1, Capture capture2)
        {
            return EqualityComparer<Capture>.Default.Equals(capture1, capture2);
        }

        public static bool operator !=(Capture capture1, Capture capture2)
        {
            return !(capture1 == capture2);
        }
    }
}