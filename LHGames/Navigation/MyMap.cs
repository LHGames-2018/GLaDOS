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

        public Tile GetClosestTileOfType(TileContent tileType, Point origin)
        {
            var distanceMin = int.MaxValue;
            Tile tile = null;
            foreach (var t in GetAllTilesOfType(tileType))
            {
                var path = ShortestPathNextTo(origin, t.Position);
                var distance = path?.Count ?? int.MaxValue;
                if (distanceMin > distance)
                {
                    distanceMin = distance;
                    tile = t;
                }
            }

            return tile;
        }
        
        public List<Tile> GetAllTilesOfType(TileContent tileType)
        {
            return _graph.Nodes.Select(n => n.Tile).Where(t => t.TileType == tileType).ToList();
        }

        public Tile HasTileOfTypeAdjacentTo(TileContent tileContent, Point pos)
        {
            return NavUtils.ForEachAdjacentTile(pos).Select(point => _graph.NodeAt(point).Tile).FirstOrDefault(tileAt => tileAt.TileType == tileContent);
        }

        public List<Node> ShortestPathNextTo(Point pos, Point origin)
        {
            var paths = new List<List<Node>>(4);            
            foreach (var point in NavUtils.ForEachAdjacentTile(pos))
            {
                _aStar = new AStar(_graph);
                paths.Add(_aStar.FindShortestPath(origin, point));
            }
            paths.RemoveAll(path => path == null);
            paths = paths.OrderBy(path => path.Count).ToList();
            return paths.FirstOrDefault();
        }
    }
}