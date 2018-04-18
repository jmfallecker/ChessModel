using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Player;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PlayerModel;
using Unity;

namespace StandardChess.Model
{
    internal static class ModelLocator
    {
        private static readonly UnityContainer Container;

        static ModelLocator()
        {
            Container = new UnityContainer();
            Container.RegisterType<IBoardState, BoardState>();
            Container.RegisterType<IBoard, Board>();
            Container.RegisterType<IBitboard, Bitboard>();
            Container.RegisterType<IChessPieceMover, ChessPieceMover>();
            Container.RegisterType<IPlayer, Player>();
            Container.RegisterType<IMove, Move>();
            Container.RegisterType<ICapture, Capture>();
        }

        public static IBoardState BoardState => Container.Resolve<IBoardState>();
        public static IBoard Board => Container.Resolve<IBoard>();
        public static IBitboard Bitboard => Container.Resolve<IBitboard>();
        public static IChessPieceMover ChessPieceMover => Container.Resolve<IChessPieceMover>();
        public static IPlayer Player => Container.Resolve<IPlayer>();
        public static IMove Move => Container.Resolve<IMove>();
        public static ICapture Capture => Container.Resolve<ICapture>();
    }
}
