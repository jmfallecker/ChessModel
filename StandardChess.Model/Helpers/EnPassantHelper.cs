using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Utility;

namespace StandardChess.Model.GameModel
{
    internal class EnPassantHelper
    {
        /// <summary>
        ///     Determines if move is a legal attempt at En Passant.
        /// </summary>
        /// <param name="capturingPiece"></param>
        /// <param name="capture"></param>
        /// <param name="gameBoard"></param>
        /// <returns></returns>
        public bool IsCaptureLegalEnPassant(IPiece capturingPiece, IPiece pieceBeingCaptured, ICapture capture, IBoard gameBoard)
        {
            // 1.) only Pawns can capture via En Passant
            if (!(capturingPiece is IPawn))
                return false;

            // 2.) white pawns must be on Rank 5, black pawns must be on Rank 4
            bool isCapturingPawnOnCorrectRank = capturingPiece.Color == ChessColor.White
                ? (capturingPiece.Location & ChessPosition.Rank5) == capturingPiece.Location
                : (capturingPiece.Location & ChessPosition.Rank4) == capturingPiece.Location;

            if (!isCapturingPawnOnCorrectRank)
                return false;

            // 3.) Pawn may not move to an occupied square
            if (gameBoard.IsPositionOccupied(capture.EndingPosition))
                return false;

            // 4.) piece must be a pawn & pawn must be capturable by en passant
            return pieceBeingCaptured is IPawn pawn && pawn.IsCapturableByEnPassant;
        }

        public ChessPosition GetLocationOfPieceBeingCaptured(IPiece capturingPiece, ICapture capture)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            // get the position of the piece we're trying to capture via En Passant
            ChessPosition locationOfPotentiallyCapturedPiece = capturingPiece.Color == ChessColor.White
                ? cpm.South(capture.EndingPosition)
                : cpm.North(capture.EndingPosition);
            
            return locationOfPotentiallyCapturedPiece;
        }
    }
}
