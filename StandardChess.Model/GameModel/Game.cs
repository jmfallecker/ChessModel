using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.Exceptions;
using StandardChess.Model.Interfaces;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;
using StandardChess.Model.PlayerModel;

namespace StandardChess.Model.GameModel
{
    public class Game
    {
        #region Fields

        /// <summary>
        /// Determines the state of the game. States include Ongoing, Check, Checkmate
        /// </summary>
        public enum GameState { Ongoing, WhiteInCheck, BlackInCheck, WhiteInCheckmate, BlackInCheckmate, Stalemate }

        private readonly Func<Type> _pawnPromotionFunc;

        #endregion

        #region Properties

        /// <summary>
        /// The current turn. Measured in halves. One full turn is a move from white and a move from black.
        /// </summary>
        public double Turn { get; private set; }

        /// <summary>
        /// The board.
        /// </summary>
        public Board GameBoard { get; private set; }

        /// <summary>
        /// White player's pieces.
        /// </summary>
        public List<Piece> WhitePieces { get; protected set; }

        /// <summary>
        /// Black player's pieces.
        /// </summary>
        public List<Piece> BlackPieces { get; protected set; }

        /// <summary>
        /// White player.
        /// </summary>
        public Player WhitePlayer { get; protected set; }

        /// <summary>
        /// Black player.
        /// </summary>
        public Player BlackPlayer { get; protected set; }

        /// <summary>
        /// Score of white player.
        /// </summary>
        public int WhitePlayerScore => WhitePlayer.Score;

        /// <summary>
        /// Score of black player.
        /// </summary>
        public int BlackPlayerScore => BlackPlayer.Score;

        /// <summary>
        /// The history of all moves for the game.
        /// </summary>
        public MoveHistory MoveHistory { get; protected set; }

        /// <summary>
        /// Returns the state of the game via <see cref="GameState"/>
        /// </summary>
        public GameState State { get; protected set; }

        /// <summary>
        /// The active player's board state.
        /// </summary>
        private IBoardState ActivePlayerBoardState
        {
            get
            {
                var positions = ChessPosition.None;

                ActivePlayerPieces.ForEach(p =>
                {
                    positions |= p.Location;
                });

                IBoardState activePlayerState = ModelLocator.BoardState;
                activePlayerState.Add(positions);

                return activePlayerState;
            }
        }

        /// <summary>
        /// The inactive player's board state.
        /// </summary>
        private IBoardState InactivePlayerBoardState
        {
            get
            {
                var positions = ChessPosition.None;

                InactivePlayerPieces.ForEach(p =>
                {
                    positions |= p.Location;
                });

                IBoardState inactiveBoardState = ModelLocator.BoardState;
                inactiveBoardState.Add(positions);
                return inactiveBoardState;
            }
        }

        /// <summary>
        /// The active player.
        /// </summary>
        private Player ActivePlayer => (ActivePlayerColor == ChessColor.White) ? WhitePlayer : BlackPlayer;

        /// <summary>
        /// The active player's pieces.
        /// </summary>
        private List<Piece> ActivePlayerPieces => ActivePlayerColor == ChessColor.White ? WhitePieces : BlackPieces;

        /// <summary>
        /// The inactive player's pieces.
        /// </summary>
        private List<Piece> InactivePlayerPieces => ActivePlayerColor == ChessColor.White ? BlackPieces : WhitePieces;

        /// <summary>
        /// Color of active player.
        /// </summary>
        private ChessColor ActivePlayerColor
        {
            get
            {
                var integerTurn = (int)(Turn * 2); // turn is incremented by 0.5 this guarantees an integer

                if (integerTurn % 2 == 0)
                    return ChessColor.White;
                else
                    return ChessColor.Black;
            }
        }

        /// <summary>
        /// Color of inactive player.
        /// </summary>
        private ChessColor InactivePlayerColor
        {
            get
            {
                var integerTurn = (int)(Turn * 2); // turn is incremented by 0.5 this guarantees an integer

                if (integerTurn % 2 == 0)
                    return ChessColor.Black;
                else
                    return ChessColor.White;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a Game with standard Chess starting board.
        /// </summary>
        /// <param name="pawnPromotionFunc">A function to allow the user to select the type of piece to promote a pawn to upon promotion.</param>
        public Game(Func<Type> pawnPromotionFunc)
        {
            GameBoard = new Board();

            WhitePieces = CreatePieces(ChessColor.White);
            BlackPieces = CreatePieces(ChessColor.Black);

            WhitePlayer = new Player();
            BlackPlayer = new Player();

            MoveHistory = new MoveHistory();

            State = GameState.Ongoing;

            _pawnPromotionFunc = pawnPromotionFunc ?? throw new ArgumentNullException();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method is used to both move and capture a piece.
        /// </summary>
        /// <param name="move">If of type <see cref="Move"/>, this will move a piece. If of type <see cref="Capture"/>, this will capture a piece.</param>
        /// <returns>Whether move/capture was successful.</returns>
        public bool MovePiece(Move move)
        {
            bool wasSuccessful = false;

            if (move.IsCapture)
                wasSuccessful |= MakeCapture(move as Capture);
            else
                wasSuccessful |= MakeMove(move);

            State = AnalyzeGameState();

            return wasSuccessful;
        }

        /// <summary>
        /// Not finished, only undoes history of last move, does not undo the last move on the board.
        /// This would need finished if a game would want to implement an undo feature.
        /// </summary>
        private void UndoLastMoveHistory()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns starting position pieces for the passed in color.
        /// </summary>
        /// <param name="color">Black or White</param>
        /// <returns></returns>
        private static List<Piece> CreatePieces(ChessColor color)
        {
            var pieces = new List<Piece>();

            switch (color)
            {
                case ChessColor.White:
                    // create pawns
                    Piece pawnA2 = new Pawn(ChessPosition.A2, color);
                    Piece pawnB2 = new Pawn(ChessPosition.B2, color);
                    Piece pawnC2 = new Pawn(ChessPosition.C2, color);
                    Piece pawnD2 = new Pawn(ChessPosition.D2, color);
                    Piece pawnE2 = new Pawn(ChessPosition.E2, color);
                    Piece pawnF2 = new Pawn(ChessPosition.F2, color);
                    Piece pawnG2 = new Pawn(ChessPosition.G2, color);
                    Piece pawnH2 = new Pawn(ChessPosition.H2, color);

                    // create bigger pieces
                    Piece rookA1 = new Rook(ChessPosition.A1, color);
                    Piece knightB1 = new Knight(ChessPosition.B1, color);
                    Piece bishopC1 = new Bishop(ChessPosition.C1, color);
                    Piece queenD1 = new Queen(ChessPosition.D1, color);
                    Piece kingE1 = new King(ChessPosition.E1, color);
                    Piece bishopF1 = new Bishop(ChessPosition.F1, color);
                    Piece knightG1 = new Knight(ChessPosition.G1, color);
                    Piece rookH1 = new Rook(ChessPosition.H1, color);

                    // add pawns to dictionary
                    pieces.Add(pawnA2);
                    pieces.Add(pawnB2);
                    pieces.Add(pawnC2);
                    pieces.Add(pawnD2);
                    pieces.Add(pawnE2);
                    pieces.Add(pawnF2);
                    pieces.Add(pawnG2);
                    pieces.Add(pawnH2);

                    // add bigger pieces to dictionary
                    pieces.Add(rookA1);
                    pieces.Add(knightB1);
                    pieces.Add(bishopC1);
                    pieces.Add(queenD1);
                    pieces.Add(kingE1);
                    pieces.Add(bishopF1);
                    pieces.Add(knightG1);
                    pieces.Add(rookH1);
                    break;
                case ChessColor.Black:
                    // create pawns
                    Piece pawnA7 = new Pawn(ChessPosition.A7, color);
                    Piece pawnB7 = new Pawn(ChessPosition.B7, color);
                    Piece pawnC7 = new Pawn(ChessPosition.C7, color);
                    Piece pawnD7 = new Pawn(ChessPosition.D7, color);
                    Piece pawnE7 = new Pawn(ChessPosition.E7, color);
                    Piece pawnF7 = new Pawn(ChessPosition.F7, color);
                    Piece pawnG7 = new Pawn(ChessPosition.G7, color);
                    Piece pawnH7 = new Pawn(ChessPosition.H7, color);

                    // create bigger pieces
                    Piece rookA8 = new Rook(ChessPosition.A8, color);
                    Piece knightB8 = new Knight(ChessPosition.B8, color);
                    Piece bishopC8 = new Bishop(ChessPosition.C8, color);
                    Piece queenD8 = new Queen(ChessPosition.D8, color);
                    Piece kingE8 = new King(ChessPosition.E8, color);
                    Piece bishopF8 = new Bishop(ChessPosition.F8, color);
                    Piece knightG8 = new Knight(ChessPosition.G8, color);
                    Piece rookH8 = new Rook(ChessPosition.H8, color);

                    // add pawns to dictionary
                    pieces.Add(pawnA7);
                    pieces.Add(pawnB7);
                    pieces.Add(pawnC7);
                    pieces.Add(pawnD7);
                    pieces.Add(pawnE7);
                    pieces.Add(pawnF7);
                    pieces.Add(pawnG7);
                    pieces.Add(pawnH7);

                    // add bigger pieces to dictionary
                    pieces.Add(rookA8);
                    pieces.Add(knightB8);
                    pieces.Add(bishopC8);
                    pieces.Add(queenD8);
                    pieces.Add(kingE8);
                    pieces.Add(bishopF8);
                    pieces.Add(knightG8);
                    pieces.Add(rookH8);
                    break;
            }

            return pieces;
        }

        /// <summary>
        /// Makes a capture.
        /// </summary>
        /// <param name="capture"></param>
        /// <returns>Returns success of capture.</returns>
        private bool MakeCapture(Capture capture)
        {
            var capturingPiece = GetPiece(ActivePlayerColor, capture.StartingPosition);

            bool isCaptureLegalEnPassant = IsCaptureLegalEnPassant(capturingPiece, capture, GameBoard);

            if (!IsCaptureLegal(capturingPiece, capture, GameBoard.State) && !isCaptureLegalEnPassant)
                return false;

            if (isCaptureLegalEnPassant)
                ActivePlayer.Score += ExecuteEnPassantCapture(capture);
            else
                ActivePlayer.Score += ExecuteCapture(capture);

            IncrementTurn();
            return true;
        }

        /// <summary>
        /// Makes a move.
        /// </summary>
        /// <param name="move"></param>
        /// <returns>Returns whether move was successful</returns>
        private bool MakeMove(Move move)
        {
            var piece = GetPiece(ActivePlayerColor, move.StartingPosition);

            bool isMoveLegalCastle = IsCastleLegal(piece, move, GameBoard);

            if (!IsMoveLegal(piece, move, GameBoard.State) && !isMoveLegalCastle)
                return false;

            if (isMoveLegalCastle)
                ExecuteCastle(piece as King, move);
            else
                ExecuteMove(move);

            IncrementTurn();
            return true;
        }

        /// <summary>
        /// Returns the current state of the game. E.g. Ongoing, BlackInCheck, WhiteInCheckmate, etc.
        /// </summary>
        /// <returns></returns>
        private GameState AnalyzeGameState()
        {
            var king = ActivePlayerPieces.Find(p => p is King) as King;
            List<Piece> piecesThreateningKing = GetPiecesThreateningKing(king);

            GameState state = AnalyzeForCheck(king, piecesThreateningKing);

            bool isCheckmate = false;
            if (state != GameState.Ongoing)
                isCheckmate = IsBoardStateInCheckmate(king, piecesThreateningKing);

            if (isCheckmate)
                state = ActivePlayerColor == ChessColor.White ? GameState.WhiteInCheckmate : GameState.BlackInCheckmate;

            bool isStalemate = false;
            if (!isCheckmate && state != GameState.BlackInCheck && state != GameState.WhiteInCheck)
                isStalemate = IsBoardStateInStalemate();

            if (isStalemate)
                state = GameState.Stalemate;

            return state;
        }

        /// <summary>
        /// Determine if the current boardstate is a stalemate state.
        /// </summary>
        /// <returns></returns>
        private bool IsBoardStateInStalemate()
        {
            if (MoveHistory.Count >= 50)
            {
                bool wasPieceCapturedInLastFiftyMoves = MoveHistory.WasPieceCapturedInLastFiftyMoves();
                bool wasPawnMovedInLastFiftyMoves = MoveHistory.WasPawnMovedInLastFiftyMoves();

                if (!wasPawnMovedInLastFiftyMoves && !wasPieceCapturedInLastFiftyMoves)
                    return true;
            }

            foreach (Piece piece in ActivePlayerPieces)
            {
                if (DoesPieceHaveLegalMove(piece) || DoesPieceHaveLegalCapture(piece))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether the piece has any possible legal moves.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private bool DoesPieceHaveLegalMove(Piece piece)
        {
            piece.GenerateMoves(GameBoard.State);
            var move = new Move
            {
                StartingPosition = piece.Location
            };
            IBoardState moveSet = ModelLocator.BoardState;
            moveSet.Add(piece.MoveSet);

            foreach (ChessPosition position in moveSet)
            {
                move.EndingPosition = position;
                if (IsMoveLegal(piece, move, GameBoard.State))
                    return true;
            }

            if (!(piece is King))
                return false;

            foreach (ChessPosition castleMove in GetCastleMovesForKing((King)piece))
            {
                move.EndingPosition = castleMove;
                if (IsCastleLegal(piece, move, GameBoard))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns whether the piece has any possible legal captures.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        private bool DoesPieceHaveLegalCapture(Piece piece)
        {
            piece.GenerateCaptures(GameBoard.State, ActivePlayerBoardState);
            var capture = new Capture
            {
                StartingPosition = piece.Location
            };
            IBoardState captureSet = ModelLocator.BoardState;
            captureSet.Add(piece.CaptureSet);

            foreach (ChessPosition position in captureSet)
            {
                capture.EndingPosition = position;
                if (IsCaptureLegal(piece, capture, GameBoard.State))
                    return true;
                if (piece is Pawn && IsCaptureLegalEnPassant(piece, capture, GameBoard))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns all pieces that are threatening the passed in King
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        private List<Piece> GetPiecesThreateningKing(King king)
        {
            var piecesThreateningKing = new List<Piece>();
            InactivePlayerPieces.ForEach(p =>
            {
                p.GenerateThreatened(GameBoard.State, InactivePlayerBoardState);

                if (p.IsThreateningAt(king.Location))
                    piecesThreateningKing.Add(p);
            });

            return piecesThreateningKing;
        }

        /// <summary>
        /// Generates the two-space moves that a king can make to initiate a castle.
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        private static IEnumerable<ChessPosition> GetCastleMovesForKing(King king)
        {
            var positions = new List<ChessPosition>();
            if (king.HasMoved)
                return positions;

            if (king.Color == ChessColor.White)
            {
                positions.Add(ChessPosition.G1);
                positions.Add(ChessPosition.C1);
            }
            else
            {
                positions.Add(ChessPosition.G8);
                positions.Add(ChessPosition.C8);
            }

            return positions;
        }

        /// <summary>
        /// Is the board in a check state?
        /// </summary>
        /// <param name="king"></param>
        /// <param name="piecesThreateningKing"></param>
        /// <returns></returns>
        private GameState AnalyzeForCheck(Piece king, List<Piece> piecesThreateningKing)
        {
            bool isKingThreatened = false;
            piecesThreateningKing.ForEach(p => isKingThreatened |= p.IsThreateningAt(king.Location));

            if (isKingThreatened)
            {
                return (ActivePlayerColor == ChessColor.White) ? GameState.WhiteInCheck : GameState.BlackInCheck;
            }

            return GameState.Ongoing;
        }

        /// <summary>
        /// Is the board in a checkmate state?
        /// </summary>
        /// <param name="king"></param>
        /// <param name="piecesThreateningKing"></param>
        /// <returns></returns>
        private bool IsBoardStateInCheckmate(King king, IReadOnlyList<Piece> piecesThreateningKing)
        {
            bool isKingInCheckFromMultiplePieces = piecesThreateningKing.Count > 1; // king must capture a piece or move

            king.GenerateThreatened(GameBoard.State, ActivePlayerBoardState);
            bool isCheckMateAvoidable = CanKingMoveOrCaptureOutOfCheck(king, GameBoard.State);

            if (isCheckMateAvoidable)
                return false;
            if (isKingInCheckFromMultiplePieces) // if king is attacked by multiple pieces, it must either move or capture to avoid checkmate.
                return true;

            Debug.Assert(piecesThreateningKing.Count == 1);

            isCheckMateAvoidable |= CanThreateningPieceBeCaptured(piecesThreateningKing[0]);
            isCheckMateAvoidable |= CanFriendlyPieceMoveBetweenKingAndAttacker(king, piecesThreateningKing[0]);

            return !isCheckMateAvoidable;
        }

        /// <summary>
        /// Can any friendly piece block the attacker?
        /// </summary>
        /// <param name="king"></param>
        /// <param name="threateningPiece"></param>
        /// <returns></returns>
        private bool CanFriendlyPieceMoveBetweenKingAndAttacker(King king, Piece threateningPiece)
        {
            switch (threateningPiece)
            {
                case Knight _: // knights jump pieces, cannot move between
                    return false;
                case Pawn _: // pawns attack in an adjacent square, cannot move between
                    return false;
                case King _: // king will never be checking another king.
                    return false;
            }

            foreach (Piece activePlayerPiece in ActivePlayerPieces.Where(p => !(p is King)))
            {
                bool FilterPieces(ChessPosition p)
                {
                    activePlayerPiece.GenerateMoves(GameBoard.State);
                    return activePlayerPiece.CanMoveTo(p);
                }

                foreach (ChessPosition position in threateningPiece.ThreatenSet.Where(FilterPieces))
                {
                    var move = new Move
                    {
                        StartingPosition = activePlayerPiece.Location,
                        EndingPosition = position
                    };

                    var capture = new Capture
                    {
                        StartingPosition = move.StartingPosition,
                        EndingPosition = move.EndingPosition
                    };

                    if (!(IsMoveLegal(activePlayerPiece, move, GameBoard.State) || IsCaptureLegal(activePlayerPiece, capture, GameBoard.State)))
                        continue;

                    if (!DoesPotentialMoveLeaveKingInCheck(move))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Does the active player control a piece that can capture the piece threatening the king?
        /// </summary>
        private bool CanThreateningPieceBeCaptured(Piece threateningPiece)
        {
            foreach (Piece activePlayerPiece in ActivePlayerPieces)
            {
                activePlayerPiece.GenerateCaptures(GameBoard.State, ActivePlayerBoardState);
                bool canCaptureAt = activePlayerPiece.CanCaptureAt(threateningPiece.Location);

                if (!canCaptureAt)
                    continue;

                var capture = new Capture
                {
                    StartingPosition = activePlayerPiece.Location,
                    EndingPosition = threateningPiece.Location
                };
                bool isCaptureLegal = IsCaptureLegal(activePlayerPiece, capture, GameBoard.State);

                if (isCaptureLegal)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determine if the king can move out of check by capturing a piece or simply moving.
        /// </summary>
        private bool CanKingMoveOrCaptureOutOfCheck(King king, IBoardState gameBoardState)
        {
            bool canKingMoveOutOfCheck = false;
            bool canKingCaptureOutOfCheck = false;

            // 1.) Can the king move or capture out of check?

            foreach (ChessPosition position in king.ThreatenSet)
            {
                canKingMoveOutOfCheck |= IsMoveLegal(king, new Move { StartingPosition = king.Location, EndingPosition = position }, gameBoardState);
                canKingCaptureOutOfCheck |= IsCaptureLegal(king, new Capture { StartingPosition = king.Location, EndingPosition = position }, gameBoardState);
            }

            return canKingMoveOutOfCheck || canKingCaptureOutOfCheck;
        }

        /// <summary>
        /// Retrieves a Piece based on color and position.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private Piece GetPiece(ChessColor color, ChessPosition position)
        {
            List<Piece> pieces = (color == ChessColor.White) ? WhitePieces : BlackPieces;
            return pieces.Find(p => p.Location == position);
        }

        /// <summary>
        /// Increments the turn by 1/2.
        /// </summary>
        private void IncrementTurn() => Turn += 0.5;

        /// <summary>
        /// Determines if move is a legal attempt at En Passant.
        /// </summary>
        /// <param name="capturingPiece"></param>
        /// <param name="capture"></param>
        /// <param name="gameBoard"></param>
        /// <returns></returns>
        private bool IsCaptureLegalEnPassant(Piece capturingPiece, Capture capture, Board gameBoard)
        {
            // 1.) only Pawns can capture via En Passant
            if (!(capturingPiece is Pawn))
                return false;

            // 2.) white pawns must be on Rank 5, black pawns must be on Rank 4
            bool isCapturingPawnOnCorrectRank = capturingPiece.Color == ChessColor.White ?
                (capturingPiece.Location & ChessPosition.Rank5) == capturingPiece.Location :
                (capturingPiece.Location & ChessPosition.Rank4) == capturingPiece.Location;

            if (!isCapturingPawnOnCorrectRank)
                return false;

            // 3.) Pawn may not move to an occupied square
            if (gameBoard.IsPositionOccupied(capture.EndingPosition))
                return false;

            var cpm = new ChessPieceMover();
            // get the position of the piece we're trying to capture via En Passant
            ChessPosition locationOfPotentiallyCapturedPiece = capturingPiece.Color == ChessColor.White ?
                cpm.South(capture.EndingPosition) :
                cpm.North(capture.EndingPosition);

            // 4.) piece being captured must be a Pawn
            Piece pieceBeingCaptured = GetPiece(InactivePlayerColor, locationOfPotentiallyCapturedPiece);

            // 5.) pawn must be capturable by en passant
            return pieceBeingCaptured is Pawn && (pieceBeingCaptured as Pawn).IsCapturableByEnPassant;
        }

        /// <summary>
        /// Checks if the castle is legal by a process of six standard steps.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private bool IsCastleLegal(Piece piece, Move move, Board board)
        {
            // 1.) has the King moved already?
            if (!(piece is King) || piece.HasMoved)
                return false;

            Piece king = piece;

            Rook rook = GetCastlingRook(move, king.Color);
            // 2.) has the Rook moved already?
            if (rook == null || rook.HasMoved)
                return false;

            List<ChessPosition> piecesBetweenRookAndKing = GetPositionsBetweenCastle(king, rook, GameBoard);
            // 3.) are there pieces standing between the King and Rook?
            foreach (ChessPosition location in piecesBetweenRookAndKing)
            {
                if (board.IsPositionOccupied(location))
                    return false;
            }

            // 4.) is the King currently in check?
            if (IsPositionThreatened(king.Location, board, InactivePlayerBoardState))
                return false;

            // 5.) are any positions between King and Rook threatened?
            foreach (ChessPosition position in piecesBetweenRookAndKing)
            {
                if (IsPositionThreatened(position, board, InactivePlayerBoardState))
                    return false;
            }

            // 6.) will the King be in check after castling?
            return !IsPositionThreatened(move.EndingPosition, board, InactivePlayerBoardState);
        }

        /// <summary>
        /// Returns approriate Rook based on where the King is trying to move.
        /// </summary>
        /// <param name="move">King movement</param>
        /// <param name="color">King color</param>
        /// <returns></returns>
        private Rook GetCastlingRook(Move move, ChessColor color)
        {
            var rookPosition = ChessPosition.None;
            switch (move.EndingPosition)
            {
                case ChessPosition.C1:
                    rookPosition = ChessPosition.A1;
                    break;
                case ChessPosition.G1:
                    rookPosition = ChessPosition.H1;
                    break;
                case ChessPosition.C8:
                    rookPosition = ChessPosition.A8;
                    break;
                case ChessPosition.G8:
                    rookPosition = ChessPosition.H8;
                    break;
            }

            return rookPosition == ChessPosition.None ? null : (Rook)GetPiece(color, rookPosition);
        }

        /// <summary>
        /// Determine whether a position is threatened.
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <param name="board">Board to reference</param>
        /// <param name="inactivePlayerBoardState">Used to only check opponent's pieces</param>
        /// <returns></returns>
        private bool IsPositionThreatened(ChessPosition position, Board board, IBoardState inactivePlayerBoardState)
        {
            foreach (Piece enemyPiece in InactivePlayerPieces.Where(p => p.Location != ChessPosition.None))
            {
                enemyPiece.GenerateThreatened(board.State, inactivePlayerBoardState);
                if (enemyPiece.IsThreateningAt(position))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Is a move legal?
        /// </summary>
        /// <returns></returns>
        private bool IsMoveLegal(IMovable piece, Move move, IBoardState state)
        {
            piece.GenerateMoves(state);

            bool canPieceMove = piece.CanMoveTo(move.EndingPosition);
            bool doesMoveLeaveKingInCheck = DoesPotentialMoveLeaveKingInCheck(move);

            return canPieceMove && !doesMoveLeaveKingInCheck;
        }

        /// <summary>
        /// Is a capture legal?
        /// </summary>
        /// <returns></returns>
        private bool IsCaptureLegal(ICapturable piece, Capture capture, IBoardState state)
        {
            piece.GenerateCaptures(state, ActivePlayerBoardState);
            bool canPieceCapture = piece.CanCaptureAt(capture.EndingPosition);
            bool doesCaptureLeaveKingInCheck = DoesPotentialMoveLeaveKingInCheck(capture);

            return canPieceCapture && !doesCaptureLeaveKingInCheck;
        }

        /// <summary>
        /// Will the friendly King be in check if this move is made?
        /// </summary>
        /// <param name="potentialMove">Move to check</param>
        /// <returns></returns>
        private bool DoesPotentialMoveLeaveKingInCheck(Move potentialMove)
        {
            var game = new Game(() => typeof(Queen))
            {
                BlackPieces = new List<Piece>(this.BlackPieces),
                WhitePieces = new List<Piece>(this.WhitePieces),
                GameBoard = new Board(this.GameBoard.State, ModelLocator.BoardState),
                Turn = this.Turn
            };

            Piece king = game.ActivePlayerPieces.Find(p => p is King);
            bool isKingMovingCurrently = king.Location == potentialMove.StartingPosition;

            game.UpdateBoard(potentialMove);

            return game.InactivePlayerPieces.Any(p =>
            {
                p.GenerateCaptures(game.GameBoard.State, game.InactivePlayerBoardState);
                return p.CanCaptureAt(isKingMovingCurrently ? potentialMove.EndingPosition : king.Location);
            });
        }

        /// <summary>
        /// Executes En Passant capture. Does not check legality.
        /// </summary>
        /// <param name="capture"></param>
        /// <returns></returns>
        private int ExecuteEnPassantCapture(Capture capture)
        {
            var cpm = new ChessPieceMover();
            Piece movingPiece = GetPiece(ActivePlayerColor, capture.StartingPosition);

            ChessPosition locationOfLostPiece = movingPiece.Color == ChessColor.White ?
                cpm.South(capture.EndingPosition) :
                cpm.North(capture.EndingPosition);

            var lostPiece = GetPiece(InactivePlayerColor, locationOfLostPiece);

            UpdateBoardForEnPassant(movingPiece as Pawn, capture, lostPiece as Pawn);

            return lostPiece.Value;
        }

        /// <summary>
        /// Updates the board after En Passant. Does not check legality.
        /// </summary>
        /// <param name="attacker">Pawn that is passing</param>
        /// <param name="capture">Capture to make</param>
        /// <param name="lostPiece">Pawn being captured</param>
        private void UpdateBoardForEnPassant(Pawn attacker, Capture capture, Pawn lostPiece)
        {
            GameBoard.Remove(attacker.Location);
            attacker.MoveTo(capture.EndingPosition);
            GameBoard.Add(capture.EndingPosition);

            GameBoard.Remove(lostPiece.Location);
            lostPiece.Location = ChessPosition.None;

            AddToMoveHistory(attacker, capture);
        }

        /// <summary>
        /// Executes a normal capture. Does not check for legality.
        /// </summary>
        /// <param name="capture"></param>
        /// <returns>Point value of piece being captured</returns>
        private int ExecuteCapture(Capture capture)
        {
            Piece movingPiece = GetPiece(ActivePlayerColor, capture.StartingPosition);
            Piece lostPiece = GetPiece(InactivePlayerColor, capture.EndingPosition);

            movingPiece.MoveTo(capture.EndingPosition);
            lostPiece.Location = ChessPosition.None;

            UpdateBoard(capture);
            AddToMoveHistory(movingPiece, capture);

            return lostPiece.Value;
        }

        /// <summary>
        /// Adds a move to the Move History
        /// </summary>
        private void AddToMoveHistory(Piece piece, Move move)
        {
            MoveHistory.Add(piece, move);
        }

        /// <summary>
        /// Executes a move. Does not check legality.
        /// </summary>
        /// <param name="move"></param>
        private void ExecuteMove(Move move)
        {
            Piece movingPiece = GetPiece(ActivePlayerColor, move.StartingPosition);

            movingPiece.MoveTo(move.EndingPosition);

            if (movingPiece is Pawn pawn && pawn.IsPromotable)
                PromotePawn(movingPiece);

            UpdateBoard(move);

            AddToMoveHistory(movingPiece, move);
        }

        /// <summary>
        /// Replaces the pawn on the board with the desired piece as defined by _pawnPromotionFunc
        /// </summary>
        /// <param name="movingPiece"></param>
        private void PromotePawn(Piece movingPiece)
        {
            Type pieceType = _pawnPromotionFunc();

            bool isPieceTypeCorrect = pieceType == typeof(Queen)
                                      || pieceType == typeof(Rook)
                                      || pieceType == typeof(Knight)
                                      || pieceType == typeof(Bishop);

            if (!isPieceTypeCorrect)
                throw new PawnPromotionException(@"A pawn can only be promoted to a Queen, Knight, Rook or Bishop.");

            Piece newPiece = null;
            if (pieceType == typeof(Queen))
                newPiece = new Queen(movingPiece.Location, movingPiece.Color);
            if (pieceType == typeof(Knight))
                newPiece = new Knight(movingPiece.Location, movingPiece.Color);
            if (pieceType == typeof(Rook))
                newPiece = new Rook(movingPiece.Location, movingPiece.Color);
            if (pieceType == typeof(Bishop))
                newPiece = new Bishop(movingPiece.Location, movingPiece.Color);

            Debug.Assert(newPiece != null);

            newPiece.HasMoved = true;

            ActivePlayerPieces.Remove(movingPiece);
            ActivePlayerPieces.Add(newPiece);
        }

        /// <summary>
        /// Updates a board for the given move. Does not check legality.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private void ExecuteCastle(King piece, Move move)
        {
            Rook rook = GetCastlingRook(move, piece.Color);

            UpdateBoardForCastle(piece, move, rook);
        }

        private void UpdateBoard(Move move)
        {
            if (!GameBoard.IsPositionOccupied(move.StartingPosition)) return;

            if (move.IsCapture && !GameBoard.IsPositionOccupied(move.EndingPosition)) return;

            if (!move.IsCapture && GameBoard.IsPositionOccupied(move.EndingPosition)) return;

            GameBoard.Remove(move.StartingPosition);
            GameBoard.Add(move.EndingPosition);
        }

        /// <summary>
        /// Executes a Castle. Does not check legality.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="move"></param>
        /// <summary>
        /// Updates board for Castle. Does not check legality.
        /// </summary>
        /// <param name="king"></param>
        /// <param name="move"></param>
        /// <param name="rook"></param>
        private void UpdateBoardForCastle(King king, Move move, Rook rook)
        {
            ChessPosition newRookLocation = GetEndingPositionForCastlingRook(king, rook);

            GameBoard.Remove(king.Location); // remove King from board
            king.MoveTo(move.EndingPosition); // move King, update location
            GameBoard.Add(move.EndingPosition); // place King on board at update location

            GameBoard.Remove(rook.Location); // remove Rook from board
            rook.MoveTo(newRookLocation); // move Rook, update location
            GameBoard.Add(rook.Location); // place Rook on board at updated location            

            AddToMoveHistory(king, move);
        }

        /// <summary>
        /// Determines the position where a castling Rook will end at.
        /// </summary>
        /// <param name="king"></param>
        /// <param name="rook"></param>
        /// <returns></returns>
        private static ChessPosition GetEndingPositionForCastlingRook(King king, Rook rook)
        {
            ChessPosition position = rook.Location;

            switch (king.Location)
            {
                case ChessPosition.E1 when rook.Location == ChessPosition.A1:
                    position = ChessPosition.D1;
                    break;
                case ChessPosition.E1 when rook.Location == ChessPosition.H1:
                    position = ChessPosition.F1;
                    break;
                case ChessPosition.E8 when rook.Location == ChessPosition.A8:
                    position = ChessPosition.D8;
                    break;
                case ChessPosition.E8 when rook.Location == ChessPosition.H8:
                    position = ChessPosition.F8;
                    break;
            }

            return position;
        }

        /// <summary>
        /// Retrieves a list of positions that are between the castling Rook and King.
        /// </summary>
        /// <param name="king"></param>
        /// <param name="rook"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private static List<ChessPosition> GetPositionsBetweenCastle(Piece king, Piece rook, Board board)
        {
            var locationsInBetween = new List<ChessPosition>();

            // add all locations to check based on where the king and rook are located
            switch (king.Location)
            {
                case ChessPosition.E1 when rook.Location == ChessPosition.A1:
                    locationsInBetween.Add(ChessPosition.D1);
                    locationsInBetween.Add(ChessPosition.C1);
                    locationsInBetween.Add(ChessPosition.B1);
                    break;
                case ChessPosition.E1 when rook.Location == ChessPosition.H1:
                    locationsInBetween.Add(ChessPosition.F1);
                    locationsInBetween.Add(ChessPosition.G1);
                    break;
                case ChessPosition.E8 when rook.Location == ChessPosition.A8:
                    locationsInBetween.Add(ChessPosition.D8);
                    locationsInBetween.Add(ChessPosition.C8);
                    locationsInBetween.Add(ChessPosition.B8);
                    break;
                case ChessPosition.E8 when rook.Location == ChessPosition.H8:
                    locationsInBetween.Add(ChessPosition.F8);
                    locationsInBetween.Add(ChessPosition.G8);
                    break;
            }

            return locationsInBetween;
        }

        #endregion
    }
}