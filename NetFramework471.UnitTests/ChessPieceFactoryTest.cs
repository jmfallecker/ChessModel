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
