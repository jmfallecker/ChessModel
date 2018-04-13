using StandardChess.Infrastructure;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.Interfaces
{
    public interface IBoardState
    {
        bool Add(BoardState boardState);
        bool Add(ChessPosition position);
        void Clear();
        bool Contains(ChessPosition position);
        bool IsPositionOccupied(ChessPosition position);
        void Remove(BoardState stateToRemove);
        bool Remove(ChessPosition position);
    }
}