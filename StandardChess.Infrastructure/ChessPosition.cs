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

using System;

namespace StandardChess.Infrastructure
{
    [Flags]
    public enum ChessPosition : ulong
    {
        None = 0x0,
        A1 = 0x0000000000000001,
        A2 = 0x0000000000000100,
        A3 = 0x0000000000010000,
        A4 = 0x0000000001000000,
        A5 = 0x0000000100000000,
        A6 = 0x0000010000000000,
        A7 = 0x0001000000000000,
        A8 = 0x0100000000000000,
        B1 = 0x0000000000000002,
        B2 = 0x0000000000000200,
        B3 = 0x0000000000020000,
        B4 = 0x0000000002000000,
        B5 = 0x0000000200000000,
        B6 = 0x0000020000000000,
        B7 = 0x0002000000000000,
        B8 = 0x0200000000000000,
        C1 = 0x0000000000000004,
        C2 = 0x0000000000000400,
        C3 = 0x0000000000040000,
        C4 = 0x0000000004000000,
        C5 = 0x0000000400000000,
        C6 = 0x0000040000000000,
        C7 = 0x0004000000000000,
        C8 = 0x0400000000000000,
        D1 = 0x0000000000000008,
        D2 = 0x0000000000000800,
        D3 = 0x0000000000080000,
        D4 = 0x0000000008000000,
        D5 = 0x0000000800000000,
        D6 = 0x0000080000000000,
        D7 = 0x0008000000000000,
        D8 = 0x0800000000000000,
        E1 = 0x0000000000000010,
        E2 = 0x0000000000001000,
        E3 = 0x0000000000100000,
        E4 = 0x0000000010000000,
        E5 = 0x0000001000000000,
        E6 = 0x0000100000000000,
        E7 = 0x0010000000000000,
        E8 = 0x1000000000000000,
        F1 = 0x0000000000000020,
        F2 = 0x0000000000002000,
        F3 = 0x0000000000200000,
        F4 = 0x0000000020000000,
        F5 = 0x0000002000000000,
        F6 = 0x0000200000000000,
        F7 = 0x0020000000000000,
        F8 = 0x2000000000000000,
        G1 = 0x0000000000000040,
        G2 = 0x0000000000004000,
        G3 = 0x0000000000400000,
        G4 = 0x0000000040000000,
        G5 = 0x0000004000000000,
        G6 = 0x0000400000000000,
        G7 = 0x0040000000000000,
        G8 = 0x4000000000000000,
        H1 = 0x0000000000000080,
        H2 = 0x0000000000008000,
        H3 = 0x0000000000800000,
        H4 = 0x0000000080000000,
        H5 = 0x0000008000000000,
        H6 = 0x0000800000000000,
        H7 = 0x0080000000000000,
        H8 = 0x8000000000000000,

        Rank1 = 0x00000000000000FF,
        Rank8 = 0xFF00000000000000,

        Rank4 = 0x00000000FF000000,
        Rank5 = 0x000000FF00000000,
        WhiteStart = 0x000000000000FFFF,
        BlackStart = 0xFFFF000000000000
    }
}