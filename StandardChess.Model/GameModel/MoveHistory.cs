using System;
using System.Collections.Generic;
using System.Linq;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model.PieceModel;

namespace StandardChess.Model.GameModel
{
    public class MoveHistory
    {
        private static readonly Dictionary<(Type, ChessColor), string> UnicodeCharacters =
            new Dictionary<(Type, ChessColor), string>
            {
                {(typeof(King), ChessColor.White), "\u2654"},
                {(typeof(Queen), ChessColor.White), "\u2655"},
                {(typeof(Rook), ChessColor.White), "\u2656"},
                {(typeof(Bishop), ChessColor.White), "\u2657"},
                {(typeof(Knight), ChessColor.White), "\u2658"},
                {(typeof(Pawn), ChessColor.White), "\u2659"},
                {(typeof(King), ChessColor.Black), "\u265A"},
                {(typeof(Queen), ChessColor.Black), "\u265B"},
                {(typeof(Rook), ChessColor.Black), "\u265C"},
                {(typeof(Bishop), ChessColor.Black), "\u265D"},
                {(typeof(Knight), ChessColor.Black), "\u265E"},
                {(typeof(Pawn), ChessColor.Black), "\u265F"}
            };

        public MoveHistory()
        {
            Moves = new List<(IMovable, IPiece)>();
            MovesByNotation = new List<string>();
        }

        public IList<(IMovable movable, IPiece piece)> Moves { get; }
        public IList<string> MovesByNotation { get; }

        public int Count => Moves.Count;

        public bool WasPieceCapturedInLastFiftyMoves
        {
            get
            {
                if (Moves.Count == 1)
                    return Moves.First().movable is ICapture;

                int mostRecentMoveIndex = Moves.Count - 1;
                int indexToEndAt = Moves.Count < 50 ? 0 : mostRecentMoveIndex - 50;

                for (int i = mostRecentMoveIndex; i > indexToEndAt; i--)
                    if (Moves[i].movable is ICapture)
                        return true;

                return false;
            }
        }

        public bool WasPawnMovedInLastFiftyMoves
        {
            get
            {
                if (Moves.Count == 1)
                    return Moves.First().piece is IPawn;

                int mostRecentMoveIndex = Moves.Count - 1;
                int indexToEndAt = Moves.Count < 50 ? 0 : mostRecentMoveIndex - 50;

                for (int i = mostRecentMoveIndex; i > indexToEndAt; i--)
                    if (Moves[i].piece is IPawn)
                        return true;

                return false;
            }
        }

        public void Add(IPiece piece, IMovable movable)
        {
            Moves.Add((movable, piece));
            AddNotation(piece, movable);
        }

        private void AddNotation(IPiece piece, IMovable movable)
        {
            if (piece.Color == ChessColor.White)
            {
                int turnNumber = MovesByNotation.Count + 1;
                MovesByNotation.Add($"{turnNumber}. {CreateAlgebraicNotation(piece, movable)}");
            }
            else
            {
                string incompleteTurn = MovesByNotation.Last();
                string completeTurn = incompleteTurn + " " + CreateAlgebraicNotation(piece, movable);

                int indexOfIncompleteTurn = MovesByNotation.IndexOf(incompleteTurn);
                MovesByNotation[indexOfIncompleteTurn] = completeTurn;
            }
        }

        private static string CreateAlgebraicNotation(IPiece piece, IMovable move)
        {
            string notation = GetChessPieceUnicode(piece);

            if (move is ICapture)
                notation += "x";


            return notation + move.EndingPosition;
        }

        private static string GetChessPieceUnicode(IPiece piece)
        {
            Type type = piece.GetType();
            return UnicodeCharacters[(type, piece.Color)];
        }
    }
}