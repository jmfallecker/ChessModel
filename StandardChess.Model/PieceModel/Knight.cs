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
    public class Knight : Piece, IKnight
    {
        public Knight(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 3;
        }

        /// <summary>
        ///     Generates all legal <see cref="IKnight" /> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(IBoardState boardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            MoveSet.Clear();

            GenerateNorthNorthEastMove(boardState, cpm);
            GenerateNorthNorthWestMove(boardState, cpm);

            GenerateSouthSouthEastMove(boardState, cpm);
            GenerateSouthSouthWestMove(boardState, cpm);

            GenerateEastNorthEastMove(boardState, cpm);
            GenerateEastSouthEastMove(boardState, cpm);

            GenerateWestNorthWestMove(boardState, cpm);
            GenerateWestSouthWestMove(boardState, cpm);
        }

        /// <summary>
        ///     Generates all legal <see cref="IKnight" /> captures
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            CaptureSet.Clear();
            IBoardState enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateEastNorthEastCapture(enemyBoardState, cpm);
            GenerateEastSouthEastCapture(enemyBoardState, cpm);
            GenerateNorthNorthEastCapture(enemyBoardState, cpm);
            GenerateNorthNorthWestCapture(enemyBoardState, cpm);
            GenerateSouthSouthEastCapture(enemyBoardState, cpm);
            GenerateSouthSouthWestCapture(enemyBoardState, cpm);
            GenerateWestNorthWestCapture(enemyBoardState, cpm);
            GenerateWestSouthWestCapture(enemyBoardState, cpm);
        }

        #region Private Methods

        private void GenerateNorthNorthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.North(cpm.NorthEast(Location));

            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateNorthNorthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.North(cpm.NorthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateEastNorthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.East(cpm.NorthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateEastSouthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.East(cpm.SouthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateWestNorthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.West(cpm.NorthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateWestSouthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.West(cpm.SouthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateSouthSouthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.South(cpm.SouthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateSouthSouthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.South(cpm.SouthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateNorthNorthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.North(cpm.NorthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateNorthNorthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.North(cpm.NorthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthSouthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.South(cpm.SouthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthSouthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.South(cpm.SouthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateEastNorthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.East(cpm.NorthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateEastSouthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.East(cpm.SouthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateWestNorthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.West(cpm.NorthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateWestSouthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.West(cpm.SouthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        #endregion
    }
}