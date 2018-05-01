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
