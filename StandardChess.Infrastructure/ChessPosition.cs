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

        Rank1 = A1 | B1 | C1 | D1 | E1 | F1 | G1 | H1,
        Rank2 = A2 | B2 | C2 | D2 | E2 | F2 | G2 | H2,
        Rank3 = A3 | B3 | C3 | D3 | E3 | F3 | G3 | H3,
        Rank4 = A4 | B4 | C4 | D4 | E4 | F4 | G4 | H4,
        Rank5 = A5 | B5 | C5 | D5 | E5 | F5 | G5 | H5,
        Rank6 = A6 | B6 | C6 | D6 | E6 | F6 | G6 | H6,
        Rank7 = A7 | B7 | C7 | D7 | E7 | F7 | G7 | H7,
        Rank8 = A8 | B8 | C8 | D8 | E8 | F8 | G8 | H8,

        FileA = A1 | A2 | A3 | A4 | A5 | A6 | A7 | A8,
        FileB = B1 | B2 | B3 | B4 | B5 | B6 | B7 | B8,
        FileC = C1 | C2 | C3 | C4 | C5 | C6 | C7 | C8,
        FileD = D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8,
        FileE = E1 | E2 | E3 | E4 | E5 | E6 | E7 | E8,
        FileF = F1 | F2 | F3 | F4 | F5 | F6 | F7 | F8,
        FileG = G1 | G2 | G3 | G4 | G5 | G6 | G7 | G8,

        WhiteStart = Rank1 | Rank2,
        BlackStart = Rank7 | Rank8
    }
}