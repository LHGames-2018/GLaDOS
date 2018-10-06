using System;
using System.Collections.Generic;
using System.Linq;
using LHGames.Helper;
using LHGames.Navigation.Pathfinding;

namespace LHGames.Navigation
{
    public class MyMap
    {
        private readonly Graph _graph = new Graph();
        private AStar _aStar;
        
        internal void MergeMap(Map map)
        {   
            foreach (var visibleTile in map.GetVisibleTiles())
            {
                _graph.UpdateNode(visibleTile.Position, visibleTile);
            }
            _aStar = new AStar(_graph);
        }

        public List<Node> PathBetween(Point start, Point end)
        {
            if (_aStar == null)
                _aStar = new AStar(_graph);

            return _aStar.FindShortestPath(start, end);
        }

        public Point GetClosestTileOfType(TileContent tileType, Point origin)
        {
            var distanceMin = Int32.MaxValue;
            Point point = null;
            foreach (var tile in GetAllTilesOfType(tileType))
            {
                var delta = tile.Position - origin;
                var distance = Math.Abs(delta.X) + Math.Abs(delta.Y);
                if (distanceMin > distance)
                {
                    distanceMin = distance;
                    point = tile.Position;
                }
            }

            return point;
        }
        
        public List<Tile> GetAllTilesOfType(TileContent tileType)
        {
            return _graph.Nodes.Select(n => n.Tile).Where(t => t.TileType == tileType).ToList();
        }
    }
}