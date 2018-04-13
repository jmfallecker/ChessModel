using StandardChess.Infrastructure;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.Interfaces
{
    public interface IMovable
    {
        void GenerateMoves(BoardState boardState);
        bool CanMoveTo(ChessPosition position);
        void MoveTo(ChessPosition position);
    }
}
