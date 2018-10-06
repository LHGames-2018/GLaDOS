using LHGames.Helper;

namespace LHGames.Navigation.Pathfinding
{
    public class Node
    {
        public Tile Tile { get; set; }

        public Node(Tile tile)
        {
            Tile = tile;
        }

        public virtual double HeuristicToNode(Node n)
        {
            return Point.DistanceSquared(Tile.Position, n.Tile.Position);
        }

        public override string ToString()
        {
            return $"Node[{Tile.TileType}@{Tile.Position}]";
        }
    }
}