namespace StandardChess.Infrastructure.BoardInterfaces
{
    public interface IBoard
    {
        IBoardState State { get; }
        bool IsPositionOccupied(ChessPosition position);
        bool Add(ChessPosition position);
        bool Remove(ChessPosition position);
    }
}