﻿using StandardChess.Infrastructure.BoardInterfaces;
using StandardChess.Model.BoardModel;
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
        }

        public static IBoardState BoardState => Container.Resolve<IBoardState>();
    }
}
