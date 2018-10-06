using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class BehaviourExecuter
    {
        private List<Behaviour> _behaviours = new List<Behaviour>();
        private Behaviour _currentBehaviours;

        public BehaviourExecuter(/*LA MAP*/)
        {
            _behaviours.Add(new UpgradeBehaviour());
            _behaviours.Add(new MiningBehaviour());
            _behaviours.Add(new ExploreBehaviour());
        }

        public string GetNextAction()
        {
            string action = null;
            for (int i = 0; i < _behaviours.Count; i++)
            {
                if (_behaviours[i].Evaluate())
                {
                    if (_behaviours[i] != _currentBehaviours)
                    {
                        _currentBehaviours?.StateOut();
                        _currentBehaviours = null;
                    }
                    else
                    {
                        _currentBehaviours = _behaviours[i];
                        _currentBehaviours.StateIn();
                    }

                    action = _currentBehaviours.Execute();
                    break;
                }
            }

            return action ?? AIHelper.CreateEmptyAction();
        }
    }
}