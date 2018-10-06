using System.Collections.Generic;
using System.Linq;
using LHGames.Helper;

namespace LHGames.Navigation.Pathfinding
{
    public class AStar
    {
        private Graph _graph;
        private Dictionary<Node, AStarNode> _aStarNodes;
        
        public AStar(Graph graph)
        {
            _graph = graph;
            _aStarNodes = new Dictionary<Node, AStarNode>(graph.Nodes.Count);
            
            // Create Dic Node ==> AStarNode
            foreach (var graphNode in graph.Nodes)
            {
                _aStarNodes.Add(graphNode, new AStarNode(graphNode));
            }
            
            foreach (var graphEdge in graph.Edges)
            {
                _aStarNodes[graphEdge.Source].Neighbours.Add(_aStarNodes[graphEdge.Destination], graphEdge.Weight);
            }
        }

        public List<Node> FindShortestPath(Point start, Point end)
        {
            var startNode = _graph.NodeAt(start);
            var endNode = _graph.NodeAt(end);

            return startNode == null || endNode == null ? null : FindShortestPath(startNode, endNode);
        }
        
        public List<Node> FindShortestPath(Node start, Node end)
        {
            if (!_graph.Nodes.Contains(start) || !_graph.Nodes.Contains(end)) return null;
            
            // Set HScore for all nodes
            foreach (var aStarNode in _aStarNodes.Values)
            {
                aStarNode.HScore = aStarNode.Node.HeuristicToNode(end);
            }
            
            // TODO: PriorityQueue (Ordered set?)
            var openSet = new List<AStarNode>();
            var closedSet = new HashSet<AStarNode>();
            var subPaths = new Dictionary<AStarNode, AStarNode>();

            var startNode = _aStarNodes[start];
            startNode.GScore = 0;
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // Simulate priority queue
                openSet = openSet.OrderBy(n => n.FScore).ToList();
                var node = openSet[0];
                openSet.Remove(node);

                if (node.Node == end)
                {
                    return CreatePath(subPaths, node);
                }

                closedSet.Add(node);
                
                foreach (var (neighbour, distance) in node.Neighbours)
                {
                    if (closedSet.Contains(neighbour))
                    {
                        // Already explored
                        continue;
                    }

                    var tentativeGScore = distance + node.GScore;
                    if (tentativeGScore >= neighbour.GScore)
                    {
                        // There's a more optimal way to get to neighbour 
                        continue;
                    }

                    neighbour.GScore = tentativeGScore;
                    if (!subPaths.ContainsKey(neighbour))
                    {
                        subPaths.Add(neighbour, node);
                    }
                    openSet.Add(neighbour);
                }
            }
            
            return null; // ¯\_(ツ)_/¯
        }

        private static List<Node> CreatePath(IReadOnlyDictionary<AStarNode, AStarNode> subPaths, AStarNode destination)
        {
            var endNode = destination;

            var path = new List<Node> {endNode.Node};

            while (subPaths.ContainsKey(endNode))
            {
                endNode = subPaths[endNode];
                path.Add(endNode.Node);
            }

            path.Reverse();
            return path;
        }

        private class AStarNode
        {
            public Dictionary<AStarNode, double> Neighbours { get; } = new Dictionary<AStarNode, double>();
            public Node Node { get; }
            public double GScore { get; set; } = double.MaxValue;
            public double HScore { get; set; }
            public double FScore => GScore + HScore;
            
            
            public AStarNode(Node node)
            {
                Node = node;
            }

            public override string ToString()
            {
                return $"{base.ToString()}[G: {GScore}, H: {HScore}, F: {FScore}]";
            }
        }

    }
    
}