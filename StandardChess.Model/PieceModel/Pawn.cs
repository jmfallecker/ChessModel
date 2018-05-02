// .NET Standard 2.0 Chess Model
// Copyright(C) 2018 Joseph M Fallecker

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<https: //www.gnu.org/licenses/>.

using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Utility;

namespace StandardChess.Model.PieceModel
{
    public class Pawn : Piece, IPawn
    {
        public Pawn(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 1;
            IsCapturableByEnPassant = false;
            IsPromotable = false;
        }

        public bool IsCapturableByEnPassant { get; protected set; }
        public bool IsPromotable { get; protected set; }

        /// <summary>
        ///     Generates all legal <see cref="IPawn" /> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(IBoardState boardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            MoveSet.Clear();
            ChessPosition oneSpaceFromLocation = Color == ChessColor.White ? cpm.North(Location) : cpm.South(Location);
            bool oneSpaceMoveIsLegal = !boardState.Contains(oneSpaceFromLocation);

            if (oneSpaceMoveIsLegal)
                MoveSet.Add(oneSpaceFromLocation);
            else
                return;

            ChessPosition twoSpaceFromLocation = Color == ChessColor.White
                ? cpm.North(oneSpaceFromLocation)
                : cpm.South(oneSpaceFromLocation);
            bool twoSpaceMoveIsLegal = !boardState.Contains(twoSpaceFromLocation);

            if (!HasMoved && twoSpaceMoveIsLegal)
                MoveSet.Add(Color == ChessColor.White
                    ? cpm.North(cpm.North(Location))
                    : cpm.South(cpm.South(Location)));
        }

        /// <summary>
        ///     Generates all legal <see cref="IPawn" /> captures
        /// </summary>
        /// <param name="boardState">Current board state</param>
        /// <param name="owningPlayerBoardState">State of all of the moving color's pieces</param>
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            CaptureSet.Clear();

            IBoardState enemyPieces = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            CaptureSet.Add(GenerateCaptureEast(enemyPieces, cpm));
            CaptureSet.Add(GenerateCaptureWest(enemyPieces, cpm));
        }

        public override void GenerateThreatened(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;

            ThreatenSet.Clear();

            ThreatenSet.Add(GenerateThreatenedEast(cpm));
            ThreatenSet.Add(GenerateThreatenedWest(cpm));
        }

        /// <summary>
        ///     Will update whether the Pawn is able to be promoted or captured via En Passant.
        /// </summary>
        /// <param name="position"></param>
        public override void MoveTo(ChessPosition position)
        {
            if (!HasMoved)
            {
                IChessPieceMover cpm = ModelLocator.ChessPieceMover;
                bool moveIsTwoForward = Color == ChessColor.White
                    ? position == cpm.North(cpm.North(Location))
                    : position == cpm.South(cpm.South(Location));

                IsCapturableByEnPassant = moveIsTwoForward;
            }
            else
            {
                IsCapturableByEnPassant = false;
            }

            base.MoveTo(position);

            ChessPosition promotionRank = Color == ChessColor.White ? ChessPosition.Rank8 : ChessPosition.Rank1;

            if ((position & promotionRank) == position)
                IsPromotable = true;
        }

        #region Private Methods

        private ChessPosition GenerateCaptureEast(IBoardState enemyPieces, IChessPieceMover cpm)
        {
            ChessPosition potentialCapture =
                Color == ChessColor.White ? cpm.NorthEast(Location) : cpm.SouthEast(Location);
            bool isPieceAtCaptureLocation = enemyPieces.Contains(potentialCapture);

            return isPieceAtCaptureLocation ? potentialCapture : ChessPosition.None;
        }

        private ChessPosition GenerateCaptureWest(IBoardState enemyPieces, IChessPieceMover cpm)
        {
            ChessPosition potentialCapture =
                Color == ChessColor.White ? cpm.NorthWest(Location) : cpm.SouthWest(Location);
            bool isPieceAtCaptureLocation = enemyPieces.Contains(potentialCapture);

            return isPieceAtCaptureLocation ? potentialCapture : ChessPosition.None;
        }

        private ChessPosition GenerateThreatenedEast(IChessPieceMover cpm)
        {
            return Color == ChessColor.White ? cpm.NorthEast(Location) : cpm.SouthEast(Location);
        }

        private ChessPosition GenerateThreatenedWest(IChessPieceMover cpm)
        {
            return Color == ChessColor.White ? cpm.NorthWest(Location) : cpm.SouthWest(Location);
        }

        #endregion
    }
}