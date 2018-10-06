using System;

namespace LHGames.Bot.Behaviours
{
    public class UpgradeBehaviour : Behaviour
    {
        public UpgradeBehaviour(BehaviourExecuter executer) : base(executer) {}

        public override bool Evaluate()
        {
            return false;
        }

        public override string Execute()
        {
            return null;
        }
    }
}