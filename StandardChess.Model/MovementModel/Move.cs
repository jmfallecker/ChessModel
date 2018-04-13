using System.Collections.Generic;
using StandardChess.Infrastructure;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.MovementModel
{
    /// <summary>
    /// Should only be used to move a piece. For captures see <see cref="Capture"/>
    /// </summary>
    public class Move
    {
        public ChessPosition StartingPosition { get; set; }
        public ChessPosition EndingPosition { get; set; }
        public bool IsCapture { get; protected set; }

        public override bool Equals(object obj)
        {
            Move move = (Move)obj;
            return StartingPosition == move.StartingPosition &&
                    EndingPosition == move.EndingPosition &&
                    IsCapture == move.IsCapture;
        }

        public override int GetHashCode()
        {
            var hashCode = -1795512632;
            hashCode = hashCode * -1521134295 + StartingPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + EndingPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + IsCapture.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Move move1, Move move2)
        {
            return EqualityComparer<Move>.Default.Equals(move1, move2);
        }

        public static bool operator !=(Move move1, Move move2)
        {
            return !(move1 == move2);
        }
    }
}
