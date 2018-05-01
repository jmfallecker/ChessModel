using System.Collections.Generic;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;

namespace StandardChess.Infrastructure.Utility
{
    public interface IMoveHistory
    {
        IList<(IMovable movable, IPiece piece)> Moves { get; }
        IList<string> MovesByNotation { get; }
        int Count { get; }
        bool WasPieceCapturedInLastFiftyMoves { get; }
        bool WasPawnMovedInLastFiftyMoves { get; }
        void Add(IPiece piece, IMovable movable);
    }
}
