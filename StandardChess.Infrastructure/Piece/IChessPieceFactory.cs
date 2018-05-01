namespace StandardChess.Infrastructure.Piece
{
    public interface IChessPieceFactory
    {
        IPiece CreatePawn(ChessPosition startingPosition, ChessColor color);
        IPiece CreateBishop(ChessPosition startingPosition, ChessColor color);
        IPiece CreateKnight(ChessPosition startingPosition, ChessColor color);
        IPiece CreateRook(ChessPosition startingPosition, ChessColor color);
        IPiece CreateQueen(ChessPosition startingPosition, ChessColor color);
        IPiece CreateKing(ChessPosition startingPosition, ChessColor color);
    }
}