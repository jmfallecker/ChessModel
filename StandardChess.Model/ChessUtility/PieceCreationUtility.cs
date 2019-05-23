using System;
using System.Collections.Generic;
using System.Text;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Piece;

namespace StandardChess.Model.ChessUtility
{
    internal class PieceCreationUtility
    {
        /// <summary>
        ///     Returns starting position pieces for the passed in color.
        /// </summary>
        /// <param name="color">Black or White</param>
        /// <returns></returns>
        public List<IPiece> CreateStartingPieces(ChessColor color)
        {
            IChessPieceFactory factory = ModelLocator.ChessPieceFactory;
            var pieces = new List<IPiece>();

            switch (color)
            {
                case ChessColor.White:
                    // create pawns
                    IPiece pawnA2 = factory.CreatePawn(ChessPosition.A2, color);
                    IPiece pawnB2 = factory.CreatePawn(ChessPosition.B2, color);
                    IPiece pawnC2 = factory.CreatePawn(ChessPosition.C2, color);
                    IPiece pawnD2 = factory.CreatePawn(ChessPosition.D2, color);
                    IPiece pawnE2 = factory.CreatePawn(ChessPosition.E2, color);
                    IPiece pawnF2 = factory.CreatePawn(ChessPosition.F2, color);
                    IPiece pawnG2 = factory.CreatePawn(ChessPosition.G2, color);
                    IPiece pawnH2 = factory.CreatePawn(ChessPosition.H2, color);

                    // create bigger pieces
                    IPiece rookA1 = factory.CreateRook(ChessPosition.A1, color);
                    IPiece knightB1 = factory.CreateKnight(ChessPosition.B1, color);
                    IPiece bishopC1 = factory.CreateBishop(ChessPosition.C1, color);
                    IPiece queenD1 = factory.CreateQueen(ChessPosition.D1, color);
                    IPiece kingE1 = factory.CreateKing(ChessPosition.E1, color);
                    IPiece bishopF1 = factory.CreateBishop(ChessPosition.F1, color);
                    IPiece knightG1 = factory.CreateKnight(ChessPosition.G1, color);
                    IPiece rookH1 = factory.CreateRook(ChessPosition.H1, color);

                    // add pawns to dictionary
                    pieces.Add(pawnA2);
                    pieces.Add(pawnB2);
                    pieces.Add(pawnC2);
                    pieces.Add(pawnD2);
                    pieces.Add(pawnE2);
                    pieces.Add(pawnF2);
                    pieces.Add(pawnG2);
                    pieces.Add(pawnH2);

                    // add bigger pieces to dictionary
                    pieces.Add(rookA1);
                    pieces.Add(knightB1);
                    pieces.Add(bishopC1);
                    pieces.Add(queenD1);
                    pieces.Add(kingE1);
                    pieces.Add(bishopF1);
                    pieces.Add(knightG1);
                    pieces.Add(rookH1);
                    break;
                case ChessColor.Black:
                    // create pawns
                    IPiece pawnA7 = factory.CreatePawn(ChessPosition.A7, color);
                    IPiece pawnB7 = factory.CreatePawn(ChessPosition.B7, color);
                    IPiece pawnC7 = factory.CreatePawn(ChessPosition.C7, color);
                    IPiece pawnD7 = factory.CreatePawn(ChessPosition.D7, color);
                    IPiece pawnE7 = factory.CreatePawn(ChessPosition.E7, color);
                    IPiece pawnF7 = factory.CreatePawn(ChessPosition.F7, color);
                    IPiece pawnG7 = factory.CreatePawn(ChessPosition.G7, color);
                    IPiece pawnH7 = factory.CreatePawn(ChessPosition.H7, color);

                    // create bigger pieces
                    IPiece rookA8 = factory.CreateRook(ChessPosition.A8, color);
                    IPiece knightB8 = factory.CreateKnight(ChessPosition.B8, color);
                    IPiece bishopC8 = factory.CreateBishop(ChessPosition.C8, color);
                    IPiece queenD8 = factory.CreateQueen(ChessPosition.D8, color);
                    IPiece kingE8 = factory.CreateKing(ChessPosition.E8, color);
                    IPiece bishopF8 = factory.CreateBishop(ChessPosition.F8, color);
                    IPiece knightG8 = factory.CreateKnight(ChessPosition.G8, color);
                    IPiece rookH8 = factory.CreateRook(ChessPosition.H8, color);

                    // add pawns to dictionary
                    pieces.Add(pawnA7);
                    pieces.Add(pawnB7);
                    pieces.Add(pawnC7);
                    pieces.Add(pawnD7);
                    pieces.Add(pawnE7);
                    pieces.Add(pawnF7);
                    pieces.Add(pawnG7);
                    pieces.Add(pawnH7);

                    // add bigger pieces to dictionary
                    pieces.Add(rookA8);
                    pieces.Add(knightB8);
                    pieces.Add(bishopC8);
                    pieces.Add(queenD8);
                    pieces.Add(kingE8);
                    pieces.Add(bishopF8);
                    pieces.Add(knightG8);
                    pieces.Add(rookH8);
                    break;
            }

            return pieces;
        }
    }
}
