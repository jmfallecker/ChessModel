Hello there,

The main way to use this model is to interact with it through the Game object.
Currently the Game object has only a few public methods. As of release (May 2,
2018), there is not an interface to program to, thus you'll need to either
implement that yourself or wait until it's added.

CREATING A GAME

When creating a new Game object, the function passed will need to return a Type
of piece. (IQueen, IBishop, IKnight or IRook). This decision should come from
the player, and is used to promote pawns after they have crossed the board.

NOTE

All functionality in Game and Pieces has been tested before migrating to .NET
Standard, so the tests for this version will come shortly. Please let me know
if you find anything wrong! My email is jmfallecker@gmail.com, if you send an
email, please include "StandardChess.Model" in the title.


Look in the future for updates to this project!

Cheers,
Joe