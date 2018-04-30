﻿using System;
using System.ComponentModel;

namespace StandardChess.Infrastructure
{
    [Flags]
    public enum ChessPosition : ulong
    {
        None = 0x0,
        [Description("A1")]
        A1 = 0x0000000000000001,
        [Description("A2")]
        A2 = 0x0000000000000100,
        [Description("A3")]
        A3 = 0x0000000000010000,
        [Description("A4")]
        A4 = 0x0000000001000000,
        [Description("A5")]
        A5 = 0x0000000100000000,
        [Description("A6")]
        A6 = 0x0000010000000000,
        [Description("A7")]
        A7 = 0x0001000000000000,
        [Description("A8")]
        A8 = 0x0100000000000000,
        [Description("B1")]
        B1 = 0x0000000000000002,
        [Description("B2")]
        B2 = 0x0000000000000200,
        [Description("B3")]
        B3 = 0x0000000000020000,
        [Description("B4")]
        B4 = 0x0000000002000000,
        [Description("B5")]
        B5 = 0x0000000200000000,
        [Description("B6")]
        B6 = 0x0000020000000000,
        [Description("B7")]
        B7 = 0x0002000000000000,
        [Description("B8")]
        B8 = 0x0200000000000000,
        [Description("C1")]
        C1 = 0x0000000000000004,
        [Description("C2")]
        C2 = 0x0000000000000400,
        [Description("C3")]
        C3 = 0x0000000000040000,
        [Description("C4")]
        C4 = 0x0000000004000000,
        [Description("C5")]
        C5 = 0x0000000400000000,
        [Description("C6")]
        C6 = 0x0000040000000000,
        [Description("C7")]
        C7 = 0x0004000000000000,
        [Description("C8")]
        C8 = 0x0400000000000000,
        [Description("D1")]
        D1 = 0x0000000000000008,
        [Description("D2")]
        D2 = 0x0000000000000800,
        [Description("D3")]
        D3 = 0x0000000000080000,
        [Description("D4")]
        D4 = 0x0000000008000000,
        [Description("D5")]
        D5 = 0x0000000800000000,
        [Description("D6")]
        D6 = 0x0000080000000000,
        [Description("D7")]
        D7 = 0x0008000000000000,
        [Description("D8")]
        D8 = 0x0800000000000000,
        [Description("E1")]
        E1 = 0x0000000000000010,
        [Description("E2")]
        E2 = 0x0000000000001000,
        [Description("E3")]
        E3 = 0x0000000000100000,
        [Description("E4")]
        E4 = 0x0000000010000000,
        [Description("E5")]
        E5 = 0x0000001000000000,
        [Description("E6")]
        E6 = 0x0000100000000000,
        [Description("E7")]
        E7 = 0x0010000000000000,
        [Description("E8")]
        E8 = 0x1000000000000000,
        [Description("F1")]
        F1 = 0x0000000000000020,
        [Description("F2")]
        F2 = 0x0000000000002000,
        [Description("F3")]
        F3 = 0x0000000000200000,
        [Description("F4")]
        F4 = 0x0000000020000000,
        [Description("F5")]
        F5 = 0x0000002000000000,
        [Description("F6")]
        F6 = 0x0000200000000000,
        [Description("F7")]
        F7 = 0x0020000000000000,
        [Description("F8")]
        F8 = 0x2000000000000000,
        [Description("G1")]
        G1 = 0x0000000000000040,
        [Description("G2")]
        G2 = 0x0000000000004000,
        [Description("G3")]
        G3 = 0x0000000000400000,
        [Description("G4")]
        G4 = 0x0000000040000000,
        [Description("G5")]
        G5 = 0x0000004000000000,
        [Description("G6")]
        G6 = 0x0000400000000000,
        [Description("G7")]
        G7 = 0x0040000000000000,
        [Description("G8")]
        G8 = 0x4000000000000000,
        [Description("H1")]
        H1 = 0x0000000000000080,
        [Description("H2")]
        H2 = 0x0000000000008000,
        [Description("H3")]
        H3 = 0x0000000000800000,
        [Description("H4")]
        H4 = 0x0000000080000000,
        [Description("H5")]
        H5 = 0x0000008000000000,
        [Description("H6")]
        H6 = 0x0000800000000000,
        [Description("H7")]
        H7 = 0x0080000000000000,
        [Description("H8")]
        H8 = 0x8000000000000000,

        //FileA = 0x0101010101010101,
        //FileH = 0x8080808080808080,
        Rank1 = 0x00000000000000FF,
        Rank8 = 0xFF00000000000000,
        //A1H8Diagonal = 0x8040201008040201,
        //H1A8Diagonal = 0x0102040810204080,
        Rank4 = 0x00000000FF000000,
        Rank5 = 0x000000FF00000000,
        WhiteStart = 0x000000000000FFFF,
        BlackStart = 0xFFFF000000000000,
    }
}
