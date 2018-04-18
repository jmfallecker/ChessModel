using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Model.Interfaces
{
    public interface IMovable
    {
        void GenerateMoves(IBoardState boardState);
        bool CanMoveTo(ChessPosition position);
        void MoveTo(ChessPosition position);
    }
}
