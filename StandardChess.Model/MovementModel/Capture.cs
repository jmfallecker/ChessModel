﻿// .NET Standard 2.0 Chess Model
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

using System.Collections.Generic;
using StandardChess.Infrastructure;
using StandardChess.Infrastructure.Movement;

namespace StandardChess.Model.MovementModel
{
    public class Capture : ICapture
    {
        public ChessPosition StartingPosition { get; set; }
        public ChessPosition EndingPosition { get; set; }

        public override bool Equals(object obj)
        {
            var capture = (Capture) obj;
            return StartingPosition == capture.StartingPosition &&
                   EndingPosition == capture.EndingPosition;
        }

        public override int GetHashCode()
        {
            int hashCode = -1795512632;
            hashCode = hashCode * -1521134295 + StartingPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + EndingPosition.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Capture capture1, Capture capture2)
        {
            return EqualityComparer<Capture>.Default.Equals(capture1, capture2);
        }

        public static bool operator !=(Capture capture1, Capture capture2)
        {
            return !(capture1 == capture2);
        }
    }
}