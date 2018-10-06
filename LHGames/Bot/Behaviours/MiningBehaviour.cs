using System;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class MiningBehaviour : Behaviour
    {
        public MiningBehaviour(BehaviourExecuter executer) : base(executer) {}

        public override bool Evaluate()
        {
            return true;
        }

        public override string Execute()
        {
            return AIHelper.CreateEmptyAction();
        }
    }
}