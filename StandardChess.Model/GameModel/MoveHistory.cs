using System;
using System.Collections.Generic;
using System.Linq;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;

namespace StandardChess.Model.GameModel
{
    public class MoveHistory
    {
        private static readonly Dictionary<(Type, ChessColor), string> UnicodeCharacters = new Dictionary<(Type, ChessColor), string>
        {
            { (typeof(IKing), ChessColor.White), "\u2654" },
            { (typeof(IQueen), ChessColor.White), "\u2655" },
            { (typeof(IRook), ChessColor.White), "\u2656" },
            { (typeof(IBishop), ChessColor.White), "\u2657" },
            { (typeof(IKnight), ChessColor.White), "\u2658" },
            { (typeof(IPawn), ChessColor.White), "\u2659" },
            { (typeof(IKing), ChessColor.Black), "\u265A" },
            { (typeof(IQueen), ChessColor.Black), "\u265B" },
            { (typeof(IRook), ChessColor.Black), "\u265C" },
            { (typeof(IBishop), ChessColor.Black), "\u265D" },
            { (typeof(IKnight), ChessColor.Black), "\u265E" },
            { (typeof(IPawn), ChessColor.Black), "\u265F" }
        };

        public IList<(IMovable movable, Type pieceType)> Moves { get; }
        public IList<string> MovesByNotation { get; }
        public int Count => Moves.Count;

        public MoveHistory()
        {
            Moves = new List<(IMovable, Type)>();
            MovesByNotation = new List<string>();
        }

        public void Add(IPiece piece, IMovable movable)
        {
            Moves.Add((movable, piece.GetType()));

            if (piece.Color == ChessColor.White)
            {
                MovesByNotation.Add(CreateAlgebraicNotation(piece, movable));
            }
            else
            {
                string incompleteTurn = MovesByNotation.Last();
                incompleteTurn += " " + CreateAlgebraicNotation(piece, movable);
            }
        }

        public void RemoveLast()
        {
            
        }
                
        private static string CreateAlgebraicNotation(IPiece piece, IMovable move)
        {
            return GetChessPieceUnicode(piece) + move.EndingPosition;
        }        

        private static string GetChessPieceUnicode(IPiece piece)
        {
            return UnicodeCharacters[(piece.GetType(), piece.Color)];
        }

        public bool WasPieceCapturedInLastFiftyMoves()
        {
            if (Moves.Count == 1)
                return Moves.First().movable is ICapture;

            int mostRecentMoveIndex = Moves.Count - 1;
            int indexToEndAt = Moves.Count < 50 ? 0 : mostRecentMoveIndex - 50;

            for (int i = mostRecentMoveIndex; i > indexToEndAt; i--)
            {
                if (Moves[i].movable is ICapture)
                    return true;
            }

            return false;
        }

        public bool WasPawnMovedInLastFiftyMoves()
        {
            if (Moves.Count == 1)
                return Moves.First().pieceType == typeof(IPawn);

            int mostRecentMoveIndex = Moves.Count - 1;
            int indexToEndAt = Moves.Count < 50 ? 0 : mostRecentMoveIndex - 50;

            for (int i = mostRecentMoveIndex; i > indexToEndAt; i--)
            {
                if (Moves[i].pieceType == typeof(IPawn))
                    return true;
            }

            return false;
        }
    }
}
