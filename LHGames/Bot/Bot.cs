﻿using System;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot
{
    internal class Bot
    {
        internal IPlayer PlayerInfo { get; set; }
        private int _currentDirection = 1;
        private bool isGoingHome = false;

        internal Bot() { }

        /// <summary>
        /// Gets called before ExecuteTurn. This is where you get your bot's state.
        /// </summary>
        /// <param name="playerInfo">Your bot's current state.</param>
        internal void BeforeTurn(IPlayer playerInfo)
        {
            PlayerInfo = playerInfo;
        }

        /// <summary>
        /// Implement your bot here.
        /// </summary>
        /// <param name="map">The gamemap.</param>
        /// <param name="visiblePlayers">Players that are visible to your bot.</param>
        /// <returns>The action you wish to execute.</returns>
        internal string ExecuteTurn(Map map, IEnumerable<IPlayer> visiblePlayers)
        {
            // TODO: Implement your AI here.
            if (map.GetTileAt(PlayerInfo.Position.X, PlayerInfo.Position.Y) == TileContent.House)
            {
                isGoingHome = false;
            }
            else if (PlayerInfo.CarriedResources == PlayerInfo.CarryingCapacity ||
                map.GetTileAt(PlayerInfo.Position.X + 1, PlayerInfo.Position.Y) == TileContent.Wall)
            {
                isGoingHome = true;
            }
            if (PlayerInfo.CarriedResources < PlayerInfo.CarryingCapacity && map.GetTileAt(PlayerInfo.Position.X, PlayerInfo.Position.Y+1) == TileContent.Resource)
            {
                return AIHelper.CreateCollectAction(new Point(0, 1));
            }
            else
            {
                int direction = isGoingHome ? -1 : 1;
                return AIHelper.CreateMoveAction(new Point(direction, 0));
            }

        }

        /// <summary>
        /// Gets called after ExecuteTurn.
        /// </summary>
        internal void AfterTurn()
        {
        }
    }
}

class TestClass
{
    public string Test { get; set; }
}