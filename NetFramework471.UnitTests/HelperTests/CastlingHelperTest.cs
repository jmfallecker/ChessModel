using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model;

namespace NetFramework471.UnitTests.HelperTests
{
    [TestClass]
    public class CastlingHelperTest
    {
        [TestMethod]
        public void Should_ReturnNone_For_CastlingRookPosition_When_NotCastling()
        {
            IMove move = ModelLocator.Move;
            move.StartingPosition = ChessPosition.E1;
            move.EndingPosition = ChessPosition.E2;

            Assert.AreEqual(ChessPosition.None, ModelLocator.CastlingHelper.GetCastlingRookPosition(move));
        }

        [TestMethod]
        public void Should_Return_RookStartingPosition_For_WhiteQueenSideCastle()
        {
            IMove move = ModelLocator.Move;
            move.StartingPosition = ChessPosition.E1;
            move.EndingPosition = ChessPosition.C1;

            Assert.AreEqual(ChessPosition.A1, ModelLocator.CastlingHelper.GetCastlingRookPosition(move));
        }

        [TestMethod]
        public void Should_Return_RookStartingPosition_For_WhiteKingSideCastle()
        {
            IMove move = ModelLocator.Move;
            move.StartingPosition = ChessPosition.E1;
            move.EndingPosition = ChessPosition.G1;

            Assert.AreEqual(ChessPosition.H1, ModelLocator.CastlingHelper.GetCastlingRookPosition(move));
        }

        [TestMethod]
        public void Should_Return_RookStartingPosition_For_BlackQueenSideCastle()
        {
            IMove move = ModelLocator.Move;
            move.StartingPosition = ChessPosition.E8;
            move.EndingPosition = ChessPosition.C8;

            Assert.AreEqual(ChessPosition.A8, ModelLocator.CastlingHelper.GetCastlingRookPosition(move));
        }

        [TestMethod]
        public void Should_Return_RookStartingPosition_For_BlackKingSideCastle()
        {
            IMove move = ModelLocator.Move;
            move.StartingPosition = ChessPosition.E8;
            move.EndingPosition = ChessPosition.G8;

            Assert.AreEqual(ChessPosition.H8, ModelLocator.CastlingHelper.GetCastlingRookPosition(move));
        }

        [TestMethod]
        public void Should_Return_GivenRookLocation_For_InvalidRookLocation()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E1, ChessColor.White) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.A2, ChessColor.White) as IRook;

            ChessPosition actualEndingPosition = ModelLocator.CastlingHelper.GetEndingPositionForCastlingRook(king, rook);

            Assert.AreEqual(rook.Location, actualEndingPosition);
        }

        [TestMethod]
        public void Should_Return_GivenRookLocation_For_InvalidKingLocation()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E2, ChessColor.White) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.A1, ChessColor.White) as IRook;

            ChessPosition actualEndingPosition = ModelLocator.CastlingHelper.GetEndingPositionForCastlingRook(king, rook);

            Assert.AreEqual(rook.Location, actualEndingPosition);
        }

        [TestMethod]
        public void Should_Return_RookEndingPosition_For_WhiteQueenSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E1, ChessColor.White) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.A1, ChessColor.White) as IRook;

            ChessPosition actualEndingPosition = ModelLocator.CastlingHelper.GetEndingPositionForCastlingRook(king, rook);

            Assert.AreEqual(ChessPosition.D1, actualEndingPosition);
        }

        [TestMethod]
        public void Should_Return_RookEndingPosition_For_WhiteKingSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E1, ChessColor.White) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.H1, ChessColor.White) as IRook;

            ChessPosition actualEndingPosition = ModelLocator.CastlingHelper.GetEndingPositionForCastlingRook(king, rook);

            Assert.AreEqual(ChessPosition.F1, actualEndingPosition);
        }

        [TestMethod]
        public void Should_Return_RookEndingPosition_For_BlackQueenSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E8, ChessColor.Black) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.A8, ChessColor.Black) as IRook;

            ChessPosition actualEndingPosition = ModelLocator.CastlingHelper.GetEndingPositionForCastlingRook(king, rook);

            Assert.AreEqual(ChessPosition.D8, actualEndingPosition);
        }

        [TestMethod]
        public void Should_Return_RookEndingPosition_For_BlackKingSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E8, ChessColor.Black) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.H8, ChessColor.Black) as IRook;

            ChessPosition actualEndingPosition = ModelLocator.CastlingHelper.GetEndingPositionForCastlingRook(king, rook);

            Assert.AreEqual(ChessPosition.F8, actualEndingPosition);
        }

        [TestMethod]
        public void Should_Return_NoCastleMoves_WhenKingHasMoved()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E1, ChessColor.White) as IKing;

            Assert.IsNotNull(king);

            king.HasMoved = true;

            var castleMovesForKing = ModelLocator.CastlingHelper.GetCastleMovesForKing(king);

            Assert.IsFalse(castleMovesForKing.Any());
        }

        [TestMethod]
        public void Should_Return_CorrectCastleMoves_For_BlackKing()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E8, ChessColor.Black) as IKing;

            Assert.IsNotNull(king);
            
            var castleMovesForKing = ModelLocator.CastlingHelper.GetCastleMovesForKing(king);

            IEnumerable<ChessPosition> movesForKing = castleMovesForKing as ChessPosition[] ?? castleMovesForKing.ToArray();

            Assert.IsTrue(movesForKing.Contains(ChessPosition.G8));
            Assert.IsTrue(movesForKing.Contains(ChessPosition.C8));
        }

        [TestMethod]
        public void Should_Return_CorrectCastleMoves_For_WhiteKing()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E1, ChessColor.White) as IKing;

            Assert.IsNotNull(king);

            var castleMovesForKing = ModelLocator.CastlingHelper.GetCastleMovesForKing(king);

            IEnumerable<ChessPosition> movesForKing = castleMovesForKing as ChessPosition[] ?? castleMovesForKing.ToArray();

            Assert.IsTrue(movesForKing.Contains(ChessPosition.G1));
            Assert.IsTrue(movesForKing.Contains(ChessPosition.C1));
        }

        [TestMethod]
        public void Should_GetCorrectPositions_Between_WhiteQueenSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E1, ChessColor.White) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.A1, ChessColor.White) as IRook;

            List<ChessPosition> locations = ModelLocator.CastlingHelper.GetPositionsBetweenCastle(king, rook);

            Assert.IsTrue(locations.Contains(ChessPosition.D1));
            Assert.IsTrue(locations.Contains(ChessPosition.C1));
            Assert.IsTrue(locations.Contains(ChessPosition.B1));
        }

        [TestMethod]
        public void Should_GetCorrectPositions_Between_WhiteKingSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E1, ChessColor.White) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.H1, ChessColor.White) as IRook;

            List<ChessPosition> locations = ModelLocator.CastlingHelper.GetPositionsBetweenCastle(king, rook);

            Assert.IsTrue(locations.Contains(ChessPosition.F1));
            Assert.IsTrue(locations.Contains(ChessPosition.G1));
        }

        [TestMethod]
        public void Should_GetCorrectPositions_Between_BlackQueenSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E8, ChessColor.Black) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.A8, ChessColor.Black) as IRook;

            List<ChessPosition> locations = ModelLocator.CastlingHelper.GetPositionsBetweenCastle(king, rook);

            Assert.IsTrue(locations.Contains(ChessPosition.D8));
            Assert.IsTrue(locations.Contains(ChessPosition.C8));
            Assert.IsTrue(locations.Contains(ChessPosition.B8));
        }

        [TestMethod]
        public void Should_GetCorrectPositions_Between_BlackKingSideCastle()
        {
            var king = ModelLocator.ChessPieceFactory.CreateKing(ChessPosition.E8, ChessColor.Black) as IKing;
            var rook = ModelLocator.ChessPieceFactory.CreateRook(ChessPosition.H8, ChessColor.Black) as IRook;

            List<ChessPosition> locations = ModelLocator.CastlingHelper.GetPositionsBetweenCastle(king, rook);

            Assert.IsTrue(locations.Contains(ChessPosition.F8));
            Assert.IsTrue(locations.Contains(ChessPosition.G8));
        }
    }
}
