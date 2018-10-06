using System;
using System.Linq;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class MiningBehaviour : Behaviour
    {
        public MiningBehaviour(BehaviourExecuter executer) : base(executer) { }

        private bool isGoingHome = false;
        private List<Point> _mines = new List<Point>();

        public override bool Evaluate()
        {
            return true;
        }

        public override string Execute()
        {
            _mines = _executer.Map.GetVisibleTiles().Where(p => p.TileType == TileContent.Resource).Select(p => p.Position).ToList();

            if (_executer.PlayerInfo.CarriedResources >= _executer.PlayerInfo.CarryingCapacity)
            {
                return GoHome();
            }
            else
            {
                return GoMine();
            }
        }

        private string GoHome()
        {
            var house = _executer.PlayerInfo.HouseLocation;
            var pos = _executer.PlayerInfo.Position;

            var dir = house - pos;
            var point = Math.Abs(dir.X) > Math.Abs(dir.Y) ? new Point(Math.Sign(dir.X), 0) : new Point(0, Math.Sign(dir.Y));
            var newPos = _executer.PlayerInfo.Position + point;
            if (_executer.Map.GetTileAt(newPos.X, newPos.Y) == TileContent.Wall)
            {
                return AIHelper.CreateMeleeAttackAction(point);
            }
            return AIHelper.CreateMoveAction(point);
        }

        private string GoMine()
        {
            var minePosition = _executer.Map.GetTileAt(_executer.PlayerInfo.Position.X + 1, _executer.PlayerInfo.Position.Y) == TileContent.Resource ? new Point(1, 0) :
            _executer.Map.GetTileAt(_executer.PlayerInfo.Position.X - 1, _executer.PlayerInfo.Position.Y) == TileContent.Resource ? new Point(-1, 0) :
            _executer.Map.GetTileAt(_executer.PlayerInfo.Position.X, _executer.PlayerInfo.Position.Y + 1) == TileContent.Resource ? new Point(0, 1) :
            _executer.Map.GetTileAt(_executer.PlayerInfo.Position.X, _executer.PlayerInfo.Position.Y - 1) == TileContent.Resource ? new Point(0, -1) : new Point(0, 0);
            if (minePosition.X != 0 || minePosition.Y != 0)
            {
                return AIHelper.CreateCollectAction(minePosition);
            }

            if (_mines.Count <= 0)
            {
                return AIHelper.CreateMoveAction(new Point(0, 1));
            }

            var closest = _mines[0];
            for (int i = 0; i < _mines.Count; i++)
            {
                if (Math.Abs(_mines[i].X - _executer.PlayerInfo.Position.X) + Math.Abs(_mines[i].Y - _executer.PlayerInfo.Position.Y) < Math.Abs(closest.X - _executer.PlayerInfo.Position.X) + Math.Abs(closest.Y - _executer.PlayerInfo.Position.Y))
                {
                    closest = _mines[i];
                }
            }

            var dir = closest - _executer.PlayerInfo.Position;
            var point = Math.Abs(dir.X) > Math.Abs(dir.Y) ? new Point(Math.Sign(dir.X), 0) : new Point(0, Math.Sign(dir.Y));

            var newPos = _executer.PlayerInfo.Position + point;
            if (_executer.Map.GetTileAt(newPos.X, newPos.Y) == TileContent.Wall)
            {
                return AIHelper.CreateMeleeAttackAction(point);
            }
            return AIHelper.CreateMoveAction(point);
        }
    }
}