using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class Pawn : Piece
    {
        public bool IsCapturableByEnPassant { get; protected set; }
        public bool IsPromotable { get; protected set; }

        public Pawn(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 1;
            IsCapturableByEnPassant = false;
            IsPromotable = false;
        }

        /// <summary>
        /// Generates all legal <see cref="Pawn"/> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(IBoardState boardState)
        {
            var cpm = new ChessPieceMover();
            MoveSet.Clear();
            var oneSpaceFromLocation = (Color == ChessColor.White ? cpm.North(Location) : cpm.South(Location));
            var oneSpaceMoveIsLegal = !boardState.Contains(oneSpaceFromLocation);

            if (oneSpaceMoveIsLegal)
            {
                MoveSet.Add(oneSpaceFromLocation);
            }
            else
                return;

            var twoSpaceFromLocation = (Color == ChessColor.White ? cpm.North(oneSpaceFromLocation) : cpm.South(oneSpaceFromLocation));
            var twoSpaceMoveIsLegal = !boardState.Contains(twoSpaceFromLocation);

            if (!HasMoved && twoSpaceMoveIsLegal)
            {
                MoveSet.Add(Color == ChessColor.White ? cpm.North(cpm.North(Location)) : cpm.South(cpm.South(Location)));
            }
        }

        /// <summary>
        /// Generates all legal <see cref="Pawn"/> captures
        /// </summary>
        /// <param name="boardState">Current board state</param>
        /// <param name="owningPlayerBoardState">State of all of the moving color's pieces</param>
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            var cpm = new ChessPieceMover();
            CaptureSet.Clear();

            var enemyPieces = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            CaptureSet.Add(GenerateCaptureEast(enemyPieces, cpm));
            CaptureSet.Add(GenerateCaptureWest(enemyPieces, cpm));
        }

        public override void GenerateThreatened(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            var cpm = new ChessPieceMover();

            ThreatenSet.Clear();

            ThreatenSet.Add(GenerateThreatenedEast(cpm));
            ThreatenSet.Add(GenerateThreatenedWest(cpm));
        }

        /// <summary>
        /// Will update whether the Pawn is able to be promoted or captured via En Passant.
        /// </summary>
        /// <param name="position"></param>
        public override void MoveTo(ChessPosition position)
        {
            if (!HasMoved)
            {
                var cpm = new ChessPieceMover();
                bool moveIsTwoForward = (Color == ChessColor.White) ?
                    position == cpm.North(cpm.North(Location)) :
                    position == cpm.South(cpm.South(Location));

                IsCapturableByEnPassant = moveIsTwoForward;
            }
            else
            {
                IsCapturableByEnPassant = false;
            }

            base.MoveTo(position);

            var promotionRank = Color == ChessColor.White ?
                ChessPosition.Rank8 : ChessPosition.Rank1;

            if ((position & promotionRank) == position)
                IsPromotable = true;
        }

        #region Private Methods

        private ChessPosition GenerateCaptureEast(IBoardState enemyPieces, ChessPieceMover cpm)
        {
            var potentialCapture = (Color == ChessColor.White) ? cpm.NorthEast(Location) : cpm.SouthEast(Location);
            var isPieceAtCaptureLocation = enemyPieces.Contains(potentialCapture);

            return isPieceAtCaptureLocation ? potentialCapture : ChessPosition.None;
        }

        private ChessPosition GenerateCaptureWest(IBoardState enemyPieces, ChessPieceMover cpm)
        {
            var potentialCapture = (Color == ChessColor.White) ? cpm.NorthWest(Location) : cpm.SouthWest(Location);
            var isPieceAtCaptureLocation = enemyPieces.Contains(potentialCapture);

            return isPieceAtCaptureLocation ? potentialCapture : ChessPosition.None;
        }

        private ChessPosition GenerateThreatenedEast(ChessPieceMover cpm)
        {
            return (Color == ChessColor.White) ? cpm.NorthEast(Location) : cpm.SouthEast(Location);
        }

        private ChessPosition GenerateThreatenedWest(ChessPieceMover cpm)
        {
            return (Color == ChessColor.White) ? cpm.NorthWest(Location) : cpm.SouthWest(Location);
        }

        #endregion
    }
}
