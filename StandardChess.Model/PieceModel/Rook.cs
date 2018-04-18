using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
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
        public override void GenerateMoves(IBoardState boardState)
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
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
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

        private void GenerateNorthMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.North(p));
        }
        private void GenerateSouthMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.South(p));
        }
        private void GenerateEastMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.East(p));
        }
        private void GenerateWestMoves(IBoardState boardState, ChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, p => cpm.West(p));
        }

        private void GenerateNorthCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.North(p));
        }
        private void GenerateSouthCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.South(p));
        }
        private void GenerateEastCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.East(p));
        }
        private void GenerateWestCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, ChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, p => cpm.West(p));
        }

        #endregion
    }
}
