using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class Rook : Piece, IRook
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
            var cpm = ModelLocator.ChessPieceMover;
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
            var cpm = ModelLocator.ChessPieceMover;
            CaptureSet.Clear();
            var enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateNorthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateSouthCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateEastCaptures(owningPlayerBoardState, enemyBoardState, cpm);
            GenerateWestCaptures(owningPlayerBoardState, enemyBoardState, cpm);
        }


        #region Private Methods

        private void GenerateNorthMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.North);
        }
        private void GenerateSouthMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.South);
        }
        private void GenerateEastMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.East);
        }
        private void GenerateWestMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            GenerateDirectionalMoves(boardState, cpm.West);
        }

        private void GenerateNorthCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.North);
        }
        private void GenerateSouthCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.South);
        }
        private void GenerateEastCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.East);
        }
        private void GenerateWestCaptures(IBoardState owningPlayerBoardState, IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            GenerateDirectionalCaptures(owningPlayerBoardState, enemyBoardState, cpm.West);
        }

        #endregion
    }
}
