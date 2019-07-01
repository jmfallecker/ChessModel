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


using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class BitBoardTest
    {
        [TestMethod]
        public void Should_StartWithBlankState()
        {
            //setup
            IBitboard bitboard = ModelLocator.BitBoard;

            // verify
            Assert.AreEqual(ChessPosition.None, bitboard.State);
        }

        [TestMethod]
        public void Should_ReturnTrueAfterAddingAPiece()
        {
            // setup
            IBitboard bitboard = ModelLocator.BitBoard;
            const ChessPosition POSITION = ChessPosition.A1;

            // execute
            bitboard.AddPieceToBoard(POSITION);

            // verify
            Assert.AreEqual(bitboard.State, POSITION);
        }

        [TestMethod]
        public void Should_ReturnTrueWhenLocationIsOccupied()
        {
            // setup
            IBitboard bitboard = ModelLocator.BitBoard;
            const ChessPosition POSITION = ChessPosition.A1;

            // execute
            bitboard.AddPieceToBoard(POSITION);

            // verify
            Assert.IsTrue(bitboard.IsLocationOccupied(POSITION));
        }

        [TestMethod]
        public void Should_ReturnRemovePieceFromBoard()
        {
            // setup
            IBitboard bitboard = ModelLocator.BitBoard;
            const ChessPosition POSITION = ChessPosition.A1;
            bitboard.AddPieceToBoard(POSITION);

            // execute
            bitboard.RemovePieceFromBoard(POSITION);

            // verify
            Assert.AreEqual(bitboard.State, ChessPosition.None);
            Assert.IsFalse(bitboard.IsLocationOccupied(POSITION));
        }

        [TestMethod]
        public void Should_ClearAllPieces()
        {
            // setup
            IBitboard bitboard = ModelLocator.BitBoard;
            bitboard.AddPieceToBoard(ChessPosition.A1);
            bitboard.AddPieceToBoard(ChessPosition.A2);
            bitboard.AddPieceToBoard(ChessPosition.B1);
            bitboard.AddPieceToBoard(ChessPosition.D7);
            bitboard.AddPieceToBoard(ChessPosition.G8);
            bitboard.AddPieceToBoard(ChessPosition.H3);
            bitboard.AddPieceToBoard(ChessPosition.C4);

            // execute
            bitboard.ClearAllPieces();

            // verify
            Assert.AreEqual(bitboard.State, ChessPosition.None);
        }
    }
}