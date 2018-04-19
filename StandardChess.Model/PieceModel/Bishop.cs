using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class Bishop : Piece, IBishop
    {
        public Bishop(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 3;
        }

        /// <summary>
        /// Generates all legal <see cref="IBishop"/> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(IBoardState boardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            MoveSet.Clear();

            GenerateNorthEastMoves(boardState, cpm);
            GenerateNorthWestMoves(boardState, cpm);
            GenerateSouthEastMoves(boardState, cpm);
            GenerateSouthWestMoves(boardState, cpm);
        }

        /// <summary>
        /// Generates all legal <see cref="IBishop"/> captures
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            CaptureSet.Clear();
            IBoardState enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateSouthWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateSouthEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateNorthEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateNorthWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
        }

        private void GenerateNorthWestCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.NorthWest);
        }

        private void GenerateNorthEastCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.NorthEast);
        }

        private void GenerateSouthEastCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.SouthEast);
        }

        private void GenerateSouthWestCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.SouthWest);
        }

        #region Private Methods
        private void GenerateSouthWestMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.SouthWest);
        }
        private void GenerateSouthEastMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.SouthEast);
        }
        private void GenerateNorthWestMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.NorthWest);
        }
        private void GenerateNorthEastMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.NorthEast);
        }

        #endregion
    }
}
