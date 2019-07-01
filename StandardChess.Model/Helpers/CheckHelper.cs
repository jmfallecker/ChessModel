using StandardChess.Infrastructure;
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
        public bool DoesPotentialMoveLeaveKingInCheck(IPlayerAction potentialMove, IPiece king, ChessGame copy)
        {
            bool isKingMovingCurrently = king.Location == potentialMove.StartingPosition;
            
            copy.GameBoard.State.Clear();
            copy.GameBoard.State.Add(copy.ActivePlayerBoardState);
            copy.GameBoard.State.Add(copy.InactivePlayerBoardState);

            copy.GameBoard.Execute(potentialMove);

            ChessPosition positionToCheck = isKingMovingCurrently ? potentialMove.EndingPosition : king.Location;

            return copy.InactivePlayerPieces.Any(p =>
            {
                p.GenerateThreatened(copy.GameBoard.State, copy.InactivePlayerBoardState);
                p.GenerateCaptures(copy.GameBoard.State, copy.InactivePlayerBoardState);

                bool canCaptureAtPosition = p.CanCaptureAt(positionToCheck);
                bool isThreateningAtPosition = p.IsThreateningAt(positionToCheck);

                return canCaptureAtPosition || isThreateningAtPosition;
            });
        }
    }
}
