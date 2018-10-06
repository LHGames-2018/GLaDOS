using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class BehaviourExecuter
    {
        private List<Behaviour> _behaviours = new List<Behaviour>();
        private Behaviour _currentBehaviours;
        private Map _map;
        private IPlayer _playerInfo;

        internal Map Map => _map;
        internal IPlayer PlayerInfo => _playerInfo;

        public BehaviourExecuter(IPlayer playerInfo)
        {
            _playerInfo = playerInfo;
            _behaviours.Add(new UpgradeBehaviour(this));
            _behaviours.Add(new MiningBehaviour(this));
            _behaviours.Add(new ExploreBehaviour(this));
        }

        internal string GetNextAction(Map map, IPlayer PlayerInfo)
        {
            _map = map;
            _playerInfo = PlayerInfo;

            string action = null;
            for (int i = 0; i < _behaviours.Count; i++)
            {
                if (_behaviours[i].Evaluate())
                {
                    if (_behaviours[i] != _currentBehaviours)
                    {
                        _currentBehaviours?.StateOut();
                        _currentBehaviours = _behaviours[i];
                        _currentBehaviours.StateIn();
                    }

                    action = _currentBehaviours?.Execute();
                    break;
                }
            }

            return action ?? AIHelper.CreateEmptyAction();
        }
    }
}