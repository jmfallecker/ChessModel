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
    public class Rook : Piece, IRook
    {
        public Rook(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 5;
        }

        /// <summary>
        ///     Generates all legal <see cref="Rook" /> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(IBoardState boardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            MoveSet.Clear();

            GenerateNorthMoves(boardState, cpm);
            GenerateSouthMoves(boardState, cpm);
            GenerateEastMoves(boardState, cpm);
            GenerateWestMoves(boardState, cpm);
        }

        /// <summary>
        ///     Generates all legal <see cref="Rook" /> captures
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            CaptureSet.Clear();
            IBoardState enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateNorthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateSouthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
        }


        #region Private Methods

        private void GenerateNorthMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.North);
        }

        private void GenerateSouthMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.South);
        }

        private void GenerateEastMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.East);
        }

        private void GenerateWestMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.West);
        }

        private void GenerateNorthCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState,
                                           IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.North);
        }

        private void GenerateSouthCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState,
                                           IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.South);
        }

        private void GenerateEastCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState,
                                          IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.East);
        }

        private void GenerateWestCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState,
                                          IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.West);
        }

        #endregion
    }
}