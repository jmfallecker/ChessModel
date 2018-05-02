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

using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Piece;

namespace StandardChess.Model.PieceModel
{
    public class ChessPieceFactory : IChessPieceFactory
    {
        public IPiece CreatePawn(ChessPosition startingPosition, ChessColor color)
        {
            return new Pawn(startingPosition, color);
        }

        public IPiece CreateBishop(ChessPosition startingPosition, ChessColor color)
        {
            return new Bishop(startingPosition, color);
        }

        public IPiece CreateKnight(ChessPosition startingPosition, ChessColor color)
        {
            return new Knight(startingPosition, color);
        }

        public IPiece CreateRook(ChessPosition startingPosition, ChessColor color)
        {
            return new Rook(startingPosition, color);
        }

        public IPiece CreateQueen(ChessPosition startingPosition, ChessColor color)
        {
            return new Queen(startingPosition, color);
        }

        public IPiece CreateKing(ChessPosition startingPosition, ChessColor color)
        {
            return new King(startingPosition, color);
        }
    }
}