namespace StandardChess.Model.Interfaces
{
    interface IThreatening
    {
        bool IsThreateningAt(ulong location, ulong boardState);
    }
}
