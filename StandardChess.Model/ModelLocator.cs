using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Player;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;
using StandardChess.Model.PlayerModel;
using Unity;
using Unity.Lifetime;

namespace StandardChess.Model
{
    internal static class ModelLocator
    {
        private static readonly UnityContainer Container;

        static ModelLocator()
        {
            Container = new UnityContainer();
            Container.RegisterType<IBoardState, BoardState>(new PerResolveLifetimeManager());
            Container.RegisterType<IBoard, Board>(new PerResolveLifetimeManager());
            Container.RegisterType<IBitboard, Bitboard>(new PerResolveLifetimeManager());
            Container.RegisterType<IChessPieceMover, ChessPieceMover>();
            Container.RegisterType<IPlayer, Player>(new PerResolveLifetimeManager());
            Container.RegisterType<IMove, Move>(new PerResolveLifetimeManager());
            Container.RegisterType<ICapture, Capture>(new PerResolveLifetimeManager());
            Container.RegisterType<IChessPieceFactory, ChessPieceFactory>();
        }

        public static IBoardState BoardState => Container.Resolve<IBoardState>();

        public static IBoard Board => Container.Resolve<IBoard>();

        public static IBitboard Bitboard => Container.Resolve<IBitboard>();

        public static IChessPieceMover ChessPieceMover => Container.Resolve<IChessPieceMover>();

        public static IPlayer Player => Container.Resolve<IPlayer>();

        public static IMove Move => Container.Resolve<IMove>();

        public static ICapture Capture => Container.Resolve<ICapture>();

        public static IChessPieceFactory ChessPieceFactory => Container.Resolve<IChessPieceFactory>();
    }
}