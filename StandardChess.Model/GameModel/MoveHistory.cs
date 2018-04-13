using System;
using System.Collections.Generic;
using System.Linq;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;

namespace StandardChess.Model.GameModel
{
    public class MoveHistory
    {
        private static readonly Dictionary<(Type, ChessColor), string> UnicodeCharacters = new Dictionary<(Type, ChessColor), string>
        {
            { (typeof(King), ChessColor.White), "\u2654" },
            { (typeof(Queen), ChessColor.White), "\u2655" },
            { (typeof(Rook), ChessColor.White), "\u2656" },
            { (typeof(Bishop), ChessColor.White), "\u2657" },
            { (typeof(Knight), ChessColor.White), "\u2658" },
            { (typeof(Pawn), ChessColor.White), "\u2659" },
            { (typeof(King), ChessColor.Black), "\u265A" },
            { (typeof(Queen), ChessColor.Black), "\u265B" },
            { (typeof(Rook), ChessColor.Black), "\u265C" },
            { (typeof(Bishop), ChessColor.Black), "\u265D" },
            { (typeof(Knight), ChessColor.Black), "\u265E" },
            { (typeof(Pawn), ChessColor.Black), "\u265F" }
        };

        public List<(Move move, Type pieceType)> Moves { get; }
        public List<string> MovesByNotation { get; }
        public int Count => Moves.Count;

        public MoveHistory()
        {
            Moves = new List<(Move, Type)>();
            MovesByNotation = new List<string>();
        }

        public void Add(Piece piece, Move move)
        {
            Moves.Add((move, piece.GetType()));

            if (piece.Color == ChessColor.White)
            {
                MovesByNotation.Add(CreateAlgebraicNotation(piece, move));
            }
            else
            {
                string incompleteTurn = MovesByNotation.Last();
                incompleteTurn += " " + CreateAlgebraicNotation(piece, move);
            }
        }

        public void RemoveLast()
        {
            
        }
                
        private static string CreateAlgebraicNotation(Piece piece, Move move)
        {
            return GetChessPieceUnicode(piece) + move.EndingPosition.ToString();
        }        

        private static string GetChessPieceUnicode(Piece piece)
        {
            return UnicodeCharacters[(piece.GetType(), piece.Color)];
        }

        public bool WasPieceCapturedInLastFiftyMoves()
        {
            if (Moves.Count == 1)
                return Moves.First().move is Capture;

            int mostRecentMoveIndex = Moves.Count - 1;
            int indexToEndAt = Moves.Count < 50 ? 0 : mostRecentMoveIndex - 50;

            for (int i = mostRecentMoveIndex; i > indexToEndAt; i--)
            {
                if (Moves[i].move is Capture)
                    return true;
            }

            return false;
        }

        public bool WasPawnMovedInLastFiftyMoves()
        {
            if (Moves.Count == 1)
                return Moves.First().pieceType == typeof(Pawn);

            int mostRecentMoveIndex = Moves.Count - 1;
            int indexToEndAt = Moves.Count < 50 ? 0 : mostRecentMoveIndex - 50;

            for (int i = mostRecentMoveIndex; i > indexToEndAt; i--)
            {
                if (Moves[i].pieceType == typeof(Pawn))
                    return true;
            }

            return false;
        }
    }
}
