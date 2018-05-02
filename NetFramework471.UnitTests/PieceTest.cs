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


using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model.PieceModel;
using Unity.Interception.Utilities;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class PieceTest
    {
        private static readonly ChessPosition[] NotMovesNotCaptures =
            {ChessPosition.A5, ChessPosition.G2, ChessPosition.C4};

        private static readonly ChessPosition[] TestMoves = {ChessPosition.A1, ChessPosition.B2, ChessPosition.C7};
        private static readonly ChessPosition[] TestCaptures = {ChessPosition.A8, ChessPosition.B3, ChessPosition.A2};

        [TestMethod]
        public void Should_MoveToLocation()
        {
            // setup
            const ChessPosition FIRST_POSITION = ChessPosition.A1;
            const ChessPosition SECOND_POSITION = ChessPosition.A3;
            var piece = new TestPiece(FIRST_POSITION, ChessColor.White);

            Assert.IsFalse(piece.HasMoved);
            Assert.AreEqual(FIRST_POSITION, piece.Location);

            // execute
            piece.MoveTo(SECOND_POSITION);

            // verify
            Assert.IsTrue(piece.HasMoved);
            Assert.AreEqual(SECOND_POSITION, piece.Location);
        }

        [TestMethod]
        public void Should_CaptureAtLocation()
        {
            // setup
            const ChessPosition FIRST_POSITION = ChessPosition.A1;
            const ChessPosition SECOND_POSITION = ChessPosition.A3;
            var piece = new TestPiece(FIRST_POSITION, ChessColor.White);

            Assert.IsFalse(piece.HasMoved);
            Assert.AreEqual(FIRST_POSITION, piece.Location);

            // execute
            piece.CaptureAt(SECOND_POSITION);

            // verify
            Assert.IsTrue(piece.HasMoved);
            Assert.AreEqual(SECOND_POSITION, piece.Location);
        }

        [TestMethod]
        public void Should_ReturnTrueForCanMoveTo()
        {
            // setup
            var piece = new TestPiece(ChessPosition.D1, ChessColor.White);
            piece.GenerateMoves(null); // boardstate is not considered in test class

            // execute
            bool canMove = TestMoves.All(m => piece.CanMoveTo(m));

            // verify
            Assert.IsTrue(canMove);
        }

        [TestMethod]
        public void Should_ReturnFalseForCanMoveTo()
        {
            // setup
            var piece = new TestPiece(ChessPosition.D1, ChessColor.White);
            piece.GenerateMoves(null); // boardstate is not considered in test class

            // execute
            bool canMove = NotMovesNotCaptures.All(n => piece.CanMoveTo(n));

            // verify
            Assert.IsFalse(canMove);
        }

        [TestMethod]
        public void Should_ReturnTrueForCanCaptureAt()
        {
            // setup
            var piece = new TestPiece(ChessPosition.D1, ChessColor.White);
            piece.GenerateCaptures(null, null); // boardstate is not considered in test class

            // execute
            bool canCapture = TestCaptures.All(c => piece.CanCaptureAt(c));

            // verify
            Assert.IsTrue(canCapture);
        }

        [TestMethod]
        public void Should_ReturnFalseForCanCaptureAt()
        {
            // setup
            var piece = new TestPiece(ChessPosition.D1, ChessColor.White);
            piece.GenerateCaptures(null, null); // boardstate is not considered in test class

            // execute
            bool canCapture = NotMovesNotCaptures.All(n => piece.CanCaptureAt(n));

            // verify
            Assert.IsFalse(canCapture);
        }

        [TestMethod]
        public void Should_ReturnTrueForIsThreateningAt()
        {
            // setup
            var piece = new TestPiece(ChessPosition.D1, ChessColor.White);
            piece.GenerateThreatened(null, null); // boardstate is not considered in test class

            // execute
            bool isThreatening = TestMoves.All(m => piece.IsThreateningAt(m)) &&
                                 TestCaptures.All(c => piece.IsThreateningAt(c));

            // verify
            Assert.IsTrue(isThreatening);
        }

        [TestMethod]
        public void Should_ReturnFalseForIsThreateningAt()
        {
            // setup
            var piece = new TestPiece(ChessPosition.D1, ChessColor.White);
            piece.GenerateThreatened(null, null); // boardstate is not considered in test class

            // execute
            bool isThreatening = NotMovesNotCaptures.All(n => piece.IsThreateningAt(n));

            // verify
            Assert.IsFalse(isThreatening);
        }

        private class TestPiece : Piece
        {
            public TestPiece(ChessPosition initialPosition, ChessColor color) : base(initialPosition, color) { }

            public override void GenerateMoves(IBoardState boardState)
            {
                TestMoves.ForEach(m => MoveSet.Add(m));
            }

            public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
            {
                TestCaptures.ForEach(c => CaptureSet.Add(c));
            }
        }
    }
}