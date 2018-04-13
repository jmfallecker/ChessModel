using StandardChess.Infrastructure;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.PieceModel
{
    public class Knight : Piece
    {
        public Knight(ChessPosition initialPosition, ChessColor color)
            : base(initialPosition, color)
        {
            Value = 3;
        }

        /// <summary>
        /// Generates all legal <see cref="Knight"/> moves
        /// </summary>
        /// <param name="boardState"></param>
        public override void GenerateMoves(BoardState boardState)
        {
            var cpm = new ChessPieceMover();
            MoveSet.Clear();

            GenerateNorthNorthEastMove(boardState, cpm);
            GenerateNorthNorthWestMove(boardState, cpm);

            GenerateSouthSouthEastMove(boardState, cpm);
            GenerateSouthSouthWestMove(boardState, cpm);

            GenerateEastNorthEastMove(boardState, cpm);
            GenerateEastSouthEastMove(boardState, cpm);

            GenerateWestNorthWestMove(boardState, cpm);
            GenerateWestSouthWestMove(boardState, cpm);
        }
        /// <summary>
        /// Generates all legal <see cref="Knight"/> captures
        /// </summary>
        /// <param name="boardState"></param>
        /// <param name="owningPlayerBoardState"></param>
        public override void GenerateCaptures(BoardState boardState, BoardState owningPlayerBoardState)
        {
            var cpm = new ChessPieceMover();
            CaptureSet.Clear();
            var enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

            GenerateEastNorthEastCapture(enemyBoardState, cpm);
            GenerateEastSouthEastCapture(enemyBoardState, cpm);
            GenerateNorthNorthEastCapture(enemyBoardState, cpm);
            GenerateNorthNorthWestCapture(enemyBoardState, cpm);
            GenerateSouthSouthEastCapture(enemyBoardState, cpm);
            GenerateSouthSouthWestCapture(enemyBoardState, cpm);
            GenerateWestNorthWestCapture(enemyBoardState, cpm);
            GenerateWestSouthWestCapture(enemyBoardState, cpm);
        }

        #region Private Methods
        private void GenerateNorthNorthEastMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.North(cpm.NorthEast(Location));

            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateNorthNorthWestMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.North(cpm.NorthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateEastNorthEastMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.East(cpm.NorthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateEastSouthEastMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.East(cpm.SouthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateWestNorthWestMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.West(cpm.NorthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateWestSouthWestMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.West(cpm.SouthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateSouthSouthEastMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.South(cpm.SouthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateSouthSouthWestMove(BoardState boardState, ChessPieceMover cpm)
        {
            var move = cpm.South(cpm.SouthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateNorthNorthEastCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.North(cpm.NorthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateNorthNorthWestCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.North(cpm.NorthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateSouthSouthEastCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.South(cpm.SouthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateSouthSouthWestCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.South(cpm.SouthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateEastNorthEastCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.East(cpm.NorthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateEastSouthEastCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.East(cpm.SouthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateWestNorthWestCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.West(cpm.NorthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateWestSouthWestCapture(BoardState enemyBoardState, ChessPieceMover cpm)
        {
            var capture = cpm.West(cpm.SouthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        #endregion
    }
}
