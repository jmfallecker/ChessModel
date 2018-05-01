namespace StandardChess.Infrastructure.Movement
{
    public interface IMove : IMovable
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}