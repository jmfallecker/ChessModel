// .NET Standard 2.0 Chess Model
// Copyright(C) 2018 Joseph M Fallecker

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<https: //www.gnu.org/licenses/>.

using System.Runtime.CompilerServices;
using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Infrastructure.Movement;
using StandardChess.Infrastructure.Piece;
using StandardChess.Infrastructure.Player;
using StandardChess.Infrastructure.Utility;
using StandardChess.Model.BoardModel;
using StandardChess.Model.ChessUtility;
using StandardChess.Model.GameModel;
using StandardChess.Model.MovementModel;
using StandardChess.Model.PieceModel;
using StandardChess.Model.PlayerModel;
using Unity;
using Unity.Lifetime;

[assembly: InternalsVisibleTo("NetFramework471.UnitTests")]

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
            Container.RegisterType<IPlayer, Player>(new PerResolveLifetimeManager());
            Container.RegisterType<IMove, Move>(new PerResolveLifetimeManager());
            Container.RegisterType<ICapture, Capture>(new PerResolveLifetimeManager());
            Container.RegisterType<IMoveHistory, MoveHistory>(new PerResolveLifetimeManager());

            Container.RegisterType<IChessPieceMover, ChessPieceMover>();
            Container.RegisterType<IChessPieceFactory, ChessPieceFactory>(new SingletonLifetimeManager());
            Container.RegisterType<PieceCreationUtility>(new SingletonLifetimeManager());
            Container.RegisterType<CastlingHelper>(new SingletonLifetimeManager());
            Container.RegisterType<EnPassantHelper>(new SingletonLifetimeManager());
        }

        public static IBoardState BoardState => Container.Resolve<IBoardState>();

        public static IBoard Board => Container.Resolve<IBoard>();

        public static IBitboard Bitboard => Container.Resolve<IBitboard>();

        public static IChessPieceMover ChessPieceMover => Container.Resolve<IChessPieceMover>();

        public static IPlayer Player => Container.Resolve<IPlayer>();

        public static IMove Move => Container.Resolve<IMove>();

        public static ICapture Capture => Container.Resolve<ICapture>();

        public static IChessPieceFactory ChessPieceFactory => Container.Resolve<IChessPieceFactory>();

        public static IMoveHistory MoveHistory => Container.Resolve<IMoveHistory>();

        public static PieceCreationUtility PieceCreationUtility => Container.Resolve<PieceCreationUtility>();

        public static CastlingHelper CastlingHelper => Container.Resolve<CastlingHelper>();

        public static EnPassantHelper EnPassantHelper => Container.Resolve<EnPassantHelper>();
    }
}