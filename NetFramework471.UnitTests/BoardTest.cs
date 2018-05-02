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
    public class BoardTest
    {
        [TestMethod]
        public void Should_AddPieceAtLocation()
        {
            // setup
            IBoard board = ModelLocator.Board;
            const ChessPosition POSITION = ChessPosition.A4;

            // execute

            // add a position that is not in the starting positions in case concrete adds starting positions
            // by default
            board.Add(POSITION);

            // verify
            Assert.IsTrue(board.State.IsPositionOccupied(POSITION));
        }

        [TestMethod]
        public void Should_RemovePieceAtLocation()
        {
            // setup
            IBoard board = ModelLocator.Board;
            const ChessPosition POSITION = ChessPosition.A4;
            // add a position that is not in the starting positions in case concrete adds starting positions
            // by default
            board.Add(POSITION);

            // execute
            board.Remove(POSITION);

            // verify
            Assert.IsFalse(board.State.IsPositionOccupied(POSITION));
        }

        [TestMethod]
        public void Should_ReturnTrueForOccupiedLocation()
        {
            // setup
            IBoard board = ModelLocator.Board;
            const ChessPosition POSITION = ChessPosition.A4;
            board.Add(POSITION);

            // execute
            bool isOccupied = board.IsPositionOccupied(POSITION);

            // verify
            Assert.IsTrue(isOccupied);
        }
    }
}
