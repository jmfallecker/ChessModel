using System;
using System.Collections.Generic;
using System.Text;

namespace StandardChess.Infrastructure.Movement
{
    public interface IMovable
    {
        ChessPosition StartingPosition { get; set; }
        ChessPosition EndingPosition { get; set; }
    }
}
