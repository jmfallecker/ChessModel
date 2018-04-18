using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Utility;
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
        public override void GenerateMoves(IBoardState boardState)
        {
            var cpm = ModelLocator.ChessPieceMover;
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
        public override void GenerateCaptures(IBoardState boardState, IBoardState owningPlayerBoardState)
        {
            IChessPieceMover cpm = ModelLocator.ChessPieceMover;
            CaptureSet.Clear();
            IBoardState enemyBoardState = CreateEnemyBoardState(boardState, owningPlayerBoardState);

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
        private void GenerateNorthNorthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.North(cpm.NorthEast(Location));

            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateNorthNorthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.North(cpm.NorthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateEastNorthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.East(cpm.NorthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateEastSouthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.East(cpm.SouthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateWestNorthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.West(cpm.NorthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateWestSouthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.West(cpm.SouthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateSouthSouthEastMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.South(cpm.SouthEast(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }
        private void GenerateSouthSouthWestMove(IBoardState boardState, IChessPieceMover cpm)
        {
            ChessPosition move = cpm.South(cpm.SouthWest(Location));
            if (!boardState.Contains(move))
                MoveSet.Add(move);
        }

        private void GenerateNorthNorthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.North(cpm.NorthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateNorthNorthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.North(cpm.NorthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateSouthSouthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.South(cpm.SouthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateSouthSouthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.South(cpm.SouthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateEastNorthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.East(cpm.NorthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateEastSouthEastCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.East(cpm.SouthEast(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateWestNorthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.West(cpm.NorthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }
        private void GenerateWestSouthWestCapture(IBoardState enemyBoardState, IChessPieceMover cpm)
        {
            ChessPosition capture = cpm.West(cpm.SouthWest(Location));
            AddCaptureToCaptureSet(capture, enemyBoardState);
        }

        #endregion
    }
}
