namespace StandardChess.Infrastructure.Piece
{
    public interface IPawn : IPiece
    {
        bool IsCapturableByEnPassant { get; }
        bool IsPromotable { get; }
    }
}