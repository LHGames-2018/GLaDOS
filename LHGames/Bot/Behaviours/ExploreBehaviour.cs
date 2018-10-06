namespace LHGames.Bot.Behaviours
{
    public class ExploreBehaviour : Behaviour
    {
        public ExploreBehaviour(BehaviourExecuter executer) : base(executer) {}
        
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