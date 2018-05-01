using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class ChessPieceMoverTest
    {
        private readonly IChessPieceMover _mover = ModelLocator.ChessPieceMover;

        [TestMethod]
        public void Should_GenerateOneMoveNorth()
        {
            // execute
            ChessPosition position = _mover.North(ChessPosition.A1);

            // verify
            Assert.AreEqual(ChessPosition.A2, position);
        }

        [TestMethod]
        public void Should_GenerateOneMoveSouth()
        {
            // execute
            ChessPosition position = _mover.South(ChessPosition.A2);

            // verify
            Assert.AreEqual(ChessPosition.A1, position);
        }

        [TestMethod]
        public void Should_GenerateOneMoveEast()
        {
            // execute
            ChessPosition position = _mover.East(ChessPosition.B1);

            // verify
            Assert.AreEqual(ChessPosition.C1, position);
        }

        [TestMethod]
        public void Should_GenerateOneMoveWest()
        {
            // execute
            ChessPosition position = _mover.West(ChessPosition.B1);

            // verify
            Assert.AreEqual(ChessPosition.A1, position);
        }

        [TestMethod]
        public void Should_GenerateOneMoveNorthWest()
        {
            // execute
            ChessPosition position = _mover.NorthWest(ChessPosition.B1);

            // verify
            Assert.AreEqual(ChessPosition.A2, position);
        }

        [TestMethod]
        public void Should_GenerateOneMoveSouthWest()
        {
            // execute
            ChessPosition position = _mover.SouthWest(ChessPosition.B2);

            // verify
            Assert.AreEqual(ChessPosition.A1, position);
        }

        [TestMethod]
        public void Should_GenerateOneMoveNorthEast()
        {
            // execute
            ChessPosition position = _mover.NorthEast(ChessPosition.B1);

            // verify
            Assert.AreEqual(ChessPosition.C2, position);
        }

        [TestMethod]
        public void Should_GenerateOneMoveSouthEast()
        {
            // execute
            ChessPosition position = _mover.SouthEast(ChessPosition.B2);

            // verify
            Assert.AreEqual(ChessPosition.C1, position);
        }
    }
}
