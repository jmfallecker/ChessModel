namespace StandardChess.Infrastructure.Piece
{
    public interface IPawn
    {
        bool IsCapturableByEnPassant { get; }
        bool IsPromotable { get; }
    }
}