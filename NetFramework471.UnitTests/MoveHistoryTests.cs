using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model.GameModel;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;
using System.Linq;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class MoveHistoryTests
    {
        [TestMethod]
        public void Should_AddPieceToMoves()
        {
            // setup
            var history = new MoveHistory();
            IPiece piece = new Pawn(ChessPosition.A2, ChessColor.White);
            IMovable move = new Move { EndingPosition = ChessPosition.A4, StartingPosition = piece.Location };

            // execute
            history.Add(piece, move);

            // validate
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(history.Moves.First(), (move, piece.GetType()));
        }

        [TestMethod]
        public void Should_AddMoveToMoveNotationHistory()
        {
            // setup
            var history = new MoveHistory();
            IPiece piece1 = new Pawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = new Move { EndingPosition = ChessPosition.A4, StartingPosition = piece1.Location };

            IPiece piece2 = new Pawn(ChessPosition.A7, ChessColor.Black);
            IMovable move2 = new Move { EndingPosition = ChessPosition.A5, StartingPosition = piece2.Location };

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
            var history = new MoveHistory();
            IPiece piece1 = new Pawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = new Move { EndingPosition = ChessPosition.A4, StartingPosition = piece1.Location };

            IPiece piece2 = new Pawn(ChessPosition.A7, ChessColor.Black);
            IMovable move2 = new Move { EndingPosition = ChessPosition.A5, StartingPosition = piece2.Location };

            // execute
            for (int i = 0; i < 50; i++)
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
            var history = new MoveHistory();
            IPiece piece1 = new Pawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = new Move { EndingPosition = ChessPosition.A4, StartingPosition = piece1.Location };

            IPiece piece2 = new Pawn(ChessPosition.A7, ChessColor.Black);
            IMovable move2 = new Move { EndingPosition = ChessPosition.A5, StartingPosition = piece2.Location };

            // execute
            history.Add(piece1, new Capture { StartingPosition = piece1.Location, EndingPosition = ChessPosition.B3 });

            for (int i = 0; i < 50; i++)
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
            var history = new MoveHistory();
            IPiece piece1 = new Pawn(ChessPosition.A2, ChessColor.White);
            IMovable move1 = new Move { EndingPosition = ChessPosition.A4, StartingPosition = piece1.Location };

            // execute
            history.Add(piece1, move1);

            // verify
            Assert.IsTrue(history.WasPawnMovedInLastFiftyMoves);
        }

        [TestMethod]
        public void Should_ReturnFalseForPawnMovedInLastFiftyMoves()
        {
            // setup
            var history = new MoveHistory();
            IPiece piece1 = new Rook(ChessPosition.A2, ChessColor.White);
            IMovable move1 = new Move { EndingPosition = ChessPosition.A4, StartingPosition = piece1.Location };

            // verify empty list
            Assert.IsFalse(history.WasPawnMovedInLastFiftyMoves);


            // execute
            history.Add(piece1, move1);

            // verify
            Assert.IsFalse(history.WasPawnMovedInLastFiftyMoves);
        }
    }
}
