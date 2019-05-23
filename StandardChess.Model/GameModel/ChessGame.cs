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

using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Game;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Player;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StandardChess.Model.GameModel
{
    public class ChessGame : IGame
    {
        #region Fields

        private readonly Func<Type> _pawnPromotionFunc;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a Game with standard Chess starting board.
        /// </summary>
        /// <param name="pawnPromotionFunc">
        ///     A function to allow the user to select the type of piece to promote a pawn to upon
        ///     promotion.
        /// </param>
        public ChessGame(Func<Type> pawnPromotionFunc)
        {
            _pawnPromotionFunc = pawnPromotionFunc ??
                                 throw new ArgumentNullException($"{nameof(pawnPromotionFunc)} cannot be null.");

            GameBoard = ModelLocator.Board;
            
            WhitePieces = ModelLocator.PieceCreationUtility.CreateStartingPieces(ChessColor.White);
            BlackPieces = ModelLocator.PieceCreationUtility.CreateStartingPieces(ChessColor.Black);

            WhitePlayer = ModelLocator.Player;
            BlackPlayer = ModelLocator.Player;

            MoveHistory = ModelLocator.MoveHistory;

            State = GameState.Ongoing;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     The current turn. Measured in halves. One full turn is a move from white and a move from black.
        /// </summary>
        public double Turn { get; private set; }

        /// <summary>
        ///     The board.
        /// </summary>
        public IBoard GameBoard { get; private set; }

        /// <summary>
        ///     White player's pieces.
        /// </summary>
        public List<IPiece> WhitePieces { get; protected set; }

        /// <summary>
        ///     Black player's pieces.
        /// </summary>
        public List<IPiece> BlackPieces { get; protected set; }

        /// <summary>
        ///     White player.
        /// </summary>
        public IPlayer WhitePlayer { get; protected set; }

        /// <summary>
        ///     Black player.
        /// </summary>
        public IPlayer BlackPlayer { get; protected set; }

        /// <summary>
        ///     Score of white player.
        /// </summary>
        public int WhitePlayerScore => WhitePlayer.Score;

        /// <summary>
        ///     Score of black player.
        /// </summary>
        public int BlackPlayerScore => BlackPlayer.Score;

        /// <summary>
        ///     The history of all moves for the game.
        /// </summary>
        public IMoveHistory MoveHistory { get; protected set; }

        /// <summary>
        ///     Returns the state of the game via <see cref="GameState" />
        /// </summary>
        public GameState State { get; protected set; }

        #endregion

        #region Private Properties

        /// <summary>
        ///     The active player's board state.
        /// </summary>
        private IBoardState ActivePlayerBoardState
        {
            get
            {
                var positions = ChessPosition.None;

                ActivePlayerPieces.ForEach(p => { positions |= p.Location; });

                IBoardState activePlayerState = ModelLocator.BoardState;
                activePlayerState.Add(positions);

                return activePlayerState;
            }
        }

        /// <summary>
        ///     The inactive player's board state.
        /// </summary>
        private IBoardState InactivePlayerBoardState
        {
            get
            {
                var positions = ChessPosition.None;

                InactivePlayerPieces.ForEach(p => { positions |= p.Location; });

                IBoardState inactiveBoardState = ModelLocator.BoardState;
                inactiveBoardState.Add(positions);
                return inactiveBoardState;
            }
        }

        /// <summary>
        ///     The active player.
        /// </summary>
        private IPlayer ActivePlayer => ActivePlayerColor == ChessColor.White ? WhitePlayer : BlackPlayer;

        /// <summary>
        ///     The active player's pieces.
        /// </summary>
        private List<IPiece> ActivePlayerPieces => ActivePlayerColor == ChessColor.White ? WhitePieces : BlackPieces;

        /// <summary>
        ///     The inactive player's pieces.
        /// </summary>
        private List<IPiece> InactivePlayerPieces => ActivePlayerColor == ChessColor.White ? BlackPieces : WhitePieces;

        /// <summary>
        ///     Color of active player.
        /// </summary>
        private ChessColor ActivePlayerColor
        {
            get
            {
                var integerTurn = (int)(Turn * 2); // turn is incremented by 0.5 this guarantees an integer

                if (integerTurn % 2 == 0)
                    return ChessColor.White;
                return ChessColor.Black;
            }
        }

        /// <summary>
        ///     Color of inactive player.
        /// </summary>
        private ChessColor InactivePlayerColor
        {
            get
            {
                var integerTurn = (int)(Turn * 2); // turn is incremented by 0.5 this guarantees an integer

                if (integerTurn % 2 == 0)
                    return ChessColor.Black;
                return ChessColor.White;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     This method is used to move a piece.
        /// </summary>
        /// <returns>Whether move/capture was successful.</returns>
        public bool MovePiece(IMove move)
        {
            bool isMoveValid = MakeMove(move);

            if (!isMoveValid) return false;

            State = AnalyzeGameState();
            IncrementTurn();

            return true;
        }

        /// <summary>
        ///     This method is used to capture a piece.
        /// </summary>
        /// <param name="capture"></param>
        /// <returns></returns>
        public bool CapturePiece(ICapture capture)
        {
            bool isCaptureValid = MakeCapture(capture);

            if (!isCaptureValid) return false;
            State = AnalyzeGameState();
            IncrementTurn();

            return true;
        }

        /// <summary>
        ///     This would need finished if a game would want to implement an undo feature.
        /// </summary>
        public void UndoLastMove()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Makes a capture.
        /// </summary>
        /// <param name="capture"></param>
        /// <returns>Returns success of capture.</returns>
        private bool MakeCapture(ICapture capture)
        {
            IPiece capturingPiece = GetPiece(ActivePlayerColor, capture.StartingPosition);

            bool isCaptureLegalEnPassant = IsCaptureLegalEnPassant(capturingPiece, capture, GameBoard);

            if (!IsCaptureLegal(capturingPiece, capture, GameBoard.State) && !isCaptureLegalEnPassant)
                return false;

            if (isCaptureLegalEnPassant)
                ActivePlayer.Score += ExecuteEnPassantCapture(capture);
            else
                ActivePlayer.Score += ExecuteCapture(capture);

            return true;
        }

        /// <summary>
        ///     Makes a move.
        /// </summary>
        /// <param name="move"></param>
        /// <returns>Returns whether move was successful</returns>
        private bool MakeMove(IMove move)
        {
            IPiece piece = GetPiece(ActivePlayerColor, move.StartingPosition);

            bool isMoveLegalCastle = IsCastleLegal(piece, move, GameBoard);

            if (!IsMoveLegal(piece, move, GameBoard.State) && !isMoveLegalCastle)
                return false;

            if (isMoveLegalCastle)
                ExecuteCastle(piece as IKing, move);
            else
                ExecuteMove(move);

            return true;
        }

        /// <summary>
        ///     Returns the current state of the game. E.g. Ongoing, BlackInCheck, WhiteInCheckmate, etc.
        /// </summary>
        /// <returns></returns>
        private GameState AnalyzeGameState()
        {
            var king = ActivePlayerPieces.Find(p => p is IKing) as IKing;
            List<IPiece> piecesThreateningKing = GetPiecesThreateningKing(king);

            GameState state = AnalyzeForCheck(king, piecesThreateningKing);

            var isCheckmate = false;
            if (state != GameState.Ongoing)
                isCheckmate = IsBoardStateInCheckmate(king, piecesThreateningKing);

            if (isCheckmate)
                state = ActivePlayerColor == ChessColor.White ? GameState.WhiteInCheckmate : GameState.BlackInCheckmate;

            var isStalemate = false;
            if (!isCheckmate && state != GameState.BlackInCheck && state != GameState.WhiteInCheck)
                isStalemate = IsBoardStateInStalemate();

            if (isStalemate)
                state = GameState.Stalemate;

            return state;
        }

        /// <summary>
        ///     Determine if the current boardstate is a stalemate state.
        /// </summary>
        /// <returns></returns>
        private bool IsBoardStateInStalemate()
        {
            if (MoveHistory.Count >= 50)
            {
                bool wasPieceCapturedInLastFiftyMoves = MoveHistory.WasPieceCapturedInLastFiftyMoves;
                bool wasPawnMovedInLastFiftyMoves = MoveHistory.WasPawnMovedInLastFiftyMoves;

                if (!wasPawnMovedInLastFiftyMoves && !wasPieceCapturedInLastFiftyMoves)
                    return true;
            }

            foreach (IPiece piece in ActivePlayerPieces)
                if (DoesPieceHaveLegalMove(piece) || DoesPieceHaveLegalCapture(piece))
                    return false;

            return true;
        }

        /// <summary>
        ///     Returns whether the piece has any possible legal moves.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private bool DoesPieceHaveLegalMove(IPiece piece)
        {
            piece.GenerateMoves(GameBoard.State);
            IMove move = ModelLocator.Move;
            move.StartingPosition = piece.Location;

            IBoardState moveSet = ModelLocator.BoardState;
            moveSet.Add(piece.MoveSet);

            foreach (ChessPosition position in moveSet)
            {
                move.EndingPosition = position;
                if (IsMoveLegal(piece, move, GameBoard.State))
                    return true;
            }

            if (!(piece is IKing))
                return false;

            IEnumerable<ChessPosition> moves = ModelLocator.CastlingHelper.GetCastleMovesForKing((IKing) piece);

            foreach (ChessPosition castleMove in moves)
            {
                move.EndingPosition = castleMove;

                if (IsCastleLegal(piece, move, GameBoard)) return true;
            }

            return false;
        }

        /// <summary>
        ///     Returns whether the piece has any possible legal captures.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private bool DoesPieceHaveLegalCapture(IPiece piece)
        {
            piece.GenerateCaptures(GameBoard.State, ActivePlayerBoardState);
            ICapture capture = ModelLocator.Capture;
            capture.StartingPosition = piece.Location;

            IBoardState captureSet = ModelLocator.BoardState;
            captureSet.Add(piece.CaptureSet);

            foreach (ChessPosition position in captureSet)
            {
                capture.EndingPosition = position;
                if (IsCaptureLegal(piece, capture, GameBoard.State))
                    return true;
                if (piece is IPawn && IsCaptureLegalEnPassant(piece, capture, GameBoard))
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Returns all pieces that are threatening the passed in King
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        private List<IPiece> GetPiecesThreateningKing(IKing king)
        {
            var piecesThreateningKing = new List<IPiece>();
            InactivePlayerPieces.ForEach(p =>
            {
                p.GenerateThreatened(GameBoard.State, InactivePlayerBoardState);

                if (p.IsThreateningAt(king.Location))
                    piecesThreateningKing.Add(p);
            });

            return piecesThreateningKing;
        }

        /// <summary>
        ///     Is the board in a check state?
        /// </summary>
        /// <param name="king"></param>
        /// <param name="piecesThreateningKing"></param>
        /// <returns></returns>
        private GameState AnalyzeForCheck(IPiece king, List<IPiece> piecesThreateningKing)
        {
            var isKingThreatened = false;
            piecesThreateningKing.ForEach(p => isKingThreatened |= p.IsThreateningAt(king.Location));

            if (isKingThreatened)
                return ActivePlayerColor == ChessColor.White ? GameState.WhiteInCheck : GameState.BlackInCheck;

            return GameState.Ongoing;
        }

        /// <summary>
        ///     Is the board in a checkmate state?
        /// </summary>
        /// <param name="king"></param>
        /// <param name="piecesThreateningKing"></param>
        /// <returns></returns>
        private bool IsBoardStateInCheckmate(IKing king, IReadOnlyList<IPiece> piecesThreateningKing)
        {
            bool isKingInCheckFromMultiplePieces = piecesThreateningKing.Count > 1; // king must capture a piece or move

            king.GenerateThreatened(GameBoard.State, ActivePlayerBoardState);
            bool isCheckMateAvoidable = CanKingMoveOrCaptureOutOfCheck(king, GameBoard.State);

            if (isCheckMateAvoidable)
                return false;
            if (isKingInCheckFromMultiplePieces
            ) // if king is attacked by multiple pieces, it must either move or capture to avoid checkmate.
                return true;

            Debug.Assert(piecesThreateningKing.Count == 1);

            isCheckMateAvoidable |= CanThreateningPieceBeCaptured(piecesThreateningKing[0]);
            isCheckMateAvoidable |= CanFriendlyPieceMoveBetweenKingAndAttacker(piecesThreateningKing[0]);

            return !isCheckMateAvoidable;
        }

        /// <summary>
        ///     Can any friendly piece block the attacker?
        /// </summary>
        /// <param name="threateningPiece"></param>
        /// <returns></returns>
        private bool CanFriendlyPieceMoveBetweenKingAndAttacker(IPiece threateningPiece)
        {
            switch (threateningPiece)
            {
                case IKnight _: // knights jump pieces, cannot move between
                    return false;
                case IPawn _: // pawns attack in an adjacent square, cannot move between
                    return false;
                case IKing _: // king will never be checking another king.
                    return false;
            }

            foreach (IPiece activePlayerPiece in ActivePlayerPieces.Where(p => !(p is IKing)))
            {
                bool FilterPieces(ChessPosition p)
                {
                    activePlayerPiece.GenerateMoves(GameBoard.State);
                    return activePlayerPiece.CanMoveTo(p);
                }

                foreach (ChessPosition position in threateningPiece.ThreatenSet.Where(FilterPieces))
                {
                    IMove move = ModelLocator.Move;
                    move.StartingPosition = activePlayerPiece.Location;
                    move.EndingPosition = position;

                    ICapture capture = ModelLocator.Capture;
                    capture.StartingPosition = move.StartingPosition;
                    capture.EndingPosition = move.EndingPosition;

                    if (!(IsMoveLegal(activePlayerPiece, move, GameBoard.State) ||
                          IsCaptureLegal(activePlayerPiece, capture, GameBoard.State)))
                        continue;

                    if (!DoesPotentialMoveLeaveKingInCheck(move))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Does the active player control a piece that can capture the piece threatening the king?
        /// </summary>
        private bool CanThreateningPieceBeCaptured(IPiece threateningPiece)
        {
            foreach (IPiece activePlayerPiece in ActivePlayerPieces)
            {
                activePlayerPiece.GenerateCaptures(GameBoard.State, ActivePlayerBoardState);
                bool canCaptureAt = activePlayerPiece.CanCaptureAt(threateningPiece.Location);

                if (!canCaptureAt)
                    continue;

                ICapture capture = ModelLocator.Capture;
                capture.StartingPosition = activePlayerPiece.Location;
                capture.EndingPosition = threateningPiece.Location;

                bool isCaptureLegal = IsCaptureLegal(activePlayerPiece, capture, GameBoard.State);

                if (isCaptureLegal)
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Determine if the king can move out of check by capturing a piece or simply moving.
        /// </summary>
        private bool CanKingMoveOrCaptureOutOfCheck(IKing king, IBoardState gameBoardState)
        {
            var canKingMoveOutOfCheck = false;
            var canKingCaptureOutOfCheck = false;

            // 1.) Can the king move or capture out of check?

            foreach (ChessPosition position in king.ThreatenSet)
            {
                IMove move = ModelLocator.Move;
                move.StartingPosition = king.Location;
                move.EndingPosition = position;

                ICapture capture = ModelLocator.Capture;
                capture.StartingPosition = king.Location;
                capture.EndingPosition = position;

                canKingMoveOutOfCheck |= IsMoveLegal(king, move, gameBoardState);
                canKingCaptureOutOfCheck |= IsCaptureLegal(king, capture, gameBoardState);
            }

            return canKingMoveOutOfCheck || canKingCaptureOutOfCheck;
        }

        /// <summary>
        ///     Retrieves a Piece based on color and position.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private IPiece GetPiece(ChessColor color, ChessPosition position)
        {
            List<IPiece> pieces = color == ChessColor.White ? WhitePieces : BlackPieces;
            return pieces.Find(p => p.Location == position);
        }

        /// <summary>
        ///     Increments the turn by 1/2.
        /// </summary>
        private void IncrementTurn()
        {
            Turn += 0.5;
        }

        /// <summary>
        ///     Determines if move is a legal attempt at En Passant.
        /// </summary>
        /// <param name="capturingPiece"></param>
        /// <param name="capture"></param>
        /// <param name="gameBoard"></param>
        /// <returns></returns>
        private bool IsCaptureLegalEnPassant(IPiece capturingPiece, ICapture capture, IBoard gameBoard)
        {
            // 1.) only Pawns can capture via En Passant
            if (!(capturingPiece is IPawn))
                return false;

            // 2.) white pawns must be on Rank 5, black pawns must be on Rank 4
            bool isCapturingPawnOnCorrectRank = capturingPiece.Color == ChessColor.White
                ? (capturingPiece.Location & ChessPosition.Rank5) == capturingPiece.Location
                : (capturingPiece.Location & ChessPosition.Rank4) == capturingPiece.Location;

            if (!isCapturingPawnOnCorrectRank)
                return false;

            // 3.) Pawn may not move to an occupied square
            if (gameBoard.IsPositionOccupied(capture.EndingPosition))
                return false;

            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            // get the position of the piece we're trying to capture via En Passant
            ChessPosition locationOfPotentiallyCapturedPiece = capturingPiece.Color == ChessColor.White
                ? cpm.South(capture.EndingPosition)
                : cpm.North(capture.EndingPosition);

            // 4.) piece being captured must be a Pawn
            IPiece pieceBeingCaptured = GetPiece(InactivePlayerColor, locationOfPotentiallyCapturedPiece);

            // 5.) pawn must be capturable by en passant
            return pieceBeingCaptured is IPawn pawn && pawn.IsCapturableByEnPassant;
        }

        /// <summary>
        ///     Checks if the castle is legal by a process of six standard steps.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private bool IsCastleLegal(IPiece piece, IMove move, IBoard board)
        {
            // 1.) has the King moved already?
            if (!(piece is IKing king) || piece.HasMoved)
                return false;
            
            IRook rook = GetCastlingRook(move, king.Color);
            // 2.) has the Rook moved already?
            if (rook == null || rook.HasMoved)
                return false;

            List<ChessPosition> piecesBetweenRookAndKing = ModelLocator.CastlingHelper.GetPositionsBetweenCastle(king, rook);
            // 3.) are there pieces standing between the King and Rook?
            foreach (ChessPosition location in piecesBetweenRookAndKing)
                if (board.IsPositionOccupied(location))
                    return false;

            // 4.) is the King currently in check?
            if (IsPositionThreatened(king.Location, board, InactivePlayerBoardState))
                return false;

            // 5.) are any positions between King and Rook threatened?
            foreach (ChessPosition position in piecesBetweenRookAndKing)
                if (IsPositionThreatened(position, board, InactivePlayerBoardState))
                    return false;

            // 6.) will the King be in check after castling?
            return !IsPositionThreatened(move.EndingPosition, board, InactivePlayerBoardState);
        }

        /// <summary>
        ///     Returns approriate Rook based on where the King is trying to move.
        /// </summary>
        /// <param name="move">King movement</param>
        /// <param name="color">King color</param>
        /// <returns></returns>
        private IRook GetCastlingRook(IMovable move, ChessColor color)
        {
            ChessPosition rookPosition = ModelLocator.CastlingHelper.GetCastlingRookPosition(move);

            return rookPosition == ChessPosition.None ? null : (IRook)GetPiece(color, rookPosition);
        }

        /// <summary>
        ///     Determine whether a position is threatened.
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <param name="board">Board to reference</param>
        /// <param name="inactivePlayerBoardState">Used to only check opponent's pieces</param>
        /// <returns></returns>
        private bool IsPositionThreatened(ChessPosition position, IBoard board, IBoardState inactivePlayerBoardState)
        {
            foreach (IPiece enemyPiece in InactivePlayerPieces.Where(p => p.Location != ChessPosition.None))
            {
                enemyPiece.GenerateThreatened(board.State, inactivePlayerBoardState);
                if (enemyPiece.IsThreateningAt(position))
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Is a move legal?
        /// </summary>
        /// <returns></returns>
        private bool IsMoveLegal(IPiece piece, IMove move, IBoardState state)
        {
            piece.GenerateMoves(state);

            bool canPieceMove = piece.CanMoveTo(move.EndingPosition);
            bool doesMoveLeaveKingInCheck = DoesPotentialMoveLeaveKingInCheck(move);

            return canPieceMove && !doesMoveLeaveKingInCheck;
        }

        /// <summary>
        ///     Is a capture legal?
        /// </summary>
        /// <returns></returns>
        private bool IsCaptureLegal(IPiece piece, ICapture capture, IBoardState state)
        {
            piece.GenerateCaptures(state, ActivePlayerBoardState);
            bool canPieceCapture = piece.CanCaptureAt(capture.EndingPosition);
            bool doesCaptureLeaveKingInCheck = DoesPotentialMoveLeaveKingInCheck(capture);

            return canPieceCapture && !doesCaptureLeaveKingInCheck;
        }

        /// <summary>
        ///     Will the friendly King be in check if this move is made?
        /// </summary>
        /// <param name="potentialMove">Move to check</param>
        /// <returns></returns>
        private bool DoesPotentialMoveLeaveKingInCheck(IMovable potentialMove)
        {
            // get a new instance of a board
            IBoard board = ModelLocator.Board;
            foreach (ChessPosition chessPosition in GameBoard.State) board.Add(chessPosition);

            // create a chess game to pre-check the move before allowing a move in the actual game
            var game = new ChessGame(() => typeof(IQueen))
            {
                BlackPieces = new List<IPiece>(BlackPieces),
                WhitePieces = new List<IPiece>(WhitePieces),
                GameBoard = board,
                Turn = Turn
            };

            IPiece king = game.ActivePlayerPieces.Find(p => p is IKing);
            bool isKingMovingCurrently = king.Location == potentialMove.StartingPosition;

            game.UpdateBoard(potentialMove);

            return game.InactivePlayerPieces.Any(p =>
            {
                p.GenerateCaptures(game.GameBoard.State, game.InactivePlayerBoardState);
                return p.CanCaptureAt(isKingMovingCurrently ? potentialMove.EndingPosition : king.Location);
            });
        }

        /// <summary>
        ///     Executes En Passant capture. Does not check legality.
        /// </summary>
        /// <param name="capture"></param>
        /// <returns></returns>
        private int ExecuteEnPassantCapture(ICapture capture)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            IPiece movingPiece = GetPiece(ActivePlayerColor, capture.StartingPosition);

            ChessPosition locationOfLostPiece = movingPiece.Color == ChessColor.White
                ? cpm.South(capture.EndingPosition)
                : cpm.North(capture.EndingPosition);

            IPiece lostPiece = GetPiece(InactivePlayerColor, locationOfLostPiece);

            UpdateBoardForEnPassant(movingPiece as IPawn, capture, lostPiece as IPawn);

            return lostPiece.Value;
        }

        /// <summary>
        ///     Updates the board after En Passant. Does not check legality.
        /// </summary>
        /// <param name="attacker">Pawn that is passing</param>
        /// <param name="capture">Capture to make</param>
        /// <param name="lostPiece">Pawn being captured</param>
        private void UpdateBoardForEnPassant(IPawn attacker, ICapture capture, IPawn lostPiece)
        {
            GameBoard.Remove(attacker.Location);
            attacker.MoveTo(capture.EndingPosition);
            GameBoard.Add(capture.EndingPosition);

            GameBoard.Remove(lostPiece.Location);
            lostPiece.Location = ChessPosition.None;

            MoveHistory.Add(attacker, capture);
        }

        /// <summary>
        ///     Executes a normal capture. Does not check for legality.
        /// </summary>
        /// <param name="capture"></param>
        /// <returns>Point value of piece being captured</returns>
        private int ExecuteCapture(ICapture capture)
        {
            IPiece movingPiece = GetPiece(ActivePlayerColor, capture.StartingPosition);
            IPiece lostPiece = GetPiece(InactivePlayerColor, capture.EndingPosition);

            movingPiece.MoveTo(capture.EndingPosition);
            lostPiece.Location = ChessPosition.None;

            UpdateBoard(capture);
            MoveHistory.Add(movingPiece, capture);

            return lostPiece.Value;
        }

        /// <summary>
        ///     Executes a move. Does not check legality.
        /// </summary>
        /// <param name="move"></param>
        private void ExecuteMove(IMove move)
        {
            IPiece movingPiece = GetPiece(ActivePlayerColor, move.StartingPosition);

            movingPiece.MoveTo(move.EndingPosition);

            if (movingPiece is IPawn pawn && pawn.IsPromotable)
                PromotePawn(movingPiece);

            UpdateBoard(move);

            MoveHistory.Add(movingPiece, move);
        }

        /// <summary>
        ///     Replaces the pawn on the board with the desired piece as defined by _pawnPromotionFunc
        /// </summary>
        /// <param name="movingPiece"></param>
        private void PromotePawn(IPiece movingPiece)
        {
            Type pieceType = _pawnPromotionFunc();

            bool isPieceTypeCorrect = pieceType == typeof(IQueen) ||
                                      pieceType == typeof(IRook) ||
                                      pieceType == typeof(IKnight) ||
                                      pieceType == typeof(IBishop);

            if (!isPieceTypeCorrect)
                throw new PawnPromotionException(@"A pawn can only be promoted to a Queen, Knight, Rook or Bishop.");
            
            IPiece newPiece =
                ModelLocator.PieceCreationUtility.PromotePawn(pieceType, movingPiece.Location, movingPiece.Color);
            
            newPiece.HasMoved = true;

            ActivePlayerPieces.Remove(movingPiece);
            ActivePlayerPieces.Add(newPiece);
        }

        /// <summary>
        ///     Updates a board for the given move. Does not check legality.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        private void ExecuteCastle(IKing piece, IMove move)
        {
            IRook rook = GetCastlingRook(move, piece.Color);

            UpdateBoardForCastle(piece, move, rook);
        }

        private void UpdateBoard(IMovable movable)
        {
            if (!GameBoard.IsPositionOccupied(movable.StartingPosition)) return;

            if (movable is ICapture && !GameBoard.IsPositionOccupied(movable.EndingPosition)) return;

            if (!(movable is ICapture) && GameBoard.IsPositionOccupied(movable.EndingPosition)) return;

            GameBoard.Remove(movable.StartingPosition);
            GameBoard.Add(movable.EndingPosition);
        }

        /// <summary>
        ///     Updates board for Castle. Does not check legality.
        /// </summary>
        /// <param name="king"></param>
        /// <param name="move"></param>
        /// <param name="rook"></param>
        private void UpdateBoardForCastle(IKing king, IMove move, IRook rook)
        {
            ChessPosition newRookLocation = ModelLocator.CastlingHelper.GetEndingPositionForCastlingRook(king, rook);

            GameBoard.Remove(king.Location);    // remove King from board
            king.MoveTo(move.EndingPosition);   // move King, update location
            GameBoard.Add(move.EndingPosition); // place King on board at update location

            GameBoard.Remove(rook.Location); // remove Rook from board
            rook.MoveTo(newRookLocation);    // move Rook, update location
            GameBoard.Add(rook.Location);    // place Rook on board at updated location            

            MoveHistory.Add(king, move);
        }

        #endregion
    }
}