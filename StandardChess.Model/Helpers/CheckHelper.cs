using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model.GameModel;
using System.Linq;

namespace StandardChess.Model.Helpers
{
    internal class CheckHelper
    {
        /// <summary>
        ///     Will the friendly King be in check if this move is made?
        /// </summary>
        /// <param name="potentialMove">Move to check</param>
        /// <returns></returns>
        public bool DoesPotentialMoveLeaveKingInCheck(IMovable potentialMove, IPiece king, ChessGame copy)
        {
            // get a new instance of a board
            IBoard board = ModelLocator.Board;
            foreach (ChessPosition chessPosition in copy.GameBoard.State) board.Add(chessPosition);
            
            bool isKingMovingCurrently = king.Location == potentialMove.StartingPosition;

            board.Execute(potentialMove);

            return copy.InactivePlayerPieces.Any(p =>
            {
                p.GenerateThreatened(board.State, copy.InactivePlayerBoardState);
                p.GenerateCaptures(board.State, copy.InactivePlayerBoardState);

                return p.CanCaptureAt(isKingMovingCurrently ? potentialMove.EndingPosition : king.Location) |
                       p.IsThreateningAt(isKingMovingCurrently ? potentialMove.EndingPosition : king.Location);
            });
        }
    }
}
