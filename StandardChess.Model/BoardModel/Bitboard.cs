using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Model.BoardModel
{
    public class Bitboard : IBitboard
    {
        public ulong State { get; private set; }

        public bool IsLocationOccupied(ChessPosition position)
        {
            return (position & (ChessPosition)State) == position;
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