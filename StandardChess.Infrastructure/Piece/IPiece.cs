using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Infrastructure.Piece
{
    public interface IPiece
    {
        ChessPosition Location { get; set; }
        bool HasMoved { get; set; }
        ChessColor Color { get; }
        IBoardState MoveSet { get; }
        IBoardState CaptureSet { get; }
        IBoardState ThreatenSet { get; }
        int Value { get; }

        /// <summary>
        ///     This function generates all legal moves, but not all legal captures.
        /// </summary>
        /// <param name="boardState"></param>
        void GenerateMoves(IBoardState boardState);

        /// <summary>
        ///     This function simply generates all potential capture locations.
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState);

        /// <summary>
        ///     Ensure that <see cref="Piece.GenerateMoves" /> is called before this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool CanMoveTo(ChessPosition position);

        /// <summary>
        ///     Moves a piece without considering legality of move.
        /// </summary>
        /// <param name="position"></param>
        void MoveTo(ChessPosition position);

        /// <summary>
        ///     Ensure that <see cref="Piece.GenerateCaptures" /> is called before this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool CanCaptureAt(ChessPosition position);

        /// <summary>
        ///     Moves a piece without considering legality of move.
        /// </summary>
        /// <param name="position"></param>
        void CaptureAt(ChessPosition position);

        /// <summary>
        ///     Ensure that <see cref="Piece.GenerateThreatened" /> is called before this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsThreateningAt(ChessPosition position);

        /// <summary>
        ///     Generates all squares that are threatened by this piece for the given boardstate.
        /// </summary>
        /// <param name="boardState">Full board state</param>
        /// <param name="owningPlayerBoardState">Used to ignore this other pieces of the same color.</param>
        void GenerateThreatened(IBoardState boardState, IBoardState owningPlayerBoardState);
    }
}