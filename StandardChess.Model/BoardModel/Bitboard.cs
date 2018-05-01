using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Model.BoardModel
{
    public class Bitboard : IBitboard
    {
        public ChessPosition State { get; private set; }

        public bool IsLocationOccupied(ChessPosition position)
        {
            return (position & State) == position;
        }

        public bool AddPieceToBoard(ChessPosition position)
        {
            if (IsLocationOccupied(position))
                return false;

            State |= position;
            return true;
        }

        public bool RemovePieceFromBoard(ChessPosition position)
        {
            if (!IsLocationOccupied(position))
                return false;

            State ^= position;
            return true;
        }

        public void ClearAllPieces()
        {
            State = ChessPosition.None;
        }
    }
}