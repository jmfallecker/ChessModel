using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class ChessPieceFactory : IChessPieceFactory
    {
        public IPiece CreatePawn(ChessPosition startingPosition, ChessColor color)
        {
            return new Pawn(startingPosition, color);
        }

        public IPiece CreateBishop(ChessPosition startingPosition, ChessColor color)
        {
            return new Bishop(startingPosition, color);
        }

        public IPiece CreateKnight(ChessPosition startingPosition, ChessColor color)
        {
            return new Knight(startingPosition, color);
        }

        public IPiece CreateRook(ChessPosition startingPosition, ChessColor color)
        {
            return new Rook(startingPosition, color);
        }

        public IPiece CreateQueen(ChessPosition startingPosition, ChessColor color)
        {
            return new Queen(startingPosition, color);
        }

        public IPiece CreateKing(ChessPosition startingPosition, ChessColor color)
        {
            return new King(startingPosition, color);
        }
    }
}