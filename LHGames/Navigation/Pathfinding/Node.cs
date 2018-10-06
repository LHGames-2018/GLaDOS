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
            return 0; //TODO
        }

        public override string ToString()
        {
            return $"Node[{Tile.TileType}@{Tile.Position}]";
        }
    }
}