using System.Collections.Generic;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;

namespace StandardChess.Model.Helpers
{
    internal class CastlingHelper
    {
        /// <summary>
        /// Determines the location of the rook that is trying to be castled with.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public ChessPosition GetCastlingRookPosition(IMovable move)
        {
            switch (move.EndingPosition)
            {
                case ChessPosition.C1: return ChessPosition.A1;
                case ChessPosition.G1: return ChessPosition.H1;
                case ChessPosition.C8: return ChessPosition.A8;
                case ChessPosition.G8: return ChessPosition.H8;
                default:               return ChessPosition.None;
            }
        }

        /// <summary>
        ///     Determines the position where a castling Rook will end at.
        /// </summary>
        /// <param name="king"></param>
        /// <param name="rook"></param>
        /// <returns></returns>
        public ChessPosition GetEndingPositionForCastlingRook(IKing king, IRook rook)
        {
            switch (king.Location)
            {
                case ChessPosition.E1 when rook.Location == ChessPosition.A1: return ChessPosition.D1;
                case ChessPosition.E1 when rook.Location == ChessPosition.H1: return ChessPosition.F1;
                case ChessPosition.E8 when rook.Location == ChessPosition.A8: return ChessPosition.D8;
                case ChessPosition.E8 when rook.Location == ChessPosition.H8: return ChessPosition.F8;
                default: return rook.Location;
            }
        }

        /// <summary>
        ///     Generates the two-space moves that a king can make to initiate a castle.
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        public IEnumerable<ChessPosition> GetCastleMovesForKing(IKing king)
        {
            var positions = new List<ChessPosition>();
            if (king.HasMoved)
                return positions;

            if (king.Color == ChessColor.White)
            {
                positions.Add(ChessPosition.G1);
                positions.Add(ChessPosition.C1);
            }
            else
            {
                positions.Add(ChessPosition.G8);
                positions.Add(ChessPosition.C8);
            }

            return positions;
        }

        /// <summary>
        ///     Retrieves a list of positions that are between the castling Rook and King.
        /// </summary>
        /// <param name="king"></param>
        /// <param name="rook"></param>
        /// <returns></returns>
        public List<ChessPosition> GetPositionsBetweenCastle(IKing king, IRook rook)
        {
            var locationsInBetween = new List<ChessPosition>();

            // add all locations to check based on where the king and rook are located
            switch (king.Location)
            {
                case ChessPosition.E1 when rook.Location == ChessPosition.A1:
                    locationsInBetween.Add(ChessPosition.D1);
                    locationsInBetween.Add(ChessPosition.C1);
                    locationsInBetween.Add(ChessPosition.B1);
                    break;
                case ChessPosition.E1 when rook.Location == ChessPosition.H1:
                    locationsInBetween.Add(ChessPosition.F1);
                    locationsInBetween.Add(ChessPosition.G1);
                    break;
                case ChessPosition.E8 when rook.Location == ChessPosition.A8:
                    locationsInBetween.Add(ChessPosition.D8);
                    locationsInBetween.Add(ChessPosition.C8);
                    locationsInBetween.Add(ChessPosition.B8);
                    break;
                case ChessPosition.E8 when rook.Location == ChessPosition.H8:
                    locationsInBetween.Add(ChessPosition.F8);
                    locationsInBetween.Add(ChessPosition.G8);
                    break;
            }

            return locationsInBetween;
        }
    }
}
