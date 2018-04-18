namespace StandardChess.Infrastructure.Movement
{
    public interface ICapture : IMovable
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}