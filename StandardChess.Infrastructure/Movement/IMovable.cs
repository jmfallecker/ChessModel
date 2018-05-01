namespace StandardChess.Infrastructure.Movement
{
    public interface IMovable
    {
        ChessPosition StartingPosition { get; set; }
        ChessPosition EndingPosition { get; set; }
    }
}