namespace LHGames.Bot.Behaviours
{
    public abstract class Behaviour
    {
        // protected Map _map;
        public Behaviour(/*Map map*/)
        {
            // _map = map;
        }

        public abstract bool Evaluate();
        public abstract string Execute();
        public virtual void StateIn() { }
        public virtual void StateOut() { }
    }
}