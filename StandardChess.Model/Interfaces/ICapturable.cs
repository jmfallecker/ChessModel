using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Model.Interfaces
{
    public interface ICapturable
    {
        void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerPieceBitBoard);
        bool CanCaptureAt(ChessPosition location);
        void CaptureAt(ChessPosition location);
    }
}
