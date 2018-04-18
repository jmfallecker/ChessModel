namespace StandardChess.Infrastructure
{
    public enum GameState
    {
        Ongoing,
        WhiteInCheck,
        BlackInCheck,
        WhiteInCheckmate,
        BlackInCheckmate,
        Stalemate
    }
}