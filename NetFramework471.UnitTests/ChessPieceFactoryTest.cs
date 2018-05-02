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
using StandardChess.Infrastructure.Piece;
using StandardChess.Model;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class ChessPieceFactoryTest
    {
        private readonly IChessPieceFactory _factory = ModelLocator.ChessPieceFactory;

        [TestMethod]
        public void Should_CreatePawn()
        {
            // setup
            const ChessPosition POSITION = ChessPosition.A2;
            const ChessColor COLOR = ChessColor.White;

            // execute
            IPiece piece = _factory.CreatePawn(POSITION, COLOR);

            // verify
            Assert.AreEqual(POSITION, piece.Location);
            Assert.AreEqual(COLOR, piece.Color);
        }

        [TestMethod]
        public void Should_CreateBishop()
        {
            // setup
            const ChessPosition POSITION = ChessPosition.A2;
            const ChessColor COLOR = ChessColor.White;

            // execute
            IPiece piece = _factory.CreateBishop(POSITION, COLOR);

            // verify
            Assert.AreEqual(POSITION, piece.Location);
            Assert.AreEqual(COLOR, piece.Color);
        }

        [TestMethod]
        public void Should_CreateRook()
        {
            // setup
            const ChessPosition POSITION = ChessPosition.A2;
            const ChessColor COLOR = ChessColor.White;

            // execute
            IPiece piece = _factory.CreateRook(POSITION, COLOR);

            // verify
            Assert.AreEqual(POSITION, piece.Location);
            Assert.AreEqual(COLOR, piece.Color);
        }

        [TestMethod]
        public void Should_CreateQueen()
        {
            // setup
            const ChessPosition POSITION = ChessPosition.A2;
            const ChessColor COLOR = ChessColor.White;

            // execute
            IPiece piece = _factory.CreateQueen(POSITION, COLOR);

            // verify
            Assert.AreEqual(POSITION, piece.Location);
            Assert.AreEqual(COLOR, piece.Color);
        }

        [TestMethod]
        public void Should_CreateKnight()
        {
            // setup
            const ChessPosition POSITION = ChessPosition.A2;
            const ChessColor COLOR = ChessColor.White;

            // execute
            IPiece piece = _factory.CreateKnight(POSITION, COLOR);

            // verify
            Assert.AreEqual(POSITION, piece.Location);
            Assert.AreEqual(COLOR, piece.Color);
        }

        [TestMethod]
        public void Should_CreateKing()
        {
            // setup
            const ChessPosition POSITION = ChessPosition.A2;
            const ChessColor COLOR = ChessColor.White;

            // execute
            IPiece piece = _factory.CreateKing(POSITION, COLOR);

            // verify
            Assert.AreEqual(POSITION, piece.Location);
            Assert.AreEqual(COLOR, piece.Color);
        }
    }
}
