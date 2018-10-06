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
                return AIHelper.CreateMoveAction(returnHomePath[1].Tile.Position - playerPos);
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
                    var closestMine = _executer.Map.GetClosestTileOfType(TileContent.Resource, playerPos);

                    var minePath = _executer.Map.ShortestPathNextTo(playerPos, closestMine.Position);
                    
//                    var minePath = _executer.Map.PathBetween(playerPos, closestMine.Position - new Point(1 , 0));
                    
                    return AIHelper.CreateMoveAction(minePath[1].Tile.Position - playerPos);
                }
            }
        }
    }
}