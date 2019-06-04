using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model;
using StandardChess.Model.Exceptions;

namespace NetFramework471.UnitTests.HelperTests
{
    [TestClass]
    public class PieceCreationHelperTest
    {
        [TestMethod]
        public void Should_ThrowException_When_PieceTypeIsNotValid()
        {
            var helper = ModelLocator.PieceCreationHelper;

            Assert.ThrowsException<PawnPromotionException>(() => helper.PromotePawn(typeof(object), ChessPosition.A1, ChessColor.Black));
            Assert.ThrowsException<PawnPromotionException>(() => helper.PromotePawn(typeof(IPawn), ChessPosition.A1, ChessColor.Black));
            Assert.ThrowsException<PawnPromotionException>(() => helper.PromotePawn(typeof(IKing), ChessPosition.A1, ChessColor.Black));
        }

        [TestMethod]
        public void Should_Return_CorrectPieceAtCorrectLocation()
        {
            var helper = ModelLocator.PieceCreationHelper;

            IPiece queen = helper.PromotePawn(typeof(IQueen), ChessPosition.A1, ChessColor.Black);
            IPiece rook = helper.PromotePawn(typeof(IRook), ChessPosition.A2, ChessColor.White);
            IPiece knight = helper.PromotePawn(typeof(IKnight), ChessPosition.A3, ChessColor.Black);
            IPiece bishop = helper.PromotePawn(typeof(IBishop), ChessPosition.A4, ChessColor.White);

            Assert.IsInstanceOfType(queen, typeof(IQueen));
            Assert.IsInstanceOfType(rook, typeof(IRook));
            Assert.IsInstanceOfType(knight, typeof(IKnight));
            Assert.IsInstanceOfType(bishop, typeof(IBishop));
        }

        [TestMethod]
        public void Should_CreateProper_WhiteStartingPieces()
        {
            List<IPiece> whiteStartPieces = ModelLocator.PieceCreationHelper.CreateStartingPieces(ChessColor.White);
            
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.A2), typeof(IPawn));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.B2), typeof(IPawn));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.C2), typeof(IPawn));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.D2), typeof(IPawn));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.E2), typeof(IPawn));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.F2), typeof(IPawn));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.G2), typeof(IPawn));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.H2), typeof(IPawn));

            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.A1), typeof(IRook));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.B1), typeof(IKnight));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.C1), typeof(IBishop));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.D1), typeof(IQueen));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.E1), typeof(IKing));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.F1), typeof(IBishop));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.G1), typeof(IKnight));
            Assert.IsInstanceOfType(whiteStartPieces.Find(p => p.Location == ChessPosition.H1), typeof(IRook));
        }

        [TestMethod]
        public void Should_CreateProper_BlackStartingPieces()
        {
            List<IPiece> blackStartPieces = ModelLocator.PieceCreationHelper.CreateStartingPieces(ChessColor.Black);

            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.A7), typeof(IPawn));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.B7), typeof(IPawn));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.C7), typeof(IPawn));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.D7), typeof(IPawn));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.E7), typeof(IPawn));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.F7), typeof(IPawn));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.G7), typeof(IPawn));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.H7), typeof(IPawn));

            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.A8), typeof(IRook));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.B8), typeof(IKnight));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.C8), typeof(IBishop));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.D8), typeof(IQueen));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.E8), typeof(IKing));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.F8), typeof(IBishop));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.G8), typeof(IKnight));
            Assert.IsInstanceOfType(blackStartPieces.Find(p => p.Location == ChessPosition.H8), typeof(IRook));
        }
    }
}
