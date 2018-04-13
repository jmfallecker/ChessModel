﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model.ChessUtility;

namespace StandardChess.Model.BoardModel
{
    public class BoardState : IBoardState, IEnumerable<ChessPosition>
    {
        #region Fields

        /// <summary>
        /// List used for easier debugging, being able to see occupied square names.
        /// </summary>
        private readonly SortedSet<ChessPosition> _occupiedSquares;
        public IEnumerable<ChessPosition> OccupiedSquares => _occupiedSquares;

        /// <summary>
        /// Used to keep track of ulong positions of boardstate with bitwise operators.
        /// </summary>
        private readonly Bitboard _bitboard;

        #endregion

        #region Constructor

        /// <summary>
        /// Provides ability to add/remove pieces from the underlying Bitboard. Also provides ability to see if a position is occupied.
        /// </summary>
        public BoardState()
        {
            _bitboard = new Bitboard();
            _occupiedSquares = new SortedSet<ChessPosition>();
        }

        public BoardState(ChessPosition position)
        {
            _bitboard = new Bitboard();
            _occupiedSquares = new SortedSet<ChessPosition>();

            Add(position);
        }

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
            return ((IEnumerable<ChessPosition>)_occupiedSquares).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ChessPosition>)_occupiedSquares).GetEnumerator();
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            BoardState boardState = (BoardState)obj;
            return _occupiedSquares.SequenceEqual(boardState._occupiedSquares) &&
                   _bitboard.State == boardState._bitboard.State;
        }

        public override int GetHashCode()
        {
            int result = 17;
            int occupiedSquaresHash = _occupiedSquares == null ? 0 : EqualityComparer<SortedSet<ChessPosition>>.Default.GetHashCode(_occupiedSquares);
            int bitBoardHash = _bitboard == null ? 0 : EqualityComparer<Bitboard>.Default.GetHashCode(_bitboard);

            return 37 * result + occupiedSquaresHash + bitBoardHash;
        }

        #endregion

        #region Private Methods

        private void AddToState(ChessPosition position)
        {
            // a position will be a power of two if it is a single chess square location.
            if (IsPowerOfTwo(position))
            {
                _occupiedSquares.Add(position);
            }
            else // a position will not be a power of two if it is multiple square locations combined into one (ChessPosition)ulong value.
            {
                foreach (ChessPosition p in Enum.GetValues(typeof(ChessPosition)))
                {
                    if ((p & position) == p && IsPowerOfTwo(p))
                        _occupiedSquares.Add(p);
                }
            }

            _bitboard.AddPieceToBoard(position);
        }

        private static bool IsPowerOfTwo(ChessPosition x)
        {
            // first operand is obvious... are we dealing with zero?
            // second operand is basically... if it's a power of two, then there is a single 1 and rest 0.
            // subtract 1 and we have a single 0 and rest 1
            // thus, (x & (x - 1)) always equals 0 if x is a power of two.

            return (x != 0) && ((x & (x - 1)) == 0);
        }

        private void RemoveFromState(ChessPosition position)
        {
            _occupiedSquares.Remove(position);
            _bitboard.RemovePieceFromBoard(position);
        }

        #endregion
    }
}
