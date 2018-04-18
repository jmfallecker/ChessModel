using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.Interfaces
{
    public interface IPiece
    {
        ChessPosition Location { get; set; }
        bool HasMoved { get; set; }
        ChessColor Color { get; }
        IBoardState MoveSet { get; }
        IBoardState CaptureSet { get; }
        IBoardState ThreatenSet { get; }
        int Value { get; }

        void GenerateMoves(IBoardState boardState);
        bool CanMoveTo(ChessPosition position);
        void MoveTo(ChessPosition position);

        void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState);
        bool CanCaptureAt(ChessPosition location);
        void CaptureAt(ChessPosition location);
    }
}
