using System;
using LHGames.Helper;

namespace LHGames.Navigation.Pathfinding
{
    public class Edge
    {
        public Node Source { get; set; }
        public Node Destination { get; set; }
        public double Weight => WeightForTileType(Destination.Tile.TileType);

        public Edge(Node source, Node destination)
        {
            Source = source;
            Destination = destination;
        }

        public override string ToString()
        {
            return $"Edge: {Source} --({Weight})--> {Destination}";
        }

        public static double WeightForTileType(TileContent content)
        {
            double weight = 0.0;
            switch (content)
            {
                case TileContent.Lava:
                case TileContent.Resource:
                case TileContent.Shop:
                case TileContent.Player:
                    weight = Double.MaxValue;
                    break;
                case TileContent.Wall:
                    weight = 5.0;
                    break;
                case TileContent.House:
                case TileContent.Empty:
                    weight = 1.0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(content), content, null);
            }

            return weight;
        }
    }
}