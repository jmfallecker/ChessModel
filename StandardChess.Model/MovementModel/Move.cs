using System.Collections.Generic;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.MovementModel
{
    public class Move : IMove
    {
        public ChessPosition StartingPosition { get; set; }
        public ChessPosition EndingPosition { get; set; }

        public override bool Equals(object obj)
        {
            var move = (Move)obj;
            return StartingPosition == move.StartingPosition &&
                   EndingPosition == move.EndingPosition;
        }
        public override int GetHashCode()
        {
            int hashCode = -1795512632;
            hashCode = hashCode * -1521134295 + StartingPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + EndingPosition.GetHashCode();
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
