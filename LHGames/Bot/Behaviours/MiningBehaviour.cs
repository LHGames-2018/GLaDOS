using System;
using System.Collections.Generic;
using System.Linq;
using LHGames.Helper;
using LHGames.Navigation;
using LHGames.Navigation.Pathfinding;

namespace LHGames.Bot.Behaviours
{
    public class MiningBehaviour : Behaviour
    {

        public MiningBehaviour(BehaviourExecuter executer) : base(executer)
        {
        }

        public override bool Evaluate()
        {
            return true;
        }

        public override string Execute()
        {
            var playerPos = _executer.PlayerInfo.Position;
            if (_executer.PlayerInfo.CarriedResources >= _executer.PlayerInfo.CarryingCapacity)
            {
                // Go home you're ~drunk~ full
                var returnHomePath = _executer.Map.PathBetween(playerPos, _executer.PlayerInfo.HouseLocation);
                return MoveTo(returnHomePath[1].Tile.Position - playerPos);
            }
            else
            {
                var adjacentMine = _executer.Map.HasTileOfTypeAdjacentTo(TileContent.Resource, playerPos);
                if (adjacentMine != null)
                {
                    // if we're next to a mine, mine
                    return AIHelper.CreateCollectAction(adjacentMine.Position - playerPos);
                }
                else
                {
                    // Go to mine
                    var resource = _executer.Map.GetClosestTileOfType(TileContent.Resource, playerPos);
                    var minePath = _executer.Map.ShortestPathNextTo(resource.Position, playerPos);
                    
                    return MoveTo(minePath[1].Tile.Position - playerPos);
                }
            }
        }

        private string MoveTo(Point direction)
        {
            var tileToGo = _executer.Map.TileAt(direction + _executer.PlayerInfo.Position);
            if (tileToGo.TileType == TileContent.Wall)
            {
                return AIHelper.CreateMeleeAttackAction(direction);
            }
            else
            {
                return AIHelper.CreateMoveAction(direction);
            }
        }
    }
}