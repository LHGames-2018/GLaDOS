namespace LHGames.Bot.Behaviours
{
    public abstract class Behaviour
    {
        protected BehaviourExecuter _executer;

        public Behaviour(BehaviourExecuter executer)
        {
            _executer = executer;
        }

        public abstract bool Evaluate();
        public abstract string Execute();
        public virtual void StateIn() { }
        public virtual void StateOut() { }
    }
}