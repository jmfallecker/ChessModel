using StandardChess.Infrastructure;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.BoardModel
{
    public class Board
    {
        #region Properties

        public BoardState State { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a board with starting positions.
        /// </summary>
        public Board()
        {
            State = new BoardState();

            State.Add(ChessPosition.WhiteStart);
            State.Add(ChessPosition.BlackStart);
        }

        /// <summary>
        /// Creates a board with passed in player boardstates.
        /// </summary>
        /// <param name="whitePlayerPieces"></param>
        /// <param name="blackPlayerPieces"></param>
        public Board(BoardState whitePlayerPieces, BoardState blackPlayerPieces)
        {
            State = new BoardState();
            State.Add(whitePlayerPieces);
            State.Add(blackPlayerPieces);
        }

        #endregion

        #region Public Methods

        public bool IsPositionOccupied(ChessPosition position)
        {
            return State.IsPositionOccupied(position);
        }

        public bool Add(ChessPosition position)
        {
            return State.Add(position);
        }

        public bool Remove(ChessPosition position)
        {
            return State.Remove(position);
        }

        #endregion
    }
}
