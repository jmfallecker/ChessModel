namespace StandardChess.Infrastructure.BoardInterfaces
{
    public interface IBitboard
    {
        ChessPosition State { get; }
        bool IsLocationOccupied(ChessPosition position);
        bool AddPieceToBoard(ChessPosition position);
        bool RemovePieceFromBoard(ChessPosition position);
        void ClearAllPieces();
    }
}