using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Model.BoardModel
{
    public class Board : IBoard
    {
        #region Properties

        public IBoardState State { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a board with starting positions.
        /// </summary>
        public Board()
        {
            State = ModelLocator.BoardState;

            State.Add(ChessPosition.WhiteStart);
            State.Add(ChessPosition.BlackStart);
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
