using StandardChess.Infrastructure;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.BoardModel
{
    public class Bitboard
    {
        public ulong State { get; private set; }

        public Bitboard()
        {
            State = 0x0;
        }

        public bool IsLocationOccupied(ChessPosition position)
        {
            return ((position & (ChessPosition)State) == position);
        }

        public bool AddPieceToBoard(ChessPosition position)
        {
            if (IsLocationOccupied(position))
                return false;

            State |= (ulong)position;
            return true;
        }

        public bool RemovePieceFromBoard(ChessPosition position)
        {
            if (!IsLocationOccupied(position))
                return false;

            State ^= (ulong)position;
            return true;
        }

        public void ClearAllPieces()
        {
            State &= 0x0;
        }
    }
}
