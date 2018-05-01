using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model;
using StandardChess.Model.GameModel;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class MoveHistoryTests
    {
        private readonly IChessPieceFactory _chessPieceFactory = ModelLocator.ChessPieceFactory;

        [TestMethod]
        public void Should_AddPieceToMoves()
        {
            // setup
            IMoveHistory history = ModelLocator.MoveHistory;
            IPiece piece = _chessPieceFactory.CreatePawn(ChessPosition.A2, ChessColor.White);
            IMovable move = ModelLocator.Move;
            move.StartingPosition = piece.Location;
            move.EndingPosition = ChessPosition.A4;

            // execute
            history.Add(piece, move);

            // validate
            IPiece p = history.Moves.First().piece;
            IMovable m = history.Moves.First().movable;
            Assert.AreEqual(1, history.Count);
            Assert.IsTrue(p is IPawn);
            Assert.IsTrue(m.StartingPosition == move.StartingPosition);
            Assert.IsTrue(m.EndingPosition == move.EndingPosition);
        }

        [TestMethod]
        public void Should_AddMoveToMoveNotationHistory()
        {
            // setup
            var history = ModelLocator.MoveHistory;
            IPiece piece1 = _chessPieceFactory.CreatePawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = ModelLocator.Move;
            move1.StartingPosition = piece1.Location;
            move1.EndingPosition = ChessPosition.A4;

            IPiece piece2 = _chessPieceFactory.CreatePawn(ChessPosition.A7, ChessColor.Black);
            IMovable move2 = ModelLocator.Move;
            move2.StartingPosition = piece1.Location;
            move2.EndingPosition = ChessPosition.A5;

            // execute
            history.Add(piece1, move1);
            var notation1 = new string(history.MovesByNotation.First().ToCharArray());

            history.Add(piece2, move2);
            var notation2 = new string(history.MovesByNotation.First().ToCharArray());

            //validate
            Assert.AreNotEqual(notation1, notation2);
            Assert.IsTrue(notation2.StartsWith(notation1));
        }

        [TestMethod]
        public void Should_ReturnTrueForCaptureInLastFifty()
        {
            // setup
            var history = ModelLocator.MoveHistory;
            IPiece piece1 = _chessPieceFactory.CreatePawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = ModelLocator.Move;
            move1.StartingPosition = piece1.Location;
            move1.EndingPosition = ChessPosition.A4;

            IPiece piece2 = _chessPieceFactory.CreatePawn(ChessPosition.A7, ChessColor.Black);
            IMovable move2 = ModelLocator.Move;
            move2.StartingPosition = piece1.Location;
            move2.EndingPosition = ChessPosition.A5;

            // execute
            for (var i = 0; i < 50; i++)
            {
                history.Add(piece1, move1);
                history.Add(piece2, move2);
            }

            history.Add(piece1, new Capture { StartingPosition = piece1.Location, EndingPosition = ChessPosition.B3 });

            // validate
            Assert.IsTrue(history.WasPieceCapturedInLastFiftyMoves);
        }

        [TestMethod]
        public void Should_ReturnFalseForCaptureInLastFifty()
        {
            // setup
            var history = ModelLocator.MoveHistory;
            IPiece piece1 = _chessPieceFactory.CreatePawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = ModelLocator.Move;
            move1.StartingPosition = piece1.Location;
            move1.EndingPosition = ChessPosition.A4;

            IPiece piece2 = _chessPieceFactory.CreatePawn(ChessPosition.A7, ChessColor.Black);
            IMovable move2 = ModelLocator.Move;
            move2.StartingPosition = piece1.Location;
            move2.EndingPosition = ChessPosition.A5;
            // execute
            history.Add(piece1, new Capture { StartingPosition = piece1.Location, EndingPosition = ChessPosition.B3 });

            for (var i = 0; i < 50; i++)
            {
                history.Add(piece1, move1);
                history.Add(piece2, move2);
            }

            // validate
            Assert.IsFalse(history.WasPieceCapturedInLastFiftyMoves);
        }

        [TestMethod]
        public void Should_ReturnTrueForPawnMovedInLastFiftyMoves()
        {
            // setup
            var history = ModelLocator.MoveHistory;
            IPiece piece1 = _chessPieceFactory.CreatePawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = ModelLocator.Move;
            move1.StartingPosition = piece1.Location;
            move1.EndingPosition = ChessPosition.A4;

            // execute
            history.Add(piece1, move1);

            // verify
            Assert.IsTrue(history.WasPawnMovedInLastFiftyMoves);
        }

        [TestMethod]
        public void Should_ReturnFalseForPawnMovedInLastFiftyMoves()
        {
            // setup
            var history = ModelLocator.MoveHistory;
            IPiece piece1 = _chessPieceFactory.CreateRook(ChessPosition.A2, ChessColor.White);
            IMovable move1 = ModelLocator.Move;
            move1.StartingPosition = piece1.Location;
            move1.EndingPosition = ChessPosition.A4;

            // verify empty list
            Assert.IsFalse(history.WasPawnMovedInLastFiftyMoves);


            // execute
            history.Add(piece1, move1);

            // verify
            Assert.IsFalse(history.WasPawnMovedInLastFiftyMoves);
        }
    }
}