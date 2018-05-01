namespace StandardChess.Infrastructure.Utility
{
    public interface IChessPieceMover
    {
        /// <summary>
        ///     Returns position one square north of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition North(ChessPosition originalPosition);

        /// <summary>
        ///     Returns position one square south of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition South(ChessPosition originalPosition);

        /// <summary>
        ///     Returns position one square east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition East(ChessPosition originalPosition);

        /// <summary>
        ///     Returns position one square west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition West(ChessPosition originalPosition);

        /// <summary>
        ///     Returns position one square north-east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition NorthEast(ChessPosition originalPosition);

        /// <summary>
        ///     Returns position one square south-east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition SouthEast(ChessPosition originalPosition);

        /// <summary>
        ///     Returns position one square north-west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition NorthWest(ChessPosition originalPosition);

        /// <summary>
        ///     Returns position one square south-west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        ChessPosition SouthWest(ChessPosition originalPosition);
    }
}