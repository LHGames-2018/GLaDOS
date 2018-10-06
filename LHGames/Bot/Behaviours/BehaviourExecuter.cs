using System.Collections.Generic;
using LHGames.Helper;
using LHGames.Navigation;

namespace LHGames.Bot.Behaviours
{
    public class BehaviourExecuter
    {
        private List<Behaviour> _behaviours = new List<Behaviour>();
        private Behaviour _currentBehaviours;
        private MyMap _map;
        private IPlayer _playerInfo;

        internal MyMap Map => _map;
        internal IPlayer PlayerInfo => _playerInfo;

        public BehaviourExecuter(/*LA MAP*/)
        {
            // _behaviours.Add(new UpgradeBehaviour(this));
            _behaviours.Add(new MiningBehaviour(this));
            _behaviours.Add(new ExploreBehaviour(this));
        }

        internal string GetNextAction(MyMap map, IPlayer PlayerInfo)
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