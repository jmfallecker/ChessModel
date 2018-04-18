using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class King : Piece
    {
        public King(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 1000;
        }

        /// <summary>
        /// Does not consider if the <see cref="King"/> will be in check afterwards
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(IBoardState boardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
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

        /// <summary>
        /// Does not consider if the <see cref="King"/> will be in check afterwards
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            IBoardState enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            CaptureSet.Clear();

            GenerateEastCaptures(enemyBoardState, cpm);
            GenerateNorthCaptures(enemyBoardState, cpm);
            GenerateSouthCaptures(enemyBoardState, cpm);
            GenerateWestCaptures(enemyBoardState, cpm);

            GenerateNorthWestCaptures(enemyBoardState, cpm);
            GenerateNorthEastCaptures(enemyBoardState, cpm);
            GenerateSouthWestCaptures(enemyBoardState, cpm);
            GenerateSouthEastCaptures(enemyBoardState, cpm);
        }

        #region Private Methods

        private void GenerateNorthMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.North(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateSouthMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.South(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateEastMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.East(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateWestMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.West(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateSouthWestMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.SouthWest(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateSouthEastMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.SouthEast(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateNorthWestMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.NorthWest(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateNorthEastMoves(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition nextMove = cpm.NorthEast(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }


        private void GenerateNorthCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.North(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.South(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateEastCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.East(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateWestCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.West(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthWestCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.SouthWest(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthEastCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.SouthEast(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateNorthWestCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.NorthWest(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateNorthEastCaptures(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.NorthEast(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        #endregion
    }
}
