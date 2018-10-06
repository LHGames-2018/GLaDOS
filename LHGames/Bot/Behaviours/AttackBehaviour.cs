using System;
using System.Linq;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class AttackBehaviour : Behaviour
    {
        public AttackBehaviour(BehaviourExecuter executer) : base(executer) { }

        private bool isGoingHome = false;
        private List<Point> _players = new List<Point>();

        public override bool Evaluate()
        {
            return true;
        }

        public override string Execute()
        {
            _players = _executer.Map.GetVisibleTiles().Where(p => p.TileType == TileContent.Player && p.Position != _executer.PlayerInfo.Position).Select(p => p.Position).ToList();

            if (_players.Count <= 0)
            {
                return Explore();
            }
            else
            {
                return Attack();
            }
        }

        private string Explore()
        {
            if (_executer.Map.GetTileAt(_executer.PlayerInfo.Position.X + 1, _executer.PlayerInfo.Position.Y) == TileContent.Wall)
            {
                return AIHelper.CreateMeleeAttackAction(new Point(1, 0));
            }
            return AIHelper.CreateMoveAction(new Point(1, 0));
        }

        private string Attack()
        {
            _players.Sort((a, b) => (int)Point.DistanceSquared(_executer.PlayerInfo.Position, a) - (int)Point.DistanceSquared(_executer.PlayerInfo.Position, b));
            var target = _players.First();

            var dir = target - _executer.PlayerInfo.Position;
            if (Point.DistanceSquared(target, _executer.PlayerInfo.Position) == 1)
            {
                return AIHelper.CreateMeleeAttackAction(dir);
            }

            var point = Math.Abs(dir.X) > Math.Abs(dir.Y) ? new Point(Math.Sign(dir.X), 0) : new Point(0, Math.Sign(dir.Y));

            var newPos = _executer.PlayerInfo.Position + point;
            if (_executer.Map.GetTileAt(newPos.X, newPos.Y) == TileContent.Wall)
            {
                return AIHelper.CreateMeleeAttackAction(point);
            }
            if (_executer.Map.GetTileAt(newPos.X, newPos.Y) == TileContent.Resource)
            {
                return AIHelper.CreateCollectAction(point);
            }
            if (_executer.Map.GetTileAt(newPos.X, newPos.Y) == TileContent.House)
            {
                return AIHelper.CreateMoveAction(new Point(0, 1));
            }
            return AIHelper.CreateMoveAction(point);
        }
    }
}