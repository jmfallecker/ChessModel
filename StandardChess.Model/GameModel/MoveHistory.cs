// .NET Standard 2.0 Chess Model
// Copyright(C) 2018 Joseph M Fallecker

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<https: //www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Utility;

namespace StandardChess.Model.GameModel
{
    public class MoveHistory : IMoveHistory
    {
        private static readonly Dictionary<(Type type, ChessColor color), string> UnicodeCharacters =
            new Dictionary<(Type, ChessColor), string>
            {
                {(typeof(IKing), ChessColor.White), "\u2654"},
                {(typeof(IQueen), ChessColor.White), "\u2655"},
                {(typeof(IRook), ChessColor.White), "\u2656"},
                {(typeof(IBishop), ChessColor.White), "\u2657"},
                {(typeof(IKnight), ChessColor.White), "\u2658"},
                {(typeof(IPawn), ChessColor.White), "\u2659"},
                {(typeof(IKing), ChessColor.Black), "\u265A"},
                {(typeof(IQueen), ChessColor.Black), "\u265B"},
                {(typeof(IRook), ChessColor.Black), "\u265C"},
                {(typeof(IBishop), ChessColor.Black), "\u265D"},
                {(typeof(IKnight), ChessColor.Black), "\u265E"},
                {(typeof(IPawn), ChessColor.Black), "\u265F"}
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
            Type[] interfaces = piece.GetType().GetInterfaces();
            Type pieceInterface = null;

            // find which piece interface (IPawn, IBishop, etc.) the piece implements
            // and get the unicode character based on that interface.
            foreach ((Type type, ChessColor color) in UnicodeCharacters.Keys)
                if (interfaces.Contains(type))
                    pieceInterface = type;

            return UnicodeCharacters[(pieceInterface, piece.Color)];
        }
    }
}