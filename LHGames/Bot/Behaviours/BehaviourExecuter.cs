using System.Collections.Generic;

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
            for (int i = 0; i < _behaviours.Count; i++)
            {
                if (_behaviours[i].Evaluate())
                {
                    if (_behaviours[i] == _currentBehaviours)
                    {
                        if (_currentBehaviours.Execute() == null)
                        {
                            _currentBehaviours.StateOut();
                            _currentBehaviours = null;
                        }
                        break;
                    }

                    _currentBehaviours?.StateOut();
                    _currentBehaviours = _behaviours[i];
                    _currentBehaviours.StateIn();
                    _currentBehaviours.Execute();
                }
            }
            return null;
        }
    }
}