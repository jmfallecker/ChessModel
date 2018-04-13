using StandardChess.Infrastructure;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.Interfaces
{
    public interface ICapturable
    {
        void GenerateCaptures(BoardState boardState, BoardState owningPlayerPieceBitBoard);
        bool CanCaptureAt(ChessPosition location);
        void CaptureAt(ChessPosition location);
    }
}
