﻿using System;
using System.Collections.Generic;
using System.Text;
using StandardChess.Infrastructure.Movement;

namespace StandardChess.Infrastructure.Game
{
    public interface IGame
    {
        bool AttemptMove(IPlayerAction playerAction);
        bool MovePiece(IMove move);
        bool CapturePiece(ICapture capture);
    }
}
