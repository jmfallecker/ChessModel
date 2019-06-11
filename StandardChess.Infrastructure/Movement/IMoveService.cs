namespace StandardChess.Infrastructure.Movement
{
    public interface IMoveService
    {
        bool MovePiece(IMove move);
        bool CapturePiece(ICapture capture);
    }
}
