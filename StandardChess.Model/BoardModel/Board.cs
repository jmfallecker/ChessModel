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
using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Model.BoardModel
{
    public class Board : IBoard
    {
        #region Constructor

        /// <summary>
        ///     Creates a board with starting positions.
        /// </summary>
        public Board()
        {
            State = ModelLocator.BoardState;

            State.Add(ChessPosition.WhiteStart);
            State.Add(ChessPosition.BlackStart);
        }

        #endregion

        #region Properties

        public IBoardState State { get; protected set; }

        #endregion

        #region Public Methods

        public bool IsPositionOccupied(ChessPosition position)
        {
            return State.IsPositionOccupied(position);
        }

        public bool Add(ChessPosition position)
        {
            return State.Add(position);
        }

        public bool Remove(ChessPosition position)
        {
            return State.Remove(position);
        }

        #endregion
    }
}