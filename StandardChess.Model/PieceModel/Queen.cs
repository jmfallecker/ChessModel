using StandardChess.Infrastructure;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class Queen : Piece
    {
        public Queen(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 9;
        }

        /// <summary>
        /// Generates all legal <see cref="Queen"/> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(BoardState boardState)
        {
            var cpm = new ChessPieceMover();
            MoveSet.Clear();

            GenerateNorthMoves(boardState, cpm);
            GenerateSouthMoves(boardState, cpm);
            GenerateEastMoves(boardState, cpm);
            GenerateWestMoves(boardState, cpm);

            GenerateNorthEastMoves(boardState, cpm);
            GenerateNorthWestMoves(boardState, cpm);
            GenerateSouthEastMoves(boardState, cpm);
            GenerateSouthWestMoves(boardState, cpm);
        }

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

        private void GenerateWestMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.West(p));
        }

        private void GenerateEastMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.East(p)); ;
        }

        private void GenerateSouthMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.South(p));
        }

        private void GenerateNorthMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.North(p));
        }

        /// <summary>
        /// Generates all legal <see cref="Queen"/> captures
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

            GenerateNorthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateSouthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
        }

        private void GenerateWestCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.West(p));
        }

        private void GenerateEastCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.East(p));
        }

        private void GenerateSouthCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.South(p));
        }

        private void GenerateNorthCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.North(p));
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



        #endregion

    }
}
