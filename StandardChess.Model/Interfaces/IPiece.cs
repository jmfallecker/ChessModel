using StandardChess.Infrastructure;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.Interfaces
{
    public interface IPiece
    {
        ChessPosition Location { get; set; }
        bool HasMoved { get; set; }
        ChessColor Color { get; }
        BoardState MoveSet { get; }
        BoardState CaptureSet { get; }
        BoardState ThreatenSet { get; }
        int Value { get; }

        void GenerateMoves(BoardState boardState);
        bool CanMoveTo(ChessPosition position);
        void MoveTo(ChessPosition position);

        void GenerateCaptures(BoardState boardState, BoardState owningPlayerBoardState);
        bool CanCaptureAt(ChessPosition location);
        void CaptureAt(ChessPosition location);
    }
}
