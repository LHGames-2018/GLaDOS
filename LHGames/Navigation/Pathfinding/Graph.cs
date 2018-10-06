using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LHGames.Helper;

namespace LHGames.Navigation.Pathfinding
{
    public class Graph
    {
        // TODO really readonly
        private readonly Dictionary<Point, Node> _nodes = new Dictionary<Point, Node>();
        private readonly HashSet<Edge> _edges = new HashSet<Edge>();

        public ImmutableList<Node> Nodes => _nodes.Values.ToImmutableList();
        public ImmutableList<Edge> Edges => _edges.ToImmutableList();

        public void UpdateNode(Point pos, Tile tile)
        {
            if (_nodes.ContainsKey(pos))
            {
                _nodes[pos].Tile = tile;
            }
            else
            {
                var newNode = new Node(tile);
                _nodes[pos] = newNode;
                foreach (var adjacent in NavUtils.ForEachAdjacentTile(tile.Position))
                {
                    // Do not add if non-existant
                    if (!_nodes.ContainsKey(adjacent))
                     continue;
                        
                    _edges.Add(new Edge(newNode, _nodes[adjacent]));
                    _edges.Add(new Edge(_nodes[adjacent], newNode));
                }
            }
        }

        public Node NodeAt(Point pos)
        {
            return _nodes[pos];
        }
        
        public Edge EdgeBetweenNodes(Node src, Node dst)
        {
            return _edges.FirstOrDefault(edge => edge.Source == src && edge.Destination == dst);
        }
        
        
    }
}