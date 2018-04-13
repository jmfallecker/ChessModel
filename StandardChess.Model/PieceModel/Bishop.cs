using StandardChess.Infrastructure;
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
        public override void GenerateMoves(BoardState boardState)
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
        public override void GenerateCaptures(BoardState boardState, BoardState owningPlayerBoardState)
        {
            var cpm = new ChessPieceMover();
            CaptureSet.Clear();
            var enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateSouthWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateSouthEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateNorthEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateNorthWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
        }

        private void GenerateNorthWestCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.NorthWest(p));
        }

        private void GenerateNorthEastCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.NorthEast(p));
        }

        private void GenerateSouthEastCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.SouthEast(p));
        }

        private void GenerateSouthWestCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.SouthWest(p));
        }

        #region Private Methods
        private void GenerateSouthWestMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.SouthWest(p));
        }
        private void GenerateSouthEastMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.SouthEast(p));
        }
        private void GenerateNorthWestMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.NorthWest(p));
        }
        private void GenerateNorthEastMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.NorthEast(p));
        }

        #endregion
    }
}
