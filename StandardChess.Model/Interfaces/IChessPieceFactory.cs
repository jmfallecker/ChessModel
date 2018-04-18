using StandardChess.Infrastructure;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.PieceModel;

namespace StandardChess.Model.Interfaces
{
    public interface IChessPieceFactory
    {
        Pawn CreatePawn(ChessPosition startingPosition, ChessColor color);
        Bishop CreateBishop(ChessPosition startingPosition, ChessColor color);
        Knight CreateKnight(ChessPosition startingPosition, ChessColor color);
        Rook CreateRook(ChessPosition startingPosition, ChessColor color);
        Queen CreateQueen(ChessPosition startingPosition, ChessColor color);
        King CreateKing(ChessPosition startingPosition, ChessColor color);
    }
}
