using StandardChess.Infrastructure;

namespace StandardChess.Model.ChessUtility
{
    public class ChessPieceMover
    {
        #region Fields
        /// <summary>
        /// The representation of all files B-H excluding file A.
        /// </summary>
        private readonly ulong _notAFile = 0xfefefefefefefefe;

        /// <summary>
        /// The representation of all files A-G excluding file H.
        /// </summary>
        private readonly ulong _notHFile = 0x7f7f7f7f7f7f7f7f;
        #endregion

        #region Public Methods

        /// <summary>
        /// Returns position one square north of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition North(ChessPosition originalPosition)
        {
            return (ChessPosition)((ulong)originalPosition << 8);
        }

        /// <summary>
        /// Returns position one square south of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition South(ChessPosition originalPosition)
        {
            return (ChessPosition)((ulong)originalPosition >> 8);
        }

        /// <summary>
        /// Returns position one square east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition East(ChessPosition originalPosition)
        {
            return (ChessPosition)(((ulong)originalPosition << 1) & _notAFile);
        }

        /// <summary>
        /// Returns position one square west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition West(ChessPosition originalPosition)
        {
            return (ChessPosition)(((ulong)originalPosition >> 1) & _notHFile);
        }

        /// <summary>
        /// Returns position one square north-east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition NorthEast(ChessPosition originalPosition)
        {
            return (ChessPosition)(((ulong)originalPosition << 9) & _notAFile);
        }

        /// <summary>
        /// Returns position one square south-east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition SouthEast(ChessPosition originalPosition)
        {
            return (ChessPosition)(((ulong)originalPosition >> 7) & _notAFile);
        }

        /// <summary>
        /// Returns position one square north-west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition NorthWest(ChessPosition originalPosition)
        {
            return (ChessPosition)(((ulong)originalPosition << 7) & _notHFile);
        }

        /// <summary>
        /// Returns position one square south-west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition SouthWest(ChessPosition originalPosition)
        {
            return (ChessPosition)(((ulong)originalPosition >> 9) & _notHFile);
        }
        #endregion
    }
}
