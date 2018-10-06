using System;

namespace LHGames.Bot.Behaviours
{
    public class UpgradeBehaviour : Behaviour
    {
        private bool _uselessState = false;
        public override bool Evaluate()
        {
            _uselessState = !_uselessState;
            return _uselessState;
        }

        public override string Execute()
        {
            Console.WriteLine("Upgrade");
            return null;
        }
    }
}