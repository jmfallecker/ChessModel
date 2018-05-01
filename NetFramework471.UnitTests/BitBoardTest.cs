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
            IBitboard bitboard = ModelLocator.Bitboard;

            // verify
            Assert.AreEqual(ChessPosition.None, bitboard.State);
        }

        [TestMethod]
        public void Should_ReturnTrueAfterAddingAPiece()
        {
            // setup
            IBitboard bitboard = ModelLocator.Bitboard;
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
            IBitboard bitboard = ModelLocator.Bitboard;
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
            IBitboard bitboard = ModelLocator.Bitboard;
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
            IBitboard bitboard = ModelLocator.Bitboard;
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
