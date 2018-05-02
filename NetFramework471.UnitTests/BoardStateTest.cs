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


using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model;
using System.Linq;
using Unity.Interception.Utilities;

namespace NetFramework471.UnitTests
{
    [TestClass]
    public class BoardStateTest
    {
        [TestMethod]
        public void Should_ReturnTrueWhenContains()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            const ChessPosition POSITION = ChessPosition.A1;
            state.Add(POSITION);

            // execute
            bool contains = state.Contains(POSITION);

            // verify
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Should_ReturnFalseWhenDoesNotContain()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            const ChessPosition POSITION = ChessPosition.A1;

            // execute
            bool contains = state.Contains(POSITION);

            // verify
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Should_AddLocation()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            const ChessPosition POSITION = ChessPosition.A1;

            // execute
            state.Add(POSITION);

            // verify
            Assert.IsTrue(state.Contains(POSITION));
        }

        [TestMethod]
        public void Should_AddAllPositionsFromBoardstate()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            var positions = new[] { ChessPosition.A2, ChessPosition.A1, ChessPosition.A4, ChessPosition.E3 };
            positions.ForEach(p => state.Add(p));

            IBoardState state2 = ModelLocator.BoardState;

            // execute
            state2.Add(state);

            // verify
            Assert.IsTrue(positions.All(p => state2.Contains(p)));
        }

        [TestMethod]
        public void Should_ClearPositions()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            var positions = new[] { ChessPosition.A2, ChessPosition.A1, ChessPosition.A4, ChessPosition.E3 };
            positions.ForEach(p => state.Add(p));

            // execute
            state.Clear();

            // verify
            Assert.IsTrue(positions.All(p => !state.Contains(p)));
        }

        [TestMethod]
        public void Should_ReturnTrueWhenPositionOccupied()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            const ChessPosition POSITION = ChessPosition.A1;
            state.Add(POSITION);

            // execute
            bool contains = state.IsPositionOccupied(POSITION);

            // verify
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Should_ReturnFalseWhenPositionNotOccupied()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            const ChessPosition POSITION = ChessPosition.A1;

            // execute
            bool contains = state.IsPositionOccupied(POSITION);

            // verify
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Should_RemovePositions()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            const ChessPosition POSITION = ChessPosition.A1;
            state.Add(POSITION);

            // execute
            state.Remove(POSITION);

            // verify
            Assert.IsFalse(state.Contains(POSITION));
        }

        [TestMethod]
        public void Should_RemoveAllPositionsFromBoardstate()
        {
            // setup
            IBoardState state = ModelLocator.BoardState;
            var positions = new[] { ChessPosition.A2, ChessPosition.A1, ChessPosition.A4, ChessPosition.E3 };
            positions.ForEach(p => state.Add(p));

            IBoardState state2 = ModelLocator.BoardState;
            state2.Add(state);

            // execute
            state.Remove(state2);

            // verify
            Assert.IsTrue(positions.All(p => !state.Contains(p)));
        }
    }
}
