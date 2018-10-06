using System;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class MiningBehaviour : Behaviour
    {
        public MiningBehaviour(BehaviourExecuter executer) : base(executer) { }

        private bool isGoingHome = false;

        public override bool Evaluate()
        {
            return true;
        }

        public override string Execute()
        {
            if (_executer.Map.GetTileAt(_executer.PlayerInfo.Position.X, _executer.PlayerInfo.Position.Y) == TileContent.House)
            {
                isGoingHome = false;
            }
            else if (_executer.PlayerInfo.CarriedResources == _executer.PlayerInfo.CarryingCapacity ||
                _executer.Map.GetTileAt(_executer.PlayerInfo.Position.X + 1, _executer.PlayerInfo.Position.Y) == TileContent.Wall)
            {
                isGoingHome = true;
            }
            if (_executer.PlayerInfo.CarriedResources < _executer.PlayerInfo.CarryingCapacity && _executer.Map.GetTileAt(_executer.PlayerInfo.Position.X + 1, _executer.PlayerInfo.Position.Y) == TileContent.Resource)
            {
                return AIHelper.CreateCollectAction(new Point(1, 0));
            }
            else
            {
                int direction = isGoingHome ? -1 : 1;
                return AIHelper.CreateMoveAction(new Point(direction, 0));
            }
        }
    }
}