using System.Collections.Generic;

namespace StandardChess.Infrastructure.BoardInterfaces
{
    public interface IBoardState : IEnumerable<ChessPosition>
    {
        IEnumerable<ChessPosition> OccupiedSquares { get; }
        bool Add(IBoardState boardState);
        bool Add(ChessPosition position);
        void Clear();
        bool Contains(ChessPosition position);
        bool IsPositionOccupied(ChessPosition position);
        void Remove(IBoardState stateToRemove);
        bool Remove(ChessPosition position);
    }
}