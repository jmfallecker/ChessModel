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
using StandardChess.Infrastructure.Utility;

namespace StandardChess.Model.ChessUtility
{
    public class ChessPieceMover : IChessPieceMover
    {
        #region Fields

        /// <summary>
        ///     The representation of all files B-H excluding file A.
        /// </summary>
        private const ulong NOT_A_FILE = 0xfefefefefefefefe;

        /// <summary>
        ///     The representation of all files A-G excluding file H.
        /// </summary>
        private const ulong NOT_H_FILE = 0x7f7f7f7f7f7f7f7f;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Returns position one square north of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition North(ChessPosition originalPosition)
        {
            return (ChessPosition) ((ulong) originalPosition << 8);
        }

        /// <summary>
        ///     Returns position one square south of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition South(ChessPosition originalPosition)
        {
            return (ChessPosition) ((ulong) originalPosition >> 8);
        }

        /// <summary>
        ///     Returns position one square east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition East(ChessPosition originalPosition)
        {
            return (ChessPosition) (((ulong) originalPosition << 1) & NOT_A_FILE);
        }

        /// <summary>
        ///     Returns position one square west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition West(ChessPosition originalPosition)
        {
            return (ChessPosition) (((ulong) originalPosition >> 1) & NOT_H_FILE);
        }

        /// <summary>
        ///     Returns position one square north-east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition NorthEast(ChessPosition originalPosition)
        {
            return (ChessPosition) (((ulong) originalPosition << 9) & NOT_A_FILE);
        }

        /// <summary>
        ///     Returns position one square south-east of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition SouthEast(ChessPosition originalPosition)
        {
            return (ChessPosition) (((ulong) originalPosition >> 7) & NOT_A_FILE);
        }

        /// <summary>
        ///     Returns position one square north-west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition NorthWest(ChessPosition originalPosition)
        {
            return (ChessPosition) (((ulong) originalPosition << 7) & NOT_H_FILE);
        }

        /// <summary>
        ///     Returns position one square south-west of passed in position.
        /// </summary>
        /// <param name="originalPosition"></param>
        /// <returns></returns>
        public ChessPosition SouthWest(ChessPosition originalPosition)
        {
            return (ChessPosition) (((ulong) originalPosition >> 9) & NOT_H_FILE);
        }

        #endregion
    }
}