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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;

namespace StandardChess.Model.BoardModel
{
    public class BoardState : IBoardState
    {
        #region Constructor

        /// <summary>
        ///     Provides ability to add/remove pieces from the underlying Bitboard. Also provides ability to see if a position is
        ///     occupied.
        /// </summary>
        public BoardState()
        {
            _bitboard = ModelLocator.Bitboard;
            _occupiedSquares = new SortedSet<ChessPosition>();
        }

        #endregion

        #region Fields

        /// <summary>
        ///     List used for easier debugging, being able to see occupied square names.
        /// </summary>
        private readonly SortedSet<ChessPosition> _occupiedSquares;

        public IEnumerable<ChessPosition> OccupiedSquares => _occupiedSquares;

        /// <summary>
        ///     Used to keep track of ulong positions of boardstate with bitwise operators.
        /// </summary>
        private readonly IBitboard _bitboard;

        #endregion

        #region IBoardState Members

        public bool Add(ChessPosition position)
        {
            if (position == ChessPosition.None)
                return false;
            if (_occupiedSquares.Contains(position))
                return false;

            AddToState(position);
            return true;
        }

        public bool Add(IBoardState boardState)
        {
            boardState.OccupiedSquares.ToList().ForEach(p => Add(p));
            return true;
        }

        public void Clear()
        {
            _occupiedSquares.Clear();
            _bitboard.ClearAllPieces();
        }

        public bool Contains(ChessPosition position)
        {
            if (position == ChessPosition.None)
                return true;

            return _occupiedSquares.Contains(position) && _bitboard.IsLocationOccupied(position);
        }

        public bool Remove(ChessPosition position)
        {
            if (!_occupiedSquares.Contains(position))
                return false;

            RemoveFromState(position);
            return true;
        }

        public void Remove(IBoardState stateToRemove)
        {
            stateToRemove.OccupiedSquares.ToList().ForEach(p => Remove(p));
        }

        public bool IsPositionOccupied(ChessPosition position)
        {
            return _bitboard.IsLocationOccupied(position) && _occupiedSquares.Contains(position);
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator<ChessPosition> GetEnumerator()
        {
            return ((IEnumerable<ChessPosition>) _occupiedSquares).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ChessPosition>) _occupiedSquares).GetEnumerator();
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is IBoardState boardState)) return false;

            return OccupiedSquares.SequenceEqual(boardState.OccupiedSquares);
        }

        public override int GetHashCode()
        {
            const int RESULT = 17;
            int occupiedSquaresHash = _occupiedSquares is null
                ? 0
                : EqualityComparer<SortedSet<ChessPosition>>.Default.GetHashCode(_occupiedSquares);
            int bitBoardHash = _bitboard == null ? 0 : EqualityComparer<IBitboard>.Default.GetHashCode(_bitboard);

            return 37 * RESULT + occupiedSquaresHash + bitBoardHash;
        }

        #endregion

        #region Private Methods

        private void AddToState(ChessPosition position)
        {
            // a position will be a power of two if it is a single chess square location.
            if (IsPowerOfTwo(position))
                _occupiedSquares.Add(position);
            else // a position will not be a power of two if it is multiple square locations combined into one (ChessPosition)ulong value.
                foreach (ChessPosition p in Enum.GetValues(typeof(ChessPosition)))
                    if ((p & position) == p && IsPowerOfTwo(p))
                        _occupiedSquares.Add(p);

            _bitboard.AddPieceToBoard(position);
        }

        private static bool IsPowerOfTwo(ChessPosition x)
        {
            // first operand is obvious... are we dealing with zero?
            // second operand is basically... if it's a power of two, then there is a single 1 and rest 0.
            // subtract 1 and we have a single 0 and rest 1
            // thus, (x & (x - 1)) always equals 0 if x is a power of two.

            return x != 0 && (x & (x - 1)) == 0;
        }

        private void RemoveFromState(ChessPosition position)
        {
            _occupiedSquares.Remove(position);
            _bitboard.RemovePieceFromBoard(position);
        }

        #endregion
    }
}