using StandardChess.Infrastructure;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.Interfaces;

namespace StandardChess.Model.PieceModel
{
    public class ChessPieceFactory : IChessPieceFactory
    {
        public Pawn CreatePawn(ChessPosition startingPosition, ChessColor color)
        {
            return new Pawn(startingPosition, color);
        }

        public Bishop CreateBishop(ChessPosition startingPosition, ChessColor color)
        {
            return new Bishop(startingPosition, color);
        }

        public Knight CreateKnight(ChessPosition startingPosition, ChessColor color)
        {
            return new Knight(startingPosition, color);
        }

        public Rook CreateRook(ChessPosition startingPosition, ChessColor color)
        {
            return new Rook(startingPosition, color);
        }

        public Queen CreateQueen(ChessPosition startingPosition, ChessColor color)
        {
            return new Queen(startingPosition, color);
        }

        public King CreateKing(ChessPosition startingPosition, ChessColor color)
        {
            return new King(startingPosition, color);
        }
    }
}