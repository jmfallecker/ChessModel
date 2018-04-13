using StandardChess.Infrastructure;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class Rook : Piece
    {
        public Rook(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 5;
        }

        /// <summary>
        /// Generates all legal <see cref="Rook"/> moves
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
        }

        /// <summary>
        /// Generates all legal <see cref="Rook"/> captures
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(BoardState boardState, BoardState owningPlayerBoardState)
        {
            var cpm = new ChessPieceMover();
            CaptureSet.Clear();
            var enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateNorthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateSouthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
        }


        #region Private Methods

        private void GenerateNorthMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.North(p));
        }
        private void GenerateSouthMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.South(p));
        }
        private void GenerateEastMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.East(p));
        }
        private void GenerateWestMoves(BoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.West(p));
        }

        private void GenerateNorthCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.North(p));
        }
        private void GenerateSouthCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.South(p));
        }
        private void GenerateEastCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.East(p));
        }
        private void GenerateWestCaptures(BoardState owningPlayerBoardState, BoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.West(p));
        }

        #endregion
    }
}
