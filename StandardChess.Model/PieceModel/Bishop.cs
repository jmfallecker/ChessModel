using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class Bishop : Piece
    {
        public Bishop(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 3;
        }

        /// <summary>
        /// Generates all legal <see cref="Bishop"/> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(IBoardState boardState)
        {
            var cpm = new ChessPieceMover();
            MoveSet.Clear();

            GenerateNorthEastMoves(boardState, cpm);
            GenerateNorthWestMoves(boardState, cpm);
            GenerateSouthEastMoves(boardState, cpm);
            GenerateSouthWestMoves(boardState, cpm);
        }

        /// <summary>
        /// Generates all legal <see cref="Bishop"/> captures
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            var cpm = new ChessPieceMover();
            CaptureSet.Clear();
            var enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateSouthWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateSouthEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateNorthEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateNorthWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
        }

        private void GenerateNorthWestCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.NorthWest(p));
        }

        private void GenerateNorthEastCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.NorthEast(p));
        }

        private void GenerateSouthEastCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.SouthEast(p));
        }

        private void GenerateSouthWestCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.SouthWest(p));
        }

        #region Private Methods
        private void GenerateSouthWestMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.SouthWest(p));
        }
        private void GenerateSouthEastMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.SouthEast(p));
        }
        private void GenerateNorthWestMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.NorthWest(p));
        }
        private void GenerateNorthEastMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.NorthEast(p));
        }

        #endregion
    }
}
