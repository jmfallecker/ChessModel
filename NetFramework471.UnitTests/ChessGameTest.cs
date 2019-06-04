using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model.Exceptions;
using StandardChess.Model.GameModel;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Should_ReturnZero_For_TurnAfterGameCreation()
        {
            var game = new ChessGame(() => typeof(IQueen));

            Assert.AreEqual(0, game.Turn);
        }

        [TestMethod]
        public void Should_CreateBoard_When_DefaultConstructorUsed()
        {
            var game = new ChessGame(() => typeof(IQueen));

            Assert.IsNotNull(game.GameBoard);
        }

        [TestMethod]
        public void Should_CreateWhitePlayer_When_DefaultConstuctorUsed()
        {
            var game = new ChessGame(() => typeof(IQueen));

            Assert.IsNotNull(game.WhitePlayer);
        }

        [TestMethod]
        public void Should_CreateBlackPlayer_When_DefaultConstuctorUsed()
        {
            var game = new ChessGame(() => typeof(IQueen));

            Assert.IsNotNull(game.BlackPlayer);
        }

        [TestMethod]
        public void Should_ReturnTrue_When_LegalMoveIsPerformed()
        {
            var game = new ChessGame(() => typeof(IQueen));

            var move = new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A4
            };

            var wasMoveSuccessful = game.MovePiece(move);

            Assert.IsTrue(wasMoveSuccessful);
        }

        [TestMethod]
        public void Should_IncrementTurn_When_LegalMoveIsPerformed()
        {
            var game = new ChessGame(() => typeof(IQueen));

            var move = new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A4
            };

            game.MovePiece(move);

            Assert.AreEqual(0.5, game.Turn);
        }

        [TestMethod]
        public void Should_ReturnTrue_WhenExecutingSeriesOfLegalMoves()
        {
            var game = new ChessGame(() => typeof(IQueen));

            var move = new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            };

            Assert.IsTrue(game.MovePiece(move));

            var move2 = new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            };

            Assert.IsTrue(game.MovePiece(move2));
        }

        [TestMethod]
        public void Should_ReturnFalse_When_ExecutingIllegalMove()
        {
            var game = new ChessGame(() => typeof(IQueen));

            var move = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.E2
            };

            Assert.IsFalse(game.MovePiece(move));
        }

        [TestMethod]
        public void Should_ReturnTrue_For_LegalCapture()
        {
            var game = new ChessGame(() => typeof(IQueen));

            var setupMove = new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            };

            var setupMove2 = new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            };

            game.MovePiece(setupMove);
            game.MovePiece(setupMove2);

            var capture = new Capture
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.D5
            };

            Assert.IsTrue(game.CapturePiece(capture));
        }

        [TestMethod]
        public void Should_UpdateScoreCorrectly_After_LegalCapture()
        {
            var game = new ChessGame(() => typeof(IQueen));

            var setupMove = new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            };

            var setupMove2 = new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            };

            game.MovePiece(setupMove);
            game.MovePiece(setupMove2);

            var capture = new Capture
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.D5
            };

            game.CapturePiece(capture);

            Assert.AreEqual(1, game.WhitePlayerScore);
        }

        [TestMethod]
        public void Should_ReturnFalse_For_IllegalCapture()
        {
            var game = new ChessGame(() => typeof(IQueen));

            var capture = new Capture
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.B3
            };

            Assert.IsFalse(game.CapturePiece(capture));
        }

        [TestMethod]
        public void Should_ReturnFalse_For_MoveLeavingKingInCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));

            Move move1 = new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            };
            Move move2 = new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            };
            Move move3 = new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E5
            };
            Move move4 = new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.H4
            };

            game.MovePiece(move1);
            game.MovePiece(move2);
            game.MovePiece(move3);
            game.MovePiece(move4);

            Move illegalMove = new Move
            {
                StartingPosition = ChessPosition.F2,
                EndingPosition = ChessPosition.F3
            };

            Assert.IsFalse(game.MovePiece(illegalMove));
        }

        [TestMethod]
        public void Should_ReturnFalse_For_CaptureLeavingKingInCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));

            // white
            Move move1 = new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A4
            };
            game.MovePiece(move1);
            // black
            Move move2 = new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E5
            };
            game.MovePiece(move2);
            // white
            Move move3 = new Move
            {
                StartingPosition = ChessPosition.A4,
                EndingPosition = ChessPosition.A5
            };
            game.MovePiece(move3);
            // black
            Move move4 = new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.H4
            };
            game.MovePiece(move4);
            // white
            Move move5 = new Move
            {
                StartingPosition = ChessPosition.A5,
                EndingPosition = ChessPosition.A6
            };
            game.MovePiece(move5);
            // black
            Move move6 = new Move
            {
                StartingPosition = ChessPosition.E5,
                EndingPosition = ChessPosition.E4
            };
            game.MovePiece(move6);
            //white
            Capture capture7 = new Capture
            {
                StartingPosition = ChessPosition.A6,
                EndingPosition = ChessPosition.B7
            };
            game.CapturePiece(capture7);
            // black
            Move move8 = new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E3
            };
            game.MovePiece(move8);

            Capture illegalCapture = new Capture
            {
                StartingPosition = ChessPosition.F2,
                EndingPosition = ChessPosition.E3
            };

            Assert.IsFalse(game.CapturePiece(illegalCapture));
        }

        [TestMethod]
        public void Should_ReturnFalse_ForKingMovingIntoCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // move white king-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E5
            });
            // move white king up one space
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.E2
            });
            // move black queen to F
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.F6
            });

            Assert.IsTrue(isSetupSuccessful);

            Move kingMovingIntoCheck = new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.F3
            };

            Assert.IsFalse(game.MovePiece(kingMovingIntoCheck));
        }

        [TestMethod]
        public void Should_RetainMoveHistory_When_MovesAndCapturesAreMade()
        {
            var game = new ChessGame(() => typeof(IQueen));

            Move move1 = new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            };
            Move move2 = new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            };
            Capture capture = new Capture
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.D5
            };

            game.MovePiece(move1);
            game.MovePiece(move2);
            game.CapturePiece(capture);

            bool isMoveHistoryCorrect = game.MoveHistory.Moves[0].movable.Equals(move1) &&
                                        game.MoveHistory.Moves[1].movable.Equals(move2) &&
                                        game.MoveHistory.Moves[2].movable.Equals(capture);

            Assert.IsTrue(isMoveHistoryCorrect);
        }

        //[TestMethod]
        //public void Should_RemoveLastMove_When_UndoLastMoveIsCalled()
        //{
        //    var game = new ChessGame(() => typeof(IQueen));

        //    Move move1 = new Move
        //    {
        //        StartingPosition = ChessPosition.E2,
        //        EndingPosition = ChessPosition.E4
        //    };
        //    Move move2 = new Move
        //    {
        //        StartingPosition = ChessPosition.D7,
        //        EndingPosition = ChessPosition.D5
        //    };
        //    Capture move3 = new Capture
        //    {
        //        StartingPosition = ChessPosition.E4,
        //        EndingPosition = ChessPosition.D5
        //    };

        //    game.MovePiece(move1);
        //    game.MovePiece(move2);
        //    game.MovePiece(move3);

        //    game.UndoLastMoveHistory();

        //    bool isMoveHistoryCorrect = game.MoveHistory.Count.Equals(2);

        //    Assert.IsTrue(isMoveHistoryCorrect);
        //}

        //[TestMethod]
        //public void Should_NotThrow_When_TryingToRemoveLastMoveFromMoveHistory()
        //{
        //    var game = new ChessGame(() => typeof(IQueen));
        //    game.UndoLastMoveHistory();
        //}

        [TestMethod]
        public void Should_AllowCastle_When_NoPiecesBetweenRookAndKing()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black A-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move black B-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B6
            });
            // move white king-side knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.F3
            });
            // move black C-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C7,
                EndingPosition = ChessPosition.C6
            });
            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsTrue(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_NotAllowCastle_When_PiecesAreBetweenRookAndKing()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black A-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move black B-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B6
            });
            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsFalse(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_NotAllowCastle_When_RookHasMoved()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black A-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move black B-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B6
            });
            // move white king-side knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.F3
            });
            // move black C-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C7,
                EndingPosition = ChessPosition.C6
            });
            // move white H-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H2,
                EndingPosition = ChessPosition.H3
            });
            // move black D-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D6
            });
            // move white rook
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H1,
                EndingPosition = ChessPosition.H2
            });
            // move black E-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });
            // move white rook back
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H2,
                EndingPosition = ChessPosition.H1
            });
            // move black F-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });

            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsFalse(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_NotAllowCastle_When_KingHasMoved()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black A-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move black B-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B6
            });
            // move white king-side knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.F3
            });
            // move black C-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C7,
                EndingPosition = ChessPosition.C6
            });
            // move white King
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.E2
            });
            // move black D-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D6
            });
            // move white king back
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E1
            });
            // move black F-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });

            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsFalse(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_NotAllowCastle_When_RookIsNotPresent()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            Assert.IsTrue(isSetupSuccessful);
            // move black A-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A6
            });
            Assert.IsTrue(isSetupSuccessful);
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.C4
            });
            Assert.IsTrue(isSetupSuccessful);
            // move black B-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B6
            });
            Assert.IsTrue(isSetupSuccessful);
            // move white king-side knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.F3
            });
            Assert.IsTrue(isSetupSuccessful);
            // move black C-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C7,
                EndingPosition = ChessPosition.C6
            });
            Assert.IsTrue(isSetupSuccessful);
            // move white H-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H2,
                EndingPosition = ChessPosition.H3
            });
            Assert.IsTrue(isSetupSuccessful);
            // move black D-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D6
            });
            Assert.IsTrue(isSetupSuccessful);
            // move white rook
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H1,
                EndingPosition = ChessPosition.H2
            });
            // move black E-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });

            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsFalse(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_NotAllowCastle_When_PositionsAreThreatenedBetweenRookAndKing()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black E-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move black queen out
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.G5
            });
            // move white knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.H3
            });
            // move black queen to threaten square F1
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.G5,
                EndingPosition = ChessPosition.G2
            });

            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsFalse(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_NotAllowCastle_When_KingWillEndUpInCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black E-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move black queen out
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.H4
            });
            // move white knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.F3
            });
            // move black queen to threaten square G1
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.H4,
                EndingPosition = ChessPosition.H2
            });

            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsFalse(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_NotAllowCastle_When_KingIsInCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black D-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.C4
            });
            // move black queen out
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.D7
            });
            // move white knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.H3
            });
            // move black queen to threaten square F1
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.E6
            });
            // move white pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E3,
                EndingPosition = ChessPosition.E4
            });
            // capture white E-pawn with black queen
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.E6,
                EndingPosition = ChessPosition.E4
            });

            Assert.IsTrue(isSetupSuccessful);

            // castle king side
            Move castle = new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.G1
            };

            Assert.IsFalse(game.MovePiece(castle));
        }

        [TestMethod]
        public void Should_UpdateBoardProperly_When_CastlingWhiteShortSide()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black A-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A6
            });
            // move white king-side bishop out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move black B-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B6
            });
            // move white king-side knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G1,
                EndingPosition = ChessPosition.F3
            });
            // move black C-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C7,
                EndingPosition = ChessPosition.C6
            });
            Assert.IsTrue(isSetupSuccessful);

            ChessPosition endingPosition = ChessPosition.G1;
            // castle king side
            game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = endingPosition
            });

            Assert.IsTrue(game.WhitePieces.Find(p => p is IKing).Location == endingPosition);
            Assert.IsTrue(game.WhitePieces.Find(p => p is IRook && p.Location == ChessPosition.F1) != null);
        }

        [TestMethod]
        public void Should_UpdateBoardProperly_When_CastlingWhiteLongSide()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            //move white king pawn to allow bishop to leave
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black A-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A6
            });
            // move white queen out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.E2
            });
            // move black B-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B6
            });
            // move white D-Pawn for Bishop
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D2,
                EndingPosition = ChessPosition.D4
            });
            // move black C-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C7,
                EndingPosition = ChessPosition.C6
            });
            // move white queen-side bishop
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C1,
                EndingPosition = ChessPosition.D2
            });
            // move black D-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D6
            });
            // move white queen-side knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B1,
                EndingPosition = ChessPosition.A3
            });
            // move black E-pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });

            Assert.IsTrue(isSetupSuccessful);

            ChessPosition endingPosition = ChessPosition.C1;
            // castle queen side
            game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = endingPosition
            });

            Assert.IsTrue(game.WhitePieces.Find(p => p is IKing).Location == endingPosition);
            Assert.IsTrue(game.WhitePieces.Find(p => p is IRook && p.Location == ChessPosition.D1) != null);
        }

        [TestMethod]
        public void Should_UpdateBoardProperly_When_CastlingBlackShortSide()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;
            // move white A-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A3
            });
            // move black king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });
            // move white B-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B2,
                EndingPosition = ChessPosition.B3
            });
            // move black king-side bishop
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F8,
                EndingPosition = ChessPosition.E7
            });
            // move white C-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C2,
                EndingPosition = ChessPosition.C3
            });
            // move black knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G8,
                EndingPosition = ChessPosition.H6
            });
            // move white D-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D2,
                EndingPosition = ChessPosition.D3
            });

            Assert.IsTrue(isSetupSuccessful);

            ChessPosition endingPosition = ChessPosition.G8;
            // castle black king-side
            game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E8,
                EndingPosition = endingPosition
            });

            Assert.IsTrue(game.BlackPieces.Find(p => p is IKing).Location == endingPosition);
            Assert.IsTrue(game.BlackPieces.Find(p => p is IRook && p.Location == ChessPosition.F8) != null);
        }

        [TestMethod]
        public void Should_UpdateBoardProperly_When_CastlingBlackLongSide()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;
            // move white A-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A3
            });
            // move black queen pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D6
            });
            // move white B-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B2,
                EndingPosition = ChessPosition.B3
            });
            // move black queen-side bishop
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C8,
                EndingPosition = ChessPosition.E6
            });
            // move white C-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C2,
                EndingPosition = ChessPosition.C3
            });
            // move black knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B8,
                EndingPosition = ChessPosition.A6
            });
            // move white D-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D2,
                EndingPosition = ChessPosition.D3
            });
            // move black queen
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.D7
            });
            // move white E-Pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });

            Assert.IsTrue(isSetupSuccessful);

            ChessPosition endingPosition = ChessPosition.C8;
            // castle black king-side
            game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E8,
                EndingPosition = endingPosition
            });

            Assert.IsTrue(game.BlackPieces.Find(p => p is IKing).Location == endingPosition);
            Assert.IsTrue(game.BlackPieces.Find(p => p is IRook && p.Location == ChessPosition.D8) != null);
        }

        [TestMethod]
        public void Should_ReturnTrue_When_AttemtingCaptureViaEnPassantLeft()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSuccessful = true;
            // move white E-pawn two
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black H-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H7,
                EndingPosition = ChessPosition.H6
            });
            // move white E-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E5
            });
            // move black D-pawn 2 (setup for en passant)
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            });

            Assert.IsTrue(isSuccessful);

            Capture enPassant = new Capture
            {
                StartingPosition = ChessPosition.E5,
                EndingPosition = ChessPosition.D6
            };

            Assert.IsTrue(game.CapturePiece(enPassant));
        }

        [TestMethod]
        public void Should_ReturnTrue_When_AttemtingCaptureViaEnPassantRight()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSuccessful = true;
            // move white E-pawn two
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black H-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H7,
                EndingPosition = ChessPosition.H6
            });
            // move white E-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E5
            });
            // move black D-pawn 2 (setup for en passant)
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F5
            });

            Assert.IsTrue(isSuccessful);

            Capture enPassant = new Capture
            {
                StartingPosition = ChessPosition.E5,
                EndingPosition = ChessPosition.F6
            };

            Assert.IsTrue(game.CapturePiece(enPassant));
        }

        [TestMethod]
        public void Should_UpdateBoardProperly_When_EnPassantAsWhite()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSuccessful = true;
            // move white E-pawn two
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black H-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H7,
                EndingPosition = ChessPosition.H6
            });
            // move white E-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E5
            });
            // move black D-pawn 2 (setup for en passant)
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            });

            Assert.IsTrue(isSuccessful);

            Capture enPassant = new Capture
            {
                StartingPosition = ChessPosition.E5,
                EndingPosition = ChessPosition.D6
            };

            IPiece lostPawn = game.BlackPieces.Find(p => p.Location == ChessPosition.D5);

            game.CapturePiece(enPassant);

            Assert.AreEqual(lostPawn.Location, ChessPosition.None);
        }

        [TestMethod]
        public void Should_UpdateBoardProperly_When_EnPassantAsBlack()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSuccessful = true;
            // move white A-pawn two
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A3
            });
            // move black H-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            });
            // move white A-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A3,
                EndingPosition = ChessPosition.A4
            });
            // move black D-pawn 2 
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D5,
                EndingPosition = ChessPosition.D4
            });
            // move white E-pawn 2 (setup for en passant)
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });

            Assert.IsTrue(isSuccessful);

            Capture enPassant = new Capture
            {
                StartingPosition = ChessPosition.D4,
                EndingPosition = ChessPosition.E3
            };

            IPiece lostPawn = game.WhitePieces.Find(p => p.Location == ChessPosition.E4);

            game.CapturePiece(enPassant);

            Assert.AreEqual(lostPawn.Location, ChessPosition.None);
            Assert.IsTrue(game.BlackPieces.Find(p => p.Location == ChessPosition.E3) is Pawn);
        }

        [TestMethod]
        public void Should_UpdatePlayerPoints_When_EnPassantIsDone()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSuccessful = true;
            // move white E-pawn two
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black H-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H7,
                EndingPosition = ChessPosition.H6
            });
            // move white E-pawn 1
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E5
            });
            // move black D-pawn 2 (setup for en passant)
            isSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            });

            Assert.IsTrue(isSuccessful);

            Capture enPassant = new Capture
            {
                StartingPosition = ChessPosition.E5,
                EndingPosition = ChessPosition.D6
            };

            game.CapturePiece(enPassant);

            Assert.AreEqual(game.WhitePlayerScore, 1);
        }

        [TestMethod]
        public void Should_SetGameStateToOngoing_Before_AnyMovesAreMade()
        {
            var game = new ChessGame(() => typeof(IQueen));

            Assert.AreEqual(game.State, GameState.Ongoing);
        }

        [TestMethod]
        public void Should_SetGameStateToOngoing_After_WhiteKingMovesOutOfCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;

            // push white king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black knight
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B8,
                EndingPosition = ChessPosition.C6
            });
            // push white queen pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E5
            });
            // move black knight
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.C6,
                EndingPosition = ChessPosition.E5
            });
            // move white queen
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.E2
            });
            // move black knight for check
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E5,
                EndingPosition = ChessPosition.F3
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.WhiteInCheck);

            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.D1
            });

            Assert.IsTrue(isSetupSuccessful);

            Assert.AreEqual(game.State, GameState.Ongoing);
        }

        [TestMethod]
        public void Should_SetGameStateToOngoing_After_BlackKingMovesOutOfCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessful = true;
            // push king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // push king pawn 
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E5
            });
            // move king-side bishop
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F1,
                EndingPosition = ChessPosition.B5
            });
            // move H pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H7,
                EndingPosition = ChessPosition.H5
            });
            // move bishop to check King
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.B5,
                EndingPosition = ChessPosition.D7
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.BlackInCheck);

            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E8,
                EndingPosition = ChessPosition.E7
            });

            Assert.AreEqual(game.State, GameState.Ongoing);
        }

        [TestMethod]
        public void Should_SetGameStateToOngoing_After_WhitePieceMoveInTheWayOfCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // move white queen pawn 1
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F2,
                EndingPosition = ChessPosition.F3
            });
            // move black king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });
            //move white queen pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black queen to check white king
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.H4
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.WhiteInCheck);

            // move white pawn between queen and king
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G2,
                EndingPosition = ChessPosition.G3
            });

            Assert.AreEqual(game.State, GameState.Ongoing);
        }

        [TestMethod]
        public void Should_SetGameStateToOngoing_After_BlackPieceMoveInTheWayOfCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // move white king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black F pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });
            // move white queen to check black king
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.H5
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.BlackInCheck);

            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G7,
                EndingPosition = ChessPosition.G6
            });

            Assert.AreEqual(game.State, GameState.Ongoing);
        }

        [TestMethod]
        public void Should_SetGameStateToWhiteChecked_When_BlackChecksWhite()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessul = true;

            // push white king pawn
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black knight
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.B8,
                EndingPosition = ChessPosition.C6
            });
            // push white queen pawn
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.E5
            });
            // move black knight
            isSetupSuccessul &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.C6,
                EndingPosition = ChessPosition.E5
            });
            // move white queen
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.E2
            });
            // move black knight for check
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E5,
                EndingPosition = ChessPosition.F3
            });

            Assert.IsTrue(isSetupSuccessul);
            Assert.AreEqual(game.State, GameState.WhiteInCheck);
        }

        [TestMethod]
        public void Should_SetGameStateToBlackChecked_When_WhiteChecksBlack()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // move white king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // move black F pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });
            // move white queen to check black king
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.H5
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.BlackInCheck);
        }

        [TestMethod]
        public void Should_SetGameStateToWhiteCheck_When_WhiteKingCanOnlyCaptureToGetOutOfCheck()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // move white H pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H2,
                EndingPosition = ChessPosition.H3
            });
            // move black king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E6
            });
            // move white H pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H3,
                EndingPosition = ChessPosition.H4
            });
            // move black queen
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.H4
            });
            // move white A pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A3
            });
            // move black queen for check
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.H4,
                EndingPosition = ChessPosition.F2
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.WhiteInCheck);

            // king captures queen
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.E1,
                EndingPosition = ChessPosition.F2
            });

            Assert.AreEqual(game.State, GameState.Ongoing);
        }

        [TestMethod]
        public void Should_SetGameStateToWhiteCheckMated_When_WhiteKingIsMated()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // move white F pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F2,
                EndingPosition = ChessPosition.F3
            });
            // move black king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E7,
                EndingPosition = ChessPosition.E5
            });
            // move G pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G2,
                EndingPosition = ChessPosition.G4
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.Ongoing);

            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.H4
            });

            Assert.AreEqual(game.State, GameState.WhiteInCheckmate);
        }

        [TestMethod]
        public void Should_SetGameStateToBlackCheckmated_When_BlackKingHasNoMoves()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // move white king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E3
            });
            // move black F pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });
            // move white king pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E3,
                EndingPosition = ChessPosition.E4
            });
            // move black G pawn 2
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.G7,
                EndingPosition = ChessPosition.G5
            });
            // move white queen for mate
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.H5
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.BlackInCheckmate);
        }

        [TestMethod]
        public void Should_SetGameStateToBlackStalemate_When_BlackHasNoMoves()
        {
            var game = new ChessGame(() => typeof(IQueen));
            bool isSetupSuccessful = true;

            // white pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C2,
                EndingPosition = ChessPosition.C4
            });
            // black pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H7,
                EndingPosition = ChessPosition.H5
            });
            // white pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.H2,
                EndingPosition = ChessPosition.H4
            });
            // black pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A5
            });
            // white queen
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.A4
            });
            // black rook
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A6
            });
            // white queen captures black pawn
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.A4,
                EndingPosition = ChessPosition.A5
            });
            // black rook
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A6,
                EndingPosition = ChessPosition.H6
            });
            // white queen caps Cpawn
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.A5,
                EndingPosition = ChessPosition.C7
            });
            // black pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });
            // white queen caps Dpawn
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.C7,
                EndingPosition = ChessPosition.D7
            });
            // black king
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E8,
                EndingPosition = ChessPosition.F7
            });
            // white queen caps Bpawn
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.B7
            });
            // black queen
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.D3
            });
            // white queen caps Bknight
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.B7,
                EndingPosition = ChessPosition.B8
            });
            // black queen
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D3,
                EndingPosition = ChessPosition.H7
            });
            // white queen caps bishop
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.B8,
                EndingPosition = ChessPosition.C8
            });
            // black king
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.G6
            });
            // white queen
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.C8,
                EndingPosition = ChessPosition.E6
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.AreEqual(game.State, GameState.Stalemate);
        }

        [TestMethod]
        public void Should_PromotePawn_When_PawnCrossesBoard()
        {
            var func = new Func<Type>(() => typeof(IQueen));
            var game = new ChessGame(func);

            bool isSetupSuccessful = true;

            // white E pawn forward
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // black d pawn forward
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            });
            // white e pawn captures black d pawn
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.D5
            });
            // black queen moves forward
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.D7
            });
            // white pawn pushes more
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D5,
                EndingPosition = ChessPosition.D6
            });
            // black queen out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.E6
            });
            // white queen blocks check
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.E2
            });
            // black pawn moves to allow king to move out of check
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });
            // check from pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D6,
                EndingPosition = ChessPosition.D7
            });
            // king moves out of check
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E8,
                EndingPosition = ChessPosition.F7
            });
            // pawn pushes to promotion rank
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D8
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.IsTrue(game.WhitePieces.Find(p => p.Location == ChessPosition.D8) is IQueen);

        }

        [TestMethod]
        [ExpectedException(typeof(PawnPromotionException))]
        public void Should_Throw_When_PawnCrossesBoard_PromotedToPawn()
        {
            var func = new Func<Type>(() => typeof(Pawn));
            var game = new ChessGame(func);

            bool isSetupSuccessful = true;

            // white E pawn forward
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E2,
                EndingPosition = ChessPosition.E4
            });
            // black d pawn forward
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D5
            });
            // white e pawn captures black d pawn
            isSetupSuccessful &= game.CapturePiece(new Capture
            {
                StartingPosition = ChessPosition.E4,
                EndingPosition = ChessPosition.D5
            });
            // black queen moves forward
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D8,
                EndingPosition = ChessPosition.D7
            });
            // white pawn pushes more
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D5,
                EndingPosition = ChessPosition.D6
            });
            // black queen out of the way
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.E6
            });
            // white queen blocks check
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D1,
                EndingPosition = ChessPosition.E2
            });
            // black pawn moves to allow king to move out of check
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.F7,
                EndingPosition = ChessPosition.F6
            });
            // check from pawn
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D6,
                EndingPosition = ChessPosition.D7
            });
            // king moves out of check
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.E8,
                EndingPosition = ChessPosition.F7
            });
            // pawn pushes to promotion rank
            isSetupSuccessful &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.D7,
                EndingPosition = ChessPosition.D8
            });

            Assert.IsTrue(isSetupSuccessful);
            Assert.IsTrue(game.WhitePieces.Find(p => p.Location == ChessPosition.D8) is Pawn);
        }

        [TestMethod]
        public void Should_SetGameToStalemate_When_NoPawnsNoCapturesForFiftyMoves()
        {
            var game = new ChessGame(() => typeof(IQueen));

            bool isSetupSuccessul = true;

            // move pawns to allow rooks to move back and forth
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A4
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A5
            });

            Assert.AreEqual(2, game.MoveHistory.Moves.Count);

            // begin moving rooks back and forth
            // fifty moves from now should be stalemate

            #region Move rooks back and forth
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 4 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 8 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 12 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 16 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 20 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 24 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 28 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 32 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 36 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 40 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 44 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A2,
                EndingPosition = ChessPosition.A1
            });
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A7,
                EndingPosition = ChessPosition.A8
            });
            // 48 moves have occured
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A1,
                EndingPosition = ChessPosition.A2
            });
            // 49 moves  have occured

            Assert.AreNotEqual(game.State, GameState.Stalemate);
            Assert.IsTrue(isSetupSuccessul);

            #endregion

            // next move is 50th move
            isSetupSuccessul &= game.MovePiece(new Move
            {
                StartingPosition = ChessPosition.A8,
                EndingPosition = ChessPosition.A7
            });


            Assert.AreEqual(52, game.MoveHistory.Moves.Count);
            Assert.AreEqual(game.State, GameState.Stalemate);
        }
    }

}
