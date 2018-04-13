using StandardChess.Infrastructure;
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

        /// <summary>
        /// Does not consider if the <see cref="King"/> will be in check afterwards
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(BoardState boardState, BoardState owningPlayerBoardState)
        {
            var cpm = new ChessPieceMover();
            var enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

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

        private void GenerateNorthMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.North(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateSouthMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.South(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateEastMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.East(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateWestMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.West(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateSouthWestMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.SouthWest(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateSouthEastMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.SouthEast(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateNorthWestMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.NorthWest(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }

        private void GenerateNorthEastMoves(BoardState boardState, ChessPieceMover cpm)
        {
            var nextMove = cpm.NorthEast(Location);
            if (!boardState.Contains(nextMove))
                MoveSet.Add(nextMove);
        }


        private void GenerateNorthCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.North(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.South(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateEastCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.East(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateWestCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.West(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthWestCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.SouthWest(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateSouthEastCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.SouthEast(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateNorthWestCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.NorthWest(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        private void GenerateNorthEastCaptures(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.NorthEast(Location);
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        #endregion
    }
}
