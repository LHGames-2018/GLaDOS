using System;
using System.Collections.Generic;
using LHGames.Helper;
using LHGames.Navigation;
using LHGames.Bot.Behaviours;

namespace LHGames.Bot
{
    internal class Bot
    {
        internal IPlayer PlayerInfo { get; set; }
        private MyMap _map;
        private BehaviourExecuter _behaviourExecuter;

        internal Bot()
        {
            _behaviourExecuter = new BehaviourExecuter();
        }

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
            if(_map == null) {
                _map = StorageHelper.Read<MyMap>("MyMap");
                if(_map == null) {
                    _map = new MyMap();
                }
            }
            _map.MergeMap(map);
            StorageHelper.Write("MyMap", _map);

            return _behaviourExecuter.GetNextAction(_map, PlayerInfo);
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