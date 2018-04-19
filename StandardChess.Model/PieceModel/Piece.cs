using System;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public abstract class Piece : IPiece
    {
        #region Properties

        public ChessPosition Location { get; set; }

        public bool HasMoved { get; set; }

        public ChessColor Color { get; }

        public IBoardState MoveSet { get; protected set; }

        public IBoardState CaptureSet { get; protected set; }

        public IBoardState ThreatenSet { get; protected set; }

        public int Value { get; protected set; }

        #endregion

        #region Constructor

        protected Piece(ChessPosition initialPosition, ChessColor color)
        {
            Location = initialPosition;
            Color = color;
            MoveSet = ModelLocator.BoardState;
            CaptureSet = ModelLocator.BoardState;
            ThreatenSet = ModelLocator.BoardState;
        }

        #endregion

        #region Abstract Methods        

        /// <summary>
        /// This function generates all legal moves, but not all legal captures.
        /// </summary>
        /// <param name="boardState"></param>
        public abstract void GenerateMoves(IBoardState boardState);

        /// <summary>
        /// This function simply generates all potential capture locations.
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public abstract void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState);

        #endregion

        #region Public Methods

        /// <summary>
        /// Ensure that <see cref="GenerateMoves(IBoardState)"/> is called before this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool CanMoveTo(ChessPosition position)
        {
            return MoveSet.Contains(position);
        }

        /// <summary>
        /// Moves a piece without considering legality of move.
        /// </summary>
        /// <param name="position"></param>
        public virtual void MoveTo(ChessPosition position)
        {
            Location = position;
            if (!HasMoved) HasMoved = true;
        }

        /// <summary>
        /// Ensure that <see cref="GenerateCaptures(IBoardState, IBoardState)"/> is called before this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool CanCaptureAt(ChessPosition position)
        {
            return CaptureSet.Contains(position);
        }

        /// <summary>
        /// Moves a piece without considering legality of move.
        /// </summary>
        /// <param name="position"></param>
        public void CaptureAt(ChessPosition position)
        {
            Location = position;
            if (!HasMoved) HasMoved = true;
        }

        /// <summary>
        /// Ensure that <see cref="GenerateThreatened(IBoardState, IBoardState)"/> is called before this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsThreateningAt(ChessPosition position)
        {
            return ThreatenSet.Contains(position);
        }

        /// <summary>
        /// Generates all squares that are threatened by this piece for the given boardstate.
        /// </summary>
        /// <param name="boardState">Full board state</param>
        /// <param name="owningPlayerBoardState">Used to ignore this other pieces of the same color.</param>
        public virtual void GenerateThreatened(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            ThreatenSet.Clear();

            GenerateMoves(boardState);
            GenerateCaptures(boardState, owningPlayerBoardState);

            ThreatenSet.Add(MoveSet);
            ThreatenSet.Add(CaptureSet);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates the state of the enemy board.
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        /// <returns></returns>
        protected IBoardState CreateEnemyBoardState(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IBoardState state = ModelLocator.BoardState;
            state.Add(boardState);
            state.Remove(owningPlayerBoardState);

            return state;
        }

        /// <summary>
        /// Adds a capture to the capture set.
        /// </summary>
        /// <param name="capturePosition"></param>
        /// <param name="enemyBoardState"></param>
        protected void AddCaptureToCaptureSet(ChessPosition capturePosition, IBoardState enemyBoardState)
        {
            if (enemyBoardState.Contains(capturePosition))
            {
                CaptureSet.Add(capturePosition);
            }
        }

        /// <summary>
        /// Should only be used by Bishop, Rook and Queen
        /// </summary>
        /// <param name="owningPlayerBoardState"></param>
        /// <param name="enemyBoardState"></param>
        /// <param name="directionFunction"></param>
        protected void GenerateDirectionalCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, Func<ChessPosition, ChessPosition> directionFunction)
        {
            var capture = ChessPosition.None;

            if (!owningPlayerBoardState.Contains(directionFunction(Location)))
            {
                capture = directionFunction(Location);

                while (!enemyBoardState.Contains(capture) && !owningPlayerBoardState.Contains(capture))
                {
                    capture = directionFunction(capture);
                }
            }

            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        protected void GenerateDirectionalMoves(IBoardState boardState, Func<ChessPosition, ChessPosition> directionFunction)
        {
            ChessPosition nextMove = directionFunction(Location);

            while (!boardState.Contains(nextMove))
            {
                MoveSet.Add(nextMove);
                nextMove = directionFunction(nextMove);
            }
        }

        #endregion
    }
}
